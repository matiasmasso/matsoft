Public Class DTOSiiConsulta
    Property Nif As String
    Property Invoice As String
    Property Fch As Date
    Property Csv As String
    Property CsvFch As Date
    Property EstadoCuadre As EstadosCuadre
    Property TimestampEstadoCuadre As DateTime
    Property TimestampUltimaModificacion As DateTime
    Property EstadoRegistro As DTOSiiLog.Results
    Property CodigoErrorRegistro As Integer
    Property DescripcionErrorRegistro As String


    'L23
    Public Enum EstadosCuadre
        NotSet
        NoContrastable ' Estas facturas no permiten contrastarse
        EnProceso 'En proceso de contraste. Estado "temporal" entre el alta/modificación de la factura y su intento de cuadre.
        NoContrastada 'El emisor o el receptor no han registrado la factura (no hay coincidencia en el NIF del emisor, número de factura del emisor y fecha de expedición).
        ParcialmenteContrastada 'El emisor y el receptor han registrado la factura (coincidencia en el NIF del emisor, número de factura del emisor y fecha de expedición) pero tiene discrepancias en algunos datos de la factura
        Contrastada 'El emisor y el receptor han registrado la factura (coincidencia en el NIF del emisor, número de factura del emisor y fecha de expedición) con los mismos datos de la factura
    End Enum

End Class
