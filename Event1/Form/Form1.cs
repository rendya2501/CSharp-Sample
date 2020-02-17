using Event1.Class;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Event1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //SleepClass sleepClass = new SleepClass();
            //sleepClass.Time += new SleepClass.TimeEventHandler(this.SleepClass_Time);
            //sleepClass.Start();

            SleepClass2 clsSleep = new SleepClass2();
            clsSleep.Time += new SleepClass2.TimeEventHandler(this.SleepClass2_Time);
            clsSleep.Start();
        }

        private void SleepClass_Time(object sender, TimeEventArgs e)
        {
            //返されたﾃﾞｰﾀを取得し表示
            MessageBox.Show(e.Message);
        }
        private void SleepClass2_Time(string message, int number)
        {
            MessageBox.Show(message + ":" + number.ToString());
        }
    }
}
