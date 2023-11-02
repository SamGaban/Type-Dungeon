using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using HeroesVersusMonstersLibrary.Abilities;

namespace HeroesVersusMonstersLibrary.Board
{
    public static class QTE
    {
        public static void RunChallenge(string challenge, int timeLimitInSeconds, Entity sendingEntity, Ability ability, Entity receivingEntity)
        {
            Console.WriteLine(challenge);

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            StringBuilder userInput = new StringBuilder();
            while (true)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                    if (keyInfo.Key == ConsoleKey.Enter)  // Enter key
                    {
                        break;
                    }

                    if (keyInfo.Key == ConsoleKey.Backspace)  // Backspace key
                    {
                        if (userInput.Length > 0)
                        {
                            userInput.Remove(userInput.Length - 1, 1);
                            Console.Write("\b \b");
                        }
                    }
                    else
                    {
                        userInput.Append(keyInfo.KeyChar);
                        Console.Write(keyInfo.KeyChar);  // Manually write the key char to the console
                    }
                }


                if (stopwatch.Elapsed >= TimeSpan.FromSeconds(timeLimitInSeconds))
                {
                    break;
                }
            }

            stopwatch.Stop();

            if (userInput.ToString().Trim() == challenge)
            {
                Console.WriteLine("\nSuccess");
                receivingEntity.UseAbility(receivingEntity.Abilities[0], sendingEntity);
                Thread.Sleep(2000);
            }
            else
            {
                Console.WriteLine("\nToo slow! / Wrong input");
                sendingEntity.UseAbility(sendingEntity.Abilities[0], receivingEntity);
                Thread.Sleep(2000);
            }

            // Clear the input buffer
            while (Console.KeyAvailable)
            {
                Console.ReadKey(true);
            }
        }
    }
}
