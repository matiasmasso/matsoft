Public Class LangLoader

    Shared Function FromTag(sTag As String) As DTOLang
        Dim retval As DTOLang = Nothing
        Dim oId As DTOLang.Ids
        If [Enum].TryParse(Of DTOLang.Ids)(sTag.ToUpper, oId) Then
            retval = New DTOLang
            retval.Id = oId
        End If
        Return retval
    End Function


End Class
