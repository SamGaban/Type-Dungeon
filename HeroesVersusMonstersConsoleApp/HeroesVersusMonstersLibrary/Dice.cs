using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroesVersusMonstersLibrary
{
    public static class Dice
    {


        static Random rnd = new Random();

        // Generic Roll For All Game purposes
        public static int Roll(int numberOfDice, int valueOfDice)
        {
            int finalNumber = 0;
            for (int i = 0; i < numberOfDice; i++)
            {
                int roll = rnd.Next(1, (valueOfDice + 1));
                finalNumber += roll;
            }
            return finalNumber;
        }

        //Roll made only for stats rolling (4 dices, keep 3 best)
        public static int RollForStats()
        {
            List<int> rollsList = new List<int>();
            for (int i = 0; i < 4; i++)
            {
                int roll = rnd.Next(1, (7));
                rollsList.Add(roll);
            }

            int maxRollPlusOne = 7;

            foreach (int roll in rollsList)
            {
                if (roll < maxRollPlusOne)
                {
                    maxRollPlusOne = roll;
                }
            }

            rollsList.Remove(maxRollPlusOne);

            return rollsList.Sum();

        }

    }


}
