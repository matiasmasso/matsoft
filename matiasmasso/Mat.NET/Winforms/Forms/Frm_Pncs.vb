Public Class Frm_Pncs
    Private _Items As List(Of DTOPurchaseOrderItem)
    Private _DefaultValue As DTOPurchaseOrderItem
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse

    Public Event itemSelected(sender As Object, e As MatEventArgs)

    Public Sub New(Optional oDefaultValue As DTOPurchaseOrderItem = Nothing, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse)
        MyBase.New()
        _DefaultValue = oDefaultValue
        _SelectionMode = oSelectionMode
        Me.InitializeComponent()
    End Sub

    Private Async Sub Frm_PurchaseOrderItems_Load(sender As Object, e As EventArgs) Handles Me.Load
        Await refresca()
    End Sub

    Private Async Sub Xl_Pncs1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_Pncs1.RequestToRefresh
        Await refresca()
    End Sub

    Private Async Function refresca() As Task
        Dim exs As New List(Of Exception)
        Dim oUser As DTOUser = Current.Session.User
        Dim oPurchaseOrderItems = Await FEB2.PurchaseOrderItems.Pending(exs, oUser, DTOPurchaseOrder.Codis.Client, GlobalVariables.Emp.Mgz)
        If exs.Count = 0 Then
            _Items = oPurchaseOrderItems.OrderBy(Function(x) x.Lin).OrderBy(Function(z) z.PurchaseOrder.Num).OrderBy(Function(y) y.PurchaseOrder.Fch).ToList
            Xl_Pncs1.Load(_Items, _DefaultValue, _SelectionMode)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Sub Xl_TextboxSearch1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_TextboxSearch1.AfterUpdate
        Xl_Pncs1.Filter = e.Argument
    End Sub

    Private Async Sub RefrescaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RefrescaToolStripMenuItem.Click
        Await refresca()
    End Sub

    Private Sub ExcelToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExcelToolStripMenuItem.Click
        Dim exs As New List(Of Exception)
        Dim oSheet As MatHelperStd.ExcelHelper.Sheet = FEB2.PurchaseOrderItems.Excel(_Items)
        If Not UIHelper.ShowExcel(oSheet, exs) Then
            UIHelper.WarnError(exs)
        End If
    End Sub
End Class