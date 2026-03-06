using System;
using System.Collections.Generic;

namespace Project2
{
    public abstract class Recognizer
    {
        protected int patternSize;
        protected string patternName;

        // Existing bucket: "pattern itself" (union of bullish/bearish)
        protected readonly List<int> patternIndexes = new List<int>();

        // New buckets: explicit bullish and bearish matches
        protected readonly List<int> bullishPatternIndexes = new List<int>();
        protected readonly List<int> bearishPatternIndexes = new List<int>();

        protected Recognizer(string name, int size = 1)
        {
            patternName = name;
            patternSize = size;
        }

        public abstract bool recognize(List<smartCandleStick> smartCandleSticks);

        // "Pattern itself" (union)
        protected void AddMatch(int index)
        {
            if (index >= 0) patternIndexes.Add(index);
        }

        // Explicit versions
        protected void AddBullishMatch(int index)
        {
            if (index >= 0) bullishPatternIndexes.Add(index);
            AddMatch(index); // also counts toward "pattern itself"
        }

        protected void AddBearishMatch(int index)
        {
            if (index >= 0) bearishPatternIndexes.Add(index);
            AddMatch(index); // also counts toward "pattern itself"
        }

        public IReadOnlyList<int> GetMatches() => patternIndexes.AsReadOnly();
        public IReadOnlyList<int> GetBullishMatches() => bullishPatternIndexes.AsReadOnly();
        public IReadOnlyList<int> GetBearishMatches() => bearishPatternIndexes.AsReadOnly();

        public void ClearMatches()
        {
            patternIndexes.Clear();
            bullishPatternIndexes.Clear();
            bearishPatternIndexes.Clear();
        }

        public string Name => patternName;
        public int Size => patternSize;
    }
}