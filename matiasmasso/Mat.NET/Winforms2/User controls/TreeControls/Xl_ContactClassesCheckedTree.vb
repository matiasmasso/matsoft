Public Class Xl_ContactClassesCheckedTree
    Inherits Xl_CheckedTreeView

    Private _CheckedClasses As DTOContactClass.Collection
    Private _AllowEvents As Boolean
    Public Property isDirty As Boolean


    Public Shadows Sub Load(values As DTOContactClass.Collection, Optional oCheckedValues As DTOContactClass.Collection = Nothing)
        MyBase.CheckBoxes = True

        Dim oTopNodeTag As Object = MyBase.TopNodeTag()
        Me.SuspendLayout()

        MyBase.Nodes.Clear()
        For Each oParent In values.Channels()
            Dim oParentNode As New TreeNode()
            oParentNode.Tag = oParent
            For Each oChild In values.Where(Function(x) x.DistributionChannel.Equals(oParent))
                Dim oChildNode As New TreeNode()
                oChildNode.Tag = oChild
                oChildNode.Text = oChild.Nom.Tradueix(Current.Session.Lang)
                If oChild.Equals(oTopNodeTag) Then MyBase.TopNode = oChildNode
                oParentNode.Nodes.Add(oChildNode)
            Next
            If oParentNode.Nodes.Count > 0 Then
                oParentNode.Tag = oParent
                oParentNode.Text = oParent.LangText.Tradueix(Current.Session.Lang)
                If MyBase.ExpandedTags.Contains(oParent) Then oParentNode.Expand()
                If oParent.Equals(oTopNodeTag) Then MyBase.TopNode = oParentNode
                MyBase.Nodes.Add(oParentNode)
            End If
        Next

        Me.ResumeLayout()
        _AllowEvents = True
    End Sub



    Public Function CheckedValues() As DTOContactClass.Collection
        Dim retval As New DTOContactClass.Collection
        For Each oParentNode In MyBase.Nodes
            For Each oChildNode As TreeNode In oParentNode.nodes
                If oChildNode.Checked Then
                    retval.Add(oChildNode.Tag)
                End If
            Next
        Next
        Return retval
    End Function

End Class
