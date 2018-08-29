using System.Windows;
using System.Windows.Controls;

namespace WPFControls.ColumnsListBox.Helpers
{
    public partial class CustomListBox : ListBox
    {
        public CustomListBox()
        {
            InitializeComponent();
            Loaded += (s, e) => CustomBoxesInfo.Instance.CustomListBoxes.Add(this);
        }

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
    }
}
