namespace MSBES.Reports.Excel;

/// <summary>
/// Builds an Excel (.xlsx) table and returns its raw bytes. Report-specific columns and data
/// stay in the host app; this package only owns workbook creation and shared layout defaults.
/// </summary>
public interface IExcelReportGenerator
{
    /// <summary>
    /// Builds a single worksheet from <paramref name="rows"/> using <paramref name="columns"/>.
    /// </summary>
    byte[] GenerateTable<T>(
        string sheetName,
        IEnumerable<T> rows,
        IReadOnlyList<ExcelColumn<T>> columns);
}
