Public Class Xl_ContactClassesTree
    Inherits TreeView

    Private _Lang As DTOLang
    Private _Values As List(Of DTOContactClass)
    Private _NodeList As List(Of TreeNode)

    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)
    Public Event RequestToAddRoot(sender As Object, e As MatEventArgs)
    Public Event RequestToRefresh(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)
    Public Event ItemSelected(sender As Object, e As MatEventArgs)

    Private _AllowEvents As Boolean

    Public Shadows Sub Load(values As List(Of DTOContactClass), oLang As DTOLang, Optional oSelectedClass As DTOContactClass = Nothing)
        _Values = values
        _Lang = oLang
        _NodeList = New List(Of TreeNode)


        If oSelectedClass Is Nothing Then
            If MyBase.SelectedNode IsNot Nothing Then
                oSelectedClass = MyBase.SelectedNode.Tag
            End If
        End If

        MyBase.Nodes.Clear()
        Dim oNodeDist As New TreeNode("Clients")
        MyBase.Nodes.Add(oNodeDist)

        For Each oValue As DTOContactClass In _Values.Where(Function(x) x.DistributionChannel.UnEquals(DTODistributionChannel.Wellknown(DTODistributionChannel.Wellknowns.Diversos))).OrderBy(Function(x) x.Nom.Tradueix(Current.Session.Lang))
            Dim oTreeNode As TreeNode = GetNode(oValue)
            oNodeDist.Nodes.Add(oTreeNode)
            _NodeList.Add(oTreeNode)
        Next
        oNodeDist.ExpandAll()

        Dim oNodeNoDist As New TreeNode("Altres")
        MyBase.Nodes.Add(oNodeNoDist)

        For Each oValue As DTOContactClass In _Values.Where(Function(x) x.DistributionChannel.Equals(DTODistributionChannel.Wellknown(DTODistributionChannel.Wellknowns.Diversos))).OrderBy(Function(x) x.Nom.Tradueix(Current.Session.Lang))
            Dim oTreeNode As TreeNode = GetNode(oValue)
            oNodeNoDist.Nodes.Add(oTreeNode)
            _NodeList.Add(oTreeNode)
        Next
        oNodeNoDist.ExpandAll()

        If oSelectedClass IsNot Nothing Then
            Me.SelectedValue = oSelectedClass
        End If

        SetContextMenu()
        _AllowEvents = True
    End Sub

    Public ReadOnly Property Values As List(Of DTOContactClass)
        Get
            Return _Values
        End Get
    End Property

    Public ReadOnly Property SelectedClass As DTOContactClass
        Get
            Dim retval As DTOContactClass = Nothing
            Dim oNode As TreeNode = MyBase.SelectedNode
            If oNode IsNot Nothing Then
                retval = oNode.Tag
            End If
            Return retval
        End Get
    End Property

    Public WriteOnly Property SelectedValue() As DTOContactClass
        Set(value As DTOContactClass)
            Dim oNode As TreeNode = _NodeList.Find(Function(x) DirectCast(x.Tag, DTOContactClass).Guid.Equals(value.Guid))
            If oNode IsNot Nothing Then
                MyBase.SelectedNode = oNode
            End If
        End Set
    End Property

    Private Function GetNode(oValue As DTOContactClass) As TreeNode
        Dim retval As New TreeNode(oValue.Nom.Tradueix(_Lang))
        retval.Tag = oValue
        Return retval
    End Function

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oNode As TreeNode = MyBase.SelectedNode

        If oNode IsNot Nothing Then
            If oNode.Tag IsNot Nothing Then
                Dim oContactClass As DTOContactClass = oNode.Tag
                Dim oClass_Menu As New Menu_ContactClass(oContactClass)
                AddHandler oClass_Menu.AfterUpdate, AddressOf RefreshRequest
                oContextMenu.Items.AddRange(oClass_Menu.Range)
                oContextMenu.Items.Add("-")
            End If
        End If
        oContextMenu.Items.Add("afegir", Nothing, AddressOf Do_AddNew)
        oContextMenu.Items.Add("afegir arrel", Nothing, AddressOf Do_AddRoot)

        MyBase.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub RefreshRequest(sender As Object, e As MatEventArgs)
        RaiseEvent RequestToRefresh(Me, e)
    End Sub

    Private Sub Do_AddNew()
        RaiseEvent RequestToAddNew(Me, MatEventArgs.Empty)
    End Sub

    Private Sub Do_AddRoot()
        RaiseEvent RequestToAddRoot(Me, MatEventArgs.Empty)
    End Sub


    Private Sub Xl_ContactClassesTree_NodeMouseDoubleClick(sender As Object, e As TreeNodeMouseClickEventArgs) Handles Me.NodeMouseDoubleClick
        SetContextMenu()
        RaiseEvent ItemSelected(Me, New MatEventArgs(e.Node.Tag))
    End Sub

    Private Sub Xl_ContactClassesTree_NodeMouseClick(sender As Object, e As TreeNodeMouseClickEventArgs) Handles Me.NodeMouseClick
        SetContextMenu()
    End Sub
End Class


