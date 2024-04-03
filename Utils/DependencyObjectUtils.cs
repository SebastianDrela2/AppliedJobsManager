using System.Windows.Media;
using System.Windows;

namespace AppliedJobsManager.Utils
{
    public static class DependencyObjectUtils
    {
        public static T FindParent<T>(DependencyObject child) where T : DependencyObject
        {
            var parentObject = VisualTreeHelper.GetParent(child);

            if (parentObject == null)
            {
                return null;
            }

            if (parentObject is T parent)
            {
                return parent;
            }

            return FindParent<T>(parentObject);
        }
    }
}
