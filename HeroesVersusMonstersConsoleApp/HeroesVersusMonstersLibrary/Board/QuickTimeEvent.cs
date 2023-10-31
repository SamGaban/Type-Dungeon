using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HeroesVersusMonstersLibrary;
using HeroesVersusMonstersLibrary.Board;
using HeroesVersusMonstersLibrary.Loots;
using HeroesVersusMonstersLibrary.Abilities;

namespace HeroesVersusMonstersLibrary.Board
{
    public class QuickTimeEvent
    {
        private string _challenge;
        private int _timeLimitInSeconds;

        private Entity _sendingEntity;

        public Entity SendingEntity
        {
            get { return _sendingEntity; }
            private set { _sendingEntity = value; }
        }

        private Ability _ability;

        public Ability Ability
        {
            get { return _ability; }
            private set { _ability = value; }
        }

        private Entity _receivingEntity;

        public Entity ReceivingEntity
        {
            get { return _receivingEntity; }
            private set { _receivingEntity = value; }
        }




        public QuickTimeEvent(string challenge, int timeLimitInSeconds, Entity sending, Ability ability, Entity receiving)
        {
            _challenge = challenge;
            _timeLimitInSeconds = timeLimitInSeconds;
            _sendingEntity = sending;
            _ability = ability;
            _receivingEntity = receiving;
        }

        public async Task RunChallengeAsync()
        {
            Console.WriteLine(_challenge);

            var cts = new CancellationTokenSource();
            Task timeoutTask = Task.Delay(TimeSpan.FromSeconds(_timeLimitInSeconds), cts.Token);
            Task<string?> inputTask = Task.Run(() => Console.ReadLine(), cts.Token);

            if (await Task.WhenAny(inputTask, timeoutTask) == inputTask && inputTask.Result == _challenge)
            {
                Console.WriteLine();
                Console.WriteLine("Success");
                Console.WriteLine();
                Console.WriteLine("Avoiding damages");

                Thread.Sleep(2000);
            }
            else
            {
                Console.WriteLine("Too slow!");

                Thread.Sleep(2000);

                Console.WriteLine();
                _sendingEntity.UseAbility(_sendingEntity.Abilities[0], _receivingEntity);

                Thread.Sleep(2000);
            }

            cts.Cancel();
        }
    }
}
