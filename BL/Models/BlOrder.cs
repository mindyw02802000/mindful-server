using System;
using System.Collections.Generic;

namespace BL.Models;

public partial class BlOrder
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
    public string? Email { get; set; }

    public List<BlDetailingOrder> DetailingOrders { get; set; }=new List<BlDetailingOrder>();

   
    
}
