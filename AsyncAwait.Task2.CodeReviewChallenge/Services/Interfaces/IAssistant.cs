using System.Threading.Tasks;

namespace AsyncAwait.Task2.CodeReviewChallenge.Services.Interfaces;

public interface IAssistant
{
    Task<string> RequestAssistanceAsync(string requestInfo);
}
