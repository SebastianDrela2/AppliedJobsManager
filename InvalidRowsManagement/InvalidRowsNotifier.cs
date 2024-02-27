using AppliedJobsManager.UI;
using System.Text;
using System.Windows;

namespace AppliedJobsManager.DataManagement
{
    internal class InvalidRowsNotifier
    {
        public void Notify(List<Row> rows, List<object> collection)
        {          
            var stringBuilder = new StringBuilder();

            if (rows.Count == 0)
            {
                return;
            }

            foreach(var row in rows)
            {
                var (column, reason) = GetNotifyReason(row);
                var index = collection.IndexOf(row);
                stringBuilder.Append($"Removed Row {index + 1} because of {reason} {column} \n");
            }
            
            System.Windows.MessageBox.Show(stringBuilder.ToString());
        }

        private (string, NotifyReason) GetNotifyReason(Row row)
        {
            if (row.Job is null)
            {
                return ("Job", NotifyReason.NullValue);
            }

            if (row.Link is null)
            {
                return ("Link", NotifyReason.NullValue);
            }

            if (row.Date is null)
            {
                return ("Date", NotifyReason.NullValue);
            }

            if (!DateTime.TryParse(row.Date, out _))
            {
                return ("Date", NotifyReason.InvalidDate);
            }

            if (row.Pay is null)
            {
                return ("Pay", NotifyReason.NullValue);
            }

            if (!int.TryParse(row.Pay, out _))
            {
                return ("Pay", NotifyReason.InvalidPay);
            }

            // should not be possible
            return (null, NotifyReason.NullValue);
        }
    }
}
