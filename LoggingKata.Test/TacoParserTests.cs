using System;
using Xunit;

namespace LoggingKata.Test
{
    public class TacoParserTests
    {

        [Theory]
        [InlineData("0,0,Tacobell")]
        [InlineData("43.43,32.32,Tacobell")]
        public void ShouldParse(string str)
        {
            // Arange
            TacoParser tacoParser = new TacoParser();

            // Act
            ITrackable actual = tacoParser.Parse(str);

            // Assert
            Assert.NotNull(actual);
            Assert.NotNull(actual.Location);
            Assert.NotNull(actual.Name);
            Assert.True(actual.Name.Length > 0);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("0,abc,TacoBell")]
        [InlineData("abc,0,TacoBell")]
        [InlineData("0,0")]
        [InlineData("300,0,TacoBell")]
        [InlineData("0,300,TacoBell")]
        [InlineData("abc,0,TacoBell")]
        public void ShouldFailParse(string str)
        {
            // Arrange
            TacoParser tacoParser = new TacoParser();

            // Act
            ITrackable actual = tacoParser.Parse(str);

            // Assert
            Assert.Null(actual);
        }
    }
}
