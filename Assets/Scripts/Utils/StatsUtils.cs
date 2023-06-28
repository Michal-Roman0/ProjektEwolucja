public static class StatsUtils
{
    private static int herbivoreCount = 0;
    private static int carnivoreCount = 0;


    public static int HerbivoresCount
    {
        get => herbivoreCount;
        private set
        {
            herbivoreCount = value;
        }
    }
    public static int CarnivoreCount
    {
        get => carnivoreCount;
        private set
        {
            carnivoreCount = value;
        }
    }

    public static void AddUnit(string unitTag)
    {
        switch (unitTag)
        {
            case "Herbivore":
                HerbivoresCount++;
                break;
            case "Carnivore":
                CarnivoreCount++;
                break;
        }

        UI_Controller.instance.UpdateUnitCounts();
    }

    public static void SubtractUnit(string unitTag)
    {
        switch (unitTag)
        {
            case "Herbivore":
                HerbivoresCount--;
                break;
            case "Carnivore":
                CarnivoreCount--;
                break;
        }

        UI_Controller.instance.UpdateUnitCounts();
    }
}
