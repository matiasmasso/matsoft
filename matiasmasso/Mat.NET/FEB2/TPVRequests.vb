Public Class TPVRequest

    Shared Async Function FromMode(oMode As DTOTpvRequest.Modes, oGuid As Guid, oLang As DTOLang, exs As List(Of Exception)) As Task(Of DTOTpvRequest)
        Dim retval As DTOTpvRequest = Nothing
        Select Case oMode
            Case DTOTpvRequest.Modes.Free
                retval = DTOTpvRequest.FromFreeConcept(oLang, "", 0, oGuid)
            Case DTOTpvRequest.Modes.Pdc
                Dim oPurchaseOrder = Await FEB2.PurchaseOrder.Find(oGuid, exs)
                retval = DTOTpvRequest.FromPdc(oPurchaseOrder, oLang)
            Case DTOTpvRequest.Modes.Alb
                Dim oDelivery As DTODelivery = Await FEB2.Delivery.Find(oGuid, exs)
                If oDelivery Is Nothing Then
                    Dim ex As New Exception("can't pay a missing document")
                    ex.Data.Add("UserDisplayText", oLang.Tradueix("Este albarán ya no está disponible.", "Aquest albarà ja no està disponible.", "This delivery note is no longer available.") & oLang.Tradueix("Por favor consulte con nuestras oficinas", "Si us plau consulti amb les nostres oficines", "Please ask our offices"))
                    exs.Add(ex)
                Else
                    retval = DTOTpvRequest.FromAlb(oDelivery, oLang)
                End If
            Case DTOTpvRequest.Modes.Impagat
                Dim oImpagat As DTOImpagat = Await FEB2.Impagat.Find(oGuid, exs)
                retval = DTOTpvRequest.FromImpagat(oImpagat, oLang)
        End Select
        Return retval
    End Function
End Class
