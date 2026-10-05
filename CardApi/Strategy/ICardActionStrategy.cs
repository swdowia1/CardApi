using CardApi.Models;

namespace CardApi.Strategy
{
    public interface ICardActionStrategy
    {
        bool CanHandle(CardDetails card);

        IReadOnlyCollection<string> GetActions(CardDetails card);
    }
}
