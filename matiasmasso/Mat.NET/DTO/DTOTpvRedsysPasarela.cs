namespace DTO
{
    public class DTOTpvRedsysPasarela
    {
        public string Ds_Url_Tpv { get; set; }

        // Constante que indica la versión de firma que se está utilizando.
        public string Ds_SignatureVersion { get; set; } = "HMAC_SHA256_V1";

        // Cadena en formato JSON con todos los parámetros de la petición codificada en Base 64 y sin retornos de carro
        public string Ds_MerchantParameters { get; set; }

        // Firma de los datos enviados. Es el resultado del HMAC SHA256 de la cadena JSON codificada en Base 64
        public string Ds_Signature { get; set; }
    }
}
