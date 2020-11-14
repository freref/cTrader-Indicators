using System;
using cAlgo.API;
using cAlgo.API.Internals;
using cAlgo.API.Indicators;
using cAlgo.Indicators;

namespace cAlgo
{
    [Indicator(IsOverlay = false, TimeZone = TimeZones.UTC, AccessRights = AccessRights.None)]
    public class ZCROSS : Indicator
    {
        [Parameter(DefaultValue = 224)]
        public int Period { get; set; }

        [Parameter(DefaultValue = "blue")]
        public string upcolor { get; set; }

        [Parameter(DefaultValue = "red")]
        public string downcolor { get; set; }

        [Output("Main", PlotType = PlotType.Histogram, LineColor = "Blue", Thickness = 3)]
        public IndicatorDataSeries Result { get; set; }

        public ZADX adx;
        public bool plus;

        protected override void Initialize()
        {
            adx = Indicators.GetIndicator<ZADX>(Period);

            if (adx.Plus.LastValue >= adx.Minus.LastValue)
                plus = true;
            else
                plus = false;
        }

        public override void Calculate(int index)
        {
            if (adx.Plus[index] >= adx.Minus[index] && !plus)
            {
                Chart.DrawIcon("Buy" + index, ChartIconType.UpArrow, Bars[index].OpenTime, Bars[index].Low, upcolor);
                plus = true;
                Result[index] = -1;
            }
            else if (adx.Plus[index] <= adx.Minus[index] && plus)
            {
                Chart.DrawIcon("Sell" + index, ChartIconType.DownArrow, Bars[index].OpenTime, Bars[index].High, downcolor);
                plus = false;
                Result[index] = 1;
            }
            else
                Result[index] = 0;
        }
    }
}
