using LandsiteMobile.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace LandsiteMobile.Models
{
    class TypeLandslideModel : BaseBinding
    {
        public string Image { get; set; }
        public string Type { get; set; }
        private bool isCheck = false;
        private string colorBorder;
        public bool IsCheck { get => isCheck; set { isCheck = value; ColorBorder = isCheck ? "#d1542f" : GetBorderColor(); } }

        public string ColorBorder { get => colorBorder; set => SetProperty(ref colorBorder, value); }

        string GetBorderColor()
        {
            OSAppTheme currentTheme = Application.Current.RequestedTheme;
            return currentTheme == OSAppTheme.Dark ? "#212121" : "#f2f2f2";
        }
    }


}
