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
            StringBuilder pressedKeys = new("12345 67890");

            // Act
            int nrCharactersIgnored = PhonePadParser.MoveValidCharactersToEndOfString(pressedKeys);

            // Assert
            Assert.Equal(0, nrCharactersIgnored);
            Assert.Equal("12345 67890", pressedKeys.ToString());
        }

        [Fact]
        public void MoveValidCharactersToEndOfString_ShouldIgnoreInvalidCharacters()
        {
            // Arrange
            StringBuilder pressedKeys = new("12345 AB 67890#");

            // Act
            int nrCharactersIgnored = PhonePadParser.MoveValidCharactersToEndOfString(pressedKeys);

            // Assert
            Assert.Equal(3, nrCharactersIgnored);
            Assert.Equal("12312345  67890", pressedKeys.ToString());
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
    }
}