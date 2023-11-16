using System;
using System.Collections.Generic;

namespace TechServicePractice;

public partial class User
{
    public override string ToString()
    {
        return Fio;
    }
    public long Id { get; set; }

    public string Fio { get; set; } = null!;

    public DateTime DateOfBirth { get; set; }

    public string PhoneNumber { get; set; } = null!;

    public string UserRoler { get; set; } = null!;

    public string UserPassword { get; set; } = null!;

    public virtual ICollection<Appeal> Appeals { get; set; } = new List<Appeal>();

    public virtual ICollection<Request> Requests { get; set; } = new List<Request>();
}
