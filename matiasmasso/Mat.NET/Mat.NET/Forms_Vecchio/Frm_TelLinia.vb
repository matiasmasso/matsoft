

Public Class Frm_TelLinia
    Private mLinia As TelLinia
    Private mAllowEvents As Boolean = False
    Private mGrupsDeResposta As TelGrupsDeResposta = Nothing
    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Sub New(ByVal oLinia As TelLinia)
        MyBase.new()
        Me.InitializeComponent()
        mLinia = oLinia
        'Me.Text = mLinia.ToString
        LoadGrupsDeResposta()
        Refresca()

        mAllowEvents = True
    End Sub

    Private Sub Refresca()
        With mLinia
            'TextBoxNom.Text = .text
            If .Exists Then
                TextBoxNum.Text = .Id
                TextBoxObs.Text = .Obs
                If .Alta = Date.MinValue Then
                    DateTimePickerAlta.Value = DateTimePicker.MinimumDateTime
                Else
                    DateTimePickerAlta.Value = .Alta
                End If
                If .Baixa > DateTimePicker.MinimumDateTime Then
                    CheckBoxBaixa.Checked = True
                    DateTimePickerBaixa.Value = .Baixa
                    DateTimePickerBaixa.Visible = True
                End If
                If Not .GrupDeResposta Is Nothing Then
                    CheckBoxCentraleta.Checked = True
                    ComboBoxGrupDeResposta.Enabled = True
                    For i As Integer = 0 To mGrupsDeResposta.Count - 1

                        If mGrupsDeResposta(i).Guid = .GrupDeResposta.Guid Then
                            ComboBoxGrupDeResposta.SelectedIndex = i
                            Exit For
                        End If

                    Next
                End If
                ButtonDel.Enabled = .AllowDelete
            End If
        End With

        CheckBoxPrivat.Visible = BLL.BLLSession.Current.Contact.Rol.IsAdmin
    End Sub

    Private Sub LoadGrupsDeResposta()
        mGrupsDeResposta = TelGrupsDeResposta.Actives
        For Each oGrup As TelGrupDeResposta In mGrupsDeResposta
            ComboBoxGrupDeResposta.Items.Add(oGrup.Nom)
        Next
    End Sub

    Private Sub ControlChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
        TextBoxNum.TextChanged, _
         TextBoxObs.TextChanged, _
          DateTimePickerAlta.ValueChanged, _
           DateTimePickerBaixa.ValueChanged, _
            CheckBoxPrivat.CheckedChanged,
              ComboBoxGrupDeResposta.SelectedIndexChanged

        If mAllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        If mLinia.Id = "" Then
            mLinia = New TelLinia(TextBoxNum.Text)
        End If
        With mLinia
            .Obs = TextBoxObs.Text
            .Alta = DateTimePickerAlta.Value
            If CheckBoxBaixa.Checked Then
                .Baixa = DateTimePickerBaixa.Value
            Else
                .Baixa = DateTime.MinValue
            End If
            .Privat = CheckBoxPrivat.Checked
            If CheckBoxCentraleta.Checked Then
                .GrupDeResposta = mGrupsDeResposta(ComboBoxGrupDeResposta.SelectedIndex)
            Else
                .GrupDeResposta = Nothing
            End If
            .Update()
            RaiseEvent AfterUpdate(mLinia, System.EventArgs.Empty)
            Me.Close()
        End With
    End Sub

    Private Sub ButtonDel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonDel.Click
        If mLinia.AllowDelete Then
            mLinia.Delete()
        End If
    End Sub

    Private Sub CheckBoxBaixa_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBoxBaixa.CheckedChanged
        DateTimePickerBaixa.Visible = CheckBoxBaixa.Checked
        ButtonOk.Enabled = True
    End Sub

    Private Sub CheckBoxCentraleta_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBoxCentraleta.CheckedChanged
        If mAllowEvents Then
            ComboBoxGrupDeResposta.Enabled = CheckBoxCentraleta.Checked
            ButtonOk.Enabled = True
        End If
    End Sub
End Class