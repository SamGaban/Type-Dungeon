namespace HeroesVersusMonstersLibrary
{
    public class Entity
    {

		#region Props

		protected string _race = "Unraced";

		public string Race
		{
			get { return _race; }
			private set { _race = value; }
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
			get { return _alive = true; }
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

		protected int _staminaModifier = 0;

		public int StaminaModifier
		{
			get { return _staminaModifier; }
			private set { _staminaModifier = value; }
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

        public Entity()
        {
			_strength = Dice.RollForStats();
			_stamina = Dice.RollForStats();
        }

		public virtual void DisplayStatus()
		{
			string finalString = $"Name : {this.Name}\nStrength : {this.Strength}\nStamina : {this.Stamina}\nHP : {this.HealthPoints}/{this.MaxHealthPoints}\n";
            Console.WriteLine(finalString);
        }

    }
}