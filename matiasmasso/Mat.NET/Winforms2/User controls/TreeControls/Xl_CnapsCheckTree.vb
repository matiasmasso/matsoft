Public Class Xl_CnapsCheckTree

    Inherits TreeView

    Private _Lang As DTOLang
    Private _Values As List(Of DTOCnap)
    Private _NodeList As List(Of TreeNode) = Nothing
    Public Property IgnoreClickAction As Integer = 0

    Public Event RequestToRefresh(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)
    Public Event ItemSelected(sender As Object, e As MatEventArgs)

    Private _AllowEvents As Boolean

    Public Enum CheckedState
        UnInitialised = -1
        UnChecked
        Checked
        Mixed
    End Enum

    Public Shadows Sub Load(values As List(Of DTOCnap), Optional oSelectedCnaps As List(Of DTOCnap) = Nothing, Optional oLang As DTOLang = Nothing)
        If oLang Is Nothing Then oLang = DTOApp.current.lang

        _Values = values
        _Lang = oLang
        _NodeList = New List(Of TreeNode)


        MyBase.Nodes.Clear()
        Dim oRootValues As List(Of DTOCnap) = _Values.Where(Function(x) x.Parent Is Nothing).ToList
        SetNodes(Me, oRootValues)

        If oSelectedCnaps IsNot Nothing Then
            For Each item In oSelectedCnaps
                Dim oNodesToCheck = _NodeList.Where(Function(x) item.IsSelfOrChildOf(x.Tag))
                For Each oNode In oNodesToCheck
                    oNode.Checked = True
                Next
            Next
        End If

        SetContextMenu()
        _AllowEvents = True
    End Sub

    Private Sub SetNodes(ByRef oParentNode As Object, ByRef oValues As List(Of DTOCnap))
        For Each oValue In oValues
            Dim oNode As New TreeNode(oValue.ShortFullNom(_Lang))
            _NodeList.Add(oNode)
            oNode.Tag = oValue
            If TypeOf oParentNode Is TreeView Then
                DirectCast(oParentNode, TreeView).Nodes.Add(oNode)
            ElseIf TypeOf oParentNode Is TreeNode Then
                DirectCast(oParentNode, TreeNode).Nodes.Add(oNode)
            End If
            If oValue.Children IsNot Nothing Then
                SetNodes(oNode, oValue.Children)
            End If
        Next
    End Sub

    Public ReadOnly Property CheckedValues As List(Of DTOCnap)
        Get
            Dim retval As New List(Of DTOCnap)
            For Each oNode In _NodeList.Where(Function(x) x.Checked)
                If oNode.Parent Is Nothing Then
                    retval.Add(oNode.Tag)
                ElseIf Not oNode.parent.Checked Then
                    retval.Add(oNode.Tag)
                End If
            Next
            Return retval
        End Get
    End Property

    Public ReadOnly Property Values As List(Of DTOCnap)
        Get
            Return _Values
        End Get
    End Property

    Public ReadOnly Property SelectedClass As DTOCnap
        Get
            Dim retval As DTOCnap = Nothing
            Dim oNode As TreeNode = MyBase.SelectedNode
            If oNode IsNot Nothing Then
                retval = oNode.Tag
            End If
            Return retval
        End Get
    End Property

    Public WriteOnly Property SelectedValue() As DTOCnap
        Set(value As DTOCnap)
            Dim oNode As TreeNode = _NodeList.Find(Function(x) DirectCast(x.Tag, DTOCnap).Guid.Equals(value.Guid))
            If oNode IsNot Nothing Then
                MyBase.SelectedNode = oNode
            End If
        End Set
    End Property


    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oNode As TreeNode = MyBase.SelectedNode

        If oNode IsNot Nothing Then
            If oNode.Tag IsNot Nothing Then
                Dim oCnap As DTOCnap = oNode.Tag
                Dim oClass_Menu As New Menu_Cnap(oCnap)
                AddHandler oClass_Menu.AfterUpdate, AddressOf RefreshRequest
                oContextMenu.Items.AddRange(oClass_Menu.Range)
            End If
        End If

        MyBase.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub RefreshRequest(sender As Object, e As MatEventArgs)
        RaiseEvent RequestToRefresh(Me, e)
    End Sub


    Private Sub Xl_CnapesTree_NodeMouseDoubleClick(sender As Object, e As TreeNodeMouseClickEventArgs) Handles Me.NodeMouseDoubleClick
        SetContextMenu()
        RaiseEvent ItemSelected(Me, New MatEventArgs(e.Node.Tag))
    End Sub

    Private Sub Xl_CnapesTree_NodeMouseClick(sender As Object, e As TreeNodeMouseClickEventArgs) Handles Me.NodeMouseClick
        SetContextMenu()
    End Sub

    Public Sub New()
        MyBase.New
        MyBase.StateImageList = New System.Windows.Forms.ImageList()
        For i As Integer = 0 To 3 - 1
            Dim bmp As System.Drawing.Bitmap = New System.Drawing.Bitmap(16, 16)
            Dim chkGraphics As System.Drawing.Graphics = System.Drawing.Graphics.FromImage(bmp)
            Select Case i
                Case 0
                    CheckBoxRenderer.DrawCheckBox(chkGraphics, New Point(0, 1), VisualStyles.CheckBoxState.UncheckedNormal)
                Case 1
                    CheckBoxRenderer.DrawCheckBox(chkGraphics, New Point(0, 1), VisualStyles.CheckBoxState.CheckedNormal)
                Case 2
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
        If info Is Nothing OrElse info.Location <> System.Windows.Forms.TreeViewHitTestLocations.StateImage Then
            Return
        End If

        Dim tn As System.Windows.Forms.TreeNode = e.Node
        tn.Checked = Not tn.Checked
    End Sub

End Class



