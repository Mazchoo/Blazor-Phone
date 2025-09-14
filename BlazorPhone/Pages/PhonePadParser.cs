using System.Text;

namespace BlazorPhone.Pages
{
    public class PhonePadParser
    {
        public static ReadOnlySpan<string> MKeyPadMapping => new[]
        {
            " ", "&'()", "ABC", "DEF", "GHI", "JKL", "MNO", "PQRS", "TUV", "WXYZ"
        };


        /// <summary>
        /// Implementation Detail - edits pressedKeys in place
        /// Effectively removes all backspace characters, characters preceding a backspace
        /// and invalid characters from the given StringBuilder in place.
        /// e.g.
        /// "12*" => 2, ignore first two characters in "121"
        /// "AB1" => 2, ignore first two characters in "AB1"
        /// "***" => 3, ignore everything in "***"
        /// </summary>
        /// <returns>The number of characters to ignore at the start of the stringBuilder</returns>
        static protected int MoveValidCharactersToEndOfString(StringBuilder pressedKeys) {
            int nrCharactersIgnored = 0;
            int currentEraseAmount = 0;

            for (int i = pressedKeys.Length - 1; i >= 0; i--)
            {
                char pressedChar = pressedKeys[i];
                if (pressedChar == '*') {
                    currentEraseAmount += 1;
                    nrCharactersIgnored += 1;  // Both * and the next character ignored
                    continue;
                }

                if (currentEraseAmount > 0) {
                    currentEraseAmount -= 1;
                    nrCharactersIgnored += 1;
                    continue;
                }

                if (char.IsDigit(pressedChar) || pressedChar == ' ') {
                    pressedKeys[i + nrCharactersIgnored] = pressedChar;
                } else {
                    nrCharactersIgnored += 1;
                }
            }
            return nrCharactersIgnored;
        }

        /// <summary>
        /// Implementation Detail
        /// Transforms a numerical digit [pressed and the number of times pressed to a character
        /// e.g.
        /// '2', 2 => 'B' the second letter of ABC
        /// '3', 1 => 'D' the second letter of DEF
        /// '#', 3 => ' ' unknown character goes to blank
        /// </summary>
        /// <returns>The number of characters to ignore at the start of the stringBuilder</returns>
        static protected char ParseCharAndAmountToLetter(char pressedChar, int nrTimesPressed)  {
            if (!char.IsDigit(pressedChar) || nrTimesPressed < 1) return ' ';  // Not expected
            String lookupString = MKeyPadMapping[pressedChar - '0'];
            return lookupString[(nrTimesPressed - 1) % lookupString.Length];
        }

        /// <summary>
        /// Edits pressedKeys in place
        /// Returns a parsed message of letters from numerical keys that were pressed
        /// e.g.
        /// </summary>
        /// <returns>The transformed input showing the message</returns>
        static public StringBuilder ParsePressedKeysToLetters(StringBuilder pressedKeys)  {
            int ignoredAtStart = MoveValidCharactersToEndOfString(pressedKeys);
            int nrParsedChars = 0;

            char currentChar = ' ';
            int nrTimesPressed = 0;
            for (int i = ignoredAtStart; i < pressedKeys.Length; i++) {
                char nextChar = pressedKeys[i];

                if (nextChar != currentChar && currentChar != ' ') {
                    // Push to output
                    pressedKeys[nrParsedChars++] = ParseCharAndAmountToLetter(currentChar, nrTimesPressed);
                    nrTimesPressed = 0;
                }

                if (nextChar != ' ') nrTimesPressed++;
                currentChar = nextChar;
            }

            if (currentChar != ' ')
            {
                // Push last char to output
                pressedKeys[nrParsedChars++] = ParseCharAndAmountToLetter(currentChar, nrTimesPressed);
            }

            pressedKeys.Length = nrParsedChars;
            return pressedKeys;
        }
    }
}