Public Class Xl_LookupPaymentGateway

    Inherits Xl_LookupTextboxButton

    Private _PaymentGateway As DTOPaymentGateway

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Shadows Property PaymentGateway() As DTOPaymentGateway
        Get
            Return _PaymentGateway
        End Get
        Set(ByVal value As DTOPaymentGateway)
            _PaymentGateway = value
            If _PaymentGateway Is Nothing Then
                MyBase.Text = ""
            Else
                MyBase.Text = _PaymentGateway.Nom
            End If
        End Set
    End Property

    Public Sub Clear()
        Me.PaymentGateway = Nothing
    End Sub

    Private Sub Xl_LookupPaymentGateway_onLookUpRequest(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.onLookUpRequest
        Dim oFrm As New Frm_PaymentGateways(_PaymentGateway, DTO.Defaults.SelectionModes.Selection)
        AddHandler oFrm.onItemSelected, AddressOf onPaymentGatewaySelected
        oFrm.Show()
    End Sub

    Private Sub onPaymentGatewaySelected(ByVal sender As Object, ByVal e As MatEventArgs)
        _PaymentGateway = e.Argument
        MyBase.Text = _PaymentGateway.Nom
        RaiseEvent AfterUpdate(Me, e)
    End Sub

    Private Sub Xl_LookupPaymentGateway_Doubleclick(sender As Object, e As EventArgs) Handles Me.Doubleclick
        Dim oFrm As New Frm_PaymentGateway(_PaymentGateway)
        AddHandler oFrm.AfterUpdate, AddressOf onAfterUpdate
        oFrm.Show()
    End Sub

    Private Sub OnAfterUpdate(sender As Object, e As MatEventArgs)
        _PaymentGateway = e.Argument
        If _PaymentGateway Is Nothing Then
            MyBase.Text = ""
        Else
            MyBase.Text = _PaymentGateway.Nom
        End If
    End Sub
End Class
