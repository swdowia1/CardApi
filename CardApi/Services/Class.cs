using CardApi.Models;

namespace CardApi.Services
{
    public interface ICardActionService
    {
        IReadOnlyCollection<string> GetAllowedActions(CardDetails card);
    }
}
