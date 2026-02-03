namespace CalculatorDomainDemo;
public record CalculationRequest(
    double left,
    double right,
    OperationType Operation
);
