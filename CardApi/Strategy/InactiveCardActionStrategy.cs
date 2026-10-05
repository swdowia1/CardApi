using CardApi.Models;

namespace CardApi.Strategy
{
    public class InactiveCardActionStrategy : ICardActionStrategy
    {
        public bool CanHandle(CardDetails card)
        {
            return card.CardStatus == CardStatus.Inactive;
        }

        public IReadOnlyCollection<string> GetActions(CardDetails card)
        {
            var actions = new List<string>
        {
            "ACTION2",
            "ACTION3",
            "ACTION4",
            "ACTION8",
            "ACTION9",
            "ACTION10",
            "ACTION11",
            "ACTION12",
            "ACTION13"
        };

            if (card.CardType == CardType.Credit)
            {
                actions.Add("ACTION5");
            }

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
