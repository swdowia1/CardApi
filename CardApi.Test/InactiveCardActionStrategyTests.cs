using CardApi.Models;
using CardApi.Strategy;
using FluentAssertions;

namespace CardApi.Test
{
    public class InactiveCardActionStrategyTests
    {
        private readonly InactiveCardActionStrategy _strategy = new();

        [Fact]
        public void CanHandle_ShouldReturnTrue_ForInactiveCard()
        {
            var card = new CardDetails(
                "123",
                CardType.Debit,
                CardStatus.Inactive,
                true);

            _strategy.CanHandle(card).Should().BeTrue();
        }

        [Fact]
        public void GetActions_ShouldReturnCorrectActions_ForDebitWithPin()
        {
            var card = new CardDetails(
                "123",
                CardType.Debit,
                CardStatus.Inactive,
                true);

            var result = _strategy.GetActions(card);

            result.Should().BeEquivalentTo(
            [
                "ACTION2",
            "ACTION3",
            "ACTION4",
            "ACTION6",
            "ACTION8",
            "ACTION9",
            "ACTION10",
            "ACTION11",
            "ACTION12",
            "ACTION13"
            ]);
        }

        [Fact]
        public void GetActions_ShouldReturnAction5_ForCredit()
        {
            var card = new CardDetails(
                "123",
                CardType.Credit,
                CardStatus.Inactive,
                true);

            var result = _strategy.GetActions(card);

            result.Should().Contain("ACTION5");
        }

        [Fact]
        public void GetActions_ShouldReturnAction7_WhenPinIsNotSet()
        {
            var card = new CardDetails(
                "123",
                CardType.Debit,
                CardStatus.Inactive,
                false);

            var result = _strategy.GetActions(card);

            result.Should().Contain("ACTION7");
            result.Should().NotContain("ACTION6");
        }
    }
}
