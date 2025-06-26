using Dal.Do;
using System;
using System.Collections.Generic;

namespace BL.Models;

public partial class BlDetailingModel
{
    public int IdModel { get; set; }

    public int Size { get; set; }

    public int Count { get; set; }

    public int CountByDate { get; set; }

    public int Id { get; set; }

/*    public virtual Modell IdModelNavigation { get; set; }*/
}
