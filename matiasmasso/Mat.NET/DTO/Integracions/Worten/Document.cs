using System;
using System.Collections.Generic;

namespace DTO.Integracions.Worten
{
    public class DocumentList
    {
        public List<Document> order_documents { get; set; }
    }
    public class Document
    {

        public DateTime date_uploaded { get; set; }
        public string file_name { get; set; }
        public int file_size { get; set; }
        public string id { get; set; }
        public string order_id { get; set; }
        public string type { get; set; } //SYSTEM_DELIVERY_BILL

        public enum DocTypes
        {
            NotSet,
            SYSTEM_DELIVERY_BILL
        }

    }
}
