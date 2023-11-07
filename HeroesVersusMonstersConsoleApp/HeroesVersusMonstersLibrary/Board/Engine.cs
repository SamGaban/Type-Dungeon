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

        private int _lastKnowX;

        public int LastKnowX
        {
            get { return _lastKnowX; }
            private set { _lastKnowX = value; }
        }

        private int _lastKnownY;

        public int LastKnownY
        {
            get { return _lastKnownY; }
            private set { _lastKnownY = value; }
        }




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

        // Generate encounters

        public void EncounterInit()
        {
            this._encounterGenerator.GenerateEncounterRoomOne(28, 10, 13, 4);
        }

        // Initializing the game loop

        public void Initialize()
        {
            TurnGameOn();
            EncounterInit(); // HEREEEEEEEEEE
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
                        case ConsoleKey.Escape:
                            this._gameRunning = false;
                            break;
                    }
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
            List<int> keysToRemove = new List<int>();
            if (combatBoard.Encounter())
            {
                foreach (KeyValuePair<int, Encounter> entry in this._encounterGenerator.EncounterList)
                {
                    if (entry.Value.PosX == this.HeroPosX && entry.Value.PosY == this.HeroPosY)
                    {
                        keysToRemove.Add(entry.Key);
                        this._activeMap.Map[entry.Value.PosY][entry.Value.PosX].ChangeType(0);
                    }
                }
                foreach (int keyToRemove in  keysToRemove)
                {
                    this._encounterGenerator.EncounterList.Remove(keyToRemove);
                }
            }
        }


        // Teleport back to last position

        public void TeleportBack()
        {
            Console.SetCursorPosition(this._activeMap.ActiveX, this._activeMap.ActiveY);
            Console.Write("E");
            this._heroPosX = this.LastKnowX;
            this._heroPosY = this.LastKnownY;
            this._activeMap.SetActivePosition(this.HeroPosX, this.HeroPosY);
            Console.SetCursorPosition(this._heroPosX, this._heroPosY);
            Console.Write(">");
        }

        //Drawing character

        public void DrawChar()
        {
            Console.SetCursorPosition(this._activeMap.ActiveX, this._activeMap.ActiveY);
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

            }
        }


        //presenting an encounter

        public void PresentEncounter()
        {
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
                    Console.WriteLine("Do you want to fight ?");
                    String choice1 = "Yes";
                    String choice2 = "No";
                    List<String> choices = new List<String>();
                    choices.Add(choice1);
                    choices.Add(choice2);
                    int userChoice = Dice.ChoiceGenerator(60, 20, choices);
                    if (userChoice == 0)
                    {
                        this.LaunchEncounter();
                        this.Hero.StaminaToMax();
                        this.Refresh();
                    }
                    else
                    {
                        this.TeleportBack();
                        this.Refresh();
                    }
                }
            }
        }


        //Refreshing the screen
        public void Refresh()
        {
            EncounterInit(); // DEBUGHEEEEEEEEEEEEEEEERE
            Console.Clear();
            foreach (Terrain terrain in mapList)
            {
                if (terrain.Active)
                {
                    this._heroPosX = terrain.ActiveX;
                    this._heroPosY = terrain.ActiveY;
                    terrain.Display();
                    this.DrawChar();
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
                        this._lastKnowX = this._activeMap.ActiveX;
                        this._lastKnownY = this._activeMap.ActiveY;
                        Console.SetCursorPosition(this._activeMap.ActiveX, this._activeMap.ActiveY);
                        Console.Write(" ");
                        this._activeMap.SetActivePosition(this._activeMap.ActiveX, this._activeMap.ActiveY - 1);
                        HeroPosSet(this._activeMap.ActiveX, this._activeMap.ActiveY - 1);
                        ChangeDirection("up");

                    }
                    else if (this._activeMap.Map[this._activeMap.ActiveY - 1][this._activeMap.ActiveX].Type < -1000)
                    {
                        this._lastKnowX = this._activeMap.ActiveX;
                        this._lastKnownY = this._activeMap.ActiveY;
                        this._activeMap.SetActivePosition(this._activeMap.ActiveX, this._activeMap.ActiveY - 1);
                        HeroPosSet(this._activeMap.ActiveX, this._activeMap.ActiveY - 1);
                        this.Refresh();
                        this.PresentEncounter();
                    }
                    break;
                case "left":
                    if (this._activeMap.Map[this._activeMap.ActiveY][this._activeMap.ActiveX - 1].Type == 0)
                    {
                        Console.SetCursorPosition(this._activeMap.ActiveX, this._activeMap.ActiveY);
                        Console.Write(" ");
                        this._activeMap.SetActivePosition(this._activeMap.ActiveX - 1, this._activeMap.ActiveY);
                        HeroPosSet(this._activeMap.ActiveX - 1, this._activeMap.ActiveY);
                        ChangeDirection("left");
                    }
                    else if (this._activeMap.Map[this._activeMap.ActiveY][this._activeMap.ActiveX - 1].Type < -1000)
                    {
                        this._lastKnowX = this._activeMap.ActiveX;
                        this._lastKnownY = this._activeMap.ActiveY;
                        this._activeMap.SetActivePosition(this._activeMap.ActiveX - 1, this._activeMap.ActiveY);
                        HeroPosSet(this._activeMap.ActiveX - 1, this._activeMap.ActiveY);
                        this.Refresh();
                        this.PresentEncounter();
                    }
                    break;
                 case "right":
                    if (this._activeMap.Map[this._activeMap.ActiveY][this._activeMap.ActiveX + 1].Type == 0)
                    {
                        Console.SetCursorPosition(this._activeMap.ActiveX, this._activeMap.ActiveY);
                        Console.Write(" ");
                        this._activeMap.SetActivePosition(this._activeMap.ActiveX + 1, this._activeMap.ActiveY);
                        HeroPosSet(this._activeMap.ActiveX + 1, this._activeMap.ActiveY);
                        ChangeDirection("right");
                    }
                    else if (this._activeMap.Map[this._activeMap.ActiveY][this._activeMap.ActiveX + 1].Type < -1000)
                    {
                        this._lastKnowX = this._activeMap.ActiveX;
                        this._lastKnownY = this._activeMap.ActiveY;
                        this._activeMap.SetActivePosition(this._activeMap.ActiveX + 1, this._activeMap.ActiveY);
                        HeroPosSet(this._activeMap.ActiveX + 1, this._activeMap.ActiveY);
                        this.Refresh();
                        this.PresentEncounter();
                    }
                    break;
                case "down":
                    if (this._activeMap.Map[this._activeMap.ActiveY + 1][this._activeMap.ActiveX].Type == 0)
                    {
                        Console.SetCursorPosition(this._activeMap.ActiveX, this._activeMap.ActiveY);
                        Console.Write(" ");
                        this._activeMap.SetActivePosition(this._activeMap.ActiveX, this._activeMap.ActiveY + 1);
                        HeroPosSet(this._activeMap.ActiveX, this._activeMap.ActiveY + 1);
                        ChangeDirection("down");
                    }
                    else if (this._activeMap.Map[this._activeMap.ActiveY + 1][this._activeMap.ActiveX].Type < -1000)
                    {
                        this._lastKnowX = this._activeMap.ActiveX;
                        this._lastKnownY = this._activeMap.ActiveY;
                        this._activeMap.SetActivePosition(this._activeMap.ActiveX, this._activeMap.ActiveY + 1);
                        HeroPosSet(this._activeMap.ActiveX, this._activeMap.ActiveY + 1);
                        this.Refresh();
                        this.PresentEncounter();
                    }
                    break;
            }
            this.DrawChar();
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
