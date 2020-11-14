using System;
using cAlgo.API;
using cAlgo.API.Internals;
using cAlgo.API.Indicators;
using cAlgo.Indicators;

namespace cAlgo
{
    [Indicator(IsOverlay = true, TimeZone = TimeZones.UTC, AccessRights = AccessRights.None)]
    public class NewIndicator : Indicator
    {

        public double num1;
        public double num2;
        public double num3;
        public double num4;
        public double num5;
        public double num6;

        public double low = 0;
        public double high = 0;

        protected override void Initialize()
        {

        }

        public override void Calculate(int index)
        {
            if (Symbol.Ask < low || Symbol.Ask > high)
            {
                high = Symbol.Ask + Symbol.PipSize * 10000;
                for (double level = Symbol.Ask; level <= high; level += Symbol.PipSize / 10)
                {
                    level = Math.Round(level, 5);
                    if (check(level))
                        ChartObjects.DrawHorizontalLine("line_" + level, level, Colors.Gray);
                }

                low = Symbol.Ask - Symbol.PipSize * 10000;
                for (double level = Symbol.Ask; level >= low; level -= Symbol.PipSize / 10)
                {
                    level = Math.Round(level, 5);
                    if (check(level))
                        ChartObjects.DrawHorizontalLine("line_" + level, level, Colors.Gray);
                }
            }
        }

        public bool check(double level)
        {
            num1 = Math.Floor(level);
            num2 = Math.Floor(level * 10 - num1 * 10);
            num3 = Math.Floor(level * 100 - num1 * 100 - num2 * 10);
            num4 = Math.Floor(level * 1000 - num1 * 1000 - num2 * 100 - num3 * 10);
            num5 = Math.Floor(level * 10000 - num1 * 10000 - num2 * 1000 - num3 * 100 - num4 * 10);
            num6 = Math.Floor(level * 100000 - num1 * 100000 - num2 * 10000 - num3 * 1000 - num4 * 100 - num5 * 10);


            if ((num1 + num2 + num3 + num4 + num5 + num6 == 3 || num1 + num2 + num3 + num4 + num5 + num6 == 6 || num1 + num2 + num3 + num4 + num5 + num6 == 9))
            {
                return true;
            }
            else
                return false;
        }
    }
}
