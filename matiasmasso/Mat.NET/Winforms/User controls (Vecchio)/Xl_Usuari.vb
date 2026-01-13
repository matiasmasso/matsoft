Public Class Xl_Usuari
    Private _User As DTOUser

    Public ReadOnly Property Value As DTOUser
        Get
            Return _User
        End Get
    End Property

    Public Shadows Sub Load(value As DTOUser)
        Dim exs As New List(Of Exception)
        If value IsNot Nothing Then
            _User = value
            If FEB2.User.Load(exs, _User) Then
                refresca()
            Else
                UIHelper.WarnError(exs)
            End If
        End If
    End Sub

    Private Sub refresca()
        TextBox1.Text = _User.EmailAddress
        Dim oContextMenu As New ContextMenuStrip
        Dim oMenu As New Menu_User(_User)
        oContextMenu.Items.AddRange(oMenu.Range)
        TextBox1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub TextBox1_DoubleClick(sender As Object, e As EventArgs) Handles TextBox1.DoubleClick
        Dim oFrm As New Frm_User(_User)
        AddHandler oFrm.AfterUpdate, AddressOf refresca
        oFrm.Show()
    End Sub


End Class

