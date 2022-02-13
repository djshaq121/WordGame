using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace WordGameAPI.Services
{
    public class WordService : IWordService
    {
        private readonly IConfiguration configuration;
        private readonly IHttpClientFactory httpClientFactory;
        private readonly int startingLetters = 12;
        private readonly int vowelMin = 4;
        private readonly List<char> vowels =  new() { 'A', 'E', 'I', 'O', 'U' };
        private readonly List<char> consonants = new() { 'B', 'C', 'D', 'F', 'G', 'H', 'J', 'K', 'L', 'M', 'N', 'P', 'Q', 'R', 'S', 'T', 'V', 'X', 'Z', 'W', 'Y' };
        public WordService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            this.httpClientFactory = httpClientFactory;
            this.configuration = configuration;
        }

        public List<char> GenerateStartingLetters()
        {
            var output = new List<char>();
            var rand = new Random();
            
            for(int i = 0; i < vowelMin; i++)
            {
                var randomNumber = rand.Next(0, vowels.Count - 1);
                var letter = vowels[randomNumber];
                output.Add(letter);
            }

            var lettersToAdd = startingLetters - vowelMin;
            var tempCosnats = new List<char>(consonants);
            for (int i = 0; i < lettersToAdd; i++)
            {
                var randomNumber = rand.Next(0, tempCosnats.Count - 1);
                var letter = tempCosnats[randomNumber];
                tempCosnats.RemoveAt(randomNumber);
                output.Add(letter);
            }

            return output;
        }

        public async Task<bool> ValidateWord(string word)
        {
            if (string.IsNullOrWhiteSpace(word) || word.Length < 3)
                return Task.FromResult(false).Result;

            var httpClient = httpClientFactory.CreateClient();

            var requestUri = new Uri($"https://twinword-word-graph-dictionary.p.rapidapi.com/definition/?entry={word}");
            //var url = "https://dictionaryapi.dev";
            var request = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = requestUri,
                Headers =
                {
                    { "x-rapidapi-host", "twinword-word-graph-dictionary.p.rapidapi.com" },
                    { "x-rapidapi-key", configuration["TwinWordApiKey"] },
                },
            }; 
           
            var response = await httpClient.SendAsync(request);

            if(!response.IsSuccessStatusCode)
                return Task.FromResult(false).Result;

            using (var result = await response.Content.ReadAsStreamAsync())
            {
                var wordModel = await JsonSerializer.DeserializeAsync<WordModel>(result);
                
                if(wordModel.ResultCode != "200")
                    return Task.FromResult(false).Result;

            }

            return Task.FromResult(true).Result;
        }

        public int PointsForWord(string word)
        {
            var points = 0;
            var wordLength = word.Length;
            if (wordLength < 3)
                points = 0;
            else if (wordLength <= 5)
                points = 2;
            else if (wordLength == 6)
                points = 3;
            else if (wordLength == 7)
                points = 5;
            else if (wordLength >= 8)
                points = 11;

            return points;
        }
    }
}
