﻿using System.Data;
using System.Runtime.Serialization.Json;

Player player = new();
World world = new();

int goal = 0;

int daysCount = 1;
//Använder lista istället för Array, för den är mer dynamisk och är enklare att ändra på, vilket görs mycket i koden
List<string> dayBasedInfo = [];

Console.WriteLine("Hej och välkommen, du behöver överleva genom att äta" +
" och göra eld för att bli varm, var försiktig så att du inte förlorar mycket hp" +
" och dör!,din hp minskar när hunger och värme går under noll, tryck vidare för att starta spelet");
Console.ReadLine();
Console.Clear();


goal = ChooseGoal(goal);

GameAlghorythm(player, world, daysCount, dayBasedInfo, goal);

static int ChooseGoal(int goal)
{
    Console.WriteLine("Vad är ditt mål? Ange antal dagar du vill överleva som mål från 1 till 20");

    //Låter användaren välja ett mål, tvingas välja rätt siffra
    string goalText = "";
    goal = 0;
    bool rightParse = false;
    int maxGoalDays = 20;
    while (!rightParse)
    {
        goalText = Console.ReadLine();
        rightParse = CheckCorrectInput(goalText, ref goal); //Kollar ifall spelaren skrivit en siffra
        TryCorrectGoalInput(ref rightParse, goal, maxGoalDays); //Säger till att siffran är inom intervallet
    }

    return goal;
}

static void EndDayScene(World world, Player player, List<string> dayBasedInfo, int daysCount)
{
    //Kvarlämnad mat går till inventory, status för dagen visas och lagras i en lista
    StoreItems(world, player, dayBasedInfo);

    Console.WriteLine("\n" + $"Dag: {daysCount} Hunger: {player.hunger} Värme: {player.heat} HP: {player.hp}");

    //Hp minskar när damage och heat sjunker ner
    world.TryGiveDamage(player.hunger, player.heat, ref player.hp);

    Console.WriteLine("Tryck vidare för att avsluta dagen");
    Console.ReadLine();
}

static void StartDeadScene(List<string> dayBasedInfo, int daysCount, int goal)
{
    ShowDeadScene(dayBasedInfo);
    CheckIfGoalReached(daysCount, goal);
    Console.WriteLine("Tryck vidare för att avsluta spelet");
    Console.ReadLine();
}

//Visar upp nuvarande mat och ved i världen
static void ShowItemsMessage(float foodCount, float treeCount)
{
    string availbleItems = $"Mat: {foodCount} Ved: {treeCount}";
    Console.WriteLine(availbleItems + "\n" + "För mat, skriv m, för ved, skriv v, "
    + "För att avsluta, skriv 's', för att öppna inventory, skriv 'in'");
}

//Visar upp statistik i slutet av spelet, skickar tillbaka poäng
static int ShowStats(List<string> dataList)
{
    for (int i = 0; i < dataList.Count; i++)
    {
        Console.WriteLine($"Dag: {i + 1} Status: {dataList[i]}");
    }
    return dataList.Count();
}

//Gör att spelaren äter ifall det finns mat
static void TryEatFood(World world, Player player)
{
    if (world.foodCount > 0)
    {
        world.foodCount = player.Eat(world.foodCount);
    }
    else { Console.WriteLine("Det finns ingen mat!"); }
}

//Gör eld ifall det finns ved
static void TryMakeFire(World world, Player player)
{
    if (world.treeCount > 0)
    {
        world.treeCount = player.MakeFire(world.treeCount);
    }
    else { Console.WriteLine("Det finns ingen trä!"); }
}

//Säger till att input är en siffra, annars blir metoden falsk och ger ett fel meddelande
static bool CheckCorrectInput(string text, ref int num)
{
    bool checkRightParse = int.TryParse(text, out num);
    if (!checkRightParse)
    {
        Console.WriteLine("Skriv ett giltigt tal, bokstäver och toma rader accepteras ej!");
        return false;
    }
    else
    {
        return true;
    }
}
//Säger till att användare skriver in rätt siffra
static void TryCorrectGoalInput(ref bool rightParse, int num, int maxGoalDays)
{
    if (rightParse && num > maxGoalDays)
    {
        Console.WriteLine($"Du kan inte ha mer än {maxGoalDays} dagar som mål!");
        rightParse = false;
    }
    else if (rightParse && num <= 0)
    {
        Console.WriteLine("Skriv ett värde större än noll tack");
        rightParse = false;
    }
}

//Kör upp algoritmen för att överleva beroende på vad spelaren vill göra
static void SurvivalAlghorithm(string answer, Player player, World world)
{
    switch (answer)
    {
        case "m":
            TryEatFood(world, player);
            break;
        case "v":
            TryMakeFire(world, player);
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
            if (answer != "s")
                Console.WriteLine("Ogiltigt val, försök igen!");
            break;
    }
}

//Kollar ifall spelaren hade uppnått sitt mål
static void CheckIfGoalReached(int daysCount, int goal)
{
    if (daysCount >= goal)
    {
        Console.WriteLine($"Grattis, du hade uppnått ditt mål som var att överleva {goal} dagar!");
    }
    else
    {
        Console.WriteLine($"Tyvärr, du hade inte uppnått ditt mål som var att överleva {goal} dagar!");
    }
}

//Lagrar kvarlämnade resurser
static void StoreItems(World world, Player player, List<string> dayBasedInfo)
{
    player.storedFood += world.foodCount;
    player.storedTree += world.treeCount;

    //Lägger till daglig information i en lista
    dayBasedInfo.Add($"Hunger: {player.hunger} Värme: {player.heat} HP: {player.hp}");
}

//Visar upp statistik efter att spelaren hade dött
static void ShowDeadScene(List<string> dayBasedInfo)

{
    Console.WriteLine("Du är död, om du vill visa statistik, skriv 's'");
    string statAnswer = Console.ReadLine();
    if (statAnswer == "s") { ShowStats(dayBasedInfo); }
    Console.WriteLine($"Du hade samlat {dayBasedInfo.Count} poäng!");
}

//Kör upp hela spel algortimen
static void GameAlghorythm(Player player, World world, int daysCount, List<string> dayBasedInfo, int goal)
{
    //Loopen som kör själva spelet så länge spelaren lever
    while (player.hp > 0)
    {
        world.SetOdz();

        world.Randomize(daysCount);
        string answer = "";

        player.TakeDamage(world);


        //Algoritm för att äta och göra eld med ved
        while (answer.ToLower() != "s")
        {
            Console.WriteLine($"Hunger: {player.hunger} Värme: {player.heat} HP: {player.hp}");
            ShowItemsMessage(world.foodCount, world.treeCount);
            answer = Console.ReadLine().ToLower();

            SurvivalAlghorithm(answer, player, world);

        }

        EndDayScene(world, player, dayBasedInfo, daysCount);
        Console.Clear();

        //Kollar ifall spelaren har dött och kör "gameover" logiken då
        if (player.hp <= 0)
        {
            StartDeadScene(dayBasedInfo, daysCount, goal);
            break;
        }

        daysCount++;

    }
}