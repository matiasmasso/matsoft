namespace DTO
{
    using MatHelperStd;
    using System;
    using System.Collections.Generic;

    public class DTOSatRecall : DTOBaseGuid
    {
        public DTOIncidencia Incidencia { get; set; }
        public string Defect { get; set; }
        public PickupFroms PickupFrom { get; set; }
        public DateTime FchCustomer { get; set; }
        public DateTime FchManufacturer { get; set; }
        public string PickupRef { get; set; }
        public Modes Mode { get; set; }
        public string ReturnRef { get; set; }
        public DateTime ReturnFch { get; set; }
        public bool Carrec { get; set; }
        public string CreditNum { get; set; }
        public DateTime CreditFch { get; set; }
        public DTOAddress Address { get; set; }
        public string ContactPerson { get; set; }
        public string Tel { get; set; }

        public string Obs { get; set; }

        public enum PickupFroms
        {
            NotSet,
            Store,
            Workshop,
            Warehouse
        }

        public enum MailTargets
        {
            Customer,
            Provider
        }

        public enum Modes
        {
            PerAbonar,
            PerReparar
        }


        public DTOSatRecall() : base()
        {
        }

        public DTOSatRecall(Guid oGuid) : base(oGuid)
        {
        }

        public static DTOSatRecall Factory(DTOIncidencia oIncidencia)
        {
            DTOSatRecall retval = new DTOSatRecall();
            {
                var withBlock = retval;
                withBlock.Incidencia = oIncidencia;
                withBlock.Address = oIncidencia.Customer.Address;
                if (oIncidencia.Codi != null)
                    retval.Defect = oIncidencia.Codi.Esp;
                withBlock.ContactPerson = oIncidencia.ContactPerson;
                withBlock.Tel = oIncidencia.Tel;
            }
            return retval;
        }

        public static string MessageSubject(DTOSatRecall oSatRecall, MailTargets mailtarget)
        {
            string retval = "";
            switch (mailtarget)
            {
                case MailTargets.Customer:
                    {
                        retval = string.Format("Recogida de incidencia {0} (requiere respuesta)", oSatRecall.Incidencia.AsinOrNum());
                        break;
                    }

                case MailTargets.Provider:
                    {
                        retval = string.Format("Service recall {0}", oSatRecall.Incidencia.AsinOrNum());
                        break;
                    }
            }
            return retval;
        }

        public static string TemplatePath(DTOSatRecall oSatRecall, List<Exception> exs)
        {
            string retval = "";
            if (oSatRecall == null)
                exs.Add(new Exception("Recollida sense dades"));
            else if (oSatRecall.Incidencia == null)
                exs.Add(new Exception("Recollida sense dades de incidencia"));
            else if (oSatRecall.Incidencia.Product == null)
                exs.Add(new Exception("Recollida sense dades de producte"));
            else if (DTOProduct.proveidor((DTOProduct)oSatRecall.Incidencia.Product) == null)
                exs.Add(new Exception("No es pot deduir el fabricant del producte " + ((DTOProduct)oSatRecall.Incidencia.Product).FullNom()));
            else
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(FileSystemHelper.PathToMyDocuments());
                sb.Append(@"\Mis Plantillas");
                sb.Append(@"\Mat.NET");
                sb.Append(@"\SatRecall");
                sb.Append(@"\" + DTOProduct.proveidor((DTOProduct)oSatRecall.Incidencia.Product).Guid.ToString() + ".docx");
                retval = sb.ToString();
            }
            return retval;
        }

        public static string LabelPath(DTOSatRecall oSatRecall, List<Exception> exs)
        {
            string retval = "";
            DTOProduct oProduct = (DTOProduct)oSatRecall.Incidencia.Product;
            if (oSatRecall == null)
                exs.Add(new Exception("Recollida sense dades"));
            else if (oSatRecall.Incidencia == null)
                exs.Add(new Exception("Recollida sense dades de incidencia"));
            else if (oProduct == null)
                exs.Add(new Exception("Recollida sense dades de producte"));
            else if (DTOProduct.proveidor(oProduct) == null)
                exs.Add(new Exception("No es pot deduir el fabricant del producte " + ((DTOProduct)oSatRecall.Incidencia.Product).FullNom()));
            else
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(FileSystemHelper.PathToMyDocuments());
                sb.Append(@"\Mis Plantillas");
                sb.Append(@"\Mat.NET");
                sb.Append(@"\SatRecall");
                sb.Append(@"\" + DTOProduct.proveidor(oProduct).Guid.ToString() + ".label.docx");
                retval = sb.ToString();
            }
            return retval;
        }
    }
}
