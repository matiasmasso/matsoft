using System;

namespace DTO
{
    public class DTOSiiConsulta
    {
        public string Nif { get; set; }
        public string Invoice { get; set; }
        public DateTime Fch { get; set; }
        public string Csv { get; set; }
        public DateTime CsvFch { get; set; }
        public EstadosCuadre EstadoCuadre { get; set; }
        public DateTime TimestampEstadoCuadre { get; set; }
        public DateTime TimestampUltimaModificacion { get; set; }
        public DTOSiiLog.Results EstadoRegistro { get; set; }
        public int CodigoErrorRegistro { get; set; }
        public string DescripcionErrorRegistro { get; set; }


        // L23
        public enum EstadosCuadre
        {
            NotSet,
            NoContrastable // Estas facturas no permiten contrastarse
    ,
            EnProceso // En proceso de contraste. Estado "temporal" entre el alta/modificación de la factura y su intento de cuadre.
    ,
            NoContrastada // El emisor o el receptor no han registrado la factura (no hay coincidencia en el NIF del emisor, número de factura del emisor y fecha de expedición).
    ,
            ParcialmenteContrastada // El emisor y el receptor han registrado la factura (coincidencia en el NIF del emisor, número de factura del emisor y fecha de expedición) pero tiene discrepancias en algunos datos de la factura
    ,
            Contrastada // El emisor y el receptor han registrado la factura (coincidencia en el NIF del emisor, número de factura del emisor y fecha de expedición) con los mismos datos de la factura
        }
    }
}
