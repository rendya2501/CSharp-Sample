using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

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

        public static object Convert2(object value, object parameter)
        {
            // パラメータの型変換
            var dictionary = (IList)parameter;
            var count = dictionary.Count;

            for (int i = 0; i < count; i++)
            {
                // これは当然うまくいく
                //if (dictionary[i] is KeyValuePair<bool?, string> conv)
                //{
                //    if (conv.Key == (bool?)value)
                //    {
                //        return conv.Value;
                //    }
                //}


                if (dictionary[i] is KeyValuePair<object, object> conv)
                {
                    if (conv.Key == value)
                    {
                        return conv.Value;
                    }
                }


                Type valueType = dictionary[i].GetType();
                if (valueType.IsGenericType)
                {
                    Type baseType = valueType.GetGenericTypeDefinition();
                    if (baseType == typeof(KeyValuePair<,>))
                    {
                        Type[] argTypes = baseType.GetGenericArguments();

                        object kvpKey = valueType.GetProperty("Key")?.GetValue(value, null);
                        object kvpValue = valueType.GetProperty("Value")?.GetValue(value, null);
                    }

                    //Type baseType = valueType.GetGenericTypeDefinition();
                    //if (baseType == typeof(KeyValuePair<,>))
                    //{
                    //    var key = baseType.GetProperty("Key", BindingFlags.Public | BindingFlags.Instance).GetValue(value, null);
                    //    var value2 = baseType.GetProperty("Value", BindingFlags.Public | BindingFlags.Instance).GetValue(value, null);
                    //}
                }
            }

            // インデクサーで値を取得
            return dictionary.Contains(value);
        }
    }
}
