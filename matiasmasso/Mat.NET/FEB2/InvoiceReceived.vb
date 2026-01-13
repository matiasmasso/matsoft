Public Class InvoiceReceived
    Inherits _FeblBase

    Shared Async Function Find(exs As List(Of Exception), oGuid As Guid) As Task(Of DTOInvoiceReceived)
        Return Await Api.Fetch(Of DTOInvoiceReceived)(exs, "InvoiceReceived", oGuid.ToString())
    End Function

    Shared Function Load(exs As List(Of Exception), ByRef oInvoiceReceived As DTOInvoiceReceived) As Boolean
        If Not oInvoiceReceived.IsLoaded And Not oInvoiceReceived.IsNew Then
            Dim pInvoiceReceived = Api.FetchSync(Of DTOInvoiceReceived)(exs, "InvoiceReceived", oInvoiceReceived.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOInvoiceReceived)(pInvoiceReceived, oInvoiceReceived, exs)
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function


    Shared Async Function Update(exs As List(Of Exception), oInvoiceReceived As DTOInvoiceReceived) As Task(Of Boolean)
        Return Await Api.Update(Of DTOInvoiceReceived)(oInvoiceReceived, exs, "InvoiceReceived")
        oInvoiceReceived.IsNew = False
    End Function

    Shared Async Function Delete(exs As List(Of Exception), value As DTOInvoiceReceived) As Task(Of Boolean)
        Dim values As New List(Of DTOInvoiceReceived)
        values.Add(value)
        Return Await InvoicesReceived.Delete(exs, values)
    End Function

    Shared Async Function Delivery(exs As List(Of Exception), oInvoiceReceived As DTOInvoiceReceived, oUser As DTOUser) As Task(Of DTODelivery)
        Return Await Api.Fetch(Of DTODelivery)(exs, "InvoiceReceived/delivery", oInvoiceReceived.Guid.ToString(), oUser.Guid.ToString)
    End Function

End Class

Public Class InvoicesReceived
    Inherits _FeblBase

    Shared Async Function All(exs As List(Of Exception), year As Integer) As Task(Of List(Of DTOInvoiceReceived))
        Dim retval = Await Api.Fetch(Of List(Of DTOInvoiceReceived))(exs, "InvoicesReceived", year)
        Return retval
    End Function

    Shared Async Function FromConfirmation(exs As List(Of Exception), oConfirmation As DTOImportacio.Confirmation) As Task(Of List(Of DTOInvoiceReceived))
        Return Await Api.Execute(Of DTOImportacio.Confirmation, List(Of DTOInvoiceReceived))(oConfirmation, exs, "InvoicesReceived/FromConfirmation")
    End Function

    Shared Async Function Delete(exs As List(Of Exception), oInvoicesReceived As List(Of DTOInvoiceReceived)) As Task(Of Boolean)
        Return Await Api.Delete(Of List(Of DTOInvoiceReceived))(oInvoicesReceived, exs, "InvoicesReceived")
    End Function

    Shared Async Function SetPrevisions(exs As List(Of Exception), oInvoicesReceived As List(Of DTOInvoiceReceived), oImportacio As DTOImportacio) As Task(Of Boolean)
        Return Await Api.Execute(Of List(Of DTOInvoiceReceived), Boolean)(oInvoicesReceived, exs, "InvoicesReceived/SetPrevisions", oImportacio.Guid.ToString)
    End Function

End Class
