

Public Class Frm_QrCampaign
    Private mQrCampaign As QrCampaign
    Private mAllowEvents As Boolean = False

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Sub New(ByVal oQrCampaign As QrCampaign)
        MyBase.new()
        Me.InitializeComponent()
        mQrCampaign = oQrCampaign
        'Me.Text = mQrCampaign.ToString
        Refresca()
        mAllowEvents = True
    End Sub

    Private Sub Refresca()
        With mQrCampaign
            TextBoxNom.Text = .Nom
            DateTimePicker1.Value = .Fch
            Xl_LookupYoutube1.YouTube = .YouTube
            Xl_Qr1.Value = .QR
            CheckBoxObsoleto.Checked = .obsoleto
        End With
    End Sub

    Private Sub ControlChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
        TextBoxNom.TextChanged, _
         DateTimePicker1.ValueChanged, _
          Xl_LookupYoutube1.AfterUpdate, _
           CheckBoxObsoleto.CheckedChanged

        If mAllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        With mQrCampaign
            .Nom = TextBoxNom.Text
            .Fch = DateTimePicker1.Value
            .YouTube = Xl_LookupYoutube1.YouTube
            .Obsoleto = CheckBoxObsoleto.Checked
            .Update()
            RaiseEvent AfterUpdate(mQrCampaign, System.EventArgs.Empty)
            Me.Close()
        End With
    End Sub

    Private Sub ButtonDel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonDel.Click
        If mQrCampaign.AllowDelete Then
            mQrCampaign.Delete()
            Me.Close()
        End If
    End Sub
End Class