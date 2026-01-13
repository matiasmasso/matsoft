Public Class Frm_Girs2
    Private _BancTerms As List(Of DTOBancTerm)
    Private _AllowEvents As Boolean

    Private Enum Tabs
        Efectes
        Bancs
    End Enum

    Private Sub Frm_Girs_Load(sender As Object, e As EventArgs) Handles Me.Load
        DateTimePicker1.Value = Today

        Dim exs As New List(Of Exception)
        Dim oCountry As DTOCountry = BLL.BLLCountry.Find("ES")
        Dim oCsbs As List(Of DTO.DTOCsb) = BLL.BLLCsbs.PendentsDeGirar(exs, oCountry)

        If exs.Count > 0 Then
            UIHelper.WarnError(exs, "errors trobats al generar les remeses:")
        End If

        Xl_AmtDisponible.Value = BLL.BLLCsbs.TotalNominal(oCsbs)

        Dim oExerci As DTOExercici = BLL.BLLApp.CurrentExercici
        Dim oCta As DTOPgcCta = BLL.BLLPgcCta.FromCod(DTOPgcPlan.Ctas.Clients, oExerci)
        Dim oDescuadres As List(Of DTOCcd) = BLL.BLLCcds.Descuadres(oCta)
        Xl_Gir_SelEfts21.Load(oCsbs, oDescuadres)

        _BancTerms = BLL_BancTerms.Active()
        BLL_BancTerms.SetEuribor(_BancTerms, Xl_PercentEuribor.Value)

        Dim oCsas As List(Of DTOCsa) = BLL.BLLCsas.NewFrom(_BancTerms)
        Xl_Gir_SelBancs21.Load(oCsas)
        Xl_AmtDespeses.Value = Xl_Gir_SelBancs21.Despeses

        _AllowEvents = True
    End Sub

    Private Sub Xl_AmtSelected_AfterUpdate(sender As Object, e As EventArgs) Handles Xl_AmtSelected.AfterUpdate
        Xl_Gir_SelEfts21.SetAmt(Xl_AmtSelected.Value)
        Xl_AmtDespeses.Value = Xl_Gir_SelBancs21.Despeses
    End Sub

    Private Sub Xl_Gir_SelEfts21_ValueChanged(sender As Object, e As MatEventArgs) Handles Xl_Gir_SelEfts21.ValueChanged
        Dim oCsbs As List(Of DTO.DTOCsb) = Xl_Gir_SelEfts21.Values
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
        Dim oCsbs As List(Of DTO.DTOCsb) = Xl_Gir_SelEfts21.Values
        BLL.BLLCsas.SetCsbs(oCsas, oCsbs)
        Xl_Gir_SelBancs21.Load(oCsas)
        Xl_AmtDespeses.Value = Xl_Gir_SelBancs21.Despeses
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        Dim oCsas As List(Of DTOCsa) = Xl_Gir_SelBancs21.NonEmptyCsas
        BLL.BLLCsas.SetCcasDescompte(oCsas, Session.User)

        Dim exs As New List(Of Exception)
        If BLL.BLLCsas.Update(oCsas, exs) Then
            MsgBox("Done!")
            Me.Close()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub ButtonRefresca_Click(sender As Object, e As EventArgs) Handles ButtonRefresca.Click
        Dim oCsas As List(Of DTOCsa) = Xl_Gir_SelBancs21.Csas
        Dim oCsbs As List(Of DTO.DTOCsb) = Xl_Gir_SelEfts21.Values
        BLL.BLLCsas.SetCsbs(oCsas, oCsbs)
        Xl_Gir_SelBancs21.refresca()
        Xl_AmtDespeses.Value = Xl_Gir_SelBancs21.Despeses
    End Sub


    Private Sub Xl_PercentEuribor_AfterUpdate(sender As Object, e As EventArgs) Handles Xl_PercentEuribor.AfterUpdate
        If _AllowEvents Then
            BLL_BancTerms.SetEuribor(_BancTerms, Xl_PercentEuribor.Value)

            Dim oCsas As List(Of DTOCsa) = BLL.BLLCsas.NewFrom(_BancTerms)
            Dim oCsbs As List(Of DTO.DTOCsb) = Xl_Gir_SelEfts21.Values
            BLL.BLLCsas.SetCsbs(oCsas, oCsbs)
            Xl_Gir_SelBancs21.refresca()
            Xl_AmtDespeses.Value = Xl_Gir_SelBancs21.Despeses
        End If
    End Sub
End Class