using LandsiteMobile.Domain;
using LandsiteMobile.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace LandsiteMobile.ViewModels.Landslide
{
    [QueryProperty(nameof(ParameterPinTag), nameof(ParameterPinTag))]
    class InfoLandslideViewModel : BaseViewModel
    {
        #region Properties 
        private string parameterPinTag;
        private string position, measureTypes, systemTypes;
        private ResponcePin pin;


        public string ParameterPinTag
        {
            get => parameterPinTag; set
            {
                parameterPinTag = Uri.UnescapeDataString(value ?? string.Empty);
                SetProperty(ref parameterPinTag, value);
            }
        }

        public ResponcePin Pin { get => pin; set => SetProperty(ref pin, value); }

        public string Position { get => position; set => SetProperty(ref position, value); }
        public string MeasureTypes { get => measureTypes; set => SetProperty(ref measureTypes, value); }
        public string SystemTypes { get => systemTypes; set => SetProperty(ref systemTypes, value); }
        #endregion


        #region Command 
        public ICommand PageAppearingCommand => new Command(async () =>
        {
            if(!string.IsNullOrEmpty(ParameterPinTag))
            {
                var pin = (await _firebaseDatabase.Child("Pins").OnceAsync<ResponcePin>()).FirstOrDefault(x => x.Object.Tag == ParameterPinTag);

                Pin = pin.Object;

                Position = "Landslide: " + Pin.Landslide + " - Longitude: " + Pin.Longitude;

                if (Pin.Measure == null || Pin.Measure.Option == "No")
                    MeasureTypes = "No measure types";
                else
                {
                    foreach (var item in Pin.Measure.Types)
                    {
                        MeasureTypes += "Type: " + item.Type + " - Status: " + item.Status + "\n";
                    }
                }

                if (Pin.System == null || Pin.System.Option == "No")
                    SystemTypes = "No measure types";
                else
                {
                    foreach (var item in Pin.System.Types)
                    {
                        SystemTypes += "Type: " + item.Type + " - Status: " + item.Status + "\n";
                    }
                }
            }
        });

        #endregion
    }
}
