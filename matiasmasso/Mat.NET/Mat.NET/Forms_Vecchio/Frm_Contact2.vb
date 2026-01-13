

Public Class Frm_Contact2
    Private mContact As Contact = Nothing
    Private mSubject As Object = Nothing
    Private mControls() As Control
    Private mVisiblePanel As Control
    Private mRootNode As maxisrvr.TreeNodeObj
    Private mGralNode As maxisrvr.TreeNodeObj
    Private mDirtyClx As Boolean = False

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Private Enum Panels
        NotSet
        Gral
        Client
        Proveidor
        Staff
        Rep
        Banc
    End Enum

    Private Enum NodeCods
        NotSet
        Root
        Panel
    End Enum

    Public Sub New(ByVal oContact As Contact)
        MyBase.New()
        Me.InitializeComponent()
        ReDim mControls([Enum].GetValues(GetType(Panels)).Length)
        mContact = oContact
        If mContact.Id = 0 Then
            Me.Text = "NOU CONTACTE"
        Else
            Me.Text = "CONTACTE #" & mContact.Id & ": " & mContact.Clx
        End If
    End Sub

    Public Sub New(ByVal oProveidor As Proveidor)
        MyBase.New()
        Me.InitializeComponent()
        ReDim mControls([Enum].GetValues(GetType(Panels)).Length)
        mSubject = oProveidor
        mContact = oProveidor
    End Sub

    Private Sub Frm_Contact2_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Xl_ImageLogo.Bitmap = mContact.Img48
        LoadTreeView()
        If mRootNode.Nodes.Count > 0 Then
            TreeView1.SelectedNode = mRootNode.Nodes(0)
        End If
        TreeView1.SelectedNode = mGralNode
    End Sub

    Private Sub LoadTreeView()
        Dim SQL As String = "SELECT " & CInt(Panels.Gral).ToString & " FROM CLIGRAL WHERE EMP=@EMP AND CLI=@CLI UNION " _
        & "SELECT " & CInt(Panels.Client).ToString & " FROM CLICLIENT WHERE EMP=@EMP AND CLI=@CLI UNION " _
        & "SELECT " & CInt(Panels.Proveidor).ToString & " FROM CLIPRV WHERE Guid='" & mContact.Guid.ToString & "' UNION " _
        & "SELECT " & CInt(Panels.Staff).ToString & " FROM CLISTAFF WHERE EMP=@EMP AND CLI=@CLI UNION " _
        & "SELECT " & CInt(Panels.Rep).ToString & " FROM CLIREP WHERE EMP=@EMP AND CLI=@CLI UNION " _
        & "SELECT " & CInt(Panels.Banc).ToString & " FROM CLIREP WHERE EMP=@EMP AND CLI=@CLI "

        mRootNode = New maxisrvr.TreeNodeObj("contacte", , NodeCods.Root)
        TreeView1.Nodes.Add(mRootNode)
        Dim odrd As SqlClient.SqlDataReader = maxisrvr.GetDataReader(SQL, maxisrvr.Databases.Maxi, "@EMP", mContact.Emp.Id, "@CLI", mContact.Id)
        Do While odrd.Read
            mRootNode.Nodes.Add(NewNode(CType(odrd(0), Panels)))
        Loop

        'at least node gral
        If mRootNode.Nodes.Count = 0 Then
            mRootNode.Nodes.Add(NewNode(Panels.Gral))
        End If

        mRootNode.ExpandAll()
    End Sub

    Private Function NewNode(ByVal oPanel As Panels) As maxisrvr.TreeNodeObj
        Dim sTxt As String = Choose(oPanel, "general", "client", "proveidor", "personal", "representant", "banc")
        Dim oNode As New maxisrvr.TreeNodeObj()
        oNode.Text = sTxt
        oNode.Obj = oPanel
        oNode.Cod = NodeCods.Panel
        If oPanel = Panels.Gral Then
            mGralNode = oNode
        End If
        Return oNode
    End Function

    Private Function CurrentNode() As maxisrvr.TreeNodeObj
        Return DirectCast(TreeView1.SelectedNode, maxisrvr.TreeNodeObj)
    End Function

    Private Function CurrentNodeCod() As NodeCods
        Return DirectCast(CurrentNode.Cod, NodeCods)
    End Function

    Private Function CurrentPanel() As Panels
        Return DirectCast(CurrentNode.Obj, Panels)
    End Function

    Private Sub TreeView1_AfterSelect(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles TreeView1.AfterSelect
        SetContextMenu()
        Select Case CurrentNodeCod()
            Case NodeCods.Panel
                ShowPanel(CurrentPanel)
        End Select
    End Sub

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Select Case CurrentNodeCod()
            Case NodeCods.Root
                Dim oMenuItemAddNew As New ToolStripMenuItem("afegir")
                oContextMenu.Items.Add(oMenuItemAddNew)
                'oMenuItemAddNew.DropDownItems.AddRange(MissingPanelsMenuItems)
                AddPanelMenuIfMissing(oMenuItemAddNew, Panels.Client)
                AddPanelMenuIfMissing(oMenuItemAddNew, Panels.Proveidor)
                AddPanelMenuIfMissing(oMenuItemAddNew, Panels.Staff)
                AddPanelMenuIfMissing(oMenuItemAddNew, Panels.Rep)
                AddPanelMenuIfMissing(oMenuItemAddNew, Panels.Banc)


            Case NodeCods.Panel
                If TypeOf mVisiblePanel Is IUpdatableDetailsPanel Then
                    Dim oMenuItemDel As New ToolStripMenuItem("eliminar", Nothing, AddressOf DeletePanel)
                    Dim exs as new list(Of Exception)
                    oMenuItemDel.Enabled = DirectCast(mVisiblePanel, IUpdatableDetailsPanel).AllowDelete( exs)
                    oContextMenu.Items.Add(oMenuItemDel)
                End If

        End Select
        TreeView1.ContextMenuStrip = oContextMenu
    End Sub


    Private Sub AddPanelMenuIfMissing(ByVal oParentMenu As ToolStripMenuItem, ByVal oPanel As Panels)
        If IsMissingNode(oPanel) Then
            Select Case oPanel
                Case Panels.Client
                    oParentMenu.DropDownItems.Add(New ToolStripMenuItem("client", Nothing, AddressOf AddPanelClient))
                Case Panels.Proveidor
                    oParentMenu.DropDownItems.Add(New ToolStripMenuItem("proveidor", Nothing, AddressOf AddPanelProveidor))
                Case Panels.Staff
                    oParentMenu.DropDownItems.Add(New ToolStripMenuItem("personal", Nothing, AddressOf AddPanelStaff))
                Case Panels.Rep
                    oParentMenu.DropDownItems.Add(New ToolStripMenuItem("representant", Nothing, AddressOf AddPanelRep))
                Case Panels.Banc
                    oParentMenu.DropDownItems.Add(New ToolStripMenuItem("banc", Nothing, AddressOf AddPanelBanc))
            End Select
        End If
    End Sub

    Private Function IsMissingNode(ByVal oPanel As Panels) As Boolean
        Dim retval As Boolean = True
        Dim oRootNode As TreeNode = TreeView1.Nodes(0)
        For iNodeIdx As Integer = 0 To oRootNode.Nodes.Count - 1
            Dim oExistingNodePanel As Panels = DirectCast(oRootNode.Nodes(iNodeIdx), maxisrvr.TreeNodeObj).Obj
            If oPanel = oExistingNodePanel Then
                retval = False
                Exit For
            End If
        Next
        Return retval
    End Function

    Private Sub AddPanelClient()
        AddOrInsertNewPanel(Panels.Client)
    End Sub

    Private Sub AddPanelProveidor()
        AddOrInsertNewPanel(Panels.Proveidor)
    End Sub

    Private Sub AddPanelStaff()
        AddOrInsertNewPanel(Panels.Staff)
    End Sub

    Private Sub AddPanelRep()
        AddOrInsertNewPanel(Panels.Rep)
    End Sub

    Private Sub AddPanelBanc()
        AddOrInsertNewPanel(Panels.Banc)
    End Sub

    Private Sub AddOrInsertNewPanel(ByVal oPanel As Panels)
        Dim oRootNode As TreeNode = TreeView1.Nodes(0)
        Dim oNodeToAdd As maxisrvr.TreeNodeObj = NewNode(oPanel)
        Dim BlNodeAlreadyInserted As Boolean = False

        For iNodeIdx As Integer = 0 To oRootNode.Nodes.Count - 1
            Dim oExistingNodePanel As Panels = DirectCast(oRootNode.Nodes(iNodeIdx), maxisrvr.TreeNodeObj).Obj
            If CInt(oPanel) < oExistingNodePanel Then
                oRootNode.Nodes.Insert(iNodeIdx, oNodeToAdd)
                BlNodeAlreadyInserted = True
            End If
        Next
        If Not BlNodeAlreadyInserted Then
            oRootNode.Nodes.Add(oNodeToAdd)
        End If
    End Sub

    Private Sub ShowPanel(ByVal oPanel As Panels)
        If mVisiblePanel IsNot Nothing Then
            mVisiblePanel.Visible = False
        End If

        If mControls(oPanel) Is Nothing Then
            Select Case oPanel
                Case Panels.Gral
                    mVisiblePanel = New Xl_Contact2_Gral(mContact)
                Case Panels.Client
                    mVisiblePanel = New Xl_Contact2_Client(mContact)
                Case Panels.Proveidor
                    If TypeOf mSubject Is Proveidor Then
                        mVisiblePanel = New Xl_Contact2_Proveidor(CType(mSubject, Proveidor))
                    Else
                        mVisiblePanel = New Xl_Contact2_Proveidor(New Proveidor(mContact.Guid))
                    End If
                Case Panels.Staff
                    If TypeOf mSubject Is Staff Then
                        mVisiblePanel = New Xl_Contact2_Staff(CType(mSubject, Staff))
                    Else
                        mVisiblePanel = New Xl_Contact2_Staff(Staff.FromNum(mContact.Emp, mContact.Id))
                    End If

                Case Panels.Rep
                Case Panels.Banc
            End Select

            mControls(oPanel) = mVisiblePanel
            PanelHost.Controls.Add(mVisiblePanel)
            mVisiblePanel.Dock = DockStyle.Fill
            AddHandler CType(mVisiblePanel, IUpdatableDetailsPanel).AfterUpdate, AddressOf AfterDirtyPanel
        Else
            mVisiblePanel = mControls(oPanel)
        End If

        mVisiblePanel.Visible = True
    End Sub

    Private Sub AfterDirtyPanel(ByVal sender As Object, ByVal e As System.EventArgs)
        ButtonOk.Enabled = True
    End Sub

    Private Sub DeletePanel()

    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        Dim exs as new list(Of Exception)
        For Each oControl As Control In PanelHost.Controls
            If TypeOf oControl Is IUpdatableDetailsPanel Then
                DirectCast(oControl, IUpdatableDetailsPanel).UpdateIfDirty( exs)
            End If
        Next

        If mDirtyClx Then
            mContact.Img48 = Xl_ImageLogo.Bitmap
            mContact.UpdateClx( exs)
        End If

        If exs.Count > 0 Then
            MsgBox("algunes dades poden no haver-se grabat:" & vbCrLf & BLL.Defaults.ExsToMultiline(exs), MsgBoxStyle.Exclamation)
        End If

        RaiseEvent AfterUpdate(mContact.clon, EventArgs.Empty)

        Me.Close()
    End Sub

    Private Sub CLX_AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
    Xl_ImageLogo.AfterUpdate

        MDirtyClx = True
        ButtonOk.Enabled = True

    End Sub
End Class

Public Interface IUpdatableDetailsPanel
    Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)
    Function UpdateIfDirty(ByRef exs as List(Of exception)) As Boolean
    Function AllowDelete(ByRef exs as List(Of exception)) As Boolean
    Function Delete(ByRef exs as List(Of exception)) As Boolean
End Interface



