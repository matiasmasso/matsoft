Public Class DTOSiiLog
    Inherits DTOBaseGuid

    Property Entorno As Defaults.Entornos
    Property Fch As DateTime
    Property Contingut As Continguts
    Property Result As Results
    Property TipoDeComunicacion As String 'A0 (alta), A1 (modificacion), A4
    Property Csv As String 'Nullable, 16 chars

    Property ErrMsg As String

    Property Nif As String
    Property FraNum As String

    Public Enum Continguts
        NotSet
        Facturas_Emitidas
        Facturas_Recibidas
    End Enum
    Public Enum Results
        NotSet
        Correcto
        Parcialmente_Correcto
        Incorrecto
        Error_De_Comunicacion
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
    End Sub

    Shared Function TipoDeComunicacionText(value As DTOSiiLog) As String
        Dim pair As KeyValuePair(Of String, String) = TiposDeComunicacion.FirstOrDefault(Function(x) x.Key = value.TipoDeComunicacion)
        Dim retval As String = pair.Value
        Return retval
    End Function

    Shared Function TiposDeComunicacion() As List(Of KeyValuePair(Of String, String))
        Dim retval As New List(Of KeyValuePair(Of String, String))
        retval.Add(New KeyValuePair(Of String, String)("", "(sel·leccionar tipus de comunicació)"))
        retval.Add(New KeyValuePair(Of String, String)("A0", "A0 Alta de facturas/registro"))
        retval.Add(New KeyValuePair(Of String, String)("A1", "Modificación de facturas/registros (errores registrales)"))
        retval.Add(New KeyValuePair(Of String, String)("A4", "A4 Modificación Factura Régimen de Viajeros"))
        Return retval
    End Function

    Shared Function ResultText(value As DTOSiiLog) As String
        Dim retval As String = ""
        Select Case value.Result
            Case DTOSiiLog.Results.Correcto
                retval = "Correcte"
            Case DTOSiiLog.Results.Parcialmente_Correcto
                retval = "Parcialment correcte"
            Case DTOSiiLog.Results.Incorrecto
                retval = "Incorrecte"
            Case DTOSiiLog.Results.Error_De_Comunicacion
                retval = "Error de comunicació"
        End Select
        Return retval
    End Function
End Class
