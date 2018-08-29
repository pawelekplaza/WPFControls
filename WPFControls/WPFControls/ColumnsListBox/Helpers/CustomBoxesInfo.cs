using System;
using System.Collections.ObjectModel;

namespace WPFControls.ColumnsListBox.Helpers
{
    public class CustomBoxesInfo
    {
        #region Singleton
        private static Lazy<CustomBoxesInfo> _instance = new Lazy<CustomBoxesInfo>(() => new CustomBoxesInfo());
        public static CustomBoxesInfo Instance => _instance.Value;
        #endregion

        public event Action NumberOfBoxesChanged;
        public ObservableCollection<CustomListBox> CustomListBoxes { get; set; } = new ObservableCollection<CustomListBox>();

        protected CustomBoxesInfo()
        {
            CustomListBoxes.CollectionChanged += (s, e) => NumberOfBoxesChanged?.Invoke();
        }
    }
}
