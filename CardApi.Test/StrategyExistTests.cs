using CardApi.Models;
using CardApi.Strategy;

namespace CardApi.Test
{
    public class StrategyExistTests
    {
        [Fact]
        public void EveryCardStatus_ShouldHaveStrategy()
        {
            // Arrange
            var strategies = typeof(ICardActionStrategy)
                .Assembly
                .GetTypes()
                .Where(type =>
                    type is { IsClass: true, IsAbstract: false }
                    && typeof(ICardActionStrategy).IsAssignableFrom(type))
                .Select(type =>
                    (ICardActionStrategy)Activator.CreateInstance(type)!)
                .ToList();

            var statuses = Enum.GetValues<CardStatus>();

            // Act
            var missingStatuses = statuses
                .Where(status =>
                    !strategies.Any(strategy =>
                        strategy.CanHandle(
                            new CardDetails(
                                "TEST",
                                CardType.Debit,
                                status,
                                false))))
                .ToList();

            // Assert
            Assert.Empty(missingStatuses);
        }
    }
}
