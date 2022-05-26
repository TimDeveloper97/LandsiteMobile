using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace LandsiteMobile.Controls
{
    public class BindableToolbarItem : ToolbarItem
    {
        public static readonly BindableProperty IsVisibleProperty =
            BindableProperty.Create("IsVisible", typeof(bool), typeof(BindableToolbarItem),
                true, BindingMode.OneWay, propertyChanged: OnIsVisibleChanged);
        public bool IsVisible
        {
            get { return (bool)GetValue(IsVisibleProperty); }
            set { SetValue(IsVisibleProperty, value); }
        }

        private static void OnIsVisibleChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var item = bindable as BindableToolbarItem;
            Device.BeginInvokeOnMainThread(() => { item.SetVisibility(oldvalue, newvalue); });
        }

        public BindableToolbarItem()
        { }

        protected override void OnParentSet()
        {
            base.OnParentSet();
            SetVisibility(false, IsVisible);
        }

        void SetVisibility(object oldvalue, object newvalue)
        {
            if (Parent != null)
            {
                var items = ((ContentPage)Parent).ToolbarItems;

                if ((bool)newvalue && !items.Contains(this))
                {
                    // Find the insertion point (based on Priority of other toolbar items)
                    var nextItem = items.FirstOrDefault(i => i.Priority > this.Priority);
                    var idx = (nextItem != null) ? items.IndexOf(nextItem) : items.Count;
                    // Insert this toolbar item
                    items.Insert(idx, this);
                }
                else if (!(bool)newvalue && items.Contains(this))
                {
                    items.Remove(this);
                }
            }
        }
    }
}
