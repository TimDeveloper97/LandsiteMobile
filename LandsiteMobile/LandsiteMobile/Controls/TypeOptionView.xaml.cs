using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LandsiteMobile.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TypeOptionView : ContentView
    {
        public TypeOptionView()
        {
            InitializeComponent();
        }

        public static readonly BindableProperty LabelRadiosProperty = BindableProperty.Create(
            "LabelRadios",        
            typeof(string),    
            typeof(TypeOptionView),   
            string.Empty,
            BindingMode.OneWay,
            propertyChanged: HandleLabelRadiosPropertyChanged);

        public static readonly BindableProperty LabelListViewProperty = BindableProperty.Create(
           "LabelListView",
           typeof(string),
           typeof(TypeOptionView),
           string.Empty,
           BindingMode.OneWay,
           propertyChanged: HandleLabelListViewRadiosPropertyChanged);

        public static readonly BindableProperty LabelAddItemProperty = BindableProperty.Create(
           "LabelAddItem",
           typeof(string),
           typeof(TypeOptionView),
           string.Empty,
           BindingMode.OneWay,
           propertyChanged: HandleLabelAddItemRadiosPropertyChanged);

        public static readonly BindableProperty ItemSourceRadiosProperty = BindableProperty.Create(
           "ItemSourceRadios",
           typeof(IList),
           typeof(TypeOptionView),
           null,
           BindingMode.TwoWay,
           propertyChanged: (_b, _old, _new) => ((TypeOptionView)_b).radios.ItemsSource = (IEnumerable<string>)(IList)_new);

        public static readonly BindableProperty ItemSourceListViewProperty = BindableProperty.Create(
           "ItemSourceListView",
           typeof(IList),
           typeof(TypeOptionView),
           null,
           BindingMode.TwoWay,
           propertyChanged: (_b, _old, _new) => ((TypeOptionView)_b).lvItem.ItemsSource = (IList)_new);

        public static readonly BindableProperty SelectItemRadioProperty = BindableProperty.Create(
           "SelectItemRadio",
           typeof(string),
           typeof(TypeOptionView),
           string.Empty,
           BindingMode.TwoWay);

        public static readonly BindableProperty HasTypesProperty = BindableProperty.Create(
           "HasTypes",
           typeof(bool),
           typeof(TypeOptionView),
           true,
           BindingMode.TwoWay,
           propertyChanged: (_b, _old, _new) => ((TypeOptionView)_b).stList.IsVisible = (bool)_new);

        public bool HasTypes
        {
            get => (bool)GetValue(TypeOptionView.HasTypesProperty);
            set => SetValue(TypeOptionView.HasTypesProperty, value);
        }

        public string SelectItemRadio
        {
            get => (string)GetValue(TypeOptionView.SelectItemRadioProperty);
            set => SetValue(TypeOptionView.SelectItemRadioProperty, value);
        }

        public string LabelRadios
        {
            get => (string)GetValue(TypeOptionView.LabelRadiosProperty);
            set => SetValue(TypeOptionView.LabelRadiosProperty, value);
        }

        public string LabelListView
        {
            get => (string)GetValue(TypeOptionView.LabelListViewProperty);
            set => SetValue(TypeOptionView.LabelListViewProperty, value);
        }

        public string LabelAddItem
        {
            get => (string)GetValue(TypeOptionView.LabelAddItemProperty);
            set => SetValue(TypeOptionView.LabelAddItemProperty, value);
        }

        public IList ItemSourceRadios
        {
            get => (IList)GetValue(TypeOptionView.ItemSourceRadiosProperty);
            set => SetValue(TypeOptionView.ItemSourceRadiosProperty, value);
        }

        public IList ItemSourceListView
        {
            get => (IList)GetValue(TypeOptionView.ItemSourceListViewProperty);
            set => SetValue(TypeOptionView.ItemSourceListViewProperty, value);
        }

        private static void HandleLabelRadiosPropertyChanged(
                            BindableObject bindable, object oldValue, object newValue)
        {
            TypeOptionView obj = (TypeOptionView)bindable;
            if (obj != null)
                obj.lOption.Text = (string)newValue;
        }

        private static void HandleLabelListViewRadiosPropertyChanged(
                            BindableObject bindable, object oldValue, object newValue)
        {
            TypeOptionView obj = (TypeOptionView)bindable;
            if (obj != null)
                obj.llistview.Text = (string)newValue;
        }

        private static void HandleLabelAddItemRadiosPropertyChanged(
                            BindableObject bindable, object oldValue, object newValue)
        {
            TypeOptionView obj = (TypeOptionView)bindable;
            if (obj != null)
                obj.lRadio.Text = (string)newValue;
        }
        

        public static BindableProperty OnPressAddTypeCommandProperty =
            BindableProperty.Create(nameof(OnPressAddTypeCommand), typeof(ICommand), typeof(TypeOptionView));
        public ICommand OnPressAddTypeCommand
        {
            get => (ICommand)GetValue(OnPressAddTypeCommandProperty);
            set => SetValue(OnPressAddTypeCommandProperty, value);
        }

        public static BindableProperty RemoveCommandProperty =
            BindableProperty.Create(nameof(RemoveCommand), typeof(ICommand), typeof(TypeOptionView));
        public ICommand RemoveCommand
        {
            get => (ICommand)GetValue(RemoveCommandProperty);
            set => SetValue(RemoveCommandProperty, value);
        }
    }
}