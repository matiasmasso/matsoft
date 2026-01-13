Public Class TradeInn

    Shared Function SendDesadvsViaFtp(exs As List(Of Exception), oDeliveries As List(Of DTODelivery)) As Boolean
        If oDeliveries.Count > 0 Then
            Dim oEmp = BEBL.Emp.Find(DTOEmp.Ids.MatiasMasso) 'per dades GLN Org
            BEBL.Contact.Load(oEmp.Org)
            For Each oDelivery In oDeliveries
                If BEBL.Delivery.Load(oDelivery) Then
                    oDelivery.emp = oEmp

                    Dim oDesadv = oDelivery.Desadv(exs)
                    Dim oByteArray = System.Text.Encoding.UTF8.GetBytes(oDesadv.EdiMessage())

                    BEBL.Ftpserver.Send(exs, oDelivery.customer, DTOFtpserver.Path.Cods.Desadv, oByteArray, oDesadv.DefaultFilename)
                End If
            Next
        End If
        Return exs.Count = 0
    End Function
End Class
