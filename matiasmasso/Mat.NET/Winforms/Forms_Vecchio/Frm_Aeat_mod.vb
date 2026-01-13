

Public Class Frm_Aeat_mod
    Private mAeat_mod As AEAT_Mod
    Private mAllowEvents As Boolean

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Public WriteOnly Property Aeat_mod() As AEAT_Mod
        Set(ByVal value As AEAT_Mod)
            mAeat_mod = value
            refresca()
            mallowevents = True
        End Set
    End Property

    Private Sub refresca()
        With mAeat_mod
            If .Exists Then
                Me.Text = "MODEL DE DECLARACIO A HISENDA"
                ButtonDel.Enabled = .AllowDelete
            Else
                Me.Text = "NOU MODEL DE DECLARACIO A HISENDA"
            End If
            TextBoxMod.Text = .Model
            ComboBoxTperiod.SelectedIndex = .TPeriod
            TextBoxDsc.Text = .Dsc
            CheckBoxSoloInfo.Checked = .SoloInfo
            CheckBoxPymes.Checked = .Pyme
            CheckBoxGranEmpresa.Checked = .GranEmpresa

        End With
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        With mAeat_mod
            .Model = TextBoxMod.Text
            .TPeriod = ComboBoxTperiod.SelectedIndex
            .Dsc = TextBoxDsc.Text
            .SoloInfo = CheckBoxSoloInfo.Checked
            .Pyme = CheckBoxPymes.Checked
            .GranEmpresa = CheckBoxGranEmpresa.Checked
            .Update()
        End With
        RaiseEvent AfterUpdate(mAeat_mod, e)
        Me.Close()
    End Sub

    Private Sub Control_Changed(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
     TextBoxMod.TextChanged, _
      ComboBoxTperiod.SelectedIndexChanged, _
       TextBoxDsc.TextChanged, _
        CheckBoxSoloInfo.CheckedChanged, _
         CheckBoxPymes.CheckedChanged, _
          CheckBoxGranEmpresa.CheckedChanged

        If mAllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub ButtonDel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonDel.Click
        If mAeat_mod.AllowDelete Then
            Dim rc As MsgBoxResult = MsgBox("eliminem aquest model?", MsgBoxStyle.OkCancel, "MAT.NET")
            If rc = MsgBoxResult.Ok Then
                mAeat_mod.Delete()
                RaiseEvent AfterUpdate(mAeat_mod, e)
                Me.Close()
            Else
                MsgBox("Operació cancelada per l'usuari", MsgBoxStyle.Information, "MAT.NET")
            End If
        Else
            MsgBox("Operació no autoritzada", MsgBoxStyle.Exclamation, "MAT.NET")
        End If
    End Sub
End Class