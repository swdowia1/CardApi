using CardApi.Models;
using CardApi.Strategy;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardApi.Test
{
    public class ActiveCardActionStrategyTests
    {
        private readonly ActiveCardActionStrategy _strategy = new();

        [Fact]
        public void CanHandle_ShouldReturnTrue_ForActiveCard()
        {
            var card = new CardDetails(
                "123",
                CardType.Debit,
                CardStatus.Active,
                true);

            _strategy.CanHandle(card).Should().BeTrue();
        }

        [Fact]
        public void GetActions_ShouldReturnCorrectActions_ForDebitWithPin()
        {
            var card = new CardDetails(
                "123",
                CardType.Debit,
                CardStatus.Active,
                true);

            var result = _strategy.GetActions(card);

            result.Should().BeEquivalentTo(
            [
                "ACTION1",
            "ACTION3",
            "ACTION4",
            "ACTION6",
            "ACTION8",
            "ACTION9",
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
                CardStatus.Active,
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
                CardStatus.Active,
                false);

            var result = _strategy.GetActions(card);

            result.Should().Contain("ACTION7");
            result.Should().NotContain("ACTION6");
        }
    }
}
