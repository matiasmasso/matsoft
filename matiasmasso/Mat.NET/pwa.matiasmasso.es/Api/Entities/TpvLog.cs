using System;
using System.Collections.Generic;

namespace Api.Entities
{
    /// <summary>
    /// Log message sent by bank online payments gateway when a customer tries to issue a payment. Info available at https://canales.redsys.es/canales/ayuda/documentacion/Manual%20integracion%20para%20conexion%20por%20Web%20Service.pdf
    /// </summary>
    public partial class TpvLog
    {
        /// <summary>
        /// Primary key
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// Unique number identifying the operation on gateway database. 12 alphanumeric chars, the first 4 characters must be numeric. Since year 2016 all our Ds_Order numbers start with &quot;9999A&quot; followed by a consecutive number of 7 digits.
        /// </summary>
        public string DsOrder { get; set; } = null!;
        /// <summary>
        /// Payment date
        /// </summary>
        public string? DsDate { get; set; }
        /// <summary>
        /// Payment hour, format HH:mm
        /// </summary>
        public string? DsHour { get; set; }
        /// <summary>
        /// Payment amount with no decimal point (the amount should be devided by 100 to get Euros)
        /// </summary>
        public string? DsAmount { get; set; }
        /// <summary>
        /// Payment currency code (978.Euros)
        /// </summary>
        public string? DsCurrency { get; set; }
        /// <summary>
        /// We are the merchant; merchant code is the code assigned to our account by the gateway service
        /// </summary>
        public string? DsMerchantCode { get; set; }
        /// <summary>
        /// We might operate with different terminal accounts for different purposes. This is not currently the case, so value for this field is always 1
        /// </summary>
        public string? DsTerminal { get; set; }
        /// <summary>
        /// Signature of the payment request sent  to the gateway
        /// </summary>
        public string? DsSignature { get; set; }
        /// <summary>
        /// Encrypted esponse sent by te gateway with the result of the operation
        /// </summary>
        public string? DsResponse { get; set; }
        /// <summary>
        /// Custom data we include on the request which is returned back on the gateway response to identify the request it refers to. It includes on an encoded string the user issuing the payment and the payment object (purchase order, delivery note, unpayment cancellation...)
        /// </summary>
        public string? DsMerchantData { get; set; }
        /// <summary>
        /// Free text payment concept
        /// </summary>
        public string? DsProductDescription { get; set; }
        /// <summary>
        /// Enumerable (0. Non secure payment, 1.Secure payment)
        /// </summary>
        public string? DsSecurePayment { get; set; }
        /// <summary>
        /// Type of transaction; always 0
        /// </summary>
        public string? DsTransactionType { get; set; }
        /// <summary>
        /// Enumerable (724.Spain)
        /// </summary>
        public string? DsCardCountry { get; set; }
        /// <summary>
        /// If authorisation is approved, gateway service returns a numerical code
        /// </summary>
        public string? DsAuthorisationCode { get; set; }
        /// <summary>
        /// Enumerable (001.Spanish)
        /// </summary>
        public string? DsConsumerLanguage { get; set; }
        /// <summary>
        /// Enumerable (C.Credit, D.Debit)
        /// </summary>
        public string? DsCardType { get; set; }
        /// <summary>
        /// Accounts entry on payment success. Foreign key for Cca table
        /// </summary>
        public Guid? CcaGuid { get; set; }
        /// <summary>
        /// User issuing the payment. Foreign key to Email table
        /// </summary>
        public Guid? User { get; set; }
        /// <summary>
        /// Payment object. Enumerable (1.Free, 2.Delivery note, 3.Purchase Order, 4.Unpayment cancellation)
        /// </summary>
        public int? Mode { get; set; }
        /// <summary>
        /// Depending on Mode field value, foreign key to the Delivery note Alb table, the Purchase Order Pdc table or the Unpayments Impagats table
        /// </summary>
        public Guid? Request { get; set; }
        /// <summary>
        /// Date the log was created
        /// </summary>
        public DateTime FchCreated { get; set; }
        /// <summary>
        /// True if signature received could be validated using the PaymentGateway.SignatureKey
        /// </summary>
        public bool? SignatureValidated { get; set; }
        /// <summary>
        /// True if the full operation completed successfully, including target object updates
        /// </summary>
        public bool? ProcessedSuccessfully { get; set; }
        /// <summary>
        /// Multiline exceptions descriptions, if any
        /// </summary>
        public string? Exceptions { get; set; }
        /// <summary>
        /// Customer issuing the payment; foreign key to CliGral table
        /// </summary>
        public Guid? Titular { get; set; }
        /// <summary>
        /// Encoded request data sent to the gateway
        /// </summary>
        public string? DsMerchantParameters { get; set; }
        /// <summary>
        /// Encoded response received from the gateway upon operation completed
        /// </summary>
        public string? DsSignatureReceived { get; set; }
    }
}
