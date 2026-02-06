namespace CalculatorDomainDemo;

/// <summary>
/// This enum represents the ONLY operations our calculator allows.
/// 
/// Think of this like rules in the Conference Booking System:
/// - You cannot book a session type that does not exist
/// - You cannot invent a new booking status at runtime
/// 
/// Enums:
/// - Replace magic strings
/// - Prevent invalid values
/// - Let the compiler enforce business rules
/// </summary>
public enum OperationType
{
    Add = 0,
    Subtract = 1,
    Multiply = 2,
    Divide = 3
}
