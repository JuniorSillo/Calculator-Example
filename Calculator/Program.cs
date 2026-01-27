using CalculatorDomainDemo;

/// <summary>
/// Program.cs is the ENTRY POINT.
/// 
/// Its job is to:
/// - Collect input
/// - Call domain logic
/// - Show output
/// 
/// It should NOT:
/// - Contain business rules
/// - Make complex decisions
/// 
/// In the booking system:
/// - This is like a controller or API endpoint
/// </summary>
Console.WriteLine("=== Calculator Demo ===");

// Create the domain object
// This is similar to creating a service in a backend system
var calculator = new Calculator("Standard Calculator");

// Collect user input (UI concern)
Console.Write("Enter first number: ");
int a = int.Parse(Console.ReadLine()!);

Console.Write("Enter second number: ");
int b = int.Parse(Console.ReadLine()!);

// UI presents choices
Console.WriteLine("Choose operation:");
Console.WriteLine("1 - Add");
Console.WriteLine("2 - Subtract");
Console.WriteLine("3 - Multiply");
Console.WriteLine("4 - Divide");

Console.Write("Selection: ");
int selection = int.Parse(Console.ReadLine()!);

// Translate UI input into a BUSINESS RULE
// UI values should never leak into the domain
OperationType operation = selection switch
{
    1 => OperationType.Add,
    2 => OperationType.Subtract,
    3 => OperationType.Multiply,
    4 => OperationType.Divide,
    _ => throw new InvalidOperationException("Invalid selection")
};

// Create a request object (data only)
// In booking: this would be a BookingRequest
var request = new CalculationRequest(a, b, operation);

// Apply behaviour
// Program.cs does not calculate anything itself
int result = calculator.Calculate(
    request.A,
    request.B,
    request.Operation
);

// Output result (UI concern)
Console.WriteLine();
Console.WriteLine($"Calculator: {calculator.Name}");
Console.WriteLine($"Result: {result}");
Console.WriteLine($"Last Result Stored: {calculator.LastResult}");


foreach(CalculationRequest req in calculator.CalculationHistory)
{
    Console.WriteLine(req);
}


// how many additions have been done
foreach(CalculationRequest req in calculator.ReturnAdditions())
{
    Console.WriteLine(req);
}