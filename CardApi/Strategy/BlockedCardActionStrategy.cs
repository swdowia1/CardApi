using CardApi.Models;

namespace CardApi.Strategy
{
    public class BlockedCardActionStrategy : ICardActionStrategy
    {
        public bool CanHandle(CardDetails card)
        {
            return card.CardStatus == CardStatus.Blocked;
        }

        public IReadOnlyCollection<string> GetActions(CardDetails card)
        {
            var actions = new List<string>
        {
            "ACTION3",
            "ACTION4",
            "ACTION8",
            "ACTION9"
        };

            if (card.CardType == CardType.Credit)
            {
                actions.Add("ACTION5");
            }

            if (card.IsPinSet)
            {
                actions.Add("ACTION6");
                actions.Add("ACTION7");
            }

            return actions;
        }
    }
}
