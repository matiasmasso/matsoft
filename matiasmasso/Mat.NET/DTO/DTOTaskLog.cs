using System;
using System.Collections.Generic;

namespace DTO
{
    public class DTOTaskLog : DTOBaseGuid
    {
        public DateTimeOffset Fch { get; set; }
        public DTOTask.ResultCods ResultCod { get; set; }
        public string ResultMsg { get; set; }

        public DTOTaskLog() : base()
        {
        }

        public DTOTaskLog(Guid oGuid) : base(oGuid)
        {
        }

        public static DTOTaskLog Factory()
        {
            DTOTaskLog retval = new DTOTaskLog();
            {
                retval.ResultCod = DTOTask.ResultCods.Running;
                retval.Fch = DateTimeOffset.Now;
            }
            return retval;
        }

        public void Merge(DTOTaskLog aux)
        {
            SetResult(aux.ResultCod, aux.ResultMsg);
        }

        public void SetResult(DTOTask.ResultCods cod, string msg, List<Exception> exs = null)
        {
            switch (cod)
            {
                case DTOTask.ResultCods.Empty:
                    this.ResultCod = cod;
                    break;
                case DTOTask.ResultCods.Running:
                    this.ResultCod = cod;
                    break;
                case DTOTask.ResultCods.DoneWithErrors:
                    if (cod == DTOTask.ResultCods.Failed)
                        this.ResultCod = cod;
                    break;
                case DTOTask.ResultCods.Failed:
                    break;
            }

            if (!string.IsNullOrEmpty(msg))
            {
                if (!string.IsNullOrEmpty(ResultMsg))
                    ResultMsg += Environment.NewLine;
                ResultMsg += msg;
            }

            if (exs != null)
            {
                foreach (Exception ex in exs)
                {
                    ResultMsg += Environment.NewLine;
                    ResultMsg += ex.Message;
                }
            }
        }
    }
}
