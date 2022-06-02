using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LandsiteMobile.Views.Landslide
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DialogMeasureView : PopupPage
    {
        public DialogMeasureView()
        {
            InitializeComponent();
        }

        private async void OnClose(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
        }

        protected override Task OnAppearingAnimationEndAsync()
        {
            return Content.FadeTo(0.5);
        }

        protected override Task OnDisappearingAnimationBeginAsync()
        {
            return Content.FadeTo(1);
        }

        public static readonly BindableProperty Title1Property = BindableProperty.Create(
            "Title1",
            typeof(string),
            typeof(DialogMeasureView),
            string.Empty,
            BindingMode.OneWay,
            propertyChanged: (_b, _old, _new) => ((DialogMeasureView)_b).ltitle1.Text = (string)_new);

        public static readonly BindableProperty Value1Property = BindableProperty.Create(
            "Value1",
            typeof(string),
            typeof(DialogMeasureView),
            string.Empty,
            BindingMode.TwoWay,
            propertyChanged: (_b, _old, _new) => ((DialogMeasureView)_b).lvalue1.Text = (string)_new);

        public static readonly BindableProperty Title2Property = BindableProperty.Create(
            "Title2",
            typeof(string),
            typeof(DialogMeasureView),
            string.Empty,
            BindingMode.OneWay,
            propertyChanged: (_b, _old, _new) => ((DialogMeasureView)_b).ltitle2.Text = (string)_new);

        public static readonly BindableProperty Value2Property = BindableProperty.Create(
            "Value2",
            typeof(string),
            typeof(DialogMeasureView),
            string.Empty,
            BindingMode.TwoWay,
            propertyChanged: (_b, _old, _new) => ((DialogMeasureView)_b).lvalue2.Text = (string)_new);

        public static readonly BindableProperty IsComboBox2Property = BindableProperty.Create(
            "IsComboBox2",
            typeof(bool),
            typeof(DialogMeasureView),
            false,
            BindingMode.TwoWay,
            propertyChanged: (_b, _old, _new) => ((DialogMeasureView)_b).cbStack.IsVisible = (bool)_new);

        public string Title1
        {
            get => (string)GetValue(DialogMeasureView.Title1Property);
            set => SetValue(DialogMeasureView.Title1Property, value);
        }

        public string Value1
        {
            get => (string)GetValue(DialogMeasureView.Value1Property);
            set => SetValue(DialogMeasureView.Value1Property, value);
        }

        public string Title2
        {
            get => (string)GetValue(DialogMeasureView.Title2Property);
            set => SetValue(DialogMeasureView.Title2Property, value);
        }

        public string Value2
        {
            get => (string)GetValue(DialogMeasureView.Value2Property);
            set => SetValue(DialogMeasureView.Value2Property, value);
        }

        public bool IsComboBox2
        {
            get => (bool)GetValue(DialogMeasureView.IsComboBox2Property);
            set => SetValue(DialogMeasureView.IsComboBox2Property, value);
        }

        public static BindableProperty ComboBox1CommandProperty =
                    BindableProperty.Create(nameof(ComboBox1Command), typeof(ICommand), typeof(DialogMeasureView));
        public ICommand ComboBox1Command
        {
            get => (ICommand)GetValue(ComboBox1CommandProperty);
            set => SetValue(ComboBox1CommandProperty, value);
        }


        public static BindableProperty ComboBox2CommandProperty =
            BindableProperty.Create(nameof(ComboBox2Command), typeof(ICommand), typeof(DialogMeasureView));
        public ICommand ComboBox2Command
        {
            get => (ICommand)GetValue(ComboBox2CommandProperty);
            set => SetValue(ComboBox2CommandProperty, value);
        }

        public static BindableProperty OkCommandProperty =
            BindableProperty.Create(nameof(OkCommand), typeof(ICommand), typeof(DialogMeasureView));
        public ICommand OkCommand
        {
            get => (ICommand)GetValue(OkCommandProperty);
            set => SetValue(OkCommandProperty, value);
        }

        public static BindableProperty CancelCommandProperty =
            BindableProperty.Create(nameof(CancelCommand), typeof(ICommand), typeof(DialogMeasureView));
        public ICommand CancelCommand
        {
            get => (ICommand)GetValue(CancelCommandProperty);
            set => SetValue(CancelCommandProperty, value);
        }
    }
}