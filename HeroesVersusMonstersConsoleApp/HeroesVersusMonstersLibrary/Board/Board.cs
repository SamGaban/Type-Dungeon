using HeroesVersusMonstersLibrary.Abilities;
using HeroesVersusMonstersLibrary.Generators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroesVersusMonstersLibrary.Board
{
    public class Board
    {
        #region Props
        protected int _horizontalSize;

        public int HorizontalSize
        {
            get { return _horizontalSize; }
            private set { _horizontalSize = value; }
        }

        protected int _verticalSize;

        public int VerticalSize
        {
            get { return _verticalSize; }
            private set { _verticalSize = value; }

        }

        protected List<Entity> _entityList = new List<Entity>();

        public List<Entity> EntityList
        {
            get { return _entityList; }
            private set { _entityList = value; }
        }
        #endregion

        #region CTOR
        public Board(int horizontalsize, int verticalsize, List<Entity> entitylist)
        {
            this._horizontalSize = horizontalsize;
            this._verticalSize = verticalsize;
            this._entityList = entitylist;
        }
        #endregion


        //Method that handles the whole ennemy turn

        public void MonsterTurn()
        {
            for (int i = 1;  i < _entityList.Count; i++)
            {
                Console.WriteLine($"Attack from {_entityList[i].Name} incoming !");
                Console.WriteLine();
                this.Refresh();
                Console.WriteLine($"Attack from {_entityList[i].Name} incoming !");
                Console.WriteLine();
                Console.WriteLine("Type the incoming sentence quickly to deflect it !");
                Console.WriteLine("───────────────────────── 2");
                Thread.Sleep(1000);
                this.Refresh();
                Console.WriteLine($"Attack from {_entityList[i].Name} incoming !");
                Console.WriteLine();
                Console.WriteLine("Type the incoming sentence quickly to deflect it !");
                Console.WriteLine("───────────────────────── 1");
                Thread.Sleep(1000);
                this.Refresh();
                Console.WriteLine($"Attack from {_entityList[i].Name} incoming !");
                Console.WriteLine();
                Console.WriteLine("Type the incoming sentence quickly to deflect it !");
                Console.WriteLine("─────────────────────────");
                Console.WriteLine();
                Console.WriteLine();
                MonsterAttack(_entityList[i], _entityList[0]);
            }
        }

        //Method that handles ennemies attack QTE's

        public void MonsterAttack(Entity monster, Entity hero)
        {
            QuickTimeEvent qte = new QuickTimeEvent(SentenceGenerator.Generate(3, monster), 8, monster, monster.Abilities[0], hero);
            qte.RunChallengeAsync().GetAwaiter().GetResult();
            this.Refresh();
        }

        //Method that handles the encounter of participants in the board

        public void Encounter()
        {
            bool fighting = true;
            // this.EntityList[0].Alive && this.EntityList.Count > 1
            while (fighting) 
            {
                this.Refresh();
                PlayerTurn();
                RefreshStamina();
                MonsterTurn();
            }
        }

        //Method to put stamina back to max at end of turn for players

        public void RefreshStamina()
        {
            foreach (Entity entity in _entityList)
            {
                if (entity.PlayerControlled)
                {
                    entity.StaminaToMax();
                }
            }
        }

        //Player turn method

        public void PlayerTurn()
        {
            Console.WriteLine("Player Turn");
            Console.WriteLine();
            Console.WriteLine("What will you do ?");
            Console.WriteLine();
            int counter = 1;
            foreach (Ability ability in this.EntityList[0].Abilities)
            {
                if (ability.Active)
                {
                    Console.Write($"{counter} : {ability.Name} (cost {ability.StaminaCost} stamina)");
                    Console.WriteLine($"{(ability.StaminaCost > this.EntityList[0].Stamina ? " | Not Enough Stamina - Will skip turn !" : "")}");
                    Console.WriteLine();
                }
                counter++;
            }
            Console.WriteLine($"{counter} : Skip Turn");
            string? userChoice = Console.ReadLine();
            this.Refresh();
            int userInput = -1;
            if (int.TryParse(userChoice, out userInput))
            {
                if (userInput != counter && this.EntityList[0].Abilities[userInput - 1].StaminaCost <= this.EntityList[0].Stamina)
                {
                    Console.WriteLine($"On who do you want to use {this.EntityList[0].Abilities[userInput - 1].Name} ?");
                    for (int i = 1; i < this.EntityList.Count; i++)
                    {
                        Console.WriteLine($"{(this.EntityList[i].Alive ? $"{i} : {this.EntityList[i].Name}" : $"{i} : {this.EntityList[i].Name} - Dead")}");
                    }
                    string? userString = Console.ReadLine();
                    int userInt = -1;
                    int.TryParse(userString, out userInt);
                    this.Refresh();
                    this.EntityList[0].UseAbility(this.EntityList[0].Abilities[userInput - 1], this.EntityList[userInt]);
                    Thread.Sleep(1500);
                }
                else
                {
                    Console.WriteLine("Turn Skipped");
                    Thread.Sleep(1500);
                }
            }
            else
            {
                Console.WriteLine("Wrong input, PUNISH !");
            }
            this.Refresh();
        }

        //Function that displays ASCII art of monsters participants of this board
        public void DisplayGraphics()
        {
            int posX = 3;
            int posY = 3;
            foreach (Entity entity in _entityList)
            {
                if (!entity.PlayerControlled)
                {
                    if (entity.Alive)
                    {
                        foreach (string line in entity.ASCII.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None))
                        {
                            Console.SetCursorPosition(posX, posY);
                            Console.WriteLine(line);
                            posY++;

                        }
                        posX += 30;
                        posY = 3;
                    }
                    else
                    {
                        foreach (string line in AsciiArt.dead.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None))
                        {
                            Console.SetCursorPosition(posX, posY);
                            Console.WriteLine(line);
                            posY++;

                        }
                        posX += 30;
                        posY = 3;
                    }

                }
            }
        }


        //Function that displays text information about participants of this board
        public void Display()
        {
            int posX = 0;
            int posY = this._verticalSize + 4;

            foreach (Entity entity in this.EntityList)
            {
                Console.SetCursorPosition(posX, posY);
                Console.Write($"┌────────────────────────");
                posY++;
                Console.SetCursorPosition(posX, posY);
                Console.Write($"│{entity.Name}");
                posY++;
                Console.SetCursorPosition(posX, posY);
                Console.Write($"{(entity.Alive ? $"│HP : {entity.HealthPoints} / {entity.MaxHealthPoints}" : "│ DEAD")}");
                posY++;
                Console.SetCursorPosition(posX, posY);
                Console.Write($"{(entity.MaxStamina != 0 ? $"│Stamina : {entity.Stamina} / {entity.MaxStamina}" : "│")}");
                posY++;
                posX += 25;
                posY = this._verticalSize + 4;

            }
            posX = 0;
            posY = this._verticalSize + 8;
            Console.SetCursorPosition(posX, posY);
            Console.WriteLine("└───────────────────────────────────────────────────────────────────────────────" +
                "────────────────────────────────────────────────────────────────────────────────");
        }


        //Function to create a game board and display entities name participating in the fight under it
        public void Refresh()
        {
            Console.Clear();
            Console.Write("╔");
            for (int i  = 0; i < this.HorizontalSize; i++)
            {
                Console.Write("═");
            }
            Console.Write("╗");
            Console.WriteLine();
            for (int i = 0; i < this.VerticalSize; i++)
            {
                Console.Write("║");
                for (int j = 0; j < this.HorizontalSize; j++)
                {
                    Console.Write(" ");
                }
                Console.Write("║");
                Console.WriteLine();
            }
            Console.Write("╚");
            for (int i = 0; i < this.HorizontalSize; i++)
            {
                Console.Write("═");
            }
            Console.Write("╝");

            this.Display();
            this.DisplayGraphics();
            Console.SetCursorPosition(0, VerticalSize + 10);
        }
    }
}
