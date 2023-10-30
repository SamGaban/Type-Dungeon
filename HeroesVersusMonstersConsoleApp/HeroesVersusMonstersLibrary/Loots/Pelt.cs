using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroesVersusMonstersLibrary.Loots
{

    // Pelt Type loot taking a monster as a parameter, creating a pelt from said monster
    public class Pelt : GenericLoot
    {
        protected int _value;

        public int Value
        {
            get { return _value; }
            private set { _value = value; }
        }


        public Pelt(Monster monster)
        {
            this._type = $"{monster.Name}'s Pelt";
        }
    }
}
