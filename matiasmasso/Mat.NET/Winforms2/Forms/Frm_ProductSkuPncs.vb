Public Class Frm_ProductSkuPncs
    Private _Sku As DTOProductSku
    Private _Cod As DTOPurchaseOrder.Codis

    Public Sub New(oSku As DTOProductSku, oCod As DTOPurchaseOrder.Codis)
        MyBase.New
        InitializeComponent()
        _Sku = oSku
        _Cod = oCod
    End Sub

    Private Async Sub Frm_SkuClients_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        Me.Text = "Pendents de " & _Sku.nomLlarg.Tradueix(Current.Session.Lang)
        Await refresca()
    End Sub

    Private Async Function refresca() As Task
        Dim exs As New List(Of Exception)
        Dim items = Await FEB.PurchaseOrderItems.Pending(exs, GlobalVariables.Emp, _Sku, _Cod)
        If exs.Count = 0 Then
            Xl_ProductSkuPncs1.Load(items)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Async Sub Xl_ProductSkuPncs1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_ProductSkuPncs1.RequestToRefresh
        Await refresca()
    End Sub

    Private Async Sub RefrescaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RefrescaToolStripMenuItem.Click
        Await refresca()
    End Sub
End Class