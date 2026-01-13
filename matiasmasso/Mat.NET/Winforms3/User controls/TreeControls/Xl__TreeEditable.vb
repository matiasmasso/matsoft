Imports DTO.DTOSession
Imports Newtonsoft.Json

Public MustInherit Class Xl__TreeEditable
    Inherits TreeView
    Private _settingsKey As String
    Public Event RequestToUpdate(sender As Object, e As MatEventArgs)

    Public Sub Load(values As IEnumerable(Of DTOGuidNomNode))
        _settingsKey = Me.Name
        MyBase.LabelEdit = True
        MyBase.AllowDrop = True
        addNodes(MyBase.Nodes, values)
        resetLayout()
        setContextMenu()
    End Sub

    Private Function CurrentNode() As TreeNode
        Return MyBase.SelectedNode
    End Function

    Public Function treeNode(value As DTOGuidNomNode) As TreeNode
        Dim retval As New TreeNode(DirectCast(value, DTOGuidNomNode).Nom)
        retval.Tag = value
        Return retval
    End Function

    Private Sub addNodes(oNodes As TreeNodeCollection, oValues As IEnumerable(Of DTOGuidNomNode))
        For Each oValue As DTOGuidNomNode In oValues
            Dim oNode As TreeNode = treeNode(oValue)
            oNodes.Add(oNode)
            addNodes(oNode.Nodes, oValue.Children)
        Next
    End Sub

    Private Sub Xl_TreeEditable_NodeMouseClick(sender As Object, e As TreeNodeMouseClickEventArgs) Handles Me.NodeMouseClick
        If e.Button = MouseButtons.Right Then
            setContextMenu(e.Node)
        End If
    End Sub

    Private Sub setContextMenu(Optional oNode As TreeNode = Nothing)
        Dim oContextmenu As ContextMenu = GetContextmenu(oNode)
        If oNode Is Nothing Then
            MyBase.ContextMenu = oContextmenu
        Else
            oNode.ContextMenu = oContextmenu
        End If
    End Sub

    Protected MustOverride Function GetContextmenu(Optional oNode As TreeNode = Nothing) As ContextMenu
    Protected MustOverride Async Function UpdateValue(oValue As DTOGuidNomNode) As Task

    Private Async Sub treeView1_AfterLabelEdit(ByVal sender As Object, ByVal e As NodeLabelEditEventArgs) Handles MyBase.AfterLabelEdit
        Dim oValue As DTOGuidNomNode = e.Node.Tag
        oValue.Nom = e.Label
        Await UpdateValue(oValue)
    End Sub

#Region "DragDrop"
    Public Sub TreeView_ItemDrag(ByVal sender As Object,
                                  ByVal e As ItemDragEventArgs) _
                                  Handles MyBase.ItemDrag
        DoDragDrop(e.Item, DragDropEffects.Move)
    End Sub

    Public Sub TreeView_DragEnter(ByVal sender As Object,
                               ByVal e As DragEventArgs) _
                               Handles MyBase.DragEnter
        e.Effect = DragDropEffects.Move
    End Sub

    Public Async Sub TreeView_DragDrop(ByVal sender As Object,
                              ByVal e As DragEventArgs) _
                              Handles MyBase.DragDrop

        If e.Data.GetDataPresent("System.Windows.Forms.TreeNode", False) Then
            Dim pt As Point = DirectCast(sender, TreeView).PointToClient(New Point(e.X, e.Y))
            Dim DestinationNode As TreeNode = DirectCast(sender, TreeView).GetNodeAt(pt)
            Dim NewNode As TreeNode = DirectCast(e.Data.GetData("System.Windows.Forms.TreeNode"), TreeNode)

            Dim ParentValue As DTOGuidNomNode = DestinationNode.Tag
            Dim ChildValue As DTOGuidNomNode = NewNode.Tag
            ChildValue.Parent = ParentValue

            DestinationNode.Nodes.Add(NewNode.Clone)
            DestinationNode.Expand()

            'Remove original node
            NewNode.Remove()

            Await UpdateValue(ChildValue)

        End If
    End Sub
#End Region


#Region "Layout"

    Public Sub resetLayout()
        Dim sLayout As String = GetSetting("Treeview layouts " & _settingsKey)
        If sLayout > "" Then
            'Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
            'Dim oLayout As Dictionary(Of String, Object) = serializer.DeserializeObject(sLayout)

            Dim oLayout As Dictionary(Of String, Object) = JsonConvert.DeserializeObject(sLayout, GetType(Dictionary(Of String, Object))) ', JsonSettings(exs))
            resetLayout(MyBase.Nodes, oLayout)
        End If
    End Sub

    Protected Sub resetLayout(nodes As TreeNodeCollection, ByRef oLayout As Dictionary(Of String, Object))
        For Each node As TreeNode In nodes
            If TypeOf node.Tag Is DTOBaseGuid Then
                Dim sGuid As String = DirectCast(node.Tag, DTOBaseGuid).Guid.ToString
                If DirectCast(oLayout("IsExpanded"), Object()).Contains(sGuid) Then node.Expand()
                'If DirectCast(oLayout("IsSelected"), Object()).Contains(sGuid) Then item.IsSelected = True
            End If
            resetLayout(node.Nodes, oLayout)
        Next
    End Sub

    Public Sub SaveLayout()
        Dim oLayout As New Dictionary(Of String, String())
        oLayout.Add("IsExpanded", {})
        oLayout.Add("IsSelected", {})
        SaveLayout(MyBase.Nodes, oLayout)

        'Dim sLayout As String = serializer.Serialize(oLayout)
        Dim serializer As New JsonSerializer() ' System.Web.Script.Serialization.JavaScriptSerializer()
        Dim settings = New JsonSerializerSettings()
        Dim sLayout As String = JsonConvert.SerializeObject(oLayout, settings)
        SaveSettingString("Treeview layouts " & _settingsKey, sLayout)
    End Sub

    Private Sub SaveLayout(nodes As TreeNodeCollection, ByRef oLayout As Dictionary(Of String, String()))
        For Each node As TreeNode In nodes
            If TypeOf node.Tag Is DTOBaseGuid Then
                Dim o As DTOBaseGuid = node.Tag
                If node.IsExpanded Then
                    Dim array As String() = oLayout("IsExpanded")
                    ReDim Preserve array(array.Length)
                    array(array.Length - 1) = o.Guid.ToString
                    oLayout("IsExpanded") = array
                End If
                If node.IsSelected Then
                    Dim array As String() = oLayout("IsSelected")
                    ReDim Preserve array(array.Length)
                    array(array.Length - 1) = o.Guid.ToString
                    oLayout("IsSelected") = array
                End If
            End If
            SaveLayout(node.Nodes, oLayout)
        Next
    End Sub




    Protected Class TreeLayout
        Property IsExpanded As String()
        Property IsSelected As String()
    End Class

#End Region



End Class
