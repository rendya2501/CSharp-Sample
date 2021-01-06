using System;

namespace IdealProcessingSystem1
{
    class Program
    {
        static void Main(string[] args)
        {
            // 日次帳票印刷API生成において、理想の形は何か色々考えた結果、行きついたとりあえずの形。
            // 理想の処理系
            // 単一責務の法則に則り、印刷データを生成するだけのクラスを作って、処理を任せたほうがいいのではないかと思ったのでこうした。
            // モデルの中でやるにはいささか大きすぎると思ったので。
            // 休み明けに早速、この形にしてみよう。
        }

        #region 実験の産物 Todo:後で消す
        //private dynamic GetFrontCashDetail(ExtendDailyReportPrintCondition exCon)
        //{
        //    // 当日基本情報を取得
        //    var basicItem = _TFr_BasicItemModel.Get(exCon.OfficeCD);
        //    // フロント現金明細一覧を取得
        //    var frontCashDetail = _TFr_FrontCashDetailModel.GetList(exCon.OfficeCD, exCon.BusinessDate);
        //    // 売上高現金
        //    var cashPriceTotal = _TFr_AmountByPaymentMethodModel.GetCashSettlement(exCon.OfficeCD, exCon.BusinessDate);
        //    // フロント現金売高
        //    var frontCashSalesTotal = cashPriceTotal
        //        + _TFr_SlipModel.GetDepartmentCashSales(exCon.OfficeCD, exCon.BusinessDate)
        //        + _TFr_FrontCashDetailModel.GetFrontCash(exCon.OfficeCD, exCon.BusinessDate);
        //    // 当日釣銭額
        //    var todayChange = basicItem.TodayChangeAmount ?? decimal.Zero;
        //    // フロント現金有高
        //    var frontCashTotal = basicItem.FrontCashAmount ?? decimal.Zero;
        //    // 現金過不足額 = フロント現金有高 - フロント現金売高 - 当日釣銭額
        //    var cashExcessOrDeficiency = frontCashTotal - frontCashSalesTotal - todayChange;
        //    // 翌日繰越額
        //    var carryOverAmount = basicItem.NextdayChangeAmount ?? decimal.Zero;
        //    // 口座振込額
        //    var bankTransferAmount = basicItem.BankTransferAmount ?? decimal.Zero;

        //    return new
        //    {
        //        CashPriceTotal = cashPriceTotal,
        //        FrontCashSalesTotal = frontCashSalesTotal,
        //        TodayChange = todayChange,
        //        FrontCashTotal = frontCashTotal,
        //        CashExcessOrDeficiency = cashExcessOrDeficiency,
        //        CarryOverAmount = carryOverAmount,
        //        BankTransferAmount = bankTransferAmount
        //    };
        //}

        //private void RegistFrontCashDetail2(ref DailyReportPrintGetPrintResponse printData, dynamic frontCashDetail)
        //{
        //    printData.CashPriceTotal = frontCashDetail.CashPriceTotal;
        //    // フロント現金売高
        //    printData.FrontCashSalesTotal = frontCashDetail.FrontCashSalesTotal;
        //    // 当日釣銭額
        //    printData.TodayChange = frontCashDetail.TodayChange;
        //    // フロント現金有高
        //    printData.FrontCashTotal = frontCashDetail.FrontCashTotal;
        //    // 現金過不足額 = フロント現金有高 - フロント現金売高 - 当日釣銭額
        //    printData.CashExcessOrDeficiency = frontCashDetail.CashExcessOrDeficiency;
        //    // 翌日繰越額
        //    printData.CarryOverAmount = frontCashDetail.CarryOverAmount;
        //    // 口座振込額
        //    printData.BankTransferAmount = frontCashDetail.BankTransferAmount;
        //}
        #endregion

        #region 印刷データ生成
        #region 伝票データ
        ///// <summary>
        ///// 伝票データを生成します。
        ///// </summary>
        //private void CreateSlipDatas(ExtendDailyReportPrintCondition exCon)
        //{
        //    // 当日伝票データ取得
        //    var currentSlipDate = GetCurrentSlipData(exCon.DisplayTaxType, exCon.BusinessDate);
        //    // 本月伝票データ取得
        //    var monthSlipData = GetPerformanceSlipData(exCon.DisplayTaxType, exCon.MonthFrom, exCon.MonthTo);
        //    // 本年伝票データ取得
        //    var yearSlipData = GetPerformanceSlipData(exCon.DisplayTaxType, exCon.YearFrom, exCon.YearTo);
        //    // 前年伝票データ取得
        //    var lastYearSlipData = GetPerformanceSlipData(exCon.DisplayTaxType, exCon.LastYearFrom, exCon.LastYearTo);
        //    // 当日データを元に印刷データを生成してセット
        //    foreach (var (item, index) in currentSlipDate.Select((item, index) => (item, index)))
        //    {
        //        var monthPrice = monthSlipData.ElementAt(index).Price;
        //        var yearPrice = yearSlipData.ElementAt(index).Price;
        //        var lastYearPrice = lastYearSlipData.ElementAt(index).Price;

        //        PrintData.Add(
        //            new DailyReportPrintGetPrintResponse()
        //            {
        //                SubjectSummaryCD = item.SubjectSummaryCD,
        //                SubjectClsCD = item.SubjectClsCD,
        //                SubjectClsName = item.SubjectClsName,
        //                SubjectCD = item.SubjectCD,
        //                SubjectName = item.SubjectName,
        //                TodayPrice = item.Price,
        //                MonthPrice = monthPrice,
        //                YearPrice = yearPrice,
        //                LastYearPrice = lastYearPrice,
        //                LastYearContrast = yearPrice - lastYearPrice,
        //                Ratio = lastYearPrice != 0 ? (yearPrice / lastYearPrice) * 100 : 0
        //            }
        //        );
        //    }
        //}

        ///// <summary>
        ///// 当日伝票データを取得します。
        ///// </summary>
        ///// <param name="displayTaxType">金額表示区分</param>
        ///// <param name="businessDate">営業日</param>
        ///// <returns></returns>
        //private IEnumerable<SlipData> GetCurrentSlipData(DisplayTaxType displayTaxType, DateTime businessDate)
        //{
        //    StringBuilder query = new StringBuilder();
        //    query
        //        .AppendLine("SELECT")
        //        .Append(CreateSlipSelectClause(displayTaxType))
        //        .AppendLine("FROM")
        //        .Append(CreateSlipFromClause(businessDate))
        //        .AppendLine("ORDER BY")
        //        .AppendLine("    [Subject].[OfficeCD],")
        //        .AppendLine("    [Subject].[SubjectSummaryCD],")
        //        .AppendLine("    [Subject].[SubjectClsCD],")
        //        .AppendLine("    [Subject].[SubjectCD]");
        //    return _DapperAction.GetDataListByQuery<SlipData>(
        //        ConnectionTypes.Data,
        //        query.ToString(),
        //        new { businessDate }
        //    );
        //}

        ///// <summary>
        ///// 過去伝票データを取得します。
        ///// </summary>
        ///// <param name="displayTaxType">金額表示区分</param>
        ///// <param name="from">開始日</param>
        ///// <param name="to">終了日</param>
        ///// <returns></returns>
        //private IEnumerable<SlipData> GetPerformanceSlipData(DisplayTaxType displayTaxType, DateTime from, DateTime to)
        //{
        //    StringBuilder query = new StringBuilder();
        //    query
        //        .AppendLine("SELECT")
        //        .Append(CreateSlipSelectClause(displayTaxType))
        //        .AppendLine("FROM")
        //        .Append(CreateSlipFromClause(from, to))
        //        .AppendLine("ORDER BY")
        //        .AppendLine("    [Subject].[OfficeCD],")
        //        .AppendLine("    [Subject].[SubjectSummaryCD],")
        //        .AppendLine("    [Subject].[SubjectClsCD],")
        //        .AppendLine("    [Subject].[SubjectCD]");
        //    return _DapperAction.GetDataListByQuery<SlipData>(
        //        ConnectionTypes.Data,
        //        query.ToString(),
        //        new
        //        { from, to }
        //    );
        //}

        ///// <summary>
        ///// 伝票情報取得クエリのSelect区を生成します。
        ///// </summary>
        ///// <param name="displayTaxType"></param>
        ///// <returns></returns>
        //private StringBuilder CreateSlipSelectClause(DisplayTaxType displayTaxType)
        //{
        //    // 税抜税込判定後の金額を入れる
        //    var price = displayTaxType == DisplayTaxType.Included
        //        ? "[PriceTaxIn]"
        //        : "[PriceWithoutTax]";
        //    return new StringBuilder()
        //        .AppendLine("    [Subject].[OfficeCD],")
        //        .AppendLine("    [Subject].[SubjectSummaryCD],")
        //        .AppendLine("    [Subject].[SubjectSummaryName],")
        //        .AppendLine("    [Subject].[SubjectSummaryShortName],")
        //        .AppendLine("    [Subject].[SubjectClsCD],")
        //        .AppendLine("    [Subject].[SubjectClsName],")
        //        .AppendLine("    [Subject].[SubjectClsShortName],")
        //        .AppendLine("    [Subject].[SubjectCD],")
        //        .AppendLine("    [Subject].[SubjectName],")
        //        .AppendLine("    [Subject].[SubjectShortName],")
        //        .AppendLine($"    [Amount].{price} AS [Price],")           // 税抜税込判定後の金額を入れる
        //        .AppendLine("    [Amount].[PriceWithoutTax],")             // 税抜金額
        //        .AppendLine("    [Amount].[DiscountWithoutTax],")          // 税抜値引き額
        //        .AppendLine("    [Amount].[DiscountTaxIn],")               // 税込値引き額
        //        .AppendLine("    [Amount].[DiscountConsumptionTax],")      // 値引き消費税
        //        .AppendLine("    [Amount].[ConsumptionTax],")              // 消費税
        //        .AppendLine("    [Amount].[PriceTaxIn],")                  // 税込金額
        //        .AppendLine("    [Amount].[CaddyPriceWithoutTax],")        // Caddy_税抜金額
        //        .AppendLine("    [Amount].[CaddyDiscountWithoutTax],")     // Caddy_税抜値引き額
        //        .AppendLine("    [Amount].[CaddyDiscountTaxIn],")          // Caddy_税込値引き額
        //        .AppendLine("    [Amount].[CaddyDiscountConsumptionTax],") // Caddy_値引き消費税
        //        .AppendLine("    [Amount].[CaddyConsumptionTax],")         // Caddy_消費税
        //        .AppendLine("    [Amount].[CaddyPriceTaxIn],")             // Caddy_税込金額
        //        .AppendLine("    [Amount].[SelfPriceWithoutTax],")         // Self_税抜金額
        //        .AppendLine("    [Amount].[SelfDiscountWithoutTax],")      // Self_税抜値引き額
        //        .AppendLine("    [Amount].[SelfDiscountTaxIn],")           // Self_税込値引き額
        //        .AppendLine("    [Amount].[SelfDiscountConsumptionTax],")  // Self_値引き消費税
        //        .AppendLine("    [Amount].[SelfConsumptionTax],")          // Self_消費税
        //        .AppendLine("    [Amount].[SelfPriceTaxIn]");              // Self_税込金額
        //}

        ///// <summary>
        ///// 伝票情報取得クエリのFrom区を生成します。
        ///// 引数がFromのみの場合、当日伝票、Toも指定されている場合、過去伝票を対象とします。
        ///// </summary>
        ///// <param name="from"></param>
        ///// <param name="to"></param>
        ///// <returns></returns>
        //private StringBuilder CreateSlipFromClause(DateTime from, DateTime? to = null)
        //{
        //    // fromのみ引数が指定されている場合、当日とみなす。
        //    var tableName = nameof(TFr_Slip);
        //    var dateCondition = "= @businessDate";
        //    if (to.HasValue)
        //    {
        //        tableName = nameof(TPa_Slip);
        //        dateCondition = "BETWEEN @from AND @to";
        //    }

        //    return new StringBuilder()
        //       .AppendLine("    (")
        //       .AppendLine("        SELECT")
        //       .AppendLine("            [TMa_Subject].[OfficeCD],")
        //       .AppendLine("            [TMa_SubjectSummary].[SubjectSummaryCD],")
        //       .AppendLine("            [TMa_SubjectSummary].[SubjectSummaryName],")
        //       .AppendLine("            [TMa_SubjectSummary].[SubjectSummaryShortName],")
        //       .AppendLine("            [TMa_SubjectCls].[SubjectClsCD],")
        //       .AppendLine("            [TMa_SubjectCls].[SubjectClsName],")
        //       .AppendLine("            [TMa_SubjectCls].[SubjectClsShortName],")
        //       .AppendLine("            [TMa_Subject].[SubjectCD],")
        //       .AppendLine("            [TMa_Subject].[SubjectName],")
        //       .AppendLine("            [TMa_Subject].[SubjectShortName]")
        //       .AppendLine("        FROM")
        //       .AppendLine("            [TMa_Subject]")
        //       .AppendLine("            LEFT OUTER JOIN [TMa_SubjectCls] ON [TMa_Subject].[OfficeCD] = [TMa_SubjectCls].[OfficeCD]")
        //       .AppendLine("            AND [TMa_Subject].[SubjectClsCD] = [TMa_SubjectCls].[SubjectClsCD]")
        //       .AppendLine("            LEFT OUTER JOIN [TMa_SubjectSummary] ON [TMa_SubjectCls].[OfficeCD] = [TMa_SubjectSummary].[OfficeCD]")
        //       .AppendLine("            AND [TMa_SubjectCls].[SubjectSummaryCD] = [TMa_SubjectSummary].[SubjectSummaryCD]")
        //       .AppendLine("    ) AS [Subject]")
        //       .AppendLine("    LEFT OUTER JOIN (")
        //       .AppendLine("        SELECT")
        //       .AppendLine("            [Slip].[OfficeCD],")
        //       .AppendLine("            [Slip].[SubjectCD],")
        //       .AppendLine("            SUM(ISNULL([Slip].[PriceWithoutTax], 0)) AS [PriceWithoutTax],")
        //       .AppendLine("            SUM(ISNULL([Slip].[DiscountWithoutTax], 0)) AS [DiscountWithoutTax],")
        //       .AppendLine("            SUM(ISNULL([Slip].[DiscountTaxIn], 0)) AS [DiscountTaxIn],")
        //       .AppendLine("            SUM(ISNULL([Slip].[DiscountConsumptionTax], 0)) AS [DiscountConsumptionTax],")
        //       .AppendLine("            SUM(ISNULL([Slip].[ConsumptionTax], 0)) AS [ConsumptionTax],")
        //       .AppendLine("            SUM(ISNULL([Slip].[PriceTaxIn], 0)) AS [PriceTaxIn],")
        //       .AppendLine("            SUM(")
        //       .AppendLine("                CASE")
        //       .AppendLine("                    [PS].[PlayStyleClsCD]")
        //       .AppendLine("                    WHEN 0 THEN ISNULL([Slip].[PriceWithoutTax], 0)")
        //       .AppendLine("                    ELSE 0")
        //       .AppendLine("                END")
        //       .AppendLine("            ) AS [CaddyPriceWithoutTax],")
        //       .AppendLine("            SUM(")
        //       .AppendLine("                CASE")
        //       .AppendLine("                    [PS].[PlayStyleClsCD]")
        //       .AppendLine("                    WHEN 0 THEN ISNULL([Slip].[DiscountWithoutTax], 0)")
        //       .AppendLine("                    ELSE 0")
        //       .AppendLine("                END")
        //       .AppendLine("            ) AS [CaddyDiscountWithoutTax],")
        //       .AppendLine("            SUM(")
        //       .AppendLine("                CASE")
        //       .AppendLine("                    [PS].[PlayStyleClsCD]")
        //       .AppendLine("                    WHEN 0 THEN ISNULL([Slip].[DiscountTaxIn], 0)")
        //       .AppendLine("                    ELSE 0")
        //       .AppendLine("                END")
        //       .AppendLine("            ) AS [CaddyDiscountTaxIn],")
        //       .AppendLine("            SUM(")
        //       .AppendLine("                CASE")
        //       .AppendLine("                    [PS].[PlayStyleClsCD]")
        //       .AppendLine("                    WHEN 0 THEN ISNULL([Slip].[DiscountConsumptionTax], 0)")
        //       .AppendLine("                    ELSE 0")
        //       .AppendLine("                END")
        //       .AppendLine("            ) AS [CaddyDiscountConsumptionTax],")
        //       .AppendLine("            SUM(")
        //       .AppendLine("                CASE")
        //       .AppendLine("                    [PS].[PlayStyleClsCD]")
        //       .AppendLine("                    WHEN 0 THEN ISNULL([Slip].[ConsumptionTax], 0)")
        //       .AppendLine("                    ELSE 0")
        //       .AppendLine("                END")
        //       .AppendLine("            ) AS [CaddyConsumptionTax],")
        //       .AppendLine("            SUM(")
        //       .AppendLine("                CASE")
        //       .AppendLine("                    [PS].[PlayStyleClsCD]")
        //       .AppendLine("                    WHEN 0 THEN ISNULL([Slip].[PriceTaxIn], 0)")
        //       .AppendLine("                    ELSE 0")
        //       .AppendLine("                END")
        //       .AppendLine("            ) AS [CaddyPriceTaxIn],")
        //       .AppendLine("            SUM(")
        //       .AppendLine("                CASE")
        //       .AppendLine("                    [PS].[PlayStyleClsCD]")
        //       .AppendLine("                    WHEN 0 THEN 0")
        //       .AppendLine("                    ELSE ISNULL([Slip].[PriceWithoutTax], 0)")
        //       .AppendLine("                END")
        //       .AppendLine("            ) AS [SelfPriceWithoutTax],")
        //       .AppendLine("            SUM(")
        //       .AppendLine("                CASE")
        //       .AppendLine("                    [PS].[PlayStyleClsCD]")
        //       .AppendLine("                    WHEN 0 THEN 0")
        //       .AppendLine("                    ELSE ISNULL([Slip].[DiscountWithoutTax], 0)")
        //       .AppendLine("                END")
        //       .AppendLine("            ) AS [SelfDiscountWithoutTax],")
        //       .AppendLine("            SUM(")
        //       .AppendLine("                CASE")
        //       .AppendLine("                    [PS].[PlayStyleClsCD]")
        //       .AppendLine("                    WHEN 0 THEN 0")
        //       .AppendLine("                    ELSE ISNULL([Slip].[DiscountTaxIn], 0)")
        //       .AppendLine("                END")
        //       .AppendLine("            ) AS [SelfDiscountTaxIn],")
        //       .AppendLine("            SUM(")
        //       .AppendLine("                CASE")
        //       .AppendLine("                    [PS].[PlayStyleClsCD]")
        //       .AppendLine("                    WHEN 0 THEN 0")
        //       .AppendLine("                    ELSE ISNULL([Slip].[DiscountConsumptionTax], 0)")
        //       .AppendLine("                END")
        //       .AppendLine("            ) AS [SelfDiscountConsumptionTax],")
        //       .AppendLine("            SUM(")
        //       .AppendLine("                CASE")
        //       .AppendLine("                    [PS].[PlayStyleClsCD]")
        //       .AppendLine("                    WHEN 0 THEN 0")
        //       .AppendLine("                    ELSE ISNULL([Slip].[ConsumptionTax], 0)")
        //       .AppendLine("                END")
        //       .AppendLine("            ) AS [SelfConsumptionTax],")
        //       .AppendLine("            SUM(")
        //       .AppendLine("                CASE")
        //       .AppendLine("                    [PS].[PlayStyleClsCD]")
        //       .AppendLine("                    WHEN 0 THEN 0")
        //       .AppendLine("                    ELSE ISNULL([Slip].[PriceTaxIn], 0)")
        //       .AppendLine("                END")
        //       .AppendLine("            ) AS [SelfPriceTaxIn]")
        //       .AppendLine("        FROM")
        //       .AppendLine($"           [{tableName}] AS [Slip]")
        //       .AppendLine("            LEFT OUTER JOIN [TRe_ReservationPlayer] ON [Slip].[PlayerNo] = [TRe_ReservationPlayer].[PlayerNo]")
        //       .AppendLine("            LEFT OUTER JOIN [TMa_PlayStyle] AS [PS] ON [TRe_ReservationPlayer].[OfficeCD] = [PS].[OfficeCD]")
        //       .AppendLine("            AND [TRe_ReservationPlayer].[PlayStyleCD] = [PS].[PlayStyleCD]")
        //       .AppendLine("        WHERE")
        //       .AppendLine("           (")
        //       .AppendLine("                [Slip].[SetParentCD] IS NULL")
        //       .AppendLine("                OR [Slip].[SetParentCD] = ''")
        //       .AppendLine("           )")
        //       .AppendLine($"          AND [Slip].[BusinessDate] {dateCondition}")
        //       .AppendLine("        GROUP BY")
        //       .AppendLine("            [Slip].[OfficeCD],")
        //       .AppendLine("            [Slip].[SubjectCD]")
        //       .AppendLine("    ) AS [Amount] ON [Subject].[OfficeCD] = [Amount].[OfficeCD]")
        //       .AppendLine("    AND [Subject].[SubjectCD] = [Amount].[SubjectCD]");
        //}
        #endregion

        #region 来場データ
        ///// <summary>
        ///// 来場データを生成します。
        ///// </summary>
        //private void CreateAttendance(ExtendDailyReportPrintCondition exCon)
        //{
        //    // 当日来場者データを生成します。
        //    CreateCurrentAttendance(exCon);
        //    // 本月来場者データを生成します。
        //    CreateMonthAttendance(exCon);
        //    // 本年来場者データを生成します。
        //    CreateYearAttendance(exCon);
        //    // 前年累計来場者データを生成します。
        //    CreateLastYearAttendance(exCon);

        //    // 当日利用税来場者データを生成します。
        //    CreateCurrentUseTaxAttendance(exCon);
        //    // 本月利用税来場者データを生成します。
        //    CreateMonthUseTaxAttendance(exCon);
        //    // 本年利用税来場者データを生成します。
        //    CreateYearUseTaxAttendance(exCon);
        //    // 前年累計利用税来場者データを生成します。
        //    CreateLastYearUseTaxAttendance(exCon);

        //    // 来場者資格欄のヘッダーを生成します。
        //    CreateAttendanceHeader(exCon);
        //}

        #region 共通処理
        ///// <summary>
        ///// 営業報告書出力における統計入場者データ取得処理の共通処理です。
        ///// </summary>
        ///// <param name="officeCD">事業者コード</param>
        ///// <param name="from">開始日</param>
        ///// <param name="to">終了日</param>
        ///// <returns></returns>
        //private IEnumerable<StatisticsVisitorsQueryResult> GetStatisticsVisitors(string officeCD, DateTime from, DateTime? to = null)
        //{
        //    // 条件生成
        //    var dateCondition = "= @businessDate";
        //    dynamic param = new { officeCD, businessDate = from };
        //    if (to.HasValue)
        //    {
        //        dateCondition = "BETWEEN @from AND @to";
        //        param = new { officeCD, from, to };
        //    }
        //    // SQL生成
        //    StringBuilder query = new StringBuilder();
        //    query
        //        .AppendLine("SELECT")
        //        .AppendLine("    [SV].[OfficeCD],")
        //        .AppendLine("    [SV].[BusinessDate],")
        //        .AppendLine("    [SV].[AttendanceCount],")
        //        .AppendLine("    CASE")
        //        .AppendLine("        [TMa_AttendanceType].[AttendanceClsCD]")
        //        .AppendLine("        WHEN 1 THEN 1 --一般来場者の呼名")
        //        .AppendLine("        WHEN 2 THEN 2 --高齢者の呼名")
        //        .AppendLine("        WHEN 3 THEN 2 --身障者の呼名")
        //        .AppendLine("        WHEN 4 THEN 4 --年次会員の呼名")
        //        .AppendLine("    END AS [AttendanceClsCD],")
        //        .AppendLine("    [TMa_PrivilegeType].[PrivilegeClsCD]")
        //        .AppendLine("FROM")
        //        .AppendLine("    (")
        //        .AppendLine("        SELECT")
        //        .AppendLine("            *")
        //        .AppendLine("        FROM")
        //        .AppendLine("            [TAs_StatisticsVisitors]")
        //        .AppendLine("        WHERE")
        //        .AppendLine("            [OfficeCD] = @officeCD")
        //        .AppendLine($"            AND [BusinessDate] {dateCondition}")
        //        .AppendLine("    ) AS [SV]")
        //        .AppendLine("    LEFT OUTER JOIN [TMa_Fee] ON [SV].[OfficeCD] = [TMa_Fee].[OfficeCD]")
        //        .AppendLine("    AND [SV].[FeeCD] = [TMa_Fee].[FeeCD]")
        //        .AppendLine("    LEFT OUTER JOIN [TMa_AttendanceType] ON [TMa_Fee].[OfficeCD] = [TMa_AttendanceType].[OfficeCD]")
        //        .AppendLine("    AND [TMa_Fee].[AttendanceTypeCD] = [TMa_AttendanceType].[AttendanceTypeCD]")
        //        .AppendLine("    LEFT OUTER JOIN [TMa_PrivilegeType] ON [TMa_Fee].[OfficeCD] = [TMa_PrivilegeType].[OfficeCD]")
        //        .AppendLine("    AND [TMa_Fee].[PrivilegeTypeCD] = [TMa_PrivilegeType].[PrivilegeTypeCD]");
        //    // SQL実行
        //    return _DapperAction.GetDataListByQuery<StatisticsVisitorsQueryResult>(
        //         ConnectionTypes.Data,
        //         query.ToString(),
        //         param
        //    );
        //}

        ///// <summary>
        ///// 資格分類コードで絞ったレコード数を取得します。
        ///// </summary>
        ///// <param name="list"></param>
        ///// <param name="attendanceClsCD"></param>
        ///// <param name="privilegeClsCD"></param>
        ///// <returns></returns>
        //private int GetPrivilegeCount(IEnumerable<AttendanceQueryResult> list, int attendanceClsCD, int privilegeClsCD)
        //{
        //    return list
        //        .Where(w => w.AttendanceClsCD == attendanceClsCD && w.PrivilegeClsCD == privilegeClsCD)
        //        .GroupBy(g => new { g.OfficeCD, g.BusinessDate })
        //        .Select(s => s.Count())
        //        .FirstOrDefault();
        //}

        ///// <summary>
        ///// 資格分類コードで絞った来場者数の合計を取得します。
        ///// </summary>
        ///// <param name="list"></param>
        ///// <param name="attendanceClsCD"></param>
        ///// <param name="privilegeClsCD"></param>
        ///// <returns></returns>
        //private int GetPrivilegeSummary(IEnumerable<StatisticsVisitorsQueryResult> list, int attendanceClsCD, int privilegeClsCD)
        //{
        //    return list
        //        .Where(w => w.AttendanceClsCD == attendanceClsCD && w.PrivilegeClsCD == privilegeClsCD)
        //        .GroupBy(g => new { g.OfficeCD, g.BusinessDate })
        //        .Select(s => s.Sum(s => s.AttendanceCount))
        //        .FirstOrDefault();
        //}

        ///// <summary>
        ///// 利用税コードで絞ったレコード数を取得します。
        ///// </summary>
        ///// <param name="list"></param>
        ///// <param name="attendanceClsCD"></param>
        ///// <param name="useTaxCD"></param>
        ///// <returns></returns>
        //private int GetUseTaxCount(IEnumerable<AttendanceQueryResult> list, int attendanceClsCD, int useTaxCD)
        //{
        //    return list
        //        .Where(w => w.AttendanceClsCD == attendanceClsCD && w.UseTaxCD == useTaxCD)
        //        .GroupBy(g => new { g.OfficeCD, g.BusinessDate })
        //        .Select(s => s.Count())
        //        .FirstOrDefault();
        //}

        ///// <summary>
        ///// 利用税コードで絞った来場者数の合計を取得します。
        ///// </summary>
        ///// <param name="list"></param>
        ///// <param name="attendanceClsCD"></param>
        ///// <param name="useTaxCD"></param>
        ///// <returns></returns>
        //private int GetUseTaxSummary(IEnumerable<StatisticsVisitorsQueryResult> list, int attendanceClsCD, int useTaxCD)
        //{
        //    return list
        //        .Where(w => w.AttendanceClsCD == attendanceClsCD && w.UseTaxCD == useTaxCD)
        //        .GroupBy(g => new { g.OfficeCD, g.BusinessDate })
        //        .Select(s => s.Sum(s => s.AttendanceCount))
        //        .FirstOrDefault();
        //}
        #endregion

        #region 当日来場者
        ///// <summary>
        ///// 当日来場者データを生成します。
        ///// </summary>
        ///// <param name="exCon"></param>
        //private void CreateCurrentAttendance(ExtendDailyReportPrintCondition exCon)
        //{
        //    // 当日来場者データと当日統計入場者データを取得します。
        //    var currentAttendanceList = GetCurrentAttendance(exCon);
        //    var currentStatisticsVisitors = GetCurrentStatisticsVisitors(exCon);
        //    // 取得した値の計算とデータのセット
        //    CalcCurrentAttendanceAndSetValues(currentAttendanceList, currentStatisticsVisitors);
        //}
        ///// <summary>
        ///// 当日来場者データを取得します。
        ///// </summary>
        //private IEnumerable<AttendanceQueryResult> GetCurrentAttendance(ExtendDailyReportPrintCondition exCon)
        //{
        //    StringBuilder query = new StringBuilder();
        //    query
        //        .AppendLine("SELECT")
        //        .AppendLine("    [Player].[OfficeCD],")
        //        .AppendLine("    [Player].[BusinessDate],")
        //        .AppendLine("    CASE [TMa_AttendanceType].[AttendanceClsCD]")
        //        .AppendLine("        WHEN 1 THEN 1 --一般来場者の呼名")
        //        .AppendLine("        WHEN 2 THEN 2 --高齢者の呼名")
        //        .AppendLine("        WHEN 3 THEN 2 --身障者の呼名")
        //        .AppendLine("        WHEN 4 THEN 4 --年次会員の呼名")
        //        .AppendLine("    END AS [AttendanceClsCD],")
        //        .AppendLine("    [TMa_PrivilegeType].[PrivilegeClsCD]")
        //        .AppendLine("FROM")
        //        .AppendLine("    [TMa_Fee]")
        //        .AppendLine("    LEFT OUTER JOIN [TMa_AttendanceType] ON [TMa_Fee].[OfficeCD] = [TMa_AttendanceType].[OfficeCD]")
        //        .AppendLine("    AND [TMa_Fee].[AttendanceTypeCD] = [TMa_AttendanceType].[AttendanceTypeCD]")
        //        .AppendLine("    LEFT OUTER JOIN (")
        //        .AppendLine("        SELECT")
        //        .AppendLine("            *")
        //        .AppendLine("        FROM")
        //        .AppendLine("            [TRe_ReservationPlayer]")
        //        .AppendLine("        WHERE")
        //        .AppendLine("            [OfficeCD] = @officeCD")
        //        .AppendLine("            AND [BusinessDate] = @businessDate")
        //        .AppendLine("    ) AS [Player] ON [TMa_Fee].[OfficeCD] = [Player].[OfficeCD]")
        //        .AppendLine("    AND [TMa_Fee].[FeeCD] = [Player].[FeeCD]")
        //        .AppendLine("    LEFT OUTER JOIN [TMa_PrivilegeType] ON [Player].[OfficeCD] = [TMa_PrivilegeType].[OfficeCD]")
        //        .AppendLine("    AND [Player].[FeePrivilegeTypeCD] = [TMa_PrivilegeType].[PrivilegeTypeCD]")
        //        .AppendLine("WHERE")
        //        .AppendLine("    [TMa_PrivilegeType].[PrivilegeClsCD] <> " + PrivilegeClsCD.NonPlay);

        //    return _DapperAction.GetDataListByQuery<AttendanceQueryResult>(
        //         ConnectionTypes.Data,
        //         query.ToString(),
        //         new
        //         {
        //             officeCD = exCon.OfficeCD,
        //             businessDate = exCon.BusinessDate
        //         }
        //    );
        //}
        ///// <summary>
        ///// 当日統計入場者データを取得します。
        ///// </summary>
        ///// <param name="exCon"></param>
        ///// <returns></returns>
        //private IEnumerable<StatisticsVisitorsQueryResult> GetCurrentStatisticsVisitors(ExtendDailyReportPrintCondition exCon)
        //{
        //    return GetStatisticsVisitors(exCon.OfficeCD, exCon.BusinessDate);
        //}
        ///// <summary>
        ///// 来場者データの計算と値をセットします。
        ///// </summary>
        ///// <param name="attendanceList"></param>
        ///// <param name="statisticsVisitorsList"></param>
        //private void CalcCurrentAttendanceAndSetValues(
        //    IEnumerable<AttendanceQueryResult> attendanceList,
        //    IEnumerable<StatisticsVisitorsQueryResult> statisticsVisitorsList
        //)
        //{
        //    // 1段目
        //    var cell00 = GetPrivilegeCount(attendanceList, AttendanceClsCD.GeneralVisitor, PrivilegeClsCD.Member)
        //        + GetPrivilegeSummary(statisticsVisitorsList, AttendanceClsCD.GeneralVisitor, PrivilegeClsCD.Member);
        //    var cell01 = GetPrivilegeCount(attendanceList, AttendanceClsCD.GeneralVisitor, PrivilegeClsCD.AnnualMember)
        //        + GetPrivilegeSummary(statisticsVisitorsList, AttendanceClsCD.GeneralVisitor, PrivilegeClsCD.AnnualMember);
        //    var cell02 = GetPrivilegeCount(attendanceList, AttendanceClsCD.GeneralVisitor, PrivilegeClsCD.Visitor)
        //        + GetPrivilegeSummary(statisticsVisitorsList, AttendanceClsCD.GeneralVisitor, PrivilegeClsCD.Visitor);
        //    var cell03 = cell00 + cell01 + cell02;
        //    // 2段目
        //    var cell10 = GetPrivilegeCount(attendanceList, AttendanceClsCD.Elderly, PrivilegeClsCD.Member)
        //        + GetPrivilegeSummary(statisticsVisitorsList, AttendanceClsCD.Elderly, PrivilegeClsCD.Member);
        //    var cell11 = GetPrivilegeCount(attendanceList, AttendanceClsCD.Elderly, PrivilegeClsCD.AnnualMember)
        //        + GetPrivilegeSummary(statisticsVisitorsList, AttendanceClsCD.Elderly, PrivilegeClsCD.AnnualMember);
        //    var cell12 = GetPrivilegeCount(attendanceList, AttendanceClsCD.Elderly, PrivilegeClsCD.Visitor)
        //        + GetPrivilegeSummary(statisticsVisitorsList, AttendanceClsCD.Elderly, PrivilegeClsCD.Visitor);
        //    var cell13 = cell10 + cell11 + cell12;
        //    // 3段目
        //    var cell20 = GetPrivilegeCount(attendanceList, AttendanceClsCD.AnnualMember, PrivilegeClsCD.Member)
        //        + GetPrivilegeSummary(statisticsVisitorsList, AttendanceClsCD.AnnualMember, PrivilegeClsCD.Member);
        //    var cell21 = GetPrivilegeCount(attendanceList, AttendanceClsCD.AnnualMember, PrivilegeClsCD.AnnualMember)
        //        + GetPrivilegeSummary(statisticsVisitorsList, AttendanceClsCD.AnnualMember, PrivilegeClsCD.AnnualMember);
        //    var cell22 = GetPrivilegeCount(attendanceList, AttendanceClsCD.AnnualMember, PrivilegeClsCD.Visitor)
        //        + GetPrivilegeSummary(statisticsVisitorsList, AttendanceClsCD.AnnualMember, PrivilegeClsCD.Visitor);
        //    var cell23 = cell20 + cell21 + cell22;
        //    // 縦の合計
        //    var cell30 = cell00 + cell10 + cell20;
        //    var cell31 = cell01 + cell11 + cell21;
        //    var cell32 = cell02 + cell12 + cell22;
        //    // 最終合計
        //    var cell33 = cell03 + cell13 + cell23 + cell30 + cell31 + cell32;

        //    // データのセット
        //    PrintData[0].AttendanceCell0000 = cell00;
        //    PrintData[0].AttendanceCell0001 = cell01;
        //    PrintData[0].AttendanceCell0002 = cell02;
        //    PrintData[0].AttendanceCell0003 = cell03;

        //    PrintData[0].AttendanceCell0100 = cell10;
        //    PrintData[0].AttendanceCell0101 = cell11;
        //    PrintData[0].AttendanceCell0102 = cell12;
        //    PrintData[0].AttendanceCell0103 = cell13;

        //    PrintData[0].AttendanceCell0200 = cell20;
        //    PrintData[0].AttendanceCell0201 = cell21;
        //    PrintData[0].AttendanceCell0202 = cell22;
        //    PrintData[0].AttendanceCell0203 = cell23;

        //    PrintData[0].AttendanceCell0300 = cell30;
        //    PrintData[0].AttendanceCell0301 = cell31;
        //    PrintData[0].AttendanceCell0302 = cell32;

        //    PrintData[0].AttendanceCell0303 = cell33;
        //}
        #endregion

        #region 本月来場者
        ///// <summary>
        ///// 本月来場者データを生成します。
        ///// </summary>
        //private void CreateMonthAttendance(ExtendDailyReportPrintCondition exCon)
        //{
        //    // 値の取得
        //    var monthStatisticsVisitors = GetMonthStatisticsVisitors(exCon);
        //    // 取得した値の計算とデータのセット
        //    CalcMonthAttendanceAndSetValues(monthStatisticsVisitors);
        //}
        ///// <summary>
        ///// 本月統計入場者データを取得します。
        ///// </summary>
        ///// <param name="exCon"></param>
        ///// <returns></returns>
        //private IEnumerable<StatisticsVisitorsQueryResult> GetMonthStatisticsVisitors(ExtendDailyReportPrintCondition exCon)
        //{
        //    return GetStatisticsVisitors(exCon.OfficeCD, exCon.MonthFrom, exCon.MonthTo);
        //}
        ///// <summary>
        ///// 本月来場者データの計算と値をセットします。
        ///// </summary>
        ///// <param name="statisticsVisitorsList"></param>
        //private void CalcMonthAttendanceAndSetValues(IEnumerable<StatisticsVisitorsQueryResult> statisticsVisitorsList)
        //{
        //    // 1段目
        //    var cell00 = PrintData[0].AttendanceCell0000
        //        + GetPrivilegeSummary(statisticsVisitorsList, AttendanceClsCD.GeneralVisitor, PrivilegeClsCD.Member);
        //    var cell01 = PrintData[0].AttendanceCell0001
        //        + GetPrivilegeSummary(statisticsVisitorsList, AttendanceClsCD.GeneralVisitor, PrivilegeClsCD.AnnualMember);
        //    var cell02 = PrintData[0].AttendanceCell0002
        //        + GetPrivilegeSummary(statisticsVisitorsList, AttendanceClsCD.GeneralVisitor, PrivilegeClsCD.Visitor);
        //    var cell03 = cell00 + cell01 + cell02;
        //    // 2段目
        //    var cell10 = PrintData[0].AttendanceCell0100
        //        + GetPrivilegeSummary(statisticsVisitorsList, AttendanceClsCD.Elderly, PrivilegeClsCD.Member);
        //    var cell11 = PrintData[0].AttendanceCell0101
        //        + GetPrivilegeSummary(statisticsVisitorsList, AttendanceClsCD.Elderly, PrivilegeClsCD.AnnualMember);
        //    var cell12 = PrintData[0].AttendanceCell0102
        //        + GetPrivilegeSummary(statisticsVisitorsList, AttendanceClsCD.Elderly, PrivilegeClsCD.Visitor);
        //    var cell13 = cell10 + cell11 + cell12;
        //    // 3段目
        //    var cell20 = PrintData[0].AttendanceCell0200
        //        + GetPrivilegeSummary(statisticsVisitorsList, AttendanceClsCD.AnnualMember, PrivilegeClsCD.Member);
        //    var cell21 = PrintData[0].AttendanceCell0201
        //        + GetPrivilegeSummary(statisticsVisitorsList, AttendanceClsCD.AnnualMember, PrivilegeClsCD.AnnualMember);
        //    var cell22 = PrintData[0].AttendanceCell0202
        //        + GetPrivilegeSummary(statisticsVisitorsList, AttendanceClsCD.AnnualMember, PrivilegeClsCD.Visitor);
        //    var cell23 = cell20 + cell21 + cell22;
        //    // 縦の合計
        //    var cell30 = cell00 + cell10 + cell20;
        //    var cell31 = cell01 + cell11 + cell21;
        //    var cell32 = cell02 + cell12 + cell22;
        //    // 最終合計
        //    var cell33 = cell03 + cell13 + cell23 + cell30 + cell31 + cell32;

        //    // データのセット
        //    PrintData[0].AttendanceCell0004 = cell00;
        //    PrintData[0].AttendanceCell0005 = cell01;
        //    PrintData[0].AttendanceCell0006 = cell02;
        //    PrintData[0].AttendanceCell0007 = cell03;

        //    PrintData[0].AttendanceCell0104 = cell10;
        //    PrintData[0].AttendanceCell0105 = cell11;
        //    PrintData[0].AttendanceCell0106 = cell12;
        //    PrintData[0].AttendanceCell0107 = cell13;

        //    PrintData[0].AttendanceCell0204 = cell20;
        //    PrintData[0].AttendanceCell0205 = cell21;
        //    PrintData[0].AttendanceCell0206 = cell22;
        //    PrintData[0].AttendanceCell0207 = cell23;

        //    PrintData[0].AttendanceCell0304 = cell30;
        //    PrintData[0].AttendanceCell0305 = cell31;
        //    PrintData[0].AttendanceCell0306 = cell32;

        //    PrintData[0].AttendanceCell0307 = cell33;
        //}
        #endregion

        #region 本年来場者
        ///// <summary>
        ///// 本年来場者データを生成します。
        ///// </summary>
        //private void CreateYearAttendance(ExtendDailyReportPrintCondition exCon)
        //{
        //    // 値の取得
        //    var yearStatisticsVisitors = GetYearStatisticsVisitors(exCon);
        //    // 取得した値の計算とデータのセット
        //    CalcYearAttendanceAndSetValues(yearStatisticsVisitors);
        //}
        ///// <summary>
        ///// 本年統計入場者データを取得します。
        ///// </summary>
        ///// <param name="exCon"></param>
        ///// <returns></returns>
        //private IEnumerable<StatisticsVisitorsQueryResult> GetYearStatisticsVisitors(ExtendDailyReportPrintCondition exCon)
        //{
        //    return GetStatisticsVisitors(exCon.OfficeCD, exCon.YearFrom, exCon.YearTo);
        //}
        ///// <summary>
        ///// 本年来場者データの計算と値をセットします。
        ///// </summary>
        ///// <param name="statisticsVisitorsList"></param>
        //private void CalcYearAttendanceAndSetValues(IEnumerable<StatisticsVisitorsQueryResult> statisticsVisitorsList)
        //{
        //    // 1段目
        //    var cell00 = PrintData[0].AttendanceCell0000
        //        + GetPrivilegeSummary(statisticsVisitorsList, AttendanceClsCD.GeneralVisitor, PrivilegeClsCD.Member);
        //    var cell01 = PrintData[0].AttendanceCell0001
        //        + GetPrivilegeSummary(statisticsVisitorsList, AttendanceClsCD.GeneralVisitor, PrivilegeClsCD.AnnualMember);
        //    var cell02 = PrintData[0].AttendanceCell0002
        //        + GetPrivilegeSummary(statisticsVisitorsList, AttendanceClsCD.GeneralVisitor, PrivilegeClsCD.Visitor);
        //    var cell03 = cell00 + cell01 + cell02;
        //    // 2段目
        //    var cell10 = PrintData[0].AttendanceCell0100
        //        + GetPrivilegeSummary(statisticsVisitorsList, AttendanceClsCD.Elderly, PrivilegeClsCD.Member);
        //    var cell11 = PrintData[0].AttendanceCell0101
        //        + GetPrivilegeSummary(statisticsVisitorsList, AttendanceClsCD.Elderly, PrivilegeClsCD.AnnualMember);
        //    var cell12 = PrintData[0].AttendanceCell0102
        //        + GetPrivilegeSummary(statisticsVisitorsList, AttendanceClsCD.Elderly, PrivilegeClsCD.Visitor);
        //    var cell13 = cell10 + cell11 + cell12;
        //    // 3段目
        //    var cell20 = PrintData[0].AttendanceCell0200
        //        + GetPrivilegeSummary(statisticsVisitorsList, AttendanceClsCD.AnnualMember, PrivilegeClsCD.Member);
        //    var cell21 = PrintData[0].AttendanceCell0201
        //        + GetPrivilegeSummary(statisticsVisitorsList, AttendanceClsCD.AnnualMember, PrivilegeClsCD.AnnualMember);
        //    var cell22 = PrintData[0].AttendanceCell0202
        //        + GetPrivilegeSummary(statisticsVisitorsList, AttendanceClsCD.AnnualMember, PrivilegeClsCD.Visitor);
        //    var cell23 = cell20 + cell21 + cell22;
        //    // 縦の合計
        //    var cell30 = cell00 + cell10 + cell20;
        //    var cell31 = cell01 + cell11 + cell21;
        //    var cell32 = cell02 + cell12 + cell22;
        //    // 最終合計
        //    var cell33 = cell03 + cell13 + cell23 + cell30 + cell31 + cell32;

        //    // データのセット
        //    PrintData[0].AttendanceCell0008 = cell00;
        //    PrintData[0].AttendanceCell0009 = cell01;
        //    PrintData[0].AttendanceCell0010 = cell02;
        //    PrintData[0].AttendanceCell0011 = cell03;

        //    PrintData[0].AttendanceCell0108 = cell10;
        //    PrintData[0].AttendanceCell0109 = cell11;
        //    PrintData[0].AttendanceCell0110 = cell12;
        //    PrintData[0].AttendanceCell0111 = cell13;

        //    PrintData[0].AttendanceCell0208 = cell20;
        //    PrintData[0].AttendanceCell0209 = cell21;
        //    PrintData[0].AttendanceCell0210 = cell22;
        //    PrintData[0].AttendanceCell0211 = cell23;

        //    PrintData[0].AttendanceCell0308 = cell30;
        //    PrintData[0].AttendanceCell0309 = cell31;
        //    PrintData[0].AttendanceCell0310 = cell32;

        //    PrintData[0].AttendanceCell0311 = cell33;
        //}
        #endregion

        #region 前年来場者
        ///// <summary>
        ///// 前年来場者データを生成します。
        ///// </summary>
        //private void CreateLastYearAttendance(ExtendDailyReportPrintCondition exCon)
        //{
        //    // 値の取得
        //    var currentStatisticsVisitors = GetLastYearStatisticsVisitors(exCon);
        //    // 取得した値の計算とデータのセット
        //    CalcLastYearAttendanceAndSetValues(currentStatisticsVisitors);
        //}
        ///// <summary>
        ///// 前年統計入場者データを取得します。
        ///// </summary>
        ///// <param name="exCon"></param>
        ///// <returns></returns>
        //private IEnumerable<StatisticsVisitorsQueryResult> GetLastYearStatisticsVisitors(ExtendDailyReportPrintCondition exCon)
        //{
        //    return GetStatisticsVisitors(exCon.OfficeCD, exCon.LastYearFrom, exCon.LastYearTo);
        //}
        ///// <summary>
        ///// 前年来場者データの計算と値をセットします。
        ///// </summary>
        ///// <param name="statisticsVisitorsList"></param>
        //private void CalcLastYearAttendanceAndSetValues(IEnumerable<StatisticsVisitorsQueryResult> statisticsVisitorsList)
        //{
        //    // 1段目
        //    var cell00 = GetPrivilegeSummary(statisticsVisitorsList, AttendanceClsCD.GeneralVisitor, PrivilegeClsCD.Member);
        //    var cell01 = GetPrivilegeSummary(statisticsVisitorsList, AttendanceClsCD.GeneralVisitor, PrivilegeClsCD.AnnualMember);
        //    var cell02 = GetPrivilegeSummary(statisticsVisitorsList, AttendanceClsCD.GeneralVisitor, PrivilegeClsCD.Visitor);
        //    var cell03 = cell00 + cell01 + cell02;
        //    // 2段目
        //    var cell10 = GetPrivilegeSummary(statisticsVisitorsList, AttendanceClsCD.Elderly, PrivilegeClsCD.Member);
        //    var cell11 = GetPrivilegeSummary(statisticsVisitorsList, AttendanceClsCD.Elderly, PrivilegeClsCD.AnnualMember);
        //    var cell12 = GetPrivilegeSummary(statisticsVisitorsList, AttendanceClsCD.Elderly, PrivilegeClsCD.Visitor);
        //    var cell13 = cell10 + cell11 + cell12;
        //    // 3段目
        //    var cell20 = GetPrivilegeSummary(statisticsVisitorsList, AttendanceClsCD.AnnualMember, PrivilegeClsCD.Member);
        //    var cell21 = GetPrivilegeSummary(statisticsVisitorsList, AttendanceClsCD.AnnualMember, PrivilegeClsCD.AnnualMember);
        //    var cell22 = GetPrivilegeSummary(statisticsVisitorsList, AttendanceClsCD.AnnualMember, PrivilegeClsCD.Visitor);
        //    var cell23 = cell20 + cell21 + cell22;
        //    // 4段目(合計)
        //    var cell33 = cell03 + cell13 + cell23;

        //    // データのセット
        //    // 合計
        //    PrintData[0].AttendanceCell0012 = cell03;
        //    PrintData[0].AttendanceCell0112 = cell13;
        //    PrintData[0].AttendanceCell0212 = cell23;
        //    PrintData[0].AttendanceCell0312 = cell33;
        //    // 率
        //    PrintData[0].AttendanceCell0013 = PrintData[0].AttendanceCell0012 != 0
        //        ? (PrintData[0].AttendanceCell0011 / PrintData[0].AttendanceCell0012) * 100
        //        : 0;
        //    PrintData[0].AttendanceCell0113 = PrintData[0].AttendanceCell0112 != 0
        //        ? (PrintData[0].AttendanceCell0111 / PrintData[0].AttendanceCell0112) * 100
        //        : 0;
        //    PrintData[0].AttendanceCell0213 = PrintData[0].AttendanceCell0212 != 0
        //        ? (PrintData[0].AttendanceCell0211 / PrintData[0].AttendanceCell0212) * 100
        //        : 0;
        //    PrintData[0].AttendanceCell0313 = PrintData[0].AttendanceCell0312 != 0
        //        ? (PrintData[0].AttendanceCell0311 / PrintData[0].AttendanceCell0312) * 100
        //        : 0;
        //}
        #endregion

        #region 当日利用税来場者
        ///// <summary>
        ///// 当日利用税来場者データを生成します。
        ///// </summary>
        //private void CreateCurrentUseTaxAttendance(ExtendDailyReportPrintCondition exCon)
        //{
        //    // 値の取得
        //    var currentAttendanceList = GetCurrentAttendance(exCon);
        //    var currentStatisticsVisitors = GetCurrentStatisticsVisitors(exCon);
        //    // 取得した値の計算とデータのセット
        //    CalcCurrentUseTaxAttendanceAndSetValues(currentAttendanceList, currentStatisticsVisitors);
        //}
        ///// <summary>
        ///// 当日利用税来場者データの計算と計算した値をセットします。
        ///// </summary>
        ///// <param name="attendanceList"></param>
        ///// <param name="statisticsVisitorsList"></param>
        //private void CalcCurrentUseTaxAttendanceAndSetValues(
        //    IEnumerable<AttendanceQueryResult> attendanceList,
        //    IEnumerable<StatisticsVisitorsQueryResult> statisticsVisitorsList
        //)
        //{
        //    // 1段目
        //    var cell00 = GetUseTaxCount(attendanceList, AttendanceClsCD.GeneralVisitor, 0)
        //        + GetUseTaxSummary(statisticsVisitorsList, AttendanceClsCD.GeneralVisitor, 0);
        //    var cell01 = GetUseTaxCount(attendanceList, AttendanceClsCD.GeneralVisitor, 1)
        //        + GetUseTaxSummary(statisticsVisitorsList, AttendanceClsCD.GeneralVisitor, 1);
        //    var cell02 = GetUseTaxCount(attendanceList, AttendanceClsCD.GeneralVisitor, 2)
        //        + GetUseTaxSummary(statisticsVisitorsList, AttendanceClsCD.GeneralVisitor, 2);
        //    var cell03 = cell00 + cell01 + cell02;
        //    // 2段目
        //    var cell10 = GetUseTaxCount(attendanceList, AttendanceClsCD.Elderly, 0)
        //        + GetUseTaxSummary(statisticsVisitorsList, AttendanceClsCD.Elderly, 0);
        //    var cell11 = GetUseTaxCount(attendanceList, AttendanceClsCD.Elderly, 1)
        //        + GetUseTaxSummary(statisticsVisitorsList, AttendanceClsCD.Elderly, 1);
        //    var cell12 = GetUseTaxCount(attendanceList, AttendanceClsCD.Elderly, 2)
        //        + GetUseTaxSummary(statisticsVisitorsList, AttendanceClsCD.Elderly, 2);
        //    var cell13 = cell10 + cell11 + cell12;
        //    // 3段目
        //    var cell20 = GetUseTaxCount(attendanceList, AttendanceClsCD.AnnualMember, 0)
        //        + GetUseTaxSummary(statisticsVisitorsList, AttendanceClsCD.AnnualMember, 0);
        //    var cell21 = GetUseTaxCount(attendanceList, AttendanceClsCD.AnnualMember, 1)
        //        + GetUseTaxSummary(statisticsVisitorsList, AttendanceClsCD.AnnualMember, 1);
        //    var cell22 = GetUseTaxCount(attendanceList, AttendanceClsCD.AnnualMember, 2)
        //        + GetUseTaxSummary(statisticsVisitorsList, AttendanceClsCD.AnnualMember, 2);
        //    var cell23 = cell20 + cell21 + cell22;
        //    // 縦の合計
        //    var cell30 = cell00 + cell10 + cell20;
        //    var cell31 = cell01 + cell11 + cell21;
        //    var cell32 = cell02 + cell12 + cell22;
        //    // 最終合計
        //    var cell33 = cell03 + cell13 + cell23 + cell30 + cell31 + cell32;

        //    // データのセット
        //    PrintData[0].UseTaxAttendanceCell0000 = cell00;
        //    PrintData[0].UseTaxAttendanceCell0001 = cell01;
        //    PrintData[0].UseTaxAttendanceCell0002 = cell02;
        //    PrintData[0].UseTaxAttendanceCell0003 = cell03;

        //    PrintData[0].UseTaxAttendanceCell0100 = cell10;
        //    PrintData[0].UseTaxAttendanceCell0101 = cell11;
        //    PrintData[0].UseTaxAttendanceCell0102 = cell12;
        //    PrintData[0].UseTaxAttendanceCell0103 = cell13;

        //    PrintData[0].UseTaxAttendanceCell0200 = cell20;
        //    PrintData[0].UseTaxAttendanceCell0201 = cell21;
        //    PrintData[0].UseTaxAttendanceCell0202 = cell22;
        //    PrintData[0].UseTaxAttendanceCell0203 = cell23;

        //    PrintData[0].UseTaxAttendanceCell0300 = cell30;
        //    PrintData[0].UseTaxAttendanceCell0301 = cell31;
        //    PrintData[0].UseTaxAttendanceCell0302 = cell32;

        //    PrintData[0].UseTaxAttendanceCell0303 = cell33;
        //}
        #endregion

        #region 本月利用税来場者
        ///// <summary>
        ///// 過去利用税来場者データを生成します。
        ///// </summary>
        //private void CreateMonthUseTaxAttendance(ExtendDailyReportPrintCondition exCon)
        //{
        //    // 値の取得
        //    var monthStatisticsVisitors = GetMonthStatisticsVisitors(exCon);
        //    // 取得した値の計算とデータのセット
        //    CalcMonthUseTaxAttendanceAndSetValues(monthStatisticsVisitors);
        //}
        ///// <summary>
        ///// 本月来場者データの計算と値をセットします。
        ///// </summary>
        ///// <param name="statisticsVisitorsList"></param>
        //private void CalcMonthUseTaxAttendanceAndSetValues(IEnumerable<StatisticsVisitorsQueryResult> statisticsVisitorsList)
        //{
        //    // 1段目
        //    var cell00 = PrintData[0].UseTaxAttendanceCell0000
        //        + GetUseTaxSummary(statisticsVisitorsList, AttendanceClsCD.GeneralVisitor, 0);
        //    var cell01 = PrintData[0].UseTaxAttendanceCell0001
        //        + GetUseTaxSummary(statisticsVisitorsList, AttendanceClsCD.GeneralVisitor, 1);
        //    var cell02 = PrintData[0].UseTaxAttendanceCell0002
        //        + GetUseTaxSummary(statisticsVisitorsList, AttendanceClsCD.GeneralVisitor, 2);
        //    var cell03 = cell00 + cell01 + cell02;
        //    // 2段目
        //    var cell10 = PrintData[0].UseTaxAttendanceCell0100
        //        + GetUseTaxSummary(statisticsVisitorsList, AttendanceClsCD.Elderly, 0);
        //    var cell11 = PrintData[0].UseTaxAttendanceCell0101
        //        + GetUseTaxSummary(statisticsVisitorsList, AttendanceClsCD.Elderly, 1);
        //    var cell12 = PrintData[0].UseTaxAttendanceCell0102
        //        + GetUseTaxSummary(statisticsVisitorsList, AttendanceClsCD.Elderly, 2);
        //    var cell13 = cell10 + cell11 + cell12;
        //    // 3段目
        //    var cell20 = PrintData[0].UseTaxAttendanceCell0200
        //        + GetUseTaxSummary(statisticsVisitorsList, AttendanceClsCD.AnnualMember, 0);
        //    var cell21 = PrintData[0].UseTaxAttendanceCell0201
        //        + GetUseTaxSummary(statisticsVisitorsList, AttendanceClsCD.AnnualMember, 1);
        //    var cell22 = PrintData[0].UseTaxAttendanceCell0202
        //        + GetUseTaxSummary(statisticsVisitorsList, AttendanceClsCD.AnnualMember, 2);
        //    var cell23 = cell20 + cell21 + cell22;
        //    // 縦の合計
        //    var cell30 = cell00 + cell10 + cell20;
        //    var cell31 = cell01 + cell11 + cell21;
        //    var cell32 = cell02 + cell12 + cell22;
        //    // 最終合計
        //    var cell33 = cell03 + cell13 + cell23 + cell30 + cell31 + cell32;

        //    // データのセット
        //    PrintData[0].UseTaxAttendanceCell0004 = cell00;
        //    PrintData[0].UseTaxAttendanceCell0005 = cell01;
        //    PrintData[0].UseTaxAttendanceCell0006 = cell02;
        //    PrintData[0].UseTaxAttendanceCell0007 = cell03;

        //    PrintData[0].UseTaxAttendanceCell0104 = cell10;
        //    PrintData[0].UseTaxAttendanceCell0105 = cell11;
        //    PrintData[0].UseTaxAttendanceCell0106 = cell12;
        //    PrintData[0].UseTaxAttendanceCell0107 = cell13;

        //    PrintData[0].UseTaxAttendanceCell0204 = cell20;
        //    PrintData[0].UseTaxAttendanceCell0205 = cell21;
        //    PrintData[0].UseTaxAttendanceCell0206 = cell22;
        //    PrintData[0].UseTaxAttendanceCell0207 = cell23;

        //    PrintData[0].UseTaxAttendanceCell0304 = cell30;
        //    PrintData[0].UseTaxAttendanceCell0305 = cell31;
        //    PrintData[0].UseTaxAttendanceCell0306 = cell32;

        //    PrintData[0].UseTaxAttendanceCell0307 = cell33;
        //}
        #endregion

        #region 本年利用税来場者
        ///// <summary>
        ///// 過去利用税来場者データを生成します。
        ///// </summary>
        //private void CreateYearUseTaxAttendance(ExtendDailyReportPrintCondition exCon)
        //{
        //    // 値の取得
        //    var yearStatisticsVisitors = GetYearStatisticsVisitors(exCon);
        //    // 取得した値の計算とデータのセット
        //    CalcYearUseTaxAttendanceAndSetValues(yearStatisticsVisitors);
        //}
        ///// <summary>
        ///// 本年利用税来場者データの計算と値をセットします。
        ///// </summary>
        ///// <param name="statisticsVisitorsList"></param>
        //private void CalcYearUseTaxAttendanceAndSetValues(IEnumerable<StatisticsVisitorsQueryResult> statisticsVisitorsList)
        //{
        //    // 1段目
        //    var cell00 = PrintData[0].UseTaxAttendanceCell0000
        //        + GetUseTaxSummary(statisticsVisitorsList, AttendanceClsCD.GeneralVisitor, 0);
        //    var cell01 = PrintData[0].UseTaxAttendanceCell0001
        //        + GetUseTaxSummary(statisticsVisitorsList, AttendanceClsCD.GeneralVisitor, 1);
        //    var cell02 = PrintData[0].UseTaxAttendanceCell0002
        //        + GetUseTaxSummary(statisticsVisitorsList, AttendanceClsCD.GeneralVisitor, 2);
        //    var cell03 = cell00 + cell01 + cell02;
        //    // 2段目
        //    var cell10 = PrintData[0].UseTaxAttendanceCell0100
        //        + GetUseTaxSummary(statisticsVisitorsList, AttendanceClsCD.Elderly, 0);
        //    var cell11 = PrintData[0].UseTaxAttendanceCell0101
        //        + GetUseTaxSummary(statisticsVisitorsList, AttendanceClsCD.Elderly, 1);
        //    var cell12 = PrintData[0].UseTaxAttendanceCell0102
        //        + GetUseTaxSummary(statisticsVisitorsList, AttendanceClsCD.Elderly, 2);
        //    var cell13 = cell10 + cell11 + cell12;
        //    // 3段目
        //    var cell20 = PrintData[0].UseTaxAttendanceCell0200
        //        + GetUseTaxSummary(statisticsVisitorsList, AttendanceClsCD.AnnualMember, 0);
        //    var cell21 = PrintData[0].UseTaxAttendanceCell0201
        //        + GetUseTaxSummary(statisticsVisitorsList, AttendanceClsCD.AnnualMember, 1);
        //    var cell22 = PrintData[0].UseTaxAttendanceCell0202
        //        + GetUseTaxSummary(statisticsVisitorsList, AttendanceClsCD.AnnualMember, 2);
        //    var cell23 = cell20 + cell21 + cell22;
        //    // 縦の合計
        //    var cell30 = cell00 + cell10 + cell20;
        //    var cell31 = cell01 + cell11 + cell21;
        //    var cell32 = cell02 + cell12 + cell22;
        //    // 最終合計
        //    var cell33 = cell03 + cell13 + cell23 + cell30 + cell31 + cell32;

        //    // データのセット
        //    PrintData[0].UseTaxAttendanceCell0008 = cell00;
        //    PrintData[0].UseTaxAttendanceCell0009 = cell01;
        //    PrintData[0].UseTaxAttendanceCell0010 = cell02;
        //    PrintData[0].UseTaxAttendanceCell0011 = cell03;

        //    PrintData[0].UseTaxAttendanceCell0108 = cell10;
        //    PrintData[0].UseTaxAttendanceCell0109 = cell11;
        //    PrintData[0].UseTaxAttendanceCell0110 = cell12;
        //    PrintData[0].UseTaxAttendanceCell0111 = cell13;

        //    PrintData[0].UseTaxAttendanceCell0208 = cell20;
        //    PrintData[0].UseTaxAttendanceCell0209 = cell21;
        //    PrintData[0].UseTaxAttendanceCell0210 = cell22;
        //    PrintData[0].UseTaxAttendanceCell0211 = cell23;

        //    PrintData[0].UseTaxAttendanceCell0308 = cell30;
        //    PrintData[0].UseTaxAttendanceCell0309 = cell31;
        //    PrintData[0].UseTaxAttendanceCell0310 = cell32;

        //    PrintData[0].UseTaxAttendanceCell0311 = cell33;
        //}
        #endregion

        #region 前年利用税来場者
        ///// <summary>
        ///// 前年利用税来場者データを生成します。
        ///// </summary>
        //private void CreateLastYearUseTaxAttendance(ExtendDailyReportPrintCondition exCon)
        //{
        //    // 値の取得
        //    var currentStatisticsVisitors = GetLastYearStatisticsVisitors(exCon);
        //    // 取得した値の計算とデータのセット
        //    CalcLastYearAttendanceAndSetValues(currentStatisticsVisitors);
        //}
        ///// <summary>
        ///// 前年来場者データの計算と値をセットします。
        ///// </summary>
        ///// <param name="statisticsVisitorsList"></param>
        //private void CalcLastYearUseTaxAttendanceAndSetValues(IEnumerable<StatisticsVisitorsQueryResult> statisticsVisitorsList)
        //{
        //    // 1段目
        //    var cell00 = GetUseTaxSummary(statisticsVisitorsList, AttendanceClsCD.GeneralVisitor, 0);
        //    var cell01 = GetUseTaxSummary(statisticsVisitorsList, AttendanceClsCD.GeneralVisitor, 1);
        //    var cell02 = GetUseTaxSummary(statisticsVisitorsList, AttendanceClsCD.GeneralVisitor, 2);
        //    var cell03 = cell00 + cell01 + cell02;
        //    // 2段目
        //    var cell10 = GetUseTaxSummary(statisticsVisitorsList, AttendanceClsCD.Elderly, 0);
        //    var cell11 = GetUseTaxSummary(statisticsVisitorsList, AttendanceClsCD.Elderly, 1);
        //    var cell12 = GetUseTaxSummary(statisticsVisitorsList, AttendanceClsCD.Elderly, 2);
        //    var cell13 = cell10 + cell11 + cell12;
        //    // 3段目
        //    var cell20 = GetUseTaxSummary(statisticsVisitorsList, AttendanceClsCD.AnnualMember, 0);
        //    var cell21 = GetUseTaxSummary(statisticsVisitorsList, AttendanceClsCD.AnnualMember, 1);
        //    var cell22 = GetUseTaxSummary(statisticsVisitorsList, AttendanceClsCD.AnnualMember, 2);
        //    var cell23 = cell20 + cell21 + cell22;
        //    // 4段目(合計)
        //    var cell33 = cell03 + cell13 + cell23;

        //    // データのセット
        //    // 合計
        //    PrintData[0].UseTaxAttendanceCell0012 = cell03;
        //    PrintData[0].UseTaxAttendanceCell0112 = cell13;
        //    PrintData[0].UseTaxAttendanceCell0212 = cell23;
        //    PrintData[0].UseTaxAttendanceCell0312 = cell33;
        //    // 率
        //    PrintData[0].UseTaxAttendanceCell0013 = PrintData[0].UseTaxAttendanceCell0012 != 0
        //        ? (PrintData[0].UseTaxAttendanceCell0011 / PrintData[0].UseTaxAttendanceCell0012) * 100
        //        : 0;
        //    PrintData[0].UseTaxAttendanceCell0113 = PrintData[0].UseTaxAttendanceCell0112 != 0
        //        ? (PrintData[0].UseTaxAttendanceCell0111 / PrintData[0].UseTaxAttendanceCell0112) * 100
        //        : 0;
        //    PrintData[0].UseTaxAttendanceCell0213 = PrintData[0].UseTaxAttendanceCell0212 != 0
        //        ? (PrintData[0].UseTaxAttendanceCell0211 / PrintData[0].UseTaxAttendanceCell0212) * 100
        //        : 0;
        //    PrintData[0].UseTaxAttendanceCell0313 = PrintData[0].UseTaxAttendanceCell0312 != 0
        //        ? (PrintData[0].UseTaxAttendanceCell0311 / PrintData[0].UseTaxAttendanceCell0312) * 100
        //        : 0;
        //}
        #endregion

        #region 来場者ヘッダー
        ///// <summary>
        ///// 来場者資格欄のヘッダー情報を生成します。
        ///// </summary>
        //private void CreateAttendanceHeader(ExtendDailyReportPrintCondition exCon)
        //{
        //    //// 資格分類名(左のヘッダー)
        //    //var privilegeClsList = _TMa_PrivilegeClsModel.GetList(exCon.OfficeCD);
        //    //PrintData[0].PrivilegeClsName1 = privilegeClsList.Where(w => w.PrivilegeClsCD == PrivilegeClsCD.Member).FirstOrDefault()?.PrivilegeClsName;
        //    //PrintData[0].PrivilegeClsName2 = privilegeClsList.Where(w => w.PrivilegeClsCD == PrivilegeClsCD.AnnualMember).FirstOrDefault()?.PrivilegeClsName;
        //    //PrintData[0].PrivilegeClsName3 = privilegeClsList.Where(w => w.PrivilegeClsCD == PrivilegeClsCD.Visitor).FirstOrDefault()?.PrivilegeClsName;
        //    //// 来場分類名(上のヘッダー)
        //    //var attendanceClsList = _TMa_AttendanceClsModel.GetList(exCon.OfficeCD);
        //    //PrintData[0].AttendanceClsName1 = attendanceClsList.Where(w => w.AttendanceClsCD == AttendanceClsCD.GeneralVisitor).FirstOrDefault()?.AttendanceClsName;
        //    //PrintData[0].AttendanceClsName2 = attendanceClsList.Where(w => w.AttendanceClsCD == AttendanceClsCD.Elderly).FirstOrDefault()?.AttendanceClsName;
        //    //PrintData[0].AttendanceClsName3 = attendanceClsList.Where(w => w.AttendanceClsCD == AttendanceClsCD.AnnualMember).FirstOrDefault()?.AttendanceClsName;

        //    // 資格分類名(左のヘッダー)
        //    PrintData[0].PrivilegeClsName1 = _TMa_PrivilegeClsModel.Get(exCon.OfficeCD, PrivilegeClsCD.Member).PrivilegeClsName;
        //    PrintData[0].PrivilegeClsName2 = _TMa_PrivilegeClsModel.Get(exCon.OfficeCD, PrivilegeClsCD.AnnualMember).PrivilegeClsName;
        //    PrintData[0].PrivilegeClsName3 = _TMa_PrivilegeClsModel.Get(exCon.OfficeCD, PrivilegeClsCD.Visitor).PrivilegeClsName;
        //    // 来場分類名(上のヘッダー)
        //    PrintData[0].AttendanceClsName1 = _TMa_AttendanceClsModel.Get(exCon.OfficeCD, AttendanceClsCD.GeneralVisitor).AttendanceClsName;
        //    PrintData[0].AttendanceClsName2 = _TMa_AttendanceClsModel.Get(exCon.OfficeCD, AttendanceClsCD.Elderly).AttendanceClsName;
        //    PrintData[0].AttendanceClsName3 = _TMa_AttendanceClsModel.Get(exCon.OfficeCD, AttendanceClsCD.AnnualMember).AttendanceClsName;
        //}
        #endregion
        #endregion 

        #region フロント現金有高内訳
        ///// <summary>
        ///// フロント現金有高内訳についての情報を生成します。
        ///// </summary>
        ///// <param name="exCon"></param>
        //private void CreateFrontCashDetail(ExtendDailyReportPrintCondition exCon)
        //{
        //    // 売上高現金
        //    var cashPriceTotal = decimal.Zero;
        //    // 内訳1~7
        //    string cashName1, cashName2, cashName3, cashName4, cashName5, cashName6, cashName7 = string.Empty;
        //    decimal cashPrice1, cashPrice2, cashPrice3, cashPrice4, cashPrice5, cashPrice6, cashPrice7 = decimal.Zero;
        //    // フロント現金売高
        //    var frontCashSalesTotal = decimal.Zero;
        //    // 当日釣銭額
        //    var todayChange = decimal.Zero;
        //    // フロント現金有高
        //    var frontCashTotal = decimal.Zero;
        //    // 現金過不足額 = フロント現金有高 - フロント現金売高 - 当日釣銭額
        //    var cashExcessOrDeficiency = decimal.Zero;
        //    // 翌日繰越額
        //    var carryOverAmount = decimal.Zero;
        //    // 口座振込額
        //    var bankTransferAmount = decimal.Zero;

        //    if (exCon.IsCurrentDay)
        //    {
        //        // Todo:メソッド化
        //        // 当日基本情報を取得
        //        var basicItem = _TFr_BasicItemModel.Get(exCon.OfficeCD);
        //        // フロント現金明細一覧を取得
        //        var frontCashDetail = _TFr_FrontCashDetailModel.GetList(exCon.OfficeCD, exCon.BusinessDate);

        //        // 売上高現金
        //        cashPriceTotal = _TFr_AmountByPaymentMethodModel.GetCashSettlement(exCon.OfficeCD, exCon.BusinessDate);
        //        // 内訳1~7
        //        var cash = frontCashDetail.Where(w => w.DailyReportItemCD == 1).FirstOrDefault();
        //        cashName1 = cash?.DailyReportItemName;
        //        cashPrice1 = cash?.Amount ?? decimal.Zero;
        //        cash = frontCashDetail.Where(w => w.DailyReportItemCD == 2).FirstOrDefault();
        //        cashName2 = cash?.DailyReportItemName;
        //        cashPrice2 = cash?.Amount ?? decimal.Zero;
        //        cash = frontCashDetail.Where(w => w.DailyReportItemCD == 3).FirstOrDefault();
        //        cashName3 = cash?.DailyReportItemName;
        //        cashPrice3 = cash?.Amount ?? decimal.Zero;
        //        cash = frontCashDetail.Where(w => w.DailyReportItemCD == 4).FirstOrDefault();
        //        cashName4 = cash?.DailyReportItemName;
        //        cashPrice4 = cash?.Amount ?? decimal.Zero;
        //        cash = frontCashDetail.Where(w => w.DailyReportItemCD == 5).FirstOrDefault();
        //        cashName5 = cash?.DailyReportItemName;
        //        cashPrice5 = cash?.Amount ?? decimal.Zero;
        //        cash = frontCashDetail.Where(w => w.DailyReportItemCD == 6).FirstOrDefault();
        //        cashName6 = cash?.DailyReportItemName;
        //        cashPrice6 = cash?.Amount ?? decimal.Zero;
        //        cash = frontCashDetail.Where(w => w.DailyReportItemCD == 7).FirstOrDefault();
        //        cashName7 = cash?.DailyReportItemName;
        //        cashPrice7 = cash?.Amount ?? decimal.Zero;
        //        // フロント現金売高
        //        frontCashSalesTotal = cashPriceTotal
        //            + _TFr_SlipModel.GetDepartmentCashSales(exCon.OfficeCD, exCon.BusinessDate)
        //            + _TFr_FrontCashDetailModel.GetFrontCash(exCon.OfficeCD, exCon.BusinessDate);
        //        // 当日釣銭額
        //        todayChange = basicItem?.TodayChangeAmount ?? decimal.Zero;
        //        // フロント現金有高
        //        frontCashTotal = basicItem?.FrontCashAmount ?? decimal.Zero;
        //        // 現金過不足額 = フロント現金有高 - フロント現金売高 - 当日釣銭額
        //        cashExcessOrDeficiency = frontCashTotal - frontCashSalesTotal - todayChange;
        //        // 翌日繰越額
        //        carryOverAmount = basicItem?.NextdayChangeAmount ?? decimal.Zero;
        //        // 口座振込額
        //        bankTransferAmount = basicItem?.BankTransferAmount ?? decimal.Zero;
        //    }
        //    else
        //    {
        //        // Todo:メソッド化
        //        // 当日基本情報を取得
        //        var basicItem = _TPa_BasicItemModel.Get(exCon.OfficeCD);
        //        // フロント現金明細一覧を取得
        //        var frontCashDetail = _TPa_FrontCashDetailModel.GetList(exCon.OfficeCD, exCon.BusinessDate);

        //        // 売上高現金
        //        cashPriceTotal = _TPa_AmountByPaymentMethodModel.GetCashSettlement(exCon.OfficeCD, exCon.BusinessDate);
        //        // 内訳1~7
        //        var cash = frontCashDetail.Where(w => w.DailyReportItemCD == 1).FirstOrDefault();
        //        cashName1 = cash?.DailyReportItemName;
        //        cashPrice1 = cash?.Amount ?? decimal.Zero;
        //        cash = frontCashDetail.Where(w => w.DailyReportItemCD == 2).FirstOrDefault();
        //        cashName2 = cash?.DailyReportItemName;
        //        cashPrice2 = cash?.Amount ?? decimal.Zero;
        //        cash = frontCashDetail.Where(w => w.DailyReportItemCD == 3).FirstOrDefault();
        //        cashName3 = cash?.DailyReportItemName;
        //        cashPrice3 = cash?.Amount ?? decimal.Zero;
        //        cash = frontCashDetail.Where(w => w.DailyReportItemCD == 4).FirstOrDefault();
        //        cashName4 = cash?.DailyReportItemName;
        //        cashPrice4 = cash?.Amount ?? decimal.Zero;
        //        cash = frontCashDetail.Where(w => w.DailyReportItemCD == 5).FirstOrDefault();
        //        cashName5 = cash?.DailyReportItemName;
        //        cashPrice5 = cash?.Amount ?? decimal.Zero;
        //        cash = frontCashDetail.Where(w => w.DailyReportItemCD == 6).FirstOrDefault();
        //        cashName6 = cash?.DailyReportItemName;
        //        cashPrice6 = cash?.Amount ?? decimal.Zero;
        //        cash = frontCashDetail.Where(w => w.DailyReportItemCD == 7).FirstOrDefault();
        //        cashName7 = cash?.DailyReportItemName;
        //        cashPrice7 = cash?.Amount ?? decimal.Zero;
        //        // フロント現金売高
        //        frontCashSalesTotal = cashPriceTotal
        //            + _TPa_SlipModel.GetDepartmentCashSales(exCon.OfficeCD, exCon.BusinessDate)
        //            + _TPa_FrontCashDetailModel.GetFrontCash(exCon.OfficeCD, exCon.BusinessDate);
        //        // 当日釣銭額
        //        todayChange = basicItem?.TodayChangeAmount ?? decimal.Zero;
        //        // フロント現金有高
        //        frontCashTotal = basicItem?.FrontCashAmount ?? decimal.Zero;
        //        // 現金過不足額 = フロント現金有高 - フロント現金売高 - 当日釣銭額
        //        cashExcessOrDeficiency = frontCashTotal - frontCashSalesTotal - todayChange;
        //        // 翌日繰越額
        //        carryOverAmount = basicItem?.NextdayChangeAmount ?? decimal.Zero;
        //        // 口座振込額
        //        bankTransferAmount = basicItem?.BankTransferAmount ?? decimal.Zero;
        //    }
        //    // データのセット
        //    PrintData[0].CashPriceTotal = cashPriceTotal;
        //    PrintData[0].CashName1 = cashName1;
        //    PrintData[0].CashPrice1 = cashPrice1;
        //    PrintData[0].CashName2 = cashName2;
        //    PrintData[0].CashPrice2 = cashPrice2;
        //    PrintData[0].CashName3 = cashName3;
        //    PrintData[0].CashPrice3 = cashPrice3;
        //    PrintData[0].CashName4 = cashName4;
        //    PrintData[0].CashPrice4 = cashPrice4;
        //    PrintData[0].CashName5 = cashName5;
        //    PrintData[0].CashPrice5 = cashPrice5;
        //    PrintData[0].CashName6 = cashName6;
        //    PrintData[0].CashPrice6 = cashPrice6;
        //    PrintData[0].CashName7 = cashName7;
        //    PrintData[0].CashPrice7 = cashPrice7;
        //    PrintData[0].FrontCashSalesTotal = frontCashSalesTotal;
        //    PrintData[0].TodayChange = todayChange;
        //    PrintData[0].FrontCashTotal = frontCashTotal;
        //    PrintData[0].CashExcessOrDeficiency = cashExcessOrDeficiency;
        //    PrintData[0].CarryOverAmount = carryOverAmount;
        //    PrintData[0].BankTransferAmount = bankTransferAmount;
        //}
        #endregion

        #region 支払方法別売上高
        ///// <summary>
        ///// 支払方法別売上高項目のデータを生成します。
        ///// </summary>
        ///// <param name="exCon">拡張印刷条件</param>
        ///// <returns></returns>
        //private void CreateAmountByPaymentMethod(ExtendDailyReportPrintCondition exCon)
        //{
        //    dynamic grouped = null;
        //    if (exCon.IsCurrentDay)
        //    {
        //        // Todo:これだけではだめ。viwFZZend00,viwFZzenN10が必要
        //        grouped = _TFr_AmountByPaymentMethodModel.GetList(exCon.OfficeCD, exCon.BusinessDate)
        //            .GroupBy(g => new { g.DepositTypeCD, g.DepositTypeName })
        //            .Select(s =>
        //            {
        //                return new
        //                {
        //                    Code = s.Key.DepositTypeCD,
        //                    Name = s.Key.DepositTypeName,
        //                    Count = s.Count(),
        //                    Amount = s.Sum(s => s.Amount) ?? decimal.Zero
        //                };
        //            });
        //    }
        //    else
        //    {
        //        grouped = _TPa_AmountByPaymentMethodModel.GetList(exCon.OfficeCD, exCon.BusinessDate)
        //            .GroupBy(g => new { g.DepositTypeCD, g.DepositTypeName })
        //            .Select(s =>
        //            {
        //                return new
        //                {
        //                    Code = s.Key.DepositTypeCD,
        //                    Name = s.Key.DepositTypeName,
        //                    Count = s.Count(),
        //                    Amount = s.Sum(s => s.Amount) ?? decimal.Zero
        //                };
        //            });
        //    }

        //    // データのセット
        //    foreach (var item in grouped)
        //    {
        //        switch (item.Code)
        //        {
        //            case 1:
        //                PrintData[0].SalesByPaymentMethodName1 = item.Name;
        //                PrintData[0].SalesByPaymentMethodCount1 = item.Count;
        //                PrintData[0].SalesByPaymentMethodPrice1 = item.Amount;
        //                break;
        //            case 2:
        //                PrintData[0].SalesByPaymentMethodName2 = item.Name;
        //                PrintData[0].SalesByPaymentMethodCount2 = item.Count;
        //                PrintData[0].SalesByPaymentMethodPrice2 = item.Amount;
        //                break;
        //            case 3:
        //                PrintData[0].SalesByPaymentMethodName3 = item.Name;
        //                PrintData[0].SalesByPaymentMethodCount3 = item.Count;
        //                PrintData[0].SalesByPaymentMethodPrice3 = item.Amount;
        //                break;
        //            case 4:
        //                PrintData[0].SalesByPaymentMethodName4 = item.Name;
        //                PrintData[0].SalesByPaymentMethodCount4 = item.Count;
        //                PrintData[0].SalesByPaymentMethodPrice4 = item.Amount;
        //                break;
        //            case 5:
        //                PrintData[0].SalesByPaymentMethodName5 = item.Name;
        //                PrintData[0].SalesByPaymentMethodCount5 = item.Count;
        //                PrintData[0].SalesByPaymentMethodPrice5 = item.Amount;
        //                break;
        //            case 6:
        //                PrintData[0].SalesByPaymentMethodName6 = item.Name;
        //                PrintData[0].SalesByPaymentMethodCount6 = item.Count;
        //                PrintData[0].SalesByPaymentMethodPrice6 = item.Amount;
        //                break;
        //            case 7:
        //                PrintData[0].SalesByPaymentMethodName7 = item.Name;
        //                PrintData[0].SalesByPaymentMethodCount7 = item.Count;
        //                PrintData[0].SalesByPaymentMethodPrice7 = item.Amount;
        //                break;
        //            default:
        //                break;
        //        }

        //        PrintData[0].SalesByPaymentMethodTotalCount += item.Count;
        //        PrintData[0].SalesByPaymentMethodTotalPrice += item.Amount;
        //    }
        //}
        #endregion

        #region クレジットカード
        ///// <summary>
        ///// クレジットカードについてのデータを生成します。
        ///// </summary>
        ///// <returns></returns>
        //private void CreateCredit(ExtendDailyReportPrintCondition exCon)
        //{
        //    // TM_カード分類→TMa_PaymentCls(決済分類)
        //    // TU_カード残高→TCs_CardBalance(カード残高)
        //    // TM_カード会社→TMa_PaymentType(決済種別)

        //    // 決済分類一覧を取得する
        //    var paymentCls = _TMa_PaymentClsModel.GetList(exCon.OfficeCD);
        //    // 決済分類CDをメインに値を取得する
        //    foreach (var item in paymentCls)
        //    {
        //        var paymentClsName = item.PaymentClsName;
        //        var currentPrice = GetCardBalanceCurrentAccountsReceivableAmount(exCon.OfficeCD, exCon.BusinessDate, item.PaymentClsCD);
        //        switch (item.PaymentClsCD)
        //        {
        //            case 1:
        //                PrintData[0].CreditName1 = paymentClsName;
        //                PrintData[0].CreditPrice1 = currentPrice;
        //                break;
        //            case 2:
        //                PrintData[0].CreditName2 = paymentClsName;
        //                PrintData[0].CreditPrice2 = currentPrice;
        //                break;
        //            case 3:
        //                PrintData[0].CreditName3 = paymentClsName;
        //                PrintData[0].CreditPrice3 = currentPrice;
        //                break;
        //            case 4:
        //                PrintData[0].CreditName4 = paymentClsName;
        //                PrintData[0].CreditPrice4 = currentPrice;
        //                break;
        //            case 5:
        //                PrintData[0].CreditName5 = paymentClsName;
        //                PrintData[0].CreditPrice5 = currentPrice;
        //                break;
        //            case 6:
        //                PrintData[0].CreditName6 = paymentClsName;
        //                PrintData[0].CreditPrice6 = currentPrice;
        //                break;
        //            case 7:
        //                PrintData[0].CreditName7 = paymentClsName;
        //                PrintData[0].CreditPrice7 = currentPrice;
        //                break;
        //            default:
        //                break;
        //        }
        //        PrintData[0].CreditPriceTotal += currentPrice;
        //    }
        //}

        ///// <summary>
        ///// カード残高の当日売掛額を取得します
        ///// </summary>
        ///// <param name="officeCD">事業者コード</param>
        ///// <param name="businessDate">営業日</param>
        ///// <param name="paymentClsCD">決済分類コード</param>
        ///// <returns></returns>
        //private decimal GetCardBalanceCurrentAccountsReceivableAmount(string officeCD, DateTime businessDate, int paymentClsCD)
        //{
        //    StringBuilder query = new StringBuilder();
        //    query
        //        .AppendLine("SELECT TOP 1")
        //        .AppendLine("    ISNULL(SUM([CardBalance].[TodayAccountsReceivable]),0) AS [CurrentAccountsReceivable]")
        //        .AppendLine("FROM")
        //        .AppendLine("    [TMa_PaymentType] AS [PaymentType]")
        //        .AppendLine("    LEFT OUTER JOIN [TCs_CardBalance] AS [CardBalance] ON [PaymentType].[OfficeCD] = [CardBalance].[OfficeCD]")
        //        .AppendLine("    AND [PaymentType].[PaymentTypeCD] = [CardBalance].[PaymentTypeCD]")
        //        .AppendLine("WHERE")
        //        .AppendLine("    [PaymentType].[OfficeCD] = @officeCD")
        //        .AppendLine("    AND [CardBalance].[BusinessDate] = @businessDate")
        //        .AppendLine("    AND [PaymentType].[PaymentClsCD] = @paymentClsCD")
        //        .AppendLine("GROUP BY")
        //        .AppendLine("    [PaymentType].[PaymentTypeCD]");

        //    return _DapperAction.GetFirstDataByQuery<decimal>(
        //        ConnectionTypes.Data,
        //        query.ToString(),
        //        new
        //        {
        //            officeCD,
        //            businessDate,
        //            paymentClsCD
        //        }
        //    );
        //}
        #endregion

        #region 地区
        ///// <summary>
        ///// 地区データを生成します。
        ///// </summary>
        ///// <param name="exCon"></param>
        //private void CreateArea(ExtendDailyReportPrintCondition exCon)
        //{
        //    // 地区分類一覧を取得する
        //    var areaCls = _TMa_AreaClsModel.GetList(exCon.OfficeCD);
        //    // 地区分類CDをメインに値を取得する
        //    foreach (var item in areaCls)
        //    {
        //        var areaName = item.AreaClsName;
        //        var todayAreaCount = GetTodayAreaAttendancesCount(exCon, item.AreaClsCD);
        //        var monthAreaCount = GetMonthAreaAttendancesCount(exCon, item.AreaClsCD);
        //        var yearAreaCount = GetYearAreaAttendancesCount(exCon, item.AreaClsCD);
        //        switch (item.AreaClsCD)
        //        {
        //            case 1:
        //                PrintData[0].AreaName1 = areaName;
        //                PrintData[0].TodayAreaCount1 = todayAreaCount;
        //                PrintData[0].MonthAreaCount1 = monthAreaCount;
        //                PrintData[0].YearAreaCount1 = yearAreaCount;
        //                break;
        //            case 2:
        //                PrintData[0].AreaName2 = areaName;
        //                PrintData[0].TodayAreaCount2 = todayAreaCount;
        //                PrintData[0].MonthAreaCount2 = monthAreaCount;
        //                PrintData[0].YearAreaCount2 = yearAreaCount;
        //                break;
        //            case 3:
        //                PrintData[0].AreaName3 = areaName;
        //                PrintData[0].TodayAreaCount3 = todayAreaCount;
        //                PrintData[0].MonthAreaCount3 = monthAreaCount;
        //                PrintData[0].YearAreaCount3 = yearAreaCount;
        //                break;
        //            case 4:
        //                PrintData[0].AreaName4 = areaName;
        //                PrintData[0].TodayAreaCount4 = todayAreaCount;
        //                PrintData[0].MonthAreaCount4 = monthAreaCount;
        //                PrintData[0].YearAreaCount4 = yearAreaCount;
        //                break;
        //            case 5:
        //                PrintData[0].AreaName5 = areaName;
        //                PrintData[0].TodayAreaCount5 = todayAreaCount;
        //                PrintData[0].MonthAreaCount5 = monthAreaCount;
        //                PrintData[0].YearAreaCount5 = yearAreaCount;
        //                break;
        //            case 6:
        //                PrintData[0].AreaName6 = areaName;
        //                PrintData[0].TodayAreaCount6 = todayAreaCount;
        //                PrintData[0].MonthAreaCount6 = monthAreaCount;
        //                PrintData[0].YearAreaCount6 = yearAreaCount;
        //                break;
        //            case 7:
        //                PrintData[0].AreaName7 = areaName;
        //                PrintData[0].TodayAreaCount7 = todayAreaCount;
        //                PrintData[0].MonthAreaCount7 = monthAreaCount;
        //                PrintData[0].YearAreaCount7 = yearAreaCount;
        //                break;
        //            default:
        //                break;
        //        }
        //        PrintData[0].TodayAreaCountTotal += todayAreaCount;
        //        PrintData[0].MonthAreaCountTotal += monthAreaCount;
        //        PrintData[0].YearAreaCountTotal += yearAreaCount;
        //    }
        //}

        ///// <summary>
        ///// 当日の地区来場者数を取得します。
        ///// </summary>
        ///// <param name="exCon"></param>
        ///// <param name="areaClsCD"></param>
        //private int GetTodayAreaAttendancesCount(ExtendDailyReportPrintCondition exCon, int areaClsCD)
        //{
        //    return exCon.IsCurrentDay
        //        ? GetAreaAttendancesCountFromReservationPlayer(exCon.OfficeCD, areaClsCD, exCon.BusinessDate)
        //        : GetAreaAttendancesCount(exCon.OfficeCD, areaClsCD, exCon.BusinessDate);
        //}

        ///// <summary>
        ///// 本月の地区来場者数を取得します。
        ///// </summary>
        ///// <param name="exCon"></param>
        ///// <param name="areaClsCD"></param>
        //private int GetMonthAreaAttendancesCount(ExtendDailyReportPrintCondition exCon, int areaClsCD)
        //{
        //    return GetAreaAttendancesCount(exCon.OfficeCD, areaClsCD, exCon.MonthFrom, exCon.MonthTo);
        //}

        ///// <summary>
        ///// 本年の地区来場者数を取得します。
        ///// </summary>
        ///// <param name="exCon"></param>
        ///// <param name="areaClsCD"></param>
        //private int GetYearAreaAttendancesCount(ExtendDailyReportPrintCondition exCon, int areaClsCD)
        //{
        //    return GetAreaAttendancesCount(exCon.OfficeCD, areaClsCD, exCon.YearFrom, exCon.YearTo);
        //}

        ///// <summary>
        ///// 予約来場者データをもとに当日の地区来場者数を取得します。
        ///// </summary>
        ///// <param name="officeCD"></param>
        ///// <param name="areaClsCD"></param>
        ///// <param name="businessDate"></param>
        ///// <returns></returns>
        //private int GetAreaAttendancesCountFromReservationPlayer(string officeCD, int areaClsCD, DateTime businessDate)
        //{
        //    // viwFZchik10 TF_来場→TRe_ReservationPlayer TB_顧客→TMc_Customer
        //    StringBuilder query = new StringBuilder();
        //    query
        //        .AppendLine("SELECT TOP 1")
        //        .AppendLine("    ISNULL(COUNT(*),0) AS [AttendanceCount]")
        //        .AppendLine("FROM")
        //        .AppendLine("    [TRe_ReservationPlayer] AS [ReservationPlayer]")
        //        .AppendLine("    LEFT OUTER JOIN [TMa_Area] AS [Area] ON [ReservationPlayer].[OfficeCD] = [Area].[OfficeCD]")
        //        .AppendLine("    AND [ReservationPlayer].[AreaCD] = [Area].[AreaCD]")
        //        .AppendLine("WHERE")
        //        .AppendLine("    [ReservationPlayer].[OfficeCD] = @officeCD")
        //        .AppendLine("    AND [ReservationPlayer].[FeePrivilegeTypeCD] <> " + PrivilegeTypeCD.NonPlay)
        //        .AppendLine("    AND [ReservationPlayer].[BusinessDate] = @businessDate")
        //        .AppendLine("    AND [Area].[AreaClsCD] = @areaClsCD")
        //        .AppendLine("GROUP BY")
        //        .AppendLine("    [ReservationPlayer].[AreaCD]");
        //    return _DapperAction.GetFirstDataByQuery<int>(
        //        ConnectionTypes.Data,
        //        query.ToString(),
        //        new
        //        {
        //            officeCD,
        //            businessDate,
        //            areaClsCD
        //        }
        //    );
        //}

        ///// <summary>
        ///// 地区来場者数取得共通処理
        ///// </summary>
        ///// <param name="officeCD"></param>
        ///// <param name="areaClsCD"></param>
        ///// <param name="from"></param>
        ///// <param name="to"></param>
        ///// <returns></returns>
        //private int GetAreaAttendancesCount(string officeCD, int areaClsCD, DateTime from, DateTime? to = null)
        //{
        //    // 条件生成
        //    var dateCondition = "= @businessDate";
        //    dynamic param = new { officeCD, areaClsCD, businessDate = from };
        //    if (to.HasValue)
        //    {
        //        dateCondition = "BETWEEN @from AND @to";
        //        param = new { officeCD, areaClsCD, from, to };
        //    }
        //    // クエリ生成
        //    StringBuilder query = new StringBuilder();
        //    query
        //        .AppendLine("SELECT TOP 1")
        //        .AppendLine("    ISNULL(SUM([AreaAttendance].[AttendanceCount]),0) AS [AttendanceCount]")
        //        .AppendLine("FROM")
        //        .AppendLine("    [TMa_Area] AS [Area]")
        //        .AppendLine("    LEFT OUTER JOIN [TAs_AreaAttendance] AS [AreaAttendance] ON [Area].[OfficeCD] = [AreaAttendance].[OfficeCD]")
        //        .AppendLine("    AND [Area].[AreaCD] = [AreaAttendance].[AreaCD]")
        //        .AppendLine("WHERE")
        //        .AppendLine("    [AreaAttendance].[OfficeCD] = @officeCD")
        //        .AppendLine($"    AND [AreaAttendance].[BusinessDate] {dateCondition}")
        //        .AppendLine("    AND [Area].[AreaClsCD] = @areaClsCD")
        //        .AppendLine("GROUP BY")
        //        .AppendLine("    [AreaAttendance].[AreaCD]");
        //    // クエリ実行
        //    return _DapperAction.GetFirstDataByQuery<int>(
        //        ConnectionTypes.Data,
        //        query.ToString(),
        //        param
        //    );
        //}
        #endregion

        #region その他情報
        //private void CreateOther(ExtendDailyReportPrintCondition exCon)
        //{

        //}
        #endregion
        #endregion
    }
}
