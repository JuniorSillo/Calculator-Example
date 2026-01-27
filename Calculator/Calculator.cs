namespace CalculatorDomainDemo;

/// <summary>
/// This class represents the DOMAIN BEHAVIOUR.
/// 
/// In real systems:
/// - This is where rules live
/// - This is where decisions are made
/// 
/// In the booking system, this is similar to:
/// - Booking management logic
/// </summary>
public class Calculator
{
    //fields
    private readonly List<CalculationRequest> _calculationHistory = new();

    public IReadOnlyList<CalculationRequest> CalculationHistory => _calculationHistory.AsReadOnly();
    
    /// <summary>
    /// This property stores state INSIDE the object.
    /// 
    /// Notice:
    /// - Public getter
    /// - Private setter
    /// 
    /// This means:
    /// - Other code can read the value
    /// - Only the Calculator can change it
    /// 
    /// This protects the object from invalid changes.
    /// </summary>
    public int LastResult { get; private set; }

    /// <summary>
    /// Every calculator must have a name.
    /// 
    /// Constructors define what MUST exist
    /// for an object to be valid.
    /// </summary>
    public string Name { get; }

    public Calculator(string name)
    {
        // Guard clause:
        // We do NOT allow invalid objects to exist.
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Calculator must have a name");

        Name = name;
    }

    /// <summary>
    /// This method applies business rules.
    /// 
    /// It does NOT:
    /// - Read from the console
    /// - Print output
    /// 
    /// This separation is important because:
    /// - Console apps are temporary
    /// - Business logic must survive
    /// 
    /// In the booking system:
    /// - This would decide if a booking is allowed
    /// - This would enforce capacity rules
    /// </summary>
    public int Calculate(int a, int b, OperationType operation)
    {
        // Switch expression ensures ALL enum cases are handled
        LastResult = operation switch
        {
            OperationType.Add => a + b,
            OperationType.Subtract => a - b,
            OperationType.Multiply => a * b,
            OperationType.Divide => a / b,

            // This should never happen if enums are used correctly
            _ => throw new InvalidOperationException("Invalid operation")
        };

        CalculationRequest request = new CalculationRequest(a, b, operation);
        _calculationHistory.Add(request);

        return LastResult;
    }

    // how many additions have been done
    public List<CalculationRequest> ReturnAdditions()
    {
        List<CalculationRequest> additions = new List<CalculationRequest>();

        foreach (CalculationRequest req in _calculationHistory)
        {
            if (req.Operation == OperationType.Add)
            {
                additions.Add(req);
            }
        }

        return additions;
    }

    public List<CalculationRequest> ReturnAdditions_LINQ()
    {
        List<CalculationRequest> req = new List<CalculatonRequest>();
    
        return req;
    }

}