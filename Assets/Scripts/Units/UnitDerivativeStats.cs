using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu]
public class UnitDerivativeStats : ScriptableObject
{
    public static Dictionary<string, float> parameters =
    new Dictionary<string, float>() {
        {"DamageStrength", 1},
        
        {"MaxEnergyBase", 2},
        {"MaxEnergyAgility", -0.2f},
        {"MaxEnergyMorality", -0.2f},
        {"MaxEnergySense", -0.2f},
        {"MaxEnergySight", -0.2f},
        {"MaxEnergyStrength", -0.2f},
        {"MaxEnergyStealth", -0.2f},
        {"MaxEnergySize", 0.2f},
        
        {"MaxHealthBase", 10},
        {"MaxHealthSize", 1},
        
        {"MaxSpeedBase", 1},
        {"MaxSpeedAgility", 0.1f},
        {"MaxSpeedSize", -0.1f},
        
        {"StaminaBase", 1},
        {"StaminaAgility", 0.2f},
        {"StaminaSize", -0.2f},
        
        {"ThreatBase", 0.8f},
        {"ThreatSize", 0.1f},
        {"ThreatStrength", 0.1f},

        {"RadiusSight", 4f},
    };

    public static float Parameter(string statName, string baseStatName)
    {
        return parameters.GetValueOrDefault(
            $"{statName}{baseStatName}",
            0f
        );
    }
    public float Energy { get; set; }
    public float Stamina { get; private set; }
    public float MaxSpeed { get; private set; }
    public float MaxEnergy { get; private set; }
    public float Damage { get; private set; }
    public float Threat { get; private set; }
    public float Radius { get; private set; }
    public int MaxHealth { get; private set; }

    public string Info =>
        string.Join(
            ",",
            new string[] {
                Energy.ToString(),
                Stamina.ToString(),
                MaxSpeed.ToString(),
                MaxEnergy.ToString(),
                Damage.ToString(),
                Threat.ToString(),
            }
        );

    public void PrintInfo()
    {
        Debug.Log(Info);
    }

    public void InitFromBase(UnitBaseStats stats)
    {
        Damage = DamageFromBase(stats);
        MaxEnergy = MaxEnergyFromBase(stats);
        MaxHealth = MaxHealthFromBase(stats);
        MaxSpeed = MaxSpeedFromBase(stats);
        Stamina = StaminaFromBase(stats);
        Threat = ThreatFromBase(stats);
        Radius = RadiusFromBase(stats);
        Energy = MaxEnergy;
    }

    private static float SumParametersForStat(string name, UnitBaseStats stats)
    {
        return parameters.Where(pair => pair.Key.Contains(name)).Sum(pair =>
        {
            string baseStatName = pair.Key.Replace(name, "");
            return pair.Value * stats.ToDictionary()
                .GetValueOrDefault(baseStatName, 0f);
        }) + Parameter(name, "Base");
    }

    private static int MaxHealthFromBase(UnitBaseStats stats)
    {
        return Convert.ToInt32(
            Math.Round(
                SumParametersForStat("MaxHealth", stats)
            )
        );
    }

    private static float StaminaFromBase(UnitBaseStats stats)
    {
        return SumParametersForStat("Stamina", stats);
    }

    private static float MaxEnergyFromBase(UnitBaseStats stats)
    {
        return SumParametersForStat("MaxEnergy", stats);
    }

    private static float MaxSpeedFromBase(UnitBaseStats stats)
    {
        return SumParametersForStat("MaxSpeed", stats);
    }

    private static float DamageFromBase(UnitBaseStats stats)
    {
        return SumParametersForStat("Damage", stats);
    }

    private static float ThreatFromBase(UnitBaseStats stats)
    {
        return SumParametersForStat("Threat", stats);
    }

    private static float RadiusFromBase(UnitBaseStats stats)
    {
        return SumParametersForStat("Radius", stats);
    }
}
