using HeroesVersusMonstersLibrary.Abilities;
using HeroesVersusMonstersLibrary.Board;
using HeroesVersusMonstersLibrary.Loots;
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


        #endregion


        public void AddAbility(Ability ability)
        {
            this._abilities.Add(ability);
        }


        public Hero()
        {
            Console.CursorVisible = true;
            this._playerControlled = true;
            Console.WriteLine(AsciiArt.startScreen);
            Console.WriteLine("What Race will you pick ? (1. Human / 2. Dwarf)");
            Console.WriteLine();
            this._race = Console.ReadLine() == "1" ? "Human" : "Dwarf";
            Console.Clear();
            Console.WriteLine(AsciiArt.startScreen);
            Console.WriteLine(this._race == "Human" ? AsciiArt.warrior : AsciiArt.dwarf);
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
            this._healthPoints = (_stamina + _staminaModifier) * 2;
            this._maxHealthPoints = _healthPoints;
            this._maxStamina = this._stamina;
            this.AddAbility(new CounterHit());
            this.AddAbility(new FaceFist());
            this.AddAbility(new DelawareSmash());
            this.AddToInventory(new GoldCoin(), 10);
            Console.CursorVisible = false;
        }
    }
}
