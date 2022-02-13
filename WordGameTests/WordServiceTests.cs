using Microsoft.Extensions.Configuration;
using Moq;
using Moq.Protected;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
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
        private readonly Mock<IConfiguration> cofigurationMock = new Mock<IConfiguration>();
        public WordServiceTests()
        {
            wordService = new WordService(clientFactoryMock.Object, cofigurationMock.Object);
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

        [Theory]
        [InlineData("hello", true)]
        [InlineData("hi", false)]
        public async Task ValidateWord_Test_Word_Validation(string word, bool expectedResult)
        {
            // Arrange
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent("{\n  \"result_msg\" : \"Success\",\n  \"result_code\" : \"200\"\n}"),
                });

            var client = new HttpClient(mockHttpMessageHandler.Object);

            clientFactoryMock.Setup(x => x.CreateClient(It.IsAny<string>()))
                   .Returns(client);

            // Act 
            var result = await wordService.ValidateWord(word);

            // Assert
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public async Task ValidateWord_When_Word_Is_Not_Found()
        {
            // Arrange
            var expcted = false;
            var word = "NotWord";

            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent("{\n  \"result_msg\" : \"Success\",\n  \"result_code\" : \"462\"\n}"),
                });

            var client = new HttpClient(mockHttpMessageHandler.Object);

            clientFactoryMock.Setup(x => x.CreateClient(It.IsAny<string>()))
                   .Returns(client);

            // Act 
            var result = await wordService.ValidateWord(word);

            // Assert
            Assert.Equal(expcted, result);
        }
    }
}
