Public Class Xl_LookupRol
    Inherits Xl_LookupTextboxButton
    Private _Rol As DTORol

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Shadows Property Rol() As DTORol
        Get
            Return _Rol
        End Get
        Set(ByVal value As DTORol)
            _Rol = value
            If _Rol Is Nothing Then
                MyBase.Text = ""
            Else
                Dim exs As New List(Of Exception)
                If FEB2.Rol.Load(_Rol, exs) Then
                    MyBase.Text = _Rol.Nom.Tradueix(Current.Session.User.Lang)
                Else
                    UIHelper.WarnError(exs)
                End If
            End If
        End Set
    End Property

    Public Sub Clear()
        Me.Rol = Nothing
    End Sub

    Private Sub Xl_LookupRol_onLookUpRequest(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.onLookUpRequest
        If _Rol IsNot Nothing Then
            Dim exs As New List(Of Exception)
            If Not FEB2.Rol.Load(_Rol, exs) Then
                UIHelper.WarnError(exs)
            End If
        End If
        Dim oFrm As New Frm_Rols(_Rol, DTO.Defaults.SelectionModes.Selection)
        AddHandler oFrm.onItemSelected, AddressOf onRolSelected
        oFrm.Show()
    End Sub

    Private Sub onRolSelected(ByVal sender As Object, ByVal e As MatEventArgs)
        _Rol = e.Argument
        MyBase.Text = _Rol.Nom.Tradueix(Current.Session.User.Lang)
        RaiseEvent AfterUpdate(Me, e)
    End Sub

End Class
