using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FinalLabTask1.Pages;

public class Index : PageModel
{
    public void OnGet()
    {
        
    }
    
    [BindProperty]
    public string FromAccountId { get; set; }
    [BindProperty]
    public string ToAccountId { get; set; }
    [BindProperty]
    public decimal Amountt { get; set; }
    public string ResultMessage { get; set; }

    public async Task<ActionResult> OnPostAsync()
    {
        var transferData = new
        {
            userId = this.FromAccountId,
        };

        using var client = new HttpClient();
        var response = await client.PostAsJsonAsync($"https://localhost:5198/api/accounts/balance-summary/{FromAccountId}", transferData);

        if (response.IsSuccessStatusCode)
        {
            ResultMessage = await response.Content.ReadAsStringAsync();
        }
        else
        {
            ResultMessage = "Transfer failed";
        }
        return Page();
    }
}