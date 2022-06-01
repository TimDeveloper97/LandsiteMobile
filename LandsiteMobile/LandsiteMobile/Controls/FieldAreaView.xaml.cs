using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LandsiteMobile.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FieldAreaView : ContentView
    {
        public FieldAreaView()
        {
            InitializeComponent();
        }

        public static readonly BindableProperty TypeProperty = BindableProperty.Create(
            "Type",        // the name of the bindable property
            typeof(string),     // the bindable property type
            typeof(FieldAreaView),   // the parent object type
            string.Empty,
            BindingMode.OneWay,
            propertyChanged: HandleTypePropertyChanged);      // the default value for the property

        public static readonly BindableProperty SourceProperty = BindableProperty.Create(
            "Source",        // the name of the bindable property
            typeof(string),     // the bindable property type
            typeof(FieldAreaView),   // the parent object type
            string.Empty,
            BindingMode.OneWay,
            propertyChanged: HandleSourcePropertyChanged);

        public static readonly BindableProperty ValueTypeProperty = BindableProperty.Create(
            "ValueType",        // the name of the bindable property
            typeof(string),     // the bindable property type
            typeof(TitleDesView),   // the parent object type
            string.Empty,
            BindingMode.TwoWay,
            propertyChanged: HandleValueTypePropertyChanged);

        public string Type
        {
            get => (string)GetValue(FieldAreaView.SourceProperty);
            set => SetValue(FieldAreaView.SourceProperty, value);
        }

        public string Source
        {
            get => (string)GetValue(FieldAreaView.TypeProperty);
            set { SetValue(FieldAreaView.TypeProperty, value); }
        }

        public string ValueType
        {
            get => (string)GetValue(FieldAreaView.ValueTypeProperty);
            set { SetValue(FieldAreaView.ValueTypeProperty, value); }
        }

        private static void HandleTypePropertyChanged(
                            BindableObject bindable, object oldValue, object newValue)
        {
            FieldAreaView td;

            td = (FieldAreaView)bindable;
            if (td != null)
                td.type.Text = (string)newValue;
        }

        private static void HandleSourcePropertyChanged(
                            BindableObject bindable, object oldValue, object newValue)
        {
            FieldAreaView td;

            td = (FieldAreaView)bindable;
            if (td != null)
                td.img.Source = (string)newValue;
        }

        private static void HandleValueTypePropertyChanged(
                            BindableObject bindable, object oldValue, object newValue)
        {
            FieldAreaView td = (FieldAreaView)bindable;
            if (td != null)
                td.valuetype.Text = (string)newValue;

        }

        public static BindableProperty OnPressCommandProperty =
            BindableProperty.Create(nameof(OnPressCommand), typeof(ICommand), typeof(FieldAreaView));
        public ICommand OnPressCommand
        {
            get => (ICommand)GetValue(OnPressCommandProperty);
            set => SetValue(OnPressCommandProperty, value);
        }
    }
}