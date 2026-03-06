using System;
using System.Collections.Generic;

namespace Project2
{
    public class Recognizer_Hammer : Recognizer
    {
        private const decimal MaxBodyToRangeRatio = 0.35m;       // small body
        private const decimal MinLowerShadowToBodyRatio = 2.0m;  // long lower shadow
        private const decimal MaxUpperShadowToRangeRatio = 0.20m;// small upper shadow
        private const decimal BodyNearHighTolerance = 0.15m;     // body near high

        public Recognizer_Hammer() : base("Hammer", size: 1) { }

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
                var lowerToBody = SafeDiv(sc.lowerTailRange, sc.bodyRange);
                var upperToRange = SafeDiv(sc.upperTailRange, sc.range);

                bool smallBody = bodyToRange <= MaxBodyToRangeRatio;
                bool longLowerShadow = lowerToBody >= MinLowerShadowToBodyRatio;
                bool smallUpperShadow = upperToRange <= MaxUpperShadowToRangeRatio;
                bool bodyNearHigh = (sc.high - sc.topOfBody) <= (sc.range * BodyNearHighTolerance);

                bool isHammerShape = smallBody && longLowerShadow && smallUpperShadow && bodyNearHigh;

                if (isHammerShape)
                {
                    bool bullish = sc.close > sc.open;
                    bool bearish = sc.close < sc.open;

                    sc.isHammer = true;
                    sc.isHammerBullish = bullish;
                    sc.isHammerBearish = bearish;

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