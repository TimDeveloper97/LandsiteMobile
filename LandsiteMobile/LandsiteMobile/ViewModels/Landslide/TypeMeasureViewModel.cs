using LandsiteMobile.Domain;
using LandsiteMobile.Models;
using LandsiteMobile.Resources.Languages;
using LandsiteMobile.Views.Landslide;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XF.Material.Forms.UI.Dialogs;
using XF.Material.Forms.UI.Dialogs.Configurations;

namespace LandsiteMobile.ViewModels.Landslide
{
    [QueryProperty(nameof(ParameterResponceType), nameof(ParameterResponceType))]
    class TypeMeasureViewModel : BaseViewModel
    {
        #region Properties
        private ObservableCollection<string> radios;
        private ObservableCollection<TypeModel> types;
        private ResponceType responceType;
        private string selectItemRadio, parameterResponceType;
        private bool hasType;

        public ObservableCollection<string> Radios { get => radios; set => SetProperty(ref radios, value); }
        public ObservableCollection<TypeModel> Types { get => types; set => SetProperty(ref types, value); }
        public string SelectItemRadio
        {
            get => selectItemRadio; set
            {
                SetProperty(ref selectItemRadio, value);
                HasType = selectItemRadio == LanguageResource.radioYes || string.IsNullOrEmpty(selectItemRadio) ? true : false;
                ResponceType.Option = selectItemRadio;
            }
        }
        public bool HasType { get => hasType; set => SetProperty(ref hasType, value); }
        public ResponceType ResponceType
        {
            get => responceType; set
            {
                SetProperty(ref responceType, value);
            }
        }
        public string ParameterResponceType
        {
            get => parameterResponceType; set
            {
                parameterResponceType = Uri.UnescapeDataString(value ?? string.Empty);
                SetProperty(ref parameterResponceType, value);
                if(!string.IsNullOrEmpty(parameterResponceType))
                    ResponceType = JsonConvert.DeserializeObject<ResponceType>(parameterResponceType);
            }
        }

        #endregion

        #region Command 

        public ICommand PageAppearingCommand => new Command(async () =>
        {
            SelectItemRadio = ResponceType.Option;

            if (ResponceType.Types != null)
                foreach (var item in ResponceType.Types)
                    Types.Add(item);
        });

        public ICommand CheckCommand => new Command(async () =>
        {
            ResponceType.Types = new List<TypeModel>();
            foreach (var item in Types)
                ResponceType.Types.Add(item);

            var data = JsonConvert.SerializeObject(ResponceType);
            await Shell.Current.GoToAsync($"..?{nameof(LandsiteViewModel.ParameterResponceTypeMeasure)}={data}");
        });

        public ICommand RemoveCommand => new Command<TypeModel>(async (m) =>
        {
            Types.Remove(m);
        });

        public ICommand AddCommand => new Command(async () =>
        {
            Trace.WriteLine("AddCommand");
            var dg = new DialogMeasureView
            {
                Title = LanguageResource.homeMeasures,
                Title1 = LanguageResource.measureTitle,
                Value1 = LanguageResource.measurePlaceholder,
            };

            dg.ComboBox1Command = new Command(async () =>
            {
                var options = new string[]
                    {
                        LanguageResource.measureOption1,
                        LanguageResource.measureOption2,
                        LanguageResource.measureOption3,
                        LanguageResource.measureOption4,
                        LanguageResource.measureOption5,
                        LanguageResource.measureOption6,
                        LanguageResource.measureOption7,
                        LanguageResource.measureOption8,
                    };
                var result = await ShowDialog(LanguageResource.measureTitle, options);
                if (result < 0) return;

                dg.Value1 = options[result];
            });

            dg.OkCommand = new Command(() =>
            {
                Types.Add(new TypeModel { Type = dg.Value1, Status = dg.Value1, HasOneItem = true });
            });

            await PopupNavigation.Instance.PushAsync(dg);
        });



        #endregion

        public TypeMeasureViewModel()
        {
            Radios = new ObservableCollection<string> { LanguageResource.radioYes, LanguageResource.radioNo };
            Types = new ObservableCollection<TypeModel>();
            ResponceType = new ResponceType() { Option = string.Empty, Types = new List<TypeModel>() };
        }

        async Task<int> ShowDialog(string title, string[] inputs)
        {
            var config = new MaterialConfirmationDialogConfiguration { ButtonAllCaps = true, ControlSelectedColor = Color.FromHex("#d1542f"), TintColor = Color.FromHex("#d1542f"), };

            return await MaterialDialog.Instance.SelectChoiceAsync(title: title,
                                                                         choices: inputs, configuration: config);
        }
    }
}
