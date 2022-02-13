using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordGameAPI;
using WordGameAPI.Controllers;
using WordGameAPI.Dto;
using WordGameAPI.Params;
using WordGameAPI.Services;
using Xunit;

namespace WordGameTests
{
    public class WordControllerTests
    {
        private readonly Mock<IWordService> wordServiceMock = new Mock<IWordService>();
        private readonly WordController wordController;
        public WordControllerTests()
        {
            wordController = new WordController(wordServiceMock.Object);
        }

        [Fact]
        public async Task SubmitWord_When_Word_Is_Not_Valid()
        {
            // Arrange
            var word = "wordNotValid";
            var expectedString = "Word not found";

            wordServiceMock.Setup(x => x.ValidateWord(It.IsAny<string>()))
                .ReturnsAsync(false);

            var wordParams = new WordParams
            {
                Word = word
            };

            // Act
            var actionResult = await wordController.SubmitWord(wordParams);

            // Assert
            var result = (actionResult.Result as NotFoundObjectResult).Value as string;

            Assert.NotNull(result);
            Assert.Equal(expectedString, result);
        }

        [Fact]
        public async Task SubmitWord_When_Word_Is_Valid()
        {
            // Arrange
            var pointsDto = new PointsDto
            {
                Word = "",
                Points = 3
            };


            wordServiceMock.Setup(x => x.ValidateWord(It.IsAny<string>()))
                .ReturnsAsync(true);

            wordServiceMock.Setup(x => x.PointsForWord(It.IsAny<string>()))
                .Returns(3);

            var wordParams = new WordParams
            {
                Word = "valid"
            };

            // Act
            var actionResult = await wordController.SubmitWord(wordParams);

            // Assert
            var result = (actionResult.Result as OkObjectResult).Value as PointsDto;

            Assert.NotNull(result);
            Assert.Equal(pointsDto.Points, result.Points);
        }
    }
}
