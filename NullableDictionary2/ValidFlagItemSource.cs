using System;
using System.Collections.Generic;
using System.Text;

namespace NullableDictionary2
{
    /// <summary>
    /// 使用フラグアイテムソース
    /// </summary>
    public static class ValidFlagItemSource
    {
        /// <summary>
        /// すべてを含む一覧
        /// </summary>
        public static IList<KeyValuePair<bool?, string>> ValidFlagListHasBlank
        {
            get
            {
                return new List<KeyValuePair<bool?, string>>()
                {
                    new KeyValuePair<bool?, string>(null, "すべて"),
                    new KeyValuePair<bool?, string>(true, "使用する"),
                    new KeyValuePair<bool?, string>(false, "使用しない"),
                };
            }
        }
    }

}
