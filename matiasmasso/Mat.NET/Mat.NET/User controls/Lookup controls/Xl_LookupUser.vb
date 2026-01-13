Public Class Xl_LookupUser
    Inherits Xl_LookupTextboxButton

    Private _User As DTOUser

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Shadows Property User() As DTOUser
        Get
            Return _User
        End Get
        Set(ByVal value As DTOUser)
            _User = value
            refresca()
        End Set
    End Property

    Public Sub Clear()
        _User = Nothing
        refresca()
    End Sub

    Private Sub Xl_LookupUser_onLookUpRequest(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.onLookUpRequest
        If _User IsNot Nothing Then BLL.BLLUser.Load(_User)
        'Dim oFrm As New Frm_Users(_User)
        'AddHandler oFrm.onItemSelected, AddressOf onUserSelected
        'oFrm.Show()
    End Sub

    Private Sub onUserSelected(ByVal sender As Object, ByVal e As MatEventArgs)
        _User = e.Argument
        refresca()
        RaiseEvent AfterUpdate(Me, e)
    End Sub

    Private Sub refresca()
        If _User Is Nothing Then
            MyBase.Text = ""
            MyBase.ClearContextMenu()
        Else
            MyBase.Text = _User.Nom
            'Dim oMenu_User As New Menu_User(_User)
            'AddHandler oMenu_User.AfterUpdate, AddressOf refresca
            'MyBase.SetContextMenuRange(oMenu_User.Range)
        End If
    End Sub

End Class



