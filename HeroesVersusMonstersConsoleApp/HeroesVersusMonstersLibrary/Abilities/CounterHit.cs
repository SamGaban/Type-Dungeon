using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroesVersusMonstersLibrary.Abilities
{
    public class CounterHit : Ability
    {
        public CounterHit()
        {
            this._active = false;
            this._name = "Counter Hit";
            this._staminaCost = 0;
            this._baseDamage = 3;
        }
    }
}
