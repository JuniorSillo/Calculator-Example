using Microsoft.Net.Http.Headers;
using CalculatorDomainDemo.Domain;
using System.Reflection.Emit;
using System.ComponentModel.DataAnnotations;
public class CreateCalculationDto
{
    [Required]
    public double left{get;set;}

    [Required]
    public double right{get;set;}

    [Required]
    public OperandType operand{get;set;}
}