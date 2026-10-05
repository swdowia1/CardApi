using CardApi.Models;

namespace CardApi.Strategy
{
    public class ExpiredCardActionStrategy : ICardActionStrategy
    {
        public bool CanHandle(CardDetails card)
        {
            return card.CardStatus == CardStatus.Expired;
        }

        public IReadOnlyCollection<string> GetActions(CardDetails card)
        {
            var actions = new List<string>
        {
            "ACTION3",
            "ACTION4",
            "ACTION9"
        };

            if (card.CardType == CardType.Credit)
            {
                actions.Add("ACTION5");
            }

            return actions;
        }
    }
}
