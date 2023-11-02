using HeroesVersusMonstersLibrary;
using HeroesVersusMonstersLibrary.Board;
using HeroesVersusMonstersLibrary.Loots;
using HeroesVersusMonstersLibrary.Abilities;
using HeroesVersusMonstersLibrary.Generators;


#region Base Setup And hero creation
Console.CursorVisible = false;
Hero hero1 = new Hero();
Console.Clear();
#endregion

#region Test monsters creations
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

#endregion



Board testBoard = new Board(testEntities);

foreach (Entity entity in testEntities)
{
    entity.OnHit += testBoard.OnHitHandler;
}

testBoard.Encounter();