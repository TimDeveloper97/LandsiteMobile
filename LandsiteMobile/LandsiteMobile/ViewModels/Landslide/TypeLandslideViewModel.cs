using LandsiteMobile.Domain;
using LandsiteMobile.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace LandsiteMobile.ViewModels.Landslide
{
    [QueryProperty(nameof(ValueLandslide), nameof(ValueLandslide))]
    class TypeLandslideViewModel : BaseViewModel
    {
        #region Properties
        private double widthCard, heightCard;
        private string valueLandslide;
        ObservableCollection<TypeLandslideModel> _typeLandslides;


        public string ValueLandslide
        {
            get => valueLandslide; set
            {
                valueLandslide = Uri.UnescapeDataString(value ?? string.Empty);
                SetProperty(ref valueLandslide, value);
            }
        }
        public ObservableCollection<TypeLandslideModel> TypeLandslides
        {
            get => _typeLandslides;

            set => SetProperty(ref _typeLandslides, value);
        }
        public double WidthCard { get => widthCard; set => SetProperty(ref widthCard, value); }
        public double HeightCard { get => heightCard; set => SetProperty(ref heightCard, value); }
        #endregion

        #region Command 
        public ICommand PageAppearingCommand => new Command(async () =>
        {
            if(!string.IsNullOrEmpty(ValueLandslide))
            {
                foreach (var item in TypeLandslides)
                {
                    if (item.Type == ValueLandslide)
                        item.IsCheck = true;
                }
            }
            
        });

        public ICommand CheckCommand => new Command(async () =>
        {
            await Shell.Current.GoToAsync($"..?{nameof(LandsiteViewModel.ValueLandslide)}={ValueLandslide}");

        });

        public ICommand SelectTypeLandslideCommand => new Command<TypeLandslideModel>(async (model) =>
        {
            foreach (var item in TypeLandslides)
            {
                item.IsCheck = false;
            }
            model.IsCheck = true;
            ValueLandslide = model.Type;
        });

        
        #endregion


        public TypeLandslideViewModel()
        {
            TypeLandslides = new ObservableCollection<TypeLandslideModel>();
            init();
        }

        async void init()
        {
            TypeLandslides.Add(new TypeLandslideModel { Image = "rockfall", Type = "Rockkfall" });
            TypeLandslides.Add(new TypeLandslideModel { Image = "topple", Type = "Toppling" });
            TypeLandslides.Add(new TypeLandslideModel { Image = "rotationallandslide", Type = "Rotational slide" });
            TypeLandslides.Add(new TypeLandslideModel { Image = "translationallandslide", Type = "Planar slide" });

            TypeLandslides.Add(new TypeLandslideModel { Image = "debrisflow", Type = "Debris flow" });
            TypeLandslides.Add(new TypeLandslideModel { Image = "earthflow", Type = "Earth flow" });
            TypeLandslides.Add(new TypeLandslideModel { Image = "complexlandslide", Type = "Complex landslide" });
            TypeLandslides.Add(new TypeLandslideModel { Image = "otherlandslide", Type = "Other" });

            WidthCard = (App.ScreenWidth >> 1) - 50;
            HeightCard = WidthCard * 1.3;
        }
    }
}
