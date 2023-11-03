using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroesVersusMonstersLibrary.Board
{
    public class Terrain
    {
		#region Props
		private int _id;

		public int Id
		{
			get { return _id; }
			private set { _id = value; }
		}


		private bool _active = false;

		public bool Active
		{
			get { return _active; }
			private set { _active = value; }
		}

		private int _activeX;

		public int ActiveX
		{
			get { return _activeX; }
			private set { _activeX = value; }
		}

		private int _activeY;

		public int ActiveY
		{
			get { return _activeY; }
			private set { _activeY = value; }
		}


		private List<List<MapCell>> _map = new List<List<MapCell>>();


        public List<List<MapCell>> Map
		{
			get { return _map; }
			private set { _map = value; }
		}
		#endregion

		#region CTOR
		public Terrain()
        {
			GenerateBase();
			CreateBorderWalls();
        }
		#endregion

		// PositionHandler

		public void SetActivePosition(int x, int y)
		{
			this._activeX = x;
			this._activeY = y;
		}

		// Map deactivator

		public void DeActivate()
		{
			this._active = false;
		}

		//Map Activator

		public void Activate()
		{
			this._active = true;
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

		//Generate walls

		public void GenerateWall(int x, int y, int sizehorizontal, int sizevertical)
		{
			int maxSizeHorizontal = 159;
			int maxSizeVertical = 15;

			for (int i = y; i < y + sizevertical; i++)
			{
				for (int j = x; j < x + sizehorizontal; j++)
				{
					if (i < maxSizeVertical && i > 0 && j < maxSizeHorizontal && j > 0 && this._map[i][j].Type == 0)
					{
						this._map[i][j].ChangeType(1);
					}
				}
			}

		}

		// Basic map display test

		public void Display()
		{
			for (int y = 0;y < Map.Count;y++)
			{
				for(int x = 0;x < Map[y].Count;x++)
				{
					string charToDisplay = "";
					switch (Map[y][x].Type)
					{
						case int n when n < -1000:
                            charToDisplay = "E";
							break;
						case -6:
							charToDisplay = "║";
                            break;
						case -5:
							charToDisplay = "╝";
							break;
						case -4:
							charToDisplay = "╚";
							break;
                        case -3:
							charToDisplay = "╗";
							break;
                        case -2:
							charToDisplay = "═";
							break;
                        case -1:
							charToDisplay = "╔";
							break;
                        case 0:
							charToDisplay = " ";
							break;
                        case 1:
							charToDisplay = "█";
							break;

                    }
                    Console.Write(charToDisplay);
                }
                Console.WriteLine();
            }
		}

		// Changing a tile of the map

		public void ChangeTileType(int coordinateX, int coordinateY, int newtype)
		{
			Map[coordinateY][coordinateX].ChangeType(newtype);
		}

		// Basic surround of the map by walls

		public void CreateBorderWalls()
		{
			ChangeTileType(0, 0, -1);
			ChangeTileType(159, 0, -3);
			ChangeTileType(0, 15, -4);
			ChangeTileType(159, 15, -5);
			for (int i = 1; i < 159; i++)
			{
				ChangeTileType(i, 0, -2);
				ChangeTileType(i, 15, -2);
			}
			for (int i = 1; i < 15; i++)
			{
				ChangeTileType(0, i, -6);
				ChangeTileType(159, i, -6);
			}
        }

    }
}