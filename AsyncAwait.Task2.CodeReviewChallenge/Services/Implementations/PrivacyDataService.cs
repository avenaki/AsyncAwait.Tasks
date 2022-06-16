using AsyncAwait.Task2.CodeReviewChallenge.Services.Interfaces;
using System.Threading.Tasks;

namespace AsyncAwait.Task2.CodeReviewChallenge.Services.Implementations;

public class PrivacyDataService : IPrivacyDataService
{
    public Task<string> GetPrivacyDataAsync()
    {
        return Task.FromResult("This Policy describes how async/await processes your personal data," +
                                     "but it may not address all possible data processing scenarios.");
    }
}
