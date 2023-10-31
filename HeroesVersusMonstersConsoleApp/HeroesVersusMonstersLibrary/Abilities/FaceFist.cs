using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroesVersusMonstersLibrary.Abilities
{
    public class FaceFist : Ability
    {

        public FaceFist()
        {
            this._active = true;
            this._name = "FaceFist";
            this._staminaCost = 1;
            this._baseDamage = 4;
        }
    }
}
