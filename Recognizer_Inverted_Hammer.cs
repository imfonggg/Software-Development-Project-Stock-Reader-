using System;
using System.Collections.Generic;

namespace Project2
{
    public class Recognizer_Inverted_Hammer : Recognizer
    {
        private const decimal MaxBodyToRangeRatio = 0.35m;        // small body
        private const decimal MinUpperShadowToBodyRatio = 2.0m;   // long upper shadow
        private const decimal MaxLowerShadowToRangeRatio = 0.20m; // small lower shadow
        private const decimal BodyNearLowTolerance = 0.15m;       // body near low

        public Recognizer_Inverted_Hammer() : base("Inverted Hammer", size: 1) { }

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

                var bodyToRange = SafeDiv(sc.bodyRange, sc.range);
                var upperToBody = SafeDiv(sc.upperTailRange, sc.bodyRange);
                var lowerToRange = SafeDiv(sc.lowerTailRange, sc.range);

                bool smallBody = bodyToRange <= MaxBodyToRangeRatio;
                bool longUpperShadow = upperToBody >= MinUpperShadowToBodyRatio;
                bool smallLowerShadow = lowerToRange <= MaxLowerShadowToRangeRatio;
                bool bodyNearLow = (sc.bottomOfBody - sc.low) <= (sc.range * BodyNearLowTolerance);

                bool isInvertedHammerShape = smallBody && longUpperShadow && smallLowerShadow && bodyNearLow;

                if (isInvertedHammerShape)
                {
                    bool bullish = sc.close > sc.open;
                    bool bearish = sc.close < sc.open;

                    sc.isInvertedHammer = true;
                    sc.isInvertedHammerBullish = bullish;
                    sc.isInvertedHammerBearish = bearish;

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