using System;

namespace Sort1
{
    class Program
    {
        /// <summary>
        /// ①中央値を決める
        /// ②中央値より小さいグループと大きいグループに分ける。
        /// ③分けたグループの中で中央値を求め、同じように小さいグループと大きいグループに分ける。
        /// ④これをソートできなくなるまで繰り返す
        /// https://www.hanachiru-blog.com/entry/2020/03/08/120000
        /// </summary>
        /// <param name="args"></param>
        /// <remarks>
        /// 処理の内容的に、先に小さいグループの再帰が完了するまで繰り返される。
        /// 小さいグループの再帰処理が終了してから大きいグループの再帰処理が実行されて、
        /// それも終わったらソートは完了しているというわけ。
        /// </remarks>
        public static void Main(string[] args)
        {
            var targetArray = new int[11] { 11, 300, 10, 51, 126, 1, 53, 14, 12, 55, 6 };

            Console.WriteLine(string.Join(",", targetArray));

            // クイックソートを行う
            QuickSort(targetArray, 0, targetArray.Length - 1);

            Console.WriteLine(string.Join(",", targetArray));

            JavaQuickSort.Execute();
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
            // これの否定は left < right なので、左が中央を突き破って右にいかない限りは続ける事を意味する。
            if (left >= right) return;

            // ピボット:グループ分けの基準。枢軸(pivot)
            // ピボットを選択(範囲の先頭・真ん中・末尾の中央値を使用)
            T pivot = Median2(array[left], array[(left + right) / 2], array[right]);

            // 左の現在位置 0
            int pl = left;
            // 右の現在位置 10
            int pr = right;

            while (true)
            {
                // 左の要素と中央値を比較して、左の要素が小さければ、左を1つ右に進める。
                // 中央値より小さい場合、中央値より小さいグループに、大きい場合は中央値より大きいグループに分けるため。
                while (array[pl].CompareTo(pivot) < 0) pl++;
                // 右の要素と中央値を比較して、右の要素が大きければ、右を1つ左に進める。
                // 中央値より大きい場合、中央値より大きいグループに、小さい場合は中央値より小さいグループに分けるため。
                while (array[pr].CompareTo(pivot) > 0) pr--;
                // 左と右のインデックスが同じか、左右が交差したら終了
                if (pl >= pr) break;
                // 現在の左の位置と右の位置の数字を入れ替える
                Swap<T>(ref array[pl], ref array[pr]);

                // 交換を行った要素の次の要素にインデックスを進める
                // 左は1つ右に。右は1つ左にインデックスを進める。
                pl++;
                pr--;
            }
            // 小さいグループ内で同じロジックを繰り替えす。
            // 繰り返して行くと、小さいグループ内で更に小さいグループと大きいグループにわかれるので、それも同じように繰り返す。
            QuickSort(array, left, pl - 1);
            // 大きいグループ内で同じロジックを繰り替えす。
            // 繰り返していくと、大きいグループ内で更に大きいグループと小さいグループにわかれるので、それも同じように繰り返す。
            QuickSort(array, pr + 1, right);
        }

        /// <summary>
        /// 3値から中央値を求める
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <returns></returns>
        /// <remarks>
        /// x.CompareTo(y)はx > y なら1以上の整数値が返却される。
        /// </remarks>
        private static T Median<T>(T x, T y, T z) where T : IComparable<T>
        {
            // 初期値で見れば、x:11,y:1,z:6と入ってくる。
            // xがyより大きい場合、xとyを入れ替える。 x=1,y=11
            if (x.CompareTo(y) > 0) Swap(ref x, ref y);
            // xがzより大きい場合、xとzを入れ替える。 x=1,z=6
            if (x.CompareTo(z) > 0) Swap(ref x, ref z);
            // yがzより大きい場合、yとzを入れ替える。 y=6,z=11
            if (y.CompareTo(z) > 0) Swap(ref y, ref z);
            // 初期値で見れば、6が中央値となる。
            return y;
        }


        /// <summary>
        /// 3値の中から中央値を返す
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <returns></returns>
        /// <remarks>
        /// 上の方式より効率がいい。
        /// 上は最悪の場合、6回の演算を行うが、こちらは最悪でも3回で済む。
        /// x:11,y:1,z:6と入ってくる。
        /// </remarks>
        public static T Median2<T>(T x, T y, T z) where T : IComparable<T> =>
            x.CompareTo(y) < 0
                ? x.CompareTo(z) < 0
                    ? (y.CompareTo(z) < 0 ? y : z)
                    : x
                : y.CompareTo(z) < 0
                    ? (x.CompareTo(z) < 0 ? x : z)
                    : y;

        //// (11 < 6) = 1 < 0 なのでelse
        //if (x.CompareTo(y) < 0)
        //{
        //    // xがzより小さい場合、
        //    return x.CompareTo(z) < 0 ? (y.CompareTo(z) < 0 ? y : z) : x;
        //}
        //
        //else
        //{ 
        // (1 < 6) = -1 < 0なのでtrue。
        // (11 < 6) = 1 < 0なのでfalse,zの6が中央値として返却される。
        //    return y.CompareTo(z) < 0 ? (x.CompareTo(z) < 0 ? x : z) : y;
        //}



        private static void Swap<T>(ref T x, ref T y)
        {
            var tmp = x;
            x = y;
            y = tmp;
        }
    }
}
