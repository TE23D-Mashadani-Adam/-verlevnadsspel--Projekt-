using System.Data;

Random random = new();

Console.WriteLine("Hej och välkommen, du behöver överleva genom att äta" +
" och göra eld för att bli varm, var försiktig så att du inte förlorar mycket hp" +
" och dör!, tryck vidare för att starta spelet");
Console.ReadLine();
Console.Clear();


int hungerDamage = 0;
int heatDamage = 0;

Player player = new();


List<string> dayBasedInfo = [];

int daysCount = 1;
int points = 0;

int storedFood = 0;
int storedTree = 0;

while (player.hp > 0)
{
    //Mängd mat och ved
    int foodCount = random.Next(0, 2);
    int treeCount = random.Next(0, 2);
    int damage = random.Next(40, 70);
    //Mängd förlorad värme och hunger
    hungerDamage = random.Next(30, 60);
    heatDamage = random.Next(30, 60);

    string answer = "";

    foodCount += storedFood;
    treeCount += storedTree;

    player.hunger -= hungerDamage;
    player.heat -= heatDamage;

    //Algoritm för att äta och göra eld med ved
    while (answer.ToLower() != "slut")
    {
        Console.WriteLine($"Hunger: {player.hunger} Värme: {player.heat} HP: {player.hp}");
        showItemsMessage(foodCount, treeCount);
        answer = Console.ReadLine().ToLower();

        switch (answer)
        {
            case "m":
                if (foodCount > 0)
                {
                    foodCount = player.eat(foodCount);
                    hungerDamage = 0;
                }
                else { Console.WriteLine("Det finns ingen mat"); }
                break;
            case "v":
                if (treeCount > 0)
                {
                    treeCount = player.makeFire(treeCount);
                    heatDamage = 0;
                }
                else { Console.WriteLine("Det finns ingen trä"); }
                break;
        }

        if (answer == "slut")
        {
            break;
        }

    }

    Console.WriteLine("\n" + $"Dag: {daysCount} Hunger: {player.hunger} Värme: {player.heat} HP: {player.hp}");


    storedFood = foodCount;
    storedTree = treeCount;



    dayBasedInfo.Add($"Hunger: {player.hunger} Värme: {player.heat} HP: {player.hp}");


    //Hp minskar när damage och heat sjunker ner
    if (player.hunger <= 10 || player.heat <= 30)
    {
        player.hp -= damage;
    }

    Console.WriteLine("Tryck vidare för att avsluta dagen");
    Console.ReadLine();

    if (player.hp <= 0)
    {
        Console.WriteLine("Du är död");
        points = ShowStats(dayBasedInfo, daysCount, points);
        Console.WriteLine($"Du hade samlat {points} poäng!");

        Console.ReadLine();
        break;
    }

    daysCount++;

}

Console.ReadLine();

void showItemsMessage(float foodCount, float treeCount)
{
    string availbleItems = $"Mat: {foodCount} Ved: {treeCount}";
    Console.WriteLine(availbleItems + "\n" + "För mat, skriv m, för ved, skriv v"
    + "För att avsluta, skriv 'slut'");
}

int ShowStats(List<string> list, int daysCount, int points)
{
    for (int i = 0; i < daysCount; i++)
    {
        Console.WriteLine($"Dag: {i + 1} Status: {list[i]}");
    }
    points = daysCount + 1;

    return points;
}

Console.ReadLine();



