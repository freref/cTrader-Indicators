using System;
using cAlgo.API;
using cAlgo.API.Internals;
using cAlgo.API.Indicators;
using cAlgo.Indicators;

namespace cAlgo
{
    [Indicator(IsOverlay = true, TimeZone = TimeZones.UTC, AccessRights = AccessRights.None)]
    public class FRAMA1 : Indicator
    {
        [Parameter(DefaultValue = 7)]
        public double len { get; set; }
        [Parameter(DefaultValue = 4)]
        public double FC { get; set; }
        [Parameter(DefaultValue = 300)]
        public double SC { get; set; }

        public double len1;
        public double w;
        public double H1;
        public double L1;
        public double N1;
        public double H2;
        public int len1_int;
        public double L2;
        public double N2;
        public double H3;
        public double L3;
        public double N3;
        public double dimen1;
        public double dimen;
        public double dimen1back = 0;
        public double alpha1;
        public double oldalpha;
        public double oldN;
        public double N;
        public double alpha_;
        public double alpha;
        public double _out;
        public double outback = 0;

        [Output("Main")]
        public IndicatorDataSeries Result { get; set; }


        protected override void Initialize()
        {
            len1 = len / 2;
            w = Math.Log(2 / (SC + 1));
            len1_int = Convert.ToInt32(Math.Floor(len1));
        }

        public override void Calculate(int index)
        {
            H1 = 0;
            for (int i = 0; i < len1_int; i++)
            {
                if (H1 < Bars.Last(i).High)
                    H1 = Bars.Last(i).High;
            }

            L1 = H1;
            for (int i = 0; i < len1_int; i++)
            {
                if (L1 > Bars.Last(i).Low)
                    L1 = Bars.Last(i).Low;
            }

            N1 = (H1 - L1) / len1;

            H2 = 0;
            for (int i = 0; i < len; i++)
            {
                if (H2 < Bars.Last(len1_int + i).High)
                    H2 = Bars.Last(len1_int + i).High;
            }

            L2 = H2;
            for (int i = 0; i < len; i++)
            {
                if (L2 > Bars.Last(len1_int + i).Low)
                    L2 = Bars.Last(len1_int + i).Low;
            }

            N2 = (H2 - L2) / len1;

            H3 = 0;
            for (int i = 0; i < len; i++)
            {
                if (H3 < Bars.Last(i).High)
                    H3 = Bars.Last(i).High;
            }

            L3 = H3;
            for (int i = 0; i < len; i++)
            {
                if (L3 > Bars.Last(i).Low)
                    L3 = Bars.Last(i).Low;
            }

            N3 = (H3 - L3) / len;

            dimen1 = (Math.Log(N1 + N2) - Math.Log(N3)) / Math.Log(2);

            if (N1 > 0 && N2 > 0 && N3 > 0)
                dimen = dimen1;
            else
                dimen = dimen1back;

            dimen1back = dimen1;

            alpha1 = Math.Exp(w * (dimen - 1));
            oldalpha = alpha1 > 1 ? 1 : (alpha1 < 0.01 ? 0.01 : alpha1);
            oldN = (2 - oldalpha) / oldalpha;
            N = (((SC - FC) * (oldN - 1)) / (SC - 1)) + FC;
            alpha_ = 2 / (N + 1);
            alpha = alpha_ < 2 / (SC + 1) ? 2 / (SC + 1) : (alpha_ > 1 ? 1 : alpha_);

            _out = (1 - alpha) * outback + alpha * (Bars.LastBar.High + Bars.LastBar.Low) / 2;

            outback = _out;

            Result[index] = _out;
        }
    }
}
