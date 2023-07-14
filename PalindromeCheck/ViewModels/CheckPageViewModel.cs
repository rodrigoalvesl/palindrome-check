using Prism.Navigation;

namespace PalindromeCheck.ViewModels
{
    class CheckPageViewModel : ViewModelBase
    {
        public CheckPageViewModel(INavigationService navigationService) : base(navigationService) { }
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            EntryText = parameters.GetValue<string>("EntryText");
            ResultText = parameters.GetValue<string>("ResultText");
        }
    }
}
