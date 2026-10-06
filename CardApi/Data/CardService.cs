using CardApi.Models;

namespace CardApi.Data
{
    public class CardService : ICardService
    {
        private readonly Dictionary<string, Dictionary<string, CardDetails>> _userCards = CreateSampleUserCards();
        public async Task<IReadOnlyDictionary<string, IReadOnlyCollection<CardDetails>>> GetAllCards(CancellationToken cancellationToken)
        {
            await Task.Delay(500, cancellationToken);

            return _userCards.ToDictionary(
                x => x.Key,
                x => (IReadOnlyCollection<CardDetails>)x.Value.Values.ToList());
        }
        public async Task<CardDetails?> GetCardDetails(string userId,string cardNumber,CancellationToken cancellationToken)
        {
            // Symulacja wywołania zewnętrznego serwisu
            await Task.Delay(1000, cancellationToken);

            if (!_userCards.TryGetValue(userId, out var cards)
                || !cards.TryGetValue(cardNumber, out var cardDetails))
            {
                return null;
            }

            return cardDetails;
        }

        private static Dictionary<string, Dictionary<string, CardDetails>> CreateSampleUserCards()
        {
            var userCards =new Dictionary<string, Dictionary<string, CardDetails>>();

            for (var i = 1; i <= 3; i++)
            {
                var cards = new Dictionary<string, CardDetails>();
                var cardIndex = 1;

                foreach (CardType cardType in Enum.GetValues(typeof(CardType)))
                {
                    foreach (CardStatus cardStatus in Enum.GetValues(typeof(CardStatus)))
                    {
                        var cardNumber = $"Card{i}{cardIndex}";

                        cards.Add(
                            cardNumber,
                            new CardDetails(
                                CardNumber: cardNumber,
                                CardType: cardType,
                                CardStatus: cardStatus,
                                IsPinSet: cardIndex % 2 == 0));


                        cardIndex++;
                    }
                }

                var userId = $"User{i}";

                userCards.Add(userId, cards);
            }

            return userCards;
        }
    }
}
