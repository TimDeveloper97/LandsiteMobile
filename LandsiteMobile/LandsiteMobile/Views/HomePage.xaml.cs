using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using LandsiteMobile.Resources.Languages;
using Plugin.Geolocator;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.Xaml;

namespace LandsiteMobile.Views
{
    [DesignTimeVisible(false)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        static int index = -1;
        public HomePage()
        {
            InitializeComponent();
        }
        //private void picker_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (index == picker.SelectedIndex)
        //        return;
        //    index = picker.SelectedIndex;
        //    CultureInfo language = new CultureInfo(picker.SelectedIndex == 0 ? "" : "vi");
        //    Thread.CurrentThread.CurrentUICulture = language;
        //    LanguageResource.Culture = language;
        //    Application.Current.MainPage = new AppShell();
        //}

        void init()
        {
            getCurrentLocation();
        }

        async void getCurrentLocation()
        {
            var locator = CrossGeolocator.Current;
            var position = await locator.GetPositionAsync(TimeSpan.FromSeconds(10));
            map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(position.Latitude, position.Longitude),
                                                         Distance.FromMiles(1)));
        }
    }
}
