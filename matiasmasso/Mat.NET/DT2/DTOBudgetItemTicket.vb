Public Class DTOBudgetItemTicket
    Inherits DTOBaseGuid

    Property BookFra As DTOBookFra
    Property BudgetItem As DTOBudget.Item
    Property Docfile As DTODocFile
    Property Amt As DTOAmt

    Public Enum Cods
        NotSet
        Factura
        Altres
    End Enum

    Public ReadOnly Property Cod As Cods
        Get
            If _BookFra IsNot Nothing Then
                Return Cods.Factura
            ElseIf _Docfile IsNot Nothing Then
                Return Cods.Altres
            Else
                Return Cods.NotSet
            End If
        End Get
    End Property
    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
    End Sub

    Shared Function Fch(oTicket As DTOBudgetItemTicket) As Date
        Dim retval As Date
        If oTicket.BookFra Is Nothing Then
            retval = oTicket.Docfile.Fch
        Else
            retval = oTicket.BookFra.Cca.Fch
        End If
        Return retval
    End Function

    Shared Function Caption(oTicket As DTOBudgetItemTicket) As String
        Dim retval As String
        If oTicket.BookFra Is Nothing Then
            retval = "see document here"
        Else
            retval = DTOBookFra.Caption(oTicket.BookFra)
        End If
        Return retval
    End Function
End Class
