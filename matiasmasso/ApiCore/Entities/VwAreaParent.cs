using System;
using System.Collections.Generic;

namespace Api.Entities;

public partial class VwAreaParent
{
    /// <summary>
    /// Foreign key for parent element to an area table depending on ParentCod field value
    /// </summary>
    public Guid ParentGuid { get; set; }

    /// <summary>
    /// Foreign key for child element to an area table depending on Child code
    /// </summary>
    public Guid ChildGuid { get; set; }

    /// <summary>
    /// Area code for parent element: 1:Country, 2:Zona, 3:Location, 4:Zip
    /// </summary>
    public int ParentCod { get; set; }

    /// <summary>
    /// Area code for child element: 1:Country, 2:Zona, 3:Location, 4:Zip
    /// </summary>
    public int ChildCod { get; set; }
}
