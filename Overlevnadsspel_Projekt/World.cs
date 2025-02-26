public class World
{
    Random random = new();
    

    public int foodCount;
    public int treeCount;
    public int damage;
    
    public int hungerDamage;
    public int heatDamage;

    //Ger skada ifall hunger och heat är under gränsen
     public void TryGiveDamage(float hunger, float heat, ref float hp)
    {
        if (hunger <= 10 || heat <= 30)
        {
            hp -= damage;
        }
    }

    //Slumpar ihop variabler som håller skador och antal resurser
    public void Randomize()
    {
        //Mängd mat och ved
        foodCount = random.Next(0, 2);
        treeCount = random.Next(0, 2);
        damage = random.Next(40, 70); // Mängd HP man förlorar
        //Mängd förlorad värme och hunger
        hungerDamage = random.Next(30, 60);
        heatDamage = random.Next(30, 60);
    }
}
