using System;
using System.Collections.Generic;

namespace Server.Do;

public partial class Modell
{
    public int IdModel { get; set; }

    public int Price { get; set; }

    public string Picture { get; set; } = null!;

    public string Kategory { get; set; } = null!;

    public virtual ICollection<DetailingModel> DetailingModels { get; set; } = new List<DetailingModel>();
}
