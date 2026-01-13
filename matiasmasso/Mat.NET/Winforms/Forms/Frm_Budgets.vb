Imports System.ComponentModel

Public Class Frm_Budgets

    Private _DefaultBudget As DTOBudget
    Private _DefaultBudgetOrder As DTOBudgetOrder
    Private _SelectionMode As DTOBudget.SelectionModes = DTOBudget.SelectionModes.Browse
    Private _done As Boolean
    Public Event itemSelected(sender As Object, e As MatEventArgs)



    Public Enum Tabs
        Budgets
        BudgetOrders
    End Enum

    Public Sub New(Optional oDefaultValue As DTOBudget = Nothing, Optional oSelectionMode As DTOBudget.SelectionModes = DTOBudget.SelectionModes.Browse)
        MyBase.New()
        _DefaultBudget = oDefaultValue
        _SelectionMode = oSelectionMode
        Me.InitializeComponent()
    End Sub

    Private Async Sub Frm_Budgets_Load(sender As Object, e As EventArgs) Handles Me.Load
        Await refrescaBudgets()
    End Sub

    Private Sub Xl_Budgets1_onItemSelected(sender As Object, e As MatEventArgs)
        RaiseEvent itemSelected(Me, e)
        Me.Close()
    End Sub

    Private Sub Xl_Budgets1_RequestToAddNew(sender As Object, e As MatEventArgs)
        Dim oBudget = DTOBudget.Factory(Current.Session.User)
        Dim oFrm As New Frm_Budget(oBudget)
        AddHandler oFrm.AfterUpdate, AddressOf refrescaBudgets
        oFrm.Show()
    End Sub

    Private Async Sub Xl_Budgets1_RequestToRefresh(sender As Object, e As MatEventArgs)
        Await refrescaBudgets()
    End Sub

    Private Async Sub refrescaBudgets(sender As Object, e As MatEventArgs)
        Await refrescaBudgets()
    End Sub

    Private Async Function refrescaBudgets() As Task
        Dim exs As New List(Of Exception)
        Dim oBudgets = Await FEB2.Budgets.All(exs, Current.Session.User)
        If exs.Count = 0 Then
            Xl_BudgetsTree1.Load(oBudgets, _DefaultBudget, _SelectionMode)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Async Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged
        Select Case TabControl1.SelectedIndex
            Case Tabs.BudgetOrders
                If Not _done Then
                    Dim exs As New List(Of Exception)
                    Dim oUser As DTOUser = Current.Session.User
                    Dim oBudgetOrders = Await FEB2.BudgetOrders.All(exs, oUser)
                    If exs.Count = 0 Then
                        Xl_BudgetOrders1.Load(oBudgetOrders)
                        _done = True
                    Else
                        UIHelper.WarnError(exs)
                    End If
                End If
        End Select
    End Sub

    Private Sub Xl_BudgetOrders1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_BudgetOrders1.RequestToAddNew
        Dim value = DTOBudgetOrder.Factory()
        If e.Argument IsNot Nothing AndAlso TypeOf e.Argument Is DTODocFile Then
            value.Docfile = e.Argument
        End If
        Dim oFrm As New Frm_BudgetOrder(value)
        AddHandler oFrm.AfterUpdate, AddressOf refrescaOrders
        oFrm.Show()
    End Sub

    Private Async Sub refrescaOrders(sender As Object, e As MatEventArgs)
        Await refrescaOrders()
    End Sub
    Private Async Function refrescaOrders() As Task
        Dim exs As New List(Of Exception)
        Dim oBudgetOrders = Await FEB2.BudgetOrders.All(exs, Current.Session.User)
        If exs.Count = 0 Then
            Xl_BudgetOrders1.Load(oBudgetOrders, _DefaultBudgetOrder, _SelectionMode)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Async Sub Xl_BudgetOrders1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_BudgetOrders1.RequestToRefresh
        Await refrescaOrders()
    End Sub

    Private Sub Xl_TextboxSearch1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_TextboxSearch1.AfterUpdate
        Xl_BudgetOrders1.Filter = e.Argument
    End Sub

    Private Sub Xl_BudgetsTree1_itemSelected(sender As Object, e As MatEventArgs)
        RaiseEvent itemSelected(Me, e)
        Me.Close()
    End Sub

    Private Sub Frm_Budgets_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        Xl_BudgetsTree1.SaveLayout()
    End Sub
End Class