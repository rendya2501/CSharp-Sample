using System;

namespace Sort1
{
    class Program
    {
        /// <summary>
        /// ①中央値を決める
        /// ②中央値以下のグループと以上のグループに分ける。
        /// ③分けたグループの中で中央値を決め、同じロジックを実行する。
        /// ④ソートできなくなるまで繰り返す
        /// https://www.hanachiru-blog.com/entry/2020/03/08/120000
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            var targetArray = new int[11] { 11, 300, 10, 51, 126, 1, 53, 14, 12, 55, 6 };

            Console.WriteLine(string.Join(",", targetArray));

            // クイックソートを行う
            QuickSort(targetArray, 0, targetArray.Length - 1);

            Console.WriteLine(string.Join(",", targetArray));
        }

        /// <summary>
        /// クイックソート
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array">対称配列</param>
        /// <param name="left">ソート範囲の最初のインデックス</param>
        /// <param name="right">ソート範囲の最後のインデックス</param>
        public static void QuickSort<T>(T[] array, int left, int right) where T : IComparable<T>
        {
            // 範囲が1つだけなら処理を抜ける
            if (left >= right) return;

            // ピボット:グループ分けの基準。枢軸(pivot)
            // ピボットを選択(範囲の先頭・真ん中・末尾の中央値を使用)
            T pivot = Median(array[left], array[(left + right) / 2], array[right]);

            int i = left;
            int j = right;

            while (i <= j)
            {
                // array[i] < pivotまで左から探索
                while (i < right && array[i].CompareTo(pivot) < 0) i++;
                // array[j] >= pivotまで右から探索
                while (j > left && array[j].CompareTo(pivot) >= 0) j--;

                if (i > j) break;
                Swap<T>(ref array[i], ref array[j]);

                // 交換を行った要素の次の要素にインデックスを進める
                i++;
                j--;
            }

            QuickSort(array, left, i - 1);
            QuickSort(array, i, right);

        }

        /// <summary>
        /// 中央値を求める
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <returns></returns>
        private static T Median<T>(T x, T y, T z) where T : IComparable<T>
        {
            // x > y なら1以上の整数値が返却される。
            if (x.CompareTo(y) > 0) Swap(ref x, ref y);
            if (x.CompareTo(z) > 0) Swap(ref x, ref z);
            if (y.CompareTo(z) > 0) Swap(ref y, ref z);

            return y;
        }

        private static void Swap<T>(ref T x, ref T y)
        {
            var tmp = x;
            x = y;
            y = tmp;
        }
    }
}
