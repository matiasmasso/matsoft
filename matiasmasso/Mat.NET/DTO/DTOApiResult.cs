using System;
using System.Collections.Generic;

namespace DTO
{
    public class DTOApiResult
    {
        public bool Success { get; set; }
        public List<String> ExceptionMessages { get; set; }


        public DTOApiResult()
        {
            ExceptionMessages = new List<String>();
        }

        public static DTOApiResult Factory(bool success, List<Exception> exs = null)
        {
            DTOApiResult retval = new DTOApiResult();
            retval.Success = success;
            if (exs != null)
            {
                foreach (Exception ex in exs)
                {
                    retval.ExceptionMessages.Add(ex.Message);
                }
            }
            return retval;
        }

        public static DTOApiResult Succeeded()
        {
            return DTOApiResult.Factory(true);
        }

        public static DTOApiResult Failed(List<Exception> exs)
        {
            return DTOApiResult.Factory(false, exs);
        }

        public List<Exception> exs()
        {
            List<Exception> retval = new List<Exception>();
            foreach (string msg in ExceptionMessages)
            {
                Exception ex = new Exception(msg);
                retval.Add(ex);
            }
            return retval;
        }
    }
}
