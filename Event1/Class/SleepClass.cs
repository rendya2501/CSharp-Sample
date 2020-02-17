using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Event1.Class
{
    public class TimeEventArgs : EventArgs
    {
        public string Message;
    }

    public class SleepClass
    {
        //デリゲート宣言
        //TimeEventArgs型のオブジェクトを返すようにする
        public delegate void TimeEventHandler(object sender, TimeEventArgs e);

        //イベントデリゲートの宣言
        public event TimeEventHandler Time;

        protected virtual void OnTime(TimeEventArgs e)
        {
            Time?.Invoke(this, e);
        }
        public void Start()
        {
            System.Threading.Thread.Sleep(5000);
            //返すﾃﾞｰﾀの設定
            TimeEventArgs e = new TimeEventArgs();
            e.Message = "終わったよ";
            //イベントの発生
            OnTime(e);
        }
    }
}
