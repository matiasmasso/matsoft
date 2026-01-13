using System;
using System.Collections.Generic;

namespace DTO
{
    public class DTOTaskResult
    {
        public DTOTask.ResultCods ResultCod { get; set; }
        public string Msg { get; set; }
        public List<Exception> Exceptions { get; set; }

        public DTOTaskResult() : base()
        {
            Exceptions = new List<Exception>();
        }

        public static DTOTaskResult Factory(DTOTask.ResultCods oResultCod, string msg = "", List<Exception> exs = null)
        {
            DTOTaskResult retval = new DTOTaskResult();
            retval.ResultCod = oResultCod;
            retval.Msg = msg;
            if (exs != null)
                retval.AddExceptions(exs);
            return retval;
        }

        public bool Success()
        {
            bool retval = false;
            switch (ResultCod)
            {
                case DTOTask.ResultCods.Success:
                case DTOTask.ResultCods.Empty:
                    {
                        retval = true;
                        break;
                    }
            }
            return retval;
        }

        public static DTOTaskResult SuccessResult(string msg = "")
        {
            DTOTaskResult retval = new DTOTaskResult();
            retval.ResultCod = DTOTask.ResultCods.Success;
            retval.Msg = msg;
            return retval;
        }

        public static DTOTaskResult FailResult(List<System.Exception> exs, string msg = "")
        {
            DTOTaskResult retval = new DTOTaskResult();
            {
                var withBlock = retval;
                withBlock.ResultCod = DTOTask.ResultCods.Failed;
                withBlock.AddExceptions(exs);
                withBlock.Msg = msg;
            }
            return retval;
        }

        public void Fail()
        {
            ResultCod = DTOTask.ResultCods.Failed;
        }

        public void Fail(string stringFormatMsg, params string[] stringFormatValues)
        {
            Fail();
            if (stringFormatValues.Length > 0)
                Msg = string.Format(stringFormatMsg, stringFormatValues);
            else
                Msg = stringFormatMsg;
        }

        public void Fail(System.Exception ex, string stringFormatMsg, params string[] stringFormatValues)
        {
            Fail(stringFormatMsg, stringFormatValues);
            AddException(ex);
        }

        public void Fail(IEnumerable<System.Exception> exs, string stringFormatMsg, params string[] stringFormatValues)
        {
            Fail(stringFormatMsg, stringFormatValues);
            AddExceptions(exs);
        }

        public List<System.Exception> SystemExceptions()
        {
            List<System.Exception> retval = new List<System.Exception>();
            foreach (Exception ex in Exceptions)
                retval.Add(new System.Exception(ex.Message));
            return retval;
        }




        public void Succeed()
        {
            ResultCod = DTOTask.ResultCods.Success;
        }

        public void Succeed(string stringFormatMsg, params string[] stringFormatValues)
        {
            Succeed();
            if (stringFormatValues.Length > 0)
                Msg = string.Format(stringFormatMsg, stringFormatValues);
            else
                Msg = stringFormatMsg;
        }

        public void Empty()
        {
            ResultCod = DTOTask.ResultCods.Empty;
        }

        public void Empty(string stringFormatMsg, params string[] stringFormatValues)
        {
            Empty();
            if (stringFormatValues.Length > 0)
                Msg = string.Format(stringFormatMsg, stringFormatValues);
            else
                Msg = stringFormatMsg;
        }

        public void AddException(string stringFormatMsg, params string[] stringFormatValues)
        {
            Exception ex = null;
            if (stringFormatValues.Length > 0)
                ex = new Exception(string.Format(stringFormatMsg, stringFormatValues));
            else
                ex = new Exception(stringFormatMsg);
            Exceptions.Add(ex);
            if (ResultCod == DTOTask.ResultCods.Success)
                ResultCod = DTOTask.ResultCods.DoneWithErrors;
        }

        public void AddException(System.Exception ex)
        {
            AddException(ex.Message);
        }

        public void AddExceptions(IEnumerable<System.Exception> exs)
        {
            foreach (var ex in exs)
                AddException(ex.Message);
        }

        public void DoneWithErrors()
        {
            ResultCod = DTOTask.ResultCods.DoneWithErrors;
        }

        public void DoneWithErrors(string stringFormatMsg, params string[] stringFormatValues)
        {
            DoneWithErrors();
            if (stringFormatValues.Length > 0)
                Msg = string.Format(stringFormatMsg, stringFormatValues);
            else
                Msg = stringFormatMsg;
        }

        public string ResultReport()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.AppendLine("Cod: " + ResultCod.ToString());
            sb.AppendLine("Msg: " + Msg);
            foreach (var ex in Exceptions)
                sb.AppendLine(ex.Message);
            string retval = sb.ToString();
            return retval;
        }


    }
}
