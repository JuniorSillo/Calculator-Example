namespace CalculatorDomainDemo;
public class CalculationHistoryException : Exception
{
    public CalculationHistoryException() : base("Calculation history is empty.")
    {
    }
}