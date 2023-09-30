using System.Diagnostics.Contracts;

namespace Services.ViewModels.DriverModels;
public class DriverCreateModel 
{
    public string FirstName {get;set;} = default!;
    public string LastName {get;set;} = default!;
    public bool Sex {get;set;} = default!;
    public DateTime DateOfBirth {get; set;} = default!;
}