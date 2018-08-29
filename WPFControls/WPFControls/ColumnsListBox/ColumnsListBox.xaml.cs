using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WPFControls.ColumnsListBox.Helpers;

namespace WPFControls.ColumnsListBox
{
    public partial class ColumnsListBox : ItemsControl
    {
        private InternalListBox _internalListBox;

        public ColumnsListBox()
        {
            InitializeComponent();
            Loaded += OnColumnsListBoxLoaded;
        }

        protected override void OnItemsSourceChanged(IEnumerable oldValue, IEnumerable newValue)
        {
            base.OnItemsSourceChanged(oldValue, newValue);
            PrepareColumnsSource();
        }

        public int Columns
        {
            get { return (int)GetValue(ColumnsProperty); }
            set { SetValue(ColumnsProperty, value); }
        }
        public static readonly DependencyProperty ColumnsProperty =
            DependencyProperty.Register("Columns", typeof(int), typeof(ColumnsListBox), new PropertyMetadata(1));

        public ObservableCollection<IEnumerable> ColumnsSource
        {
            get { return (ObservableCollection<IEnumerable>)GetValue(ColumnsSourceProperty); }
            protected set { SetValue(ColumnsSourceProperty, value); }
        }
        public static readonly DependencyProperty ColumnsSourceProperty =
            DependencyProperty.Register("ColumnsSource", typeof(ObservableCollection<IEnumerable>), typeof(ColumnsListBox), new PropertyMetadata(new ObservableCollection<IEnumerable>()));

        public void PageDown() =>
            _internalListBox.PageDown();

        public void PageUp() =>
            _internalListBox.PageUp();

        public void ScrollUp() =>
            _internalListBox.ScrollUp();

        public void ScrollDown() =>
            _internalListBox.ScrollDown();

        public void ScrollHome() =>
            _internalListBox.ScrollHome();

        public void ScrollEnd() =>
            _internalListBox.ScrollEnd();

        private void OnColumnsListBoxLoaded(object sender, RoutedEventArgs e)
        {
            InternalBoxesInfo.Instance.NumberOfBoxesChanged += () => _internalListBox = InternalBoxesInfo.Instance.InternalListBoxes.First();
        }

        private async void PrepareColumnsSource()
        {
            ColumnsSource.Clear();
            var itemsPerColumn = (int)Math.Ceiling((double)Items.Count / Columns);
            var itemsAdded = 0;

            for (int i = 0; i < Columns; i++)
            {
                var tempList = new List<object>();
                await Task.Factory.StartNew(() =>
                {
                    foreach (var item in ItemsSource)
                    {
                        itemsAdded++;
                        if (itemsAdded <= i * itemsPerColumn)
                            continue;

                        tempList.Add(item);
                        if (itemsAdded >= (i + 1) * itemsPerColumn)
                        {
                            itemsAdded = 0;
                            break;
                        }
                    }
                });                
                ColumnsSource.Add(tempList);
            }
        }
    }
}
