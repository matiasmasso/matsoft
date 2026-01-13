Public Class CustomerPlatform
    Shared Async Function Find(oGuid As Guid, exs As List(Of Exception)) As Task(Of DTOCustomerPlatform)
        Return Await Api.Fetch(Of DTOCustomerPlatform)(exs, "CustomerPlatform", oGuid.ToString())
    End Function

    Shared Function Load(ByRef oCustomerPlatform As DTOCustomerPlatform, exs As List(Of Exception)) As Boolean
        If Not oCustomerPlatform.IsLoaded And Not oCustomerPlatform.IsNew Then
            Dim pCustomerPlatform = Api.FetchSync(Of DTOCustomerPlatform)(exs, "CustomerPlatform", oCustomerPlatform.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOCustomerPlatform)(pCustomerPlatform, oCustomerPlatform, exs)
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Update(oCustomerPlatform As DTOCustomerPlatform, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Update(Of DTOCustomerPlatform)(oCustomerPlatform, exs, "CustomerPlatform")
        oCustomerPlatform.IsNew = False
    End Function

    Shared Async Function Delete(oCustomerPlatform As DTOCustomerPlatform, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOCustomerPlatform)(oCustomerPlatform, exs, "CustomerPlatform")
    End Function
End Class

Public Class CustomerPlatforms

    Shared Async Function All(oParent As DTOContact, exs As List(Of Exception)) As Task(Of List(Of DTOCustomerPlatform))
        Return Await Api.Fetch(Of List(Of DTOCustomerPlatform))(exs, "CustomerPlatforms", oParent.Guid.ToString())
    End Function

End Class
