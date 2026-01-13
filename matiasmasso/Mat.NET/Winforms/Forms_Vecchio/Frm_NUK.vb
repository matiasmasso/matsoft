

Public Class Frm_NUK
    Private mPdcErrsArray As New ArrayList
    Private mPendingPdcs As Pdcs = Nothing
    Private mNodePdcs As maxisrvr.TreeNodeObj
    Private mNodePdcPendents As maxisrvr.TreeNodeObj
    Private mNodePdcRetinguts As maxisrvr.TreeNodeObj
    Private mNodePdcValidats As maxisrvr.TreeNodeObj
    Private mNodePdcEnviades As maxisrvr.TreeNodeObj
    Private mNodePdcPerConfirmar As maxisrvr.TreeNodeObj
    Private mNodePdcParcials As maxisrvr.TreeNodeObj
    Private mNodeAlbRetinguts As maxisrvr.TreeNodeObj
    Private mNodeAlbEntregats As maxisrvr.TreeNodeObj
    Private mNodeFrasRetingudes As maxisrvr.TreeNodeObj
    Private mNodeFrasValidades As maxisrvr.TreeNodeObj
    Private mNodes As New maxisrvr.TreeNodeObjs

    Public Enum TreeNodes
        NotSet
        Pdcs
        PdcsPerEnviar
        PdcsRetinguts
        PdcsValidats
        PdcsEnviats
        PdcsPerConfirmar
        PdcsSortits
        Albs
        AlbsRetinguts
        AlbsParcials
        AlbsEntregats
        Fras
        FrasIncidencies
        FrasValidades
        LastNode
    End Enum

    Private Enum Images
        Nuk
        Folder
        Clock
        Warning
        WarnEmpty
    End Enum

    Private Enum ColPdcs
        Id
    End Enum

    Private Sub Frm_NUK_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        LoadTree()
        LoadPendingPdcNodes()
    End Sub

    Private Sub LoadTree()
        Dim oNode0 As maxisrvr.TreeNodeObj = Nothing
        Dim oNode1 As maxisrvr.TreeNodeObj = Nothing
        Dim oNode2 As maxisrvr.TreeNodeObj = Nothing
        Dim oNode3 As maxisrvr.TreeNodeObj = Nothing
        Dim oNode4 As maxisrvr.TreeNodeObj = Nothing

        Dim iPdcs As Integer = Roche.TotalComandes
        mPendingPdcs = Roche.PendingPdcsFromCustomers

        oNode0 = New maxisrvr.TreeNodeObj("ROCHE", Roche.Contact, TreeNodes.NotSet)
        oNode0.ImageIndex = Images.Nuk
        TreeView1.Nodes.Add(oNode0)

        mNodePdcs = AddNode(oNode0, "comandes (" & iPdcs & ")", TreeNodes.Pdcs, Images.Folder)
        mNodePdcPendents = AddNode(mNodePdcs, "pendents d'enviar (" & mPendingPdcs.Count & ")", TreeNodes.PdcsPerEnviar, Images.Folder)
        mNodePdcRetinguts = AddNode(mNodePdcPendents, "retingudes", TreeNodes.PdcsRetinguts, Images.Warning)
        mNodePdcValidats = AddNode(mNodePdcPendents, "validades", TreeNodes.PdcsValidats, Images.Clock)
        mNodePdcEnviades = AddNode(mNodePdcs, "enviades", TreeNodes.PdcsEnviats, Images.Folder)
        mNodePdcPerConfirmar = AddNode(mNodePdcEnviades, "pendents de confirmar", TreeNodes.PdcsPerConfirmar, Images.Clock)
        oNode3 = AddNode(mNodePdcEnviades, "sortides", TreeNodes.PdcsSortits, Images.Folder)
        oNode1 = AddNode(oNode0, "albarans (" & Roche.TotalAlbarans & ")", TreeNodes.Pdcs, Images.Folder)
        mNodeAlbRetinguts = AddNode(oNode1, "retinguts", TreeNodes.AlbsRetinguts, Images.Warning)
        mNodePdcParcials = AddNode(oNode1, "entregues parcials", TreeNodes.AlbsParcials, Images.Folder)
        mNodeAlbEntregats = AddNode(oNode1, "entregats (" & Roche.AlbsEntregats & ")", TreeNodes.AlbsEntregats, Images.Folder)
        oNode1 = AddNode(oNode0, "factures", TreeNodes.Fras, Images.Folder)
        mNodeFrasRetingudes = AddNode(oNode1, "amb incidencies", TreeNodes.FrasIncidencies, Images.Warning)
        mNodeFrasValidades = AddNode(oNode1, "validades", TreeNodes.FrasValidades, Images.Folder)

        TreeView1.ExpandAll()
    End Sub

    Private Function AddNode(ByVal oParent As maxisrvr.TreeNodeObj, ByVal sText As String, ByVal oCod As TreeNodes, ByVal oImg As Images) As maxisrvr.TreeNodeObj
        Dim oChild As New maxisrvr.TreeNodeObj(sText, , CInt(oCod))
        oChild.ImageIndex = CInt(oImg)
        oChild.SelectedImageIndex = CInt(oImg)
        oParent.Nodes.Add(oChild)
        Return oChild
    End Function


    Private Sub LoadPendingPdcNodes()
        Dim iErrsCount As Integer = [Enum].GetValues(GetType(Roche.Errors)).Length
        ClearHostPanel()

        Dim mCounts(iErrsCount) As Integer
        ToolStripProgressBar1.Visible = True

        ToolStripProgressBar1.Minimum = CInt(mPendingPdcs.Count / 10)
        ToolStripProgressBar1.Maximum = ToolStripProgressBar1.Minimum + mPendingPdcs.Count

        Dim oPdcErrors As ArrayList = Nothing
        For Each oPdc As Pdc In mPendingPdcs
            ToolStripProgressBar1.Increment(1)
            oPdcErrors = Roche.CheckPdcErrs(oPdc)
            If oPdcErrors.Count = 0 Then
                mCounts(Roche.Errors.None) += 1
            Else
                For Each oErr As Roche.Errors In oPdcErrors
                    mCounts(CInt(oErr)) += 1
                Next
            End If
            mPdcErrsArray.Add(oPdcErrors)
        Next

        For i As Integer = 1 To iErrsCount - 1
            Dim sErrNom As String = [Enum].GetNames(GetType(Roche.Errors))(i)
            Dim iErrVal As Integer = [Enum].GetValues(GetType(Roche.Errors))(i)
            Dim iCount As Integer = mCounts(iErrVal)
            Dim sNodeText As String = sErrNom.Replace("_", " ")
            Dim ImageIndex As Integer = Images.WarnEmpty
            If iCount > 0 Then
                ImageIndex = Images.Warning
                sNodeText = sNodeText & " (" & iCount.ToString & ")"
            End If
            AddNode(mNodePdcRetinguts, sNodeText, iErrVal, ImageIndex)
        Next

        mNodePdcRetinguts.ExpandAll()
        mNodePdcRetinguts.Text = "retingudes (" & mPendingPdcs.Count - mCounts(Roche.Errors.None).ToString & ")"
        mNodePdcValidats.Text = "validades (" & mCounts(Roche.Errors.None).ToString & ")"
        ToolStripProgressBar1.Visible = False
    End Sub

    Private Sub TreeView1_NodeMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles TreeView1.NodeMouseClick
        ToolStripStatusLabelTit.Text = e.Node.Text
        ClearHostPanel()

        Dim oPdcs As Pdcs = Nothing
        If e.Node.Parent Is mNodePdcRetinguts Then
            oPdcs = New Pdcs
            Dim oSelectedError As Roche.Errors = CType(e.Node, maxisrvr.TreeNodeObj).Cod
            For i As Integer = 0 To mPendingPdcs.Count - 1
                For Each oErr As Roche.Errors In mPdcErrsArray(i)
                    If oErr = oSelectedError Then
                        oPdcs.Add(mPendingPdcs(i))
                    End If
                Next
            Next
            LoadPdcGrid(Xl_NUK_Pdcs.Cods.Pendents, e.Node.Text, oPdcs, oSelectedError)

        ElseIf e.Node Is mNodePdcs Then
            LoadPdcGrid(Xl_NUK_Pdcs.Cods.Totes, e.Node.Text)

        ElseIf e.Node Is mNodePdcPendents Then
            LoadPdcGrid(Xl_NUK_Pdcs.Cods.Pendents, e.Node.Text, mPendingPdcs)

        ElseIf e.Node Is mNodePdcRetinguts Then
            oPdcs = New Pdcs
            For i As Integer = 0 To mPendingPdcs.Count - 1
                If mPdcErrsArray(i).Count > 0 Then
                    oPdcs.Add(mPendingPdcs(i))
                End If
            Next
            LoadPdcGrid(Xl_NUK_Pdcs.Cods.Pendents, e.Node.Text, oPdcs)

        ElseIf e.Node Is mNodePdcValidats Then

            oPdcs = New Pdcs
            For i As Integer = 0 To mPendingPdcs.Count - 1
                If mPdcErrsArray(i).Count = 0 Then
                    oPdcs.Add(mPendingPdcs(i))
                End If
            Next

            LoadPdcGrid(Xl_NUK_Pdcs.Cods.Validades, e.Node.Text, oPdcs)

        ElseIf e.Node Is mNodePdcEnviades Then
            LoadPdcGrid(Xl_NUK_Pdcs.Cods.Enviades, e.Node.Text)
        ElseIf e.Node Is mNodePdcPerConfirmar Then
            LoadPdcGrid(Xl_NUK_Pdcs.Cods.PerConfirmar, e.Node.Text)
        ElseIf e.Node Is mNodeAlbRetinguts Then
            'Dim oControl As New Xl_NUK_Desadv(ToolStripProgressBar1)
            'AddHandler oControl.SelectionChange, AddressOf GridControlSelectionChanged
            'PanelHost.Controls.Add(oControl)
            'oControl.Dock = DockStyle.Fill
        ElseIf e.Node Is mNodePdcParcials Then
            LoadPdcGrid(Xl_NUK_Pdcs.Cods.Parcials, e.Node.Text)

        ElseIf e.Node Is mNodeAlbEntregats Then
            Dim oControl As New Xl_NUK_Albs()
            AddHandler oControl.SelectionChange, AddressOf GridControlSelectionChanged
            PanelHost.Controls.Add(oControl)
            oControl.Dock = DockStyle.Fill

        ElseIf e.Node Is mNodeFrasRetingudes Then
            Dim oControl As New Xl_NUK_Invoic(ToolStripProgressBar1)
            AddHandler oControl.SelectionChange, AddressOf GridControlSelectionChanged
            PanelHost.Controls.Add(oControl)
            oControl.Dock = DockStyle.Fill

        ElseIf e.Node Is mNodeFrasValidades Then
            Dim oControl As New Xl_NUK_fras()
            AddHandler oControl.SelectionChange, AddressOf GridControlSelectionChanged
            PanelHost.Controls.Add(oControl)
            oControl.Dock = DockStyle.Fill
        End If

    End Sub

    Private Sub LoadPdcGrid(ByVal oCod As Xl_NUK_Pdcs.Cods, ByVal sTitle As String, Optional ByVal oPdcs As Pdcs = Nothing, Optional ByVal oErr As Roche.Errors = Errors.None)
        Dim oControl As New Xl_NUK_Pdcs(oCod, oErr, oPdcs)
        AddHandler oControl.AfterUpdate, AddressOf RefreshRequest
        AddHandler oControl.SelectionChange, AddressOf GridControlSelectionChanged
        PanelHost.Controls.Add(oControl)
        oControl.Dock = DockStyle.Fill
    End Sub

    Private Sub ClearHostPanel()
        For Each oExistingControl As Control In PanelHost.Controls
            PanelHost.Controls.Remove(oExistingControl)
        Next
    End Sub

    Protected Sub GridControlSelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        PropertyGrid1.SelectedObject = sender
    End Sub

    Protected Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        LoadTree()
        LoadPendingPdcNodes()
    End Sub
End Class