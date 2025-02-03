using System.Data;

Random random = new();

Console.WriteLine("Hej och välkommen, du behöver överleva genom att äta" +
" och göra eld för att bli varm, var försiktig så att du inte förlorar mycket hp" +
" och dör!, tryck vidare för att starta spelet");
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
                if (world.foodCount > 0)
                {
                    world.foodCount = player.Eat(world.foodCount);
                    world.hungerDamage = 0;
                }
                else { Console.WriteLine("Det finns ingen mat"); }
                break;
            case "v":
                if (world.treeCount > 0)
                {
                    world.treeCount = player.MakeFire(world.treeCount);
                    world.heatDamage = 0;
                }
                else { Console.WriteLine("Det finns ingen trä"); }
                break;
            case "in":
                player.OpenInventory();
                string inAnswer = Console.ReadLine();
                switch (inAnswer)
                {
                    case "m":
                        if (Player.storedFood > 0)
                        { player.Eat(Player.storedFood); Player.storedFood--; }
                        else { Console.WriteLine("Det finns ingen mat i din inventory"); }
                        break;
                    case "v":
                        if (Player.storedTree > 0)
                        { player.MakeFire(Player.storedTree); Player.storedTree--; }
                        else { Console.WriteLine("Det finns ingen trä i din inventory"); }
                        break;
                }
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
    + "För att avsluta, skriv 's', för att öppna inventory, skriv 'in'");
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



