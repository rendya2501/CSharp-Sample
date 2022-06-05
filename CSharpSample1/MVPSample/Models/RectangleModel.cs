namespace MVPSample.Models
{
    public class RectangleModel : IRectangleModel
    {
        public double Length { get; set; }
        public double Breadth { get; set; }
        public double CalculateArea() => Length * Breadth;
    }
}
