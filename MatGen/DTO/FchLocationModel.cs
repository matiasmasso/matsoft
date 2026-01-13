using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class FchLocationModel
    {
        public FchModel? Fch { get; set; }
        public LocationModel? Location { get; set; }

        public FchLocationModel() : base() { }

        public FchLocationModel(string? qualifier = null, string? fch1 = null, string? fch2 = null, Guid? location=null)
        {
            Fch = new FchModel(fch1, fch2, Enum.TryParse<FchModel.Qualifiers>(qualifier, out var q) ? q : null);
            Location = location != null ? new LocationModel((Guid)location) : null;
        }

        public FchLocationModel(string? fch, LocationModel? location)
        { //DEPRECATED
            Fch = new FchModel(fch);
            Location = location;
        }

        public string? Year => Fch?.Fch1?.Truncate(4);

        public FchLocationModel AddYears(int years) //clones the object with modified years
        {
            var retval = new FchLocationModel
            {
                Location = this.Location,
                Fch = this.Fch != null ? this.Fch.AddYears(years) : null
            };
            return retval;
        }

        public override string? ToString()
        {
            string? retval = null;
            string? location = Location?.ToString();
            string? fch = Fch?.ToString()!;

            if (string.IsNullOrEmpty(location) && string.IsNullOrEmpty(fch))
                retval = null;
            else if (string.IsNullOrEmpty(fch))
                retval = location;
            else if (string.IsNullOrEmpty(location))
                retval = fch;
            else
                retval = $"{location}, {fch}";
            return retval;
        }
    }
}
