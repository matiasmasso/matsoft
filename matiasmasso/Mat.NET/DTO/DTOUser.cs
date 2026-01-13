using MatHelperStd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace DTO
{
    public class DTOUser : DTOBaseTel
    {
        public class Compact : DTOGuidNom.Compact
        {
            public DateTime FchCreated { get; set; }
            public DTOLang Lang { get; set; }
            public string value { get; set; } //email address
        }

        public DTOEmp Emp { get; set; }
        public string NickName { get; set; }
        public string Nom { get; set; }
        public string Cognoms { get; set; }
        public Sexs Sex { get; set; } = Sexs.notSet;
        public int BirthYear { get; set; }
        public DateTime Birthday { get; set; }
        public int ChildCount { get; set; }
        public DateTime LastChildBirthday { get; set; }
        public string Adr { get; set; }
        public string ZipCod { get; set; }
        public DTOLocation Location { get; set; }
        public string LocationNom { get; set; }
        public string ProvinciaNom { get; set; }
        public DTOCountry Country { get; set; }
        public string Tel { get; set; }
        public string Password { get; set; }
        public DTOLang Lang { get; set; }
        public bool EFras { get; set; }
        public Sources Source { get; set; }
        public DTORol Rol { get; set; }
        public DTOContact Contact { get; set; }
        public List<DTOContact> Contacts { get; set; }
        public bool Privat { get; set; }
        public bool NoNews { get; set; }
        public List<DTOSubscription> Subscriptions { get; set; }
        public DTOCod BadMail { get; set; }
        public bool Obsoleto { get; set; }
        public DateTime FchCreated { get; set; }
        public DateTime FchActivated { get; set; }
        public DateTime FchDeleted { get; set; }

        public ValidationResults ValidationResult { get; set; }

        public string EmailAddress
        {
            get
            {
                return base.Value;
            }
            set
            {
                base.Value = value;
            }
        }

        public enum Wellknowns
        {
            info,
            portugal,
            matias,
            victoria,
            toni,
            zabalaHoyos,
            rosillo,
            carlosRuiz,
            enric,
            xavi,
            eric,
            traquinaPerfeito
        }

        public enum Sexs
        {
            notSet,
            male,
            female,
            notApplicable
        }

        public enum Sources
        {
            notSet,
            website,
            blog,
            wpFollower,
            wpComment,
            manual,
            webComment,
            raffle,
            facebook,
            fb4moms,
            iMat,
            external,
            spv,
            erp
        }

        public enum ValidationResults
        {
            notSet,
            success,
            emptyEmail,
            wrongEmail,
            emailNotRegistered,
            emptyPassword,
            wrongPassword,
            newValidatedUser,
            systemError,
            userDeleted,
            notAuthorized,
            wrongZip
        }


        public DTOUser(Guid oGuid) : base(oGuid)
        {
            base.ObjCod = DTOBaseTel.ObjCods.User;
            this.Subscriptions = new List<DTOSubscription>();
        }

        public DTOUser() : base() // Constructor sense parametres per serialitzar-lo al pujar les dades via Ajax per exemple de Quiz

        {
            base.ObjCod = DTOBaseTel.ObjCods.User;
            this.Subscriptions = new List<DTOSubscription>();
        }

        public static DTOUser FactoryOrDefault(Guid guid)
        {
            DTOUser retval = null;
            if (!guid.Equals(Guid.Empty))
                retval = new DTOUser(guid);
            return retval;
        }

        public static DTOUser Factory(DTOEmp oEmp, DTOContact oContact = null/* TODO Change to default(_) if this is not a reference type */, string sFullEmailAddress = "")
        {
            DTOLang oLang = DTOLang.ESP();
            DTOUser retval = new DTOUser();
            {
                var withBlock = retval;
                withBlock.Emp = oEmp;
                if (oContact == null)
                    withBlock.Lang = DTOLang.ESP();
                else
                {
                    withBlock.Contacts = new[] { oContact }.ToList();
                    withBlock.Contact = oContact;
                    withBlock.Rol = oContact.Rol;
                    withBlock.Lang = oContact.Lang;
                }

                if (!string.IsNullOrEmpty(sFullEmailAddress))
                {
                    var pattern = @"(\w[\w.-]+@)+\w[\w.-]+";
                    System.Text.RegularExpressions.Regex r = new System.Text.RegularExpressions.Regex(pattern, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                    System.Text.RegularExpressions.Match oMatch = r.Match(sFullEmailAddress);
                    withBlock.EmailAddress = oMatch.Value.ToLower();

                    pattern = "<.*?>";
                    withBlock.Obs = System.Text.RegularExpressions.Regex.Replace(sFullEmailAddress, pattern, string.Empty);
                }

                withBlock.Source = DTOUser.Sources.manual;
            }

            return retval;
        }

        public string HashIdentityPassword()
        {
            byte[] salt;
            byte[] buffer2;
            if (Password == null)
            {
                throw new ArgumentNullException("password");
            }
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(Password, 0x10, 0x3e8))
            {
                salt = bytes.Salt;
                buffer2 = bytes.GetBytes(0x20);
            }
            byte[] dst = new byte[0x31];
            Buffer.BlockCopy(salt, 0, dst, 1, 0x10);
            Buffer.BlockCopy(buffer2, 0, dst, 0x11, 0x20);
            return Convert.ToBase64String(dst);
        }
        public string HashPassword()
        {
            string retval = Helpers.CryptoHelper.Hash(EmailAddress, Password);
            return retval;
        }

        public static string GetEmailAddress(DTOUser oUser)
        {
            string retval = "";
            if (oUser != null)
                retval = oUser.EmailAddress;
            return retval;
        }

        public static bool IsAuthenticated(DTOUser oUser)
        {
            bool retval = false;
            if (oUser != null)
            {
                if (oUser.Rol != null)
                    retval = oUser.Rol.isAuthenticated();
            }
            return retval;
        }

        public DTOUser Trim()
        {
            DTOUser retval = new DTOUser(base.Guid);
            return retval;
        }

        public DTOGuidNom ToGuidNom()
        {
            DTOGuidNom retval = new DTOGuidNom(this.Guid, this.NicknameOrElse());
            return retval;
        }

        public static DTOUser FromGuidNom(DTOGuidNom guidnom)
        {
            DTOUser retval = new DTOUser(guidnom.Guid);
            retval.NickName = guidnom.Nom;
            return retval;
        }

        public string NicknameOrElse()
        {
            string retval = this.NickName;
            if (retval == "")
                retval = string.Format("{0} {1}", this.Nom, this.Cognoms).Trim();
            if (retval == "")
                retval = this.EmailAddress;
            return retval;
        }
        public string NicknameAndEmailAddress()
        {
            string retval = "";
            if (!string.IsNullOrEmpty(NickName))
                retval = string.Format("{0} <{1}>", NickName, EmailAddress);
            else if (!string.IsNullOrEmpty(Nom + Cognoms))
                retval = string.Format("{0} {1} <{2}>", Nom.Trim(), Cognoms.Trim(), EmailAddress);
            else
                retval = this.EmailAddress;
            return retval;
        }

        public static string NicknameOrElse(DTOUser oUser)
        {
            string retval = "";
            if (oUser != null)
                retval = oUser.NicknameOrElse();
            return retval;
        }

        public static string AddressAndNickname(DTOUser oUser)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            if (oUser != null)
            {
                sb.Append(oUser.EmailAddress);
                if (oUser.NickName.isNotEmpty())
                    sb.Append(" " + oUser.NickName);
                if (oUser.Nom.isNotEmpty() && oUser.Cognoms.isNotEmpty())
                    sb.Append(" " + oUser.Nom + " " + oUser.Cognoms);
            }
            string retval = sb.ToString();
            return retval;
        }

        public static string Nom_i_Cognoms(DTOUser oUser)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            if (oUser.Nom.isNotEmpty())
            {
                sb.Append(oUser.Nom);
                if (oUser.Cognoms.isNotEmpty())
                {
                    sb.Append(" ");
                    sb.Append(oUser.Cognoms);
                }
            }
            else if (oUser.Cognoms.isNotEmpty())
                sb.Append(oUser.Cognoms);
            string retval = sb.ToString();
            return retval;
        }

        public string FullLocation(DTOLang lang)
        {
            string retval = "";
            if (this.Location == null)
            {
                if (this.Country != null)
                {
                    retval = this.Country.LangNom.Tradueix(lang);
                }
            }
            else
            {
                retval = this.Location.FullNom(lang);

                if (string.IsNullOrEmpty(retval))
                    retval = this.LocationNom;
            }
            return retval;
        }

        public string FullNom()
        {
            string retval = string.IsNullOrEmpty(this.Nom) ? "" : this.Nom + " ";
            retval += this.Cognoms;
            return retval;
        }

        public string FullBirthAge(DTOLang lang)
        {
            string retval = "";
            if (this.BirthYear > 0)
            {
                retval = string.Format("{0} ({1} {2})", this.BirthYear, DTO.GlobalVariables.Today().Year - this.BirthYear, lang.Tradueix("años", "anys", "years old"));
            }
            return retval;

        }

        public string FullLogText(DTOLang lang)
        {
            string retval = string.Format("{0} {1:dd/MM/yy HH:mm}", (int)this.Source, this.FchCreated);
            return retval;
        }

        public static DTOUser Wellknown(Wellknowns id)
        {
            DTOUser retval = null;
            string sGuid = "";
            switch (id)
            {
                case DTOUser.Wellknowns.info:
                    {
                        sGuid = "A117BF28-CADF-439E-B3B2-575B9AC615B4";
                        break;
                    }

                case DTOUser.Wellknowns.portugal:
                    {
                        sGuid = "CB110644-92A9-404A-99B8-79D7C48A6502";
                        break;
                    }

                case DTOUser.Wellknowns.victoria:
                    {
                        sGuid = "B166BB33-C277-4FC4-B1FF-F7713C101215";
                        break;
                    }

                case DTOUser.Wellknowns.matias:
                    {
                        sGuid = "961297AF-BC62-44ED-932A-2C445B7D69C3";
                        break;
                    }

                case DTOUser.Wellknowns.toni:
                    {
                        sGuid = "5FA9EE85-02D2-415A-AB30-A015A240CD13";
                        break;
                    }

                case DTOUser.Wellknowns.zabalaHoyos:
                    {
                        sGuid = "9512706E-06AF-4859-B4AE-D639DEC471A7";
                        break;
                    }

                case DTOUser.Wellknowns.rosillo:
                    {
                        sGuid = "7AC3B5CD-C0EB-40C3-820B-5D3FE44ABF05";
                        break;
                    }

                case DTOUser.Wellknowns.carlosRuiz:
                    {
                        sGuid = "0BFC6E6C-1E78-48ED-B105-B16A19869840";
                        break;
                    }

                case Wellknowns.enric:
                    {
                        sGuid = "38D5EC9D-B830-478E-9CB6-6C7945F4BA82";
                        break;
                    }

                case DTOUser.Wellknowns.xavi:
                    {
                        sGuid = "FA89CF75-71C9-48EA-BC20-B882C6C6FED7";
                        break;
                    }

                case DTOUser.Wellknowns.eric:
                    {
                        sGuid = "79C24788-A7E4-4520-9804-D95DDFDE915F";
                        break;
                    }

                case Wellknowns.traquinaPerfeito:
                    {
                        sGuid = "8501C51D-EEC8-44C3-B3CE-1468F673354D";
                        break;
                    }
            }

            if (sGuid.isNotEmpty())
            {
                Guid oGuid = new Guid(sGuid);
                retval = new DTOUser(oGuid);
                retval.Emp = new DTOEmp(DTOEmp.Ids.MatiasMasso);
            }
            return retval;
        }

        public string DisplayObs()
        {
            // especific per acompanyar la asdreça email a Xl_Tels
            string retval = this.NickName;

            if (base.Obs.isNotEmpty())
            {
                if (retval.isNotEmpty())
                    retval += " ";
                retval = string.Format("{0} ({1})", retval, base.Obs);
            }
            return retval;
        }

        public static DTOUser Anonymous(DTOEmp oEmp)
        {
            var retval = DTOUser.Factory(oEmp);
            {
                var withBlock = retval;
                withBlock.Rol = new DTORol(DTORol.Ids.unregistered);
                withBlock.Lang = DTOLang.ESP();
            }
            return retval;
        }

        public static bool CheckEmailSintaxis(string sEmail)
        {
            bool retval = true;
            if (sEmail.Contains("@"))
            {
                string[] segments = sEmail.Split(("@").ToCharArray().First());
                if (segments[0].Length < 1)
                    retval = false;
                string[] DomainSegments = segments[1].Split((".").ToCharArray().First());
                if (DomainSegments.Length < 2)
                    retval = false;
                if (DomainSegments.Last().Length < 2)
                    retval = false;
            }
            else
                retval = false;

            return retval;
        }



        public static bool IsEmailNameAddressValid(string src)
        {
            bool retval = false;
            try
            {
                System.Net.Mail.MailAddress email = new System.Net.Mail.MailAddress(src);
                retval = true;
            }
            catch (Exception)
            {
            }
            return retval;
        }

        public static int GetBirthYear(DTOUser oUser)
        {
            int retval = 0;
            if (oUser.BirthYear > 0)
                retval = oUser.BirthYear;
            else if (oUser.Birthday != default(DateTime))
                retval = oUser.Birthday.Year;
            return retval;
        }

        public static void Merge(ref DTOUser target, DTOUser source)
        {
            {
                var withBlock = target;
                withBlock.Guid = source.Guid;
                withBlock.EmailAddress = source.EmailAddress;
                withBlock.NickName = source.NickName;
                withBlock.Nom = source.Nom;
                withBlock.Rol = source.Rol;
                withBlock.Password = source.Password;
                withBlock.BadMail = source.BadMail;
                withBlock.NoNews = source.NoNews;
                withBlock.Contacts.AddRange(source.Contacts.Except(target.Contacts));
            }
        }

        public static string VerificationCode(string sEmail)
        {
            // test@test.com 746b4
            byte[] oBytes = FileSystemHelper.GetStreamFromString(sEmail);
            string sHash = CryptoHelper.HashMD5(oBytes);
            string sCodi = CryptoHelper.StringToHexadecimal(sHash);
            string retval = sCodi.Substring(0, 5);
            return retval;
        }

        public static bool AllowContactBrowse(DTOUser oUser, DTOContact oContact)
        {
            bool retval;
            if (oContact == null)
                retval = true;
            else
                switch (oUser.Rol.id)
                {
                    case DTORol.Ids.superUser:
                    case DTORol.Ids.admin:
                    case DTORol.Ids.accounts:
                    case DTORol.Ids.auditor:
                        {
                            retval = true;
                            break;
                        }

                    default:
                        {
                            retval = !oContact.Rol.isStaff();
                            break;
                        }
                }
            return retval;
        }


        public static bool IsUserAllowedToRead(DTOUser requestUser, DTOUser targetUser)
        {
            bool retval = requestUser.Equals(targetUser);
            if (!retval)
            {
                switch (requestUser.Rol.id)
                {
                    case DTORol.Ids.superUser:
                    case DTORol.Ids.admin:
                        {
                            retval = true;
                            break;
                        }

                    case DTORol.Ids.logisticManager:
                    case DTORol.Ids.accounts:
                        {
                            if (targetUser.Rol != null)
                            {
                                switch (targetUser.Rol.id)
                                {
                                    case DTORol.Ids.cliFull:
                                    case DTORol.Ids.cliLite:
                                    case DTORol.Ids.rep:
                                    case DTORol.Ids.banc:
                                    case DTORol.Ids.manufacturer:
                                    case DTORol.Ids.marketing:
                                    case DTORol.Ids.auditor:
                                        {
                                            retval = true;
                                            break;
                                        }
                                }
                            }

                            break;
                        }

                    case DTORol.Ids.salesManager:
                        {
                            if (targetUser.Rol != null)
                            {
                                switch (targetUser.Rol.id)
                                {
                                    case DTORol.Ids.cliFull:
                                    case DTORol.Ids.cliLite:
                                    case DTORol.Ids.rep:
                                    case DTORol.Ids.comercial:
                                        {
                                            retval = true;
                                            break;
                                        }
                                }
                            }

                            break;
                        }

                    case DTORol.Ids.operadora:
                    case DTORol.Ids.marketing:
                        {
                            if (targetUser.Rol != null)
                            {
                                switch (targetUser.Rol.id)
                                {
                                    case DTORol.Ids.cliFull:
                                    case DTORol.Ids.cliLite:
                                    case DTORol.Ids.rep:
                                        {
                                            retval = true;
                                            break;
                                        }
                                }
                            }

                            break;
                        }
                }
            }

            return retval;
        }

        public static bool IsUserAllowedToRead(DTOUser oUser, DTORol oTargetRol)
        {
            bool retval = false;

            switch (oUser.Rol.id)
            {
                case DTORol.Ids.superUser:
                case DTORol.Ids.admin:
                case DTORol.Ids.auditor:
                    {
                        retval = true;
                        break;
                    }

                case DTORol.Ids.logisticManager:
                case DTORol.Ids.accounts:
                    {
                        if (oTargetRol != null)
                        {
                            switch (oTargetRol.id)
                            {
                                case DTORol.Ids.cliFull:
                                case DTORol.Ids.cliLite:
                                case DTORol.Ids.rep:
                                case DTORol.Ids.banc:
                                case DTORol.Ids.manufacturer:
                                case DTORol.Ids.marketing:
                                case DTORol.Ids.auditor:
                                    {
                                        retval = true;
                                        break;
                                    }
                            }
                        }

                        break;
                    }

                case DTORol.Ids.salesManager:
                    {
                        if (oTargetRol != null)
                        {
                            switch (oTargetRol.id)
                            {
                                case DTORol.Ids.cliFull:
                                case DTORol.Ids.cliLite:
                                case DTORol.Ids.rep:
                                case DTORol.Ids.comercial:
                                    {
                                        retval = true;
                                        break;
                                    }
                            }
                        }

                        break;
                    }

                case DTORol.Ids.operadora:
                case DTORol.Ids.marketing:
                    {
                        if (oTargetRol != null)
                        {
                            switch (oTargetRol.id)
                            {
                                case DTORol.Ids.cliFull:
                                case DTORol.Ids.cliLite:
                                case DTORol.Ids.rep:
                                    {
                                        retval = true;
                                        break;
                                    }
                            }
                        }

                        break;
                    }
            }
            return retval;
        }

        public static bool IsStaff(DTOUser oUser)
        {
            bool retval = false;
            switch (oUser.Rol.id)
            {
                case DTORol.Ids.superUser:
                case DTORol.Ids.admin:
                case DTORol.Ids.salesManager:
                case DTORol.Ids.accounts:
                case DTORol.Ids.comercial:
                case DTORol.Ids.logisticManager:
                case DTORol.Ids.marketing:
                case DTORol.Ids.operadora:
                case DTORol.Ids.taller:
                    {
                        retval = true;
                        break;
                    }
            }
            return retval;
        }

        public string Gravatar(int width = 80)
        {
            var encoder = new System.Text.UTF8Encoding();
            var md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            var hashedBytes = md5.ComputeHash(encoder.GetBytes(this.EmailAddress.ToLower()));
            var sb = new System.Text.StringBuilder(hashedBytes.Length * 2);

            for (var i = 0; i <= hashedBytes.Length - 1; i++)
                sb.Append(hashedBytes[i].ToString("X2"));


            // Dim MD5Hash = CryptoHelper.HashMD5(emailaddress.ToLower.Trim)
            string retval = string.Format("https://www.gravatar.com/avatar/{0}", sb.ToString()); // MD5Hash)
                                                                                                 // https://s.gravatar.com/avatar/2326d54196390a4b59757ee508d59530?s=80
            UrlHelper.addParam(ref retval, "s", width.ToString());
            UrlHelper.addParam(ref retval, "d", "mp"); // sets default avatar to "mistery man"
            return retval;
        }

        public string NicknameForComments()
        {
            string retval = this.NickName;
            if (retval == "")
                retval = this.Nom;
            if (retval == "")
                retval = this.EmailAddress.Substring(0, this.EmailAddress.IndexOf('@'));
            return retval;
        }

        public class ValidatePasswordModel
        {
            public DTOUser User { get; set; }
            public DTOUser.ValidationResults Result { get; set; }
        }
    }
}
