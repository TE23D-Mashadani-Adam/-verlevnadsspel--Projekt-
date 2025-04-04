public class World
{
    Random random = new();


    public int foodCount;
    public int treeCount;
    public int damage;

    public int hungerDamage;
    public int heatDamage;

    int treeLowOdz;
    int foodLowOdz;
    int treeNormalOdz;
    int foodNormalOdz;
    int hungerDamageOdz;
    int heatDamageOdz;
    int hpDamageOdz;


    //Ger skada ifall hunger och heat är under gränsen
    public void TryGiveDamage(float hunger, float heat, ref float hp)
    {
        if (hunger <= 10 || heat <= 30)
        {
            hp -= damage;
        }
    }

    //Bestämmer odzen för variablerna i world
    public void SetOdz()
    {
        treeLowOdz = random.Next(0, 2);
        foodLowOdz = random.Next(0, 2);
        treeNormalOdz = random.Next(1, 4);
        foodNormalOdz = random.Next(1, 4);
        hungerDamageOdz = random.Next(30, 60);
        heatDamageOdz = random.Next(30, 60);
        hpDamageOdz = random.Next(40, 70);
    }

    //Slumpar ihop variabler som håller skador och antal resurser
    public void Randomize(int daysCount)
    {
        //Mängd mat och ved
        if (daysCount <= 3)
        {
            foodCount = foodNormalOdz;
            treeCount = treeNormalOdz;
        }
        else
        {
            foodCount = treeLowOdz;
            treeCount = foodLowOdz;
        }
        damage = hpDamageOdz; // Mängd HP man förlorar
        //Mängd förlorad värme och hunger
        hungerDamage = hungerDamageOdz;
        heatDamage = heatDamageOdz;
    }
}
