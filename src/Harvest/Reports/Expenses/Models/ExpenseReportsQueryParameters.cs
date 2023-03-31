namespace Harvest.Reports.Expenses.Models;

using System;
using Common.Requests;

/// <summary>
/// Defines the parameters for the expense reports report.
/// </summary>
public class ExpenseReportsQueryParameters : PaginatedQueryParameters
{
    /// <summary>
    /// Gets or sets the date range from which to retrieve expense reports. Defaults to the current date.
    /// </summary>
    [QueryParameter("from")]
    public DateTime From { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the date range to which to retrieve expense reports. Defaults to the current date plus 1 month.
    /// </summary>
    [QueryParameter("to")]
    public DateTime To { get; set; } = DateTime.UtcNow.AddMonths(1);
}