using System.Windows;

namespace AppliedJobsManager.Utils
{
    internal class RectangleUtils
    {
        public static Rectangle GetRectangleFromWindow(Window window)
        {
            return new Rectangle((int)window.Left, (int)window.Top, (int)window.Width, (int)window.Height);
        }
    }
}
