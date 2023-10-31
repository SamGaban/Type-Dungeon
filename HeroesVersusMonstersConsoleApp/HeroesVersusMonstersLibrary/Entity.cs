using HeroesVersusMonstersLibrary.Abilities;

namespace HeroesVersusMonstersLibrary
{
    public class Entity
    {

        #region Props

        // List of abilities linked to the entity

        protected List<Ability> _abilities = new List<Ability>();

        public List<Ability> Abilities
        {
            get { return _abilities; }
            private set { _abilities = value; }
        }



        protected string _race = "Unraced";

		public string Race
		{
			get { return _race; }
			private set { _race = value; }
		}

		protected string _ascii = " X ";

        public string ASCII
        {
            get { return _ascii; }
            private set { _ascii = value; }
        }


        protected string _name = "unnamed";

		public string Name
		{
			get { return _name; }
			private set { _name = (value != null) ? value : "unnamed"; }
		}

		protected bool _alive = true;

		public bool Alive
		{
			get { return _alive; }
			private set { _alive = value; }
		}

		protected int _strength = 0;

		public int Strength
		{
			get { return _strength; }
			private set { _strength = value; }
		}

		protected int _strengthModifier = 0;

		public int StrengthModifier
		{
			get { return _strengthModifier; }
			private set { _strengthModifier = value; }
		}


		protected int _stamina;

		public int Stamina
		{
			get { return _stamina; }
			private set { _stamina = value; }
		}

        // Max stamina to keep track of the difference between used and unused

        protected int _maxStamina;

        public int MaxStamina
        {
            get { return _maxStamina; }
            private set { _maxStamina = value; }
        }

        protected int _staminaModifier = 0;

		public int StaminaModifier
		{
			get { return _staminaModifier; }
			private set { _staminaModifier = value; }
		}


		protected bool _playerControlled = false;

		public bool PlayerControlled
		{
			get { return _playerControlled; }
			private set { _playerControlled = value; }
		}



		protected int _healthPoints;

		public int HealthPoints
		{
			get { return _healthPoints; }
			private set { _healthPoints = value; }
		}

		protected int _maxHealthPoints;

		public int MaxHealthPoints
		{
			get { return _maxHealthPoints; }
			private set { _maxHealthPoints = value; }
		}

		#endregion

		//Repleneshing stamina to max

		public void StaminaToMax()
		{
			this._stamina = this._maxStamina;
		}


		//Using an ability on an entity

		public void UseAbility(Ability ability, Entity ennemy)
		{
			Random random = new Random();
			int modifier = random.Next(1, this.StrengthModifier + 1);
			ennemy._healthPoints -= ability.BaseDamage * modifier;
			this._stamina -= ability.StaminaCost;
			ennemy._stamina -= ability.BaseDamage;
			if (ennemy.MaxStamina > 0)
			{
            Console.WriteLine($"{this.Name} hit {ennemy.Name} for {ability.BaseDamage * modifier} dmg !");
            Console.WriteLine($"{ennemy.Name} lost {ability.BaseDamage} stamina for the next turn");
            }
			else
			{
            Console.WriteLine($"{this.Name} hit {ennemy.Name} for {ability.BaseDamage * modifier} dmg !");
			}
			if (ennemy.HealthPoints <= 0)
			{
				ennemy._alive = false;
			}

        }

        public Entity()
        {
			_strength = Dice.RollForStats();
			_stamina = Dice.RollForStats();
			CalculateModifier();
        }


		//Calculation modifier based on strength and stamina stat
		public virtual void CalculateModifier()
		{
			switch (this._strength)
			{
				case < 5:
                    this._strengthModifier = -1;
					break;
				case < 10:
                    this._strengthModifier = 0;
					break;
				case < 15:
                    this._strengthModifier = 1;
					break;
				default:
                    this._strengthModifier = 2;
					break;
			}

            switch (this._stamina)
            {
                case < 5:
                    this._staminaModifier = -1;
                    break;
                case < 10:
                    this._staminaModifier = 0;
                    break;
                case < 15:
                    this._staminaModifier = 1;
                    break;
                default:
                    this._staminaModifier = 2;
                    break;
            }

        }

    }
}