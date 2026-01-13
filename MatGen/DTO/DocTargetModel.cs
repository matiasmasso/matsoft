namespace DTO
{
    public class DocTargetModel : BaseGuid, IModel
    {
        public Guid? Doc { get; set; }
        public Guid? Target { get; set; }
        public TargetCods TargetCod { get; set; } = TargetCods.Person;
        public Guid? Rel { get; set; }
        public bool Difunt { get; set; }
        public bool SubjectePassiu { get; set; }

        //just when Doc is not persisted yet
        //public DocModel? DocModel { get; set; }

        public enum TargetCods
        {
            NotSet,
            Person,
            Location
        }

        public enum MenuKeys
        {
            Doc,
            Person,
            Location,
            DocRel,
            AddCreate,
            AddExisting,
            AddCreatePare,
            AddCreateMare,
        }

        public DocTargetModel() : base() { }
        public DocTargetModel(Guid guid) : base(guid) { }
        public static DocTargetModel Factory() => new DocTargetModel();

        public static DocTargetModel Factory(DocModel doc, DocRelModel.Wellknowns? rel = null, bool subjectePassiu = false, bool difunt = false)
        {
            var item = new DocTargetModel { Doc = doc.Guid };
            if (rel != null)
                item.Rel = DocRelModel.Wellknown((DocRelModel.Wellknowns)rel!)!.Guid;
            item.SubjectePassiu = subjectePassiu;
            item.Difunt = difunt;
            return item;
        }

        public string PropertyPageUrl() => Globals.PageUrl("docTarget", Guid.ToString());
        public string CreatePageUrl() => Globals.PageUrl("docTarget");
        public static string CreatePageUrlFromDoc(DocModel fromDoc, DocRelModel.Wellknowns? docRel = null, Guid? personGuid = null)
        {
            if (docRel == null && personGuid == null)
                return Globals.PageUrl("docTarget/fromDoc", fromDoc.Guid.ToString());
            else if (personGuid == null)
                return Globals.PageUrl("docTarget/fromDoc", fromDoc.Guid.ToString(), ((int)docRel!).ToString());
            else
                return Globals.PageUrl("docTarget/fromDoc", fromDoc.Guid.ToString(), ((int)docRel!).ToString(), personGuid.ToString()!);
        }

        public bool Matches(string? searchTerm)
        {
            //TODO: completar
            bool retval = true;
            return retval;
        }

        public string Caption() => Guid.ToString(); //To implement iModel Interface for property grid selectors



        public bool IsMenuItemVisible(string menuKey)
        {
            bool retval = true;
            switch((MenuKeys)Convert.ToInt32(menuKey))
            {
                case MenuKeys.Person:
                    retval = TargetCod == TargetCods.Person;
                    break;
                case MenuKeys.Location:
                    retval = TargetCod == TargetCods.Location;
                    break;
                case MenuKeys.AddCreatePare:
                    retval = TargetCod == TargetCods.Person;
                    break;
                case MenuKeys.AddCreateMare:
                    retval = TargetCod == TargetCods.Person;
                    break;
            }
            return retval;
        }
    }
}
