Public Class Incentiu

    Shared Function Find(oGuid As Guid) As DTOIncentiu
        Dim retval As DTOIncentiu = IncentiuLoader.Find(oGuid)
        Return retval
    End Function

    Shared Function Update(oIncentiu As DTOIncentiu, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = IncentiuLoader.Update(oIncentiu, exs)
        Return retval
    End Function

    Shared Function Delete(oIncentiu As DTOIncentiu, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = IncentiuLoader.Delete(oIncentiu, exs)
        Return retval
    End Function

    Shared Function PurchaseOrders(oIncentiu As DTOIncentiu, oUser As DTOUser) As List(Of DTOPurchaseOrder)
        Dim retval As List(Of DTOPurchaseOrder) = BEBL.PurchaseOrders.All(oIncentiu, oUser)
        Return retval
    End Function

    Shared Function Participants(oIncentiu As DTOIncentiu) As List(Of DTOContact)
        Dim retval As List(Of DTOContact) = IncentiuLoader.Participants(oIncentiu)
        Return retval
    End Function

    Shared Function DeliveryAddresses(oIncentiu As DTOIncentiu) As MatHelper.Excel.Sheet
        Return IncentiuLoader.DeliveryAddresses(oIncentiu)
    End Function

    Shared Function PncProducts(oIncentiu As DTOIncentiu) As List(Of DTOProductSkuQtyEur)
        Dim retval As List(Of DTOProductSkuQtyEur) = IncentiuLoader.PncProducts(oIncentiu)
        Return retval
    End Function

    Shared Function ExcelResults(oIncentiu As DTOIncentiu) As MatHelper.Excel.Sheet
        Return IncentiuLoader.ExcelResults(oIncentiu)
    End Function
End Class



Public Class Incentius

    Shared Function All(oUser As DTOUser, Optional BlIncludeObsolets As Boolean = False, Optional BlIncludeFutureIncentius As Boolean = False) As List(Of DTOIncentiu)
        Dim retval As List(Of DTOIncentiu) = IncentiusLoader.All(oUser, BlIncludeObsolets, BlIncludeFutureIncentius)
        Return retval
    End Function

    Shared Function Headers(oUser As DTOUser, Optional BlIncludeObsolets As Boolean = False, Optional BlIncludeFutureIncentius As Boolean = False) As List(Of DTOIncentiu)
        Dim retval As List(Of DTOIncentiu) = IncentiusLoader.Headers(oUser, BlIncludeObsolets, BlIncludeFutureIncentius)
        Return retval
    End Function

End Class
