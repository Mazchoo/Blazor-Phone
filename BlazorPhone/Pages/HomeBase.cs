using Microsoft.AspNetCore.Components;
using System.Text;

namespace BlazorPhone.Pages
{
    public class HomeBase : ComponentBase
    {
        protected IEnumerable<string> mKeys { get; set; } = new[]
        {
            "1 [&'()]", "2 [ABC] ", "3 [DEF] ",
            "4 [GHI] ", "5 [JKL] ", "6 [MNO] ",
            "7 [PQRS]", "8 [TUV] ", "9 [WXYZ]",
            "*       ", "0 [ _ ] ", "#       "
        };

        protected StringBuilder mPressedKeys { get; set; } = new StringBuilder();
        protected StringBuilder mOutputWord { get; set; } = new StringBuilder();

        protected void HandleClick(string key)
        {
            char c = key[0];
            mPressedKeys.Append(c);

            if (c == '#') {
                PhonePadParser.ParsePressedKeysToLetters(mPressedKeys);
                mOutputWord = mPressedKeys;
                mPressedKeys = new StringBuilder();
            }

            StateHasChanged();
        }
    }
}