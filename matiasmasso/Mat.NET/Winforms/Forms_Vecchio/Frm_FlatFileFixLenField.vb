

Public Class Frm_FlatFileFixLenField
    Private mField As MaxiSrvr.FlatFileFixLen_Field
    Private mAllowEvents As Boolean = False

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Sub New(ByVal oField As MaxiSrvr.FlatFileFixLen_Field)
        MyBase.new()
        Me.InitializeComponent()
        mField = oField
        Me.Text = "diseny de camp " & oField.Reg.File.Id.ToString & oField.Reg.id & "/" & oField.Lin
        UIHelper.LoadComboFromEnum(ComboBoxOpcional, GetType(MaxiSrvr.FlatFileFixLen.CampsOpcionals), 0)
        UIHelper.LoadComboFromEnum(ComboBoxFormat, GetType(DTO.DTOFlatField.Formats), 0)
        Refresca()
        mAllowEvents = True
    End Sub

    Private Sub Refresca()
        With mField
            Xl_TextBoxNumLin.Value = .Lin
            TextBoxNom.Text = .Nom
            Xl_TextBoxNumLen.Value = .len
            Xl_TextBoxNumPosFrom.Value = .GetPosFrom
            Xl_TextBoxNumPosTo.Value = Xl_TextBoxNumPosFrom.Value + Xl_TextBoxNumLen.Value
            ComboBoxOpcional.SelectedValue = .Opcional
            ComboBoxFormat.SelectedValue = .Format
            TextBoxDefaultValue.Text = .Default
            TextBoxRegex.Text = .Regex
            TextBoxObs.Text = .Obs
            If .Exists Then
                ButtonDel.Enabled = .AllowDelete
            End If
        End With
    End Sub

    Private Sub ControlChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
         TextBoxNom.TextChanged, _
          ComboBoxOpcional.SelectedIndexChanged, _
           ComboBoxFormat.SelectedIndexChanged, _
            TextBoxDefaultValue.TextChanged, _
             TextBoxRegex.TextChanged, _
              TextBoxObs.TextChanged

        If mAllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        With mField
            .Nom = TextBoxNom.Text
            .len = Xl_TextBoxNumLen.Value
            .Opcional = ComboBoxOpcional.SelectedValue
            .Format = ComboBoxFormat.SelectedValue
            .Default = TextBoxDefaultValue.Text
            .Regex = TextBoxRegex.Text
            .Obs = TextBoxObs.Text
            .Update()
            RaiseEvent AfterUpdate(mField, System.EventArgs.Empty)
            Me.Close()
        End With
    End Sub

    Private Sub ButtonDel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonDel.Click
        If mField.AllowDelete Then
            mField.Delete()
            Me.Close()
        End If
    End Sub

    Private Sub Xl_TextBoxNumLin_AfterUpdate(sender As Object, e As System.EventArgs) Handles Xl_TextBoxNumLin.AfterUpdate
        If mAllowEvents Then
            mAllowEvents = False
            Dim iLin As Integer = Xl_TextBoxNumLin.Value
            mField = New MaxiSrvr.FlatFileFixLen_Field(mField.Reg, iLin)
            Refresca()
            mAllowEvents = True
        End If
    End Sub

    Private Sub Xl_TextBoxNumLen_AfterUpdate(sender As Object, e As System.EventArgs) Handles Xl_TextBoxNumLen.AfterUpdate
        If mAllowEvents Then
            Xl_TextBoxNumPosTo.Value = Xl_TextBoxNumPosFrom.Value + Xl_TextBoxNumLen.Value - 1
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub Xl_TextBoxNumPosTo_AfterUpdate(sender As Object, e As System.EventArgs) Handles Xl_TextBoxNumPosTo.AfterUpdate
        If mAllowEvents Then
            Xl_TextBoxNumLen.Value = Xl_TextBoxNumPosTo.Value - Xl_TextBoxNumPosFrom.Value + 1
            ButtonOk.Enabled = True
        End If
    End Sub


End Class