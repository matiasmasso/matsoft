Public Class Frm_BritaxTarget
    Private _year As Integer
    Private _orders As List(Of DTOPurchaseOrder)
    Private _bookfras As List(Of DTOBookFra)
    Private _pendingOrders As List(Of DTOPurchaseOrder)

    Private Async Sub Frm_BritaxTarget_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        _year = DTO.GlobalVariables.Today().Year

        Dim fchTo As New Date(_year, 12, 15)
        Dim fchfrom As Date = fchTo.AddYears(-1)
        Dim oProveidor = DTOProveidor.Wellknown(DTOProveidor.Wellknowns.Roemer)
        _orders = Await FEB.PurchaseOrders.Headers(exs, Current.Session.Emp,
                                            Cod:=DTOPurchaseOrder.Codis.Proveidor,
                                                Contact:=oProveidor,
                                                FchCreatedFrom:=fchfrom,
                                                FchCreatedTo:=fchTo)
        If exs.Count = 0 Then
            _pendingOrders = Await FEB.PurchaseOrders.Pending(exs, Current.Session.Emp, cod:=DTOPurchaseOrder.Codis.proveidor, contact:=oProveidor)
            _bookfras = Await FEB.Bookfras.All(exs, DTOBookFra.Modes.all, DTOExercici.Current(Current.Session.Emp), , oProveidor)
            If exs.Count = 0 Then
                _bookfras = _bookfras.Where(Function(x) x.cta.codi = DTOPgcPlan.Ctas.compras).ToList
                Xl_BritaxTarget1.Load(_year, _bookfras, _orders, _pendingOrders)
                ProgressBar1.Visible = False
            Else
                ProgressBar1.Visible = False
                UIHelper.WarnError(exs)
            End If
        Else
            ProgressBar1.Visible = False
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub ExcelToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExcelToolStripMenuItem.Click
        Dim exs As New List(Of Exception)
        Dim oSheet = FEB.Britax.ExcelTarget(_year, _bookfras, _orders, _pendingOrders)
        If Not UIHelper.ShowExcel(oSheet, exs) Then
            UIHelper.WarnError(exs)
        End If
    End Sub
End Class