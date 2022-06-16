using IdealProcessingSystem1.Data.Entity;
using IdealProcessingSystem1.Data.Response;
using System.Collections.Generic;

namespace IdealProcessingSystem1.Model
{
    /// <summary>
    /// 印刷データ生成クラス
    /// </summary>
    public class PrintDataCreater
    {
        /// <summary>
        /// 印刷データ
        /// </summary>
        private readonly List<TestPrintResponse> PrintData;
        /// <summary>
        /// 拡張印刷条件
        /// </summary>
        private readonly ExtendPrintCondition ExCon;

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="exCon">拡張印刷条件</param>
        public PrintDataCreater(ExtendPrintCondition exCon)
        {
            // 印刷データを生成するだけなので、拡張印刷条件は作らない。
            // 外部からもらって使うだけ。
            ExCon = exCon;
        }
        #endregion

        /// <summary>
        /// 印刷データを取得します。
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TestPrintResponse> GetPrintData()
        {
            var printData = new List<TestPrintResponse>();
            
            // 来場者データ生成
            CreateAttendance();
            // 伝票データ生成
            CreateSlip();
            // 生成した印刷データを返却します。
            return printData;
        }

        #region 来場者
        /// <summary>
        /// 来場者データを生成します。
        /// </summary>
        private void CreateAttendance()
        {
            if (ExCon != null)
            {

            }
            PrintData[0].AttendanceCell00 = 0;
            PrintData[0].AttendanceCell01 = 1;
            PrintData[0].AttendanceCell10 = 2;
            PrintData[0].AttendanceCell11 = 3;
        }
        #endregion

        #region 伝票
        /// <summary>
        /// 伝票データを生成します。
        /// </summary>
        private void CreateSlip()
        {
            if (ExCon != null)
            {

            }
            PrintData[0].TodayPrice = 100;
            PrintData[0].MonthPrice = 200;
            PrintData[0].YearPrice = 300;
            PrintData[0].LastYearPrice = 400;
        }
        #endregion
    }
}
