Public Class Xl_LookupRepKpi

    Inherits Xl_LookupTextboxButton

    Private _RepKpi As DTORepKpi

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Shadows Property RepKpi() As DTORepKpi
        Get
            Return _RepKpi
        End Get
        Set(ByVal value As DTORepKpi)
            _RepKpi = value
            If _RepKpi Is Nothing Then
                MyBase.Text = ""
            Else
                MyBase.Text = _RepKpi.Nom.Tradueix(Current.Session.Lang)
            End If
        End Set
    End Property

    Public Sub Clear()
        Me.RepKpi = Nothing
    End Sub

    Private Sub Xl_LookupRepKpi_onLookUpRequest(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.onLookUpRequest
        Dim oFrm As New Frm_RepKpis(_RepKpi, DTO.Defaults.SelectionModes.Selection)
        AddHandler oFrm.itemSelected, AddressOf onRepKpiSelected
        oFrm.Show()
    End Sub

    Private Sub onRepKpiSelected(ByVal sender As Object, ByVal e As MatEventArgs)
        _RepKpi = e.Argument
        MyBase.Text = _RepKpi.Nom.Tradueix(Current.Session.Lang)
        RaiseEvent AfterUpdate(Me, e)
    End Sub

End Class

