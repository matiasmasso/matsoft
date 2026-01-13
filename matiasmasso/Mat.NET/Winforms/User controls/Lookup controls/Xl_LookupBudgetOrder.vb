Public Class Xl_LookupBudgetOrder
    Inherits Xl_LookupTextboxButton

    Private _BudgetOrder As DTOBudgetOrder

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Shadows Property BudgetOrder() As DTOBudgetOrder
        Get
            Return _BudgetOrder
        End Get
        Set(ByVal value As DTOBudgetOrder)
            _BudgetOrder = value
            If _BudgetOrder Is Nothing Then
                MyBase.Text = ""
            Else
                MyBase.Text = DTOBudgetOrder.Caption(_BudgetOrder)
            End If
        End Set
    End Property

    Public Sub Clear()
        Me.BudgetOrder = Nothing
    End Sub

    Private Sub Xl_LookupBudgetOrder_onLookUpRequest(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.onLookUpRequest
        'Dim oFrm As New Frm_BudgetOrders(_BudgetOrder, DTO.Defaults.SelectionModes.Selection)
        'AddHandler oFrm.OnItemSelected, AddressOf onBudgetOrderSelected
        'oFrm.Show()
    End Sub

    Private Sub onBudgetOrderSelected(ByVal sender As Object, ByVal e As MatEventArgs)
        _BudgetOrder = e.Argument
        MyBase.Text = DTOBudgetOrder.Caption(_BudgetOrder)
        RaiseEvent AfterUpdate(Me, e)
    End Sub
End Class

