using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WordGameAPI.Services;
using Xunit;

namespace WordGameTests
{
    public class WordServiceTests
    {
        private readonly IWordService wordService;
        private readonly Mock<IHttpClientFactory> clientFactoryMock = new Mock<IHttpClientFactory>();
        private readonly Mock<HttpMessageHandler> mockHttpMessageHandler = new Mock<HttpMessageHandler>();
        public WordServiceTests()
        {
            wordService = new WordService(clientFactoryMock.Object);
        }

        [Theory]
        [InlineData("hello", 2)]
        [InlineData("", 0)]
        [InlineData("puzzle", 3)]
        [InlineData("grizzly", 5)]
        [InlineData("absolute", 11)]
        public void PointsForWord_Test_Points_For_Word(string word, int expectedPoints)
        {
           var result = wordService.PointsForWord(word);

           Assert.Equal(expectedPoints, result);
        }
    }
}
