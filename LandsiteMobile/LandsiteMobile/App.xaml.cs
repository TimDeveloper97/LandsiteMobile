using System;
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
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
