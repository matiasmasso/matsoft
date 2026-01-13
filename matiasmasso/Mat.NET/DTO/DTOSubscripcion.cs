using System;
using System.Collections.Generic;

namespace DTO
{
    public class DTOSubscription : DTOBaseGuid
    {
        public DTOEmp Emp { get; set; }
        public Ids Id { get; set; }
        public DTOLangText Nom { get; set; }
        public DTOLangText Dsc { get; set; }
        public bool Reverse { get; set; } = false; // Reverse: señala els que han retirat la subscripcio
        public List<DTORol> Rols { get; set; }
        public DateTime FchSubscribed { get; set; }
        public new bool IsLoaded { get; set; }

        public enum Ids
        {
            NotSet,
            Stocks,
            Facturacio,
            ConfirmacioEnviament,
            ConfirmacioComanda_Deprecated,
            AvisArribadaCamion,
            VencimentsPropers,
            Noticias,
            NovaIncidencia,
            IT,
            Blog,
            Comptabilitat,
            CopiaAvisVenciment,
            CopiaFactura,
            StoreLocatorExcelMailing,
            TransportOrdenDeCarga,
            Transmisio,
            Marketing,
            RocheAltaGln = 90,
            EmailReport,
            AltaClient,
            CopiaSorteigConfirmacioParticipacio,
            CopiaSorteigWinnerCongrats,
            AvisRecollidesServeiTecnic,
            CreditsCaducatsList
        }
        public enum Wellknowns
        {
            NotSet,
            Comptabilitat,
            Marketing,
            ConfirmacioEnviament,
            Stocks,
            CopiaAvisVenciment,
            CopiaSorteigConfirmacioParticipacio,
            Transmisio,
            NovaIncidencia,
            Facturacio,
            CopiaFactura,
            Noticias,
            AvisDescatalogats,
            StoreLocatorExcelMailing,
            TransportOrdenDeCarga,
            AvisRecollidesServeiTecnic,
            AltaClient,
            AvisArribadaCamion,
            PedidosIncidencias,
            BankTransferReminder,
            CreditsCaducatsList,
            TamariuCheckPort
        }

        public DTOSubscription(Guid oGuid) : base(oGuid)
        {
            Rols = new List<DTORol>();
            Nom = new DTOLangText(base.Guid, DTOLangText.Srcs.SubscriptionNom);
            Dsc = new DTOLangText(base.Guid, DTOLangText.Srcs.SubscriptionDsc);
        }

        public static DTOSubscription Factory(DTOEmp emp)
        {
            DTOSubscription retval = new DTOSubscription(Guid.NewGuid());
            retval.Nom.Esp = "(nova subscripció)";
            retval.Emp = emp;
            return retval;
        }
        public static DTOSubscription Wellknown(DTOSubscription.Wellknowns id)
        {
            DTOSubscription retval = null;
            string sGuid = "";
            switch (id)
            {
                case Wellknowns.Comptabilitat:
                    {
                        sGuid = "FDE52732-7C28-489F-B4A5-14CED11E8524";
                        break;
                    }
                case Wellknowns.Marketing:
                    {
                        sGuid = "D66945C8-D75E-46D9-A529-038C9C2071CF";
                        break;
                    }
                case Wellknowns.ConfirmacioEnviament:
                    {
                        sGuid = "D3C2B29F-3BE5-4177-B669-5FDAF22B3829";
                        break;
                    }
                case Wellknowns.Stocks:
                    {
                        sGuid = "6F16549B-F4FA-45A6-BFC9-035A215AE6C6";
                        break;
                    }
                case Wellknowns.CopiaAvisVenciment:
                    {
                        sGuid = "D8B64EDF-F438-4D93-A96E-FC4431026E9E";
                        break;
                    }
                case Wellknowns.BankTransferReminder:
                    {
                        sGuid = "EBFB5EC4-1071-420F-8FDA-FB2C732A62F7";
                        break;
                    }
                case Wellknowns.CopiaSorteigConfirmacioParticipacio:
                    {
                        sGuid = "065EE556-B855-4D34-9D6C-FDB8DBC20236";
                        break;
                    }
                case Wellknowns.Transmisio:
                    {
                        sGuid = "822B67EF-C0E1-4BB3-8DBF-BB81673DD1C5";
                        break;
                    }
                case Wellknowns.NovaIncidencia:
                    {
                        sGuid = "BB46ACE6-1280-4495-B3D3-383AFA29878D";
                        break;
                    }
                case Wellknowns.Facturacio:
                    {
                        sGuid = "78B47B9A-BDEE-48DC-BE10-0AE7E7DAFE24";
                        break;
                    }
                case Wellknowns.CopiaFactura:
                    {
                        sGuid = "B561E34C-74F5-4C60-B638-B65F926829AE";
                        break;
                    }
                case Wellknowns.AvisDescatalogats:
                    {
                        sGuid = "EAF9AFE4-5861-435C-99D0-EE37F8E0F7E5";
                        break;
                    }
                case Wellknowns.StoreLocatorExcelMailing:
                    {
                        sGuid = "37FA536A-3B0E-43E2-B0A5-34A641914FB5";
                        break;
                    }
                case Wellknowns.TransportOrdenDeCarga:
                    {
                        sGuid = "F843BDCC-CE42-4F0B-9E0D-68677A99B6D2";
                        break;
                    }
                case Wellknowns.AvisRecollidesServeiTecnic:
                    {
                        sGuid = "E24253D1-19AB-4E5F-B3A3-DB569BF7E9DC";
                        break;
                    }
                case Wellknowns.AltaClient:
                    {
                        sGuid = "D2B62399-497F-456D-9C01-5AC1C8FC521F";
                        break;
                    }
                case Wellknowns.AvisArribadaCamion:
                    {
                        sGuid = "ED671B7F-814B-4146-AE6E-331433A6FECB";
                        break;
                    }
                case Wellknowns.PedidosIncidencias:
                    {
                        sGuid = "11AAD15E-1301-4B83-81E7-24A5C4C3531C";
                        break;
                    }
                case Wellknowns.Noticias:
                    {
                        sGuid = "9C2F00E2-84D8-446E-BE7C-846AF3BC79B5";
                        break;
                    }
                case Wellknowns.CreditsCaducatsList:
                    {
                        sGuid = "b9dc0505-60ec-4dcc-8f63-0d69809420ae";
                        break;
                    }
                case Wellknowns.TamariuCheckPort:
                    {
                        sGuid = "30B60FA0-928B-41D1-846E-281E99191A31";
                        break;
                    }

            }

            if (sGuid.isNotEmpty())
            {
                Guid oGuid = new Guid(sGuid);
                retval = new DTOSubscription(oGuid);
            }
            return retval;
        }

        public static string GetNom(DTOSubscription oSubscription, DTOLang oLang = null/* TODO Change to default(_) if this is not a reference type */)
        {
            if (oLang == null)
                oLang = DTOApp.Current.Lang;
            // Dim retval As String = oLang.Tradueix(oSubscription.Nom_ESP, oSubscription.Nom_CAT, oSubscription.Nom_ENG)
            string retval = oSubscription.Nom.Tradueix(oLang); // oLang.Tradueix(oSubscription.Nom_ESP, oSubscription.Nom_CAT, oSubscription.Nom_ENG)
            return retval;
        }

        public static string NomOrId(DTOSubscription oSubscription, DTOLang oLang = null/* TODO Change to default(_) if this is not a reference type */)
        {
            string sNom = oSubscription.Nom.Tradueix(oLang);
            string retval = sNom.isNotEmpty() ? sNom : oSubscription.Id.ToString();
            return retval;
        }

        public static string GetDsc(DTOSubscription oSubscription, DTOLang oLang = null/* TODO Change to default(_) if this is not a reference type */)
        {
            if (oLang == null)
                oLang = DTOApp.Current.Lang;
            string retval = oSubscription.Dsc.Tradueix(oLang);
            return retval;
        }
    }
}
