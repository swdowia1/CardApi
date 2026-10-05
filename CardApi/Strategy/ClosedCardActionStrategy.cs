using CardApi.Models;

namespace CardApi.Strategy
{
    public class ClosedCardActionStrategy : ICardActionStrategy
    {
        public bool CanHandle(CardDetails card)
        {
            return card.CardStatus == CardStatus.Closed;
        }

        public IReadOnlyCollection<string> GetActions(CardDetails card)
        {
            return
            [
                "ACTION3",
            "ACTION4",
            "ACTION9"
            ];
        }
    }
}
