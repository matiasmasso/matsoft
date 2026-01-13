Public Class AmortizationItem
    Shared Function FromCcaSync(oCca As DTOCca, exs As List(Of Exception)) As DTOAmortizationItem
        Return Api.FetchSync(Of DTOAmortizationItem)(exs, "AmortizationItem", oCca.Guid.ToString())
    End Function

    Shared Function Load(ByRef oAmortizationItem As DTOAmortizationItem, exs As List(Of Exception)) As Boolean
        If Not oAmortizationItem.IsLoaded And Not oAmortizationItem.IsNew Then
            Dim pAmortizationItem = Api.FetchSync(Of DTOAmortizationItem)(exs, "AmortizationItem", oAmortizationItem.Cca.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOAmortizationItem)(pAmortizationItem, oAmortizationItem, exs)
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function


    Shared Async Function Update(oUser As DTOUser, oAmortizationItem As DTOAmortizationItem, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Execute(Of DTOAmortizationItem)(oAmortizationItem, exs, "AmortizationItem", oUser.Guid.ToString())
        oAmortizationItem.IsNew = False
    End Function

    Shared Async Function Delete(oAmortizationItem As DTOAmortizationItem, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOAmortizationItem)(oAmortizationItem, exs, "AmortizationItem")
    End Function

End Class
