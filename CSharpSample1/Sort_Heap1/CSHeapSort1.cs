using System;

namespace Sort_Heap1
{
    class CSHeapSort1
    {
        /// <summary>
        /// https://algoful.com/Archive/Algorithm/HeapSort
        /// </summary>
        public static void Execute()
        {
            Console.WriteLine("C#版ヒープソート1");
            var targetArray = new int[10] { 10, 9, 5, 8, 3, 2, 4, 6, 7, 1 };
            Console.WriteLine(string.Join(",", targetArray));
            HeapSort(targetArray);
            Console.WriteLine(string.Join(",", targetArray));
        }

        /// <summary>
        /// ヒープソートを実行します。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <remarks>
        /// 1.データを取り出し、ヒープに追加(up-heap)します。すべてのデータをヒープに追加するまで繰り返します。
        /// 2.ルートデータを取り出します(down-heap)。ソート済データに取り出したデータを追加します。
        ///   全データ繰り返してソートが完了します。
        /// ヒープ構造はデータ自体の並びのみで表現できる(ポインタなどを持つ必要がない)という利点を活かし、
        /// 元のデータ領域をそのまま使うことで内部ソートとして実装できます。
        /// ヒープ構造のデータとして表現すると、n番目の要素の親要素のインデックスは (n - 1) / 2 となります。
        /// 同じく子要素(左)のインデックスは 2n + 1 で求まります。
        /// </remarks>
        private static void HeapSort<T>(T[] array) where T : IComparable<T>
        {
            int i = 0;
            // 1.データを全てヒープへ追加
            while (i < array.Length)
            {
                UpHeap(array, i++);
            }
            // 2.
            while (--i > 0)
            {
                // ヒープの最大値を末端へ移動
                Swap(ref array[0], ref array[i]);
                // ヒープを再構成
                DownHeap(array, i - 1);
            }
        }

        /// <summary>
        /// ヒープへの追加
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="n"></param>
        /// <remarks>
        /// 1.ヒープの最下層(配列の場合はヒープデータの直後)へデータを追加(up-heap)します。
        /// 2.追加されたデータの親データと比較し、順序が正しければ処理完了です。
        /// 3.比較結果が正しくなければ、親データと交換して、停止するまで2.を繰り返します。
        /// </remarks>
        private static void UpHeap<T>(T[] array, int n) where T : IComparable<T>
        {
            while (n != 0)
            {
                // ary[n]の親要素のインデックス
                int parent = (n - 1) / 2;
                if (array[n].CompareTo(array[parent]) > 0)
                {
                    // 交換後のインデックスを保持
                    Swap(ref array[n], ref array[parent]);
                    n = parent;
                }
                else
                {
                    break;
                }
            }
        }

        /// <summary>
        /// ヒープの取り出し(ルートの削除)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="n"></param>
        /// <remarks>
        /// 1.ルートデータを取り出し再構成(down-heap)します、ヒープ最下層のデータと交換します。
        /// 2.ルートデータを子データと比較し、正しい順序であれば処理完了です。
        /// 3.比較結果が正しくなければ、子データと交換して、停止するまで2.を繰り返します。
        /// </remarks>
        private static void DownHeap<T>(T[] array, int n) where T : IComparable<T>
        {
            if (n == 0)
            {
                return;
            }
            int parent = 0;
            while (true)
            {
                int child = 2 * parent + 1; // ary[n]の子要素
                if (child > n)
                {
                    break;
                }
                if ((child < n) && array[child].CompareTo(array[child + 1]) < 0)
                {
                    child++;
                }
                if (array[parent].CompareTo(array[child]) < 0) // 子要素より小さい場合スワップ
                {
                    Swap(ref array[parent], ref array[child]);
                    parent = child; // 交換後のインデックスを保持
                }
                else
                {
                    break;
                }
            }
        }

        private static void Swap<T>(ref T a, ref T b)
        {
            var tmp = a;
            a = b;
            b = tmp;
        }
    }
}
