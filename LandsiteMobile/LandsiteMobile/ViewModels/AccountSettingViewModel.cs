using LandsiteMobile.Domain;
using LandsiteMobile.Resources.Languages;
using LandsiteMobile.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace LandsiteMobile.ViewModels
{
    class AccountSettingViewModel : BaseViewModel
    {
        #region Command 
        public ICommand DetailAccountCommand => new Command(async () =>
        {
            await Shell.Current.GoToAsync(nameof(DetailAccountPage));
        });

        public ICommand LogoutCommand => new Command(async () =>
        {
            await Shell.Current.GoToAsync("//LoginPage");
        });
        #endregion

        public AccountSettingViewModel()
        {
            Title = LanguageResource.settingAccount;
        }
    }
}
