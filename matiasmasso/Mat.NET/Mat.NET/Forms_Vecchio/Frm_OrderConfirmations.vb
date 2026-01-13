Public Class Frm_OrderConfirmations
    Private _Proveidor As Proveidor

    Public Sub New(value As Proveidor)
        MyBase.New()
        Me.InitializeComponent()
        _Proveidor = value
        Xl_OrderConfirmations1.Load(OrderConfirmationsLoader.FromProveidor(_Proveidor))
    End Sub

    Private Sub Xl_OrderConfirmations1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_OrderConfirmations1.RequestToRefresh
        Xl_OrderConfirmations1.Load(OrderConfirmationsLoader.FromProveidor(_Proveidor))
    End Sub
End Class