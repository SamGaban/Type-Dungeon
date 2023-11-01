using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroesVersusMonstersLibrary.Abilities
{
    public class DelawareSmash : Ability
    {
        public DelawareSmash()
        {
            this._active = true;
            this._name = "Delaware Smash";
            this._staminaCost = 8;
            this._baseDamage = 12;
        }
    }
}
