using System;
using cAlgo.API;
using cAlgo.API.Indicators;

namespace cAlgo.Indicators
{
    [Cloud("MACD", "Signal", Opacity = 0.2, FirstColor = "FF7CFC00")]
    [Indicator(IsOverlay = false, AccessRights = AccessRights.None)]
    public class ZMACD : Indicator
    {


        [Output("Bull Up", PlotType = PlotType.Histogram, LineColor = "ForestGreen", Thickness = 4)]
        public IndicatorDataSeries StrongBullish { get; set; }

        [Output("Bull Down", PlotType = PlotType.Histogram, LineColor = "LawnGreen", Thickness = 4)]
        public IndicatorDataSeries WeakBulish { get; set; }

        [Output("Bear Down", PlotType = PlotType.Histogram, LineColor = "Red", Thickness = 4)]
        public IndicatorDataSeries StrongBearish { get; set; }

        [Output("Bear Up", PlotType = PlotType.Histogram, LineColor = "DarkSalmon", Thickness = 4)]
        public IndicatorDataSeries WeakBearish { get; set; }

        [Output("MACD", LineColor = "DodgerBlue", LineStyle = LineStyle.Lines)]
        public IndicatorDataSeries MACD { get; set; }

        [Output("Signal", LineColor = "Red", LineStyle = LineStyle.Lines, Thickness = 1)]
        public IndicatorDataSeries Signal { get; set; }

        [Parameter(DefaultValue = 12)]
        public int periodFast { get; set; }

        [Parameter(DefaultValue = 26)]
        public int periodSlow { get; set; }

        [Parameter(DefaultValue = 9)]
        public int periodSignal { get; set; }

        private WeightedMovingAverage emaSlow;
        private WeightedMovingAverage emaFast;
        private WeightedMovingAverage emaSignal;
        private IndicatorDataSeries closePrice;
        private IndicatorDataSeries macd;

        protected override void Initialize()
        {
            closePrice = CreateDataSeries();
            macd = CreateDataSeries();
            emaSlow = Indicators.WeightedMovingAverage(closePrice, periodSlow);
            emaFast = Indicators.WeightedMovingAverage(closePrice, periodFast);
            emaSignal = Indicators.WeightedMovingAverage(macd, periodSignal);
        }

        public override void Calculate(int index)
        {
            closePrice[index] = Bars.ClosePrices[index];
            double prevMACD = emaFast.Result[index - 1] - emaSlow.Result[index - 1];
            double currentMACD = emaFast.Result[index] - emaSlow.Result[index];
            MACD[index] = emaFast.Result[index] - emaSlow.Result[index];
            macd[index] = MACD[index];
            Signal[index] = emaSignal.Result[index];
            double signalValue = emaSignal.Result[index];
            double Histogram = MACD[index] - emaSignal.Result[index];
            double prevHistogram = MACD[index - 1] - emaSignal.Result[index - 1];

            if (Histogram > 0.0)
            {
                if (prevHistogram >= Histogram)
                {
                    WeakBulish[index] = MACD[index] - emaSignal.Result[index];
                }
                else
                {
                    StrongBullish[index] = MACD[index] - emaSignal.Result[index];
                }
            }
            else if (Histogram < 0.0)
            {
                if (Histogram <= prevHistogram)
                {
                    StrongBearish[index] = MACD[index] - emaSignal.Result[index];
                }
                else
                {
                    WeakBearish[index] = MACD[index] - emaSignal.Result[index];
                }
            }
        }
    }
}
