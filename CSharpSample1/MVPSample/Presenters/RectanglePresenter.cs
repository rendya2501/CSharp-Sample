using MVPSample.Models;
using MVPSample.Views;
using System;

namespace MVPSample.Presenters
{
    public class RectanglePresenter
    {
        readonly IRectangleView rectangleView;
        readonly IRectangleModel rectangleModel;

        public RectanglePresenter(IRectangleView view, IRectangleModel model)
        {
            rectangleView = view;
            rectangleModel = model;
            rectangleView.Calculate += new EventHandler((o,e) => CalculateArea());
        }

        public void CalculateArea()
        {
            rectangleModel.Length = double.Parse(rectangleView.LengthText);
            rectangleModel.Breadth = double.Parse(rectangleView.BreadthText);
            rectangleView.AreaText = rectangleModel.CalculateArea().ToString();
        }
    }
}
