using System.Windows.Forms;
using WindowsFormsApp1.Data;

namespace WindowsFormsApp1.View
{
    public partial class Form1 : Form
    {
        protected WindowsFormsApp1.ViewModel.ViewModel ViewModel { get; private set; } = new WindowsFormsApp1.ViewModel.ViewModel();

        public Form1()
        {
            InitializeComponent();
            this.label1.Bind(() => ViewModel.Counter.Value);
            this.button1.Bind(ViewModel.UpCommand);
            this.button2.Bind(ViewModel.DownCommand);
        }
    }
}
