using System;

namespace DTO
{
    public class DTOFeedback : DTOBaseGuid
    {
        public DTOBaseGuid Target { get; set; }
        public DateTime Fch { get; set; }
        public DTOGuidNom UserOrCustomer { get; set; }
        public UserOrCustomerCods UserOrCustomerCod { get; set; }
        public Cods Cod { get; set; }
        public enum Cods
        {
            NotSet,
            Like,
            Share
        }


        public enum UserOrCustomerCods
        {
            NotSet,
            User,
            Customer
        }

        public DTOFeedback() : base()
        {

        }

        public DTOFeedback(Guid guid) : base(guid)
        {

        }

        public static DTOFeedback Factory(DTOBaseGuid target, Cods cod, DTOUser user = null)
        {
            DTOFeedback retval = new DTOFeedback();
            retval.Target = target;
            retval.Fch = DTO.GlobalVariables.Now();
            retval.Cod = cod;
            if (user != null)
                retval.UserOrCustomer = new DTOGuidNom(user.Guid, user.NicknameOrElse());
            retval.UserOrCustomerCod = UserOrCustomerCods.User;
            return retval;
        }

        public class Model
        {
            public int Likes { get; set; }
            public int Shares { get; set; }

            public bool HasLiked { get; set; }
            public bool HasShared { get; set; }
        }

    }
}
