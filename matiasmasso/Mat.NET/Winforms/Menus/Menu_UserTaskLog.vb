Public Class Menu_UserTaskLog
    Inherits Menu_Base

    Private _UserTaskLogs As List(Of DTOUserTaskLog)
    Private _UserTaskLog As DTOUserTaskLog

    Public Sub New(ByVal oUserTaskLogs As List(Of DTOUserTaskLog))
        MyBase.New()
        _UserTaskLogs = oUserTaskLogs
        If _UserTaskLogs IsNot Nothing Then
            If _UserTaskLogs.Count > 0 Then
                _UserTaskLog = _UserTaskLogs.First
            End If
        End If
        AddMenuItems()
    End Sub

    Public Sub New(ByVal oUserTaskLog As DTOUserTaskLog)
        MyBase.New()
        _UserTaskLog = oUserTaskLog
        _UserTaskLogs = New List(Of DTOUserTaskLog)
        If _UserTaskLog IsNot Nothing Then
            _UserTaskLogs.Add(_UserTaskLog)
        End If
        AddMenuItems()
    End Sub

    Private Sub AddMenuItems()
        If _UserTaskLogs.Count = 1 Then
            Select Case _UserTaskLog.UserTaskId.Id
                Case DTOUserTaskId.Ids.CheckCustomerPurchaseOrder
                    Dim oParentMenuItem As New ToolStripMenuItem("Comanda")
                    MyBase.AddMenuItem(oParentMenuItem)
                    Dim oPurchaseOrder As New DTOPurchaseOrder(_UserTaskLog.Ref.Guid)
                    Dim oOrders As New List(Of DTOPurchaseOrder)
                    oOrders.Add(oPurchaseOrder)
                    Dim oMenu_Order As New Menu_Pdc(oOrders)
                    oParentMenuItem.DropDownItems.AddRange(oMenu_Order.Range)
                Case DTOUserTaskId.Ids.RequestForSupplierPurchaseOrder
                    Dim oMenuItem As New ToolStripMenuItem("Forecast", Nothing, AddressOf Do_ShowForecast)
                    MyBase.AddMenuItem(oMenuItem)
                Case DTOUserTaskId.Ids.CheckSoftwareIncidence
                    Dim oMenuItem As New ToolStripMenuItem("Incidencia", Nothing, AddressOf Do_ShowMatsoftIncidencia)
                    MyBase.AddMenuItem(oMenuItem)
            End Select
        End If

        MyBase.AddMenuItem(MenuItem_ToggleComplete())
    End Sub


    '==========================================================================
    '                            MENU ITEMS BUILDER
    '==========================================================================



    Private Function MenuItem_ToggleComplete() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem

        Dim oFirstItem As DTOUserTaskLog = _UserTaskLogs.First
        If oFirstItem.FchCompleted = Nothing Then
            oMenuItem.Enabled = _UserTaskLogs.All(Function(x) x.FchCompleted = Nothing)
            oMenuItem.Text = IIf(_UserTaskLogs.Count = 1, "Marcar com a completada", "Marcar com a completades")
        Else
            oMenuItem.Enabled = _UserTaskLogs.All(Function(x) x.FchCompleted <> Nothing)
            oMenuItem.Text = IIf(_UserTaskLogs.Count = 1, "Marcar com a pendent", "Marcar com a pendents")
        End If

        AddHandler oMenuItem.Click, AddressOf Do_ToggleComplete
        Return oMenuItem
    End Function


    '==========================================================================
    '                               EVENT HANDLERS
    '==========================================================================

    Private Async Sub Do_ToggleComplete(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oUser As DTOUser = Current.Session.User
        Dim exs As New List(Of Exception)
        If Await FEB2.UserTaskLogs.ToggleComplete(exs, _UserTaskLogs, oUser) Then
            MyBase.RefreshRequest(Me, New MatEventArgs(_UserTaskLogs))
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Do_ShowForecast()
        Dim oProveidor As New DTOProveidor(_UserTaskLog.Ref.Guid)
        Dim oFrm As New Frm_ProductSkuForecast(oProveidor)
        'AddHandler oFrm.onOrderComplete, 
        oFrm.Show()
    End Sub

    Private Sub Do_ShowMatsoftIncidencia()
        Dim oSupportCase As New DTOMatsoftSupportCase(_UserTaskLog.Ref.Guid)
        Dim oFrm As New Frm_MatsoftSupportCase(oSupportCase)
        'AddHandler oFrm.onOrderComplete, 
        oFrm.Show()

    End Sub
End Class


