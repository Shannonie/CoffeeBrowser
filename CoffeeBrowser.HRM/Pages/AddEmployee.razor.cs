using CoffeeBrowser.HRM.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CoffeeBrowser.HRM.Pages;

public partial class AddEmployee
{
    private Employee? Employee { get; set; }
    private Department[]? Departments { get; set; }
    private bool IsDbBusy { get; set; }
    private string? SuccessMessage { get; set; }
    private string? ErrorMessage { get; set; }

    protected override async Task OnInitializedAsync()
    {
        using var context = ContextFactory.CreateDbContext();

        Departments = await context.Departments
                             .OrderBy(dept => dept.Name)
                             .ToArrayAsync();

        Employee = new()
        {
            DepartmentId = Departments.FirstOrDefault()?.Id
        };
    }

    private async Task HandleSubmit(bool isValid)
    {
        if (isValid)
        {
            await HandleValidSubmit();
        }
        else
        {
            HandleInvalidSubmit();
        }
    }

    private async Task HandleValidSubmit()
    {
        if (IsDbBusy) return;

        try
        {
            IsDbBusy = true;
            if (Employee is not null)
            {
                using Data.EmployeeHRMDbContext context =
                    ContextFactory.CreateDbContext();
                context.Employees.Add(Employee);
                await context.SaveChangesAsync();

                SuccessMessage = $"Employee {Employee.FirstName} {Employee.LastName}" +
                    $" was added successfully.";

                Employee = new()
                {
                    DepartmentId = Employee.DepartmentId
                };
            }
        }
        catch (Exception exception)
        {
            SuccessMessage = null;
            ErrorMessage = $"Error while adding employee: {exception.Message}";
        }
        finally
        {
            IsDbBusy = false;
        }
    }

    private void HandleInvalidSubmit()
    {
        SuccessMessage = null;
        ErrorMessage = null;
    }
}