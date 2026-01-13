Public Class Frm_PaymentGateways
    Private _DefaultValue As DTOPaymentGateway
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse

    Public Event onItemSelected(sender As Object, e As MatEventArgs)

    Public Sub New(Optional oDefaultValue As DTOPaymentGateway = Nothing, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse)
        MyBase.New()
        _DefaultValue = oDefaultValue
        _SelectionMode = oSelectionMode
        Me.InitializeComponent()
    End Sub

    Private Async Sub Frm_PaymentGateways_Load(sender As Object, e As EventArgs) Handles Me.Load
        Await refresca()
    End Sub

    Private Sub Xl_PaymentGateways1_onItemSelected(sender As Object, e As MatEventArgs) Handles Xl_PaymentGateways1.onItemSelected
        RaiseEvent onItemSelected(Me, e)
        Me.Close()
    End Sub

    Private Sub Xl_PaymentGateways1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_PaymentGateways1.RequestToAddNew
        Dim oPaymentGateway As New DTOPaymentGateway
        Dim oFrm As New Frm_PaymentGateway(oPaymentGateway)
        AddHandler oFrm.AfterUpdate, AddressOf refresca
        oFrm.Show()
    End Sub

    Private Async Sub Xl_PaymentGateways1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_PaymentGateways1.RequestToRefresh
        Await refresca()
    End Sub

    Private Async Sub refresca(sender As Object, e As MatEventArgs)
        Await refresca()
    End Sub

    Private Async Function refresca() As Task
        Dim exs As New List(Of Exception)
        Dim oPaymentGateways = Await FEB.PaymentGateways.All(exs)
        If exs.Count = 0 Then
            Xl_PaymentGateways1.Load(oPaymentGateways, _DefaultValue, _SelectionMode)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function

End Class