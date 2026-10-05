using CardApi.Models;
using CardApi.Strategy;
using FluentAssertions;

namespace CardApi.Test
{
    public class ClosedCardActionStrategyTests
    {
        private readonly ClosedCardActionStrategy _strategy = new();

        [Fact]
        public void CanHandle_ShouldReturnTrue_ForClosedCard()
        {
            var card = new CardDetails(
                "123",
                CardType.Debit,
                CardStatus.Closed,
                true);

            _strategy.CanHandle(card).Should().BeTrue();
        }

        [Fact]
        public void GetActions_ShouldReturnCorrectActions()
        {
            var card = new CardDetails(
                "123",
                CardType.Debit,
                CardStatus.Closed,
                true);

            var result = _strategy.GetActions(card);

            result.Should().BeEquivalentTo(
            [
                "ACTION3",
            "ACTION4",
            "ACTION9"
            ]);
        }

        [Fact]
        public void GetActions_ShouldNotReturnAction5_ForCredit()
        {
            var card = new CardDetails(
                "123",
                CardType.Credit,
                CardStatus.Closed,
                true);

            var result = _strategy.GetActions(card);

            result.Should().NotContain("ACTION5");
        }
    }
}
