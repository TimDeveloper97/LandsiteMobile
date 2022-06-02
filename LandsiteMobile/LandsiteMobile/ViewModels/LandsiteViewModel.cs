using LandsiteMobile.Controls;
using LandsiteMobile.Domain;
using LandsiteMobile.Resources.Languages;
using LandsiteMobile.ViewModels.Landslide;
using LandsiteMobile.Views.Landslide;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using XF.Material.Forms.UI.Dialogs;
using XF.Material.Forms.UI.Dialogs.Configurations;

namespace LandsiteMobile.ViewModels
{
    [QueryProperty(nameof(ValueLandslide), nameof(ValueLandslide))]
    class LandsiteViewModel : BaseViewModel
    {
        #region Properties 
        private string valueLandslide, valueMaterial, valueNote, valueSystem, valueWater, valueHill, valueVegetation, valueMeasures, valueDamages;
        private ImageSource source;
        private bool isTaked;

        public string ValueLandslide
        {
            get => valueLandslide; set
            {
                valueLandslide = Uri.UnescapeDataString(value ?? string.Empty);
                SetProperty(ref valueLandslide, value);
                OnPropertyChanged();
            }
        }
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
            await Shell.Current.GoToAsync($"{nameof(TypeLandslidePage)}?{nameof(TypeLandslideViewModel.ValueLandslide)}={ValueLandslide}");
        });

        public ICommand TypeMaterialCommand => new Command(async () =>
        {
            //Create choices
            var materials = new string[]
            {
                "Rock", "Debris", "Dỉrt", "Mixed", "Cannot determine",
            };
            var result = await ShowDialog(LanguageResource.homeMaterialPlaceholder, materials);
            if (result < 0) return;

            ValueMaterial = materials[result];
        });

        public ICommand HillCommand => new Command(async () =>
        {
            //Create choices
            var hills = new string[]
            {
                "At the top", "Uphill", "Midslope", "Downhill", "In the valley",
            };
            var result = await ShowDialog(LanguageResource.homeHillPlaceholder, hills);
            if (result < 0) return;

            ValueHill = hills[result];
        });

        public ICommand WaterCommand => new Command(async () =>
        {
            //Create choices
            var waters = new string[]
            {
                "Dry", "Humid", "Wet", "Very wet", "Cannot determine",
            };
            var result = await ShowDialog(LanguageResource.homeWaterPlaceholder, waters);
            if (result < 0) return;

            ValueWater = waters[result];
        });

        public ICommand VegetationCommand => new Command(async () =>
        {
            //Create choices
            var vegetations = new string[]
            {
                "Grass", "Low-growing plants", "Trees", "Mixed", "Absent", 
            };
            var result = await ShowDialog(LanguageResource.homeVegetationPlaceholder, vegetations);
            if (result < 0) return;

            ValueVegetation = vegetations[result];
        });

        public ICommand MeasuresCommand => new Command(async () =>
        {
            await Shell.Current.GoToAsync(nameof(TypeOptionView));
        });

        public ICommand SystemCommand => new Command(async () =>
        {
            await Shell.Current.GoToAsync(nameof(TypeOptionView));
        });

        public ICommand DamagesCommand => new Command(async () =>
        {
            //Create choices
            var damages = new string[]
            {
                "No damages", "Direct damage", "Obstruction of water course", "Collapsed bank or dam", "Cannot determine",
            };
            var result = await ShowDialog(LanguageResource.homeDamagesPlaceholder, damages);
            if (result < 0) return;

            ValueDamages = damages[result];
        });

        public ICommand NoteCommand => new Command(async () =>
        {
            var config = new MaterialInputDialogConfiguration
            {
                ButtonAllCaps = true,
                InputMaxLength = 500,
                TintColor = Color.FromHex("#d1542f"),
                InputType = XF.Material.Forms.UI.MaterialTextFieldInputType.Text,
            };
            var input = await MaterialDialog.Instance.InputAsync(title: LanguageResource.homeNotePlaceholder, inputPlaceholder: " Max 500 characters...", configuration: config);
            if (string.IsNullOrEmpty(input)) return;

            ValueNote = input;
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

        async Task<int> ShowDialog(string title, string[] inputs)
        {
            var config = new MaterialConfirmationDialogConfiguration { ButtonAllCaps = true, ControlSelectedColor = Color.FromHex("#d1542f"), TintColor = Color.FromHex("#d1542f"), };

            return await MaterialDialog.Instance.SelectChoiceAsync(title: title,
                                                                         choices: inputs, configuration: config);
        }
    }
}
