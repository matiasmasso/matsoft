

Public Class Frm_Faqs
    Private Sub Frm_Faqs_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        LoadChildNodes(Faq.Root, Nothing)
    End Sub

    Private Sub LoadChildNodes(ByVal oParentFaq As Faq, ByVal oParentNode As maxisrvr.TreeNodeObj)
        Dim oNode As maxisrvr.TreeNodeObj = Nothing
        For Each oFaq As Faq In oParentFaq.Children(BLL.BLLSession.Current.User.Rol.Id)
            oNode = New MaxiSrvr.TreeNodeObj(oFaq.Question.Text, oFaq)
            If oParentNode Is Nothing Then
                TreeView1.Nodes.Add(oNode)
            Else
                oParentNode.Nodes.Add(oNode)
            End If
            LoadChildNodes(oFaq, oNode)
        Next
    End Sub

    Private Function CurrentNode() As maxisrvr.TreeNodeObj
        Dim oCurrentNode As maxisrvr.TreeNodeObj = TreeView1.SelectedNode
        Return oCurrentNode
    End Function

    Private Function CurrentFaq() As Faq
        Dim oFaq As Faq = Nothing
        Dim oNode As maxisrvr.TreeNodeObj = CurrentNode()
        If oNode IsNot Nothing Then
            oFaq = oNode.Obj
        End If
        Return oFaq
    End Function

    Private Sub AfegirToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AfegirToolStripMenuItem.Click
        Dim oParentFaq As Faq = CurrentFaq()
        If oParentFaq Is Nothing Then oParentFaq = Faq.Root
        Dim oFaq As Faq = oParentFaq.NewChild
        Dim oFrm As New Frm_Faq(oFaq)
        AddHandler oFrm.AfterUpdate, AddressOf AfterAddNode
        oFrm.Show()
    End Sub

    Private Sub AfterAddNode(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFaq As Faq = sender
        Dim oNode As maxisrvr.TreeNodeObj = New maxisrvr.TreeNodeObj(oFaq.Question.Text, oFaq)
        If oFaq.Parent.Guid = Faq.Root.Guid Then
            TreeView1.Nodes.Add(oNode)
        Else
            Dim oParentNode As maxisrvr.TreeNodeObj = CurrentNode()
            If CType(oParentNode.Obj, Faq).Guid <> oFaq.Parent.Guid Then
                oParentNode = GetNodeFromFaq(oFaq.Parent, Nothing)
            End If
            oParentNode.Nodes.Add(oNode)
        End If
    End Sub

    Private Function GetNodeFromFaq(ByVal oParentFaq As Faq, ByVal oParentNode As maxisrvr.TreeNodeObj) As maxisrvr.TreeNodeObj
        Dim oNode As maxisrvr.TreeNodeObj = Nothing
        Dim oChildNode As maxisrvr.TreeNodeObj = Nothing
        Dim oNodes As TreeNodeCollection
        If oParentNode Is Nothing Then
            oNodes = TreeView1.Nodes
        Else
            oNodes = oParentNode.Nodes
        End If

        For Each oNode In oNodes
            If CType(oNode.Obj, Faq).Guid = oParentFaq.Guid Then
                Exit For
            Else
                oChildNode = GetNodeFromFaq(oParentFaq, oNode)
                If oChildNode IsNot Nothing Then
                    oNode = oChildNode
                    Exit For
                End If
            End If
        Next
        Return oNode
    End Function

    Private Sub EliminarToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles EliminarToolStripMenuItem.Click
        Dim oNode As maxisrvr.TreeNodeObj = CurrentNode()
        Dim oFaq As Faq = CType(oNode.Obj, Faq)
        If oFaq.Children(DTORol.Ids.SuperUser).Count > 0 Then
            MsgBox("aquest nodo no esta buit!", MsgBoxStyle.Exclamation, "MAT.NET")
        Else
            oFaq.Delete()
            TreeView1.Nodes.Remove(oNode)
        End If
    End Sub

    Private Sub ZoomToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ZoomToolStripMenuItem.Click
        ZOOM()
    End Sub

    Private Sub AfterUpdateNode(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFaq As Faq = sender
        Dim oNode As maxisrvr.TreeNodeObj = CurrentNode()
        If CType(oNode.Obj, Faq).Guid <> oFaq.Guid Then
            oNode = GetNodeFromFaq(oFaq, TreeView1.TopNode)
        End If
        oNode.Text = oFaq.Question.Text
    End Sub



    Private Sub PujaToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PujaToolStripMenuItem.Click
        If (CurrentNode.Parent IsNot Nothing) Then
            MoveCurrentNode(CurrentNode.Index - 1)
            SortCurrentLeaf()
        End If
    End Sub

    Private Sub BaixaToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BaixaToolStripMenuItem.Click
        If (CurrentNode.Parent IsNot Nothing) Then
            MoveCurrentNode(CurrentNode.Index - 1)
            SortCurrentLeaf()
        End If
    End Sub

    Private Sub MoveCurrentNode(ByVal iNewIdx As Integer)
        Dim oCurrentFaq As Faq = CurrentNode.Obj
        Dim oClone As New maxisrvr.TreeNodeObj(oCurrentFaq.Question.Text, oCurrentFaq)
        CurrentNode.Parent.Nodes.Insert(iNewIdx, oClone)
        CurrentNode.Remove()
        TreeView1.SelectedNode = oClone
    End Sub

    Private Sub SortCurrentLeaf()
        Dim oFaq As Faq
        Dim oNode As maxisrvr.TreeNodeObj
        Dim oParent As TreeNode = CurrentNode.Parent
        For i As Integer = 0 To oParent.Nodes.Count - 1
            oNode = CType(oParent.Nodes(i), maxisrvr.TreeNodeObj)
            oFaq = CType(oNode.Obj, Faq)
            oFaq.Ord = oNode.Index
            oFaq.Update()
        Next
    End Sub

    Private Sub CopyLinkToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CopyLinkToolStripMenuItem.Click
        Dim sUrl As String = CurrentFaq.Url(BLL.BLLApp.Emp, True)
        Clipboard.SetDataObject(sUrl, True)
    End Sub

    Private Sub TreeView1_NodeMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles TreeView1.NodeMouseDoubleClick
        Zoom()
    End Sub

    Private Sub Zoom()
        Dim oFaq As Faq = CurrentFaq()
        Dim oFrm As New Frm_Faq(oFaq)
        AddHandler oFrm.AfterUpdate, AddressOf AfterUpdateNode
        oFrm.Show()
    End Sub

    Private Sub WebToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles WebToolStripMenuItem.Click
        Dim sUrl As String = CurrentFaq.Url(BLL.BLLApp.Emp)
        UIHelper.ShowHtml(sUrl)
    End Sub
End Class