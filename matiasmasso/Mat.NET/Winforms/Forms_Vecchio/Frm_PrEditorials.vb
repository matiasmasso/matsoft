Imports System.Data.SqlClient


Public Class Frm_PrEditorials
    Private mEmp as DTOEmp = BLL.BLLApp.Emp
    Private mMode As Modes = Modes.Consulta
    Private mAllowEvents As Boolean = False

    Public Event AfterSelect(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Enum RootNodes
        Mitjans
        Anuncis
    End Enum

    Public Enum Modes
        Consulta
        SelectEditorial
        SelectRevista
        SelectNumero
    End Enum

    Private Enum Icons
        editorial
        revista
        revistaOberta
    End Enum

    Public Sub New(Optional ByVal oMode As Modes = Modes.Consulta)
        MyBase.New()
        Me.InitializeComponent()
        mMode = oMode
    End Sub

    Public Sub New(ByVal oMode As Modes, ByVal oEditorial As PrEditorial, ByVal oNumero As PrNumero)
        MyBase.New()
        Me.InitializeComponent()
        mMode = oMode
        refresca(oEditorial, oNumero)
    End Sub

    Private Sub refresca(ByVal oEditorial As PrEditorial, ByVal oNumero As PrNumero)
        LoadGrid()
        Dim oRevista As PrRevista = Nothing
        If oEditorial IsNot Nothing Then
            For Each oParentNode As maxisrvr.TreeNodeObj In TreeView1.Nodes
                If DirectCast(oParentNode.Obj, PrEditorial).Guid.Equals(oEditorial.Guid) Then
                    oParentNode.Expand()
                    TreeView1.SelectedNode = oParentNode
                End If
                'If oNumero.Revista IsNot Nothing Then
                'For Each oNode As maxisrvr.TreeNodeObj In oParentNode.Nodes
                'oRevista = DirectCast(oNode.Obj, PrRevista)
                'If oRevista.Guid.Equals(oNumero.Revista.Guid) Then
                'TreeView1.SelectedNode = oNode
                'SplitContainer1.Panel2.Controls.Clear()
                'ShowRevista(oRevista, oNumero)
                'Exit Sub
                'End If
                'Next
                'End If
            Next
        End If
    End Sub

    Private Sub Frm_PrEditorials_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        LoadGrid()
        mAllowEvents = True
    End Sub

    Private Sub LoadGrid()
        Dim oParentNode As maxisrvr.TreeNodeObj = Nothing
        Dim oChildNode As maxisrvr.TreeNodeObj = Nothing
        Dim SQL As String = "SELECT PRREVISTAS.EditorialGuid, CliGral.RaoSocial, PRREVISTAS.GUID, PRREVISTAS.NOM " _
        & "FROM PRREVISTAS INNER JOIN " _
        & "CliGral ON PRREVISTAS.EMP = CliGral.emp AND PRREVISTAS.EDITORIAL = CliGral.Cli " _
        & "ORDER BY CliGral.RaoSocial, PRREVISTAS.NOM"
        Dim oDrd As SqlDataReader = maxisrvr.GetDataReader(SQL, maxisrvr.Databases.Maxi, "@EMP", mEmp.Id)
        Dim sKey As String = ""
        Dim oEditorial As New PrEditorial(Guid.NewGuid)
        Dim oRevista As PrRevista = Nothing
        Do While oDrd.Read
            If Not oDrd("EditorialGuid").Equals(oEditorial.Guid) Then
                oEditorial = New PrEditorial(CType(oDrd("EditorialGuid"), Guid))
                oParentNode = New MaxiSrvr.TreeNodeObj(oDrd("RAOSOCIAL").ToString, oEditorial, , Icons.editorial, Icons.editorial)
                TreeView1.Nodes.Add(oParentNode)
            End If
            oRevista = New PrRevista(New Guid(oDrd("GUID").ToString))
            oChildNode = New maxisrvr.TreeNodeObj(oDrd("NOM").ToString, oRevista, , Icons.revista, Icons.revistaOberta)
            oParentNode.Nodes.Add(oChildNode)
        Loop
    End Sub

    Private Sub TreeView1_AfterSelect(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles TreeView1.AfterSelect

        If mAllowEvents Then
            Dim oNode As maxisrvr.TreeNodeObj = TreeView1.SelectedNode
            Dim oObj As Object = oNode.Obj
            SplitContainer1.Panel2.Controls.Clear()
            If TypeOf (oObj) Is PrEditorial Then
                ShowEditorial(oObj)
                SetContextMenu(oObj)
            Else
                ShowRevista(oObj)
            End If
        End If
    End Sub

    Private Sub TreeView1_NodeMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles TreeView1.NodeMouseClick
    End Sub

    Private Sub ShowEditorial(ByVal oEditorial As PrEditorial)
        Dim oControl As New Xl_PrEditorial_PropertyPage(oEditorial)
        oControl.Dock = DockStyle.Fill
        SplitContainer1.Panel2.Controls.Add(oControl)
    End Sub

    Private Sub ShowRevista(ByVal oRevista As PrRevista, Optional ByVal oNumero As PrNumero = Nothing)
        Dim oMode As Xl_PrRevista_PropertyPage.Modes
        Select Case mMode
            Case Modes.SelectNumero
                oMode = Xl_PrRevista_PropertyPage.Modes.SelectNumero
            Case Else
                oMode = Xl_PrRevista_PropertyPage.Modes.Consulta
        End Select

        Dim oControl As New Xl_PrRevista_PropertyPage(oMode, oRevista, oNumero)
        AddHandler oControl.AfterSelect, AddressOf AfterSelectNumero
        AddHandler oControl.AfterUpdate, AddressOf AfterUpdateRevista
        oControl.Dock = DockStyle.Fill
        SplitContainer1.Panel2.Controls.Add(oControl)
    End Sub

    Private Sub AfterSelectNumero(ByVal sender As Object, ByVal e As System.EventArgs)
        RaiseEvent AfterSelect(sender, e)
        Me.Close()
    End Sub

    Private Sub AfterUpdateRevista(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oRevista As PrRevista = sender
        Dim oNode As TreeNode = TreeView1.SelectedNode
        oNode.Text = oRevista.Nom
    End Sub

    Private Sub SetContextMenu(ByVal oEditorial As PrEditorial)
        Dim oContextMenu As New ContextMenuStrip

        Dim oMenuItem As New ToolStripMenuItem("afegir editorial", My.Resources.clip, AddressOf AddEditorial)
        oContextMenu.Items.Add(oMenuItem)
        oMenuItem = New ToolStripMenuItem("afegir revista", My.Resources.clip, AddressOf AddRevista)
        oContextMenu.Items.Add(oMenuItem)

        TreeView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub AddEditorial(ByVal sender As Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub AddRevista(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oNode As maxisrvr.TreeNodeObj = TreeView1.SelectedNode
        Dim oEditorial As PrEditorial = oNode.Obj
        Dim oRevista As New PrRevista(oEditorial)

        Dim oMode As Xl_PrRevista_PropertyPage.Modes = Xl_PrRevista_PropertyPage.Modes.Consulta

        Dim oControl As New Xl_PrRevista_PropertyPage(oMode, oRevista)
        AddHandler oControl.AfterUpdate, AddressOf AfterAddRevista
        SplitContainer1.Panel2.Controls.Clear()
        SplitContainer1.Panel2.Controls.Add(oControl)
    End Sub

    Private Sub AfterAddRevista(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oRevista As PrRevista = sender
        Dim oParentNode As TreeNode = TreeView1.SelectedNode

        Dim oChildNode As New maxisrvr.TreeNodeObj(oRevista.Nom, oRevista, , Icons.revista, Icons.revistaOberta)
        oParentNode.Nodes.Add(oChildNode)
    End Sub
End Class