

Public Class Frm_FlatFileFixLenReg
    Private mReg As FlatFileFixLen_Reg
    Private mAllowEvents As Boolean = False

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Sub New(ByVal oReg As FlatFileFixLen_Reg)
        MyBase.new()
        Me.InitializeComponent()
        mReg = oReg
        Me.Text = "diseny de registre de fitxer " & mReg.File.Id
        Refresca()
        mAllowEvents = True
    End Sub

    Private Sub Refresca()
        With mReg
            TextBoxId.MaxLength = .File.RegIdLen
            TextBoxId.Text = .id
            TextBoxNom.Text = .Nom
            TextBoxRegex.Text = .Regex
            If .Exists Then
                ButtonDel.Enabled = .AllowDelete
            End If
        End With
    End Sub

    Private Sub ControlChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
         TextBoxNom.TextChanged, _
          TextBoxRegex.TextChanged

        If mAllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        With mReg
            .Nom = TextBoxNom.Text
            .Regex = TextBoxRegex.Text
            .Update()
            RaiseEvent AfterUpdate(mReg, System.EventArgs.Empty)
            Me.Close()
        End With
    End Sub

    Private Sub ButtonDel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonDel.Click
        If mReg.allowDelete Then
            mReg.delete()
            Me.Close()
        End If
    End Sub

    Private Sub TextBoxId_TextChanged(sender As Object, e As System.EventArgs) Handles TextBoxId.TextChanged
        If mAllowEvents Then
            mAllowEvents = False
            Dim sId As String = TextBoxId.Text
            mReg = New FlatFileFixLen_Reg(mReg.File, sId)
            Refresca()
            mAllowEvents = True
        End If
    End Sub
End Class