using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Firebase.Database;
using LandsiteMobile.Domain;
using LandsiteMobile.Models;
using LandsiteMobile.Resources.Languages;
using LandsiteMobile.Services.Temp;
using LandsiteMobile.ViewModels.Landslide;
using LandsiteMobile.Views;
using LandsiteMobile.Views.Landslide;
using Newtonsoft.Json;
using Plugin.Geolocator;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using XF.Material.Forms.UI.Dialogs;

namespace LandsiteMobile.ViewModels
{
    [QueryProperty(nameof(ParameterPinId), nameof(ParameterPinId))]
    public class HomeViewModel : BaseViewModel
    {
        #region Properties
        public ISomeService _someService => DependencyService.Get<ISomeService>();
        ObservableCollection<Pin> _pins;
        MapSpan clinicMapSpan;
        private string parameterPinId;
        private ResponcePosition position;
        private bool hasPosition;

        public ObservableCollection<Pin> Pins
        {
            get => _pins;
            set => SetProperty(ref _pins, value);
        }
        public string ParameterPinId
        {
            get => parameterPinId; set
            {
                parameterPinId = Uri.UnescapeDataString(value ?? string.Empty);
                SetProperty(ref parameterPinId, value);
                if (!string.IsNullOrEmpty(parameterPinId))
                {
                    Position = JsonConvert.DeserializeObject<ResponcePosition>(parameterPinId);
                    AddToMap();
                }
            }
        }
        public bool HasPosition
        {
            get => hasPosition; set
            {
                SetProperty(ref hasPosition, value);
            }
        }
        public ResponcePosition Position
        {
            get => position; set
            {
                SetProperty(ref position, value);
                HasPosition = Position == null ? false : true;
            }
        }

        public MapSpan ClinicMapSpan { get => clinicMapSpan; set => SetProperty(ref clinicMapSpan, value); }
        #endregion

        #region Command 
        public ObservableCollection<GroundOverlay> GroundOverlays { get; set; }
        public Command<MapClickedEventArgs> MapClickedCommand => new Command<MapClickedEventArgs>(
            args =>
            {
                Position = new ResponcePosition
                {
                    Latitude = args.Point.Latitude,
                    Longitude = args.Point.Longitude,
                };
            });

        public Command<InfoWindowClickedEventArgs> InfoWindowClickedCommand => new Command<InfoWindowClickedEventArgs>(
            async args =>
            {
                await Shell.Current.GoToAsync($"{nameof(InfoLandslidePage)}?{nameof(InfoLandslideViewModel.ParameterPinTag)}={args.Pin.Tag}");
            });

        public ICommand PageAppearingCommand => new Command(async () =>
        {
            Pins?.Clear();
            init();

            try
            {
                var list = (await _firebaseDatabase
                  .Child("Pins")
                  .OnceAsync<ResponcePin>()).Select(item => new ResponcePin
                  {
                      Damages = item.Object.Damages,
                      Hill = item.Object.Hill,
                      Vegetation = item.Object.Vegetation,
                      Water = item.Object.Water,
                      Material = item.Object.Material,
                      Landslide = item.Object.Landslide,
                      Longitude = item.Object.Longitude,
                      Latitude = item.Object.Latitude,
                      Notes = item.Object.Notes,
                      Title = item.Object.Title,
                      Tag = item.Object.Tag,
                      Measure = item.Object.Measure,
                      Photo = item.Object.Photo,
                      System = item.Object.System,
                  }).ToList();

                foreach (var item in list)
                {
                    if (item.HasFixed == false)
                        Pins.Add(new Pin
                        {
                            Tag = item.Tag,
                            Label = item.Title ?? "Landslide",
                            Position = new Position(item.Latitude, item.Longitude),
                            Type = PinType.SavedPin,
                        });
                }
            }
            catch (FirebaseException fe)
            {
                if (fe.ResponseData == "N/A")
                    await MaterialDialog.Instance.SnackbarAsync(message: LanguageResource.messageInternet,
                                     msDuration: MaterialSnackbar.DurationLong);
                else if (fe.ResponseData == "")
                    await MaterialDialog.Instance.SnackbarAsync(message: LanguageResource.messageTimeout,
                                     msDuration: MaterialSnackbar.DurationLong);
            }
        });

        public ICommand NewsCommand => new Command(async () =>
        {
            await Shell.Current.GoToAsync(nameof(NewsPage));
        });

        public ICommand LocationCommand => new Command(async () =>
        {
            init();
        });

        public ICommand LoadingCommand => new Command(async () =>
        {
            try
            {
                var list = (await _firebaseDatabase
                  .Child("Pins")
                  .OnceAsync<ResponcePin>()).Select(item => new ResponcePin
                  {
                      Damages = item.Object.Damages,
                      Hill = item.Object.Hill,
                      Vegetation = item.Object.Vegetation,
                      Water = item.Object.Water,
                      Material = item.Object.Material,
                      Landslide = item.Object.Landslide,
                      Longitude = item.Object.Longitude,
                      Latitude = item.Object.Latitude,
                      Notes = item.Object.Notes,
                      Title = item.Object.Title,
                      Tag = item.Object.Tag,
                      Measure = item.Object.Measure,
                      Photo = item.Object.Photo,
                      System = item.Object.System,
                  }).ToList();

                Pins?.Clear();
                foreach (var item in list)
                {
                    if (item.HasFixed == false)
                        Pins.Add(new Pin
                        {
                            Tag = item.Tag,
                            Label = item.Title ?? "Landslide",
                            Position = new Position(item.Latitude, item.Longitude),
                            Type = PinType.SavedPin,
                        });
                }
            }
            catch (FirebaseException fe)
            {
                if (fe.ResponseData == "N/A")
                    await MaterialDialog.Instance.SnackbarAsync(message: LanguageResource.messageInternet,
                                     msDuration: MaterialSnackbar.DurationLong);
                else if (fe.ResponseData == "")
                    await MaterialDialog.Instance.SnackbarAsync(message: LanguageResource.messageTimeout,
                                     msDuration: MaterialSnackbar.DurationLong);
            }
        });

        public ICommand NewLandslideCommand => new Command(async () =>
        {
            var data = JsonConvert.SerializeObject(Position);
            await Shell.Current.GoToAsync($"{nameof(LandsitePage)}?{nameof(LandsiteViewModel.ParameterPinId)}={data}");
        });

        public ICommand SettingCommand => new Command(async () =>
        {
            await Shell.Current.GoToAsync(nameof(SettingPage));
        });

        #endregion


        public HomeViewModel()
        {
            Position = null;
            HasPosition = false;
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

        void AddToMap()
        {
            double latlongDegrees = 360 / Math.Pow(2, 10);
            Pin clinicPin = new Pin()
            {
                Type = PinType.SavedPin,
                Label = Position.Title ?? "Landslide",
                Tag = Position.Tag,
                Position = new Position(Position.Latitude, Position.Longitude),
            };

            ClinicMapSpan = MapSpan.FromCenterAndRadius(new Position(clinicPin.Position.Latitude, clinicPin.Position.Longitude), Distance.FromMiles(latlongDegrees));
            Pins.Add(clinicPin);
        }
    }
}
