Public Class Xl_CheckedTreeView
    Inherits TreeView

    Public Enum CheckedState
        UnInitialised = -1
        UnChecked
        Checked
        Mixed
    End Enum

    Public Property IgnoreClickAction As Integer = 0

    Public Event NodeSelectedChanged(sender As Object, e As MatEventArgs)

    Public Sub New()
        MyBase.New
        MyBase.StateImageList = New System.Windows.Forms.ImageList()
        For i As Integer = 0 To 3 - 1
            Dim bmp As System.Drawing.Bitmap = New System.Drawing.Bitmap(16, 16)
            Dim chkGraphics As System.Drawing.Graphics = System.Drawing.Graphics.FromImage(bmp)
            Select Case i
                Case CheckedState.UnChecked
                    CheckBoxRenderer.DrawCheckBox(chkGraphics, New Point(0, 1), VisualStyles.CheckBoxState.UncheckedNormal)
                Case CheckedState.Checked
                    CheckBoxRenderer.DrawCheckBox(chkGraphics, New Point(0, 1), VisualStyles.CheckBoxState.CheckedNormal)
                Case CheckedState.Mixed
                    CheckBoxRenderer.DrawCheckBox(chkGraphics, New Point(0, 1), VisualStyles.CheckBoxState.MixedNormal)
            End Select

            MyBase.StateImageList.Images.Add(bmp)
        Next
    End Sub

    Protected Overrides Sub OnCreateControl()
        MyBase.OnCreateControl()
        CheckBoxes = False
        _IgnoreClickAction += 1
        UpdateChildState(Me.Nodes, CInt(CheckedState.UnChecked), False, True)
        _IgnoreClickAction -= 1
    End Sub

    Protected Overrides Sub OnAfterCheck(ByVal e As System.Windows.Forms.TreeViewEventArgs)
        MyBase.OnAfterCheck(e)
        If _IgnoreClickAction > 0 Then
            Return
        End If

        _IgnoreClickAction += 1
        Dim tn As System.Windows.Forms.TreeNode = e.Node
        tn.StateImageIndex = If(tn.Checked, CInt(CheckedState.Checked), CInt(CheckedState.UnChecked))
        UpdateChildState(e.Node.Nodes, e.Node.StateImageIndex, e.Node.Checked, False)
        UpdateParentState(e.Node.Parent)
        _IgnoreClickAction -= 1
    End Sub

    Protected Overrides Sub OnAfterExpand(ByVal e As System.Windows.Forms.TreeViewEventArgs)
        MyBase.OnAfterExpand(e)
        _IgnoreClickAction += 1
        UpdateChildState(e.Node.Nodes, e.Node.StateImageIndex, e.Node.Checked, True)
        _IgnoreClickAction -= 1
    End Sub

    Protected Sub UpdateChildState(ByVal Nodes As System.Windows.Forms.TreeNodeCollection, ByVal StateImageIndex As Integer, ByVal Checked As Boolean, ByVal ChangeUninitialisedNodesOnly As Boolean)
        For Each tnChild As System.Windows.Forms.TreeNode In Nodes
            If Not ChangeUninitialisedNodesOnly OrElse tnChild.StateImageIndex = -1 Then
                tnChild.StateImageIndex = StateImageIndex
                tnChild.Checked = Checked
                If tnChild.Nodes.Count > 0 Then
                    UpdateChildState(tnChild.Nodes, StateImageIndex, Checked, ChangeUninitialisedNodesOnly)
                End If
            End If
        Next
    End Sub


    Protected Sub UpdateParentState(ByVal tn As System.Windows.Forms.TreeNode)
        If tn Is Nothing Then Return
        Dim OrigStateImageIndex As Integer = tn.StateImageIndex
        Dim UnCheckedNodes As Integer = 0, CheckedNodes As Integer = 0, MixedNodes As Integer = 0
        For Each tnChild As System.Windows.Forms.TreeNode In tn.Nodes
            If tnChild.StateImageIndex = CInt(CheckedState.Checked) Then
                CheckedNodes += 1
            ElseIf tnChild.StateImageIndex = CInt(CheckedState.Mixed) Then
                MixedNodes += 1
                Exit For
            Else
                UnCheckedNodes += 1
            End If
        Next

        If MixedNodes = 0 Then
            If UnCheckedNodes = 0 Then
                tn.Checked = True
            Else
                tn.Checked = False
            End If
        End If

        If MixedNodes > 0 Then
            tn.StateImageIndex = CInt(CheckedState.Mixed)
        ElseIf CheckedNodes > 0 AndAlso UnCheckedNodes = 0 Then
            If tn.Checked Then tn.StateImageIndex = CInt(CheckedState.Checked) Else tn.StateImageIndex = CInt(CheckedState.Mixed)
        ElseIf CheckedNodes > 0 Then
            tn.StateImageIndex = CInt(CheckedState.Mixed)
        Else
            If tn.Checked Then tn.StateImageIndex = CInt(CheckedState.Mixed) Else tn.StateImageIndex = CInt(CheckedState.UnChecked)
        End If

        If OrigStateImageIndex <> tn.StateImageIndex AndAlso tn.Parent IsNot Nothing Then
            UpdateParentState(tn.Parent)
        End If
    End Sub

    Protected Overrides Sub OnKeyDown(ByVal e As System.Windows.Forms.KeyEventArgs)
        MyBase.OnKeyDown(e)
        If e.KeyCode = System.Windows.Forms.Keys.Space Then
            SelectedNode.Checked = Not SelectedNode.Checked
        End If
    End Sub

    Protected Overrides Sub OnNodeMouseClick(ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs)
        MyBase.OnNodeMouseClick(e)
        Dim info As System.Windows.Forms.TreeViewHitTestInfo = HitTest(e.X, e.Y)
        If info Is Nothing Then
            'ignora-ho
        ElseIf info.Location <> System.Windows.Forms.TreeViewHitTestLocations.StateImage Then
            RaiseEvent NodeSelectedChanged(Me, New MatEventArgs(e.Node.Tag))
        Else
            Dim tn As System.Windows.Forms.TreeNode = e.Node
            tn.Checked = Not tn.Checked
        End If
    End Sub


End Class
