namespace Sort_Merge1
{
    interface ISwap
    {
        public static void Swap<T>(ref T a, ref T b)
        {
            var tmp = a;
            a = b;
            b = tmp;
        }
    }
}
