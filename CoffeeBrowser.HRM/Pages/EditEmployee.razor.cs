using CoffeeBrowser.HRM.Data.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace CoffeeBrowser.HRM.Pages;

public partial class EditEmployee
{
    [Parameter]
    public int EmployeeId { get; set; }
    [Inject]
    protected NavigationManager NavigationManager { get; set; } = default!;
    private Employee? Employee { get; set; }
    private Department[]? Departments { get; set; }
    private bool IsDbBusy { get; set; }
    private string? ErrorMessage { get; set; }
    
    protected override async Task OnParametersSetAsync()
    {
        IsDbBusy = true;
        try
        {
            using Data.EmployeeHRMDbContext context =
                        ContextFactory.CreateDbContext();
            Departments = await context.Departments
                .AsNoTracking()
                .OrderBy(dept => dept.Name)
                .ToArrayAsync();
            Employee = await context.Employees
                .AsNoTracking()
                .FirstOrDefaultAsync(emp => emp.Id == EmployeeId);
        }
        finally
        {
            IsDbBusy = false;
        }
    }

    private async Task HandleSubmit(bool isValid)
    {
        if (!isValid || Employee is null || IsDbBusy)
        {
            ErrorMessage = null;
            return;
        }

        try
        {
            IsDbBusy = true;

            using Data.EmployeeHRMDbContext context =
                    ContextFactory.CreateDbContext();
            context.Update(Employee);
            await context.SaveChangesAsync();

            NavigateToOverviewPage();
        }
        catch (DBConcurrencyException)
        {
            ErrorMessage = "The employee was modified by another user. " +
                "Please reload this page.";
        }
        catch (Exception exception)
        {
            ErrorMessage = $"Error while saving employee:" +
                $"{exception.Message}";
        }
        finally
        { IsDbBusy = false; }
    }

    private void NavigateToOverviewPage()
    {
        NavigationManager.NavigateTo($"/employees/list/" +
            $"{StateContainer.EmployeeOverviewPage}");
    }
}
