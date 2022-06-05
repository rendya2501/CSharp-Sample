using System;

namespace MVPSample.Views
{
    public interface IRectangleView
    {
        string LengthText { get; set; }
        string BreadthText { get; set; }
        string AreaText { get; set; }
        event EventHandler Calculate;
        void Show();
    }
}
