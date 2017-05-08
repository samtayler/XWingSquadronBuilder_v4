﻿using Windows.Foundation;


namespace Windows.UI.Xaml.Controls
{
    public static class ScrollViewerExtension
    {
        public static void ScrollToElement(this ScrollViewer scrollViewer, UIElement element,
            bool isVerticalScrolling = true, bool smoothScrolling = true, float? zoomFactor = null)
        {
            var transform = element.TransformToVisual((UIElement)scrollViewer.Content);
            var position = transform.TransformPoint(new Point(0, 0));

            if (isVerticalScrolling)
            {
                scrollViewer.ChangeView(null, position.Y + element.DesiredSize.Height, zoomFactor, !smoothScrolling);
            }
            else
            {
                scrollViewer.ChangeView(position.X + element.DesiredSize.Width, null, zoomFactor, !smoothScrolling);
            }
        }
    }
}
