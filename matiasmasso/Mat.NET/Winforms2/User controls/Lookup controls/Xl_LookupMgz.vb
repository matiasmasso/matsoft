Public Class Xl_LookupMgz
    Inherits Xl_LookupTextboxButton

    Private _Mgz As DTOMgz

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Shadows Property Mgz() As DTOMgz
        Get
            Return _Mgz
        End Get
        Set(ByVal value As DTOMgz)
            _Mgz = value
            If _Mgz Is Nothing Then
                MyBase.Text = ""
            Else
                MyBase.Text = _Mgz.Nom
            End If
        End Set
    End Property

    Public Sub Clear()
        Me.Mgz = Nothing
    End Sub

    Private Sub Xl_LookupMgz_onLookUpRequest(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.onLookUpRequest
        Dim oFrm As New Frm_Mgzs(_Mgz, DTO.Defaults.SelectionModes.Selection)
        AddHandler oFrm.onItemSelected, AddressOf onMgzSelected
        oFrm.Show()
    End Sub

    Private Sub onMgzSelected(ByVal sender As Object, ByVal e As MatEventArgs)
        _Mgz = e.Argument
        MyBase.Text = DTOMgz.AbrOrNom(_Mgz)
        RaiseEvent AfterUpdate(Me, e)
    End Sub
End Class
