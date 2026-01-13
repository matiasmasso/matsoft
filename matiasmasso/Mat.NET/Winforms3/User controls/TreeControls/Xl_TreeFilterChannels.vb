Public Class Xl_TreeFilterChannels
    Inherits TreeView

    Private _SelectedItems As IEnumerable(Of DTOContact)
    Private _PropertiesSet As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)


    Public Shadows Sub load(oAllItems As IEnumerable(Of DTOContact), oSelectedItems As IEnumerable(Of DTOContact))
        If Not _PropertiesSet Then SetProperties()
        If oAllItems Is Nothing Then
        ElseIf oAllItems.Count = 0 Then
        Else
            _SelectedItems = oSelectedItems
            LoadNodes(oAllItems)
        End If
    End Sub


    Public Function SelectedItems() As IEnumerable(Of DTOContact)
        Dim retval As New List(Of DTOContact)
        For Each oChannelNode In MyBase.Nodes
            For Each oClassNode In oChannelNode.nodes
                For Each oContactNode As TreeNode In oClassNode.nodes
                    If oContactNode.Checked Then
                        retval.Add(oContactNode.Tag)
                    End If
                Next
            Next
        Next
        Return retval
    End Function


    Private Sub LoadNodes(oAllItems As IEnumerable(Of DTOContact))
        Dim oLang As DTOLang = Current.Session.Lang
        Dim oChannels = DTOCustomer.DistributionChannels(oAllItems)
        Dim oClasses = DTOCustomer.ContactClasses(oAllItems)
        For Each oChannel In oChannels
            Dim oChannelNode = MyBase.Nodes.Add(oChannel.Guid.ToString, oChannel.LangText.Tradueix(oLang))
            oChannelNode.Tag = oChannel
            For Each oClass In oClasses.Where(Function(x) x.DistributionChannel.Guid.Equals(oChannel.Guid))
                Dim oClassNode = oChannelNode.Nodes.Add(oClass.Guid.ToString, oClass.Nom.Tradueix(oLang))
                oClassNode.Tag = oClass
                For Each oContact In oAllItems.Where(Function(x) x.ContactClass.Guid.Equals(oClass.Guid))
                    Dim oNode = oClassNode.Nodes.Add(oContact.Guid.ToString, oContact.FullNom)
                    oNode.Checked = _SelectedItems.Any(Function(x) x.Guid.Equals(oContact.Guid))
                    oNode.Tag = oContact
                Next
            Next
        Next

        For Each oChannelNode As TreeNode In MyBase.Nodes
            oChannelNode.Checked = True
            For Each oClassNode As TreeNode In oChannelNode.Nodes
                oClassNode.Checked = True
                For Each oContactNode As TreeNode In oClassNode.Nodes
                    If Not oContactNode.Checked Then
                        oClassNode.Checked = False
                        Exit For
                    End If
                Next
                If Not oClassNode.Checked Then
                    oChannelNode.Checked = False
                    Exit For
                End If
            Next
        Next
    End Sub


    Private Sub SetProperties()
        MyBase.CheckBoxes = True
        _PropertiesSet = True
    End Sub

    Private Sub CheckAllChildNodes(ByVal treeNode As TreeNode, ByVal nodeChecked As Boolean)
        For Each node As TreeNode In treeNode.Nodes
            node.Checked = nodeChecked

            If node.Nodes.Count > 0 Then
                Me.CheckAllChildNodes(node, nodeChecked)
            End If
        Next
    End Sub

    Private Sub node_AfterCheck(ByVal sender As Object, ByVal e As TreeViewEventArgs) Handles MyBase.AfterCheck
        If e.Action <> TreeViewAction.Unknown Then
            If e.Node.Nodes.Count > 0 Then
                Me.CheckAllChildNodes(e.Node, e.Node.Checked)
            End If
            RaiseEvent AfterUpdate(Me, New MatEventArgs(SelectedItems()))
        End If
    End Sub
End Class



