Public Class Frm_Girs2
    Private _BancTerms As List(Of DTOBancTerm)
    Private _AllowEvents As Boolean

    Private Enum Tabs
        Efectes
        Bancs
    End Enum

    Private Async Sub Frm_Girs_Load(sender As Object, e As EventArgs) Handles Me.Load
        DateTimePicker1.Value = Today

        Dim exs As New List(Of Exception)
        Dim oCountry = DTOCountry.WellKnown(DTOCountry.WellKnowns.Spain)
        Dim oCsbs = Await FEBL.Csbs.PendentsDeGirar(exs, Current.Session.Emp, oCountry, sepa:=True)

        If exs.Count = 0 Then
            Xl_AmtDisponible.Value = BLL.BLLCsbs.TotalNominal(oCsbs)

            Dim oExercici As DTOExercici = DTOExercici.Current(Current.Session.Emp)
            Dim oCta As DTOPgcCta = BLL.BLLPgcCta.FromCod(DTOPgcPlan.Ctas.Clients, oExercici)
            Dim oDescuadres As List(Of DTOCcd) = BLL.BLLCcds.Descuadres(oExercici, oCta)
            Xl_Gir_SelEfts21.Load(oCsbs, oDescuadres)

            _BancTerms = BLLBancTerms.Active(Current.Session.Emp)
            BLLBancTerms.SetEuribor(_BancTerms, Xl_PercentEuribor.Value)

            Dim oCsas As List(Of DTOCsa) = BLL.BLLCsas.NewFrom(Current.Session.Emp, _BancTerms)
            Xl_Gir_SelBancs21.Load(oCsas)
            Xl_AmtDespeses.Value = Xl_Gir_SelBancs21.Despeses

            _AllowEvents = True
        Else
            UIHelper.WarnError(exs, "errors trobats al generar les remeses:")

        End If

    End Sub

    Private Sub Xl_AmtSelected_AfterUpdate(sender As Object, e As EventArgs) Handles Xl_AmtSelected.AfterUpdate
        Xl_Gir_SelEfts21.SetAmt(Xl_AmtSelected.Value)
        Xl_AmtDespeses.Value = Xl_Gir_SelBancs21.Despeses
    End Sub

    Private Sub Xl_Gir_SelEfts21_ValueChanged(sender As Object, e As MatEventArgs) Handles Xl_Gir_SelEfts21.ValueChanged
        Dim oCsbs As List(Of DTOCsb) = Xl_Gir_SelEfts21.Values
        Dim oNominal As DTOAmt = BLL.BLLCsbs.TotalNominal(oCsbs)
        Xl_AmtSelected.Value = oNominal

        Dim oCsas As List(Of DTOCsa) = Xl_Gir_SelBancs21.Csas
        BLL.BLLCsas.SetCsbs(oCsas, oCsbs)
        Xl_Gir_SelBancs21.Load(oCsas)
        Xl_AmtDespeses.Value = Xl_Gir_SelBancs21.Despeses

        'Xl_Gir_SelBancs21.Refresca()
        ButtonOk.Enabled = (oNominal.Eur > 0)
    End Sub

    Private Sub Xl_Gir_SelBancs21_ValueChanged(sender As Object, e As MatEventArgs) Handles Xl_Gir_SelBancs21.ValueChanged
        Dim oCsas As List(Of DTOCsa) = Xl_Gir_SelBancs21.Csas
        Dim oCsbs As List(Of DTOCsb) = Xl_Gir_SelEfts21.Values
        BLL.BLLCsas.SetCsbs(oCsas, oCsbs)
        Xl_Gir_SelBancs21.Load(oCsas)
        Xl_AmtDespeses.Value = Xl_Gir_SelBancs21.Despeses
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        Dim oCsas As List(Of DTOCsa) = Xl_Gir_SelBancs21.NonEmptyCsas
        BLL.BLLCsas.SetCcasDescompte(oCsas, Current.Session.User)

        Dim exs As New List(Of Exception)
        If Await FEBL.Csas.Update(oCsas, exs) Then
            MsgBox("Done!")
            Me.Close()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub ButtonRefresca_Click(sender As Object, e As EventArgs) Handles ButtonRefresca.Click
        Dim oCsas As List(Of DTOCsa) = Xl_Gir_SelBancs21.Csas
        Dim oCsbs As List(Of DTOCsb) = Xl_Gir_SelEfts21.Values
        BLL.BLLCsas.SetCsbs(oCsas, oCsbs)
        Xl_Gir_SelBancs21.refresca()
        Xl_AmtDespeses.Value = Xl_Gir_SelBancs21.Despeses
    End Sub


    Private Sub Xl_PercentEuribor_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_PercentEuribor.AfterUpdate
        If _AllowEvents Then
            BLLBancTerms.SetEuribor(_BancTerms, Xl_PercentEuribor.Value)

            Dim oCsas As List(Of DTOCsa) = BLLCsas.NewFrom(Current.Session.Emp, _BancTerms)
            Dim oCsbs As List(Of DTOCsb) = Xl_Gir_SelEfts21.Values
            BLL.BLLCsas.SetCsbs(oCsas, oCsbs)
            Xl_Gir_SelBancs21.refresca()
            Xl_AmtDespeses.Value = Xl_Gir_SelBancs21.Despeses
        End If
    End Sub
End Class