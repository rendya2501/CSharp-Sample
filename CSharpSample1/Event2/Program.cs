using System;


namespace Event2
{
    class Program
    {
        static void Main(string[] args)
        {
            var test = new Test();
            test.TestEvent += new EventHandler<TestEventArgs>(test_TestEvent);
            test.Fire();
            test.BaseFire();
            Console.Read();
        }
        static void test_TestEvent(object sender, TestEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
    }

    /// <summary>
    /// 基底クラス
    /// </summary>
    public class TestBase
    {
        //イベントをvirtualで宣言する。
        internal virtual event EventHandler<TestEventArgs> TestEvent;
        //イベントを発生させる
        public void BaseFire()
        {
            TestEventArgs args = new TestEventArgs("Fired on Base class");
            TestEvent(this, args);//派生クラス側で処理がないとイベントを発生できないので、ここでExceptionが発生する。
        }
    }

    /// <summary>
    /// 派生クラス
    /// </summary>
    public class Test : TestBase
    {
        internal override event EventHandler<TestEventArgs> TestEvent;
        public Test()
        {
            //基底クラスのイベントにイベント伝達用のイベントハンドラを登録する。
            base.TestEvent += new EventHandler<TestEventArgs>(Test_TestEvent);
        }
        //イベント伝達用のイベントハンドラ
        void Test_TestEvent(object sender, TestEventArgs e)
        {
            //利用先に基底クラスのイベントを伝達する
            this.TestEvent(sender, e);
        }
        //イベントを発生させる
        public void Fire()
        {
            TestEvent(this, new TestEventArgs("Fired on Extended class"));
        }
    }

    /// <summary>
    /// テストイベント引数
    /// </summary>
    public class TestEventArgs : EventArgs
    {
        public TestEventArgs(string message)
        {
            this.Message = message;
        }
        public string Message { get; set; }
    }
}
