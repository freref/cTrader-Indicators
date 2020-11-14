using System;
using cAlgo.API;
using cAlgo.API.Internals;
using cAlgo.API.Indicators;
using cAlgo.Indicators;

namespace cAlgo
{
    [Indicator(IsOverlay = true, TimeZone = TimeZones.UTC, AccessRights = AccessRights.None)]
    public class Quantum : Indicator
    {
        [Parameter(DefaultValue = 300)]
        public int BarsBack { get; set; }

        public Bars bars;

        protected override void Initialize()
        {
            // Initialize and create nested indicators
        }

        public override void Calculate(int index)
        {
            if (IsLower(index))
                Chart.DrawIcon("Lower" + index, ChartIconType.UpArrow, Bars[index].OpenTime, Bars[index].Low, Color.Green);

            if (IsHigher(index))
                Chart.DrawIcon("Higher" + index, ChartIconType.DownArrow, Bars[index].OpenTime, Bars[index].High, Color.Red);
        }

        public bool IsLower(int index)
        {
            bool isLower = true;

            for (int i = 1; i <= BarsBack; i++)
            {
                if (Bars[index].Low > Bars[index - i].Low)
                    isLower = false;
            }
            return isLower;
        }

        public bool IsHigher(int index)
        {
            bool isHigher = true;

            for (int i = 1; i <= BarsBack; i++)
            {
                if (Bars[index].High < Bars[index - i].High)
                    isHigher = false;
            }
            return isHigher;
        }

    }
}
