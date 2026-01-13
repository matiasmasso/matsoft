Public Class Frm_PdcFchs
    Private _allOrders As List(Of DTOPurchaseOrder)
    Private _allowEvents As Boolean

    Private Async Sub Frm_PdcFchs_Load(sender As Object, e As EventArgs) Handles Me.Load
        DateTimePickerTo.Value = Today
        DateTimePickerFrom.Value = Today.AddMonths(-3)
        Await refrescaSources()
        _allowEvents = True
    End Sub

    Private Sub Xl_PdcSrcs_Checklist1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_PdcSrcs_Checklist1.AfterUpdate
        refrescaDetails()
    End Sub

    Private Async Sub DateTimePicker_ValueChanged(sender As Object, e As EventArgs) Handles _
        DateTimePickerFrom.ValueChanged,
         DateTimePickerTo.ValueChanged

        If _allowEvents Then
            Await refrescaSources()
        End If
    End Sub

    Private Async Function refrescaSources() As Task
        Dim exs As New List(Of Exception)
        _allOrders = Await FEB2.PurchaseOrders.Headers(exs, Current.Session.Emp, Cod:=DTOPurchaseOrder.Codis.Client, FchCreatedFrom:=DateTimePickerFrom.Value, FchCreatedTo:=DateTimePickerTo.Value)
        If exs.Count = 0 Then
            Xl_PdcSrcs_Checklist1.Load(_allOrders)
            refrescaDetails()
        Else
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Sub refrescaDetails()
        Dim oSources As List(Of DTOPurchaseOrder.Sources) = Xl_PdcSrcs_Checklist1.SelectedValues
        Dim oOrders As List(Of DTOPurchaseOrder) = _allOrders.Where(Function(x) oSources.Any(Function(y) x.Source)).ToList
        Dim filteredOrders = _allOrders.Where(Function(x) oSources.Any(Function(y) y = x.Source)).ToList
        Xl_PdcFchs1.Load(filteredOrders, Current.Session.Lang)
    End Sub
End Class