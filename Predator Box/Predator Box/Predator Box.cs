using System;
using System.Collections.Generic;
using cAlgo.API;
using cAlgo.API.Internals;
using cAlgo.API.Indicators;
using cAlgo.Indicators;

namespace cAlgo
{
    [Indicator(IsOverlay = true, TimeZone = TimeZones.UTC, AccessRights = AccessRights.None)]
    public class PredatorBox : Indicator
    {

        public string asia = "06:00";

        public int asiaHour = 1;

        [Parameter("Color", DefaultValue = "MediumPurple")]
        public string lineColor { get; set; }

        [Parameter("Linestyle", DefaultValue = "Solid")]
        public LineStyle LineStyle { get; set; }

        [Parameter("Thickness", DefaultValue = "1", MinValue = 1)]
        public int Thickness { get; set; }


        // store last Session Start and Session End
        public DateTime asStart;
        public DateTime asEnd;
        public Color colour;

        protected override void Initialize()
        {
            // Initialize and create nested indicators
        }

        public override void Calculate(int index)
        {
            var dateTime = Bars.OpenTimes[index];
            List<Box> boxs = sbox(index);
            for (int i = 0; i < boxs.Count; i++)
            {
                var box = boxs[i];
                double[] high_low = boxHighLow(index, box);
                drawBox(box.label, box.left, box.right, high_low[0], high_low[1], box.clr);
            }

        }

        // box calcuate logic
        public List<Box> sbox(int index)
        {
            List<Box> boxs = new List<Box>();
            DateTime current = Bars.OpenTimes[index];
            string asStartHour = asia.Split('-')[0].Split(':')[0];
            string asStartMinute = asia.Split('-')[0].Split(':')[1];
            colour = Color.FromName(lineColor);


            if (current.Hour == Int32.Parse(asStartHour) && current.Minute == Int32.Parse(asStartMinute))
            {
                asStart = current;
                asEnd = current.AddHours(asiaHour);
            }


            if (current >= asStart && current <= asEnd)
            {
                boxs.Add(new Box(asStart.ToString(), asStart, asEnd, colour));
            }

            return boxs;



        }

        // calculate session High Low
        private double[] boxHighLow(int index, Box box)
        {
            DateTime left = box.left;
            double[] high_low = new double[2] 
            {
                Bars.HighPrices[index],
                Bars.LowPrices[index]
            };
            while (Bars.OpenTimes[index] >= left)
            {
                high_low[0] = Math.Max(high_low[0], Bars.HighPrices[index]);
                high_low[1] = Math.Min(high_low[1], Bars.LowPrices[index]);
                index--;
            }
            return high_low;
        }

        // draw session box
        private void drawBox(String label, DateTime left, DateTime right, Double high, Double low, Color clr)
        {
            Chart.DrawTrendLine(label + "_low", left, low, right.AddHours(5), low, clr, Thickness, LineStyle);
            Chart.DrawTrendLine(label + "_high", left, high, right.AddHours(5), high, clr, Thickness, LineStyle);
            Chart.DrawTrendLine(label + "_left", left, high, left, low, clr, Thickness, LineStyle);
            Chart.DrawTrendLine(label + "_right", right, high, right, low, clr, Thickness, LineStyle);
        }

        // box data struct
        public struct Box
        {
            public string label;
            public DateTime left;
            public DateTime right;
            public Color clr;

            public Box(string label, DateTime left, DateTime right, Color clr)
            {
                this.label = label;
                this.left = left;
                this.right = right;
                this.clr = clr;
            }
        }
    }
}
