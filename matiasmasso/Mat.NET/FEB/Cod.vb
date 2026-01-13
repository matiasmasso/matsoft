Public Class Cod
    Inherits _FeblBase

    Shared Async Function Find(exs As List(Of Exception), oGuid As Guid) As Task(Of DTOCod)
        Return Await Api.Fetch(Of DTOCod)(exs, "Cod", oGuid.ToString())
    End Function

    Shared Function Load(exs As List(Of Exception), ByRef oCod As DTOCod) As Boolean
        If Not oCod.IsLoaded And Not oCod.IsNew Then
            Dim pCod = Api.FetchSync(Of DTOCod)(exs, "Cod", oCod.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOCod)(pCod, oCod, exs)
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Update(exs As List(Of Exception), oCod As DTOCod) As Task(Of Boolean)
        Return Await Api.Update(Of DTOCod)(oCod, exs, "Cod")
        oCod.IsNew = False
    End Function


    Shared Async Function Delete(exs As List(Of Exception), oCod As DTOCod) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOCod)(oCod, exs, "Cod")
    End Function
End Class

Public Class Cods
    Inherits _FeblBase

    Shared Async Function All(exs As List(Of Exception), Optional oParent As DTOCod = Nothing) As Task(Of DTOCod.Collection)
        Dim retval As DTOCod.Collection = Nothing
        If oParent Is Nothing Then
            retval = Await Api.Fetch(Of DTOCod.Collection)(exs, "Cods")
        Else
            retval = Await Api.Fetch(Of DTOCod.Collection)(exs, "Cods", OpcionalGuid(oParent))
        End If
        Return retval
    End Function

    Shared Async Function All(exs As List(Of Exception), parentId As DTOCod.Wellknowns, oLang As DTOLang) As Task(Of List(Of DTO.Models.Base.IdNom))
        Dim oParent = DTOCod.Wellknown(parentId)
        Dim retval = Await Api.Fetch(Of List(Of DTO.Models.Base.IdNom))(exs, "Cods/IdNoms", oParent.Guid.ToString(), oLang.Tag)
        Return retval
    End Function

    Shared Async Function Sort(exs As List(Of Exception), oCods As DTOCod.Collection) As Task(Of Boolean)
        Dim oGuids = oCods.Select(Function(x) x.Guid).ToList()
        Return Await Api.Execute(Of List(Of Guid))(oGuids, exs, "Cods/sort")
    End Function

End Class
