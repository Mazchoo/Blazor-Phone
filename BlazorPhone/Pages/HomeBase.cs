using Microsoft.AspNetCore.Components;
using System.Text;

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

        protected StringBuilder mPressedKeys { get; set; } = new StringBuilder();
        protected StringBuilder mOutputWord { get; set; } = new StringBuilder();

        protected void HandleClick(string key)
        {
            mPressedKeys.Append(key);

            if (key == "#") {
                mOutputWord = PhonePadParser.ParsePressedKeysToLetters(mPressedKeys);
                mPressedKeys = new StringBuilder();
            }

            StateHasChanged();
        }
    }
}