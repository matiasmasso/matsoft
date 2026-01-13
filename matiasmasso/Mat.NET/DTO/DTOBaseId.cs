namespace DTO
{
    public class DTOBaseId
    {
        private int _Id;

        public object AuxObject { get; set; }

        public bool IsNew { get; set; }
        public bool IsLoaded { get; set; }

        public DTOBaseId(int iId) : base()
        {
            _Id = iId;
        }

        public int Id
        {
            get
            {
                return _Id;
            }
        }

        public bool UnEquals(DTOBaseId oCandidate)
        {
            bool retval = !Equals(oCandidate);
            return retval;
        }

        public bool Equals(DTOBaseId oCandidate)
        {
            bool retval = false;
            if (oCandidate != null)
                retval = _Id == oCandidate.Id;
            return retval;
        }
    }
}
