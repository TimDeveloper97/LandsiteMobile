using Firebase.Database;
using Firebase.Database.Query;
using Firebase.Storage;
using LandsiteMobile.Controls;
using LandsiteMobile.Domain;
using LandsiteMobile.Models;
using LandsiteMobile.Resources.Languages;
using LandsiteMobile.ViewModels.Landslide;
using LandsiteMobile.Views.Landslide;
using Newtonsoft.Json;
using Plugin.Media;
using System;
using System.Collections.Generic;
using System.IO;
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
    [QueryProperty(nameof(ParameterResponceTypeMeasure), nameof(ParameterResponceTypeMeasure))]
    [QueryProperty(nameof(ParameterResponceTypeSystem), nameof(ParameterResponceTypeSystem))]
    [QueryProperty(nameof(ParameterPinId), nameof(ParameterPinId))]
    class LandsiteViewModel : BaseViewModel
    {
        #region Properties 
        private string valueLandslide, valueMaterial, valueNote, valueSystem, valueWater, valueHill, valueVegetation, valueMeasures, valueDamages;
        private string parameterResponceType1, parameterResponceType2;
        private ResponceType responceType1, responceType2;
        private string source;
        private string _url;
        private static ResponcePin _responcePin;
        private bool isTaked;
        private string parameterPinId;
        private ResponcePosition position;
        public string ParameterPinId
        {
            get => parameterPinId; set
            {
                parameterPinId = Uri.UnescapeDataString(value ?? string.Empty);
                SetProperty(ref parameterPinId, value);
                if (!string.IsNullOrEmpty(parameterPinId))
                {
                    Position = JsonConvert.DeserializeObject<ResponcePosition>(parameterPinId);
                    _responcePin.Latitude = Position.Latitude;
                    _responcePin.Longitude = Position.Longitude;
                }
            }
        }
        public ResponcePosition Position { get => position; set => SetProperty(ref position, value); }
        public string ValueLandslide
        {
            get => valueLandslide; set
            {
                valueLandslide = Uri.UnescapeDataString(value ?? string.Empty);
                SetProperty(ref valueLandslide, value);
                if (!string.IsNullOrEmpty(valueLandslide))
                    _responcePin.Landslide = valueLandslide;
                OnPropertyChanged();
            }
        }
        public ResponceType ResponceTypeMeasure
        {
            get => responceType1; set
            {
                SetProperty(ref responceType1, value);
            }
        }
        public ResponceType ResponceTypeSystem
        {
            get => responceType2; set
            {
                SetProperty(ref responceType2, value);
            }
        }
        public string ParameterResponceTypeMeasure
        {
            get => parameterResponceType1; set
            {
                parameterResponceType1 = Uri.UnescapeDataString(value ?? string.Empty);
                SetProperty(ref parameterResponceType1, value);
                if (!string.IsNullOrEmpty(parameterResponceType1))
                {
                    ResponceTypeMeasure = JsonConvert.DeserializeObject<ResponceType>(parameterResponceType1);
                    _responcePin.Measure = ResponceTypeMeasure;
                }
            }
        }
        public string ParameterResponceTypeSystem
        {
            get => parameterResponceType2; set
            {
                parameterResponceType2 = Uri.UnescapeDataString(value ?? string.Empty);
                SetProperty(ref parameterResponceType2, value);
                if (!string.IsNullOrEmpty(parameterResponceType2))
                {
                    ResponceTypeSystem = JsonConvert.DeserializeObject<ResponceType>(parameterResponceType2);
                    _responcePin.System = ResponceTypeSystem;
                }
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
        public string Source { get => source; set { SetProperty(ref source, value); IsTaked = Source == null ? false : true; } }
        public bool IsTaked { get => isTaked; set { SetProperty(ref isTaked, value); } }

        #endregion

        #region Command 
        public ICommand PageAppearingCommand => new Command(async () =>
        {
            IsTaked = false;
            if(!string.IsNullOrEmpty(_responcePin.Landslide))
                ValueLandslide = _responcePin.Landslide;
            if (!string.IsNullOrEmpty(_responcePin.Damages))
                ValueDamages = _responcePin.Damages;
            if (!string.IsNullOrEmpty(_responcePin.Hill))
                ValueHill = _responcePin.Hill;
            if (!string.IsNullOrEmpty(_responcePin.Material))
                ValueMaterial = _responcePin.Material;
            if (!string.IsNullOrEmpty(_responcePin.Notes))
                ValueNote = _responcePin.Notes;
            if (!string.IsNullOrEmpty(_responcePin.Vegetation))
                ValueVegetation = _responcePin.Vegetation;
            if (!string.IsNullOrEmpty(_responcePin.Water))
                ValueWater = _responcePin.Water;
            if (!string.IsNullOrEmpty(_responcePin.Photo))
                Source = _responcePin.Photo;
        });

        public ICommand TypeLandslideCommand => new Command(async () =>
        {
            await Shell.Current.GoToAsync($"{nameof(TypeLandslidePage)}?{nameof(TypeLandslideViewModel.ValueLandslide)}={_responcePin.Landslide}");
        });

        public ICommand TypeMaterialCommand => new Command(async () =>
        {
            //Create choices
            var materials = new string[]
            {
                LanguageResource.materialOption1,
                        LanguageResource.materialOption2,
                        LanguageResource.materialOption3,
                        LanguageResource.materialOption4,
                        LanguageResource.materialOption5,
            };
            var result = await ShowDialog(LanguageResource.homeMaterialPlaceholder, materials, FindIndex(materials, _responcePin.Material));
            if (result < 0) return;

            _responcePin.Material = ValueMaterial = materials[result];
        });

        public ICommand HillCommand => new Command(async () =>
        {
            //Create choices
            var hills = new string[]
            {
                LanguageResource.hillOption1,
                        LanguageResource.hillOption2,
                        LanguageResource.hillOption3,
                        LanguageResource.hillOption4,
                        LanguageResource.hillOption5,
            };
            var result = await ShowDialog(LanguageResource.homeHillPlaceholder, hills, FindIndex(hills, _responcePin.Hill));
            if (result < 0) return;

            _responcePin.Hill = ValueHill = hills[result];
        });

        public ICommand WaterCommand => new Command(async () =>
        {
            //Create choices
            var waters = new string[]
            {
                LanguageResource.waterOption1,
                        LanguageResource.waterOption2,
                        LanguageResource.waterOption3,
                        LanguageResource.waterOption4,
                        LanguageResource.waterOption5,
            };
            var result = await ShowDialog(LanguageResource.homeWaterPlaceholder, waters, FindIndex(waters, _responcePin.Water));
            if (result < 0) return;

            _responcePin.Water = ValueWater = waters[result];
        });

        public ICommand VegetationCommand => new Command(async () =>
        {
            //Create choices
            var vegetations = new string[]
            {
                LanguageResource.vegeOption1,
                        LanguageResource.vegeOption2,
                        LanguageResource.vegeOption3,
                        LanguageResource.vegeOption4,
                        LanguageResource.vegeOption5,
            };
            var result = await ShowDialog(LanguageResource.homeVegetationPlaceholder, vegetations, FindIndex(vegetations, _responcePin.Vegetation));
            if (result < 0) return;

            _responcePin.Vegetation = ValueVegetation = vegetations[result];
        });

        public ICommand MeasuresCommand => new Command(async () =>
        {
            var data = JsonConvert.SerializeObject(_responcePin.Measure ?? new ResponceType());
            await Shell.Current.GoToAsync($"{nameof(TypeMeasurePage)}?{nameof(TypeMeasureViewModel.ParameterResponceType)}={data}");
        });

        public ICommand SystemCommand => new Command(async () =>
        {
            var data = JsonConvert.SerializeObject(_responcePin.System ?? new ResponceType());
            await Shell.Current.GoToAsync($"{nameof(TypeSystemPage)}?{nameof(TypeSystemViewModel.ParameterResponceType)}={data}");
        });

        public ICommand DamagesCommand => new Command(async () =>
        {
            //Create choices
            var damages = new string[]
            {
                LanguageResource.damagesOption1,
                        LanguageResource.damagesOption2,
                        LanguageResource.damagesOption3,
                        LanguageResource.damagesOption4,
                        LanguageResource.damagesOption5,
            };
            var result = await ShowDialog(LanguageResource.homeDamagesPlaceholder, damages, FindIndex(damages, _responcePin.Damages));
            if (result < 0) return;

            _responcePin.Damages = ValueDamages = damages[result];
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
            var input = await MaterialDialog.Instance.InputAsync(title: LanguageResource.homeNotePlaceholder, inputPlaceholder: LanguageResource.messageMax, configuration: config);
            if (string.IsNullOrEmpty(input)) return;

            _responcePin.Notes = ValueNote = input;
        });

        public ICommand RemoveImageCommand => new Command(() => Source = null);

        public ICommand CheckCommand => new Command(async () =>
        {
            if (Source == null)
            {
                await MaterialDialog.Instance.SnackbarAsync(message: LanguageResource.messagePicture,
                                         msDuration: MaterialSnackbar.DurationLong);
            }
            else if (string.IsNullOrEmpty(ValueLandslide))
            {
                await MaterialDialog.Instance.SnackbarAsync(message: LanguageResource.messageLandslide,
                                         msDuration: MaterialSnackbar.DurationLong);
            }
            else
            {
                var loadingDialog = await MaterialDialog.Instance.LoadingDialogAsync(message: LanguageResource.messageUpload);
                try
                {
                    _responcePin.Title = _responcePin.Landslide;
                    _responcePin.Photo = _url;
                    _responcePin.Tag = Guid.NewGuid().ToString().Substring(0, 8);
                    _responcePin.Start = DateTime.Now;
                    _responcePin.Finish = DateTime.MinValue;

                    Position.Tag = _responcePin.Tag;
                    Position.Title = _responcePin.Landslide;
                    await _firebaseDatabase.Child("Pins").PostAsync(_responcePin);

                    var data = JsonConvert.SerializeObject(Position);
                    await Shell.Current.GoToAsync($"..?{nameof(HomeViewModel.ParameterPinId)}={data}");
                }
                catch (FirebaseStorageException fs)
                {
                    if (fs.ResponseData == "N/A")
                        await MaterialDialog.Instance.SnackbarAsync(message: LanguageResource.messageInternet,
                                         msDuration: MaterialSnackbar.DurationLong);
                    else
                        await MaterialDialog.Instance.SnackbarAsync(message: LanguageResource.messageTimeout,
                                             msDuration: MaterialSnackbar.DurationLong);
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
                finally
                {
                    await loadingDialog.DismissAsync();
                }


            }
        });

        public ICommand TakePictureCommand => new Command(async () =>
        {
            var result = await MediaPicker.CapturePhotoAsync();

            if (result != null)
            {
                

                var loadingDialog = await MaterialDialog.Instance.LoadingDialogAsync(message: "Waiting to upload image");
                try
                {
                    var stream = await result.OpenReadAsync();

                    _url = await _firebaseStorage
                       .Child(DateTime.Now.ToString("ddMMyyyy_hhmmss") + ".png")
                       .PutAsync(stream);

                    _responcePin.Photo = Source = _url;
                }
                catch (FirebaseStorageException fs)
                {
                    if (fs.ResponseData == "N/A")
                        await MaterialDialog.Instance.SnackbarAsync(message: "Internet connection error",
                                         msDuration: MaterialSnackbar.DurationLong);
                    else
                        await MaterialDialog.Instance.SnackbarAsync(message: "Fail to upload image",
                                             msDuration: MaterialSnackbar.DurationLong);
                }
                catch (FirebaseException fe)
                {
                    if (fe.ResponseData == "N/A")
                        await MaterialDialog.Instance.SnackbarAsync(message: "Internet connection error",
                                         msDuration: MaterialSnackbar.DurationLong);
                    else if (fe.ResponseData == "")
                        await MaterialDialog.Instance.SnackbarAsync(message: "Timeout responce",
                                         msDuration: MaterialSnackbar.DurationLong);
                }
                finally
                {
                    await loadingDialog.DismissAsync();
                }
            }
        });

        #endregion

        public LandsiteViewModel()
        {
            _responcePin = new ResponcePin();
            Title = "New landslide";
            ResponceTypeMeasure = new ResponceType() { Option = string.Empty, Types = new List<TypeModel>() };
            ResponceTypeSystem = new ResponceType() { Option = string.Empty, Types = new List<TypeModel>() };
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

        async Task<int> ShowDialog(string title, string[] inputs, int selectindex)
        {
            var config = new MaterialConfirmationDialogConfiguration { ButtonAllCaps = true, ControlSelectedColor = Color.FromHex("#d1542f"), TintColor = Color.FromHex("#d1542f"), };

            if(selectindex != -1)
                return await MaterialDialog.Instance.SelectChoiceAsync(title: title, selectedIndex: selectindex,
                                                                         choices: inputs, configuration: config);
            else return await MaterialDialog.Instance.SelectChoiceAsync(title: title,
                                                                         choices: inputs, configuration: config);
        }

        int FindIndex(string[] list, string value)
        {
            for (int i = 0; i < list.Length; i++)
            {
                if (list[i] == value) return i;
            }
            return - 1;
        }
    }
}
