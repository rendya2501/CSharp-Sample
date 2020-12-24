using System.Collections.Generic;

namespace NullableDictionary
{
    public static class ValidItemsSource
    {
        public static Dictionary<Nullable<bool?>, string> ValidItems => new Dictionary<Nullable<bool?>, string>()
        {
            {null, "すべて" },
            {true, "使用する" },
            {false, "使用しない" }
        };
    }

    public static class IntItemsSource
    {
        public static Dictionary<Nullable<int?>, string> IntItems => new Dictionary<Nullable<int?>, string>()
        {
            {null, "null" },
            {0, "0" },
            {1, "1" }
        };
    }

    public static class StringItemsSource
    {
        public static Dictionary<Nullable<string>, string> StringItems => new Dictionary<Nullable<string>, string>()
        {
            {null, "チェックイン処理"},
            {"001","チェックイン処理"},
            {"002","アドバンスチェックイン処理"},
            {"003","スタート入力処理"},
            {"004","売掛伝票入力・会費伝票入力"},
            {"005","利用伝票入力"},
            {"006","現金振替入力"},
            {"007","伝票一括入力"},
            {"008","振替伝票入力"},
            {"009","チェックアウト処理(個人精算)"},
            {"010","チェックアウト処理"},
        };
    }
}
