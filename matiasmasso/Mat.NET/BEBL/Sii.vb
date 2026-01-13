Public Class Sii
    Shared Function SendEmeses(oEmp As DTOEmp, exs As List(Of Exception)) As Boolean
        Dim FchTo As Date = TimeHelper.AddDiasHabils(Today, -4)

        Dim oFacturesEmeses As List(Of DTOInvoice) = BEBL.Invoices.SiiPending(oEmp).ToList
        Dim oEmesesToSend As List(Of DTOInvoice) = oFacturesEmeses.Where(Function(x) x.Fch <= FchTo).ToList

        If oEmesesToSend.Count > 0 Then
            '============================================================================================================
            oEmesesToSend.RemoveRange(3, oEmesesToSend.Count - 3)
            '============================================================================================================
            BEBL.Invoices.SendToSii(DTO.Defaults.Entornos.Produccion, oEmp.Org, oEmesesToSend, exs)
        End If

        Return exs.Count = 0
    End Function

    Shared Function SendRebudes(oEmp As DTOEmp, exs As List(Of Exception)) As Boolean
        Dim FchTo As Date = TimeHelper.AddDiasHabils(Today, -4)

        Dim oFacturesRebudes As List(Of DTOBookFra) = BEBL.BookFras.SiiPending(oEmp).ToList
        Dim oRebudesToSend As List(Of DTOBookFra) = oFacturesRebudes.Where(Function(x) x.Cca.Fch <= FchTo).ToList

        If oRebudesToSend.Count > 0 Then
            BEBL.BookFras.SendToSii(DTO.Defaults.Entornos.Produccion, oEmp.Org, oRebudesToSend, exs)
        End If

        Return exs.Count = 0
    End Function

End Class
