Imports System.Web.Http
Public Class BuzonController

    Inherits _BaseController

    <HttpPost>
    <Route("api/buzon")>
    Public Function Buzon(src As String) As String
        Dim retval As String = "<RESPUESTA>Información recibida pero no validada. Servicio en pruebas</RESPUESTA>"
        Return retval
    End Function
End Class
