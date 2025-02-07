public class Player
{
    public float hp = 100;
    public float hunger = 100;
    public float heat = 100;

    static public int storedFood;
    static public int storedTree;

    List<int> inventory = [storedFood, storedTree];


    public int Eat(int foodCount)
    {
        if (hunger <= 60) { hunger += 40; }
        else
        {
            hunger += 100 - hunger; // Gör så att hungern går upp till max 100
        }
        foodCount--;
        return foodCount;
    }

    public int MakeFire(int treeCount)

    {
        if (heat <= 50) { heat += 50; }
        else
        {
            heat += 100 - heat;
        }
        treeCount--;
        return treeCount;
    }

    public void OpenInventory()
    {
        Console.WriteLine($"Stored Food: {storedFood}" + "\n"
        + $"Store trees: {storedTree}");
        Console.WriteLine("Skriv m för att äta mat, skriv v för ved");
    }

    public void EatFromInventory()
    {
        if (storedFood > 0)
        { storedFood = Eat(storedFood); }
        else { Console.WriteLine("Det finns ingen mat i din inventory"); }
    }

    public void MakeFireFromInventory()
    {
        if (storedTree > 0)
        { storedTree = MakeFire(storedTree); }
        else { Console.WriteLine("Det finns ingen trä i din inventory"); }
    }

}
