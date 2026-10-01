# MSBES.Reports.Excel

Builds Excel (`.xlsx`) table reports with [ClosedXML](https://github.com/ClosedXML/ClosedXML).

One job: rows + column definitions → `.xlsx` bytes. Column maps and data stay in the host app;
ClosedXML stays inside this package.

## Installation

```
dotnet add package MSBES.Reports.Excel
```

## Setup

```csharp
builder.Services.AddMsbesExcelReports();
```

## Usage

Define columns next to the report, then call from the endpoint:

```csharp
private static readonly ExcelColumn<LateMaternalDeathsReportRow>[] LateMaternalDeathsColumns =
[
    new() { Header = "Facility", Value = r => r.FacilityName },
    new() { Header = "Deaths", Value = r => r.Count },
    new() { Header = "Date", Value = r => r.OccurredOn },
];

var excelBytes = excelReportGenerator.GenerateTable(
    "Late Maternal Deaths",
    result.Data.Rows,
    LateMaternalDeathsColumns);

return BusinessResult<FileContentResult?>.Success(
    new FileContentResult(
        excelBytes,
        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
    {
        FileDownloadName = ToExcelFileName(SharedResource.Report_LateMaternalDeaths_Title),
    });
```

## Design notes

- Endpoints stay thin — query → generate → return file (same shape as `MSBES.Reports.Pdf`).
- Column maps live with the report — not hardcoded in the API method.
- No HTML → Excel — reuse the same report DTO you already use for PDF.
- No styling in v1 — plain headers and values only.
