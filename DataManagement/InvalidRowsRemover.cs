using AppliedJobsManager.UI;
using System.Collections.ObjectModel;

namespace AppliedJobsManager.DataManagement
{
    internal class InvalidRowsRemover
    {
        private readonly ObservableCollection<Row> _dataItems;
        public InvalidRowsRemover(ObservableCollection<Row> dataItems)
        {
            _dataItems = dataItems;
        }

        public void RemoveInvalidRows()
        {
            var invalidRows = _dataItems.Where(IsInvalidRow).ToList();

            foreach (var invalidRow in invalidRows)
            {
                _dataItems.Remove(invalidRow);
            }         
        }
              
        private bool IsInvalidRow(Row dataItem)
        {
            if (dataItem.Job is null || dataItem.Link is null)
            {
                return true;
            }

            if (dataItem.Date is null || !DateTime.TryParse(dataItem.Date, out _))
            {
                return true;
            }

            if (dataItem.Pay is null || !int.TryParse(dataItem.Pay, out _))
            {
                return true;
            }

            return false;
        }
    }
}
