Public Class Pnd

    Shared Function Find(oGuid As Guid) As DTOPnd
        Dim retval As DTOPnd = PndLoader.Find(oGuid)
        Return retval
    End Function

    Shared Function Load(ByRef oPnd As DTOPnd) As Boolean
        Dim retval As Boolean = PndLoader.Load(oPnd)
        Return retval
    End Function

    Shared Function Update(oPnd As DTOPnd, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = PndLoader.Update(oPnd, exs)
        Return retval
    End Function

    Shared Function Delete(oPnd As DTOPnd, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = PndLoader.Delete(oPnd, exs)
        Return retval
    End Function

End Class


Public Class Pnds

    Shared Function All(oEmp As DTOEmp, Optional oContact As DTOContact = Nothing,
                        Optional sFraNum As String = "",
                        Optional DtFch As Date = Nothing,
                        Optional cod As DTOPnd.Codis = DTOPnd.Codis.NotSet,
                        Optional onlyPendents As Boolean = True,
                        Optional DcEur As Decimal = 0) As List(Of DTOPnd)
        'BEBL.Contact.Load(oContact)
        Dim retval As List(Of DTOPnd) = PndsLoader.All(oEmp, oContact, sFraNum, DtFch, cod, onlyPendents, DcEur)
        Return retval
    End Function

    Shared Function Pending(oEmp As DTOEmp, Optional Cod As DTOPnd.Codis = DTOPnd.Codis.NotSet, Optional IncludeDescomptats As Boolean = False) As List(Of DTOPnd)
        Return PndsLoader.Pending(oEmp, Cod, IncludeDescomptats)
    End Function

    Shared Function BankTransferReminderDeutors(oEmp As DTOEmp, Vto As Date) As List(Of DTOCustomer)
        Return PndsLoader.BankTransferReminderDeutors(oEmp, Vto)
    End Function

    Shared Function Cartera(oEmp As DTOEmp, DtFch As Date) As List(Of DTOPnd)
        Return PndsLoader.Cartera(oEmp, DtFch)
    End Function

    Shared Function Descuadres(oExercici As DTOExercici) As List(Of DTOPgcSaldo)
        Dim retval As New List(Of DTOPgcSaldo)
        Dim oPnds = Pending(oExercici.Emp, IncludeDescomptats:=True).
            Where(Function(x) x.Cta IsNot Nothing And x.Contact IsNot Nothing).ToList

        Dim oCtaCods = {DTOPgcPlan.Ctas.ProveidorsEur,
            DTOPgcPlan.Ctas.ProveidorsGbp,
            DTOPgcPlan.Ctas.ProveidorsUsd,
            DTOPgcPlan.Ctas.DeutorsVaris,
            DTOPgcPlan.Ctas.Clients}

        Dim oSdos = BEBL.PgcSaldos.All(oExercici, HideEmptySaldo:=True).
            Where(Function(x) x.Epg IsNot Nothing And x.Contact IsNot Nothing).ToList

        oSdos = oSdos.
            Where(Function(x) oCtaCods.Any(Function(y) y = DirectCast(x.Epg, DTOPgcCta).Codi)).
            ToList

        For Each oSdo In oSdos
            Dim oCtaGuid = oSdo.Epg.Guid
            Dim oCcdPnds = oPnds.Where(Function(x) x.Cta.Guid.Equals(oCtaGuid) And x.Contact.Equals(oSdo.Contact)).ToList
            Dim o = oPnds.Where(Function(x) x.Contact.Equals(oSdo.Contact)).ToList
            Dim oCcdPndsNotEmpty = oCcdPnds.Where(Function(x) x.Amt IsNot Nothing).ToList
            Dim dcSum = oCcdPndsNotEmpty.Sum(Function(x) x.Amt.Eur)
            oSdo.Pendent = DTOAmt.Factory(dcSum)
            Dim DcSdo = IIf(oSdo.IsDeutor, oSdo.SdoDeudor.Eur, -oSdo.SdoCreditor.Eur)
            If Math.Abs(oSdo.Pendent.Eur) <> Math.Abs(DcSdo) Then
                retval.Add(oSdo)
            End If
        Next

        Dim oMissing = oPnds.
            Where(Function(x) x.Cta IsNot Nothing AndAlso Not oSdos.Any(Function(y) y.Epg IsNot Nothing AndAlso y.Epg.Guid.Equals(x.Cta.Guid))).
            GroupBy(Function(g) New With {Key g.Cta, g.Contact}).
            Select(Function(group) New With {.Cta = group.Key.Cta,
            .Contact = group.Key.Contact,
            .Eur = group.Sum(Function(z) z.Amt.Eur)}).ToList

        For Each o In oMissing
            Dim oSdo As New DTOPgcSaldo()
            With oSdo
                .Epg = o.Cta
                .Contact = o.Contact
                .Pendent = DTOAmt.Factory(o.Eur)
            End With
            If oSdo.Contact IsNot Nothing Then
                If oSdo.Contact.FullNom > "" Then
                    retval.Add(oSdo)
                End If
            End If
        Next

        Return retval
    End Function

End Class