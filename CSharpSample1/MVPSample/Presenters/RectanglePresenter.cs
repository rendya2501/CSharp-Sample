using MVPSample.Models;
using MVPSample.Views;

namespace MVPSample.Presenters
{
    public class RectanglePresenter
    {
        readonly IRectangle rectangleView;

        public RectanglePresenter(IRectangle rectangleView)
        {
            this.rectangleView = rectangleView;
        }

        public void CalculateArea()
        {
            Rectangle rectangle = new()
            {
                Length = double.Parse(rectangleView.LengthText),
                Breadth = double.Parse(rectangleView.BreadthText)
            };
            rectangleView.AreaText = rectangle.CalculateArea().ToString();
        }
    }
}
