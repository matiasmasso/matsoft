using System;
using System.Collections.Generic;

namespace Api.Entities
{
    /// <summary>
    /// Structure of Iban digits for each EU country
    /// </summary>
    public partial class IbanStructure
    {
        /// <summary>
        /// ISO 3166-1 code for the country
        /// </summary>
        public string CountryIso { get; set; } = null!;
        /// <summary>
        /// Length of the digits representing the bank entity
        /// </summary>
        public short BankLength { get; set; }
        /// <summary>
        /// Enumerable DTOIbanStructure.Formats 0.Numeric, 1.Alfanumeric
        /// </summary>
        public byte BankFormat { get; set; }
        /// <summary>
        /// Index of the first character of the bank code, starting with zero
        /// </summary>
        public short BankPosition { get; set; }
        /// <summary>
        /// Length of the digits representing the bank branch
        /// </summary>
        public short BranchLength { get; set; }
        /// <summary>
        /// Enumerable DTOIbanStructure.Formats 0.Numeric, 1.Alfanumeric
        /// </summary>
        public byte BranchFormat { get; set; }
        /// <summary>
        /// Index of the first character of the bank branch code, starting with zero
        /// </summary>
        public short BranchPosition { get; set; }
        /// <summary>
        /// number of check control digits
        /// </summary>
        public short CheckDigitsLength { get; set; }
        /// <summary>
        /// Enumerable DTOIbanStructure.Formats 0.Numeric, 1.Alfanumeric
        /// </summary>
        public byte CheckDigitsFormat { get; set; }
        /// <summary>
        /// Index of the first check control digit, starting with zero
        /// </summary>
        public short CheckDigitsPosition { get; set; }
        /// <summary>
        /// Number of dogits of the account number
        /// </summary>
        public short AccountLength { get; set; }
        /// <summary>
        /// Enumerable DTOIbanStructure.Formats 0.Numeric, 1.Alfanumeric
        /// </summary>
        public byte AccountFormat { get; set; }
        /// <summary>
        /// Index of the first character of the account number, starting with zero
        /// </summary>
        public short AccountPosition { get; set; }
        /// <summary>
        /// Total number of digits 
        /// </summary>
        public short OverallLength { get; set; }
    }
}
