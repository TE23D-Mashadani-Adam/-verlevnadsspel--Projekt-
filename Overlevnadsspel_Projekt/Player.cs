public class Player
{
    public float hp = 100;
    public float hunger = 100;
    public float heat = 100;
    public int damage;
    

     public int storedFood;
     public int storedTree;

    //Höjer spelarens hunger bar genom att äta
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

    //Gör eld och höjer spelarens "heat" bar
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

    //Öppnar invenotry och visar upp dens innehåll
    public void OpenInventory()
    {
        Console.WriteLine($"Stored Food: {storedFood}" + "\n"
        + $"Store trees: {storedTree}");
        Console.WriteLine("Skriv m för att äta mat, skriv v för ved, tryck vidare för att stänga inventory");
    }

    //Spelaren äter från inventoryn
    public void EatFromInventory()
    {
        if (storedFood > 0)
        { storedFood = Eat(storedFood); }
        else { Console.WriteLine("Det finns ingen mat i din inventory"); }
    }

    //Spelaren gör eld med ved från inventory
    public void MakeFireFromInventory()
    {
        if (storedTree > 0)
        { storedTree = MakeFire(storedTree); }
        else { Console.WriteLine("Det finns ingen trä i din inventory"); }
    }

    

   

}
