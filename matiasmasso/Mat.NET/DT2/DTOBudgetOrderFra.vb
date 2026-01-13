Public Class DTOBudgetOrderFra
    Inherits DTOBaseGuid

    Property BookFra As DTOBookFra
    Property BudgetOrder As DTOBudgetOrder
    Property Amt As DTOAmt

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
    End Sub
End Class
