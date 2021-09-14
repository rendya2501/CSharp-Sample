using System;

namespace Sort_Merge1
{
    class CMergeSort
    {
        /// <summary>
        /// https://daeudaeu.com/merge_sort/
        /// このサイトが一番わかりやすい。
        /// </summary>
        public static void Execute()
        {
            var array = new int[10] { 5, 0, 9, 7, 1, 6, 3, 8, 4, 2 };
            var work = new int[10];
            Console.WriteLine(string.Join(",", array));
            MergeSort(array, 0, array.Length - 1, work);
            Console.WriteLine(string.Join(",", array));
        }

        /// <summary>
        /// マージソートを行う
        /// </summary>
        /// <param name="array">ソートを行うデータの集合(ソート後のデータの集合を格納)</param>
        /// <param name="left">ソートを行う範囲の開始点</param>
        /// <param name="right">ソートを行う範囲の終了点</param>
        /// <param name="work">ソートを行うために必要な作業メモリ</param>
        private static void MergeSort(int[] array, int left, int right, int[] work)
        {
            // ソートを行うデータ数が1つになった場合は処理終了
            if (left == right) return;

            // 集合を中央で2つに分割する
            // left = midの集合とmid+1 ~ rightの集合に分割
            var mid = (left + right) / 2;

            // 分割後の各集合のデータをそれぞれソートする
            MergeSort(array, left, mid, work);
            MergeSort(array, mid + 1, right, work);

            // ソート済みの各集合をマージする
            Merge(array, left, mid, right, work);
        }

        /// <summary>
        /// 2つの集合をマージする関数
        /// </summary>
        /// <param name="data">マージを行うデータの集合(マージ後のデータの集合を格納)</param>
        /// <param name="left">マージする１つ目の集合範囲の開始点</param>
        /// <param name="mid">マージする１つ目の集合の範囲の終了点</param>
        /// <param name="right">マージする２つ目の集合の範囲の終了点</param>
        /// <param name="work">マージを行うために必要な作業メモリ(マージ先集合として一時退避先として使用)</param>
        public static void Merge(int[] data, int left, int mid, int right, int[] work)
        {
            // 1つ目の集合の開始点をセット
            int i = left;
            // 2つ目の集合の開始点をセット
            int j = mid + 1;
            // マージ先集合の開始点をセット
            int k = 0;

            // 2つの集合のどちらかが全てマージ済みになるまでループ
            while (i <= mid && j <= right)
            {
                // マージ済みデータを抜いた2つの集合の先頭のデータの小さいほうをマージ
                if (data[i] < data[j])
                {
                    work[k] = data[i];
                    // マージした集合のインデックスをインクリメント
                    i++;
                    // マージ先集合のインデックスをインクリメント
                    k++;
                }
                else
                {
                    work[k] = data[j];
                    // マージした集合のインデックスをインクリメント
                    j++;
                    // マージ先集合のインデックスをインクリメント
                    k++;
                }
            }
            // マージ済みでないデータが残っている集合をマージ先集合にマージ
            while (i <= mid)
            {
                work[k] = data[i];
                i++;
                k++;
            }
            while (j <= right)
            {
                work[k] = data[j];
                j++;
                k++;
            }

            // マージ先集合をdataにコピー
            for (int x = 0; x < right - left + 1; x++)
            {
                data[left + x] = work[x];
            }
        }
    }
}
