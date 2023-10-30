using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroesVersusMonstersLibrary.Loots
{

	//Generic loot class created for polymorphism reasons
    public class GenericLoot
    {
		protected string _type = "untyped";

		public string Type
		{
			get { return _type; }
			private set { _type = value; }
		}

    }
}
