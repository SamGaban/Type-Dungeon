using HeroesVersusMonstersLibrary;
using HeroesVersusMonstersLibrary.Board;
using HeroesVersusMonstersLibrary.Loots;
using HeroesVersusMonstersLibrary.Abilities;
using HeroesVersusMonstersLibrary.Generators;


Console.WriteLine("Press Any Key To Continue");
Console.ReadKey(true);
Console.Clear();
Console.CursorVisible = false;
Hero hero1 = new Hero();
Console.Clear();


Terrain map1 = new Terrain();
map1.SetActivePosition(2, 13);
map1.GenerateWall(4, 1, 18, 4);
map1.GenerateWall(4, 6, 4, 9);
map1.GenerateWall(9, 6, 35, 3);
map1.GenerateWall(23, 4, 7, 1);
map1.GenerateWall(25, 9, 2, 3);
map1.GenerateWall(25, 13, 2, 2);
map1.GenerateWall(30, 1, 14, 4);
map1.GenerateWall(42, 9, 2, 6);
Engine gameEngine = new Engine(map1, hero1);
gameEngine.Initialize();