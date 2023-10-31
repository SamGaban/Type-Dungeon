using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroesVersusMonstersLibrary.Abilities
{

	//Generic ability class
    public class Ability
    {

		// Is the ability an active or reactive one, active by default (Active is to use a your turn, reactive is to use as defense to another entity's turn)

		protected bool _active = true;

		public bool Active
		{
			get { return _active; }
			private set { _active = value; }
		}


		//Name of the ability to display in game


		protected string _name = "Unnamed Ability";

		public string Name
		{
			get { return _name; }
			private set { _name = value; }
		}

		// Stamina drain of the ability

		protected int _staminaCost = 0;

		public int StaminaCost
		{
			get { return _staminaCost; }
			private set { _staminaCost = value; }
		}

		//Base damage of the ability that will be multiplied by the strength modifier

        protected int _baseDamage;

        public int BaseDamage
        {
            get { return _baseDamage; }
            private set { _baseDamage = value; }
        }

    }
}
