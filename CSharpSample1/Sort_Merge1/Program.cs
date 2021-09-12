using System;

namespace Sort_Merge1
{
    /// <summary>
    /// マージソート
    /// 1.データを2つのデータに分割する
    /// 2.分割したデータを更にマージソートでソートする
    /// 3.分割されたデータをマージ(小さい方から順に取り出して並べる)
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            MergeSort1.Execute();
            JavaMergeSort.Execute();
        }
    }

    class MergeSort1 : ISwap
    {
        /// <summary>
        /// https://algoful.com/Archive/Algorithm/MergeSort
        /// </summary>
        public static void Execute()
        {
            var targetArray = new int[11] { 11, 300, 10, 51, 126, 1, 53, 14, 12, 55, 6 };
            Console.WriteLine(string.Join(",", targetArray));
            MergeSort(targetArray);
            Console.WriteLine(string.Join(",", targetArray));
        }

        private static void MergeSort<T>(T[] array) where T : IComparable<T>
        {
            // 検査配列の半分の長さの配列を生成する
            var work = new T[(array.Length + 1) / 2];
            // 最初の1回は配列全て、左端インデックス、右端インデックス、一時配列を渡す
            // 配列全体をマージソート
            MergeSort(array, 0, array.Length - 1, work);
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <param name="work"></param>
        private static void MergeSort<T>(T[] array, int left, int right, T[] work) where T : IComparable<T>
        {
            // 左右のインデックスが同じになったら処理終了
            if (left == right) return;

            // 中央のインデックスを求める
            var mid = left + (right - left) / 2;

            // 分割した左側をマージソート
            // 1,left:0,mid:5
            // 2,left:0,mid:2
            // 3,left:0,mid:1
            // 4,left:0,mid:0
            // 5,left:3,mid:4
            // 6,left:3,mid:3
            // 7,left:6,mid:8
            // 8,left:6,mid:7
            // 9,left:6,mid:6
            // 10,left:9,mid:9
            MergeSort(array, left, mid, work);

            // 分割した右側をマージソート
            // 1,mid:1,right:1
            // 2,mid:2,right:2
            // 3,mid:3,right:5
            // 4,mid:4,right:4
            // 5,mid:5,right:5
            // 6,mid:6,right:10
            // 7,mid:7,right:7
            // 8,mid:8,right:8
            // 9,mid:9,right:10
            // 10,mid:10,right:10
            MergeSort(array, mid + 1, right, work);

            // ソートされた左右のデータをマージ
            // 1,left:0,right:1,mid:0
            // 2,left:0,right:2,mid:1
            // 3,left:3,right:4,mid:3
            // 4,left:3,right:5,mid:4
            // 5,left:0,right:5,mid:2
            // 6,left:6,right:7,mid:6
            // 7,left:6,right:8,mid:7
            // 8,left:9,right:10,mid:9
            // 9,left:6,right:10,mid:8
            // 10,left:0,right:10,mid:5
            Merge(array, left, right, mid, work);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ary"></param>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <param name="mid"></param>
        /// <param name="work"></param>
        public static void Merge<T>(T[] ary, int left, int right, int mid, T[] work) where T : IComparable<T>
        {
            // 左部分を作業領域に退避
            for (int i = left; i <= mid; i++)
            {
                work[i - left] = ary[i];
            }

            int l = left;
            int r = mid + 1;
            // 左部分と右部分(作業領域)をマージ
            while (true)
            {
                // メイン配列指定用のインデックス
                int k = l + r - (mid + 1);

                // 左部分(作業領域)が並べ終わったらマージ完了(右部分残りはもとの位置のまま)
                if (l > mid) break;

                // 右部分が並べ終わったら左部分(作業領域)をすべて並べて完了
                if (r > right)
                {
                    while (l <= mid)
                    {
                        k = l + r - (mid + 1);
                        ary[k] = work[l++ - left];
                    }
                    break;
                }

                // 小さい方の値を配列に並べ、インクリメントしておく
                // 
                ary[k] = work[l - left].CompareTo(ary[r]) < 0
                    ? work[l++ - left]
                    : ary[r++];
            }
        }
    }
}
