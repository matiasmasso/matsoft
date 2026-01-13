Public Class Amortization
    Shared Function Find(oGuid As Guid) As DTOAmortization
        Dim retval As DTOAmortization = AmortizationLoader.Find(oGuid)
        Return retval
    End Function

    Shared Function Load(ByRef oAmortization As DTOAmortization) As Boolean
        Dim retval As Boolean = AmortizationLoader.Load(oAmortization)
        Return retval
    End Function

    Shared Function FromAlta(value As DTOCca) As DTOAmortization
        Dim retval As DTOAmortization = AmortizationLoader.FromAlta(value)
        Return retval
    End Function

    Shared Function FromBaixa(value As DTOCca) As DTOAmortization
        Dim retval As DTOAmortization = AmortizationLoader.FromBaixa(value)
        Return retval
    End Function

    Shared Function Update(oAmortization As DTOAmortization, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = AmortizationLoader.Update(oAmortization, exs)
        Return retval
    End Function

    Shared Function Delete(oAmortization As DTOAmortization, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = AmortizationLoader.Delete(oAmortization, exs)
        Return retval
    End Function

    Shared Function Amortitza(oUser As DTOUser, oAmortization As DTOAmortization, ByVal oExercici As DTOExercici, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim DcTipus As Decimal = oAmortization.Tipus
        If DcTipus > 0 Then
            Dim iDaysFromAdquisicio As Integer = (oExercici.LastFch - oAmortization.Fch).Days
            If iDaysFromAdquisicio < 360 Then
                DcTipus = Math.Round(DcTipus * iDaysFromAdquisicio / 365, 2, MidpointRounding.AwayFromZero)
            End If
            Dim oCtaImmobilitzat As DTOPgcCta = oAmortization.Cta
            BEBL.PgcCta.Load(oCtaImmobilitzat)

            Dim oCtaDotacio = BEBL.PgcCta.FromCod(DTOAmortization.CtaCodDotacio(oCtaImmobilitzat.Codi), oUser.Emp)
            Dim oCtaAmrtAcumulada = BEBL.PgcCta.FromCod(DTOAmortization.CtaCodAmrtAcumulada(oCtaImmobilitzat.Codi), oUser.Emp)

            Dim DcValorInicial As Decimal = oAmortization.Amt.Eur
            Dim DcValorResidual As Decimal = 1
            Dim DcAmortitzat As Decimal = oAmortization.Items.Sum(Function(x) x.Amt.Eur)
            Dim DcPendiente As Decimal = DcValorInicial - DcAmortitzat
            Dim DcCuota As Decimal = Math.Round(DcValorInicial * DcTipus / 100, 2, MidpointRounding.AwayFromZero)

            Dim BlSaldo As Boolean
            If DcCuota > (DcPendiente - DcValorResidual) Then
                DcCuota = DcPendiente - DcValorResidual
                DcTipus = Math.Round(100 * DcCuota / DcValorInicial, 2, MidpointRounding.AwayFromZero)
                BlSaldo = True
            End If

            Dim oCuota As DTOAmt = DTOAmt.Factory(DcCuota)

            Dim DtFch As New Date(oExercici.Year, 12, 31)
            Dim oCca As DTOCca = DTOCca.Factory(DtFch, oUser, DTOCca.CcdEnum.Amortitzacions)

            oCca.AddDebit(oCuota, oCtaDotacio)
            oCca.AddSaldo(oCtaAmrtAcumulada)

            Dim item As New DTOAmortizationItem()
            With item
                .Parent = oAmortization
                .Fch = DtFch
                .Tipus = DcTipus
                .Amt = oCuota
                .Cod = DTOAmortizationItem.Cods.Amortitzacio
                .Cca = oCca
            End With

            If BlSaldo Then
                oCca.Concept = "Cta." & oCtaImmobilitzat.Id & " Saldo " & oAmortization.Dsc
            Else
                oCca.Concept = BEBL.AmortizationItem.ConcepteAmortitzacio(item)
            End If
            oCca.Ref = item

            retval = AmortizationItemLoader.Update(item, exs)
        Else
            'partida no amortitzable (tipus 0)
            retval = True
        End If

        Return retval
    End Function


End Class
Public Class Amortizations

    Shared Function All(oEmp As DTOEmp) As List(Of DTOAmortization)
        Dim retval As List(Of DTOAmortization) = AmortizationsLoader.All(oEmp)
        Return retval
    End Function

    Shared Function DefaultTipus() As List(Of DTOAmortizationTipus)
        Return AmortizationsLoader.DefaultTipus()
    End Function

    Shared Function PendentsDeAmortitzar(oExercici As DTOExercici) As List(Of DTOAmortization)
        Dim retval As List(Of DTOAmortization) = AmortizationsLoader.PendentsDeAmortitzar(oExercici)
        Return retval
    End Function

    Shared Function Amortitza(oUser As DTOUser, oExercici As DTOExercici, exs As List(Of Exception)) As Boolean
        Dim items = BEBL.Amortizations.PendentsDeAmortitzar(oExercici)
        For Each item As DTOAmortization In items
            BEBL.Amortization.Load(item)
            Dim ex2 As New List(Of Exception)
            If Not BEBL.Amortization.Amortitza(oUser, item, oExercici, ex2) Then
                exs.Add(New Exception("error al amortitzar " & item.Dsc))
                exs.AddRange(ex2)
            End If
        Next
        Dim retval As Boolean = (exs.Count = 0)
        Return retval
    End Function

    Shared Function RevertAmortizations(oExercici As DTOExercici, exs As List(Of Exception)) As Boolean
        Dim items As List(Of DTOAmortizationItem) = BEBL.AmortizationItems.LastItems(oExercici)
        For Each item As DTOAmortizationItem In items
            Dim ex2 As New List(Of Exception)
            If Not BEBL.AmortizationItem.Delete(item, ex2) Then
                exs.Add(New Exception("error al retrocedir " & item.Parent.Dsc))
                exs.AddRange(ex2)
            End If
        Next
        Dim retval As Boolean = (exs.Count = 0)
        Return retval
    End Function


    Shared Function Excel(items As List(Of DTOAmortization)) As MatHelper.Excel.Sheet
        Dim retval As New MatHelper.Excel.Sheet("catalogo M+O")
        With retval
            .AddColumn("Compte", MatHelper.Excel.Cell.NumberFormats.PlainText)
            .AddColumn("Concepte", MatHelper.Excel.Cell.NumberFormats.PlainText)
            .AddColumn("Data Adquisició", MatHelper.Excel.Cell.NumberFormats.Euro)
            .AddColumn("Valor Adquisició", MatHelper.Excel.Cell.NumberFormats.Euro)
            .AddColumn("Valor Amortitzat", MatHelper.Excel.Cell.NumberFormats.Euro)
            .AddColumn("Saldo", MatHelper.Excel.Cell.NumberFormats.Euro)
        End With

        For Each item In items
            Dim valorAdquisicio As Decimal = item.Amt.Eur
            Dim valorAmortitzat As Decimal = item.Items.Sum(Function(x) x.Amt.Eur)
            Dim oRow As MatHelper.Excel.Row = retval.AddRow()
            oRow.AddCell(DTOPgcCta.FullNom(item.Cta, DTOLang.CAT))
            oRow.AddCell(item.Dsc)
            oRow.AddCell(item.Fch)
            oRow.AddCell(valorAdquisicio)
            oRow.AddCell(valorAmortitzat)
            oRow.AddCell(valorAdquisicio - valorAmortitzat)
        Next
        Return retval
    End Function
End Class


Public Class AmortizationItem

    Shared Function Find(oGuid As Guid) As DTOAmortizationItem
        Dim retval As DTOAmortizationItem = AmortizationItemLoader.Find(oGuid)
        Return retval
    End Function

    Shared Function Load(ByRef oAmortizationItem As DTOAmortizationItem) As Boolean
        Dim retval As Boolean
        Dim oParent As DTOAmortization = oAmortizationItem.Parent
        BEBL.Amortization.Load(oParent)
        Dim oGuid As Guid = oAmortizationItem.Guid
        Dim Query = oParent.Items.Where(Function(x) x.Guid.Equals(oGuid)).ToList
        If Query.Count > 0 Then
            oAmortizationItem = Query.First
            retval = True
        End If
        Return retval
    End Function

    Shared Function Update(oUser As DTOUser, oAmortizationItem As DTOAmortizationItem, ByRef exs As List(Of Exception)) As Boolean
        Dim oCca As DTOCca = Nothing

        If oAmortizationItem.IsNew Then
            oCca = DTOCca.Factory(oAmortizationItem.Fch, oUser, DTOCca.CcdEnum.InmovilitzatBaixa)
            oCca.Concept = Left("Baixa de immobilitzat " & oAmortizationItem.Parent.Dsc, 60)
        Else
            oCca = oAmortizationItem.Cca
            BEBL.Cca.Load(oCca)
            oCca.Items.Clear()
            oCca.UsrLog.usrLastEdited = oUser
        End If

        Dim oSaldo As DTOAmt = DTOAmortization.Saldo(oAmortizationItem.Parent)
        Dim oCtaImmobilitzat As DTOPgcCta = oAmortizationItem.Parent.Cta
        Dim oCtaAmrtAcumulada = BEBL.PgcCta.FromCod(DTOAmortization.CtaCodAmrtAcumulada(oCtaImmobilitzat.Codi), oUser.Emp)

        oCca.AddCredit(oSaldo, oCtaImmobilitzat)
        oCca.AddSaldo(oCtaAmrtAcumulada)
        oAmortizationItem.Cca = oCca

        Dim retval As Boolean = AmortizationItemLoader.Update(oAmortizationItem, exs)
        Return retval
    End Function

    Shared Function Delete(oAmortizationItem As DTOAmortizationItem, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = AmortizationItemLoader.Delete(oAmortizationItem, exs)
        Return retval
    End Function

    Shared Function FromCca(value As DTOCca) As DTOAmortizationItem
        Dim retval As DTOAmortizationItem = AmortizacionItemLoader.FromCca(value)
        Return retval
    End Function

    Shared Function ConcepteAmortitzacio(oItem As DTOAmortizationItem) As String
        Dim oAmortization As DTOAmortization = oItem.Parent
        Dim oCtaImmobilitzat As DTOPgcCta = oAmortization.Cta
        Dim retval As String = "Cta." & oCtaImmobilitzat.Id & " " & oItem.Tipus & "% s/" & DTOAmt.CurFormatted(oAmortization.Amt) & "-" & oAmortization.Dsc
        retval = Left(retval, 60)
        Return retval
    End Function

End Class

Public Class AmortizationItems

    Shared Function LastItems(oExercici As DTOExercici) As List(Of DTOAmortizationItem)
        Dim oAll As List(Of DTOAmortization) = BEBL.Amortizations.All(oExercici.Emp).Where(Function(x) x.Items.Count > 0).ToList
        Dim DtFch As New Date(oExercici.Year, 12, 31)
        Dim retval As New List(Of DTOAmortizationItem)
        For Each oMrt As DTOAmortization In oAll
            Dim item As DTOAmortizationItem = oMrt.Items.Find(Function(x) x.Cod = DTOAmortizationItem.Cods.Amortitzacio And x.Fch = DtFch)
            If item IsNot Nothing Then
                retval.Add(item)
            End If
        Next
        Return retval
    End Function

End Class
