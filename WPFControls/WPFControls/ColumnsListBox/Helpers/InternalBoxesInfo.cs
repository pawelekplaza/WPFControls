using System;
using System.Collections.ObjectModel;

namespace WPFControls.ColumnsListBox.Helpers
{
    public class InternalBoxesInfo
    {
        #region Singleton
        private static Lazy<InternalBoxesInfo> _instance = new Lazy<InternalBoxesInfo>(() => new InternalBoxesInfo());
        public static InternalBoxesInfo Instance => _instance.Value;
        #endregion

        public event Action NumberOfBoxesChanged;
        public ObservableCollection<InternalListBox> InternalListBoxes { get; set; } = new ObservableCollection<InternalListBox>();

        protected InternalBoxesInfo()
        {
            InternalListBoxes.CollectionChanged += (s, e) => NumberOfBoxesChanged?.Invoke();
        }
    }
}
