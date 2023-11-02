using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroesVersusMonstersLibrary.Board
{
    public class Terrain
    {
		protected Entity _hero;

		public Entity Hero
		{
			get { return _hero; }
			private set { _hero = value; }
		}

		protected int _posXHero;

		public int PosXHero
		{
			get { return _posXHero; }
			private set { _posXHero = value; }
		}

		protected int _posYHero;

		public int PosYHero
		{
			get { return _posYHero; }
			private set { _posYHero = value; }
		}

		private List<List<MapCell>> _map = new List<List<MapCell>>();


        public List<List<MapCell>> Map
		{
			get { return _map; }
			private set { _map = value; }
		}


		public Terrain(Entity hero)
        {
            this._hero = hero;
			GenerateBase();
			Display();
        }

		//Generating the base map

		public void GenerateBase()
		{
			for (int y = 0; y < 16;  y++)
			{
                List<MapCell> row = new List<MapCell>();
                for (int x = 0; x < 160; x++)
				{
					row.Add(new MapCell(x, y, 0));
				}
				this._map.Add(row);
			}
		}

		public void Display()
		{
			for (int y = 0;y < Map.Count;y++)
			{
				for(int x = 0;x < Map[y].Count;x++)
				{
					Console.Write(Map[y][x].Type);
                }
                Console.WriteLine();
            }
		}

    }
}

// 1 = "█"
// 0 = " "
// -1 = "╔"
// -2 = "═"
// -3 = "╗"
// - 4 = "╚"
// -5 = "╝"