using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class EnumItemModel:IModel
    {
        public Guid Guid { get; set; } = System.Guid.NewGuid();
        public string? Name { get; set; }
        public int Value { get; set; }

        public bool IsNew { get; set; }
        //Guid IModel.Guid { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }


        public static List<IModel>Factory(Enum e)
        {
            var retval = new List<IModel>();
            var values = Enum.GetValues(e.GetType()).Cast<Int32>().ToList();
            retval.AddRange( values
                .Select(x => new EnumItemModel
                {
                    Value = x,
                    Name = Enum.GetName(e.GetType(), x)
                }));
            return retval;
        }

        public string Caption() => Name ?? "?";

        public override string ToString()
        {
            return Caption();
        }

        public string PropertyPageUrl() => String.Empty;

        public bool Matches(string? searchTerm)
        {
            bool retval = true;
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var searchTerms = searchTerm.Split("+", StringSplitOptions.RemoveEmptyEntries);
                var searchTarget = Name ?? "";
                retval = searchTerms.All(x => searchTarget.Contains(x, StringComparison.OrdinalIgnoreCase));
            }
            return retval;
        }
    }
}
