using CardApi.Models;
using CardApi.Strategy;
using FluentAssertions;

namespace CardApi.Test
{
    public class BlockedCardActionStrategyTests
    {
        private readonly BlockedCardActionStrategy _strategy = new();

        [Fact]
        public void CanHandle_ShouldReturnTrue_ForBlockedCard()
        {
            var card = new CardDetails(
                "123",
                CardType.Credit,
                CardStatus.Blocked,
                true);

            _strategy.CanHandle(card).Should().BeTrue();
        }

        [Fact]
        public void GetActions_ShouldReturnCorrectActions_WhenPinIsSet()
        {
            var card = new CardDetails(
                "123",
                CardType.Credit,
                CardStatus.Blocked,
                true);

            var result = _strategy.GetActions(card);

            result.Should().BeEquivalentTo(
            [
                "ACTION3",
            "ACTION4",
            "ACTION5",
            "ACTION6",
            "ACTION7",
            "ACTION8",
            "ACTION9"
            ]);
        }

        [Fact]
        public void GetActions_ShouldNotReturnAction6And7_WhenPinIsNotSet()
        {
            var card = new CardDetails(
                "123",
                CardType.Credit,
                CardStatus.Blocked,
                false);

            var result = _strategy.GetActions(card);

            result.Should().NotContain("ACTION6");
            result.Should().NotContain("ACTION7");
        }

        [Fact]
        public void GetActions_ShouldNotReturnAction5_ForDebit()
        {
            var card = new CardDetails(
                "123",
                CardType.Debit,
                CardStatus.Blocked,
                true);

            var result = _strategy.GetActions(card);

            result.Should().NotContain("ACTION5");
        }
    }
}
