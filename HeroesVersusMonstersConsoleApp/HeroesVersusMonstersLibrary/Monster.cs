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
    public class Monster : Entity
    {

        //Value modifier to handle how much gold this mob is worth

        protected int _valueModifier;

		public int ValueModifier
		{
			get { return _valueModifier; }
			private set { _valueModifier = value; }
		}


        //Method to add an ability to the monster
        public void AddAbility(Ability ability)
        {
            this._abilities.Add(ability);
        }


        //Specifications related to monster type
        public Monster(int raceModifier)
        {
            switch (raceModifier)
            {
                case 0:
                    this._race = "Wolf";
                    this._name = "Wolf";
                    this._ascii = AsciiArt.wolf;
                    this._valueModifier = 1;
                    Pelt pelt = new Pelt(this);
                    GoldCoin gold = new GoldCoin();
                    _lootTable[pelt] = Dice.Roll(0, 4);
                    this.AddAbility(new Bite());
                    break;
                case 1:
                    this._race = "Orc";
                    this._name = "Orc";
                    this._ascii = AsciiArt.orc;
                    this._strengthModifier += 1;
                    this._valueModifier = 2;
                    GoldCoin gold1 = new GoldCoin();
                    _lootTable[gold1] = Dice.Roll(1, 6) * this._valueModifier;
                    this.AddAbility(new Maul());
                    break;
                case 2:
                    this._race = "Dragonling";
                    this._name = "Dragonling";
                    this._ascii = AsciiArt.dragonling;
                    this._staminaModifier += 1;
                    this._valueModifier = 3;
                    Pelt pelt2 = new Pelt(this);
                    GoldCoin gold2 = new GoldCoin();
                    _lootTable[pelt2] = Dice.Roll(0, 4);
                    _lootTable[gold2] = Dice.Roll(1, 6) * this._valueModifier;
                    this.AddAbility(new Fireball());
                    break;
            }

            this._healthPoints = (_stamina + _staminaModifier) * 2;
            this._maxHealthPoints = _healthPoints;

        }

    }
}
