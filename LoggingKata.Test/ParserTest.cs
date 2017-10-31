using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace LoggingKata.Test
{
    [TestFixture]
    public class TacoParserTestFixture
    {
        [Test]
        public void ShouldParseLine()
        {
            //Arrange

            var tacoParser = new TacoParser();

            var lessThan3 = "1234, 1234";
            var longNotNumber = "abc, 1234, Testing";
            var latNotNumber = "123, abc, Testing";

            //Act

            var lessThan3Value = tacoParser.Parse(lessThan3);
            var longNotNumberValue = tacoParser.Parse(longNotNumber);
            var latNotNumberValue = tacoParser.Parse(latNotNumber);

            //Assert

            Assert.Null(lessThan3Value, lessThan3 + " should not parse");
            Assert.Null(longNotNumberValue, longNotNumber + " should not parse");
            Assert.Null(latNotNumberValue, latNotNumber + " should not parse");
        }

        [Test]
        public void ShouldNotParseLine()
        {
            //Arrange
            var tacoParser = new TacoParser();

            var correctString = "123.04, 456.07, Name of Place";

            //Act

            var value = tacoParser.Parse(correctString);

            //Assert

            Assert.NotNull(value, correctString + " should parse into an ITrackable");
        }
    }
}