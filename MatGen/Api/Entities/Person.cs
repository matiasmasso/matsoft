using System;
using System.Collections.Generic;

namespace Api.Entities;

public partial class Person
{
    public Guid Guid { get; set; }

    public string? Nom { get; set; }

    public short Sex { get; set; }

    public Guid? Pare { get; set; }

    public Guid? Mare { get; set; }

    public string? FchFromQualifier { get; set; }

    public string? FchFrom { get; set; }

    public string? FchFrom2 { get; set; }

    public string? FchToQualifier { get; set; }

    public string? FchTo { get; set; }

    public string? FchTo2 { get; set; }

    public string? FchSepulturaQualifier { get; set; }

    public string? FchSepultura { get; set; }

    public string? FchSepultura2 { get; set; }

    public Guid? LocationFrom { get; set; }

    public Guid? LocationTo { get; set; }

    public Guid? Sepultura { get; set; }

    public Guid? Firstnom { get; set; }

    public Guid? Cognom { get; set; }

    public Guid? Profession { get; set; }

    public string? Obs { get; set; }

    public DateTime FchCreated { get; set; }

    public DateTime FchLastEdited { get; set; }

    public virtual Cognom? CognomNavigation { get; set; }

    public virtual ICollection<Enlace> EnlaceMaritNavigations { get; set; } = new List<Enlace>();

    public virtual ICollection<Enlace> EnlaceMullerNavigations { get; set; } = new List<Enlace>();

    public virtual Firstnom? FirstnomNavigation { get; set; }

    public virtual ICollection<Person> InverseMareNavigation { get; set; } = new List<Person>();

    public virtual ICollection<Person> InversePareNavigation { get; set; } = new List<Person>();

    public virtual Person? MareNavigation { get; set; }

    public virtual Person? PareNavigation { get; set; }

    public virtual Profession? ProfessionNavigation { get; set; }
}
