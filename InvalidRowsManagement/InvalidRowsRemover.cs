using AppliedJobsManager.Models;
using AppliedJobsManager.ViewModels;

namespace AppliedJobsManager.DataManagement
{
    public class InvalidRowsRemover
    {
        private readonly AppliedJobsViewModel _appliedJobsViewModel;

        public InvalidRowsRemover(AppliedJobsViewModel appliedJobsViewModel)
        {
            _appliedJobsViewModel = appliedJobsViewModel;
        }

        public List<Row> ManageInvalidRows()
        {
            var invalidRows = _appliedJobsViewModel.Rows.Where(IsInvalidRow).ToList();

            foreach (var invalidRow in invalidRows)
            {
                _appliedJobsViewModel.Rows.Remove(invalidRow);
            }

            if (invalidRows.Count is not 0)
            {
                _appliedJobsViewModel.RowsAreOutdated = true;
            }

            return invalidRows;
        }
              
        private bool IsInvalidRow(Row dataItem)
        {
            if (dataItem.Job is null || dataItem.Link is null)
            {
                return true;
            }

            if (dataItem.Date is null || !DateTime.TryParse(dataItem.Date, out var date) || date < DateTime.Now)
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
