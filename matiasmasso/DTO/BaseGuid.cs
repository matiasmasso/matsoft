using DocumentFormat.OpenXml.Math;
using DocumentFormat.OpenXml.Vml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace DTO
{
    public class BaseGuidNom:BaseGuid
    {
        public string? Nom { get; set; }
        public BaseGuidNom() { }
        public BaseGuidNom(Guid guid, string? nom = null):base(guid)
        {
            Nom = nom;
        }
    }
    public class BaseGuid
    {
        private bool _isNew = true;
        public bool IsNew {
            get {
                return _isNew;
            }
            set
            {
                _isNew = value;
            }
        }
        private Guid _guid;
        public Guid Guid
        {
            get { 
                return _guid; 
            }
            set {
                _guid = value; 
                //IsNew = false; 
            }
        }


        public BaseGuid() : base()
        {
            _guid = System.Guid.NewGuid();
        }

        public BaseGuid(Guid oGuid) : base()
        {
            Guid = oGuid;
            _isNew = false;
        }

        public override bool Equals(object? candidate)
        {
            var retval = false;
            if (candidate != null && candidate is BaseGuid)
                retval = Guid.Equals(((BaseGuid)candidate).Guid);
            return retval;
        }

        public virtual bool Matches(string? searchTerm)
        {
            //this method should be overriden.
            //implemented here to appear as generic type member
            bool retval = true;
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var searchTerms = searchTerm.Split("+", StringSplitOptions.RemoveEmptyEntries);
                var searchTarget = this.ToString();
                retval = searchTerms.All(x => searchTarget!.Contains(x, StringComparison.OrdinalIgnoreCase));
            }
            return retval;

        }

        public static decimal? ParseAmount(string numberString)
        {
            decimal? retval = null;
            // Replace with your input

            // Regex to extract the number part from the string (supports thousands and decimal separators)
            // Simple replace of all non numeric and non ',' '.' characters with nothing might suffice as well
            // Depends on the input you receive
            var regex = new System.Text.RegularExpressions.Regex("^[^\\d-]*(-?(?:\\d|(?<=\\d)\\.(?=\\d{3}))+(?:,\\d+)?|-?(?:\\d|(?<=\\d),(?=\\d{3}))+(?:\\.\\d+)?)[^\\d]*$");

            char decimalChar;
            char thousandsChar;

            // Get the numeric part from the string

            if (!string.IsNullOrEmpty(regex.Match(numberString).Value))
            {

                var numberPart = regex.Match(numberString).Groups[1].Value;
                // Try to guess which character is used for decimals and which is used for thousands
                if (numberPart.LastIndexOf(',') > numberPart.LastIndexOf('.'))
                {
                    decimalChar = ',';
                    thousandsChar = '.';
                }
                else
                {
                    decimalChar = '.';
                    thousandsChar = ',';
                }

                // Remove thousands separators as they are not needed for parsing
                numberPart = numberPart.Replace(thousandsChar.ToString(), string.Empty);

                // Replace decimal separator with the one from InvariantCulture
                // This makes sure the decimal parses successfully using InvariantCulture
                numberPart = numberPart.Replace(decimalChar.ToString(),
                    CultureInfo.InvariantCulture.NumberFormat.CurrencyDecimalSeparator);

                // Voilá
                retval = decimal.Parse(numberPart, NumberStyles.AllowDecimalPoint | NumberStyles.Number, CultureInfo.InvariantCulture);
            }

            return retval;
        }

        public GuidNom ToGuidnom() => new GuidNom(Guid, ToString());


        public override int GetHashCode()
        {
            return this.Guid.GetHashCode(); 
        }
    }
}
