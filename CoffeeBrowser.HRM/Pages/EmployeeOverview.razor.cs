using CoffeeBrowser.HRM.Data;
using CoffeeBrowser.HRM.Data.Models;
using CoffeeBrowser.HRM.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop;
using System.Data;

namespace CoffeeBrowser.HRM.Pages;

public partial class EmployeeOverview
{
    [Parameter]
    public int? CurrentPage { get; set; }
    [Inject]
    protected NavigationManager NavigationManager { get; set; } = default!;
    private int TotalPages { get; set; }
    private Employee[]? Employees { get; set; }
    private const int _itemsPerPage = 3;

    protected override async Task OnParametersSetAsync()
    {
        await LoadData();
    }

    private async void HandleDelete(Employee employee)
    {
        bool isToDelete = await JS.InvokeAsync<bool>("confirm",
          $"Delete employee {employee.FirstName} {employee.LastName}?");

        if (isToDelete)
        {
            try
            {
                using EmployeeHRMDbContext context =
                ContextFactory.CreateDbContext();
                context.Employees.Remove(employee);
                await context.SaveChangesAsync();
            }
            catch (DBConcurrencyException) {; }
        }

        await LoadData();
    }

    private async Task LoadData()
    {
        if (CurrentPage is null or < 1)
        {
            NavigationManager.NavigateTo("/employees/list/1");
            return;
        }

        EmployeeHRMDbContext context = ContextFactory.CreateDbContext();
        int employeeCount = await context.Employees.CountAsync();
        TotalPages = employeeCount > 0 ?
        (int)Math.Ceiling((double)employeeCount / _itemsPerPage) : 1;

        if (CurrentPage > TotalPages)
        {
            NavigationManager.NavigateTo($"/employees/list/{TotalPages}");
            return;
        }

        StateContainer.EmployeeOverviewPage = CurrentPage.Value;

        int itemsToSkip = (CurrentPage.Value - 1) * _itemsPerPage;
        Employees = await context.Employees
          .Include(emp => emp.Department)
          .OrderBy(emp => emp.FirstName)
          .Skip(itemsToSkip)
          .Take(_itemsPerPage)
          .ToArrayAsync();
    }
}
