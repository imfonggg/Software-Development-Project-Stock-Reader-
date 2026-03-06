using System;
using System.Collections.Generic;

namespace Project2
{
    public class Recognizer_Harami : Recognizer
    {
        private const decimal MinPrevBodyToRangeRatio = 0.40m;
        private const decimal MaxCurrBodyToPrevBodyRatio = 0.60m;
        private const decimal InsideToleranceRatio = 0.001m;

        public Recognizer_Harami() : base("Harami", size: 2) { }

        public override bool recognize(List<smartCandleStick> smartCandleSticks)
        {
            ClearMatches();
            if (smartCandleSticks == null || smartCandleSticks.Count < 2) return false;

            bool found = false;

            for (int i = 1; i < smartCandleSticks.Count; i++)
            {
                var prev = smartCandleSticks[i - 1];
                var curr = smartCandleSticks[i];

                prev.computeProperties();
                curr.computeProperties();

                if (prev.range <= 0m || curr.range <= 0m) continue;

                var prevBodyToRange = SafeDiv(prev.bodyRange, prev.range);
                var currBodyToPrevBody = SafeDiv(curr.bodyRange, prev.bodyRange);

                if (prevBodyToRange < MinPrevBodyToRangeRatio) continue;
                if (currBodyToPrevBody > MaxCurrBodyToPrevBodyRatio) continue;

                decimal tol = prev.range * InsideToleranceRatio;

                bool bodyInside =
                    (curr.topOfBody <= prev.topOfBody + tol) &&
                    (curr.bottomOfBody >= prev.bottomOfBody - tol);

                if (!bodyInside) continue;

                bool bullishHarami = (prev.close < prev.open) && (curr.close > curr.open);
                bool bearishHarami = (prev.close > prev.open) && (curr.close < curr.open);

                if (bullishHarami)
                {
                    curr.isBullish = true;
                    curr.isBearish = false;
                    curr.isNeutral = false;
                    curr.isHarami = true;
                    AddBullishMatch(i);
                    AddMatch(i);
                    found = true;
                }
                else if (bearishHarami)
                {
                    curr.isBullish = false;
                    curr.isBearish = true;
                    curr.isNeutral = false;
                    curr.isHarami = true;
                    AddBearishMatch(i);
                    AddMatch(i);
                    found = true;
                }
            }

            return found;
        }

        private static decimal SafeDiv(decimal n, decimal d) => d == 0m ? 0m : n / d;
    }
}
