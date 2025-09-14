using System.Text;
using Xunit;
using BlazorPhone.Pages;

namespace BlazorPhone.Tests
{
    public class PhonePadParserTests: PhonePadParser
    {
        [Fact]
        public void MoveValidCharactersToEndOfString_ShouldHandleEmptyString()
        {
            // Arrange
            StringBuilder pressedKeys = new("");

            // Act
            int nrCharactersIgnored = PhonePadParser.MoveValidCharactersToEndOfString(pressedKeys);

            // Assert
            Assert.Equal(0, nrCharactersIgnored);
            Assert.Equal("", pressedKeys.ToString());
        }

        [Fact]
        public void MoveValidCharactersToEndOfString_ShouldIgnoreNothingWithValidCharacters()
        {
            // Arrange
            StringBuilder pressedKeys = new("12345 6789");

            // Act
            int nrCharactersIgnored = PhonePadParser.MoveValidCharactersToEndOfString(pressedKeys);

            // Assert
            Assert.Equal(0, nrCharactersIgnored);
            Assert.Equal("12345 6789", pressedKeys.ToString());
        }

        [Fact]
        public void MoveValidCharactersToEndOfString_ShouldIgnoreInvalidCharacters()
        {
            // Arrange
            StringBuilder pressedKeys = new("12345 AB 6789#");

            // Act
            int nrCharactersIgnored = PhonePadParser.MoveValidCharactersToEndOfString(pressedKeys);

            // Assert
            Assert.Equal(3, nrCharactersIgnored);
            Assert.Equal("12312345  6789", pressedKeys.ToString());
        }

        [Fact]
        public void MoveValidCharactersToEndOfString_ShouldDeleteAnyNonDeleteCharThatCameBefore()
        {
            // Arrange
            StringBuilder pressedKeys = new("12345**");

            // Act
            int nrCharactersIgnored = PhonePadParser.MoveValidCharactersToEndOfString(pressedKeys);

            // Assert
            Assert.Equal(4, nrCharactersIgnored);
            Assert.Equal("1234123", pressedKeys.ToString());
        }

        [Fact]
        public void MoveValidCharactersToEndOfString_EraseEverythingReturnsNothing()
        {
            // Arrange
            StringBuilder pressedKeys = new("***");

            // Act
            int nrCharactersIgnored = PhonePadParser.MoveValidCharactersToEndOfString(pressedKeys);

            // Assert
            Assert.Equal(3, nrCharactersIgnored);
            Assert.Equal("***", pressedKeys.ToString());
        }

        [Fact]
        public void MoveValidCharactersToEndOfString_ShouldDeleteAnythingThatCameBeforeHandleMixedString()
        {
            // Arrange
            StringBuilder pressedKeys = new("123 * * A*2#");

            // Act
            int nrCharactersIgnored = PhonePadParser.MoveValidCharactersToEndOfString(pressedKeys);

            // Assert
            Assert.Equal(7, nrCharactersIgnored);
            Assert.Equal("123 * *123 2", pressedKeys.ToString());
        }

        [Fact]
        public void ParseCharAndAmountToLetter_ShouldHandleInvalidCharacter()
        {
            // Arrange
            char c = '#';
            int nrTimesPressed = 1;

            // Act
            char parsedChar = PhonePadParser.ParseCharAndAmountToLetter(c, nrTimesPressed);

            // Assert
            Assert.Equal(' ', parsedChar);
        }

        [Fact]
        public void ParseCharAndAmountToLetter_ShouldHandleCharacterPressedZeroTimes()
        {
            // Arrange
            char c = '1';
            int nrTimesPressed = 0;

            // Act
            char parsedChar = PhonePadParser.ParseCharAndAmountToLetter(c, nrTimesPressed);

            // Assert
            Assert.Equal(' ', parsedChar);
        }

        [Fact]
        public void ParseCharAndAmountToLetter_ShouldReturnExpectedCharacterPressedOnce()
        {
            // Arrange
            char c = '2';
            int nrTimesPressed = 1;

            // Act
            char parsedChar = PhonePadParser.ParseCharAndAmountToLetter(c, nrTimesPressed);

            // Assert
            Assert.Equal('A', parsedChar);
        }

        [Fact]
        public void ParseCharAndAmountToLetter_ShouldReturnExpectedCharacterPressedTwice()
        {
            // Arrange
            char c = '3';
            int nrTimesPressed = 2;

            // Act
            char parsedChar = PhonePadParser.ParseCharAndAmountToLetter(c, nrTimesPressed);

            // Assert
            Assert.Equal('E', parsedChar);
        }

        [Fact]
        public void ParseCharAndAmountToLetter_ShouldReturnCycleWhenPressedManyTimes()
        {
            // Arrange
            char c = '9';
            int nrTimesPressed = 7;

            // Act
            char parsedChar = PhonePadParser.ParseCharAndAmountToLetter(c, nrTimesPressed);

            // Assert
            Assert.Equal('Y', parsedChar);
        }

        [Fact]
        public void ParseCharAndAmountToLetter_ShouldReturnSpaceOnZero()
        {
            // Arrange
            char c = '0';
            int nrTimesPressed = 1;

            // Act
            char parsedChar = PhonePadParser.ParseCharAndAmountToLetter(c, nrTimesPressed);

            // Assert
            Assert.Equal(' ', parsedChar);
        }


        [Fact]
        public void ParsePressedKeysToLetters_ShouldParsedSpacedString()
        {
            // Arrange
            StringBuilder pressedKeys = new("222 2 22");

            // Act
            PhonePadParser.ParsePressedKeysToLetters(pressedKeys);

            // Assert
            Assert.Equal("CAB", pressedKeys.ToString());
        }

        [Fact]
        public void ParsePressedKeysToLetters_ShouldIgnoreSpacesAtBeginningSpacedString()
        {
            // Arrange
            StringBuilder pressedKeys = new(" 222 2 22#");

            // Act
            PhonePadParser.ParsePressedKeysToLetters(pressedKeys);

            // Assert
            Assert.Equal("CAB", pressedKeys.ToString());
        }

        [Fact]
        public void ParsePressedKeysToLetters_ShouldProccessUnspacedLetters()
        {
            // Arrange
            StringBuilder pressedKeys = new("4433555 555666#");

            // Act
            PhonePadParser.ParsePressedKeysToLetters(pressedKeys);

            // Assert
            Assert.Equal("HELLO", pressedKeys.ToString());
        }

        [Fact]
        public void ParsePressedKeysToLetters_ShouldIgnoreInvalidCharacters()
        {
            // Arrange
            StringBuilder pressedKeys = new("44K33E555B 55A5666B#");

            // Act
            PhonePadParser.ParsePressedKeysToLetters(pressedKeys);

            // Assert
            Assert.Equal("HELLO", pressedKeys.ToString());
        }

        [Fact]
        public void ParsePressedKeysToLetters_ShouldProccessSingleUndo()
        {
            // Arrange
            StringBuilder pressedKeys = new("227*#");

            // Act
            PhonePadParser.ParsePressedKeysToLetters(pressedKeys);

            // Assert
            Assert.Equal("B", pressedKeys.ToString());
        }

        [Fact]
        public void ParsePressedKeysToLetters_ShouldProccessDoubleDelete()
        {
            // Arrange
            StringBuilder pressedKeys = new("2277**#");

            // Act
            PhonePadParser.ParsePressedKeysToLetters(pressedKeys);

            // Assert
            Assert.Equal("", pressedKeys.ToString());
        }

        [Fact]
        public void ParsePressedKeysToLetters_ShouldTotalProcessUnsequencedDeletes()
        {
            // Arrange
            StringBuilder pressedKeys = new("*22 2277*7**#");

            // Act
            PhonePadParser.ParsePressedKeysToLetters(pressedKeys);

            // Assert
            Assert.Equal("B", pressedKeys.ToString());
        }

        [Fact]
        public void ParsePressedKeysToLetters_ShouldDeletePreviousSequence()
        {
            // Arrange
            StringBuilder pressedKeys = new("8 88777444666*664#");

            // Act
            PhonePadParser.ParsePressedKeysToLetters(pressedKeys);

            // Assert
            Assert.Equal("TURING", pressedKeys.ToString());  // Probably Failing test
        }

        [Fact]
        public void ParsePressedKeysToLetters_ShouldCycleThroughKeys()
        {
            // Arrange
            StringBuilder pressedKeys = new("66666");

            // Act
            PhonePadParser.ParsePressedKeysToLetters(pressedKeys);

            // Assert
            Assert.Equal("N", pressedKeys.ToString());  // Probably Failing test
        }

        [Fact]
        public void ParsePressedKeysToLetters_ShouldConvertDoubleSpaceToSpace()
        {
            // Arrange
            StringBuilder pressedKeys = new("  22   ");

            // Act
            PhonePadParser.ParsePressedKeysToLetters(pressedKeys);

            // Assert
            Assert.Equal(" B  ", pressedKeys.ToString());  // Probably Failing test
        }

        [Fact]
        public void ParsePressedKeysToLetters_ZerosConvertToSpaces()
        {
            // Arrange
            StringBuilder pressedKeys = new("0200");

            // Act
            PhonePadParser.ParsePressedKeysToLetters(pressedKeys);

            // Assert
            Assert.Equal("A ", pressedKeys.ToString());  // Probably Failing test
        }
    }
}