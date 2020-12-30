using IdealProcessingSystem1.Data.Entity;
using IdealProcessingSystem1.Data.Request;
using IdealProcessingSystem1.Data.Response;
using IdealProcessingSystem1.Model;
using System.Collections.Generic;

namespace IdealProcessingSystem1
{
    /// <summary>
    /// モデル
    /// </summary>
    public class TestModel
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public TestModel()
        {
            // 本来ならDIする
        }

        public object Print()
        {
            var condition = new PrintCondition();
            var printData = GetPrintData(condition);
            // 本来ならほかにも色々ある
            return new object();
        }


        public IEnumerable<TestPrintResponse> GetPrintData(PrintCondition condition)
        {
            // 拡張印刷条件を生成します。
            var exCon = new ExtendPrintCondition(condition);
            // 印刷データ生成クラスにおいて生成したデータを返却します。
            return new PrintDataCreater(exCon).GetPrintData();
        }
    }
}
