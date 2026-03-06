using System;

namespace Project2
{
    public class smartCandleStick : aCandlestick
    {
        public bool isBullish;
        public bool isBearish;
        public bool isNeutral;

        public decimal range;
        public decimal bodyRange;
        public decimal upperTailRange;
        public decimal lowerTailRange;
        public decimal topOfBody;
        public decimal bottomOfBody;

        // 1-candle pattern flags
        public bool isDoji;
        public bool isDragonflyDoji;
        public bool isGravestoneDoji;
        public bool isMarubozu;
        public bool isMarubozuBullish;
        public bool isMarubozuBearish;
        public bool isHammer;
        public bool isHammerBullish;
        public bool isHammerBearish;
        public bool isInvertedHammer;
        public bool isInvertedHammerBullish;
        public bool isInvertedHammerBearish;

        // 2-candle pattern flags
        public bool isHarami;
        public bool isEngulfing;

        public smartCandleStick() : base() { }
        public smartCandleStick(aCandlestick cs) : base(cs) { }
        public smartCandleStick(DateTime date, decimal open, decimal high, decimal low, decimal close, decimal volume) : base(date, open, high, low, close, volume) { }
        public smartCandleStick(string line) : base(line) { }
        public smartCandleStick(smartCandleStick scs) : base(scs) { }

        public void computeProperties()
        {
            range = high - low;
            bodyRange = Math.Abs(close - open);
            topOfBody = Math.Max(open, close);
            bottomOfBody = Math.Min(open, close);
            upperTailRange = range > 0m ? (high - topOfBody) : 0m;
            lowerTailRange = range > 0m ? (bottomOfBody - low) : 0m;
        }
    }
}
