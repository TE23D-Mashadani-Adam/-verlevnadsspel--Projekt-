using System.Data;

Random random = new();

Console.WriteLine("Hej och välkommen, du behöver överleva genom att äta" +
" och göra eld för att bli varm, var försiktig så att du inte förlorar mycket hp" +
" och dör!, tryck vidare för att starta spelet");
Console.ReadLine();
Console.Clear();

float hp = 100;
float hunger = 100;
float heat = 100;

while (hp > 0)
{
    //Mängd mat och ved
    int foodCount = random.Next(0, 5); 
    int treeCount = random.Next(0, 5);
    int damage = random.Next(40,70);
    //Mängd förlorad värme och hunger
    int hungerDamage = random.Next(10, 40);
    int heatDamage = random.Next(10, 40);

    hunger -= hungerDamage;
    heat -= heatDamage;

    if (hunger <= 10 || heat <= 30)
    {
        hp -= damage;
    }

    Console.WriteLine($"Hunger: {hunger} Värme: {heat} HP: {hp}");
    Console.WriteLine("Tryck vidare för att avsluta dagen");
    Console.ReadLine();

    if (hp <= 0)
    {
        Console.WriteLine("Du är död");
        break;
    }

}

Console.ReadLine();



