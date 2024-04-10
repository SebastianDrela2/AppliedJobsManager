using AppliedJobsManager.Models;

namespace AppliedJobsManager.DataManagement
{
    public class InvalidRowsRemover
    {
        private readonly IList<Row> _rows;

        public InvalidRowsRemover(IList<Row> rows)
        {
            _rows = rows;
        }

        public List<Row> ManageInvalidRows()
        {
            var invalidRows = _rows.Where(IsInvalidRow).ToList();

            foreach (var invalidRow in invalidRows)
            {
                _rows.Remove(invalidRow);
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
