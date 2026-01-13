

Public Class Xl_MMC_tv
    Inherits System.Windows.Forms.UserControl

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'UserControl overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents TreeView1 As System.Windows.Forms.TreeView
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Xl_MMC_tv))
        Me.TreeView1 = New System.Windows.Forms.TreeView()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.SuspendLayout()
        '
        'TreeView1
        '
        Me.TreeView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TreeView1.ImageIndex = 0
        Me.TreeView1.ImageList = Me.ImageList1
        Me.TreeView1.Location = New System.Drawing.Point(0, 0)
        Me.TreeView1.Name = "TreeView1"
        Me.TreeView1.SelectedImageIndex = 1
        Me.TreeView1.Size = New System.Drawing.Size(150, 131)
        Me.TreeView1.TabIndex = 0
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "")
        Me.ImageList1.Images.SetKeyName(1, "")
        '
        'Xl_MMC_tv
        '
        Me.Controls.Add(Me.TreeView1)
        Me.Name = "Xl_MMC_tv"
        Me.Size = New System.Drawing.Size(150, 131)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private ImgIdx As Integer

    Public Event onSelectedNode(ByVal sender As Object, e As MatEventArgs)

    Public Shadows Function Load() As Boolean
        Dim retval As Boolean = refresca()
        Return retval
    End Function

    Private Function refresca() As Boolean
        'Dim oRoot As MMC = WinMenuLoader.RootNodeFromRol(BLL.BLLApp.Emp, BLL.BLLSession.Current.User.Rol)
        'TreeView1.Nodes.Clear()
        'AddNodes(oRoot)
        'TreeView1.ExpandAll()
        'Return True
    End Function

    Private Sub RefreshRequest(sender As Object, e As System.EventArgs)
        Dim exs as New List(Of exception)
        If Not refresca() Then
            MsgBox("error al actualitzar el menu" & vbCrLf & BLL.Defaults.ExsToMultiline(exs), MsgBoxStyle.Exclamation)
        End If
    End Sub

    Private Sub AddNodes(ByVal ParentMMC As MMC, Optional ByVal oParentNode As TreeNode = Nothing)
        Dim oMMC As MMC
        Dim oNode As MaxiSrvr.TreeNodeObj
        For Each oMMC In ParentMMC.Children
            If oMMC.Cod = MMC.Cods.Folder Then

                oNode = New MaxiSrvr.TreeNodeObj
                oNode.Obj = oMMC

                'ho treiem per no provocar setitm a l'arbre

                'If Not oMMC.ImgSmall Is Nothing Then
                'ImageList1.Images.Add(oMMC.ImgSmall)
                'oNode.ImageIndex = ImgIdx
                'ImgIdx = ImgIdx + 1
                'End If

                'If Not oMMC.ImgSmallSelected Is Nothing Then
                'ImageList1.Images.Add(oMMC.ImgSmallSelected)
                'oNode.ImageIndex = ImgIdx
                'ImgIdx = ImgIdx + 1
                'End If

                With oNode
                    .Text = oMMC.Nom
                End With

                If oParentNode Is Nothing Then
                    TreeView1.Nodes.Add(oNode)
                Else
                    oParentNode.Nodes.Add(oNode)
                End If
                AddNodes(oMMC, oNode)
            End If
        Next
    End Sub

    Private Sub TreeView1_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles TreeView1.AfterSelect
        Dim oMmc As MMC = CurrentMMC()
        Dim oEventArgs As New MatEventArgs(oMmc)
        RaiseEvent onSelectedNode(Me, oEventArgs)
    End Sub

    Private Function CurrentMMC() As MMC
        Dim oNode As MaxiSrvr.TreeNodeObj
        oNode = TreeView1.SelectedNode
        Return CType(oNode.Obj, MMC)
    End Function


    Private Sub Do_Zoom(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oMmc As MMC = CurrentMMC()
        ShowMmc(oMmc)
    End Sub


    Private Sub Do_AddChild(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oCurrent As MMC = CurrentMMC()
        Dim oMmc As New MMC(BLL.BLLApp.Emp)
        oMmc.Parent = oCurrent
        ShowMmc(oMmc)
    End Sub

    Private Sub Do_AddSibling(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oCurrent As MMC = CurrentMMC()
        Dim oMmc As New MMC(BLL.BLLApp.Emp)
        oMmc.Parent = oCurrent.Parent
        ShowMmc(oMmc)
    End Sub

    Private Sub ShowMmc(oMmc As MMC)
        Dim oFrm As New Frm_MMC(oMmc)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub MenuItemRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        RefreshRequest(sender, e)
    End Sub


    Private Sub TreeView1_NodeMouseClick(sender As Object, e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles TreeView1.NodeMouseClick
        If BLL.BLLSession.Current.User.Rol.IsSuperAdmin Then
            Dim oContextMenu As New ContextMenuStrip

            With oContextMenu.Items
                .Add(New ToolStripMenuItem("zoom", Nothing, AddressOf Do_Zoom))
                .Add(New ToolStripMenuItem("add child", Nothing, AddressOf Do_AddChild))
                .Add(New ToolStripMenuItem("add sibling", Nothing, AddressOf Do_AddSibling))
            End With

            TreeView1.ContextMenuStrip = oContextMenu
        End If
    End Sub
End Class
