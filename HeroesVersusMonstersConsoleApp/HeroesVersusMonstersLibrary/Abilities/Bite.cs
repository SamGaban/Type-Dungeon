using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroesVersusMonstersLibrary.Abilities
{
    public class Bite : Ability
    {
        public Bite()
        {
            this._active = true;
            this._name = "Bite";
            this._staminaCost = 0;
            this._baseDamage = 3;
        }
    }
}
