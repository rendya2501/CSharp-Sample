namespace Abstract_Interface
{
    public abstract class Bell
    {
        protected string sound;

        public Bell()
        {
            this.sound = "ting";
        }

        abstract public void Ring();

        public void IncreaseVolume()
        {
            Console.WriteLine("Increasing Volume");
        }

        public void DecreaseVolume()
        {
            Console.WriteLine("Decreasing Volume");
        }
    }

    public class SchoolBell : Bell
    {
        public override void Ring()
        {
            Console.WriteLine($"Ringing the School Bell : {sound}");
        }
    }

    public class ChruchBell : Bell
    {
        public override void Ring()
        {
            Console.WriteLine($"Ringing the Chruch Bell : {sound}");
        }
    }
}
