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

int daysCount = 1;

int storedFood = 0;
int storedTree = 0;

while (player.hp > 0)
{
    //Mängd mat och ved
    int foodCount = random.Next(0, 2);
    int treeCount = random.Next(0, 2);
    int damage = random.Next(40, 70);
    //Mängd förlorad värme och hunger
    hungerDamage = random.Next(15, 40);
    heatDamage = random.Next(15, 40);

    string availbleItems = "";
    string answer = "";

    foodCount += storedFood;
    treeCount += storedTree;

    //Algoritm för att äta och göra eld med ved
    while (answer.ToLower() != "slut")
    {

        Console.WriteLine(storedFood + " " + storedTree);
        showItemsMessage(availbleItems, foodCount, treeCount);
        answer = Console.ReadLine();

        if (answer.ToLower() == "m" && foodCount > 0)
        {
            foodCount = player.eat(foodCount);
            hungerDamage = 0;
        }
        else if (answer.ToLower() == "m" && foodCount == 0)
        { Console.WriteLine("Det finns ingen mat"); }

        else if (answer.ToLower() == "v" && treeCount > 0)
        {
            treeCount = player.makeFire(treeCount);
            heatDamage = 0;
        }
        else if (answer.ToLower() == "v" && treeCount == 0)
        { Console.WriteLine("Det finns inga träd"); }

        Console.WriteLine($"Hunger: {player.hunger} Värme: {player.heat} HP: {player.hp}");

    }

    storedFood = foodCount;
    storedTree = treeCount;

    player.hunger -= hungerDamage;
    player.heat -= heatDamage;

    //Hp minskar när damage och heat sjunker ner
    if (player.hunger <= 10 || player.heat <= 30)
    {
        player.hp -= damage;
    }

    Console.WriteLine($"Hunger: {player.hunger} Värme: {player.heat} HP: {player.hp} Dag: {daysCount}");
    Console.WriteLine("Tryck vidare för att avsluta dagen");
    Console.ReadLine();

    if (player.hp <= 0)
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



