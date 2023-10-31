using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroesVersusMonstersLibrary.Abilities
{
    public class Fireball : Ability
    {
        public Fireball()
        {
            this._active = true;
            this._name = "Fireball";
            this._staminaCost = 0;
            this._baseDamage = 10;
        }
    }
}
