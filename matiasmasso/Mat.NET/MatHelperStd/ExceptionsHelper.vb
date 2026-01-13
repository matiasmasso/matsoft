Public Class ExceptionsHelper
    Shared Function ToFlatString(oExs As List(Of Exception)) As String
        Dim retval As String = ""
        If oExs IsNot Nothing Then
            Dim msgs = oExs.Select(Function(x) x.Message).ToArray
            retval = String.Join("-", msgs)
        End If
        Return retval
    End Function

    Shared Function ToHtml(oExs As List(Of Exception)) As String
        Dim oArray = oExs.Select(Function(x) x.Message)
        Dim retval As String = String.Join("<br/>", oArray)
        Return retval
    End Function
End Class
