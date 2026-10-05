using CardApi.Models;
using CardApi.Strategy;
using FluentAssertions;

namespace CardApi.Test
{
    public class ExpiredCardActionStrategyTests
    {
        private readonly ExpiredCardActionStrategy _strategy = new();

        [Fact]
        public void CanHandle_ShouldReturnTrue_ForExpiredCard()
        {
            var card = new CardDetails(
                "123",
                CardType.Debit,
                CardStatus.Expired,
                true);

            _strategy.CanHandle(card).Should().BeTrue();
        }

        [Fact]
        public void GetActions_ShouldReturnBasicActions()
        {
            var card = new CardDetails(
                "123",
                CardType.Debit,
                CardStatus.Expired,
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
        public void GetActions_ShouldReturnAction5_ForCredit()
        {
            var card = new CardDetails(
                "123",
                CardType.Credit,
                CardStatus.Expired,
                true);

            var result = _strategy.GetActions(card);

            result.Should().Contain("ACTION5");
        }
    }
}
