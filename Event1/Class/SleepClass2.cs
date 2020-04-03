using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Event1.Class
{
    class SleepClass2
    {
        //デリゲートの宣言
        public delegate void TimeEventHandler(string message, int number);
        //イベントデリゲートの宣言
        public event TimeEventHandler Time;

        protected virtual void OnTime(string message,int number)
        {
            Time?.Invoke(message,number);
        }

        public void Start()
        {
            System.Threading.Thread.Sleep(5000);
            OnTime("End", 5000);
        }
    }
}
