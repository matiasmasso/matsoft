

Public Class Frm_LiniaTelefon
    Private mLiniaTelefon As LiniaTelefon
    Private mAllowEvents As Boolean = False

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Sub New(ByVal oLiniaTelefon As LiniaTelefon)
        MyBase.new()
        Me.InitializeComponent()
        mLiniaTelefon = oLiniaTelefon
        Refresca()
        mAllowEvents = True
    End Sub

    Private Sub Refresca()
        With mLiniaTelefon
            TextBoxNum.Text = .Num
            TextBoxAlias.Text = .Alias
            Xl_Contact1.Contact = .Usuari
            DateTimePickerAlta.Value = .Alta

            If .Baixa <> Date.MinValue Then
                CheckBoxBaixa.Checked = True
                DateTimePickerBaixa.Visible = True
                DateTimePickerBaixa.Value = .Baixa
            End If

            CheckBoxPrivat.Checked = .Privat

            If .exists Then
                ButtonDel.Enabled = .allowdelete
            End If
        End With
    End Sub

    Private Sub ControlChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
        TextBoxNum.TextChanged, _
         TextBoxAlias.TextChanged, _
          Xl_Contact1.AfterUpdate, _
           DateTimePickerAlta.ValueChanged, _
            DateTimePickerBaixa.ValueChanged, _
             CheckBoxPrivat.CheckedChanged

        If mAllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub CheckBoxBaixa_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBoxBaixa.CheckedChanged
        If mAllowEvents Then
            DateTimePickerBaixa.Visible = CheckBoxBaixa.Checked
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        With mLiniaTelefon
            .Num = TextBoxNum.Text
            .Alias = TextBoxAlias.Text
            .Alta = DateTimePickerAlta.Value
            If CheckBoxBaixa.Checked Then
                .Baixa = DateTimePickerBaixa.Value
            Else
                .Baixa = Date.MinValue
            End If
            .Privat = CheckBoxPrivat.Checked
            .Update()
            RaiseEvent AfterUpdate(mLiniaTelefon, System.EventArgs.Empty)
            Me.Close()
        End With
    End Sub

    Private Sub ButtonDel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonDel.Click
        If mLiniaTelefon.allowDelete Then
            mLiniaTelefon.delete()
        End If
    End Sub


End Class