using System;
using cAlgo.API;
using cAlgo.API.Internals;
using cAlgo.API.Indicators;
using cAlgo.Indicators;

namespace cAlgo
{
    [Indicator(IsOverlay = false, TimeZone = TimeZones.UTC, AccessRights = AccessRights.None)]
    public class nnoise : Indicator
    {
        [Parameter(DefaultValue = 10)]
        public int Length { get; set; }

        public double output;

        [Output("Main")]
        public IndicatorDataSeries Result { get; set; }


        protected override void Initialize()
        {
            // Initialize and create nested indicators
        }

        public override void Calculate(int index)
        {
            output = 0.0;
            for (int i = 1; i <= Length; i++)
            {
                output += Math.Abs(Bars.Last(i).Close - Bars.Last(i + 1).Close);
            }
            Result[index] = output;
        }
    }
}
