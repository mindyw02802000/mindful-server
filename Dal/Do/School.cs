using System;
using System.Collections.Generic;

namespace Dal.Do;

public partial class School
{
    public int IdSchool { get; set; }

    public string Name { get; set; } = null!;

    public string AddressSchool { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
