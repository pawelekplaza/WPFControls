using System;
using WPFControls.ColumnsListBox.Helpers.Interfaces;

namespace WPFControls.ColumnsListBox.Helpers
{
    public class SelectionNotifier
    {
        #region Singletone
        private static Lazy<SelectionNotifier> _instance = new Lazy<SelectionNotifier>(() => new SelectionNotifier());
        public static SelectionNotifier Instance => _instance.Value;
        protected SelectionNotifier() { }
        #endregion

        public ISelectable CurrentSelection { get; private set; }

        public void Select(ISelectable element)
        {
            if (CurrentSelection == element)
                return;

            if (CurrentSelection != null)
                CurrentSelection.IsSelected = false;

            if (element != null)
            {
                CurrentSelection = element;
                element.IsSelected = true;
            }            
        }
    }
}
