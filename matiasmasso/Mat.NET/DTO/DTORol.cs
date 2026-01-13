namespace DTO
{
    public class DTORol
    {
        public Ids id { get; set; } //lowercase due to dependency on IOS iMat 24/3/2022

        public DTOLangText Nom { get; set; }

        public string Dsc { get; set; }

        public bool IsLoaded { get; set; }
        public bool IsNew { get; set; }

        public enum Ids
        {
            notSet,
            superUser,
            admin,
            operadora,
            accounts,
            salesManager,
            comercial,
            taller,
            rep = 8,
            cliFull = 9,
            manufacturer = 10,
            marketing = 11,
            cliLite = 12,
            auditor = 14,
            banc = 15,
            transportista = 16,
            logisticManager = 17,
            pr = 18,
            guest = 20,
            employee = 21,
            lead = 98,
            unregistered = 99,
            denied = 100
        }

        public DTORol() : base()
        {
            id = Ids.unregistered;
            Nom = new DTOLangText();
        }

        public DTORol(Ids oId) : base()
        {
            id = oId;
            Nom = new DTOLangText();
        }

        public string nomEsp
        {
            get
            {
                return Nom.Esp;
            }
        }

        public bool isAuthenticated()
        {
            bool retval = false;
            switch (id)
            {
                case DTORol.Ids.notSet:
                case DTORol.Ids.unregistered:
                case DTORol.Ids.denied:
                    {
                        break;
                    }

                default:
                    {
                        retval = true;
                        break;
                    }
            }
            return retval;
        }


        public bool isSuperAdmin()
        {
            bool Retval = false;
            switch (id)
            {
                case Ids.superUser:
                    {
                        Retval = true;
                        break;
                    }
            }
            return Retval;
        }

        public bool isAdmin()
        {
            bool Retval = false;
            switch (id)
            {
                case Ids.superUser:
                case Ids.admin:
                    {
                        Retval = true;
                        break;
                    }
            }
            return Retval;
        }

        public bool isMainboard()
        {
            bool Retval = false;
            switch (id)
            {
                case Ids.superUser:
                case Ids.admin:
                case Ids.salesManager:
                    {
                        Retval = true;
                        break;
                    }
            }
            return Retval;
        }

        public bool isStaff(Ids oRolId = Ids.notSet)
        {
            if (oRolId == Ids.notSet)
                oRolId = id;

            bool oRetVal = false;
            switch (oRolId)
            {
                case Ids.superUser:
                case Ids.admin:
                case Ids.salesManager:
                case Ids.accounts:
                case Ids.logisticManager:
                case Ids.marketing:
                case Ids.operadora:
                case Ids.comercial:
                case Ids.taller:
                    {
                        oRetVal = true;
                        break;
                    }
            }
            return oRetVal;
        }

        public bool isRep(Ids oRolId = Ids.notSet)
        {
            if (oRolId == Ids.notSet)
                oRolId = id;

            bool oRetVal = false;
            switch (oRolId)
            {
                case Ids.rep:
                case Ids.comercial:
                    {
                        oRetVal = true;
                        break;
                    }
            }

            return oRetVal;
        }

        public bool isCustomer(Ids oRolId = Ids.notSet)
        {
            if (oRolId == Ids.notSet)
                oRolId = id;

            bool oRetVal = false;
            switch (oRolId)
            {
                case Ids.cliFull:
                case Ids.cliLite:
                    {
                        oRetVal = true;
                        break;
                    }
            }

            return oRetVal;
        }

        public bool isProfesional()
        {
            bool retval = false;
            switch (id)
            {
                case Ids.denied:
                case Ids.guest:
                case Ids.lead:
                case Ids.notSet:
                case Ids.unregistered:
                    {
                        break;
                    }

                default:
                    {
                        retval = true;
                        break;
                    }
            }
            return retval;
        }

        public new bool Equals(object oCandidate)
        {
            bool retval = false;
            if (oCandidate != null)
            {

                if (oCandidate.GetType() == typeof(DTORol))
                {
                    DTORol oCandidateRol = (DTORol)oCandidate;
                    retval = (oCandidateRol.id == this.id);
                }

            }
            return retval;
        }
    }
}
