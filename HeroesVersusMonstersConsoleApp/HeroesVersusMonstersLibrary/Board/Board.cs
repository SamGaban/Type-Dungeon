using HeroesVersusMonstersLibrary.Abilities;
using HeroesVersusMonstersLibrary.Generators;
using HeroesVersusMonstersLibrary.Loots;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroesVersusMonstersLibrary.Board
{
    public class Board
    {
        #region Props

        private bool _fighting = true;

        public bool Fighting
        {
            get { return _fighting; }
            private set { _fighting = value; }
        }



        protected int _horizontalSize = 160;

        //List of loot for the completion of the board

        protected Dictionary<GenericLoot, int> _lootTable = new Dictionary<GenericLoot, int>();

        public Dictionary<GenericLoot, int> LootTable
        {
            get { return _lootTable; }
            private set { _lootTable = value; }
        }


        public int HorizontalSize
        {
            get { return _horizontalSize; }
            private set { _horizontalSize = value; }
        }

        protected int _verticalSize = 16;

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
        public Board(List<Entity> entitylist)
        {
            this._entityList = entitylist;

            this.FillLootTableWithEmpties();
        }

        #endregion

        // Adding to the loot table which is already initialized with all possible loots set to 0

        public void AddToLootTable(string nameOfTheLoot, int quantity)
        {
            foreach (KeyValuePair<GenericLoot, int> entry in _lootTable)
            {
                if (entry.Key.Type == nameOfTheLoot)
                {
                    _lootTable[entry.Key] += quantity;
                }
            }
        }

        //Filling the loot tables with empty values of each possible loot from the ennemies present

        public void FillLootTableWithEmpties()
        {
            List<string> lootToFill = new List<string> { "base" };
            foreach (Entity entity in _entityList)
            {
                foreach (KeyValuePair<GenericLoot, int> entry in entity.LootTable)
                {
                    if (!lootToFill.Contains(entry.Key.Type))
                    {
                        lootToFill.Add(entry.Key.Type);
                    }
                }
            }

            foreach (Entity entity in _entityList)
            {
                foreach (KeyValuePair<GenericLoot, int> entry in entity.LootTable)
                {
                    if (lootToFill.Contains(entry.Key.Type))
                    {
                        _lootTable[entry.Key] = 0;
                        lootToFill.Remove(entry.Key.Type);
                    }
                }
            }
        }

        //Check if an entity on the board is dead

        public void OnHitHandler(Entity entity, Ability ability, Entity entity1)
        {
            CheckIfDead();
        }

        public void CheckIfDead()
        {
            List<Entity> entitiesToRemove = new List<Entity>();
            foreach (Entity e in this._entityList)
            {
                if (e.HealthPoints <= 0)
                {
                    if (e.PlayerControlled)
                    {
                        this._fighting = false;
                    }
                    else
                    {
                        entitiesToRemove.Add(e);
                    }
                }
            }
            foreach (Entity a in entitiesToRemove)
            {
                foreach (KeyValuePair<GenericLoot, int> entry in a.LootTable)
                {
                    this.AddToLootTable(entry.Key.Type, entry.Value);
                }
                this._entityList.Remove(a);
            }
            if (this._entityList.Count() == 1)
            {
                this._fighting = false;
            }
        }

        //Method that handles the whole ennemy turn

        public void MonsterTurn()
        {
            int countBefore;
            for (int i = 1; i < _entityList.Count; i++)
            {
                countBefore = this._entityList.Count();
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
                while (Console.KeyAvailable)
                {
                    Console.ReadKey(true);
                }
                MonsterAttack(_entityList[i], _entityList[0]);
                if (countBefore != this._entityList.Count())
                {
                    i--;
                }
            }
        }

        //Method that handles ennemies attack QTE's

        public void MonsterAttack(Entity monster, Entity hero)
        {
            QTE.RunChallenge(SentenceGenerator.Generate(3, monster), 8, monster, monster.Abilities[0], hero);
            this.Refresh();
        }

        //Method that handles the encounter of participants in the board

        public bool Encounter()
        {
            // this.EntityList[0].Alive && this.EntityList.Count > 1
            while (this.Fighting && this._entityList.Count() > 1) 
            {
                this.Refresh();
                PlayerTurn();
                if (!this.Fighting) break;
                RefreshStamina();
                if (!this.Fighting) break;
                MonsterTurn();
                if (!this.Fighting) break;
            }

            Console.Clear();
            this._entityList[0].Rest();
            Console.WriteLine(AsciiArt.fightWon);
            Console.WriteLine();
            Console.WriteLine("Health Replenished");
            Console.WriteLine();
            Console.WriteLine("You've looted :");
            Console.WriteLine();
            foreach (KeyValuePair<GenericLoot, int> entry in this._lootTable)
            {
                this._entityList[0].AddToInventory(entry.Key, entry.Value);
                Console.WriteLine(entry.Value > 0 ? $"{entry.Value} X {entry.Key.Type}" : "");
            }
            Console.ReadKey();
            return true;

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
            List<String> options = new List<String>();
            foreach (Ability ability in this.EntityList[0].Abilities.Where(a => a.Active))
            {
                String toAdd2 = "";
                if (ability.Active)
                {
                    toAdd2 += $"{ability.Name} (cost {ability.StaminaCost} stamina)";
                    toAdd2 += $"{(ability.StaminaCost > this.EntityList[0].Stamina ? " | Not Enough Stamina - Will skip turn !" : "")}";
                }
                options.Add(toAdd2);
            }
            String toAdd = $"Skip Turn";
            options.Add(toAdd);
            this.Refresh();
            int userChoice = Dice.ChoiceGenerator(Console.CursorLeft, Console.CursorTop, options);
            this.Refresh();

            if (userChoice < options.Count() - 1 && this.EntityList[0].Abilities[userChoice].StaminaCost <= this.EntityList[0].Stamina)
            {
                Console.WriteLine($"On who do you want to use {this.EntityList[0].Abilities[userChoice].Name} ?");
                List<String> options2 = new List<String>();
                String monsterOption = "";
                Dictionary<String, int> presentMonsters = new Dictionary<String, int>();
                for (int i = 1; i < this.EntityList.Count; i++)
                {
                    monsterOption = "";
                    if (presentMonsters.ContainsKey(this.EntityList[i].Name))
                    {
                        presentMonsters[this.EntityList[i].Name] += 1;
                    }
                    else
                    {
                        presentMonsters[this.EntityList[i].Name] = 1;
                    }
                    monsterOption += $"{this.EntityList[i].Name} {presentMonsters[this.EntityList[i].Name]}";
                    options2.Add(monsterOption);
                }
                int userChoice2 = Dice.ChoiceGenerator(Console.CursorLeft, Console.CursorTop, options2);
                this.Refresh();
                this.EntityList[0].UseAbility(this.EntityList[0].Abilities[userChoice], this.EntityList[userChoice2 + 1]);
                Thread.Sleep(1500);
            }
            else
            {
                Console.WriteLine("Turn Skipped");
                Thread.Sleep(1500);
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
