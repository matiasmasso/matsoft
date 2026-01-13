Public Class Session

    Public Function AppPath() As String
        Dim retval As String = ""
        Dim Str As String
        Dim i As Integer
        Str = Application.ExecutablePath
        For i = Len(Str) To 1 Step -1
            If Mid$(Str, i, 1) = "\" Then
                retval = Left$(Str, i)
                Exit For
            End If
        Next
        Return retval
    End Function

    Public Function GetTemplate(ByVal TemplateName As String) As String
        GetTemplate = AppPath() & TemplateName & ".dot"
    End Function



End Class
