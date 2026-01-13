
Public Class Repliq

    Shared Function Find(oGuid As Guid) As DTORepLiq
        Dim retval As DTORepLiq = RepLiqLoader.Find(oGuid)
        Return retval
    End Function

    Shared Function Pdf(ByVal oRepLiq As DTORepLiq) As Byte()
        Return RepLiqLoader.Pdf(oRepLiq)
    End Function


    Shared Function Update(exs As List(Of Exception), oRepliq As DTORepLiq) As Boolean
        Dim retval As Boolean = RepLiqLoader.Update(exs, oRepliq)
        Return retval
    End Function

    Shared Function Delete(oRepliq As DTORepLiq, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = RepLiqLoader.Delete(oRepliq, exs)
        Return retval
    End Function

    Shared Function Header(oEmp As DTOEmp, oInvoice As DTOInvoice, oRep As DTORep) As DTORepLiq
        Dim retval As DTORepLiq = Nothing
        Dim oRepLiqs As List(Of DTORepLiq) = RepLiqsLoader.Headers(oEmp, oInvoice, oRep)
        If oRepLiqs.Count > 0 Then retval = oRepLiqs(0)
        Return retval
    End Function
End Class


Public Class RepLiqs

    Shared Function Model(oUser As DTOUser) As Models.RepLiqsModel
        Return RepLiqsLoader.Model(oUser)
    End Function

    Shared Function All(Optional oRep As DTORep = Nothing) As List(Of DTORepLiq)
        Dim retval As List(Of DTORepLiq) = RepLiqsLoader.All(oRep.Emp, oRep)
        Return retval
    End Function

    Shared Function Headers(oEmp As DTOEmp, Optional oInvoice As DTOInvoice = Nothing, Optional oRep As DTORep = Nothing) As List(Of DTORepLiq)
        Dim retval As List(Of DTORepLiq) = RepLiqsLoader.Headers(oEmp, oInvoice, oRep)
        Return retval
    End Function

    Shared Function Headers(oRep As DTORep) As List(Of DTORepLiq)
        Dim retval = RepLiqsLoader.Headers(oRep:=oRep)
        Return retval
    End Function

    Shared Function Headers(oUser As DTOUser) As List(Of DTORepLiq)
        Dim retval = RepLiqsLoader.Headers(oUser:=oUser)
        Return retval
    End Function


    Shared Function Delete(exs As List(Of Exception), oRepLiqs As List(Of DTORepLiq)) As Boolean
        Dim retval As Boolean = RepLiqsLoader.Delete(oRepLiqs, exs)
        Return retval
    End Function
End Class
