using HeroesVersusMonstersLibrary;
using HeroesVersusMonstersLibrary.Board;
using HeroesVersusMonstersLibrary.Loots;
using HeroesVersusMonstersLibrary.Abilities;
using HeroesVersusMonstersLibrary.Generators;



Console.CursorVisible = false;
Hero hero1 = new Hero();
Console.Clear();


Terrain map1 = new Terrain();
map1.SetActivePosition(2, 13);
map1.GenerateWall(4, 12, 26, 3);
map1.GenerateWall(4, 8, 31, 2);
map1.GenerateWall(43, 5, 2, 10);
map1.GenerateWall(35, 5, 8, 5);
map1.GenerateWall(31, 3, 5, 2);
map1.GenerateWall(4, 3, 27, 4);
map1.GenerateWall(45, 9, 13, 4);
Engine gameEngine = new Engine(map1, hero1);
gameEngine.Initialize();