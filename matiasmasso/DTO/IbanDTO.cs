using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using static DTO.StoreLocatorDTO;

namespace DTO
{
    public class IbanModel:BaseGuid
    {
        public Guid Titular { get; set; }
        public Cods Cod { get; set; }
        public Guid? Branch { get; set; }
        public string? Ccc { get; set; }

        public DateTime? FchFrom { get; set; }
        public DateTime? FchTo { get; set; }

        public enum Cods
        {
            _NotSet,
            proveidor,
            client,
            staff,
            banc
        }

        public IbanModel() : base() { }
        public IbanModel(Guid guid) : base(guid) { }

        public string? FormatedDigits() => Ccc == null ? null : Regex.Replace(Ccc, ".{4}", "$0 ").Trim();

        public bool IsActive(DateTime? fch = null)
        {
            if (fch == null) fch = DateTime.Now;
            var tooSoon = FchFrom != null && FchFrom > fch;
            var tooLate = FchTo != null && FchTo < fch;
            var retval  = !tooSoon && !tooLate;
            return retval;
        }


    }
}
