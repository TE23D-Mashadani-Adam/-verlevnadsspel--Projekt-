using System.Data;
using System.Runtime.Serialization.Json;

Random random = new();

Console.WriteLine("Hej och välkommen, du behöver överleva genom att äta" +
" och göra eld för att bli varm, var försiktig så att du inte förlorar mycket hp" +
" och dör!,din hp minskar när hunger och värme går under noll, tryck vidare för att starta spelet");
Console.ReadLine();
Console.Clear();


Player player = new();
World world = new();


List<string> dayBasedInfo = [];

int daysCount = 1;
int points = 0;


while (player.hp > 0)
{
    world.Randomize();

    string answer = "";

    player.hunger -= world.hungerDamage;
    player.heat -= world.heatDamage;

    //Algoritm för att äta och göra eld med ved
    while (answer.ToLower() != "slut")
    {
        Console.WriteLine($"Hunger: {player.hunger} Värme: {player.heat} HP: {player.hp}");
        showItemsMessage(world.foodCount, world.treeCount);
        answer = Console.ReadLine().ToLower();

        switch (answer)
        {
            case "m":
                TryEatFood(ref world.foodCount, ref world.hungerDamage);
                break;
            case "v":
                TryMakeFire(ref world.treeCount, ref world.heatDamage);
                break;
            case "in":
                player.OpenInventory();
                string inAnswer = Console.ReadLine();
                switch (inAnswer)
                {
                    case "m":
                        player.EatFromInventory();
                        break;
                    case "v":
                        player.MakeFireFromInventory();
                        break;
                }
                break;
            default:
                Console.WriteLine("Ogiltigt val, försök igen!");
                break;
        }

        if (answer == "s")
        {
            break;
        }

    }

    Player.storedFood += world.foodCount;
    Player.storedTree += world.treeCount;

    Console.WriteLine("\n" + $"Dag: {daysCount} Hunger: {player.hunger} Värme: {player.heat} HP: {player.hp}");

    //Daglig information lagras i listan
    dayBasedInfo.Add($"Hunger: {player.hunger} Värme: {player.heat} HP: {player.hp}");

    //Hp minskar när damage och heat sjunker ner
    if (player.hunger <= 10 || player.heat <= 30)
    {
        player.hp -= world.damage;
    }

    Console.WriteLine("Tryck vidare för att avsluta dagen");
    Console.ReadLine();

    if (player.hp <= 0)
    {
        Console.WriteLine("Du är död, om du vill visa fullständig statistik, skriv 's'");
        string statAnswer = Console.ReadLine();
        points = ShowStats(dayBasedInfo, daysCount, points, statAnswer);
        Console.WriteLine($"Du hade samlat {points} poäng!");

        Console.ReadLine();
        break;
    }

    daysCount++;

}

int h = 9;
int u = 19;

Console.ReadLine();

void showItemsMessage(float foodCount, float treeCount)
{
    string availbleItems = $"Mat: {foodCount} Ved: {treeCount}";
    Console.WriteLine(availbleItems + "\n" + "För mat, skriv m, för ved, skriv v"
    + "För att avsluta, skriv 's', för att öppna inventory, skriv 'in'");
}

int ShowStats(List<string> list, int daysCount, int points, string statAnswer)
{
    for (int i = 0; i < daysCount; i++)
    {
        if (statAnswer == "s")
        {
            Console.WriteLine($"Dag: {i + 1} Status: {list[i]}");
        }
        else
        {
            Console.WriteLine($"Dag: {i + 1} Status: Överlevde!");
        }
    }
    points = daysCount + 1;

    return points;
}

void TryEatFood(ref int foodCount, ref int hungerDamage)
{
    if (foodCount > 0)
    {
        foodCount = player.Eat(foodCount);
        hungerDamage = 0;
    }
    else { Console.WriteLine("Det finns ingen mat!"); }
}

void TryMakeFire(ref int treeCount, ref int heatDamage)
{
    if (treeCount > 0)
    {
        treeCount = player.Eat(treeCount);
        heatDamage = 0;
    }
    else { Console.WriteLine("Det finns ingen trä!"); }
}



