using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace DTO
{
    public class DTOSpv : DTOBaseGuid
    {
        public new Guid Guid
        {
            // obligatori per serializer
            get
            {
                return base.Guid;
            }
            set
            {
                base.Guid = value;
            }
        }

        public DTOEmp emp { get; set; }
        public int id { get; set; }
        public DateTime fchAvis { get; set; }
        public DateTime fchRead { get; set; }
        public DTOCustomer customer { get; set; }
        public bool solicitaGarantia { get; set; }
        public bool garantia { get; set; }
        public string contacto { get; set; }
        public string sRef { get; set; }
        public DTOSpvIn spvIn { get; set; }
        public object product { get; set; }
        public string serialNumber { get; set; }

        public string ManufactureDate { get; set; }
        public string obsClient { get; set; }
        public string obsTecnic { get; set; }
        public string labelEmailedTo { get; set; }
        public string nom { get; set; }
        public DTOAddress address { get; set; }
        public string tel { get; set; }

        public DTOAmt valJob { get; set; }
        public DTOAmt valMaterial { get; set; }
        public DTOAmt valEmbalatje { get; set; }
        public DTOAmt valPorts { get; set; }
        public DTODelivery delivery { get; set; }
        public DTOIncidencia incidencia { get; set; }

        public DTOUser usrRegister { get; set; }
        public DTOUser usrTecnic { get; set; }

        public DTOUser usrOutOfSpvIn { get; set; }
        public DateTime fchOutOfSpvIn { get; set; }
        public string obsOutOfSpvIn { get; set; }
        public DTOUser usrOutOfSpvOut { get; set; }
        public DateTime fchOutOfSpvOut { get; set; }
        public string obsOutOfSpvOut { get; set; }

        public DTOSpv() : base()
        {
        }

        public DTOSpv(Guid oGuid) : base(oGuid)
        {
        }

        public static DTOSpv Factory(DTOUser oUsrRegister)
        {
            DTOSpv retval = new DTOSpv();
            {
                var withBlock = retval;
                withBlock.emp = oUsrRegister.Emp;
                withBlock.usrRegister = oUsrRegister;
                withBlock.valJob = DTOAmt.Empty();
                withBlock.fchAvis = DTO.GlobalVariables.Today();
            }
            return retval;
        }


        public void restoreObjects()
        {
            this.product = DTOProduct.fromJObject(this.product as JObject);

            if (this.incidencia != null)
                this.incidencia.restoreObjects();
        }


        public List<string> lines(DTOLang oLang)
        {
            List<string> retval = new List<string>();

            string Str = oLang.Tradueix("Reparación num.", "Reparació num.", "Service Job num.");
            Str = Str + id + " ";
            if (garantia)
                Str = Str + oLang.Tradueix("en garantía", "en garantía", "under warranty");
            else
                Str = Str + oLang.Tradueix("con cargo", "amb carrec", "");
            retval.Add(Str);

            Str = oLang.Tradueix("Solicitada", "Sol·licitada", "Requested");
            if (contacto.isNotEmpty())
                Str = Str + oLang.Tradueix(" por ", " per ", " by ") + contacto;
            Str = Str + oLang.Tradueix(" en fecha ", " en data ", " on ") + fchAvis.ToShortDateString();
            retval.Add(Str);

            Str = oLang.Tradueix("Recibida el ", "Rebuda el ", "Received on ") + spvIn.fch.ToShortDateString();
            if (sRef.isNotEmpty())
                Str = Str + " (s/ref.: " + sRef + ")";
            retval.Add(Str);

            if (incidencia != null)
            {
                Str = oLang.Tradueix("Incidencia ", "Incidència ", "Incidence ") + incidencia.AsinOrNum() + " del " + incidencia.Fch.ToShortDateString();
                retval.Add(Str);
            }

            DTOProduct oProduct = null;
            if (product is JObject)
                oProduct = DTOProduct.fromJObject((JObject)product);
            else
                oProduct = (DTOProduct)product;

            Str = oLang.Tradueix("Producto: ", "Producte: ", "Product: ") + oProduct.FullNom(oLang);
            retval.Add(Str);

            if (serialNumber.isNotEmpty())
            {
                Str = oLang.Tradueix("Número de serie: ", "Número de serie: ", "Serial number: ", "Número de serie: ") + serialNumber;
                retval.Add(Str);
            }

            if (ManufactureDate.isNotEmpty())
            {
                Str = oLang.Tradueix("Fecha de fabricación: ", "Data de fabricació: ", "Manufacturing date: ") + ManufactureDate;
                retval.Add(Str);
            }

            retval.Add("");

            List<string> sObsTecnic = obsTecnic.toLinesList();
            foreach (string str in sObsTecnic)
                retval.Add(str.Trim());

            retval.Add("");

            return retval;
        }

        public static string TextRegistre(DTOSpv oSpv, DTOLang oLang)
        {
            string ESP = "registrado";
            string CAT = "registrat";
            string ENG = "recorder";

            if (oSpv.usrRegister != null)
            {
                string sLogin = DTOUser.NicknameOrElse(oSpv.usrRegister);
                ESP = ESP + " por " + sLogin;
                CAT = CAT + " per " + sLogin;
                ENG = ENG + " by " + sLogin;
            }

            string sFch = oSpv.fchAvis.ToShortDateString();
            ESP = ESP + " el " + sFch;
            CAT = CAT + " el " + sFch;
            ENG = ENG + " on " + sFch;

            string retVal = oLang.Tradueix(ESP, CAT, ENG);
            return retVal;
        }

        public static string textOutSpvIn(DTOSpv oSpv)
        {
            string s = "retirat de pendents de entrar per " + DTOUser.NicknameOrElse(oSpv.usrOutOfSpvIn);
            s = s + " el " + oSpv.fchOutOfSpvIn.ToShortDateString();
            if (oSpv.obsOutOfSpvIn.isNotEmpty())
                s = s + ": " + oSpv.obsOutOfSpvIn;
            return s;
        }

        public static string textOutSpvOut(DTOSpv oSpv)
        {
            string s = "retirat de pendents de sortir per " + DTOUser.NicknameOrElse(oSpv.usrOutOfSpvOut);
            s = s + " el " + oSpv.fchOutOfSpvOut.ToShortDateString();
            if (oSpv.obsOutOfSpvOut.isNotEmpty())
                s = s + ": " + oSpv.obsOutOfSpvOut;
            return s;
        }
    }
}
