Public Class BuzonController
    Inherits _BaseController

    <HttpPost>
    <Route("api/buzon")>
    Public Function Buzon(src As String) As String
        Dim retval As String = "<RESPUESTA>La gestión del documento recibido se halla en periodo de pruebas y aun no se valida</RESPUESTA>"
        Return retval
    End Function
End Class
