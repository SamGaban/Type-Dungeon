using HeroesVersusMonstersLibrary;
using HeroesVersusMonstersLibrary.Board;
using HeroesVersusMonstersLibrary.Loots;
using HeroesVersusMonstersLibrary.Abilities;
using HeroesVersusMonstersLibrary.Generators;


Console.CursorVisible = false;
Hero hero1 = new Hero();
Console.Clear();

Monster monster1 = new Monster(0);
Monster monster2 = new Monster(0);
Monster monster3 = new Monster(1);
Monster monster4 = new Monster(0);


List<Entity> testEntities = new List<Entity>();

testEntities.Add(hero1);
testEntities.Add(monster1);
testEntities.Add(monster2);
testEntities.Add(monster3);
testEntities.Add(monster4);
Board testBoard = new Board(160, 16, testEntities);
testBoard.Encounter();