using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class PndModel:BaseGuid
    {
        public EmpModel.EmpIds Emp { get; set; }
        public Guid Contact { get; set; }
        public Guid? Cca { get; set; }
        public Guid? Cta { get; set; }
        public DateOnly? Fch { get; set; }
        public DateOnly? Vto { get; set; }
        public decimal Eur {  get; set; }

        public string? Fra { get; set; }
        public string? Fpg { get; set; }
        public Cfps Cfp { get; set; }
        public ADs AD { get; set; }

        public Statuses Status { get; set; }
        public Guid? Result { get; set; }
        public DateTime FchCreated { get; set; }




        public enum ADs
        {
            Deutor,
            Creditor
        }

        public enum Cfps
        {
            notSet = 0,
            rebut = 1,
            reposicioFons = 2,
            comptat = 3,
            xerocopia = 4,
            domiciliacioBancaria = 5,
            transferencia = 6,
            aNegociar = 9,
            efteAndorra = 10,
            transfPrevia = 11,
            diposit = 12
        }

        public enum Statuses
        {
            notSet = -1,
            pendent = 0,
            enCartera = 1,
            enCirculacio = 2,
            saldat = 10,
            compensat = 11
        }

        public PndModel():base() { }
        public PndModel(Guid guid) : base(guid) { }
    }
}
