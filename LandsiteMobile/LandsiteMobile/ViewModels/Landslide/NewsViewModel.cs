using LandsiteMobile.Domain;
using LandsiteMobile.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace LandsiteMobile.ViewModels.Landslide
{
    class NewsViewModel : BaseViewModel
    {
        #region Properties
        private ObservableCollection<PinView> news;

        public ObservableCollection<PinView> News { get => news; set => SetProperty(ref news, value); }

        #endregion

        #region Command 
        public ICommand PageAppearingCommand => new Command(() =>
        {
            IsBusy = true;
        });

        public ICommand LoadNewsCommand { get; set; }

        #endregion

        public NewsViewModel()
        {
            Init();
            //////////////////////////////////
            LoadNewsCommand = new Command(async () => await ExecuteLoadNewsItemCommand());
        }


        #region Method
        void Init()
        {
            News = new ObservableCollection<PinView>();
        }

        async Task ExecuteLoadNewsItemCommand()
        {
            IsBusy = true;

            try
            {
                News?.Clear();

                var lpin = (await _firebaseDatabase.Child("Pins").OnceAsync<ResponcePin>())
                                .Select(x => new ResponcePin
                                {
                                    Damages = x.Object.Damages,
                                    Hill = x.Object.Hill,
                                    Vegetation = x.Object.Vegetation,
                                    Water = x.Object.Water,
                                    Material = x.Object.Material,
                                    Landslide = x.Object.Landslide,
                                    Longitude = x.Object.Longitude,
                                    Latitude = x.Object.Latitude,
                                    Title = x.Object.Title,
                                    Measure = x.Object.Measure,
                                    System = x.Object.System,
                                    Start = x.Object.Start,
                                    Finish = x.Object.Finish,
                                    HasFixed = x.Object.HasFixed,
                                });
                foreach (var pin in lpin)
                {
                    var cview = new PinView
                    {
                        ColorCard = pin.HasFixed ? "#1fb141" : "#ff471a",
                        Landslide = pin.Landslide,
                        Start = pin.Start,
                        Finish = pin.Finish == DateTime.MinValue ? null : pin.Finish,
                        Latitude = pin.Latitude,
                        Longitude = pin.Longitude,
                        Hill = pin.Hill,
                        Damages = pin.Damages,
                        Vegetation = pin.Vegetation,
                        Water = pin.Water,
                        Material = pin.Material,
                        Measure = pin.Measure ?? new ResponceType { Option = "No" },
                        System = pin.System ?? new ResponceType { Option = "No" },
                    };

                    News.Add(cview);
                }

            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
        #endregion
    }
}
