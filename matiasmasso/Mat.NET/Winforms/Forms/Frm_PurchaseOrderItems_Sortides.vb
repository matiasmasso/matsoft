Public Class Frm_PurchaseOrderItems_Sortides
    Private _value As DTOPurchaseOrderItem

    Public Sub New(value As DTOPurchaseOrderItem)
        MyBase.New
        Me.InitializeComponent()
        _value = value
    End Sub

    Private Async Sub Frm_PurchaseOrderItems_Sortides_Load(sender As Object, e As EventArgs) Handles Me.Load
        LoadHeader()
        Await LoadSortides()
    End Sub

    Private Sub LoadHeader()
        Dim exs As New List(Of Exception)
        Dim oOrder As DTOPurchaseOrder = _value.PurchaseOrder
        FEB2.Contact.Load(oOrder.Contact, exs)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine(oOrder.Contact.FullNom)
        sb.AppendLine(oOrder.Caption())
        sb.AppendLine(_value.qty & " x " & _value.sku.nomLlarg.Tradueix(Current.Session.Lang))
        TextBox1.Text = sb.ToString
    End Sub

    Private Async Function LoadSortides() As Task
        Dim exs As New List(Of Exception)
        Dim items = Await FEB2.DeliveryItems.All(exs, _value)
        If exs.Count = 0 Then
            Xl_PurchaseOrderItem_Sortides1.Load(items)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function
End Class