Public Class Xl_LookupPdcSrc
    Inherits Xl_LookupTextboxButton

    Private _Value = DTOPurchaseOrder.Sources.no_Especificado

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Shadows Property Value() As DTOPurchaseOrder.Sources
        Get
            Return _Value
        End Get
        Set(ByVal Value As DTOPurchaseOrder.Sources)
            _Value = Value
            refresca()
        End Set
    End Property

    Public Sub Clear()
        _Value = DTOPurchaseOrder.Sources.no_Especificado
        refresca()
    End Sub

    Private Sub Xl_LookupUser_onLookUpRequest(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.onLookUpRequest
        Dim oFrm As New Frm_PdcSrcs(_Value, Defaults.SelectionModes.selection)
        AddHandler oFrm.ItemSelected, AddressOf onUserSelected
        oFrm.Show()
    End Sub

    Private Sub onUserSelected(ByVal sender As Object, ByVal e As MatEventArgs)
        _Value = e.Argument
        refresca()
        RaiseEvent AfterUpdate(Me, e)
    End Sub

    Private Sub refresca()
        If _Value = DTOPurchaseOrder.Sources.no_Especificado Then
            MyBase.Text = ""
        Else
            MyBase.Text = _Value.ToString.Replace("_", " ")
        End If
    End Sub
End Class