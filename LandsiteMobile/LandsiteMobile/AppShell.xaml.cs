using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LandsiteMobile.Controls;
using LandsiteMobile.Services.Temp;
using LandsiteMobile.Views;
using LandsiteMobile.Views.Landslide;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LandsiteMobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            InitDependencyService();
            InitRoute();
        }

        void InitDependencyService()
        {
            DependencyService.Register<SomeService>();
        }

        void InitRoute()
        {
            Routing.RegisterRoute(nameof(SignUpPage), typeof(SignUpPage));
            Routing.RegisterRoute(nameof(PolicyPage), typeof(PolicyPage));
            Routing.RegisterRoute(nameof(SettingPage), typeof(SettingPage));
            Routing.RegisterRoute(nameof(AccountSettingPage), typeof(AccountSettingPage));
            Routing.RegisterRoute(nameof(LandsitePage), typeof(LandsitePage));
            Routing.RegisterRoute(nameof(DetailAccountPage), typeof(DetailAccountPage));
            Routing.RegisterRoute(nameof(TypeLandslidePage), typeof(TypeLandslidePage));
            Routing.RegisterRoute(nameof(TypeOptionView), typeof(TypeOptionView));
            Routing.RegisterRoute(nameof(TypeMeasurePage), typeof(TypeMeasurePage));
            Routing.RegisterRoute(nameof(TypeSystemPage), typeof(TypeSystemPage));
            Routing.RegisterRoute(nameof(InfoLandslidePage), typeof(InfoLandslidePage));
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
