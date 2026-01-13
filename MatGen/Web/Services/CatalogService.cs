using DTO;
using System.Runtime.CompilerServices;

namespace Web.Services
{

    public class CatalogService
    {
        //public event Action<Exception?>? OnChange;

        public List<UserModel>? Users { get; set; }
        public List<PersonModel>? Persons { get; set; }
        public List<FirstnomModel>? Firstnoms { get; set; }
        public List<CognomModel>? Cognoms { get; set; }
        public List<EnlaceModel>? Enlaces { get; set; }
        public List<ProfessionModel>? Professions { get; set; }
        public List<DocModel>? Docs { get; set; }
        public List<DocSrcModel>? DocSrcs { get; set; }
        public List<DocCodModel>? DocCods { get; set; }
        public List<DocRelModel>? DocRels { get; set; }
        public List<DocTargetModel>? DocTargets { get; set; }
        public List<LocationModel>? Locations { get; set; }
        public List<PubModel>? Pubs { get; set; }
        public List<CitaModel>? Citas { get; set; }
        public List<RepoModel>? Repos { get; set; }

        public States State = States.IsEmpty;

        public enum States
        {
            IsEmpty,
            IsLoading,
            IsLoaded
        }


        public void Reset()
        {
            Users = null;
            Persons = null;
            Firstnoms = null;
            Cognoms = null;
            Enlaces = null;
            Professions = null;
            Docs = null;
            DocSrcs = null;
            DocCods = null;
            DocRels = null;
            DocTargets = null;
            Locations = null;
            Pubs = null;
            Repos = null;
        }

    }
}
