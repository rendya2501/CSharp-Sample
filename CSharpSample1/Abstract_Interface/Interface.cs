namespace Abstract_Interface
{
    public interface IBell
    {
        public static readonly string sound = "I ting";

        public void Ring();
        public void IncreaseVolume();
        public void DecreaseVolume();
    }

    public class ISchoolBell : IBell
    {
        public void DecreaseVolume()
        {
            Console.WriteLine("Decreasing Volume of ISchool Bell");
        }

        public void IncreaseVolume()
        {
            Console.WriteLine("Increasing Volume of ISchool Bell");
        }

        public void Ring()
        {
            Console.WriteLine($"Ringing the ISchool bell : {IBell.sound}");
        }
    }

    public class IChurchBell : IBell
    {
        public void DecreaseVolume()
        {
            Console.WriteLine("Decreasing Volume of IChurch Bell");
        }

        public void IncreaseVolume()
        {
            Console.WriteLine("Increasing Volume of IChurch Bell");
        }

        public void Ring()
        {
            Console.WriteLine($"Ringing the IChurch bell : {IBell.sound}");
        }
    }
}
