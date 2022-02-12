using System.Collections.Generic;
using System.Threading.Tasks;

namespace WordGameAPI.Services
{
    public interface IWordService
    {
        List<char> GenerateStartingLetters();

        Task<bool> ValidateWord(string word);

        public int PointsForWord(string word);
    }
}