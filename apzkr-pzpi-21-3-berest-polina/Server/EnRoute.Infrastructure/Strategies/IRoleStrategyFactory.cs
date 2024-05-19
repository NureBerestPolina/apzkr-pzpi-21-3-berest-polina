using EnRoute.Infrastructure.Services.Interfaces;

namespace EnRoute.Infrastructure.Strategies
{
    public interface IRoleStrategyFactory
    {
        public IRoleStrategy CreateStrategy(string role);
    }
}