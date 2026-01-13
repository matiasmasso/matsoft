using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class MsgModel:BaseGuid
    {
        public int Id { get; set; }
        public string? Content { get; set; }
        public Srcs Src { get; set; } = Srcs.None;
        public DateTime FchCreated { get; set; } = DateTime.Now;
        public Guid UsrCreated { get; set; }

        public UsrLogModel? UsrLog { get; set; }


        public MsgModel():base() { }
        public MsgModel(Guid guid):base(guid) { }

        public enum Srcs
        {
            None,
            Shop4moms
        }

    }
}
