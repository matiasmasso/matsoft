using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class IdNom
    {
        public int Id { get; set; }
        public string? Nom { get; set; }

        public override string ToString()
        {
            return Nom ?? "";
        }
    }

    public class IdNoms
    {
        public static List<IdNom> FromEnum<TEnum>(List<TEnum>? values = null) where TEnum : struct, Enum
        {
            if (values == null) values = Enum.GetValues(typeof(TEnum)).Cast<TEnum>().ToList();
            var retval = values // Enum.GetValues(typeof(TEnum))
                   .Cast<TEnum>()
                   .Select(e => new IdNom { Id = (int)(object)e, Nom = e.ToString() })
                   .ToList();
            return retval;
        }
    }
}
