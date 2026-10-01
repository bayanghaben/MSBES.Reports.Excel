using Microsoft.Extensions.DependencyInjection;

namespace MSBES.Reports.Excel;

/// <summary>Dependency-injection registration for MSBES.Reports.Excel.</summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Registers <see cref="IExcelReportGenerator"/>. Call once from the app's composition root.
    /// </summary>
    public static IServiceCollection AddMsbesExcelReports(this IServiceCollection services)
    {
        services.AddScoped<IExcelReportGenerator, ExcelReportGenerator>();
        return services;
    }
}
