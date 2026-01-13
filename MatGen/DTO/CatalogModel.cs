using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class CatalogModel
    {
        public List<UserModel> UserAccounts { get; set; } = new();
        public List<DocModel> Docs { get; set; } = new();
        public List<PersonModel> Persons { get; set; } = new();
        public List<DocTargetModel> DocTargets { get; set; } = new();
        public List<DocSrcModel> DocSrcs { get; set; } = new();
        public List<DocCodModel> DocCods { get; set; } = new();
        public List<DocRelModel> DocRels { get; set; } = new();
        public List<EnlaceModel> Enlaces { get; set; } = new();
        public List<LocationModel> Locations { get; set; } = new();
        public List<ProfessionModel> Professions { get; set; } = new();
        public List<FirstnomModel> Firstnoms { get; set; } = new();
        public List<CognomModel> Cognoms { get; set; } = new();
        public List<Guid>? RootAncestors { get; set; } = new();
        public PersonModel? RootPerson { get; set; } = new();
    }
}
