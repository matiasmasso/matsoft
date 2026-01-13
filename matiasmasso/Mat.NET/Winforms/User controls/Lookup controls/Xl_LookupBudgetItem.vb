Public Class Xl_LookupBudgetItem

    Inherits Xl_LookupTextboxButton

    Private _BudgetItem As DTOBudget.Item

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Shadows Property BudgetItem() As DTOBudget.Item
        Get
            Return _BudgetItem
        End Get
        Set(ByVal value As DTOBudget.Item)
            _BudgetItem = value
            If _BudgetItem Is Nothing Then
                MyBase.Text = ""
            Else
                MyBase.Text = DTOBudget.Item.Caption(_BudgetItem, Current.Session.Lang)
            End If
        End Set
    End Property

    Public Sub Clear()
        Me.BudgetItem = Nothing
    End Sub

    Private Sub Xl_LookupBudgetItem_onLookUpRequest(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.onLookUpRequest
        'Bubbled
    End Sub

    Private Sub onBudgetItemSelected(ByVal sender As Object, ByVal e As MatEventArgs)
        _BudgetItem = e.Argument
        MyBase.Text = DTOBudget.Item.Caption(_BudgetItem, Current.Session.Lang)
        RaiseEvent AfterUpdate(Me, e)
    End Sub
End Class
