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
                BLL.BLLRol.Load(_Rol)
                MyBase.Text = BLL.BLLRol.Nom(_Rol, BLL.BLLSession.Current.User.Lang)
            End If
        End Set
    End Property

    Public Sub Clear()
        Me.Rol = Nothing
    End Sub

    Private Sub Xl_LookupRol_onLookUpRequest(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.onLookUpRequest
        If _Rol IsNot Nothing Then BLL.BLLRol.Load(_Rol)
        Dim oFrm As New Frm_Rols(BLL.Defaults.SelectionModes.Selection)
        AddHandler oFrm.onItemSelected, AddressOf onRolSelected
        oFrm.Show()
    End Sub

    Private Sub onRolSelected(ByVal sender As Object, ByVal e As MatEventArgs)
        _Rol = CType(e.Argument, List(Of DTORol))(0)
        MyBase.Text = BLL.BLLRol.Nom(_Rol, BLL.BLLSession.Current.User.Lang)
        RaiseEvent AfterUpdate(Me, e)
    End Sub

End Class
