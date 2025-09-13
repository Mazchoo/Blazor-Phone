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
    }
}