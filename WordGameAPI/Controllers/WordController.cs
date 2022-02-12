using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WordGameAPI.Dto;
using WordGameAPI.Services;

namespace WordGameAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WordController : ControllerBase
    {
        private readonly IWordService wordService;

        public WordController(IWordService wordService)
        {
            this.wordService = wordService;
        }

        [HttpGet]
        [Route("start-game")]
        public ActionResult<string> BeginGame()
        {
            var listOfLetters = wordService.GenerateStartingLetters();

            var boardDto = new BoardDto
            {
                startingLetters = listOfLetters
            };

            return Ok(boardDto);
        }

        [HttpPost]
        [Route("submit-word")]
        public async Task<ActionResult<PointsDto>> SubmitWord(string word)
        {
            var result = await wordService.ValidateWord(word);

            if (!result)
                return NotFound("Word not found");

            var points = wordService.PointsForWord(word);
            var pointsDto = new PointsDto
            {
                Points = points
            };

            return Ok(pointsDto);
        }


    }
}
