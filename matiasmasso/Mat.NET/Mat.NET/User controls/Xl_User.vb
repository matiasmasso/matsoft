Public Class Xl_User
    Inherits TextBox

    Private _User As DTOUser

    Public Sub New()
        MyBase.New()
        MyBase.ReadOnly = True

    End Sub

    Public Shadows Sub Load(oUser As DTOUser)
        _User = oUser
        refresca()
    End Sub

    Public ReadOnly Property Value As DTOUser
        Get
            Return _User
        End Get
    End Property

    Private Sub refresca()
        If _User Is Nothing Then
            MyBase.Text = ""
        Else
            MyBase.Text = BLL.BLLUser.NicknameOrElse(_User)
            Dim oContextMenu As New ContextMenuStrip
            Dim oMenu As New Menu_User(_User)
            oContextMenu.Items.AddRange(oMenu.Range)
            MyBase.ContextMenuStrip = oContextMenu
        End If
    End Sub

    Private Sub TextBox1_DoubleClick(sender As Object, e As EventArgs) Handles MyBase.DoubleClick
        Dim oFrm As New Frm_User(_User)
        AddHandler oFrm.AfterUpdate, AddressOf refresca
        oFrm.Show()
    End Sub
End Class
