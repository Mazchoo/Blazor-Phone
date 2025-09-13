using Microsoft.AspNetCore.Components;

namespace BlazorPhone.Pages
{
    public class HomeBase : ComponentBase
    {
        protected void HandleClick()
        {
            Console.WriteLine("Button clicked from HomeBase.cs!");
        }
    }
}