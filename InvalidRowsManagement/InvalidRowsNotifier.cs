using AppliedJobsManager.Models;
using System.Text;
using MessageBox = System.Windows.Forms.MessageBox;

namespace AppliedJobsManager.DataManagement
{
    public class InvalidRowsNotifier
    {
        public void Notify(List<Row> rows, List<Row> collection)
        {          
            var stringBuilder = new StringBuilder();

            if (rows.Count == 0)
            {
                return;
            }

            foreach(var row in rows)
            {
                var (column, reason) = GetNotifyComponents(row);
                var index = collection.IndexOf(row);
                stringBuilder.Append($"Removed Row {index + 1} because of {reason} {column} \n");
            }
            
            MessageBox.Show(stringBuilder.ToString());
        }

        public bool TryNotifyRow(Row row)
        {
            var (column, reason) = GetNotifyComponents(row);

            if (column is null && reason is NotifyReason.NullValue)
            {
                return false;
            }

            MessageBox.Show($"Invalid data: {column} because of {reason}", 
                "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Error);

            return true;
        }

        private (string?, NotifyReason) GetNotifyComponents(Row row)
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

            if (!DateTime.TryParse(row.Date, out var date))
            {
                return ("Date", NotifyReason.InvalidDate);
            }

            if (date < DateTime.Now)
            {
                return ("Date", NotifyReason.Outdated);
            }

            if (row.Pay is null)
            {
                return ("Pay", NotifyReason.NullValue);
            }

            if (!int.TryParse(row.Pay, out _))
            {
                return ("Pay", NotifyReason.InvalidPay);
            }
           
            return (null, NotifyReason.NullValue);
        }
    }
}
