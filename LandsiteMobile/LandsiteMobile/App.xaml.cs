using LandsiteMobile.Resources.Languages;
using System;
using Xamarin.CommunityToolkit.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LandsiteMobile
{
    public partial class App : Application
    {
        public static int ScreenHeight { get; set; }
        public static int ScreenWidth { get; set; }
        public App()
        {
            InitializeComponent();
            XF.Material.Forms.Material.Init(this);

            LocalizationResourceManager.Current.PropertyChanged += Current_PropertyChanged;
            LocalizationResourceManager.Current.Init(LanguageResource.ResourceManager);

            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        private void Current_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            LanguageResource.Culture = LocalizationResourceManager.Current.CurrentCulture;
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
