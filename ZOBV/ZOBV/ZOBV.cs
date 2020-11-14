using System;
using cAlgo.API;
using cAlgo.API.Internals;
using cAlgo.API.Indicators;
using cAlgo.Indicators;

namespace cAlgo
{
    [Indicator(IsOverlay = false, TimeZone = TimeZones.UTC, AccessRights = AccessRights.None)]
    public class ZOBV : Indicator
    {
        [Parameter(DefaultValue = 200)]
        public int Period { get; set; }
        [Parameter(DefaultValue = MovingAverageType.Exponential)]
        public MovingAverageType MAType { get; set; }

        [Output("OBV")]
        public IndicatorDataSeries OBV { get; set; }
        [Output("MA")]
        public IndicatorDataSeries MA { get; set; }

        public OnBalanceVolume obv;
        public MovingAverage ma;

        protected override void Initialize()
        {

            obv = Indicators.OnBalanceVolume(Bars.ClosePrices);
            ma = Indicators.MovingAverage(OBV, Period, MAType);
        }

        public override void Calculate(int index)
        {
            OBV[index] = obv.Result[index];
            MA[index] = ma.Result[index];
        }
    }
}
