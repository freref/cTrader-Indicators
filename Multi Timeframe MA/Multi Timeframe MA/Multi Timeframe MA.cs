using System;
using cAlgo.API;
using cAlgo.API.Internals;
using cAlgo.API.Indicators;

namespace cAlgo.Indicators
{
    [Indicator(IsOverlay = true, TimeZone = TimeZones.UTC)]
    public class MultiTimeframeMA : Indicator
    {

        [Output("MA1", LineColor = "FFBF9100")]
        public IndicatorDataSeries MA1 { get; set; }

        [Output("MA2", LineColor = "FF808080")]
        public IndicatorDataSeries MA2 { get; set; }

        [Output("MA3", LineColor = "Red")]
        public IndicatorDataSeries MA3 { get; set; }

        [Output("MA4", LineColor = "FF02AFF1")]
        public IndicatorDataSeries MA4 { get; set; }


        [Parameter("Visible", Group = "Moving Average 1", DefaultValue = true)]
        public bool Visibility1 { get; set; }

        [Parameter("Period", Group = "Moving Average 1", DefaultValue = 200)]
        public int Period1 { get; set; }

        [Parameter("Type", Group = "Moving Average 1", DefaultValue = MovingAverageType.Exponential)]
        public MovingAverageType MAType1 { get; set; }

        [Parameter("Timeframe", Group = "Moving Average 1", DefaultValue = "Daily")]
        public TimeFrame timeframe1 { get; set; }

        [Parameter("Color", Group = "Moving Average 1", DefaultValue = "FFBF9100")]
        public string lineColor1 { get; set; }

        [Parameter("Linestyle", Group = "Moving Average 1", DefaultValue = "Solid")]
        public LineStyle LineStyle1 { get; set; }

        [Parameter("Thickness", Group = "Moving Average 1", DefaultValue = "1", MinValue = 1)]
        public int Thickness1 { get; set; }

        private Bars series1;

        private MovingAverage ma1;

        public Color color1;


        [Parameter("Visible", Group = "Moving Average 2", DefaultValue = true)]
        public bool Visibility2 { get; set; }

        [Parameter("Period", Group = "Moving Average 2", DefaultValue = 100)]
        public int Period2 { get; set; }

        [Parameter("Type", Group = "Moving Average 2", DefaultValue = MovingAverageType.Exponential)]
        public MovingAverageType MAType2 { get; set; }

        [Parameter("Timeframe", Group = "Moving Average 2", DefaultValue = "Daily")]
        public TimeFrame timeframe2 { get; set; }

        [Parameter("Color", Group = "Moving Average 2", DefaultValue = "FF808080")]
        public string lineColor2 { get; set; }

        [Parameter("Linestyle", Group = "Moving Average 2", DefaultValue = "Solid")]
        public LineStyle LineStyle2 { get; set; }

        [Parameter("Thickness", Group = "Moving Average 2", DefaultValue = "1", MinValue = 1)]
        public int Thickness2 { get; set; }

        private Bars series2;

        private MovingAverage ma2;

        public Color color2;


        [Parameter("Visible", Group = "Moving Average 3", DefaultValue = true)]
        public bool Visibility3 { get; set; }

        [Parameter("Period", Group = "Moving Average 3", DefaultValue = 50)]
        public int Period3 { get; set; }

        [Parameter("Type", Group = "Moving Average 3", DefaultValue = MovingAverageType.Exponential)]
        public MovingAverageType MAType3 { get; set; }

        [Parameter("Timeframe", Group = "Moving Average 3", DefaultValue = "Daily")]
        public TimeFrame timeframe3 { get; set; }

        [Parameter("Color", Group = "Moving Average 3", DefaultValue = "FFFE0000")]
        public string lineColor3 { get; set; }

        [Parameter("Linestyle", Group = "Moving Average 3", DefaultValue = "Solid")]
        public LineStyle LineStyle3 { get; set; }

        [Parameter("Thickness", Group = "Moving Average 3", DefaultValue = "1", MinValue = 1)]
        public int Thickness3 { get; set; }

        private Bars series3;

        private MovingAverage ma3;

        public Color color3;


        [Parameter("Visible", Group = "Moving Average 4", DefaultValue = true)]
        public bool Visibility4 { get; set; }

        [Parameter("Period", Group = "Moving Average 4", DefaultValue = 28)]
        public int Period4 { get; set; }

        [Parameter("Type", Group = "Moving Average 4", DefaultValue = MovingAverageType.Exponential)]
        public MovingAverageType MAType4 { get; set; }

        [Parameter("Timeframe", Group = "Moving Average 4", DefaultValue = "Daily")]
        public TimeFrame timeframe4 { get; set; }

        [Parameter("Color", Group = "Moving Average 4", DefaultValue = "FF02AFF1")]
        public string lineColor4 { get; set; }

        [Parameter("Linestyle", Group = "Moving Average 4", DefaultValue = "Solid")]
        public LineStyle LineStyle4 { get; set; }

        [Parameter("Thickness", Group = "Moving Average 4", DefaultValue = "1", MinValue = 1)]
        public int Thickness4 { get; set; }

        private Bars series4;

        private MovingAverage ma4;

        public Color color4;

        public double val1;

        public double val2;

        public double val3;

        public double val4;

        protected override void Initialize()
        {
            color1 = Color.FromHex(lineColor1);
            series1 = MarketData.GetBars(timeframe1);
            ma1 = Indicators.MovingAverage(series1.ClosePrices, Period1, MAType1);
            val1 = ma1.Result.LastValue;
            if (Visibility1 == true)
                Chart.DrawTrendLine("Extension1", series1.OpenTimes.LastValue, val1, MarketData.GetBars(TimeFrame.Minute).OpenTimes.LastValue, val1, color1, Thickness1, LineStyle1);

            color2 = Color.FromHex(lineColor2);
            series2 = MarketData.GetBars(timeframe2);
            ma2 = Indicators.MovingAverage(series2.ClosePrices, Period2, MAType2);
            val2 = ma2.Result.LastValue;
            if (Visibility2 == true)
                Chart.DrawTrendLine("Extension2", series2.OpenTimes.LastValue, val2, MarketData.GetBars(TimeFrame.Minute).OpenTimes.LastValue, val2, color2, Thickness2, LineStyle2);



            color3 = Color.FromHex(lineColor3);
            series3 = MarketData.GetBars(timeframe3);
            ma3 = Indicators.MovingAverage(series3.ClosePrices, Period3, MAType3);
            val3 = ma3.Result.LastValue;
            if (Visibility3 == true)
                Chart.DrawTrendLine("Extension3", series3.OpenTimes.LastValue, val3, MarketData.GetBars(TimeFrame.Minute).OpenTimes.LastValue, val3, color3, Thickness3, LineStyle3);


            color4 = Color.FromHex(lineColor4);
            series4 = MarketData.GetBars(timeframe4);
            ma4 = Indicators.MovingAverage(series4.ClosePrices, Period4, MAType4);
            val4 = ma4.Result.LastValue;
            if (Visibility4 == true)
                Chart.DrawTrendLine("Extension4", series4.OpenTimes.LastValue, val4, MarketData.GetBars(TimeFrame.Minute).OpenTimes.LastValue, val4, color4, Thickness4, LineStyle4);

        }

        public override void Calculate(int index)
        {
            DateTime open = MarketData.GetBars(Chart.TimeFrame).OpenTimes.LastValue;
            open = open.AddSeconds(-open.Second);
            open = open.AddMilliseconds(-open.Millisecond);

            DateTime now = Server.TimeInUtc;
            now = now.AddSeconds(-now.Second);
            now = now.AddMilliseconds(-now.Millisecond);

            int res = DateTime.Compare(open, now);
            if (res == 0)
            {
                Chart.RemoveObject("Extension1");
                Chart.RemoveObject("Extension2");
                Chart.RemoveObject("Extension3");
                Chart.RemoveObject("Extension4");
                if (Visibility1 == true)
                {
                    series1 = MarketData.GetBars(timeframe1);
                    ma1 = Indicators.MovingAverage(series1.ClosePrices, Period1, MAType1);
                    Chart.DrawTrendLine("Extension1", series1.OpenTimes.LastValue, val1, MarketData.GetBars(TimeFrame.Minute).OpenTimes.LastValue, val1, color1, Thickness1, LineStyle1);

                    DateTime now1 = Server.TimeInUtc;
                    now1 = now1.AddSeconds(-now1.Second);
                    now1 = now1.AddMilliseconds(-now1.Millisecond);

                    DateTime open1 = series1.OpenTimes.LastValue;
                    open1 = open1.AddSeconds(-open1.Second);
                    open1 = open1.AddMilliseconds(-open1.Millisecond);

                    int res1 = DateTime.Compare(open1, now1);

                    if (res1 == 0)
                    {
                        val1 = ma1.Result.LastValue;
                    }
                }

                if (Visibility2 == true)
                {
                    series2 = MarketData.GetBars(timeframe2);
                    ma2 = Indicators.MovingAverage(series2.ClosePrices, Period2, MAType2);
                    Chart.DrawTrendLine("Extension2", series2.OpenTimes.LastValue, val2, MarketData.GetBars(TimeFrame.Minute).OpenTimes.LastValue, val2, color2, Thickness2, LineStyle2);

                    DateTime now2 = Server.TimeInUtc;
                    now2 = now2.AddSeconds(-now2.Second);
                    now2 = now2.AddMilliseconds(-now2.Millisecond);

                    DateTime open2 = series2.OpenTimes.LastValue;
                    open2 = open2.AddSeconds(-open2.Second);
                    open2 = open2.AddMilliseconds(-open2.Millisecond);

                    int res2 = DateTime.Compare(open2, now2);

                    if (res2 == 0)
                    {
                        val2 = ma2.Result.LastValue;
                    }
                }

                if (Visibility3 == true)
                {
                    series3 = MarketData.GetBars(timeframe3);
                    ma3 = Indicators.MovingAverage(series3.ClosePrices, Period3, MAType3);
                    Chart.DrawTrendLine("Extension3", series3.OpenTimes.LastValue, val3, MarketData.GetBars(TimeFrame.Minute).OpenTimes.LastValue, val3, color3, Thickness3, LineStyle3);

                    DateTime now3 = Server.TimeInUtc;
                    now3 = now3.AddSeconds(-now3.Second);
                    now3 = now3.AddMilliseconds(-now3.Millisecond);

                    DateTime open3 = series3.OpenTimes.LastValue;
                    open3 = open3.AddSeconds(-open3.Second);
                    open3 = open3.AddMilliseconds(-open3.Millisecond);

                    int res3 = DateTime.Compare(open3, now3);

                    if (res3 == 0)
                    {
                        val3 = ma3.Result.LastValue;
                    }
                }

                if (Visibility4 == true)
                {
                    series4 = MarketData.GetBars(timeframe4);
                    ma4 = Indicators.MovingAverage(series4.ClosePrices, Period4, MAType4);
                    Chart.DrawTrendLine("Extension4", series4.OpenTimes.LastValue, val4, MarketData.GetBars(TimeFrame.Minute).OpenTimes.LastValue, val4, color4, Thickness4, LineStyle4);

                    DateTime now4 = Server.TimeInUtc;
                    now4 = now4.AddSeconds(-now4.Second);
                    now4 = now4.AddMilliseconds(-now4.Millisecond);

                    DateTime open4 = series4.OpenTimes.LastValue;
                    open4 = open4.AddSeconds(-open4.Second);
                    open4 = open4.AddMilliseconds(-open4.Millisecond);

                    int res4 = DateTime.Compare(open4, now4);

                    if (res4 == 0)
                    {
                        val4 = ma4.Result.LastValue;
                    }
                }
            }

            var index1 = GetIndexByDate(series1, Bars.OpenTimes[index]);

            if (index1 != -1)
                MA1[index] = ma1.Result[index1];

            var index2 = GetIndexByDate(series2, Bars.OpenTimes[index]);
            if (index2 != -1)
                MA2[index] = ma2.Result[index2];

            var index3 = GetIndexByDate(series3, Bars.OpenTimes[index]);
            if (index3 != -1)
                MA3[index] = ma3.Result[index3];

            var index4 = GetIndexByDate(series4, Bars.OpenTimes[index]);
            if (index4 != -1)
                MA4[index] = ma4.Result[index4];

        }


        private int GetIndexByDate(Bars series, DateTime time)
        {
            for (int i = series.ClosePrices.Count - 1; i > 0; i--)
            {
                if (time == series.OpenTimes[i])
                    return i;
            }
            return -1;
        }
    }
}

