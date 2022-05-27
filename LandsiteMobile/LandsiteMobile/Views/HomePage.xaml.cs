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
        public HomePage()
        {
            InitializeComponent();
            init();
        }

        [Obsolete]
        void init()
        {
            getCurrentLocation();
        }

        [Obsolete]
        async void getCurrentLocation()
        {
            var locator = CrossGeolocator.Current;
            map.IsShowingUser = true;
            double zoomLevel = 10;
            double latlongDegrees = 360 / (Math.Pow(2, zoomLevel));
            var position = await locator.GetPositionAsync(10000);
            map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(21.023857, 105.789279),
                                                         Distance.FromMiles(latlongDegrees)));
            map.Pins.Add(new Pin { Position = new Position(21.023857, 105.789279), Label = "SAMSUNG" });
        }
    }
}
