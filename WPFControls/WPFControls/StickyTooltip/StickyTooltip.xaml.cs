using System.Windows;
using System.Windows.Controls;

namespace WPFControls.StickyTooltip
{
    public partial class StickyTooltip : ToolTip
    {
        private Point _toolTipOpenPosition;

        public StickyTooltip()
        {
            InitializeComponent();
            Loaded += OnStickyTooltipLoaded;
            Opened += OnStickyTooltipOpened;
            Closed += OnStickyTooltipClosed;
        }

        private void OnStickyTooltipClosed(object sender, RoutedEventArgs e)
        {
            ResetOffsets();
            _toolTipOpenPosition = default(Point);
        }

        private void OnStickyTooltipOpened(object sender, RoutedEventArgs e)
        {
            ResetOffsets();
            _toolTipOpenPosition = TranslatePoint(new Point(0, -20), PlacementTarget);
        }

        private void OnStickyTooltipLoaded(object sender, RoutedEventArgs e)
        {
            ToolTipService.SetShowDuration(PlacementTarget, int.MaxValue);
            PlacementTarget.MouseMove += OnPlacementTargetMouseMove;
        }

        private void OnPlacementTargetMouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            var point = e.GetPosition(PlacementTarget);
            HorizontalOffset = point.X - _toolTipOpenPosition.X;
            VerticalOffset = point.Y - _toolTipOpenPosition.Y;
        }

        private void ResetOffsets()
        {
            HorizontalOffset = 0;
            VerticalOffset = 0;
        }
    }
}
