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


        // Function that takes a position x, y and a list of strings, display the options, makes the user able to chose with up and down arrows and enter, returns int of choice
        public static int ChoiceGenerator(int posx, int posy, List<String> list)
        {
            Console.CursorVisible = false;
            Dictionary<int, String> choicesDic = new Dictionary<int, String>();
            int counter = 0;
            int currentChoice = 0;
            foreach (String s in list)
            {
                choicesDic.Add(counter++, s);
            }
            bool notChosen = true;
            Console.SetCursorPosition(posx, posy - 1);
            Console.WriteLine("See options with Up and Down arrows, and select your choice with Enter");
            Console.SetCursorPosition(posx, posy);
            Console.WriteLine(choicesDic[currentChoice]);
            ConsoleKeyInfo key;
            while (notChosen)
            {
                if (Console.KeyAvailable)
                {
                    key = Console.ReadKey(true);
                    switch (key.Key)
                    {
                        case ConsoleKey.UpArrow:
                            currentChoice = (currentChoice == 0) ? list.Count() - 1 : currentChoice - 1;
                            Console.SetCursorPosition(posx, posy);
                            Console.WriteLine(choicesDic[currentChoice]);
                            break;
                        case ConsoleKey.DownArrow:
                            currentChoice = (currentChoice == list.Count() - 1) ? 0 : currentChoice + 1;
                            Console.SetCursorPosition(posx, posy);
                            Console.WriteLine(choicesDic[currentChoice]);
                            break;
                        case ConsoleKey.Enter:
                            notChosen = false;
                            break;
                    }
                }
            }
            return currentChoice;

        }
    }


}
