Public Class Xl_LookupConsumer
    Inherits Xl_LookupTextboxButton

    Private _User As DTOUser

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Shadows Property User() As DTOUser
        Get
            Return _User
        End Get
        Set(ByVal value As DTOUser)
            _User = value
            If _User Is Nothing Then
                MyBase.Text = ""
            Else
                MyBase.Text = _User.EmailAddress
            End If
        End Set
    End Property

    Public Sub Clear()
        Me.User = Nothing
    End Sub

    Private Sub Xl_LookupUser_onLookUpRequest(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.onLookUpRequest
        Dim oFrm As New Frm_Consumer(_User)
        AddHandler oFrm.afterupdate, AddressOf refresca
        oFrm.Show()
    End Sub

    Private Sub Refresca(ByVal sender As Object, ByVal e As MatEventArgs)
        _User = e.Argument
        MyBase.Text = _User.EmailAddress
        RaiseEvent AfterUpdate(Me, e)
    End Sub

End Class
