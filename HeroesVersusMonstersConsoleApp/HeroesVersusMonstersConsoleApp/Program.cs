using HeroesVersusMonstersLibrary;
using HeroesVersusMonstersLibrary.Loots;

Hero hero1 = new Hero();

Console.Clear();

hero1.DisplayStatus();

Console.WriteLine();

Monster monster1 = new Monster(0);
monster1.DisplayStatus();
Console.WriteLine();
foreach (KeyValuePair<GenericLoot, int> entry in monster1.LootTable)
{
    Console.WriteLine($"{entry.Key.Type} : {entry.Value}");
}

Console.WriteLine();

Monster monster2 = new Monster(2);
monster2.DisplayStatus();
Console.WriteLine();
foreach (KeyValuePair<GenericLoot, int> entry in monster2.LootTable)
{
    Console.WriteLine($"{entry.Key.Type} : {entry.Value}");
}