Public Class Langs

    Shared Function All() As List(Of DTOLang)
        Static retval As List(Of DTOLang)
        If retval Is Nothing Then
            retval = New List(Of DTOLang)
            With retval
                .Add(New DTOLang(DTOLang.Ids.ESP))
                .Add(New DTOLang(DTOLang.Ids.CAT))
                .Add(New DTOLang(DTOLang.Ids.ENG))
                .Add(New DTOLang(DTOLang.Ids.POR))
            End With
        End If
        Return retval
    End Function

End Class
