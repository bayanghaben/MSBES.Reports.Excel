using ClosedXML.Excel;

namespace MSBES.Reports.Excel;

internal sealed class ExcelReportGenerator : IExcelReportGenerator
{
    public byte[] GenerateTable<T>(
        string sheetName,
        IEnumerable<T> rows,
        IReadOnlyList<ExcelColumn<T>> columns)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(sheetName);
        ArgumentNullException.ThrowIfNull(rows);
        ArgumentNullException.ThrowIfNull(columns);

        if (columns.Count == 0)
        {
            throw new ArgumentException(
                "At least one column is required.",
                nameof(columns));
        }

        using var workbook = new XLWorkbook();
        var worksheet = workbook.Worksheets.Add(sheetName);

        // Write headers
        for (var columnIndex = 0; columnIndex < columns.Count; columnIndex++)
        {
            worksheet.Cell(1, columnIndex + 1).Value = columns[columnIndex].Header;
        }

        // Write data
        var rowIndex = 2;

        foreach (var row in rows)
        {
            for (var columnIndex = 0; columnIndex < columns.Count; columnIndex++)
            {
                var value = columns[columnIndex].Value(row);

                // Excel does not have a time-zone-aware date type.
                if (value is DateTimeOffset dateTimeOffset)
                {
                    value = dateTimeOffset.UtcDateTime;
                }
                // Excel does not have a DateOnly type.
                else if (value is DateOnly dateOnly)
                {
                    value = dateOnly.ToDateTime(TimeOnly.MinValue);
                }

                worksheet.Cell(rowIndex, columnIndex + 1).Value =
                    XLCellValue.FromObject(value);
            }

            rowIndex++;
        }

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);

        return stream.ToArray();
    }
}
