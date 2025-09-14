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
        /// Zeros are converted to spaces
        /// e.g.
        /// "12*" => 2, ignore first two characters in "121"
        /// "AB1" => 2, ignore first two characters in "AB1"
        /// "***" => 3, ignore everything in "***"
        /// "77*4" => 3, ignore 77 and * in "77*4"
        /// </summary>
        /// <returns>The number of characters to ignore at the start of the stringBuilder</returns>
        static protected int MoveValidCharactersToEndOfString(StringBuilder pressedKeys) {
            int nrCharactersIgnored = 0;
            int currentEraseAmount = 0;
            char previousChar = ' ';

            for (int i = pressedKeys.Length - 1; i >= 0; i--)
            {
                char pressedChar = pressedKeys[i];
                if (pressedChar == '0') pressedChar = ' ';

                if (currentEraseAmount > 0 && previousChar != pressedChar && previousChar != '*')
                {
                    currentEraseAmount -= 1; // Stop current erase
                }

                if (pressedChar == '*') {
                    currentEraseAmount += 1;
                    nrCharactersIgnored += 1;  // * is ignored
                    previousChar = pressedChar;
                    continue;
                }

                if (currentEraseAmount > 0) {
                    // If delete not stopped yet ignore current character
                    previousChar = pressedChar;
                    nrCharactersIgnored += 1;
                    continue;
                }

                // Append character to end of buffer
                if (char.IsDigit(pressedChar) || pressedChar == ' ') {
                    pressedKeys[i + nrCharactersIgnored] = pressedChar;
                } else {
                    nrCharactersIgnored += 1;
                }

                previousChar = pressedChar;
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
        /// Turns pressedKeys into letters from numerical phone keys according to MKeyPadMapping
        /// * Will delete the last complete instuction
        /// 0 Must be occur in sequence n + 1 times to get n spaces
        /// e.g.
        /// "22202022" -> "CAB"
        /// "222*02022" -> "AB"
        /// "200" -> "A "
        /// </summary>
        /// <returns>The transformed input showing the parsed message</returns>
        static public void ParsePressedKeysToLetters(StringBuilder pressedKeys)  {
            int ignoredAtStart = MoveValidCharactersToEndOfString(pressedKeys);
            int nrParsedChars = 0;

            char prevChar = ' ';
            int nrTimesPressed = 0;
            for (int i = ignoredAtStart; i < pressedKeys.Length; i++) {
                char currentChar = pressedKeys[i];

                if (currentChar != prevChar && prevChar != ' ') {
                    // Push to output
                    pressedKeys[nrParsedChars++] = ParseCharAndAmountToLetter(prevChar, nrTimesPressed);
                    nrTimesPressed = 0;
                } else if (currentChar == ' ' && currentChar == prevChar && i != ignoredAtStart) {
                    pressedKeys[nrParsedChars++] = ' ';
                }

                if (currentChar != ' ') nrTimesPressed++;
                prevChar = currentChar;
            }

            if (prevChar != ' ')
            {
                // Push last char to output
                pressedKeys[nrParsedChars++] = ParseCharAndAmountToLetter(prevChar, nrTimesPressed);
            }

            pressedKeys.Length = nrParsedChars;
        }
    }
}