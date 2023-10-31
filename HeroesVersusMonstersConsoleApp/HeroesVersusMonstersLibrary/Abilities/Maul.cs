using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroesVersusMonstersLibrary.Abilities
{
    public class Maul : Ability
    {
        public Maul()
        {
            this._active = true;
            this._name = "Maul";
            this._staminaCost = 0;
            this._baseDamage = 5;
        }
    }
}
