using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace WPFControls.ColumnsListBox.Helpers
{
    public partial class InternalListBox : ItemsControl
    {
        private List<ScrollViewer> _scrollViewers = new List<ScrollViewer>();

        public int Columns
        {
            get { return (int)GetValue(ColumnsProperty); }
            set { SetValue(ColumnsProperty, value); }
        }
        public static readonly DependencyProperty ColumnsProperty =
            DependencyProperty.Register("Columns", typeof(int), typeof(InternalListBox), new PropertyMetadata(1));

        public Style ContainerStyle
        {
            get { return (Style)GetValue(ContainerStyleProperty); }
            set { SetValue(ContainerStyleProperty, value); }
        }
        public static readonly DependencyProperty ContainerStyleProperty =
            DependencyProperty.Register("ContainerStyle", typeof(Style), typeof(InternalListBox), new PropertyMetadata(default(Style)));

        public InternalListBox()
        {
            InitializeComponent();
            Loaded += OnInternalListBoxLoaded;
        }

        public void PageDown() =>
            _scrollViewers.FirstOrDefault()?.PageDown();

        public void PageUp() =>
            _scrollViewers.FirstOrDefault()?.PageUp();

        public void ScrollUp() =>
            _scrollViewers.FirstOrDefault()?.LineUp();

        public void ScrollDown() =>
            _scrollViewers.FirstOrDefault()?.LineDown();

        public void ScrollHome() =>
            _scrollViewers.FirstOrDefault()?.ScrollToHome();

        public void ScrollEnd() =>
            _scrollViewers.FirstOrDefault()?.ScrollToEnd();

        private void OnInternalListBoxLoaded(object sender, RoutedEventArgs e)
        {
            CustomBoxesInfo.Instance.NumberOfBoxesChanged += SynchronizeScrollViewers;
            InternalBoxesInfo.Instance.InternalListBoxes.Add(this);
        }

        private void SynchronizeScrollViewers()
        {
            GetScrollViewers();
            foreach (var viewer in _scrollViewers)
            {
                viewer.ScrollChanged -= OnScrollViewerScrollChanged;
                viewer.ScrollChanged += OnScrollViewerScrollChanged;
            }
        }

        private void OnScrollViewerScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            foreach (var viewer in _scrollViewers)
                viewer.ScrollToVerticalOffset(e.VerticalOffset);
        }

        private void GetScrollViewers()
        {
            _scrollViewers.Clear();
            foreach (var box in CustomBoxesInfo.Instance.CustomListBoxes)
                foreach (var viewer in VisualChildHelper.FindVisualChildren<ScrollViewer>(box))
                    _scrollViewers.Add(viewer);
        }
    }
}
