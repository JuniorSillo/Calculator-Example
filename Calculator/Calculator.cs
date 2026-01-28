using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading;

namespace CalculatorDomainDemo;

/// <summary>
/// Owns calculator behaviour and internal state.
/// 
/// This class:
/// - Performs calculations
/// - Applies business rules
/// - Maintains history
/// 
/// Booking analogy:
/// similar to a booking logic / rules component.
/// </summary>
public class Calculator
{
    /*
     * INTERNAL MUTABLE STATE
     * 
     * This list is intentionally mutable.
     * The calculator changes it over time.
     */
    private readonly List<CalculationRequest> _history = new();

    public string Name { get; }
    public int LastResult { get; private set; }

    public Calculator(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Calculator must have a name.");

        Name = name;
    }

    
    public IReadOnlyList<CalculationRequest> GetHistory()
    {
        return _history.ToList(); // defensive copy
    }

    
    public int Calculate(int a, int b, OperationType operation)
    {
        // Guard clause: fail fast
        if (operation == OperationType.Divide && b == 0)
            throw new InvalidOperationException("Cannot divide by zero.");

        int result = operation switch
        {
            OperationType.Add => a + b,
            OperationType.Subtract => a - b,
            OperationType.Multiply => a * b,
            OperationType.Divide => a / b,
            _ => throw new InvalidOperationException("Invalid operation.")
        };

        // MUTATION happens here (internally only)
        _history.Add(new CalculationRequest(a, b, operation));

        LastResult = result;
        return result;
    }

   
   public CalculationRequest GetCalculationRequest()
    {   
        if (!_history.Any())
            throw new CalculationHistoryException();
        
        else
        {
            CalculationRequest request = _history.Last();
            return request;
        }
    }

    public bool HasUsedDivision()
    {
        return _history.Any(r => r.Operation == OperationType.Divide);
    }

    public CalculationRequest? GetLastMultiplication()
      {
        return _history.LastOrDefault(m => m.Operation == OperationType.Multiply);
      }


    public CalculationRequest? GetLastCalculation()
    {
        return _history.LastOrDefault();
    }

    public IEnumerable<CalculationRequest> GetByOperation(OperationType operation)
    {
        return _history.Where(r => r.Operation == operation);
    }

    /*
     * ============================
     * GROUPING WITH DICTIONARY
     * ============================
     */
    public Dictionary<OperationType, List<CalculationRequest>> GroupByOperation()
    {
        var grouped = new Dictionary<OperationType, List<CalculationRequest>>();

        foreach (var request in _history)
        {
            if (!grouped.ContainsKey(request.Operation))
            {
                grouped[request.Operation] = new List<CalculationRequest>();
            }

            grouped[request.Operation].Add(request);
        }

        return grouped;
    }


    //Writing History to files
    public async Task SaveHistoryAsync(string filepath)
    {
        List<CalculationRequest> snapshot = _history.ToList();
        string json = JsonSerializer.Serialize(snapshot);
        await File.WriteAllTextAsync(filepath, json);

    }

    public async Task<List<CalculationRequest>> LoadHistoryAsync(string filepath)
    {
        if (File.Exists(filepath))
        {
            string json = await File.ReadAllTextAsync(filepath);
            return JsonSerializer.Deserialize<List<CalculationRequest>>(json) ?? new List<CalculationRequest>();
        }
       
    }



}
