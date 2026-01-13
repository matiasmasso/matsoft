Public Class Xl_LookupBanc
    Inherits Xl_LookupTextboxButton

    Private _Banc As DTOBanc

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Shadows Property Banc() As DTOBanc
        Get
            Return _Banc
        End Get
        Set(ByVal value As DTOBanc)
            _Banc = value
            If _Banc Is Nothing Then
                MyBase.Text = ""
            Else
                MyBase.Text = _Banc.Nom
            End If
        End Set
    End Property

    Public Sub Clear()
        Me.Banc = Nothing
    End Sub

    Private Sub Xl_LookupBanc_onLookUpRequest(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.onLookUpRequest
        Dim oFrm As New Frm_Bancs(_Banc, BLL.Defaults.SelectionModes.Selection)
        AddHandler oFrm.onItemSelected, AddressOf onBancSelected
        oFrm.Show()
    End Sub

    Private Sub onBancSelected(ByVal sender As Object, ByVal e As MatEventArgs)
        _Banc = e.Argument
        MyBase.Text = _Banc.Abr
        RaiseEvent AfterUpdate(Me, e)
    End Sub
End Class
