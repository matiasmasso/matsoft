using System;
using System.Collections.Generic;

namespace DTO
{
    public class DTOSepaDirectDebit
    {
        public string MessageId { get; set; }
        public DateTime FchCreated { get; set; }
        public string PresentadorCountry { get; set; } = "ES";
        public string PresentadorNIF { get; set; } = "A58007857";
        public string PresentadorSufijo { get; set; } = "005";
        public string CreditorNom { get; set; } = "MATIAS MASSO, S.A.";
        public string CreditorIban { get; set; } = "ES9821000909110200116204";
        public string CreditorBic { get; set; } = "CAIXESBBXXX";

        public List<DTOSepaDirectDebitItem> Items { get; set; }

        public enum TiposAdeudo
        {
            NotSet,
            FRST,
            RCUR,
            FNAL,
            OOFF
        }

        public DTOSepaDirectDebit() : base()
        {
            Items = new List<DTOSepaDirectDebitItem>();
        }

        public void AddItem(string Id, string Nom, string Iban, string BIC, string MandateId, DateTime MandateFch, DateTime DueDate, decimal Value, DTOSepaDirectDebitItem.SequenceTypes SequenceType
    )
        {
            DTOSepaDirectDebitItem item = new DTOSepaDirectDebitItem();
            {
                var withBlock = item;
                withBlock.Id = Id;
                withBlock.Nom = Nom;
                withBlock.Iban = Iban;
                withBlock.BIC = BIC;
                withBlock.MandateId = MandateId;
                withBlock.MandateFch = MandateFch;
                withBlock.DueDate = DueDate;
                withBlock.Value = Value;
                withBlock.SequenceType = SequenceType;
            }
            Items.Add(item);
        }
    }

    public class DTOSepaDirectDebitItem
    {
        public string Id { get; set; }
        public decimal Value { get; set; }
        public SequenceTypes SequenceType { get; set; }
        public DateTime DueDate { get; set; }
        public string MandateId { get; set; }
        public DateTime MandateFch { get; set; }
        public string BIC { get; set; }
        public string Nom { get; set; }
        public string Iban { get; set; }

        public enum SequenceTypes
        {
            FNAL,
            FRST,
            OOFF,
            RCUR
        }
    }
}
