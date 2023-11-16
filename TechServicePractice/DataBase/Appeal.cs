using System;
using System.Collections.Generic;

namespace TechServicePractice;

public partial class Appeal
{
    public long Id { get; set; }

    public DateTime DateOfAppeal { get; set; }

    public string AppealStatus { get; set; } = null!;

    public string GadgetType { get; set; } = null!;

    public string ProblemDescription { get; set; } = null!;

    public long ClientId { get; set; }

    public virtual User Client { get; set; } = null!;

    public virtual ICollection<Request> Requests { get; set; } = new List<Request>();
}
