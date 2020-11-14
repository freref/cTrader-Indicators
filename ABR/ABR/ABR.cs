using System;
using cAlgo.API;
using cAlgo.API.Internals;
using cAlgo.API.Indicators;
using cAlgo.Indicators;

namespace cAlgo
{
    [Indicator(IsOverlay = false, TimeZone = TimeZones.UTC, AccessRights = AccessRights.None)]
    public class ABR : Indicator
    {
        [Parameter(DefaultValue = 14)]
        public int Period { get; set; }

        [Output("Main")]
        public IndicatorDataSeries Result { get; set; }

        public double sum = 0;

        protected override void Initialize()
        {
        }

        public override void Calculate(int index)
        {
            for (int i = Period; i > 0; i--)
            {
                sum += Math.Abs(Bars.Last(i).Close - Bars.Last(i).Open);
            }
            Result[index] = sum / Period / Symbol.PipSize;
            sum = 0;
        }
    }
}
