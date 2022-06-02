using LandsiteMobile.Domain;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace LandsiteMobile.ViewModels.Landslide
{
    class TypeMeasureViewModel : BaseViewModel
    {
        #region Properties
        private ObservableCollection<string> radios, types;

        public ObservableCollection<string> Radios { get => radios; set => SetProperty(ref radios, value); }
        public ObservableCollection<string> Types { get => types; set => SetProperty(ref types, value); }
        #endregion

        #region Command 

        public ICommand PageAppearingCommand => new Command(async () =>
        {
        });

        public ICommand CheckCommand => new Command(async () =>
        {
        });

        public ICommand AddCommand => new Command(async () =>
        {
            
        });

        #endregion

        public TypeMeasureViewModel()
        {
            Radios = new ObservableCollection<string>();
            Radios.Add("Yes");
            Radios.Add("No");
            Types = new ObservableCollection<string>();
        }
    }
}
