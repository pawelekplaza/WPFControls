using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace WPFControls.ColumnsListBox.Helpers
{
    public partial class CustomListBox : ListBox
    {
        private List<FrameworkElement> _itemsTemplates;

        public double WholeWidth
        {
            get { return (double)GetValue(WholeWidthProperty); }
            set { SetValue(WholeWidthProperty, value); }
        }
        public static readonly DependencyProperty WholeWidthProperty =
            DependencyProperty.Register("WholeWidth", typeof(double), typeof(CustomListBox), new PropertyMetadata(default(double)));

        public int Columns
        {
            get { return (int)GetValue(ColumnsProperty); }
            set { SetValue(ColumnsProperty, value); }
        }
        public static readonly DependencyProperty ColumnsProperty =
            DependencyProperty.Register("Columns", typeof(int), typeof(CustomListBox), new PropertyMetadata(1));

        public CustomListBox()
        {
            InitializeComponent();
            Loaded += (s, e) => CustomBoxesInfo.Instance.CustomListBoxes.Add(this);
            Unloaded += (s, e) => CustomBoxesInfo.Instance.CustomListBoxes.Remove(this);
        }

        protected override void OnSelectionChanged(SelectionChangedEventArgs e)
        {
            base.OnSelectionChanged(e);

            SetItemsTemplates();
            foreach (var item in e.AddedItems)
            {
                var associatedItem = GetAssociatedItem(item);
                if (!associatedItem.IsFocused)
                    associatedItem.Focus();
            }
        }

        private FrameworkElement GetAssociatedItem(object dataContext)
        {
            return _itemsTemplates?.FirstOrDefault(v => v?.DataContext == dataContext);
        }

        private void SetItemsTemplates()
        {
            var panel = VisualChildHelper.FindVisualChildren<VirtualizingStackPanel>(this).FirstOrDefault();
            _itemsTemplates = VisualChildHelper.FindDirectChildren<FrameworkElement>(panel).ToList();
        }
    }
}
