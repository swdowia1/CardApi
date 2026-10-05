using CardApi.Models;

namespace CardApi.Data
{
    public interface ICardService
    {
        Task<CardDetails?> GetCardDetails(
            string userId,
            string cardNumber,
            CancellationToken cancellationToken);
        Task<IReadOnlyDictionary<string, IReadOnlyCollection<CardDetails>>>
      GetAllCards(CancellationToken cancellationToken);
    }
}
