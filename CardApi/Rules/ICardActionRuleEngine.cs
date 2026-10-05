using CardApi.Models;

namespace CardApi.Rules
{
    public interface ICardActionRuleEngine
    {
        IReadOnlyCollection<string> GetAllowedActions(CardDetails card);
    }
}
