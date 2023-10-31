using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroesVersusMonstersLibrary.Board
{
    public class Board
    {

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

        public Board(int horizontalsize, int verticalsize, List<Entity> entitylist)
        {
            this._horizontalSize = horizontalsize;
            this._verticalSize = verticalsize;
            this._entityList = entitylist;
        }

        public void Display()
        {
            int posX = 0;
            int posY = this._verticalSize + 4;

            foreach (Entity entity in this.EntityList)
            {
                Console.SetCursorPosition(posX, posY);
                Console.Write($"┌───────────────────────");
                posY++;
                Console.SetCursorPosition(posX, posY);
                Console.Write($"│{entity.Name}");
                posY++;
                Console.SetCursorPosition(posX, posY);
                Console.Write($"│HP : {entity.HealthPoints} / {entity.MaxHealthPoints}");
                posY++;
                Console.SetCursorPosition(posX, posY);
                Console.Write($"│Stamina : {entity.Stamina} / {entity.MaxStamina}");
                posY++;
                posX += 25;
                posY = this._verticalSize + 4;

            }
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
        }
    }
}
