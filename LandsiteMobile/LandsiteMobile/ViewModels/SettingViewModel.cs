using LandsiteMobile.Domain;
using LandsiteMobile.Resources.Languages;
using LandsiteMobile.Views;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading;
using System.Windows.Input;
using Xamarin.CommunityToolkit.Helpers;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace LandsiteMobile.ViewModels
{
    class SettingViewModel : BaseViewModel
    {
        #region Properties
        private bool isExpert;
        private static string _currentLanguage;

        public bool IsExpert { get => isExpert; set => SetProperty(ref isExpert, value); }
        #endregion

        #region Command 
        public ICommand AccountSettingCommand => new Command(async () =>
        {
            await Shell.Current.GoToAsync(nameof(AccountSettingPage));
        });

        public ICommand PageAppearingCommand => new Command(async () =>
        {
            _currentLanguage = Preferences.Get("language", "");
        });

        public ICommand ExpertCommand => new Command(() => IsExpert = !IsExpert);

        public ICommand LanguageCommand => new Command(async () =>
        {
            if (_currentLanguage == "")
                _currentLanguage = "vi";
            else
                _currentLanguage = "";

            ChangeLanguage(_currentLanguage);
        });

        public ICommand AboutCommand => new Command(async () =>
        {
            await Shell.Current.GoToAsync(nameof(AboutPage));
        });
        #endregion

        public SettingViewModel()
        {
            Title = "Settings";
        }

        #region Method
        void ChangeLanguage(string lang)
        {
            Preferences.Set("language", lang);
            LocalizationResourceManager.Current.SetCulture(lang == null ? CultureInfo.CurrentCulture : new CultureInfo(lang));
        }
        #endregion
    }
}
