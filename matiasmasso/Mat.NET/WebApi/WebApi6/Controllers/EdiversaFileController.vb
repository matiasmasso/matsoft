Public Class EdiversaFileController

    Inherits _BaseController

    <HttpPost>
    <Route("api/ediversa/readpending")>
    Public Function ReadPending() As String
        Dim sb As New Text.StringBuilder
        Dim oEdiversaFiles As New List(Of DTOEdiversaFile)
        Dim exs As New List(Of Exception)
        If BLLEdiversaFiles.ReadPending(oEdiversaFiles, exs) Then
            Dim iComandes As Integer = oEdiversaFiles.Where(Function(x) x.Tag = DTOEdiversaFile.Tags.ORDERS_D_96A_UN_EAN008.ToString).Count
            sb.Append(String.Format("{0} comandes pendents de importar del servidor", iComandes))
        Else
            sb.Append(BLLExceptions.ToFlatString(exs))
        End If

        Return sb.ToString
    End Function

End Class
