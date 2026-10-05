using CardApi.Models;
using CardApi.Strategy;
using FluentAssertions;

namespace CardApi.Test
{
    public class OrderedCardActionStrategyTests
    {
        private readonly OrderedCardActionStrategy _strategy = new();

        [Fact]
        public void CanHandle_ShouldReturnTrue_ForOrderedCard()
        {
            var card = new CardDetails(
                "123",
                CardType.Credit,
                CardStatus.Ordered,
                true);

            _strategy.CanHandle(card).Should().BeTrue();
        }

        [Fact]
        public void GetActions_ShouldReturnCorrectActions_WhenPinIsSet()
        {
            var card = new CardDetails(
                "123",
                CardType.Credit,
                CardStatus.Ordered,
                true);

            var result = _strategy.GetActions(card);

            result.Should().BeEquivalentTo(
            [
                "ACTION3",
            "ACTION4",
            "ACTION6",
            "ACTION8",
            "ACTION9",
            "ACTION10",
            "ACTION12",
            "ACTION13"
            ]);
        }

        [Fact]
        public void GetActions_ShouldReturnAction7_WhenPinIsNotSet()
        {
            var card = new CardDetails(
                "123",
                CardType.Credit,
                CardStatus.Ordered,
                false);

            var result = _strategy.GetActions(card);

            result.Should().Contain("ACTION7");
            result.Should().NotContain("ACTION6");
        }
    }
}
