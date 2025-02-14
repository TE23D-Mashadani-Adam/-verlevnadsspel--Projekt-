using System.Data;
using System.Runtime.Serialization.Json;

Player player = new();
World world = new();


int daysCount = 1;
int points = 0;
List<string> dayBasedInfo = [];

Console.WriteLine("Hej och välkommen, du behöver överleva genom att äta" +
" och göra eld för att bli varm, var försiktig så att du inte förlorar mycket hp" +
" och dör!,din hp minskar när hunger och värme går under noll, tryck vidare för att starta spelet");
Console.ReadLine();
Console.Clear();

Console.WriteLine("Vad är ditt mål? Ange antal dagar du vill överleva som mål från 0 till 20");


//Låter användaren välja ett mål, tvingas välja rätt siffra
string goalText = "";
int goal = 0;
bool rightParse = false;
int maxGoalDays = 20;
while (!rightParse)
{
    rightParse = TryCorrectInput(goalText, ref goal, rightParse, maxGoalDays);
}

//Loopen som kör själva spelet så länge spelaren lever
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

        SurvivalAlghorythm(answer);

        if (answer == "s")
        {
            break;
        }
    }

    //Kvarlämnad mat går till inventory, status för dagen visas och lagras i en lista
    StoreItems();

    Console.WriteLine("\n" + $"Dag: {daysCount} Hunger: {player.hunger} Värme: {player.heat} HP: {player.hp}");

    //Hp minskar när damage och heat sjunker ner
    world.TryGiveDamage(player.hunger, player.heat, ref player.hp);

    Console.WriteLine("Tryck vidare för att avsluta dagen");
    Console.ReadLine();

    //Kollar ifall spelaren har dött och kör "gameover" logiken då
    if (player.hp <= 0)
    {
        ShowDeadScene();
        goal = CheckIfGoalReached(goal);
        Console.ReadLine();
        break;
    }

    daysCount++;

}

Console.ReadLine();

//Visar upp nuvarande mat och ved i världen
void showItemsMessage(float foodCount, float treeCount)
{
    string availbleItems = $"Mat: {foodCount} Ved: {treeCount}";
    Console.WriteLine(availbleItems + "\n" + "För mat, skriv m, för ved, skriv v"
    + "För att avsluta, skriv 's', för att öppna inventory, skriv 'in'");
}

//Visar upp statistik i slutet av spelet, skickar tillbaka poäng
int ShowStats(List<string> list, int points, string statAnswer)
{
    for (int i = 0; i < list.Count; i++)
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
    points = list.Count;

    return points;
}

//Gör att spelaren äter ifall det finns mat
void TryEatFood(ref int foodCount, ref int hungerDamage)
{
    if (foodCount > 0)
    {
        foodCount = player.Eat(foodCount);
        hungerDamage = 0;
    }
    else { Console.WriteLine("Det finns ingen mat!"); }
}

//Gör eld ifall det finns ved
void TryMakeFire(ref int treeCount, ref int heatDamage)
{
    if (treeCount > 0)
    {
        treeCount = player.MakeFire(treeCount);
        heatDamage = 0;
    }
    else { Console.WriteLine("Det finns ingen trä!"); }
}

//Säger till att användare skriver in rätt siffra
bool TryCorrectInput(string goalText, ref int goal, bool rightParse, int maxGoalDays)

{
    goalText = Console.ReadLine();
    rightParse = int.TryParse(goalText, out goal);
    if (!rightParse)
    {
        Console.WriteLine("Skriv ett giltigt tal, bokstäver och toma rader accepteras ej!");
        return false;
    }
    else if (rightParse && goal > maxGoalDays)
    {
        Console.WriteLine($"Du kan inte ha mer än {maxGoalDays} dagar som mål!");
        return false;
    }
    else if (rightParse && goal <= 0)
    {
        Console.WriteLine("Skriv ett värde större än noll tack");
        return false;
    }
    else
    {
        return true;
    }
}

//Kör upp algoritmen för att överleva beroende på vad spelaren vill göra
void SurvivalAlghorythm(string answer)
{
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
}

int CheckIfGoalReached(int goal)
{
    if (daysCount >= goal)
    {
        Console.WriteLine($"Grattis, du hade uppnått ditt mål som var att överleva {goal} dagar!");
    }
    else
    {
        Console.WriteLine($"Tyvärr, du hade inte uppnått ditt mål som var att överleva {goal} dagar!");
    }
    return goal;
}

void StoreItems()
{
    Player.storedFood += world.foodCount;
    Player.storedTree += world.treeCount;

    dayBasedInfo.Add($"Hunger: {player.hunger} Värme: {player.heat} HP: {player.hp}");

}

void ShowDeadScene()
{
    Console.WriteLine("Du är död, om du vill visa fullständig statistik, skriv 's'");
    string statAnswer = Console.ReadLine();
    points = ShowStats(dayBasedInfo, points, statAnswer);
    Console.WriteLine($"Du hade samlat {points} poäng!");
}