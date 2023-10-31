using HeroesVersusMonstersLibrary.Abilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroesVersusMonstersLibrary
{
    public class Hero : Entity
    {


        #region props

        // List of abilities linked to the hero

        protected List<Ability> _abilities = new List<Ability>();

        public List<Ability> Abilities
        {
            get { return _abilities; }
            private set { _abilities = value; }
        }



        #endregion


        public void AddAbility(Ability ability)
        {
            this._abilities.Add(ability);
        }


        public Hero()
        {
            this._playerControlled = true;
            Console.WriteLine("What Race will you pick ? (1. Human / 2. Dwarf)");
            this._race = Console.ReadLine() == "1" ? "Human" : "Dwarf";
            Console.WriteLine("What will your name be ?");
            string? nameTemp = Console.ReadLine();
            this._name = (nameTemp != null) ? nameTemp : "Unnamed Hero";
            switch (this._race)
            {
                case "Human":
                    this._staminaModifier += 1;
                    this._strengthModifier += 1;
                    break;
                case "Dwarf":
                    this._staminaModifier += 2;
                    break;
            }
            this._healthPoints = (_stamina + _staminaModifier) * 10;
            this._maxHealthPoints = _healthPoints;
            this._maxStamina = this._stamina;
            this.AddAbility(new FaceFist());
        }
    }
}
