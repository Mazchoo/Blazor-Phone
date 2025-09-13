using Microsoft.AspNetCore.Components;

namespace BlazorPhone.Pages
{
    public class HomeBase : ComponentBase
    {
        protected IEnumerable<string> Keys { get; set; } = new[]
        {
            "1", "2", "3",
            "4", "5", "6",
            "7", "8", "9",
            "*", "0", "#"
        };

        protected void HandleClick(string key)
        {
            Console.WriteLine($"Button {key} clicked");
        }
    }
}