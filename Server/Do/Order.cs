using System;
using System.Collections.Generic;

namespace Server.Do;

public partial class Order
{
    public int IdOrder { get; set; }

    public int? IdSchool { get; set; }

    public string Contact { get; set; } = null!;

    public string PhoneContact { get; set; } = null!;

    public string ProvisionAddress { get; set; } = null!;

    public DateTime DateOfOrdder { get; set; }

    public DateTime DateOfEvent { get; set; }

    public int CostPrice { get; set; }

    public string? SchoolName { get; set; }

    public virtual ICollection<DetailingOrder> DetailingOrders { get; set; } = new List<DetailingOrder>();

    public virtual School? IdSchoolNavigation { get; set; }
}
