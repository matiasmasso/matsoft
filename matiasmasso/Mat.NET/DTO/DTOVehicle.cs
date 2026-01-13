using Newtonsoft.Json;

using System;
using System.Collections.Generic;

namespace DTO
{
    public class DTOVehicle : DTOGuidNom
    {
        public DTOEmp Emp { get; set; }
        public ModelClass Model { get; set; }
        public string Matricula { get; set; }
        public string Bastidor { get; set; }
        public DTOContact Conductor { get; set; }
        public DTOContact Venedor { get; set; }
        public DateTime Alta { get; set; }
        public DateTime Baixa { get; set; }
        public DTOContract Contract { get; set; }
        public DTOContract Insurance { get; set; }
        public bool Privat { get; set; }
        [JsonIgnore]
        public Byte[] Image { get; set; }

        public DTOVehicle() : base()
        {
        }

        public DTOVehicle(Guid oGuid) : base(oGuid)
        {
        }

        public static DTOVehicle Factory(DTOEmp oEmp)
        {
            DTOVehicle retval = new DTOVehicle();
            {
                var withBlock = retval;
                withBlock.Emp = oEmp;
            }
            return retval;
        }

        public string MarcaYModel()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            if (Model != null)
                sb.Append(Model.FullNom() + " ");
            string retval = sb.ToString();
            return retval;
        }

        public string MarcaModelYMatricula()
        {
            string retval = string.Format("{0} {1}", this.MarcaYModel(), Matricula);
            return retval;
        }

        public string MatriculaMarcaYModel()
        {
            string retval = string.Format("{0} {1}", Matricula, this.MarcaYModel());
            return retval;
        }

        public bool IsDonatDeBaixa()
        {
            return Baixa != DateTime.MinValue;
        }


        public class Marca : DTOBaseGuid
        {
            public string Nom { get; set; }
            public List<ModelClass> Models { get; set; }

            public Marca() : base()
            {
                Models = new List<ModelClass>();
            }

            public Marca(Guid oGuid) : base(oGuid)
            {
                Models = new List<ModelClass>();
            }
        }

        public class ModelClass : DTOBaseGuid
        {
            public Marca Marca { get; set; }
            public string Nom { get; set; }

            public ModelClass() : base()
            {
            }

            public ModelClass(Guid oGuid) : base(oGuid)
            {
            }

            public static ModelClass Factory(Marca oMarca)
            {
                ModelClass retval = new ModelClass();
                retval.Marca = oMarca;
                return retval;
            }

            public string FullNom()
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                if (Marca != null)
                    sb.Append(Marca.Nom + " ");
                sb.Append(Nom);
                string retval = sb.ToString();
                return retval;
            }
        }
    }
}
