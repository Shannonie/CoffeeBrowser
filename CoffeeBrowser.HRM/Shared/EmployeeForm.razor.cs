using CoffeeBrowser.HRM.Data.Models;
using Microsoft.AspNetCore.Components;

namespace CoffeeBrowser.HRM.Shared;

public partial class EmployeeForm
{
    [Parameter]
    public Employee? Employee { get; set; }

    [Parameter]
    public Department[]? Departments { get; set; }

    [Parameter]
    public bool IsDbBusy { get; set; }

    [Parameter]
    public bool IsToEdit { get; set; }

    [Parameter]
    public EventCallback OnCancel { get; set; }

    [Parameter]
    public EventCallback<bool> OnSubmit { get; set; }

    private async Task HandleValidSubmit()
    {
        if (OnSubmit.HasDelegate)
        {
            await OnSubmit.InvokeAsync(true);
        }
    }

    private async Task HandleInvalidSubmit()
    {
        if (OnSubmit.HasDelegate)
        {
            await OnSubmit.InvokeAsync(false);
        }
    }

    private async Task HandleCancel()
    {
        if (OnCancel.HasDelegate)
        {
            await OnCancel.InvokeAsync();
        }
    }
}
