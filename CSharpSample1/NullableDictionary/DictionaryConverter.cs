using System;
using System.Collections;
using System.Collections.Generic;


namespace NullableDictionary
{
    /// <summary>
    /// DictionaryのKeyをValueに変換するコンバーター
    /// </summary>
    public static class DictionaryConverter
    {
        /// <summary>
        /// DictionaryのKeyをValueに変換します。
        /// </summary>
        /// <param name="value">バインディング ソースによって生成された値</param>
        /// <param name="targetType">バインディング ターゲット プロパティの型</param>
        /// <param name="parameter">使用するコンバーター パラメーター</param>
        /// <param name="culture">コンバーターで使用するカルチャ</param>
        /// <returns></returns>
        public static object Convert(object value, object parameter)
        {
            // if (!(parameter is IDictionary)) throw new Exception("型");
            // パラメータの型変換
            var dictionary = (IDictionary)parameter;
            // インデクサーで値を取得
            return dictionary[value];
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        /// <remarks>
        /// 参考サイト : https://stackoverflow.com/questions/2729614/c-sharp-reflection-how-can-i-tell-if-object-o-is-of-type-keyvaluepair-and-then
        /// </remarks>
        public static object Convert2(object value, object parameter)
        {
            // null判定
            if (parameter == null) throw new Exception("null");
            // 型の判定とIListへ変換
            if (!(parameter is IList list)) throw new Exception("型");
            // 要素をループ
            foreach (var item in list)
            {
                // 値が一般的であることを確認
                Type valueType = item.GetType();
                if (valueType.IsGenericType)
                {
                    // ジェネリック型の定義を抽出
                    Type baseType = valueType.GetGenericTypeDefinition();
                    // KeyValuePair型の判定
                    if (baseType == typeof(KeyValuePair<,>))
                    {
                        // その中の値のタイプを抽出
                        //Type[] argTypes = baseType.GetGenericArguments();

                        // KeyとValueの取得
                        var kvpKey = valueType.GetProperty("Key")?.GetValue(item, null);
                        var kvpValue = valueType.GetProperty("Value")?.GetValue(item, null);
                        // Keyと引数valueの比較
                        if (kvpKey?.Equals(value) ?? kvpKey == value)
                        {
                            return kvpValue;
                        }
                    }
                }
            }
            // Keyに合致するものがなければnullを返却。
            return null;
        }
    }
}
