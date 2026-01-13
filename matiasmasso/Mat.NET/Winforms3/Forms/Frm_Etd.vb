Public Class Frm_Etd
    Private _items As List(Of DTOPurchaseOrderItem)
    Private _allowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(items As List(Of DTOPurchaseOrderItem))
        MyBase.New
        InitializeComponent()
        _items = items
        Dim previousFch As Date = DTO.GlobalVariables.Today()
        If items.Any(Function(x) x.ETD <> items.First.ETD) Then
            DateTimePicker1.Value = DTO.GlobalVariables.Today()
            ButtonOk.Enabled = True
        Else
            If items.First.ETD = Nothing Then
                DateTimePicker1.Value = DTO.GlobalVariables.Today()
                ButtonOk.Enabled = True
                ButtonDel.Enabled = False
            Else
                DateTimePicker1.Value = items.First.ETD
            End If
        End If
        _allowEvents = True
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        For Each item As DTOPurchaseOrderItem In _items
            item.ETD = DateTimePicker1.Value
        Next
        Await Save()
    End Sub

    Private Async Sub ButtonDel_Click(sender As Object, e As EventArgs) Handles ButtonDel.Click
        For Each item As DTOPurchaseOrderItem In _items
            item.ETD = Nothing
        Next
        Await Save()
    End Sub

    Private Async Function Save() As Task
        Dim exs As New List(Of Exception)
        If Await FEB.PurchaseOrderItems.UpdateEtd(exs, _items) Then
            RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
            Me.Close()
        Else
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Sub DateTimePicker1_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker1.ValueChanged
        If _allowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub
End Class