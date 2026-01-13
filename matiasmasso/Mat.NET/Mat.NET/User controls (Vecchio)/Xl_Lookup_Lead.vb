Public Class Xl_Lookup_Lead
    Inherits Xl_LookupTextboxButton

    Private _Usuari As DTOUsuari

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Property Usuari() As DTOUsuari
        Get
            Return _Usuari
        End Get
        Set(ByVal value As DTOUsuari)
            _Usuari = value
            If _Usuari Is Nothing Then
                MyBase.Text = ""
            Else
                MyBase.Text = _Usuari.EmailAddress
            End If
        End Set
    End Property

    Public Sub Clear()
        Me.Usuari = Nothing
    End Sub

    Private Sub Xl_LookupUsuari_onLookUpRequest(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.onLookUpRequest
        Dim oFrm As New Frm_Leads(bll.dEFAULTS.SelectionModes.Selection)
        AddHandler oFrm.onItemSelected, AddressOf onUsuariSelected
        oFrm.Show()
    End Sub

    Private Sub onUsuariSelected(ByVal sender As Object, ByVal e As MatEventArgs)
        _Usuari = e.Argument
        MyBase.Text = _Usuari.EmailAddress
        RaiseEvent AfterUpdate(Me, e)
    End Sub

End Class
