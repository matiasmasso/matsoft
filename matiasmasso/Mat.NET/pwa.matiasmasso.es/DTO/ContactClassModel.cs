using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ContactClassModel:BaseGuid,IModel
    {
        public Guid Channel { get; set; }

        public LangTextModel? Nom { get; set; }

        public enum Wellknowns
        {
            Banc,
            Notari
        }

        public ContactClassModel() : base() {}
        public ContactClassModel(Guid guid) : base(guid) { }

        public static ContactClassModel? Wellknown(ContactClassModel.Wellknowns id)
        {
            if (id == Wellknowns.Banc)
                return new ContactClassModel(new Guid("8B731793-7A54-4683-9FB9-5E6B00C91220"));
            else if (id == Wellknowns.Notari)
                return new ContactClassModel(new Guid("7BC5A035-947A-4F2F-9FCB-4E9DB2635796"));
            else
                return null;
        }


        public override string ToString()
        {
            return Nom?.Esp ?? "?";
        }
    }


}
