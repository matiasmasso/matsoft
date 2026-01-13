Public Class ShipmentsReport

    ' Shared Function Merge(exs As List(Of Exception), ByRef oShipmentsReport As DTO.Integracions.Vivace.ShipmentsReport_Deprecated) As Boolean
    'Return ShipmentsReportLoader_Deprecated.Merge(exs, oShipmentsReport)
    'End Function

    Shared Function Update(exs As List(Of Exception), oShipmentsReport As DTO.Integracions.Vivace.ShipmentsReport) As Boolean
        Return ShipmentsReportLoader.Update(exs, oShipmentsReport)
    End Function



    Shared Function Procesa(exs As List(Of Exception), oShipmentsReport As DTO.Integracions.Vivace.ShipmentsReport, oEmp As DTOEmp, oDeliveries As List(Of DTODelivery)) As Boolean

        Dim oTradeInnDeliveries = oDeliveries.Where(Function(x) x.Contact.Equals(DTOCustomer.Wellknown(DTOCustomer.Wellknowns.tradeInn))).ToList()
        If oTradeInnDeliveries.Count > 0 Then
            BEBL.TradeInn.SendDesadvsViaFtp(exs, oTradeInnDeliveries)
        End If

        Dim oAmazonDeliveries = oDeliveries.Where(Function(x) x.Contact.FullNom.Contains("AMAZON")).ToList()
        If oAmazonDeliveries.Count > 0 Then
            BEBL.EdiversaDesadvs.SendViaEdi(exs, oAmazonDeliveries, oEmp)
        End If

        Return exs.Count = 0
    End Function


    Shared Function Deliveries(oShipmentsReportLog As DTOJsonLog) As List(Of DTODelivery)
        Return ShipmentsReportLoader.Deliveries(oShipmentsReportLog)
    End Function
End Class
