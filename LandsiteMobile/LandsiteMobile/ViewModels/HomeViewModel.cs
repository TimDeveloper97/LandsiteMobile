using System;
using System.Collections.Generic;
using System.Text;
using LandsiteMobile.Domain;
using LandsiteMobile.Services.Temp;
using Xamarin.Forms;

namespace LandsiteMobile.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        #region Properties
        public ISomeService _someService => DependencyService.Get<ISomeService>();
        #endregion

        public HomeViewModel()
        {
            Title = "Home Page";
            _someService.SomeMethod();
        }
    }
}
