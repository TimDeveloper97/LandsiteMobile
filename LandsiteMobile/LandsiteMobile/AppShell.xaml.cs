using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LandsiteMobile.Services.Temp;
using LandsiteMobile.Views;
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
            Routing.RegisterRoute(nameof(DetailAccountPage), typeof(DetailAccountPage));
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
