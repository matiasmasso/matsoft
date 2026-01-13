Public Class Xl_LookupHolding
    Inherits Xl_LookupTextboxButton

    Private _Holding As DTOHolding

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Shadows Property Holding() As DTOHolding
        Get
            Return _Holding
        End Get
        Set(ByVal value As DTOHolding)
            _Holding = value
            If _Holding Is Nothing Then
                MyBase.Text = ""
            Else
                MyBase.Text = _Holding.Nom
            End If
        End Set
    End Property

    Public Sub Clear()
        Me.Holding = Nothing
    End Sub

    Private Sub Xl_LookupHolding_onLookUpRequest(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.onLookUpRequest
        Dim oFrm As New Frm_Holdings(_Holding, DTO.Defaults.SelectionModes.Selection)
        AddHandler oFrm.itemSelected, AddressOf onHoldingSelected
        oFrm.Show()
    End Sub

    Private Sub onHoldingSelected(ByVal sender As Object, ByVal e As MatEventArgs)
        _Holding = e.Argument
        MyBase.Text = _Holding.Nom
        RaiseEvent AfterUpdate(Me, e)
    End Sub
End Class
