Public Class Xl_BudgetsTree
    Inherits Xl__TreeEditable

    Private _DefaultValue As DTOBudget
    Private _SelectionMode As DTOBudget.SelectionModes = DTOBudget.SelectionModes.Browse

    Public Event itemSelected(sender As Object, e As MatEventArgs)

    Public Shadows Sub Load(values As IEnumerable(Of DTOBudget), Optional oDefaultValue As DTOBudget = Nothing, Optional oSelectionMode As DTOBudget.SelectionModes = DTOBudget.SelectionModes.Browse)
        _DefaultValue = oDefaultValue
        _SelectionMode = oSelectionMode
        MyBase.Load(values)
    End Sub

    Protected Overrides Function GetContextmenu(Optional oNode As TreeNode = Nothing) As ContextMenu
        Dim retval As New ContextMenu
        If oNode IsNot Nothing Then
            Dim oBudget As DTOBudget = oNode.Tag
            Dim oMenuBudget As New Menu_Budget(oBudget)
            For Each menuitem As ToolStripMenuItem In oMenuBudget.MenuItems
                'retval.MenuItems.Add(New MenuItem(menuitem.Text, menuitem.eventhandler))
            Next
            If TypeOf oNode.Tag Is DTOBudget Then
                If oBudget.Codi = DTOBudget.Codis.Group Then
                    AddMenuItem("zoom", oNode, retval, AddressOf Do_ZoomGrup)
                    AddMenuItem("excel", oNode, retval, AddressOf Do_Excel)
                    AddMenuItem("afegir grup", oNode, retval, AddressOf Do_AddGrup)
                    AddMenuItem("afegir partida", oNode, retval, AddressOf Do_AddPartida)
                ElseIf oBudget.Codi = DTOBudget.Codis.Partida Then
                    AddMenuItem("zoom", oNode, retval, AddressOf Do_ZoomPartida)
                    AddMenuItem("excel", oNode, retval, AddressOf Do_Excel)
                End If
            End If
        End If
        Return retval
    End Function

    Private Sub AddMenuItem(caption As String, ByRef oNode As TreeNode, ByRef oContextMenu As ContextMenu, e As EventHandler)
        Dim oMenuItem As New MenuItem(caption, e)
        oMenuItem.Tag = oNode
        oContextMenu.MenuItems.Add(oMenuItem)
    End Sub

    Private Sub refrescaNode(sender As Object, e As MatEventArgs)
        Dim oValue As DTOGuidNomNode = e.Argument
        Dim oNode As TreeNode = getNode(MyBase.Nodes, oValue)
        oNode.Tag = oValue
        oNode.Text = oValue.Nom
    End Sub

    Private Sub removeNode(sender As Object, e As MatEventArgs)
        Dim oValue As DTOGuidNomNode = e.Argument
        Dim oNode As TreeNode = getNode(MyBase.Nodes, oValue)
        oNode.Remove()
    End Sub

    Private Function getNode(oNodes As TreeNodeCollection, oValue As DTOGuidNomNode) As TreeNode
        Dim retval As TreeNode = Nothing
        For Each oNode As TreeNode In oNodes
            If oNode.Tag.Equals(oValue) Then
                retval = oNode
                Exit For
            Else
                retval = getNode(oNode.Nodes, oValue)
                If retval IsNot Nothing Then Exit For
            End If
        Next
        Return retval
    End Function

    Private Sub Do_ZoomGrup(sender As Object, e As EventArgs)
        Dim menuItem As MenuItem = sender
        Dim oNode As TreeNode = menuItem.Tag
        Dim oValue As DTOBudget = oNode.Tag
        Dim oFrm As New Frm_Budget(oValue)
        AddHandler oFrm.AfterUpdate, AddressOf refrescaNode
        AddHandler oFrm.AfterDelete, AddressOf removeNode
        oFrm.Show()
    End Sub

    Private Sub Do_ZoomPartida(sender As Object, e As EventArgs)
        Dim menuItem As MenuItem = sender
        Dim oNode As TreeNode = menuItem.Tag
        Dim oValue As DTOBudget = oNode.Tag
        Dim oFrm As New Frm_BudgetPartida(oValue)
        AddHandler oFrm.AfterUpdate, AddressOf refrescaNode
        AddHandler oFrm.itemSelected, AddressOf onItemSelected
        oFrm.Show()
    End Sub

    Private Sub Do_Excel(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim menuItem As MenuItem = sender
        Dim oNode As TreeNode = menuItem.Tag
        Dim oValue As DTOBudget = oNode.Tag
        Dim oBook = FEB2.Budget.ExcelBook(oValue)
        Dim exs As New List(Of Exception)
        If Not UIHelper.ShowExcel(oBook, exs) Then
            UIHelper.WarnError(exs)
        End If
    End Sub


    Private Sub onItemSelected(sender As Object, e As MatEventArgs)
        RaiseEvent itemSelected(Me, e)
    End Sub

    Private Sub Do_AddGrup(sender As Object, e As EventArgs)
        Dim menuItem As MenuItem = sender
        Dim oNodeParent As TreeNode = menuItem.Tag
        Dim oNode As TreeNode = Nothing
        Dim oParentValue As DTOGuidNomNode = Nothing
        If oNodeParent Is Nothing Then
            Dim oValue As DTOGuidNomNode = CreateBudget()
            oNode = treeNode(oValue)
            MyBase.Nodes.Add(oNode)
        Else
            oParentValue = oNodeParent.Tag
            Dim oChildValue As DTOGuidNomNode = CreateBudget(oParentValue)
            oNode = treeNode(oChildValue)
            oParentValue.AddChild(oChildValue)
            oNodeParent.Nodes.Add(oNode)
        End If
        oNodeParent.Expand()
        oNode.BeginEdit()

    End Sub

    Private Sub Do_AddPartida(sender As Object, e As EventArgs)
        Dim menuItem As MenuItem = sender
        Dim oNodeParent As TreeNode = menuItem.Tag

        Dim oParentValue As DTOBudget = oNodeParent.Tag
        Dim oChildValue As DTOBudget = CreatePartida(oParentValue)
        Dim oNode As TreeNode = treeNode(oChildValue)
        oParentValue.AddChild(oChildValue)
        oNodeParent.Nodes.Add(oNode)
        oNodeParent.Expand()
        oNode.BeginEdit()
    End Sub

    Private Sub Do_AddItem(sender As Object, e As EventArgs)
        Dim menuItem As MenuItem = sender
        Dim oNodeParent As TreeNode = menuItem.Tag
        Dim oPartida As DTOBudget = oNodeParent.Tag
        'Dim oFrm As New frm_BudgetItem
    End Sub

    Protected Overrides Async Function UpdateValue(oValue As DTOGuidNomNode) As Task
        Dim exs As New List(Of Exception)
        If Not Await FEB2.Budget.Update(oValue, exs) Then
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Function CreateBudget(Optional oParent As DTOBudget = Nothing) As DTOBudget
        Dim retval As New DTOBudget
        With retval
            If oParent Is Nothing Then
                .Exercici = DTOExercici.Current(Current.Session.Emp)
                .Ord = MyBase.Nodes.Count
            Else
                .Parent = oParent
                .Exercici = oParent.Exercici
                .Ord = oParent.Children.Count
            End If
            .Nom = "(nou pressupost)"
            .Codi = DTOBudget.Codis.Group
        End With
        Return retval
    End Function

    Private Function CreatePartida(Optional oParent As DTOBudget = Nothing) As DTOBudget
        Dim retval As DTOBudget = CreateBudget(oParent)
        With retval
            .Nom = "(nova partida pressupostaria)"
            .Codi = DTOBudget.Codis.Partida
        End With
        Return retval
    End Function

    Private Function CreateItem(Optional oParent As DTOBudget = Nothing) As DTOBudget.Item
        Dim retval As New DTOBudget.Item
        With retval
            .Budget = oParent
        End With
        Return retval
    End Function

    Private Sub Xl_BudgetsTree_DoubleClick(sender As Object, e As EventArgs) Handles Me.DoubleClick
        Dim oNode As TreeNode = MyBase.SelectedNode
        Dim oBudget As DTOBudget = oNode.Tag
        Select Case oBudget.Codi
            Case DTOBudget.Codis.Group
                Select Case _SelectionMode
                    Case DTOBudget.SelectionModes.Budget
                        RaiseEvent itemSelected(Me, New MatEventArgs(oBudget))
                    Case Else
                        Dim oFrm As New Frm_Budget(oBudget)
                        AddHandler oFrm.AfterUpdate, AddressOf refrescaNode
                        oFrm.Show()
                End Select
            Case DTOBudget.Codis.Partida
                Select Case _SelectionMode
                    Case DTOBudget.SelectionModes.Budget
                        RaiseEvent itemSelected(Me, New MatEventArgs(oBudget))
                    Case Else
                        Dim oFrm As New Frm_BudgetPartida(oBudget, Nothing, _SelectionMode)
                        AddHandler oFrm.AfterUpdate, AddressOf refrescaNode
                        oFrm.Show()
                End Select
        End Select
    End Sub


End Class
