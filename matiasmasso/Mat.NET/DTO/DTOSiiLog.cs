using System;
using System.Collections.Generic;
using System.Linq;

namespace DTO
{
    public class DTOSiiLog : DTOBaseGuid
    {
        public Defaults.Entornos Entorno { get; set; }
        public DateTime Fch { get; set; }
        public Continguts Contingut { get; set; }
        public Results Result { get; set; }
        public string TipoDeComunicacion { get; set; } // A0 (alta), A1 (modificacion), A4
        public string Csv { get; set; } // Nullable, 16 chars

        public string ErrMsg { get; set; }

        public string Nif { get; set; }
        public string FraNum { get; set; }

        public enum Continguts
        {
            NotSet,
            Facturas_Emitidas,
            Facturas_Recibidas
        }
        public enum Results
        {
            NotSet,
            Correcto,
            Parcialmente_Correcto,
            Incorrecto,
            Error_De_Comunicacion
        }

        public DTOSiiLog() : base()
        {
        }

        public DTOSiiLog(Guid oGuid) : base(oGuid)
        {
        }

        public static string TipoDeComunicacionText(DTOSiiLog value)
        {
            KeyValuePair<string, string> pair = TiposDeComunicacion().FirstOrDefault(x => x.Key == value.TipoDeComunicacion);
            string retval = pair.Value;
            return retval;
        }

        public static List<KeyValuePair<string, string>> TiposDeComunicacion()
        {
            List<KeyValuePair<string, string>> retval = new List<KeyValuePair<string, string>>();
            retval.Add(new KeyValuePair<string, string>("", "(sel·leccionar tipus de comunicació)"));
            retval.Add(new KeyValuePair<string, string>("A0", "A0 Alta de facturas/registro"));
            retval.Add(new KeyValuePair<string, string>("A1", "Modificación de facturas/registros (errores registrales)"));
            retval.Add(new KeyValuePair<string, string>("A4", "A4 Modificación Factura Régimen de Viajeros"));
            return retval;
        }

        public static string ResultText(DTOSiiLog value)
        {
            string retval = "";
            switch (value.Result)
            {
                case DTOSiiLog.Results.Correcto:
                    {
                        retval = "Correcte";
                        break;
                    }

                case DTOSiiLog.Results.Parcialmente_Correcto:
                    {
                        retval = "Parcialment correcte";
                        break;
                    }

                case DTOSiiLog.Results.Incorrecto:
                    {
                        retval = "Incorrecte";
                        break;
                    }

                case DTOSiiLog.Results.Error_De_Comunicacion:
                    {
                        retval = "Error de comunicació";
                        break;
                    }
            }
            return retval;
        }
    }
}
