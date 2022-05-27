using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LandsiteMobile.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TitleDesView : ContentView
    {
        public TitleDesView()
        {
            InitializeComponent();
        }

        public static readonly BindableProperty HeadingProperty = BindableProperty.Create(
            "Heading",        // the name of the bindable property
            typeof(string),     // the bindable property type
            typeof(TitleDesView),   // the parent object type
            string.Empty,
            BindingMode.OneWay,
            propertyChanged: HandleHeadingPropertyChanged);      // the default value for the property

        public static readonly BindableProperty DescriptionProperty = BindableProperty.Create(
            "Description",        // the name of the bindable property
            typeof(string),     // the bindable property type
            typeof(TitleDesView),   // the parent object type
            string.Empty,
            BindingMode.OneWay,
            propertyChanged: HandleDescriptionPropertyChanged);

        public string Heading
        {
            get => (string)GetValue(TitleDesView.HeadingProperty);
            set => SetValue(TitleDesView.HeadingProperty, value);
        }

        public string Description
        {
            get => (string)GetValue(TitleDesView.DescriptionProperty);
            set => SetValue(TitleDesView.DescriptionProperty, value);
        }

        private static void HandleHeadingPropertyChanged(
                            BindableObject bindable, object oldValue, object newValue)
        {
            TitleDesView td;

            td = (TitleDesView)bindable;
            if (td != null)
                td.heading.Text = (string)newValue;
        }

        private static void HandleDescriptionPropertyChanged(
                            BindableObject bindable, object oldValue, object newValue)
        {
            TitleDesView td;

            td = (TitleDesView)bindable;
            if (td != null)
                td.description.Text = (string)newValue;
        }
    }
}