using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using LandsiteMobile.Domain;
using LandsiteMobile.Services.Temp;
using LandsiteMobile.Views;
using Plugin.Geolocator;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using XF.Material.Forms.UI.Dialogs;

namespace LandsiteMobile.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        #region Properties
        public ISomeService _someService => DependencyService.Get<ISomeService>();
        ObservableCollection<Pin> _pins;
        MapSpan clinicMapSpan;

        public ObservableCollection<Pin> Pins
        {
            get => _pins;

            set => SetProperty(ref _pins, value);
        }
        public MapSpan ClinicMapSpan { get => clinicMapSpan; set => SetProperty(ref clinicMapSpan, value); }
        #endregion

        #region Command 
        public ICommand PageAppearingCommand => new Command(async () =>
        {
            Pins?.Clear();
            init();
        });

        public ICommand LocationCommand => new Command(async () =>
        {
            init();
        });

        public ICommand LoadingCommand => new Command(async () =>
        {
            var list = (await _firebaseDatabase
              .Child("Pins")
              .OnceAsync<Pin>()).Select(item => new Pin
              {
                  
              }).ToList();
        });

        public ICommand SettingCommand => new Command(async () =>
        {
            await Shell.Current.GoToAsync(nameof(SettingPage));
        });
        #endregion


        public HomeViewModel()
        {
            Title = "Home Page";
            _someService.SomeMethod();
            Pins = new ObservableCollection<Pin>();
        }

        async void init()
        {
            try
            {
                var locator = CrossGeolocator.Current;
                double latlongDegrees = 360 / Math.Pow(2, 10);
                var position = await locator.GetPositionAsync(10000);

                Pin clinicPin = new Pin()
                {
                    Type = PinType.SavedPin,
                    Label = "My location",
                    Position = new Position(position.Latitude, position.Longitude),
                };


                ClinicMapSpan = MapSpan.FromCenterAndRadius(new Position(clinicPin.Position.Latitude, clinicPin.Position.Longitude), Distance.FromMiles(latlongDegrees));
                Pins.Add(clinicPin);
            }
            catch (Exception e)
            {
                await MaterialDialog.Instance.SnackbarAsync(message: "Can't load map",
                                     msDuration: MaterialSnackbar.DurationLong);
            }
        }
    }
}
