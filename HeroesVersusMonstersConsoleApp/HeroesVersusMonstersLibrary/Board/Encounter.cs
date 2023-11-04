using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HeroesVersusMonstersLibrary;
using HeroesVersusMonstersLibrary.Board;
using HeroesVersusMonstersLibrary.Loots;
using HeroesVersusMonstersLibrary.Abilities;
using HeroesVersusMonstersLibrary.Generators;

namespace HeroesVersusMonstersLibrary.Board
{

	#region Props
	public class Encounter
    {
		private List<Entity> _entities = new List<Entity>();

		public List<Entity> Entities
		{
			get { return _entities; }
			private set { _entities = value; }
		}

		private Terrain _mapLink;

		public Terrain MapLink
		{
			get { return _mapLink; }
			private set { _mapLink = value; }
		}

		private int _posX;

		public int PosX
		{
			get { return _posX; }
			private set { _posX = value; }
		}

		private int _posY;

		public int PosY
		{
			get { return _posY; }
			private set { _posY = value; }
		}

		private int _power;

		public int Power
		{
			get { return _power; }
			private set { _power = value; }
		}
		#endregion


		#region ctor
		public Encounter(EncounterGenerator generator, int power, int posx, int posy)
        {
			this._posX = posx;
			this._posY = posy;
            this._power = power;
			Random rnd = new Random();
			for (int i = 0; i < rnd.Next(1, 5); i++)
			{
				Monster newMonster = new Monster(rnd.Next(3));
                this._entities.Add(newMonster);
			}
        }
		#endregion

    }
}