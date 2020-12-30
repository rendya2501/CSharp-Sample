using IdealProcessingSystem1.Data.Request;

namespace IdealProcessingSystem1.Data.Entity
{
    /// <summary>
    /// 拡張印刷条件
    /// </summary>
    public class ExtendPrintCondition : PrintCondition
    {
        public string ExtendCondition1 { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="condition">印刷条件</param>
        public ExtendPrintCondition(PrintCondition condition)
        {
            this.Condition1 = condition.Condition1;
        }
    }
}
