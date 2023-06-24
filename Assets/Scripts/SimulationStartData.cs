using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SimulationStartData
{
    public static float Herbivore_AgilityMin { get; set; }
    public static float Herbivore_AgilityMax { get; set; }
    public static float Herbivore_StrengthMin { get; set; }
    public static float Herbivore_StrengthMax { get; set; }
    public static float Herbivore_SightMin { get; set; }
    public static float Herbivore_SightMax { get; set; }
    public static float Herbivore_SizeMin { get; set; }
    public static float Herbivore_SizeMax { get; set; }

    
    public static float Carnivore_AgilityMin { get; set; }
    public static float Carnivore_AgilityMax { get; set; }
    public static float Carnivore_StrengthMin { get; set; }
    public static float Carnivore_StrengthMax { get; set; }
    public static float Carnivore_SightMin { get; set; }
    public static float Carnivore_SightMax { get; set; }
    public static float Carnivore_SizeMin { get; set; }
    public static float Carnivore_SizeMax { get; set; }

    public static int Organisms_Number { get; set; }
    public static float Organisms_Proportion { get; set; }

    public static bool New_Simulation { get; set; }
    public static string Savefile_Name { get; set; }
}
