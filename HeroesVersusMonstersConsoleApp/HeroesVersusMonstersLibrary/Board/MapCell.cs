using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroesVersusMonstersLibrary.Board
{
    public class MapCell
    {

		#region Props

		protected int _x;

		public int X
		{
			get { return _x; }
			private set { _x = value; }
		}

		protected int _y;

		public int Y
		{
			get { return _y; }
			private set { _y = value; }
		}

		protected int _type = 0;

		public int Type
		{
			get { return _type; }
			private set { _type = value; }
		}
		#endregion

        public MapCell(int x, int y, int type)
		{
			_x = x;
			_y = y;
			_type = type;
		}

		//Method to change the type of a map tile

		public void ChangeType(int newtype)
		{
			_type = newtype;
		}

    }
}
