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

int daysCount = 1;

while (hp > 0)
{
    //Mängd mat och ved
    int foodCount = random.Next(0, 2);
    int treeCount = random.Next(0, 2);
    int damage = random.Next(40, 70);
    //Mängd förlorad värme och hunger
    int hungerDamage = random.Next(10, 40);
    int heatDamage = random.Next(10, 40);

    string availbleItems = "";
    string answer = "";

    //Algoritm för att äta och göra eld med ved
    while (answer.ToLower() != "slut")
    {
        showItemsMessage(availbleItems, foodCount, treeCount);
        answer = Console.ReadLine();

        if (answer.ToLower() == "m" && foodCount > 0)
        {
            hungerDamage = 0;
            if (hunger <= 60) { hunger += 40; }
            else
            {
                hunger += 100 - hunger; // Gör så att hungern går upp till max 100
            }
            foodCount--;
        }
        else if (answer.ToLower() == "m" && foodCount == 0)
        { Console.WriteLine("Det finns ingen mat"); }

        else if (answer.ToLower() == "v" && treeCount > 0)
        {
            heatDamage = 0;
            if (heat <= 50) { heat += 50; }
            else
            {
                heat += 100 - heat;
            }
            treeCount--;
        } else if (answer.ToLower() == "v" && treeCount == 0)
        { Console.WriteLine("Det finns inga träd"); }

        Console.WriteLine($"Hunger: {hunger} Värme: {heat} HP: {hp}");

    }


    hunger -= hungerDamage;
    heat -= heatDamage;

    //Hp minskar när damage och heat sjunker ner
    if (hunger <= 10 || heat <= 30)
    {
        hp -= damage;
    }

    Console.WriteLine($"Hunger: {hunger} Värme: {heat} HP: {hp} Dag: {daysCount}");
    Console.WriteLine("Tryck vidare för att avsluta dagen");
    Console.ReadLine();

    if (hp <= 0)
    {
        Console.WriteLine("Du är död");
        break;
    }

    daysCount++;

}

void showItemsMessage(string availbleItems, float foodCount, float treeCount)
{
    availbleItems = $"Mat: {foodCount} Ved: {treeCount}";
    Console.WriteLine(availbleItems + "\n" + "För mat, skriv m, för ved, skriv v"
    + "För att avsluta, skriv 'slut'");
}

Console.ReadLine();



