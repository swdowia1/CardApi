using CardApi.Models;
using CardApi.Strategy;

namespace CardApi.Rules
{
    public class CardActionRuleEngine : ICardActionRuleEngine
    {
        private readonly IEnumerable<ICardActionStrategy> _strategies;

        public CardActionRuleEngine(IEnumerable<ICardActionStrategy> strategies)
        {
            _strategies = strategies;
        }

        public IReadOnlyCollection<string> GetAllowedActions(CardDetails card)
        {
            var strategy = _strategies.FirstOrDefault(x => x.CanHandle(card));

            if (strategy is null)
            {
                return [];
            }

            return strategy.GetActions(card);
        }
    }
}
