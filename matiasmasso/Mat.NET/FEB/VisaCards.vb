Public Class VisaCard


    Shared Async Function Find(oGuid As Guid, exs As List(Of Exception)) As Task(Of DTOVisaCard)
        Return Await Api.Fetch(Of DTOVisaCard)(exs, "VisaCard", oGuid.ToString())
    End Function

    Shared Function Load(ByRef oVisaCard As DTOVisaCard, exs As List(Of Exception)) As Boolean
        If Not oVisaCard.IsLoaded And Not oVisaCard.IsNew Then
            Dim pVisaCard = Api.FetchSync(Of DTOVisaCard)(exs, "VisaCard", oVisaCard.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOVisaCard)(pVisaCard, oVisaCard, exs)
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Update(oVisaCard As DTOVisaCard, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Update(Of DTOVisaCard)(oVisaCard, exs, "VisaCard")
        oVisaCard.IsNew = False
    End Function


    Shared Async Function Delete(oVisaCard As DTOVisaCard, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOVisaCard)(oVisaCard, exs, "VisaCard")
    End Function
End Class

Public Class VisaCards

    Shared Async Function All(exs As List(Of Exception), oEmp As DTOEmp, Optional oContact As DTOContact = Nothing, Optional active As Boolean = True) As Task(Of List(Of DTOVisaCard))
        Dim oContactGuid As Guid = Nothing
        If oContact IsNot Nothing Then oContactGuid = oContact.Guid
        Return Await Api.Fetch(Of List(Of DTOVisaCard))(exs, "VisaCards", oEmp.Id, oContactGuid.ToString, If(active, 1, 0))
    End Function

End Class

