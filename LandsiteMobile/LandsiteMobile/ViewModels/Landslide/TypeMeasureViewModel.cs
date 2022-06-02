using LandsiteMobile.Domain;
using LandsiteMobile.Models;
using LandsiteMobile.Resources.Languages;
using LandsiteMobile.Views.Landslide;
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
    class TypeMeasureViewModel : BaseViewModel
    {
        #region Properties
        private ObservableCollection<string> radios;
        private ObservableCollection<TypeModel> types;

        public ObservableCollection<string> Radios { get => radios; set => SetProperty(ref radios, value); }
        public ObservableCollection<TypeModel> Types { get => types; set => SetProperty(ref types, value); }
        #endregion

        #region Command 

        public ICommand PageAppearingCommand => new Command(async () =>
        {
        });

        public ICommand CheckCommand => new Command(async () =>
        {
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
                Title1 = "Type of the mitigation work",
                Value1 = "Select an option",
            };

            dg.ComboBox1Command = new Command(async () =>
            {
                var options = new string[]
                    {
                            "Retaining wall", "Gabions", "Anchorage", "Rockfall barriers", "Spritz betoz", "Friction nets", "Drains",
                            "Geogrids"
                    };
                var result = await ShowDialog("Type of the mitigation work", options);
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
            Radios = new ObservableCollection<string>();
            Radios.Add("Yes");
            Radios.Add("No");
            Types = new ObservableCollection<TypeModel>();
        }

        async Task<int> ShowDialog(string title, string[] inputs)
        {
            var config = new MaterialConfirmationDialogConfiguration { ButtonAllCaps = true, ControlSelectedColor = Color.FromHex("#d1542f"), TintColor = Color.FromHex("#d1542f"), };

            return await MaterialDialog.Instance.SelectChoiceAsync(title: title,
                                                                         choices: inputs, configuration: config);
        }
    }
}
