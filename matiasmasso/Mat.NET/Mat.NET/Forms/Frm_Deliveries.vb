Public Class Frm_Deliveries
    Private _Orders As List(Of DTODelivery)
    Private _Mode As Modes = Modes.All

    Private Enum Modes
        All
        Some
    End Enum

    Public Sub New(oOrders As List(Of DTODelivery))
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

    Private Sub Frm_Deliveries_Load(sender As Object, e As EventArgs) Handles Me.Load
        If _Mode = Modes.All Then
            Dim iYeas As List(Of Integer) = BLL.BLLDeliveries.Years()
            Xl_Years1.Load(iYeas)
        End If
        refresca()
    End Sub

    Private Sub refresca()
        If _Mode = Modes.All Then
            _Orders = BLL.BLLDeliveries.Headers(DTOPurchaseOrder.Codis.Client, , Xl_Years1.Exercici.Year)
        End If
        Xl_Deliveries1.Load(_Orders, Xl_Deliveries.Modes.MultipleCustomers)
    End Sub

    Private Sub TextBoxSearch_TextChanged(sender As Object, e As EventArgs) Handles TextBoxSearch.TextChanged
        Dim sSearchKey As String = TextBoxSearch.Text
        If sSearchKey.Length > 3 Then
            TextBoxSearch.ForeColor = Color.Black
            Xl_Deliveries1.Filter = sSearchKey
            Dim iCount As Integer = Xl_Deliveries1.Count
            If iCount = 0 Then
                LabelFiltre.Text = "filtre:"
            Else
                LabelFiltre.Text = iCount.ToString
            End If
        Else
            Xl_Deliveries1.ClearFilter()
            TextBoxSearch.ForeColor = Color.Gray
            LabelFiltre.Text = "filtre:"
        End If
    End Sub


    Private Sub Xl_Years1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_Years1.AfterUpdate
        refresca()
    End Sub

    Private Sub Xl_Deliveries1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_Deliveries1.RequestToRefresh
        refresca()
    End Sub
End Class