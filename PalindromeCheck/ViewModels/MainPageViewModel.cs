using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PalindromeCheck.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private bool _isEnabled;
        public bool IsEnabled
        {
            get { return _isEnabled; }
            set
            {
                SetProperty(ref _isEnabled, value);
            }
        }

        private DelegateCommand<string> _textChangedCommand;
        public DelegateCommand<string> TextChangedCommand =>
          _textChangedCommand ?? (_textChangedCommand = new DelegateCommand<string>(TextChanged));

        public DelegateCommand CheckCommand { get; private set; }
        public DelegateCommand EraseCommand { get; private set; }

        public MainPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            CheckCommand = new DelegateCommand(async () => await Check());
            EraseCommand = new DelegateCommand(Erase);
        }

        private void Erase()
        {
            EntryText = string.Empty;
        }

        public async Task Check()
        {
            PalindromeCheck();

            var param = new NavigationParameters
            {
                { "EntryText", EntryText },
                { "ResultText", ResultText }
            };

            await NavigationService.NavigateAsync("CheckPage", param);
        }

        public void TextChanged(string p)
        {
            if (string.IsNullOrWhiteSpace(EntryText))
                IsEnabled = false;
            else
                IsEnabled = true;
        }

        private void PalindromeCheck()
        {
            if (ResultPalindrome())
                ResultText = "This text is a palindrome.";
            else
                ResultText = "This text is not a palindrome!";
        }

        private bool ResultPalindrome()
        {
            string trimText = Regex.Replace(EntryText.ToLower(), @"\s+", string.Empty);
            var listChar = (IEnumerable<char>)trimText.ToList();

            return trimText.SequenceEqual(listChar.Reverse());
        }
    }
}
