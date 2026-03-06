using System;
using System.Collections.Generic;

namespace Project2
{
    public class Recognizer_Doji : Recognizer
    {
        private const decimal MaxBodyToRangeRatio = 0.10m;   // classic doji: tiny body
        private const decimal TailDominanceRatio = 0.35m;    // tails dominate for dragonfly/gravestone
        private const decimal MinRange = 0.0000001m;

        public Recognizer_Doji() : base("Doji", size: 1) { }

        public override bool recognize(List<smartCandleStick> smartCandleSticks)
        {
            ClearMatches();
            if (smartCandleSticks == null || smartCandleSticks.Count == 0) return false;

            bool found = false;
            for (int i = 0; i < smartCandleSticks.Count; i++)
            {
                var sc = smartCandleSticks[i];
                sc.computeProperties();
                if (sc.range <= MinRange) continue;

                var bodyRatio = SafeDiv(sc.bodyRange, sc.range);
                var upperRatio = SafeDiv(sc.upperTailRange, sc.range);
                var lowerRatio = SafeDiv(sc.lowerTailRange, sc.range);

                bool classicDoji = bodyRatio <= MaxBodyToRangeRatio;
                bool dragonflyDoji = classicDoji && lowerRatio >= TailDominanceRatio && upperRatio <= (MaxBodyToRangeRatio * 1.5m);
                bool gravestoneDoji = classicDoji && upperRatio >= TailDominanceRatio && lowerRatio <= (MaxBodyToRangeRatio * 1.5m);

                if (classicDoji)
                {
                    sc.isDoji = true;
                    sc.isDragonflyDoji = dragonflyDoji;
                    sc.isGravestoneDoji = gravestoneDoji;
                    sc.isBullish = false;
                    sc.isBearish = false;
                    sc.isNeutral = true;

                    AddMatch(i);
                    found = true;
                }
            }
            return found;
        }

        private static decimal SafeDiv(decimal n, decimal d) => d == 0m ? 0m : n / d;
    }
}
