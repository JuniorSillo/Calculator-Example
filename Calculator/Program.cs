using System;
using CalculatorDomainDemo;

class Program
{
    static void Main()
    {
        Console.WriteLine("=== Calculator – End of Day 2 (Defensive Copy Version) ===");

        var calculator = new Calculator("Training Calculator");

        calculator.Calculate(10, 5, OperationType.Add);
        calculator.Calculate(20, 4, OperationType.Divide);
        calculator.Calculate(7, 3, OperationType.Multiply);

        // ---------------------------------
        // HISTORY ACCESS (COPY)
        // ---------------------------------
        var historySnapshot = calculator.GetHistory();

        Console.WriteLine("\n--- Calculation History (Snapshot) ---");
        foreach (var request in historySnapshot)
        {
            Console.WriteLine($"{request.A} {request.Operation} {request.B}");
        }

        // Even if someone modifies this list,
        // it does NOT affect the calculator.
        // (Try clearing it to prove the point.)
        // historySnapshot.Clear(); // <-- does NOT break the calculator

        Console.WriteLine("\n--- Business Questions ---");
        Console.WriteLine($"Has division been used? {calculator.HasUsedDivision()}");

        var last = calculator.GetLastCalculation();
        if (last != null)
        {
            Console.WriteLine($"Last calculation: {last.A} {last.Operation} {last.B}");
        }

        Console.WriteLine("\n=== End ===");
    }
}
