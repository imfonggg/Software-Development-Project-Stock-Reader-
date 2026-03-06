using System;
using System.Collections.Generic;

namespace Project2
{
    // Engulfing Pattern (Bullish/Bearish) over 2 candles
    public class Recognizer_EP : Recognizer
    {
        private const decimal MinBodyRatio = 0.25m; // require some meaningful bodies

        public Recognizer_EP() : base("Engulfing", size: 2) { }

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

                var prevBodyRatio = SafeDiv(prev.bodyRange, prev.range);
                var currBodyRatio = SafeDiv(curr.bodyRange, curr.range);

                if (prevBodyRatio < MinBodyRatio || currBodyRatio < MinBodyRatio) continue;

                bool prevBearish = prev.close < prev.open;
                bool prevBullish = prev.close > prev.open;
                bool currBullish = curr.close > curr.open;
                bool currBearish = curr.close < curr.open;

                // Bodies (not shadows) engulf: current body fully covers previous body
                bool bodyEngulfs =
                    curr.topOfBody >= prev.topOfBody &&
                    curr.bottomOfBody <= prev.bottomOfBody;

                bool bullishEngulfing = prevBearish && currBullish && bodyEngulfs;
                bool bearishEngulfing = prevBullish && currBearish && bodyEngulfs;

                if (bullishEngulfing)
                {
                    curr.isBullish = true;
                    curr.isBearish = false;
                    curr.isNeutral = false;
                    curr.isEngulfing = true;
                    AddBullishMatch(i);
                    AddMatch(i);
                    found = true;
                }
                else if (bearishEngulfing)
                {
                    curr.isBullish = false;
                    curr.isBearish = true;
                    curr.isNeutral = false;
                    curr.isEngulfing = true;
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