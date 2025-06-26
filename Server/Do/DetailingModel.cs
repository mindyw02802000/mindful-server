using System;
using System.Collections.Generic;

namespace Server.Do;

public partial class DetailingModel
{
    public int IdModel { get; set; }

    public int Size { get; set; }

    public int Count { get; set; }

    public int Id { get; set; }

    public virtual Modell IdModelNavigation { get; set; } = null!;
}
