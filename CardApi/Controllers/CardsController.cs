using CardApi.Data;
using CardApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace CardApi.Controllers
{
    [ApiController]
    [Route("api/cards")]
    public class CardsController : ControllerBase
    {
        private readonly ICardService _cardService;
        private readonly ICardActionService _cardActionService;

        public CardsController(ICardService cardService,ICardActionService cardActionService)
        {
            _cardService = cardService;
            _cardActionService = cardActionService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllCards(CancellationToken cancellationToken)
        {
            var cards = await _cardService.GetAllCards(
                cancellationToken);

            var result = cards.ToDictionary(
                user => user.Key,
                user => user.Value.Select(card => new
                {
                    card.CardNumber,
                    CardType = card.CardType.ToString(),
                    CardStatus = card.CardStatus.ToString(),
                    card.IsPinSet
                }));

            return Ok(result);
        }
        [HttpGet("{userId}/{cardNumber}/actions")]
        public async Task<IActionResult> GetAllowedActions(string? userId,string? cardNumber,CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                return BadRequest("UserId is required.");
            }

            if (string.IsNullOrWhiteSpace(cardNumber))
            {
                return BadRequest("CardNumber is required.");
            }

            var card = await _cardService.GetCardDetails(
                userId,
                cardNumber,
                cancellationToken);

            if (card is null)
            {
                return NotFound();
            }

            var actions = _cardActionService.GetAllowedActions(card);

            return Ok(actions);
        }
    }
}
