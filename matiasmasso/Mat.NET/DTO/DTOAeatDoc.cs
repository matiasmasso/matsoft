using MatHelperStd;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DTO
{
    public class DTOAeatDoc : DTOBaseGuid
    {
        public DTOEmp Emp { get; set; }
        public DTOAeatModel Model { get; set; }
        public int Period { get; set; }
        public DateTime Fch { get; set; }
        public DTODocFile DocFile { get; set; }

        public DTOAeatDoc() : base()
        {
        }

        public DTOAeatDoc(Guid oGuid) : base(oGuid)
        {
        }

        public class Header
        {
            public Guid Guid { get; set; }
            public DateTime Fch { get; set; }
            public string DownloadUrl { get; set; }
            public string ThumbnailUrl { get; set; }
            public string Features { get; set; }

            public class Collection : List<DTOAeatDoc.Header>
            {
                public static Collection Factory(DTOAeatDoc.Collection src)
                {
                    Collection retval = new Collection();
                    foreach (var oDoc in src)
                    {
                        DTOAeatDoc.Header item = new DTOAeatDoc.Header();
                        item.Guid = oDoc.Guid;
                        item.Fch = oDoc.Fch;
                        if (oDoc.DocFile != null)
                        {
                            item.DownloadUrl = oDoc.DocFile.DownloadUrl();
                            item.ThumbnailUrl = oDoc.DocFile.ThumbnailUrl();
                        }
                        retval.Add(item);
                    }
                    return retval;
                }

                public List<int> Years()
                {
                    return this.GroupBy(x => x.Fch.Year).Select(y => y.First()).Select(z => z.Fch.Year).ToList();
                }

                //public string Serialized()
                //{
                //    List<Exception> exs = new List<Exception>();
                //    JavaScriptSerializer serializer = new JavaScriptSerializer();
                //    return JsonHelper.Serialize(this, exs);
                //}
            }
        }

        public class Collection : List<DTOAeatDoc>
        {
        }
    }
}
