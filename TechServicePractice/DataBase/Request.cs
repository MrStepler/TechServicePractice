using System;
using System.Collections.Generic;

namespace TechServicePractice;

public partial class Request
{
    public long Id { get; set; }

    public string? ProblemType { get; set; }

    public long? SerialNumber { get; set; }

    public int RequestPriority { get; set; }

    public long AppealId { get; set; }

    public long? ExecutorId { get; set; }

    public DateTime? CompleatingDate { get; set; }

    public string? Commentary { get; set; }

    public virtual Appeal Appeal { get; set; } = null!;

    public virtual User? Executor { get; set; }
}
