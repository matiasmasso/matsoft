Imports System.ComponentModel

''' <summary>
''' Treeview with 3-state Checkboxes.
''' Get the Treenodes 3-state Checkstate by calling ThreeStateTreeview.NodeCheckstate(Treenode)
''' </summary>
<DesignerCategory("Code")>
Public Class Xl_CheckedTreeView : Inherits TreeView
    Public Property ExpandedTags As List(Of Object)

    Private _indeterminateds As New List(Of TreeNode)
    Private _graphics As Graphics
    Private _imgIndeterminate As Image
    Private _skipCheckEvents As Boolean = False

    Public Event NodeSelectedChanged(sender As Object, e As MatEventArgs)
    Public Event AfterUpdate(sender As Object, e As MatEventArgs)


    Public Enum CheckedState
        UnInitialised = -1
        UnChecked
        Checked
        Mixed
    End Enum

    Public Sub New()
        MyBase.DrawMode = TreeViewDrawMode.OwnerDrawAll
        MyBase.CheckBoxes = True
        MyBase.StateImageList = StateImageBuilder() ' ImageList1
        _imgIndeterminate = MyBase.StateImageList.Images(2)
        _ExpandedTags = New List(Of Object)
    End Sub

    Public Function TopNodeTag() As Object
        Dim retval As Object = Nothing
        If MyBase.TopNode IsNot Nothing Then
            retval = MyBase.TopNode.Tag
        End If
        Return retval
    End Function

    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso _graphics IsNot Nothing Then
            _graphics.Dispose()
            _graphics = Nothing
        End If
        MyBase.Dispose(disposing)
    End Sub

    Protected Overrides Sub OnHandleCreated(ByVal e As EventArgs)
        MyBase.OnHandleCreated(e)
        _graphics = Me.CreateGraphics
    End Sub

    Protected Overrides Sub OnSizeChanged(ByVal e As EventArgs)
        MyBase.OnSizeChanged(e)
        If _graphics IsNot Nothing Then
            _graphics.Dispose()
            _graphics = Me.CreateGraphics
        End If
    End Sub

    Protected Overrides Sub OnBeforeCheck(ByVal e As System.Windows.Forms.TreeViewCancelEventArgs)
        If _skipCheckEvents Then Return
        MyBase.OnBeforeCheck(e)
    End Sub

    Protected Overrides Sub OnAfterCheck(ByVal e As TreeViewEventArgs)
        ' Logic: All children of an (un)checked Node inherit its Checkstate
        ' Parents recompute their state: if all children of a parent have same state, that one will be taken over as parents state
        ' otherwise take Indeterminate
        ' changing any Treenodes .Checked-Property will raise another Before- and After-Check. Skip'em
        If _skipCheckEvents Then Return
        _skipCheckEvents = True
        Try
            Dim nd As TreeNode = e.Node
            'newState is toggled from Checked to Unchecked, and from both Unchecked or Indeterminated to Checked
            Dim newState = If(NodeCheckState(nd) = CheckState.Checked, CheckState.Unchecked, CheckState.Checked)
            If (newState = CheckState.Checked) <> nd.Checked Then Return 'suppress redundant event
            AssignAndPropagateState(nd, newState)
            MyBase.OnAfterCheck(e)
            RaiseEvent AfterUpdate(Me, New MatEventArgs)
        Finally
            _skipCheckEvents = False
        End Try
    End Sub

    Public Sub AssignAndPropagateState(nd As TreeNode, newState As CheckState)
        'Check a node programmatically
        Dim newStateToChildNodes As Action(Of TreeNode) = Sub(nd0)
                                                              AssignState(nd0, newState)
                                                              For Each ndChild As TreeNode In nd0.Nodes
                                                                  newStateToChildNodes(ndChild)
                                                              Next
                                                          End Sub
        newStateToChildNodes(nd) 'call recursive anonyme Sub
        ' Parents recompute their state
        nd = nd.Parent
        Do Until nd Is Nothing
            If newState <> CheckState.Indeterminate AndAlso nd.Nodes.Cast(Of TreeNode).Any(Function(nd2) NodeCheckState(nd2) <> newState) Then
                newState = CheckState.Indeterminate
            End If
            AssignState(nd, newState)
            nd = nd.Parent
        Loop
    End Sub


    Public Sub CheckNode(nd As TreeNode)
        'Check a node programmatically
        AssignAndPropagateState(nd, CheckState.Checked)
    End Sub

    Private Sub AssignState(ByVal nd As TreeNode, ByVal state As CheckState)
        Dim ck As Boolean = state = CheckState.Checked
        Dim stateInvalid As Boolean = NodeCheckState(nd) <> state
        If stateInvalid Then NodeCheckState(nd) = state
        If nd.Checked <> ck Then
            nd.Checked = ck ' changing .Checked-Property raises Invalidating internally
        ElseIf stateInvalid Then
            ' in general: the less and small the invalidated area, the less flickering
            ' so avoid calling Invalidate() if possible - just call, if really needed.
            Me.Invalidate(GetCheckRect(nd))
        End If
    End Sub

    Public Property NodeCheckState(ByVal nd As TreeNode) As System.Windows.Forms.CheckState
        Get
            Return DirectCast(Math.Max(0, nd.StateImageIndex), CheckState)
        End Get
        Private Set(value As System.Windows.Forms.CheckState)
            nd.StateImageIndex = value
        End Set
    End Property

    Protected Overrides Sub OnDrawNode(ByVal e As DrawTreeNodeEventArgs)
        ' here nothing is drawn. Only collect Indeterminated Nodes, to draw them later (in WndProc())
        ' because drawing Treenodes properly (Text, Icon(s) Focus, Selection...) is very complicated
        If e.Node.StateImageIndex = CheckState.Indeterminate Then _indeterminateds.Add(e.Node)
        e.DrawDefault = True
        MyBase.OnDrawNode(e)
    End Sub

    Protected Overrides Sub WndProc(ByRef m As Message)
        Const WM_LBUTTONDBLCLK As Integer = &H203
        If m.Msg = WM_LBUTTONDBLCLK Then
            'fix the bug, when doubleclick on a StateImage
            Dim pt As Point = Me.PointToClient(Control.MousePosition)
            If Me.HitTest(pt).Location = TreeViewHitTestLocations.StateImage Then Return
        End If
        MyBase.WndProc(m)
        Const WM_Paint As Integer = 15
        If m.Msg = WM_Paint Then
            ' at that point the built-in drawing is completed - and I quickly paint over the indeterminated Checkboxes
            For Each nd As TreeNode In _indeterminateds
                _graphics.DrawImage(_imgIndeterminate, GetCheckRect(nd).Location)
            Next
            _indeterminateds.Clear()
        End If
    End Sub

    Private Function GetCheckRect(ByVal nd As TreeNode) As Rectangle
        With nd.Bounds
            If Me.ImageList Is Nothing Then
                Return New Rectangle(.X - 16, .Y, 16, 16)
            Else
                Return New Rectangle(.X - 35, .Y, 16, 16)
            End If
        End With
    End Function

    Protected Overrides Sub OnNodeMouseClick(ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs)
        MyBase.OnNodeMouseClick(e)
        Dim info As System.Windows.Forms.TreeViewHitTestInfo = HitTest(e.X, e.Y)
        If info Is Nothing Then
            'ignora-ho
        ElseIf info.Location <> System.Windows.Forms.TreeViewHitTestLocations.StateImage Then
            RaiseEvent NodeSelectedChanged(Me, New MatEventArgs(e.Node.Tag))
        Else
            'Dim tn As System.Windows.Forms.TreeNode = e.Node
            'tn.Checked = Not tn.Checked
        End If
    End Sub


    Private Function StateImageBuilder() As ImageList
        Dim retval As New ImageList
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

            retval.Images.Add(bmp)
        Next

        Return retval
    End Function

    Private Sub Xl_CheckedTreeView_AfterCollapse(sender As Object, e As TreeViewEventArgs) Handles Me.AfterCollapse
        _ExpandedTags.Remove(e.Node.Tag)
    End Sub

    Private Sub Xl_CheckedTreeView_AfterExpand(sender As Object, e As TreeViewEventArgs) Handles Me.AfterExpand
        _ExpandedTags.Add(e.Node.Tag)
    End Sub

    Protected Sub RaiseAfterUpdate()
        RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
    End Sub
End Class
