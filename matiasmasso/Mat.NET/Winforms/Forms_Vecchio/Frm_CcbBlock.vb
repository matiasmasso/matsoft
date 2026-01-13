

Public Class Frm_CcbBlock

    Private mCcbBlock As CcbBlock
    Private mLastBlockedCcaYea As Integer

    Public WriteOnly Property CcbBlock() As CcbBlock
        Set(ByVal value As CcbBlock)
            mCcbBlock = value
            With mCcbBlock
                mLastBlockedCcaYea = BLL.BLLDefault.EmpValue(DTODefault.Codis.LastBlockedCcaYea)
                If mLastBlockedCcaYea = 0 Then mLastBlockedCcaYea = Today.Year - 3
                DateTimePicker1.MinDate = "1/1/" & mLastBlockedCcaYea
                TextBoxCta.Text = .Ccd.Cta.FullNom
                TextBoxContacte.Text = .Ccd.Contact.Clx
                If .Fch.Year > mLastBlockedCcaYea Then
                    CheckBoxBlock.Checked = .Blocked
                    DateTimePicker1.Value = .Fch
                    SetOptions()
                End If
            End With
        End Set
    End Property


    Private Sub CheckBoxBlock_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBoxBlock.CheckedChanged
        GroupBoxBlockOptions.Visible = CheckBoxBlock.Checked
        EnableButtons()
    End Sub

    Private Sub SetOptions()
        Dim DtFch As Date = DateTimePicker1.Value
        If DtFch.Month = 12 And DtFch.Day = 31 Then
            RadioButtonFullYea.Checked = True
        Else
            RadioButtonFch.Checked = True
        End If
    End Sub

    Private Sub RadioButtonFch_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
       RadioButtonFullYea.CheckedChanged, _
        RadioButtonFch.CheckedChanged

        If mCcbBlock IsNot Nothing Then
            If RadioButtonFullYea.Checked Then
                Dim iYea As Integer = DateTimePicker1.Value.Year
                Dim sFch As String = "31/12/" & iYea.ToString
                DateTimePicker1.Value = sFch
                DateTimePicker1.Enabled = False
            Else
                DateTimePicker1.Enabled = True
                If mCcbBlock.Fch < CDate("1/1/" & mLastBlockedCcaYea + 1) Then
                    DateTimePicker1.Value = CDate("1/1/" & mLastBlockedCcaYea + 1)
                Else
                    DateTimePicker1.Value = mCcbBlock.Fch
                End If
            End If
            EnableButtons()

        End If
    End Sub

    Private Sub ControlChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
       DateTimePicker1.ValueChanged
        EnableButtons()
    End Sub

    Private Sub EnableButtons()
        Select Case BLL.BLLSession.Current.User.Rol.id
            Case DTORol.Ids.SuperUser, DTORol.Ids.Admin
                ButtonOk.Enabled = True
            Case DTORol.Ids.Accounts
                If DateTimePicker1.Value >= mCcbBlock.Fch Then
                    ButtonOk.Enabled = True
                Else
                    ButtonOk.Enabled = False
                End If
                If mCcbBlock.Blocked And Not CheckBoxBlock.Checked Then
                    ButtonOk.Enabled = False
                End If
        End Select
    End Sub

    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        With mCcbBlock
            .Blocked = CheckBoxBlock.Checked
            .Fch = DateTimePicker1.Value
            .Update()
        End With
        Me.Close()
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub
End Class