using System.Collections.Generic;

namespace NullableDictionary2
{
    /// <summary>
    /// 課税区分アイテムソース
    /// </summary>
    public static class TaxationTypeItemSource
    {
        /// <summary>
        /// 課税区分一覧
        /// </summary>
        public static Dictionary<TaxationType, string> TaxationTypeList
        {
            get
            {
                return new Dictionary<TaxationType, string>()
                {
                    { TaxationType.OutsideTax, "外税" },
                    { TaxationType.InsideTax, "内税" },
                    { TaxationType.TaxFree, "非課税" }
                };
            }
        }

        /// <summary>
        /// 課税区分一覧
        /// </summary>
        public static Dictionary<TaxationType, string> ShortTaxationTypeList
        {
            get
            {
                return new Dictionary<TaxationType, string>()
                {
                    { TaxationType.OutsideTax, "外" },
                    { TaxationType.InsideTax, "内" },
                    { TaxationType.TaxFree, "非" }
                };
            }
        }
    }

    /// <summary>
    /// 課税区分項目
    /// </summary>
    public enum TaxationType : byte
    {
        /// <summary>
        /// 外税
        /// </summary>
        OutsideTax = 1,
        /// <summary>
        /// 内税
        /// </summary>
        InsideTax = 2,
        /// <summary>
        /// 非課税
        /// </summary>
        TaxFree = 3
    }
}
