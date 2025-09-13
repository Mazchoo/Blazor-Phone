using Microsoft.AspNetCore.Components;

namespace BlazorPhone.Pages
{
    public class HomeBase : ComponentBase
    {
        protected IEnumerable<string> mKeys { get; set; } = new[]
        {
            "1", "2", "3",
            "4", "5", "6",
            "7", "8", "9",
            "*", "0", "#"
        };

        protected string mPressedKeys { get; set; } = string.Empty;
        protected string mOutputWord { get; set; } = string.Empty;

        protected void HandleClick(string key)
        {
            mPressedKeys += key;
            StateHasChanged();
            Console.WriteLine($"Button {key} clicked");
        }
    }
}