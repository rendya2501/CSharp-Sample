using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConsoleApp3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 重たい処理
        /// </summary>
        /// <param name="label"></param>
        /// <returns></returns>
        private async Task CalcAsync(Label label)
        {
            label.Text = @"wait...";
            await Task.Run(() =>
            {
                Thread.Sleep(3000);
            });
            label.Text = @"done.";
        }

        /// <summary>
        /// フリーズしない処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void button1_Click_1(object sender, EventArgs e)
        {
            this.button1.Enabled = false;

            var t1 = CalcAsync(this.label1);
            var t2 = CalcAsync(this.label2);
            var t3 = CalcAsync(this.label3);

            // すべて完了するまで待機
            await Task.WhenAll(t1, t2, t3);

            this.button1.Enabled = true;
        }
    }
}
