using System;
using cAlgo.API;
using cAlgo.API.Internals;
using cAlgo.API.Indicators;
using cAlgo.Indicators;

namespace cAlgo
{
    [Indicator(IsOverlay = true, TimeZone = TimeZones.UTC, AccessRights = AccessRights.None)]
    public class nAMA : Indicator
    {
        [Output("Main")]
        public IndicatorDataSeries Result { get; set; }

        [Parameter("Length", DefaultValue = 10)]
        public int Length { get; set; }
        [Parameter("Fastend", DefaultValue = 2)]
        public double Fastend { get; set; }
        [Parameter("Slowend", DefaultValue = 30)]
        public double Slowend { get; set; }

        public double nfastend;
        public double nslowend;
        public double xvnoise;
        public double nsignal;
        public double nnoise = 0;
        public double nefratio;
        public double nsmooth;
        public double nz = 0;


        protected override void Initialize()
        {
            nfastend = 2 / (Fastend + 1);
        }

        public override void Calculate(int index)
        {
            xvnoise = Math.Abs(Bars.Last(0).Close - Bars.Last(1).Close);
            nsignal = Math.Abs(Bars.Last(0).Close - Bars.Last(Length).Close);

            nnoise = 0;
            for (int i = 0; i < Length; i++)
            {
                nnoise += Math.Abs(Bars.Last(i).Close - Bars.Last(i + 1).Close);
            }

            if (nnoise != 0)
                nefratio = nsignal / nnoise;
            else
                nefratio = 0;

            nsmooth = Math.Pow(nefratio * (nfastend - nslowend) + nslowend, 2);

            Result[index] = nz + nsmooth * (Bars.Last(0).Close - nz);

            nz = Result[index];
        }
    }
}
