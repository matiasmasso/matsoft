

Public Class Frm_Csa_NewAndorra
    Private mBanc As Banc  'la caixa

    Private Sub Frm_Csa_NewAndorra_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim oCountry As Country = Country.FromEnum(Country.Countries.Andorra)
        Xl_Gir_SelEfts1.LoadData(oCountry, False)
        mBanc = MaxiSrvr.Banc.FromNum(BLL.BLLApp.Emp, LaCaixa.Id) 'la caixa
    End Sub

    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        Dim oCsbs As csbs = Xl_Gir_SelEfts1.Csbs
        Dim oCsa As New MaxiSrvr.Csa(mBanc, DTOCsa.FileFormats.NormaAndorrana, DTOCsa.Types.AlCobro)
        With oCsa
            .Andorra = True
            .Banc = mBanc
            .descomptat = False
            .fch = Today
            .csbs = oCsbs

            Dim exs as New List(Of exception)
            If .Update( exs) Then
                root.SaveCsaFile(oCsa)
                Me.Close()
            Else
                MsgBox("error" & vbCrLf & BLL.Defaults.ExsToMultiline(exs))
            End If

        End With

    End Sub
End Class