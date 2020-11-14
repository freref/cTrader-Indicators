using System;
using cAlgo.API;
using cAlgo.API.Internals;
using cAlgo.API.Indicators;
using cAlgo.Indicators;

namespace cAlgo
{
    [Indicator(IsOverlay = false, TimeZone = TimeZones.UTC, AccessRights = AccessRights.None)]
    public class ZADX : Indicator
    {
        [Parameter(DefaultValue = 224)]
        public int Period { get; set; }

        [Output("+DI", PlotType = PlotType.Line, LineColor = "Blue", Thickness = 1)]
        public IndicatorDataSeries Plus { get; set; }

        [Output("-DI", PlotType = PlotType.Line, LineColor = "Red", Thickness = 1)]
        public IndicatorDataSeries Minus { get; set; }

        public IndicatorDataSeries plusdi, minusdi;
        public ExponentialMovingAverage plusma, minusma;

        public double tr;
        public double pdm, mdm;
        public double num1, num2, num3;

        protected override void Initialize()
        {
            plusdi = CreateDataSeries();
            minusdi = CreateDataSeries();
            plusma = Indicators.ExponentialMovingAverage(plusdi, Period);
            minusma = Indicators.ExponentialMovingAverage(minusdi, Period);
        }

        public override void Calculate(int index)
        {
            pdm = Bars[index].High - Bars[index - 1].High;
            mdm = Bars[index - 1].Low - Bars[index].Low;

            if (pdm < 0)
                pdm = 0;

            if (mdm < 0)
                mdm = 0;

            if (pdm == mdm)
            {
                pdm = 0;
                mdm = 0;
            }
            else if (pdm < mdm)
                pdm = 0;
            else if (mdm < pdm)
                mdm = 0;

            num1 = Math.Abs(Bars[index].High - Bars[index].Low);
            num2 = Math.Abs(Bars[index].High - Bars[index].Close);
            num3 = Math.Abs(Bars[index].Low - Bars[index].Close);

            tr = Math.Max(num1, Math.Max(num2, num3));

            if (tr == 0)
            {
                plusdi[index] = 0;
                minusdi[index] = 0;
            }
            else
            {
                plusdi[index] = 100 * pdm / tr;
                minusdi[index] = 100 * mdm / tr;
            }

            Plus[index] = plusma.Result[index];
            Minus[index] = minusma.Result[index];
        }
    }
}
