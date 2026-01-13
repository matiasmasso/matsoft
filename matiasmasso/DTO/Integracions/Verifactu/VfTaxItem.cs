using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Integracions.Verifactu
{
    public class VfTaxItem
    {
        private decimal taxBase;
        private decimal taxRate;
        private decimal taxAmount;

        public decimal TaxBase
        {
            get => taxBase;
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(TaxBase), "Tax base no puede ser negativa.");
                taxBase = value;
            }
        }

        public decimal TaxRate
        {
            get => taxRate;
            set
            {
                if (value < 0 || value > 100)
                    throw new ArgumentOutOfRangeException(nameof(TaxRate), "Tax rate debe estar entre 0 y 100.");
                taxRate = value;
            }
        }

        public decimal TaxAmount
        {
            get => taxAmount;
            set
            {
                var expected = Math.Round(TaxBase * TaxRate / 100, 2, MidpointRounding.AwayFromZero);
                if (Math.Abs(value - expected) > 0.02m)
                    throw new ArgumentException("El importe de impuesto no coincide con base y tipo.");
                taxAmount = value;
            }
        }

        public string? TaxScheme { get; set; } // "01" Regimen general
        public string? TaxType { get; set; } // "S1" Sujeta No Exenta
        public string? TaxException { get; set; } // E1: Exenta por el artículo 20.El alquiler de vivienda habitual a particulares está exento de IVA según el artículo 20.1.23 de la Ley del IVA y el Real Decreto 1619/2012,
    }
}
