public class Player
{
    public float hp = 100;
    public float hunger = 100;
    public float heat = 100;

    public int eat(int foodCount)
    {
        if (hunger <= 60) { hunger += 40; }
        else
        {
            hunger += 100 - hunger; // Gör så att hungern går upp till max 100
        }
        foodCount--;
        return foodCount;
    }

    public int makeFire(int treeCount)
    {
            if (heat <= 50) { heat += 50; }
            else
            {
                heat += 100 - heat;
            }
            treeCount--;
            return treeCount;
    }
}
