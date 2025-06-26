using System;
using System.Collections.Generic;

namespace BL.Models;

public partial class BlSchool
{
    public int IdSchool { get; set; }

    public string Name { get; set; } = null!;

    public string AddressSchool { get; set; } = null!;

    public string Phone { get; set; } = null!;

}
