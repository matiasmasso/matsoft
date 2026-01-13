Public Class DTOBudget
    Inherits DTOGuidNomNode

    Property Exercici As DTOExercici
    Property Contact As DTOContact
    Property Docfile As DTODocFile
    Property Ord As Integer
    Property Amt As DTOAmt
    Property Codi As Codis
    Property Level As Integer
    Property Rols As New List(Of DTORol)
    Property Items As New List(Of DTOBudget.Item)

    Public Enum SelectionModes
        Browse
        Budget
        Partida
        Item
    End Enum

    Public Enum Codis
        Group 'te childrens budget
        Partida 'te childrens items
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
    End Sub

    Shared Shadows Function Factory(oUser As DTOUser) As DTOBudget
        Dim retval As New DTOBudget
        With retval
            .Exercici = DTOExercici.Current(oUser.Emp)
            .Rols.Add(oUser.Rol)
        End With
        Return retval
    End Function

    Shared Function Budget(oBudget As DTOBudget) As DTOAmt
        Dim DcEur As Decimal = oBudget.Items.Sum(Function(x) x.Amt.Eur)
        Dim retval As DTOAmt = DTOAmt.Factory(DcEur)
        Return retval
    End Function

    Shared Function Spent(oBudget As DTOBudget) As DTOAmt
        Dim retval = DTOAmt.Empty
        Return retval
    End Function

    Shared Function ReverseNom(ByVal oBudget As DTOBudget) As String
        Dim segments As New List(Of String)
        Do While oBudget.Parent IsNot Nothing
            oBudget = oBudget.Parent
            segments.Insert(0, oBudget.Nom)
        Loop

        Dim sb As New Text.StringBuilder
        For Each segment As String In segments
            If sb.Length > 0 Then sb.Append(".")
            sb.Append(segment)
        Next

        Dim retval As String = sb.ToString
        Return retval
    End Function


    Shared Function FlatChildren(oBudget As DTOBudget) As List(Of DTOBudget)
        Dim retval As New List(Of DTOBudget)
        For Each child As DTOBudget In oBudget.Children
            child.Level = oBudget.Level + 1
            retval.Add(child)
            retval.AddRange(FlatChildren(child))
        Next
        Return retval
    End Function

    Shared Function Partidas(oBudget As DTOBudget) As List(Of DTOBudget)
        Dim oFlatChildren As List(Of DTOBudget) = FlatChildren(oBudget)
        Dim retval As List(Of DTOBudget) = oFlatChildren.Where(Function(x) x.Codi = DTOBudget.Codis.Partida).ToList
        Return retval
    End Function

    Shared Function Assignat(oBudget As DTOBudget) As DTOAmt
        Dim dcVal As Decimal
        If oBudget.Codi = DTOBudget.Codis.Partida Then
            Dim oPartida As DTOBudget = oBudget
            dcVal = oPartida.Items.Sum(Function(y) y.Amt.Eur)
        Else
            Dim oPartidas As List(Of DTOBudget) = Partidas(oBudget)
            For Each oPartida As DTOBudget In oPartidas
                dcVal += oPartida.Items.Sum(Function(y) y.Amt.Eur)
            Next
        End If
        Dim retval As DTOAmt = DTOAmt.Factory(dcVal)
        Return retval
    End Function

    Shared Function Aplicat(oBudget As DTOBudget) As DTOAmt
        Dim dcVal As Decimal
        If oBudget.Codi = DTOBudget.Codis.Partida Then
            Dim oPartida As DTOBudget = oBudget
            dcVal = oPartida.Items.Sum(Function(x) x.Tickets.Sum(Function(y) y.Amt.Eur))
        Else
            Dim oPartidas As List(Of DTOBudget) = Partidas(oBudget)
            For Each oPartida As DTOBudget In oPartidas
                dcVal = oPartida.Items.Sum(Function(x) x.Tickets.Sum(Function(y) y.Amt.Eur))
            Next
        End If
        Dim retval As DTOAmt = DTOAmt.Factory(dcVal)

        Return retval
    End Function

    Shared Sub Merge(ByRef oBudgets As List(Of DTOBudget), oItems As List(Of DTOBudget.Item))
        For Each oBudget As DTOBudget In oBudgets
            If oBudget.Parent IsNot Nothing Then
                oBudget.Parent = oBudgets.FirstOrDefault(Function(x) x.Equals(oBudget.Parent))
                oBudget.Parent.Children.Add(oBudget)
            End If
        Next
        For Each item As DTOBudget.Item In oItems
            item.Budget = oBudgets.FirstOrDefault(Function(x) x.Equals(item.Budget))
            DirectCast(item.Budget, DTOBudget).Items.Add(item)
        Next
    End Sub

    Shared Function RootBudgets(oBudgets As List(Of DTOBudget)) As List(Of DTOBudget)
        Dim retval As List(Of DTOBudget) = oBudgets.Where(Function(x) x.Parent Is Nothing).ToList
        Return retval
    End Function

    Shared Function FilterByUser(oBudgets As List(Of DTOBudget), oUser As DTOUser) As List(Of DTOBudget)
        Dim retval As New List(Of DTOBudget)
        For Each oBudget As DTOBudget In oBudgets
            Select Case oUser.Rol.Id
                Case DTORol.Ids.SuperUser, DTORol.Ids.Admin, DTORol.Ids.Accounts, DTORol.Ids.Marketing
                    retval.Add(oBudget)
                Case Else
                    Dim match As Boolean = oBudget.Rols.Any(Function(y) y.Id = oUser.Rol.Id)
                    If match Then
                        retval.Add(oBudget)
                    End If
            End Select
        Next
        Return retval
    End Function


    Public Class Item
        Inherits DTOBaseGuid
        Property Budget As DTOBudget
        Property MonthFrom As DTOYearMonth
        Property MonthTo As DTOYearMonth
        Property Amt As DTOAmt

        Property Tickets As New List(Of DTOBudgetItemTicket)

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(oGuid As Guid)
            MyBase.New(oGuid)
        End Sub

        Shared Function Caption(value As DTOBudget.Item, oLang As DTOLang)
            Dim sb As New Text.StringBuilder
            If value.Budget IsNot Nothing Then
                sb.Append(value.Budget.Nom)
            End If
            If value.MonthFrom IsNot Nothing Then

                If value.MonthFrom.Month = value.MonthTo.Month Then
                    sb.AppendFormat(" ({0})", oLang.MesAbr(value.MonthFrom.Month))
                Else
                    sb.AppendFormat(" ({0}-{1})", oLang.MesAbr(value.MonthFrom.Month), oLang.MesAbr(value.MonthTo.Month))
                End If
            End If
            Dim retval As String = sb.ToString
            Return retval
        End Function
    End Class
End Class

Public Class DTOBudgetOrder
    Inherits DTOBaseGuid

    Property Fch As Date
    Property Num As String
    Property Contact As DTOContact
    Property Obs As String
    Property Docfile As DTODocFile
    Property Amt As DTOAmt

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
    End Sub

    Shared Function Factory() As DTOBudgetOrder
        Dim retval As New DTOBudgetOrder
        With retval
            .Fch = DateTime.Today
            .Amt = DTOAmt.Empty
        End With
        Return retval
    End Function

    Shared Function Caption(oBudgetOrder As DTOBudgetOrder) As String
        Dim sb As New Text.StringBuilder
        sb.Append("Ordre")
        If oBudgetOrder.Num > "" Then
            sb.Append(" #" & oBudgetOrder.Num)
        End If
        sb.Append(String.Format(" del {0:dd/MM/yy}", oBudgetOrder.Fch))
        sb.Append(String.Format(" de {0}", oBudgetOrder.Contact.FullNom))
        Dim retval As String = sb.ToString
        Return retval
    End Function
End Class

Public Class DTOBudgetOrderItem
    Inherits DTOBaseGuid

    Property BudgetOrder As DTOBudgetOrder
    Property Budget As DTOBudget
    Property Amt As DTOAmt

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
    End Sub
End Class

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
            retval = oTicket.BookFra.cca.Fch
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
