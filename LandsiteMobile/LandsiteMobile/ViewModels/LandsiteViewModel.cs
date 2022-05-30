using LandsiteMobile.Domain;
using LandsiteMobile.Resources.Languages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace LandsiteMobile.ViewModels
{
    class LandsiteViewModel : BaseViewModel
    {
        #region Command 
        private string valueLandslide, valueMaterial, valueNote, valueSystem, valueWater, valueHill, valueVegetation, valueMeasures, valueDamages;
        private ImageSource source;
        private bool isTaked;

        public string ValueLandslide { get => valueLandslide; set => SetProperty(ref valueLandslide, value); }
        public string ValueMaterial { get => valueMaterial; set => SetProperty(ref valueMaterial, value); }
        public string ValueNote { get => valueNote; set => SetProperty(ref valueNote, value); }
        public string ValueSystem { get => valueSystem; set => SetProperty(ref valueSystem, value); }
        public string ValueWater { get => valueWater; set => SetProperty(ref valueWater, value); }
        public string ValueHill { get => valueHill; set => SetProperty(ref valueHill, value); }
        public string ValueVegetation { get => valueVegetation; set => SetProperty(ref valueVegetation, value); }
        public string ValueMeasures { get => valueMeasures; set => SetProperty(ref valueMeasures, value); }
        public string ValueDamages { get => valueDamages; set { SetProperty(ref valueDamages, value); } }
        public ImageSource Source { get => source; set { SetProperty(ref source, value); IsTaked = Source == null ? false : true; } }
        public bool IsTaked { get => isTaked; set { SetProperty(ref isTaked, value); } }

        #endregion

        #region Command 
        public ICommand PageAppearingCommand => new Command(async () =>
        {
            IsTaked = false;
        });

        public ICommand TypeLandslideCommand => new Command(async () =>
        {
            await Shell.Current.DisplayAlert("TypeLandslideCommand", "Click", "Cancle");
        });

        public ICommand RemoveImageCommand => new Command(() => Source = null);

        public ICommand TakePictureCommand => new Command(async () =>
        {
            var result = await MediaPicker.CapturePhotoAsync();

            if (result != null)
            {
                var stream = await result.OpenReadAsync();
                Source = ImageSource.FromStream(() => stream);
            }
        });


        #endregion

        public LandsiteViewModel()
        {
            Title = "New landslide";
            ValueLandslide = LanguageResource.homeLandslidePlaceholder;
            ValueMaterial = LanguageResource.homeMaterialPlaceholder;
            ValueNote = LanguageResource.homeNotePlaceholder;
            ValueSystem = LanguageResource.homeSystemPlaceholder;
            ValueWater = LanguageResource.homeWaterPlaceholder;
            ValueHill = LanguageResource.homeHillPlaceholder;
            ValueVegetation = LanguageResource.homeVegetationPlaceholder;
            ValueMeasures = LanguageResource.homeMeasuresPlaceholder;
            ValueDamages = LanguageResource.homeDamagesPlaceholder;
        }
    }
}
