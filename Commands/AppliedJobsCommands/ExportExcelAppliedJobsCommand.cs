using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Windows.Input;

namespace AppliedJobsManager.Commands.AppliedJobsCommands
{
    internal class ExportExcelAppliedJobsCommand : ICommand
    {
        private readonly IList<Models.Row> _rows;

        public event EventHandler? CanExecuteChanged;

        public ExportExcelAppliedJobsCommand(IList<Models.Row> rows)
        {
            _rows = rows;
        }

        public bool CanExecute(object? parameter) => true;
        public void Execute(object? parameter)
        {
            var dialog = new SaveFileDialog
            {
                Filter = "Excel Files (*.xlsx)|*.xlsx"
            };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                using var spreadSheet = SpreadsheetDocument.Create(dialog.FileName, SpreadsheetDocumentType.Workbook);
                var workbookPart = spreadSheet.AddWorkbookPart();
                workbookPart.Workbook = new Workbook();

                var sheetData = GetSheetData(spreadSheet, workbookPart);

                foreach (var row in _rows)
                {
                    AddRow(sheetData!, row);
                }

                workbookPart.Workbook.Save();
                AnnounceExcelWasCreated(dialog.FileName);
            }
        }
        
        private SheetData? GetSheetData(SpreadsheetDocument spreadSheet, WorkbookPart workbookPart)
        {
            var worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
            worksheetPart.Worksheet = new Worksheet(new SheetData());

            var sheets = spreadSheet.WorkbookPart!.Workbook.AppendChild(new Sheets());
            var sheet = new Sheet()
            {
                Id = spreadSheet.WorkbookPart.GetIdOfPart(worksheetPart),
                SheetId = 1,
                Name = "Sheet1"
            };

            sheets.Append(sheet);

            return worksheetPart.Worksheet.GetFirstChild<SheetData>();
        }

        private void AddRow(SheetData sheetData, Models.Row row)
        {
            var excelRow = new DocumentFormat.OpenXml.Spreadsheet.Row();

            excelRow.AppendChild(new Cell(new CellValue(row.Link)));
            excelRow.AppendChild(new Cell(new CellValue(row.Job)));
            excelRow.AppendChild(new Cell(new CellValue(row.Description)));
            excelRow.AppendChild(new Cell(new CellValue(row.Pay)));
            excelRow.AppendChild(new Cell(new CellValue(row.Date)));

            sheetData.AppendChild(excelRow);
        }

        private void AnnounceExcelWasCreated(string filename) 
            => MessageBox.Show($"Excel file created successfully.\n Saved file to {filename}");
    }
}
