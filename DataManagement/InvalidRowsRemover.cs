using AppliedJobsManager.UI;
using System.Collections.ObjectModel;

namespace AppliedJobsManager.DataManagement
{
    internal class InvalidRowsRemover
    {
        private readonly ObservableCollection<DataItem> _dataItems;
        public InvalidRowsRemover(ObservableCollection<DataItem> dataItems)
        {
            _dataItems = dataItems;
        }

        public void RemoveInvalidRows()
        {
            var corruptedDataItems = _dataItems.Where(RowHasUnnacceptableNulls).ToList();

            foreach (var corruptedItem in corruptedDataItems)
            {
                _dataItems.Remove(corruptedItem);
            }

            RemoveInvalidDates();
            RemoveInvalidPays();
        }

        private bool RowHasUnnacceptableNulls(DataItem row) => row.Job is null || row.Link is null || row.Description is null;

        private void RemoveInvalidDates()
        {
            var invalidDates = _dataItems.Where(x => x.Date is null || !DateTime.TryParse(x.Date, out _)).ToList();

            foreach (var invalidDate in invalidDates)
            {
                _dataItems.Remove(invalidDate);
            }
        }

        private void RemoveInvalidPays()
        {
            var invalidPays = _dataItems.Where(x => x.Pay is null || !int.TryParse(x.Pay, out _)).ToList();

            foreach (var invalidPay in invalidPays)
            {
                _dataItems.Remove(invalidPay);
            }
        }
    }
}
