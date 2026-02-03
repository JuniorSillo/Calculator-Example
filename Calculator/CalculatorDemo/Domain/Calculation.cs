
namespace CalculatorDomainDemo.Domain
{
    public class Calculation
    {
        public Guid Id { get; }
        public double Left { get; }
        public double Right { get; }
        public OperationType Operation { get; }
        public double Result { get; }
        public DateTime CreatedAt { get; }

        public Calculation(
            double left,
            double right,
            OperationType operation,
            double result)
        {
            Id = Guid.NewGuid();
            Left = left;
            Right = right;
            Operation = operation;
            Result = result;
            CreatedAt = DateTime.UtcNow;
        }
    }
}