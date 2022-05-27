using LandsiteMobile.Domain;
using LandsiteMobile.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace LandsiteMobile.ViewModels
{
    class PolicyViewModel : BaseViewModel
    {
        #region Command 
        public ICommand AcceptCommand => new Command(async () =>
        {
            await Shell.Current.GoToAsync(nameof(SignUpPage));
        });
        public ICommand GoBackCommand => new Command(async () =>
        {
            await Shell.Current.GoToAsync("..");
        });
        #endregion

        public PolicyViewModel()
        {
            Title = "Policy";
        }

    }
}
