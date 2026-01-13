Public Class ConsumerTicket
    Inherits _FeblBase

    Shared Async Function Find(exs As List(Of Exception), oGuid As Guid) As Task(Of DTOConsumerTicket)
        Return Await Api.Fetch(Of DTOConsumerTicket)(exs, "ConsumerTicket", oGuid.ToString())
    End Function

    Shared Async Function FromDelivery(exs As List(Of Exception), oDelivery As DTODelivery) As Task(Of DTOConsumerTicket)
        Return Await Api.Fetch(Of DTOConsumerTicket)(exs, "ConsumerTicket/FromDelivery", oDelivery.Guid.ToString())
    End Function

    Shared Function Load(exs As List(Of Exception), ByRef oConsumerTicket As DTOConsumerTicket) As Boolean
        If Not oConsumerTicket.IsLoaded And Not oConsumerTicket.IsNew Then
            Dim pConsumerTicket = Api.FetchSync(Of DTOConsumerTicket)(exs, "ConsumerTicket", oConsumerTicket.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOConsumerTicket)(pConsumerTicket, oConsumerTicket, exs)
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Update(exs As List(Of Exception), value As DTOConsumerTicket) As Task(Of Integer)
        Dim retval As Boolean
        Dim serialized = Api.Serialize(value, exs)
        If exs.Count = 0 Then
            Dim oMultipart As New ApiHelper.MultipartHelper()
            oMultipart.AddStringContent("Serialized", serialized)
            If value.PurchaseOrder IsNot Nothing AndAlso value.PurchaseOrder.DocFile IsNot Nothing Then
                oMultipart.AddFileContent("docfile_thumbnail", value.PurchaseOrder.DocFile.Thumbnail)
                oMultipart.AddFileContent("docfile_stream", value.PurchaseOrder.DocFile.Stream)
            End If
            retval = Await Api.Upload(oMultipart, exs, "ConsumerTicket")
        End If
        Return retval
    End Function

    Shared Async Function Delete(exs As List(Of Exception), oConsumerTicket As DTOConsumerTicket) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOConsumerTicket)(oConsumerTicket, exs, "ConsumerTicket")
    End Function
End Class

Public Class ConsumerTickets
    Inherits _FeblBase

    Shared Async Function All(exs As List(Of Exception), oEmp As DTOEmp, year As Integer) As Task(Of List(Of DTOConsumerTicket))
        Dim retval = Await Api.Fetch(Of List(Of DTOConsumerTicket))(exs, "ConsumerTickets", oEmp.Id, year)
        Return retval
    End Function

    Shared Async Function All(exs As List(Of Exception), oMarketPlace As DTOMarketPlace, year As Integer) As Task(Of List(Of DTOConsumerTicket))
        Return Await Api.Fetch(Of List(Of DTOConsumerTicket))(exs, "ConsumerTickets/FromMarketPlace", oMarketPlace.Guid.ToString, year)
    End Function

End Class

