Public Class Xl_LookupUser
    Inherits Xl_LookupTextboxButton

    Private _User As DTOUser

    Public Event RequestToLookup(ByVal sender As Object, e As MatEventArgs)
    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Shadows ReadOnly Property User() As DTOUser
        Get
            Return _User
        End Get
    End Property

    Public Shadows Sub Load(oUser As DTOUser)
        _User = oUser
        refresca()
    End Sub

    Public Sub Clear()
        _User = Nothing
        refresca()
    End Sub

    Private Sub Xl_LookupUser_onLookUpRequest(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.onLookUpRequest
        RaiseEvent RequestToLookup(Me, New MatEventArgs(_User))
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
            MyBase.Text = _User.NicknameAndEmailAddress()
            Dim oMenu_User As New Menu_User(_User)
            AddHandler oMenu_User.AfterUpdate, AddressOf refresca
            MyBase.SetContextMenuRange(oMenu_User.Range)
        End If
    End Sub

    Private Sub Xl_LookupUser_Doubleclick(sender As Object, e As EventArgs) Handles Me.Doubleclick
        Dim oFrm As New Frm_Contact_Email(_User)
        AddHandler oFrm.AfterUpdate, AddressOf refresca
        oFrm.Show()
    End Sub


End Class



