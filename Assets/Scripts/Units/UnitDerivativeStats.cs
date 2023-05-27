using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu]
public class UnitDerivativeStats : ScriptableObject
{
    private static Dictionary<string, float> parameters = 
    new Dictionary<string, float>() {
        {"StaminaSize", -1},
        {"StaminaAgility", 1},
        {"HealthSize", 1},
        {"MaxSpeedSize", -1},
        {"MaxSpeedAgility", 1},
        {"MaxEnergy", -1},
        {"MaxEnergySize", 1},
        {"DamageStrength", 1},
        {"ThreatSize", 1},
        {"ThreatStrength", 1},
    };
    private float energy;
    private float stamina;
    private float maxSpeed;
    private float maxEnergy;
    private float energyEfficiency;
    private float range;
    private float damage;
    public float getParameter(string statName, string baseStatName) {
        return parameters[$"{statName}{baseStatName}"];
    }

    public float Energy {
        get { return energy; }
        set { this.energy = value;}
    }
    public float Stamina => stamina;
    public float MaxSpeed => maxSpeed;
    public float MaxEnergy => maxEnergy;
    public float EnergyEfficiency => energyEfficiency;
    public float Range => range;
    public float Damage => damage;

    public string Info =>
        string.Join(
            ",",
            new string[] {
                Energy.ToString(),
                MaxSpeed.ToString(),
                MaxEnergy.ToString(),
                EnergyEfficiency.ToString(),
                Range.ToString(),
                Damage.ToString(),
            }
        );

    public void PrintInfo(){
        Debug.Log(Info);
    }

    public void InitFromBase(UnitBaseStats stats) {
        maxEnergy = EnergyFromBase(stats);
        maxSpeed = MaxSpeedFromBase(stats);
        energy = maxEnergy;
        energyEfficiency = EnergyEfficiencyFromBase(stats);
        range = RangeFromBase(stats);
        damage = DamageFromBase(stats);
    }

    public static float EnergyFromBase(UnitBaseStats stats) {
        return (stats.agility + 2 * stats.strength) / 3;
    }

    public static float MaxSpeedFromBase(UnitBaseStats stats) {
        return (3 * stats.agility + stats.strength) / 4;
    }

    public static float EnergyEfficiencyFromBase(UnitBaseStats stats) {
        return (4 * stats.agility + 2 * stats.stealth) / 6;
    }

    public static float RangeFromBase(UnitBaseStats stats) {
        return (5 * stats.sight + 4 * stats.sense + stats.agility) / 10;
    }

    public static float DamageFromBase(UnitBaseStats stats) {
        return (stats.strength * stats.size) / 2;
    }
}
