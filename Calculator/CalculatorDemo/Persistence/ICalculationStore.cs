using CalculatorDomainDemo.Domain;

namespace CalculatorDomainDemo.Persistence
{
    public interface ICalculationStore
    {
        Task SaveAsync(Calculation calculation);
        Task<IReadOnlyList<Calculation>> LoadAllAsync();
    }
}