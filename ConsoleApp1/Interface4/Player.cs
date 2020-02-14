using System;
using System.Collections.Generic;
using System.Text;

namespace Interface4
{
    public interface IPlayer
    {
        public int Attack(int amount);
    }

    public interface IPowerPlayer : IPlayer
    {
        int IPlayer.Attack(int amount)
        {
            return amount + 50;
        }
    }

    public interface ILimitedPlayer : IPlayer
    {
        int IPlayer.Attack(int amount)
        {
            return amount + 10;
        }
    }

    public class WeakPlayer : ILimitedPlayer
    {
    }

    public class StrongPlayer : IPowerPlayer
    {
    }


    class Player
    {
        private void Main()
        {
            IPlayer powerPlayer = new StrongPlayer();
            Console.WriteLine(powerPlayer.Attack(5));// Output 55

            IPlayer limitedPlayer = new WeakPlayer();
            Console.WriteLine(limitedPlayer.Attack(5));  // Output 15
        }
    }
}
