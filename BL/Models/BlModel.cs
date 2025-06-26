using Dal.Do;
using System;
using System.Collections.Generic;

namespace BL.Models;

public partial class BlModell
{
    public int IdModel { get; set; }

    public int Price { get; set; }

    public string Picture { get; set; } 

    public string Kategory { get; set; }

    public virtual ICollection<BlDetailingModel> DetailingModels { get; set; } = new List<BlDetailingModel>();


}
