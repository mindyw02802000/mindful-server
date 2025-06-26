using System;
using System.Collections.Generic;

namespace Dal.Do;

public partial class DetailingOrder
{
    public int IdOrder { get; set; }

    public int IdModel { get; set; }

    public int Size { get; set; }

    public int Count { get; set; }

    public int Id { get; set; }

    public virtual Order IdOrderNavigation { get; set; } = null!;
}
