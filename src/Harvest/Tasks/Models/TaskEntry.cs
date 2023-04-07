namespace Harvest.Tasks.Models;

using Newtonsoft.Json;

/// <summary>
/// Defines the detail for a task.
/// </summary>
public class TaskEntry : TaskSummary
{
    /// <summary>
    /// Gets or sets a value indicating whether default tasks should be marked as billable when creating a new project.
    /// </summary>
    [JsonProperty("billable_by_default")]
    public bool? BillableByDefault { get; set; }

    /// <summary>
    /// Gets or sets the hourly rate to use for this task when it is added to a project.
    /// </summary>
    [JsonProperty("default_hourly_rate")]
    public decimal? DefaultHourlyRate { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the task should be automatically added to future projects.
    /// </summary>
    [JsonProperty("is_default")]
    public bool? IsDefault { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the task is active.
    /// </summary>
    [JsonProperty("is_active")]
    public bool? IsActive { get; set; }
}