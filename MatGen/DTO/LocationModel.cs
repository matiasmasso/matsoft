using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class LocationModel : BaseGuid, IModel
    {
        public Guid? Parent { get; set; }
        public string? Nom { get; set; }
        public string? NomLlarg { get; set; }

        public LocationModel() : base() { }
        public LocationModel(Guid guid) : base(guid) { }
        public static LocationModel Factory(LocationModel? parent = null) => new LocationModel {Parent = parent?.Guid,  Nom = "(nova població)" };

        public static string CollectionPageUrl() => Globals.PageUrl("locations");
        public string PropertyPageUrl() => Globals.PageUrl("location", Guid.ToString());
        public string CreatePageUrl() => Globals.PageUrl("Location");

        public string AddNewChildPageUrl() => Globals.PageUrl("Location/fromParent", Guid.ToString());

        public bool Matches(string? searchTerm)
        {
            bool retval = true;
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var searchTerms = searchTerm.Split("+", StringSplitOptions.RemoveEmptyEntries);
                var searchTarget = NomLlarg ?? "";
                var compareInfo = CultureInfo.InvariantCulture.CompareInfo;
                var options = CompareOptions.IgnoreCase | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreNonSpace;
                retval = searchTerms.All(x => compareInfo.IndexOf(searchTarget, x, options) >= 0);
            }
            return retval;
        }

        public string Caption() => NomLlarg ?? ""; //To implement iModel Interface for property grid selectors
        public override string? ToString() => NomLlarg;

        public List<PersonModel> Persons(List<LocationModel>? allLocations, List<PersonModel>? allPersons)
        {
            return Persons(this, allLocations, allPersons);
        }

        public static List<PersonModel> Persons(LocationModel item, List<LocationModel>? allLocations, List<PersonModel>? allPersons)
        {
            var retval = new List<PersonModel>();
            if (allLocations != null && allPersons != null)
            {
                retval = allPersons.Where(x => x.FchLocationFrom?.Location?.Guid == item.Guid).ToList();
                foreach (var childLocation in allLocations.Where(x => x.Parent == item?.Guid))
                {
                    retval.AddRange(Persons(childLocation, allLocations, allPersons));
                }
            }
            return retval;
        }
        public List<DocTargetModel> DocTargets(List<LocationModel>? allLocations, List<DocTargetModel>? allDocTargets)
        {
            return DocTargets(this, allLocations, allDocTargets);
        }

        public static List<DocTargetModel> DocTargets(LocationModel item, List<LocationModel>? allLocations, List<DocTargetModel>? allDocTargets)
        {
            List<DocTargetModel> retval = new();
            if (allLocations != null & allDocTargets != null)
            {
                retval = allDocTargets!.Where(x => x.Target == item.Guid).ToList();
                foreach (var childLocation in allLocations!.Where(x => x.Parent == item?.Guid))
                {
                    retval.AddRange(DocTargets(childLocation, allLocations, allDocTargets));
                }
            }
            return retval;
        }
    }
}
