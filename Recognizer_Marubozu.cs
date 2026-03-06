using System;
using System.Collections.Generic;

namespace Project2
{
    public class Recognizer_Marubozu : Recognizer
    {
        private const decimal MinBodyToRangeRatio = 0.90m;   // body ~ full range
        private const decimal MaxTailToRangeRatio = 0.05m;   // near-zero shadows

        public Recognizer_Marubozu() : base("Marubozu", size: 1) { }

        public override bool recognize(List<smartCandleStick> smartCandleSticks)
        {
            ClearMatches();
            if (smartCandleSticks == null || smartCandleSticks.Count == 0) return false;

            bool found = false;
            for (int i = 0; i < smartCandleSticks.Count; i++)
            {
                var sc = smartCandleSticks[i];
                sc.computeProperties();
                if (sc.range <= 0m) continue;

                var bodyRatio = SafeDiv(sc.bodyRange, sc.range);
                var upperRatio = SafeDiv(sc.upperTailRange, sc.range);
                var lowerRatio = SafeDiv(sc.lowerTailRange, sc.range);

                bool isMarubozuShape = bodyRatio >= MinBodyToRangeRatio &&
                                       upperRatio <= MaxTailToRangeRatio &&
                                       lowerRatio <= MaxTailToRangeRatio;

                if (isMarubozuShape)
                {
                    bool bullish = sc.close > sc.open;
                    bool bearish = sc.close < sc.open;

                    sc.isMarubozu = true;
                    sc.isMarubozuBullish = bullish;
                    sc.isMarubozuBearish = bearish;

                    sc.isBullish = bullish;
                    sc.isBearish = bearish;
                    sc.isNeutral = !bullish && !bearish;

                    AddMatch(i);
                    found = true;
                }
            }
            return found;
        }

        private static decimal SafeDiv(decimal n, decimal d) => d == 0m ? 0m : n / d;
    }
}