namespace CalculatorDomainDemo;

/// <summary>
/// A record is a DATA HOLDER.
/// 
/// It does NOT:
/// - Perform calculations
/// - Validate rules
/// - Make decisions
/// 
/// It simply describes a request.
/// 
/// In the booking system, this is similar to:
/// - A booking request coming from a user
/// - Data submitted via a form
/// 
/// Records are:
/// - Immutable (cannot change after creation)
/// - Used to pass data across boundaries
/// </summary>
public record CalculationRequest(
    int A,
    int B,
    OperationType Operation
);
