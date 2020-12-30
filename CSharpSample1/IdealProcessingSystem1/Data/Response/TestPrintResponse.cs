namespace IdealProcessingSystem1.Data.Response
{
    /// <summary>
    /// 印刷データ
    /// </summary>
    public class TestPrintResponse
    {
        /// <summary>
        /// タイトル
        /// </summary>
        public string Title { get; set; }

        #region 来場者
        /// <summary>
        /// 縦のヘッダー名1
        /// </summary>
        public string VerticalHeaderName1 { get; set; }

        /// <summary>
        /// 横のヘッダー名1
        /// </summary>
        public string HorizontalHeaderName1 { get; set; }

        /// <summary>
        /// セル00
        /// </summary>
        public int AttendanceCell00 { get; set; }

        /// <summary>
        /// セル01
        /// </summary>
        public int AttendanceCell01 { get; set; }

        /// <summary>
        /// セル10
        /// </summary>
        public int AttendanceCell10 { get; set; }

        /// <summary>
        /// セル11
        /// </summary>
        public int AttendanceCell11 { get; set; }
        #endregion

        #region 伝票詳細
        /// <summary>
        /// 当日額
        /// </summary>
        public decimal TodayPrice { get; set; }

        /// <summary>
        /// 本月額
        /// </summary>
        public decimal MonthPrice { get; set; }

        /// <summary>
        /// 本年額
        /// </summary>
        public decimal YearPrice { get; set; }

        /// <summary>
        /// 前年額
        /// </summary>
        public decimal LastYearPrice { get; set; }
        #endregion
    }
}
