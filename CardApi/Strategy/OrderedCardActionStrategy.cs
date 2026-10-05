using CardApi.Models;

namespace CardApi.Strategy
{
    public class OrderedCardActionStrategy : ICardActionStrategy
    {
        public bool CanHandle(CardDetails card)
        {
            return card.CardStatus == CardStatus.Ordered;
        }

        public IReadOnlyCollection<string> GetActions(CardDetails card)
        {
            var actions = new List<string>
        {
            "ACTION3",
            "ACTION4",
            "ACTION8",
            "ACTION9",
            "ACTION10",
            "ACTION12",
            "ACTION13"
        };

            if (card.IsPinSet)
            {
                actions.Add("ACTION6");
            }
            else
            {
                actions.Add("ACTION7");
            }

            return actions;
        }
    }
}
