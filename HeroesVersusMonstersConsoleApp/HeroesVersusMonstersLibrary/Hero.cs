using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroesVersusMonstersLibrary
{
    public class Hero : Entity
    {
        public Hero()
        {
            Console.WriteLine("What Race will you pick ? (1. Human / 2. Dwarf)");
            this._race = Console.ReadLine() == "1" ? "Human" : "Dwarf";
            Console.WriteLine("What will your name be ?");
            string? nameTemp = Console.ReadLine();
            this._name = (nameTemp != null) ? nameTemp : "Unnamed Hero";
            switch (this._race)
            {
                case "Human":
                    this._staminaModifier = 1;
                    this._strengthModifier = 1;
                    break;
                case "Dwarf":
                    this._staminaModifier = 2;
                    break;
            }
            this._healthPoints = (_stamina + _staminaModifier) * 10;
            this._maxHealthPoints = _healthPoints;
        }
    }
}
