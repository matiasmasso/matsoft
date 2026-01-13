Public Class Frm_EciPurchaseOrders
    Private _AllowEvents As Boolean


    Private Async Sub Frm_EciPurchaseOrders_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        ProgressBar1.Visible = True
        Dim iYears = Await FEB2.ElCorteIngles.PurchaseOrderYears(exs, Current.Session.Emp)
        If exs.Count = 0 Then
            Xl_Years1.Load(iYears)
            Dim oOrders = Await FEB2.ElCorteIngles.PurchaseOrders(exs, Current.Session.Emp, Xl_Years1.Value)
            ProgressBar1.Visible = False
            If exs.Count = 0 Then
                Xl_ECIPurchaseOrders1.Load(oOrders)
                LoadDepts()
            Else
                UIHelper.WarnError(exs)
            End If
        Else
            ProgressBar1.Visible = False
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub LoadYears(oOrders As List(Of DTOPurchaseOrder))
        Dim Query = oOrders.GroupBy(Function(g) New With {Key g.Fch.Year}).Select(Function(group) New With {.Year = group.Key.Year})
        Dim iYears As New List(Of Integer)
        For Each item In Query
            iYears.Add(item.Year)
        Next
        iYears.Sort()
        iYears.Reverse()
        Xl_Years1.Load(iYears)
    End Sub

    Private Sub LoadDepts()
        Dim sDepts As List(Of String) = Xl_ECIPurchaseOrders1.Depts
        ComboBoxDepts.Items.Clear()
        ComboBoxDepts.Items.Add("(tots)")
        For Each sDept As String In sDepts
            If sDept > "" Then ComboBoxDepts.Items.Add(sDept)
        Next
        ComboBoxDepts.SelectedIndex = 0
    End Sub

    Private Async Function refresca() As Task
        Dim exs As New List(Of Exception)
        Dim oOrders = Await FEB2.ElCorteIngles.PurchaseOrders(exs, GlobalVariables.Emp, Xl_Years1.Value)
        If exs.Count = 0 Then
            Xl_ECIPurchaseOrders1.Load(oOrders)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Sub Xl_TextboxSearch1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_TextboxSearch1.AfterUpdate
        Xl_ECIPurchaseOrders1.Filter = e.Argument
    End Sub

    Private Async Sub Xl_Years1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_Years1.AfterUpdate
        Await refresca()
        LoadDepts()
    End Sub

    Private Sub ComboBoxDepts_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxDepts.SelectedIndexChanged
        If ComboBoxDepts.SelectedIndex = 0 Then
            Xl_ECIPurchaseOrders1.Dept = ""
        Else
            Xl_ECIPurchaseOrders1.Dept = ComboBoxDepts.SelectedItem
        End If
    End Sub

    Private Async Sub Xl_ECIPurchaseOrders1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_ECIPurchaseOrders1.RequestToRefresh
        Await refresca()
    End Sub
End Class