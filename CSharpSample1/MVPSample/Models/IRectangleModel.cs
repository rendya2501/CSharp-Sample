namespace MVPSample.Models
{
    public interface IRectangleModel
    {
        public double Length { get; set; }
        public double Breadth { get; set; }
        public double CalculateArea();
    }
}
