using System.Text;

namespace BlazorPhone.Pages
{
    public class PhonePadParser
    {
        /// <summary>
        /// Effectively removes all backspace characters, characters preceding a backspace
        /// and invalid characters from the given StringBuilder in place.
        /// e.g.
        /// "12*" => 2, ignore first two characters in "121"
        /// "AB1" => 2, ignore first two characters in "AB1"
        /// "***" => 3, ignore everything in "***"
        /// </summary>
        /// <param name="pressedKeys">The StringBuilder containing characters to process.</param>
        /// <returns>The number of characters to ignore at the start of the stringBuilder</returns>
        static protected int MoveValidCharactersToEndOfString(StringBuilder pressedKeys) {
            int nrCharactersIgnored = 0;
            int currentEraseAmount = 0;

            for (int i = pressedKeys.Length - 1; i >= 0; i--)
            {
                char c = pressedKeys[i];
                if (c == '*') {
                    currentEraseAmount += 1;
                    nrCharactersIgnored += 1;  // Both * and the next character ignored
                    continue;
                }

                if (currentEraseAmount > 0) {
                    currentEraseAmount -= 1;
                    nrCharactersIgnored += 1;
                    continue;
                }

                if (char.IsDigit(c) || c == ' ') {
                    pressedKeys[i + nrCharactersIgnored] = c;
                } else {
                    nrCharactersIgnored += 1;
                }
            }
            return nrCharactersIgnored;
        }
    }
}