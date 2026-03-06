using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2
{
    public class aCandlestick
    {

        public DateTime date { get; set; }
        public decimal open { get; set; }
        public decimal high { get; set; }
        public decimal low { get; set; }
        public decimal close { get; set; }
        public decimal volume { get; set; }

        // Delimiters for candlestick string (kept for compatibility)
        private static char[] delimeters = { ',' };

        /// <summary>
        /// Default constructor
        /// </summary>
        public aCandlestick() { }

        /// <summary>
        /// A constructor that initializes all fields based on the provided parameters.
        /// </summary>
        /// <param name="date"></param>
        /// <param name="open"></param>
        /// <param name="high"></param>
        /// <param name="low"></param>
        /// <param name="close"></param>
        /// <param name="volume"></param>
        public aCandlestick(DateTime date, decimal open, decimal high, decimal low, decimal close, decimal volume)
        {
            // Initialize fields with the provided parameters
            this.date = date;
            this.open = open;
            this.high = high;
            this.low = low;
            this.close = close;
            this.volume = volume;
        }

        /// <summary>
        /// A constructor that creates a copy of another aCandlestick instance.
        /// </summary>
        /// <param name="otherCandleStick"></param>
        public aCandlestick(aCandlestick otherCandleStick)
        {
            // Copy values from the other instance
            this.date = otherCandleStick.date;
            this.open = otherCandleStick.open;
            this.high = otherCandleStick.high;
            this.low = otherCandleStick.low;
            this.close = otherCandleStick.close;
            this.volume = otherCandleStick.volume;
        }

        /// <summary>
        /// A constructor that parses through a string line and initializes the fields to form a candlestick.
        /// </summary>
        /// <param name="line"></param>
        /// <exception cref="FormatException"></exception>
        public aCandlestick(string line)
        {
            // Try to parse the line
            aCandlestick parsed;
            if (!TryParse(line, out parsed))
                throw new FormatException($"Invalid candlestick line: '{line}'");

            // Assign parsed values to the current instance
            this.date = parsed.date;
            this.open = parsed.open;
            this.high = parsed.high;
            this.low = parsed.low;
            this.close = parsed.close;
            this.volume = parsed.volume;
        }

        /// <summary>
        /// Attempts to parse a string line into a aCandlestick object.
        /// </summary>
        /// <param name="line"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static bool TryParse(string line, out aCandlestick result)
        {
            // Initialize result to null
            result = null;
            if (string.IsNullOrWhiteSpace(line)) return false;

            // Split the line into tokens
            var tokens = line.Split(delimeters, StringSplitOptions.None).Select(t => t.Trim().Trim('"')).ToList();

            // Need at least 6 tokens: date, open, high, low, close, volume. If not return false
            if (tokens.Count < 6) return false;

            // Find the date token (could be in different positions)
            var formats = new[] { "yyyy-MM-dd" };
            var culture = CultureInfo.InvariantCulture;
            DateTime d = default;
            int dateIndex = -1;


            // Try to parse the first three tokens as date
            for (int i = 0; i < Math.Min(3, tokens.Count); i++)
            {
                if (DateTime.TryParseExact(tokens[i], formats, culture, DateTimeStyles.None, out d) ||
                    DateTime.TryParse(tokens[i], culture, DateTimeStyles.None, out d))
                {
                    dateIndex = i;
                    break;
                }
            }
            // If no valid date found, return false
            if (dateIndex < 0) return false;

            // Ensure there are enough tokens for OHLC
            if (tokens.Count <= dateIndex + 4) return false;

            // Parse OHLC values
            decimal o, h, l, c;
            if (!TryParseDecimal(tokens[dateIndex + 1], out o)) return false;
            if (!TryParseDecimal(tokens[dateIndex + 2], out h)) return false;
            if (!TryParseDecimal(tokens[dateIndex + 3], out l)) return false;
            if (!TryParseDecimal(tokens[dateIndex + 4], out c)) return false;

            // Parse volume value (search backwards from the end)
            ulong v = 0;
            bool volFound = false;
            for (int i = tokens.Count - 1; i > dateIndex + 4; i--)
            {
                var t = tokens[i];

                // Try to parse volume
                if (TryParseDecimal(t, out var vd) && vd >= 0)
                {
                    v = (ulong)vd;
                    volFound = true;
                    break;
                }
            }
            if (!volFound)
            {
                // If no volume found in the trailing tokens, try the token right after close
                if (tokens.Count > dateIndex + 5)
                {
                    if (TryParseDecimal(tokens[dateIndex + 5], out var vd) && vd >= 0) v = (ulong)vd;
                    else return false;
                }
            }

            // Create the candlestick object
            result = new aCandlestick(d, o, h, l, c, v);
            return true;
        }

        /// <summary>
        /// Try to convert a string to a decimal.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private static bool TryParseDecimal(string s, out decimal value)
        {
            return decimal.TryParse(s, NumberStyles.Number | NumberStyles.AllowThousands | NumberStyles.AllowExponent, CultureInfo.InvariantCulture, out value);
        }
    }
}

