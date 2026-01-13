Public Class Xl_LookupVisaOrg
    Inherits Xl_LookupTextboxButton

    Private _VisaOrg As DTOVisaEmisor

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Shadows Property VisaOrg() As DTOVisaEmisor
        Get
            Return _VisaOrg
        End Get
        Set(ByVal value As DTOVisaEmisor)
            _VisaOrg = value
            If _VisaOrg Is Nothing Then
                MyBase.Text = ""
            Else
                MyBase.Text = _VisaOrg.Nom
            End If
        End Set
    End Property

    Public Sub Clear()
        Me.VisaOrg = Nothing
    End Sub

    Private Sub Xl_LookupVisaOrg_onLookUpRequest(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.onLookUpRequest
        Dim oFrm As New Frm_VisaOrgs(_VisaOrg, DTO.Defaults.SelectionModes.Selection)
        AddHandler oFrm.onItemSelected, AddressOf onVisaOrgSelected
        oFrm.Show()
    End Sub

    Private Sub onVisaOrgSelected(ByVal sender As Object, ByVal e As MatEventArgs)
        _VisaOrg = e.Argument
        MyBase.Text = _VisaOrg.Nom
        RaiseEvent AfterUpdate(Me, e)
    End Sub

End Class
