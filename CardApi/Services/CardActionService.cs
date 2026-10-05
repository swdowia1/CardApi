using CardApi.Models;
using CardApi.Rules;

namespace CardApi.Services
{
    public class CardActionService : ICardActionService
    {
        private readonly ICardActionRuleEngine _ruleEngine;

        public CardActionService(
            ICardActionRuleEngine ruleEngine)
        {
            _ruleEngine = ruleEngine;
        }

        public IReadOnlyCollection<string> GetAllowedActions(
            CardDetails card)
        {
            return _ruleEngine.GetAllowedActions(card);
        }
    }
}
