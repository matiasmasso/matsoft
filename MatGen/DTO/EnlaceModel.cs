using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class EnlaceModel:BaseGuid, IModel
    {
        public Guid? Marit { get; set; }
        public Guid? Muller { get; set; }
        public int? NupciesMarit { get; set; }
        public int? NupciesMuller { get; set; }

        public FchLocationModel? FchLocation { get; set; }
        //public string? Fch { get; set; }
        //public Guid? Location { get; set; }

        public enum MenuKeys
        {
            Zoom,
            AddChild
        }

        public EnlaceModel() : base() { }
        public EnlaceModel(Guid guid) : base(guid) { }
        public static EnlaceModel Factory() => new EnlaceModel();

        public static string CollectionPageUrl() => Globals.PageUrl("enlaces");
        public string PropertyPageUrl(){
            string retval = Globals.PageUrl("enlace");
            if (IsNew)
            {
                if(Marit!= null & Muller != null)
                    retval = Globals.PageUrl("enlace/FromConjuges", Marit.ToString()!, Muller.ToString()!);
                else if (Marit != null)
                    retval = Globals.PageUrl("enlace/FromConjuges", Marit.ToString()!);
                else if (Muller != null)
                    retval = Globals.PageUrl("enlace/FromConjuges", Muller.ToString()!);
            }
            else
            {
                retval = Globals.PageUrl("enlace", Guid.ToString());
            }
            return retval;
        }
        public string CreatePageUrl() => Globals.PageUrl("enlace");
        public static string CreatePageUrlFromConjuge(PersonModel conjuge) => Globals.PageUrl("enlace/FromConjuges",conjuge.Guid.ToString());


        public Guid? Conjuge(PersonModel? fromPerson)
        {
            Guid? retval = null;
            if(fromPerson != null)
            {
                if (Muller == fromPerson.Guid)
                    retval = Marit;
                else if (Marit == fromPerson.Guid)
                    retval = Muller;
            }
            return retval;
        }

        public string Year()
        {
            string retval = "XXXX";
            try
            {
                retval = FchLocation?.Fch?.Fch1?.Truncate(4) ?? "XXXX";
                //if (Fch != null) 
                //retval = Fch.Substring(0, Math.Min(4, Fch.Length));

            } catch(Exception ex)
            {
                var a = ex.Message;
            }
            return retval;
        }

        public List<PersonModel> Children(List<PersonModel> allPersons)
        {
            return allPersons.Where(x => x.Pare == Marit && x.Mare == Muller).ToList();
        }

        public bool Matches(string? searchTerm)
        {
            //TODO: completar
            bool retval = true;
            return retval;
        }

        public bool BelongsToAncestor(List<Guid>? ancestors)
        {
            bool retval = false;
            if (ancestors != null)
            {
                if (Marit != null && ancestors.Contains((Guid)Marit)) retval = true;
                else if (Muller != null && ancestors.Contains((Guid)Muller)) retval = true;
            }
            return retval;
        }

        public string Caption() => Guid.ToString(); //To implement iModel Interface for property grid selectors


    }
}
