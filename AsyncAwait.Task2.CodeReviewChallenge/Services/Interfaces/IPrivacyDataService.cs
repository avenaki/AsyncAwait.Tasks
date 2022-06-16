using System.Threading.Tasks;

namespace AsyncAwait.Task2.CodeReviewChallenge.Services.Interfaces;

public interface IPrivacyDataService
{
    Task<string> GetPrivacyDataAsync();
}
