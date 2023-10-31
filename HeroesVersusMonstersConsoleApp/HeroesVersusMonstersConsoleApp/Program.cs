using HeroesVersusMonstersLibrary;
using HeroesVersusMonstersLibrary.Board;
using HeroesVersusMonstersLibrary.Loots;

Hero hero1 = new Hero();
Console.Clear();

Monster monster1 = new Monster(0);


List<Entity> testEntities = new List<Entity>();

testEntities.Add(hero1);
testEntities.Add(monster1);

Board testBoard = new Board(100, 16, testEntities);
testBoard.Refresh();
