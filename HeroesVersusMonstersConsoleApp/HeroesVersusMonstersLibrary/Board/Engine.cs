using HeroesVersusMonstersLibrary.Loots;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroesVersusMonstersLibrary.Board
{
    public class Engine
    {


        #region Props
        private EncounterGenerator _encounterGenerator;

        public EncounterGenerator EncounterGenerator
        {
            get { return _encounterGenerator; }
            private set { _encounterGenerator = value; }
        }


        private string _direction = "right";

        public string Direction
        {
            get { return _direction; }
            private set { _direction = value; }
        }



        private Terrain _activeMap;

        public Terrain ActiveMap
        {
            get { return _activeMap; }
            private set { _activeMap = value; }
        }


        private int _heroPosX;

        public int HeroPosX
        {
            get { return _heroPosX; }
            private set { _heroPosX = value; }
        }

        private int _heroPosY;

        public int HeroPosY
        {
            get { return _heroPosY; }
            private set { _heroPosY = value; }
        }



        private bool _gameRunning = false;

        public bool GameRunning
        {
            get { return _gameRunning; }
            private set { _gameRunning = value; }
        }



        protected Entity _hero;

        public Entity Hero
        {
            get { return _hero; }
            private set { _hero = value; }
        }


        List<Terrain> mapList = new List<Terrain>();
        #endregion

        #region CTOR
        public Engine(Terrain FirstMap, Entity hero)
        {
            this._hero = hero;
            _encounterGenerator = new EncounterGenerator(this._hero, this);
            this._activeMap = FirstMap;
            mapList.Add(FirstMap);
            FirstMap.Activate();
        }
        #endregion

        //Adding a map to the engine

        public void AddMap(Terrain mapToAdd)
        {
            mapList.Add(mapToAdd);
        }

        // Initializing the game loop

        public void Initialize()
        {
            TurnGameOn();
            Refresh();
            ConsoleKeyInfo keyInfo;
            while (this._gameRunning)
            {
                if (Console.KeyAvailable)
                {
                    keyInfo = Console.ReadKey(true);
                    switch (keyInfo.Key)
                    {
                        case ConsoleKey.Z:
                            Move("up");
                            break;
                        case ConsoleKey.Q:
                            Move("left");
                            break;
                        case ConsoleKey.S:
                            Move("down");
                            break;
                        case ConsoleKey.D:
                            Move("right");
                            break;
                        case ConsoleKey.Y:
                            this.LaunchEncounter();
                            break;
                        case ConsoleKey.Escape:
                            this._gameRunning = false;
                            break;
                        case ConsoleKey.G:
                            this._encounterGenerator.GenerateEncounter();
                            break;
                    }
                Refresh();
                }
            }
        }

        //Launch encounter on current position

        public void LaunchEncounter()
        {
            List<Entity> entityList = new List<Entity>();
            entityList.Add(this.Hero);
            foreach(KeyValuePair<int, Encounter> entry in this._encounterGenerator.EncounterList)
            {
                if (entry.Value.PosX == this.HeroPosX && entry.Value.PosY == this.HeroPosY)
                {
                    foreach(Entity entity in entry.Value.Entities)
                    {
                        entityList.Add(entity);
                    }
                }
            }

            Board combatBoard = new Board(entityList);
            foreach (Entity entity in entityList)
            {
                entity.OnHit += combatBoard.OnHitHandler;
            }
            if (combatBoard.Encounter())
            {
                foreach (KeyValuePair<int, Encounter> entry in this._encounterGenerator.EncounterList)
                {
                    if (entry.Value.PosX == this.HeroPosX && entry.Value.PosY == this.HeroPosY)
                    {
                        this._encounterGenerator.EncounterList.Remove(entry.Key);
                        this._activeMap.Map[entry.Value.PosY][entry.Value.PosX].ChangeType(0);
                    }
                }
            }
        }


        //Refreshing the screen
        public void Refresh()
        {
            Console.Clear();
            foreach (Terrain terrain in mapList)
            {
                if (terrain.Active)
                {
                    this._heroPosX = terrain.ActiveX;
                    this._heroPosY = terrain.ActiveY;
                    terrain.Display();
                    Console.SetCursorPosition(this._heroPosX, this._heroPosY);
                    switch (this._direction)
                    {
                        case "up":
                            Console.Write("^");
                            break;
                        case "left":
                            Console.Write("<");
                            break;
                        case "right":
                            Console.Write(">");
                            break;
                        case "down":
                            Console.Write("v");
                            break;
                        case "encounter":
                            foreach (KeyValuePair<int, Encounter> entry in this._encounterGenerator.EncounterList)
                            {
                                if (entry.Value.PosX == this.HeroPosX && entry.Value.PosY == this.HeroPosY)
                                {
                                    Console.SetCursorPosition(60, 18);
                                    Console.Write(new string(' ', Console.WindowWidth));
                                    Console.SetCursorPosition(60, 18);
                                    foreach (Entity entity in entry.Value.Entities)
                                    {
                                        Console.Write($"{entity.Name} ");
                                    }
                                    Console.SetCursorPosition(60, 19);
                                    Console.WriteLine("Do you want to fight ? (Y for yes, move for no)");
                                }
                            }
                            break;
                    }
                    Console.SetCursorPosition(0, 18);
                    Console.WriteLine(this._hero.Name);
                    Console.WriteLine("Inventory");
                    foreach(KeyValuePair<GenericLoot, int> item in this.Hero.Inventory)
                    {
                        Console.WriteLine(item.Value > 0 ? $"{item.Key.Type} : x{item.Value}" : "");
                    }
                }
            }
        }

        // Change orientation / direction

        public void ChangeDirection(string direction)
        {
            this._direction = direction;
        }

        public void Move(string direction)
        {
            switch (direction)
            {
                case "up":
                    if (this._activeMap.Map[this._activeMap.ActiveY - 1][this._activeMap.ActiveX].Type == 0)
                    {
                        this._activeMap.SetActivePosition(this._activeMap.ActiveX, this._activeMap.ActiveY - 1);
                        HeroPosSet(this._activeMap.ActiveX, this._activeMap.ActiveY - 1);
                        ChangeDirection("up");
                    }
                    else if (this._activeMap.Map[this._activeMap.ActiveY - 1][this._activeMap.ActiveX].Type < -1000)
                    {
                        ChangeDirection("encounter");
                        this._activeMap.SetActivePosition(this._activeMap.ActiveX, this._activeMap.ActiveY - 1);
                        HeroPosSet(this._activeMap.ActiveX, this._activeMap.ActiveY - 1);
                    }
                    break;
                case "left":
                    if (this._activeMap.Map[this._activeMap.ActiveY][this._activeMap.ActiveX - 1].Type == 0)
                    {
                        this._activeMap.SetActivePosition(this._activeMap.ActiveX - 1, this._activeMap.ActiveY);
                        HeroPosSet(this._activeMap.ActiveX - 1, this._activeMap.ActiveY);
                        ChangeDirection("left");
                    }
                    else if (this._activeMap.Map[this._activeMap.ActiveY][this._activeMap.ActiveX - 1].Type < -1000)
                    {
                        ChangeDirection("encounter");
                        this._activeMap.SetActivePosition(this._activeMap.ActiveX - 1, this._activeMap.ActiveY);
                        HeroPosSet(this._activeMap.ActiveX - 1, this._activeMap.ActiveY);
                    }
                    break;
                 case "right":
                    if (this._activeMap.Map[this._activeMap.ActiveY][this._activeMap.ActiveX + 1].Type == 0)
                    {
                        this._activeMap.SetActivePosition(this._activeMap.ActiveX + 1, this._activeMap.ActiveY);
                        HeroPosSet(this._activeMap.ActiveX + 1, this._activeMap.ActiveY);
                        ChangeDirection("right");
                    }
                    else if (this._activeMap.Map[this._activeMap.ActiveY][this._activeMap.ActiveX + 1].Type < -1000)
                    {
                        ChangeDirection("encounter");
                        this._activeMap.SetActivePosition(this._activeMap.ActiveX + 1, this._activeMap.ActiveY);
                        HeroPosSet(this._activeMap.ActiveX + 1, this._activeMap.ActiveY);
                    }
                    break;
                case "down":
                    if (this._activeMap.Map[this._activeMap.ActiveY + 1][this._activeMap.ActiveX].Type == 0)
                    {
                        this._activeMap.SetActivePosition(this._activeMap.ActiveX, this._activeMap.ActiveY + 1);
                        HeroPosSet(this._activeMap.ActiveX, this._activeMap.ActiveY + 1);
                        ChangeDirection("down");
                    }
                    else if (this._activeMap.Map[this._activeMap.ActiveY + 1][this._activeMap.ActiveX].Type < -1000)
                    {
                        ChangeDirection("encounter");
                        this._activeMap.SetActivePosition(this._activeMap.ActiveX, this._activeMap.ActiveY + 1);
                        HeroPosSet(this._activeMap.ActiveX, this._activeMap.ActiveY + 1);
                    }
                    break;
            }
        }

        // Change Active Map

        public void ChangeActiveMap(Terrain old, Terrain newMap)
        {
            old.DeActivate();
            newMap.Activate();
            this._activeMap = newMap;
        }

        // Set hero position

        public void HeroPosSet(int  x, int y)
        {
            this._heroPosY = y;
            this._heroPosX = x;
        }

        // Turning game on

        public void TurnGameOn()
        {
            this._gameRunning = true;
        }

        public void TurnGameOff()
        {
            this._gameRunning = false;
        }
    }
}
