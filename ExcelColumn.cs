namespace MSBES.Reports.Excel;

/// <summary>
/// One column in a table-style Excel report. Define next to each report (not in the endpoint).
/// </summary>
public sealed class ExcelColumn<T>
{
    /// <summary>Header text written in row 1.</summary>
    public required string Header { get; init; }

    /// <summary>Extracts the cell value from a row item.</summary>
    public required Func<T, object?> Value { get; init; }
}
