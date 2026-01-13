Public Class Frm_PurchaseOrders
    Private _Orders As List(Of DTOPurchaseOrder)
    Private _Mode As Modes = Modes.All

    Private Enum Modes
        All
        Some
    End Enum

    Public Sub New(oOrders As List(Of DTOPurchaseOrder))
        MyBase.New()
        Me.InitializeComponent()
        _Orders = oOrders
        _Mode = Modes.Some
        Xl_Years1.Visible = False
    End Sub

    Public Sub New()
        MyBase.New()
        Me.InitializeComponent()
        _Mode = Modes.All
    End Sub

    Private Sub Frm_PurchaseOrders_Load(sender As Object, e As EventArgs) Handles Me.Load
        If _Mode = Modes.All Then
            Dim iYeas As List(Of Integer) = BLL.BLLPurchaseOrders.Years(BLL.BLLApp.Emp, DTOPurchaseOrder.Codis.Client, Nothing)
            Xl_Years1.Load(iYeas)
        End If
        refresca()
    End Sub

    Private Sub refresca()
        If _Mode = Modes.All Then
            _Orders = BLL.BLLPurchaseOrders.Headers(DTOPurchaseOrder.Codis.Client, , Xl_Years1.Exercici.Year)
        End If
        Xl_PurchaseOrders1.Load(_Orders, Xl_PurchaseOrders.Modes.MultipleCustomers)
    End Sub

    Private Sub TextBoxSearch_TextChanged(sender As Object, e As EventArgs) Handles TextBoxSearch.TextChanged
        Dim sSearchKey As String = TextBoxSearch.Text
        If sSearchKey.Length > 3 Then
            TextBoxSearch.ForeColor = Color.Black
            Xl_PurchaseOrders1.Filter = sSearchKey
            Dim iCount As Integer = Xl_PurchaseOrders1.Count
            If iCount = 0 Then
                LabelFiltre.Text = "filtre:"
            Else
                LabelFiltre.Text = iCount.ToString
            End If
        Else
            Xl_PurchaseOrders1.ClearFilter()
            TextBoxSearch.ForeColor = Color.Gray
            LabelFiltre.Text = "filtre:"
        End If
    End Sub


    Private Sub Xl_Years1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_Years1.AfterUpdate
        refresca()
    End Sub

    Private Sub Xl_PurchaseOrders1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_PurchaseOrders1.RequestToRefresh
        refresca()
    End Sub
End Class