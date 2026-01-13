

Public Class Frm_NUK_Pncs
    Private mEmp as DTOEmp = BLL.BLLApp.Emp
    Private mAllowEvents As Boolean = False
    Private mNodes(10) As maxisrvr.TreeNodeObj

    Private Enum Nodes
        Pendents
        Validats
        Retinguts
        Enviats
        Per_confirmar
        Entregues_parcials
    End Enum

    Private Enum Cols
        Yea
        Pdc
        Fch
        Clx
        amt
        VatIncluded
    End Enum

    Private Enum ColsArt
        Id
        Nom
    End Enum

    Private Enum Images
        Folder
        FolderWarn
        FolderWarnEmpty
        FolderGreen
        FolderUpRed
        FolderDownBlue
        FolderBlueBallLight
        FolderBlueBallMedium
        FolderBlueBallFull
        FolderNotes
        FolderLocked
        FolderWait
    End Enum

    Private Sub Frm_NUK_Pncs_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Cursor = Cursors.WaitCursor
        LoadImageList()
        LoadNodeStructure()
        LoadNodes()
        mAllowEvents = True
        TreeView1.SelectedNode = mNodes(Nodes.Retinguts)
        LoadGridFromPdcs(mNodes(Nodes.Retinguts))
        Cursor = Cursors.Default
    End Sub

    Private Sub Refresca()
        Cursor = Cursors.WaitCursor
        Dim oSelectedNode As maxisrvr.TreeNodeObj = CurrentNode()
        mAllowEvents = False
        LoadNodes()
        mAllowEvents = True
        TreeView1.SelectedNode = oSelectedNode
        LoadGridFromPdcs(oSelectedNode)
        Cursor = Cursors.Default
    End Sub

    Private Sub LoadNodeStructure()
        Dim oNewPdcs As New Pdcs

        mNodes(Nodes.Pendents) = NewFolderNode(Nodes.Pendents, Images.FolderWait)
        mNodes(Nodes.Validats) = NewFolderNode(Nodes.Validats, Images.FolderWait)
        mNodes(Nodes.Retinguts) = NewFolderNode(Nodes.Retinguts, Images.FolderWarn)
        mNodes(Nodes.Enviats) = NewFolderNode(Nodes.Enviats, Images.FolderBlueBallFull)
        mNodes(Nodes.Per_confirmar) = NewFolderNode(Nodes.Per_confirmar, Images.FolderWait)
        mNodes(Nodes.Entregues_parcials) = NewFolderNode(Nodes.Entregues_parcials, Images.FolderWarn)

        With TreeView1.Nodes
            .Add(mNodes(Nodes.Pendents))
            .Add(mNodes(Nodes.Enviats))
        End With
        With mNodes(Nodes.Pendents).Nodes
            .Add(mNodes(Nodes.Retinguts))
            .Add(mNodes(Nodes.Validats))
        End With
        With mNodes(Nodes.Enviats).Nodes
            .Add(mNodes(Nodes.Per_confirmar))
            .Add(mNodes(Nodes.Entregues_parcials))
        End With

        With mNodes(Nodes.Retinguts).Nodes
            For Each oErr As Roche.Errors In [Enum].GetValues(GetType(Roche.Errors))
                If oErr <> Errors.None Then
                    .Add(NewErrNode(oErr))
                End If
            Next
        End With

        TreeView1.ExpandAll()
    End Sub

    Private Sub LoadNodes()
        ToolStripProgressBar1.Visible = True

        Dim oPendingPdcs As Pdcs = Roche.PendingPdcsFromCustomers
        ToolStripProgressBar1.Minimum = CInt(oPendingPdcs.Count / 10)
        ToolStripProgressBar1.Maximum = ToolStripProgressBar1.Minimum + oPendingPdcs.Count


        Dim oDelivered As New Pdcs
        Dim oDeliveredPerConfirmar As New Pdcs
        Dim oDeliveredParcial As New Pdcs
        Dim oNewPdcs As New Pdcs

        Roche.DeliveredPdcs(oDelivered, oDeliveredPerConfirmar, oDeliveredParcial)

        mNodes(Nodes.Pendents).Obj = oPendingPdcs
        mNodes(Nodes.Validats).Obj = New Pdcs
        mNodes(Nodes.Retinguts).Obj = New Pdcs
        mNodes(Nodes.Enviats).Obj = oDelivered
        mNodes(Nodes.Per_confirmar).Obj = oDeliveredPerConfirmar
        mNodes(Nodes.Entregues_parcials).Obj = oDeliveredParcial

        For Each oNode As maxisrvr.TreeNodeObj In mNodes(Nodes.Retinguts).Nodes
            oNode.Obj = New Pdcs
        Next

        For Each oErrNode As maxisrvr.TreeNodeObj In mNodes(Nodes.Retinguts).Nodes
            Dim oErr As Roche.Errors = oErrNode.Cod
            With oErrNode
                .Obj = New Pdcs
                .Text = oErr.ToString.Replace("_", " ")
                .ImageIndex = Images.FolderWarnEmpty
                .SelectedImageIndex = Images.FolderWarnEmpty
            End With
        Next

        Dim oPdcErrors As ArrayList = Nothing
        For Each oPdc As Pdc In oPendingPdcs
            ToolStripProgressBar1.Increment(1)
            oPdcErrors = Roche.CheckPdcErrs(oPdc)
            If oPdcErrors.Count = 0 Then
                DirectCast(mNodes(Nodes.Validats).Obj, Pdcs).Add(oPdc)
            Else
                DirectCast(mNodes(Nodes.Retinguts).Obj, Pdcs).Add(oPdc)
                For Each oErr As Roche.Errors In oPdcErrors
                    AddPdcToErrNode(oErr, oPdc)
                Next
            End If
        Next

        For Each oCod As Nodes In [Enum].GetValues(GetType(Nodes))
            Dim oNode As maxisrvr.TreeNodeObj = mNodes(oCod)
            oNode.Text = oCod.ToString.Replace("_", " ") & " (" & CType(oNode.Obj, Pdcs).Count & ")"
        Next

        ToolStripProgressBar1.Visible = False
    End Sub

    Private Function NewFolderNode(ByVal oNodeCod As Nodes, ByVal ImageIndex As Integer) As maxisrvr.TreeNodeObj
        Dim sNodeText As String = oNodeCod.ToString.Replace("_", " ")
        Dim oNode As New maxisrvr.TreeNodeObj(sNodeText, , CInt(oNodeCod))
        With oNode
            .ImageIndex = ImageIndex
            .SelectedImageIndex = ImageIndex
        End With
        Return oNode
    End Function

    Private Sub AddPdcToErrNode(ByVal oErr As Roche.Errors, ByVal oPdc As Pdc)
        Dim oErrNode As maxisrvr.TreeNodeObj = Nothing
        For Each oErrNode In mNodes(Nodes.Retinguts).Nodes
            If oErr = CType(oErrNode.Cod, Roche.Errors) Then
                If oErrNode.ImageIndex = Images.FolderWarnEmpty Then
                    oErrNode.ImageIndex = Images.FolderWarn
                    oErrNode.SelectedImageIndex = Images.FolderWarn
                End If
                Dim oPdcs As Pdcs = DirectCast(oErrNode.Obj, Pdcs)
                oPdcs.Add(oPdc)
                oErrNode.Text = oErr.ToString.Replace("_", " ") & " (" & oPdcs.Count.ToString & ")"
                Exit Sub
            End If
        Next
    End Sub

    Private Function NewErrNode(ByVal oErr As Roche.Errors) As maxisrvr.TreeNodeObj
        Dim oErrNode As New maxisrvr.TreeNodeObj(oErr.ToString.Replace("_", " "))
        With oErrNode
            .Obj = New Pdcs
            .Cod = CInt(oErr)
            .ImageIndex = Images.FolderWarnEmpty
            .SelectedImageIndex = Images.FolderWarnEmpty
        End With
        Return oErrNode
    End Function


    Private Sub LoadGridFromPdcs(ByVal oNode As maxisrvr.TreeNodeObj)
        Dim OldAllowEvents As Boolean = mAllowEvents
        mAllowEvents = False

        ToolStripStatusLabel1.Text = oNode.Text

        Dim oTb As New DataTable
        Dim oRow As DataRow = Nothing
        With oTb.Columns
            .Add("YEA", System.Type.GetType("System.Int32"))
            .Add("PDC", System.Type.GetType("System.Int32"))
            .Add("FCH", System.Type.GetType("System.DateTime"))
            .Add("CLX", System.Type.GetType("System.String"))
            .Add("AMT", System.Type.GetType("System.Decimal"))
            .Add("VATINCLUDED", System.Type.GetType("System.Decimal"))
        End With

        Dim oPdcs As Pdcs = oNode.Obj
        For Each oPdc As Pdc In oPdcs
            oRow = oTb.NewRow
            oRow(Cols.Yea) = oPdc.Yea
            oRow(Cols.Pdc) = oPdc.Id
            oRow(Cols.Fch) = oPdc.Fch
            oRow(Cols.Clx) = oPdc.Client.Clx
            oRow(Cols.amt) = oPdc.BaseImponible(True, Roche.Mgz).Eur
            oRow(Cols.VatIncluded) = oPdc.TotalVatIncluded(True, Roche.Mgz).Eur
            oTb.Rows.Add(oRow)
        Next

        With DataGridView1
            .DataSource = oTb
            With .RowTemplate
                '.Height = DataGridView1.Font.Height * 1.3
            End With
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = False
            .AllowUserToResizeRows = False
            .AllowUserToResizeColumns = True

            With .Columns(Cols.Yea)
                .Visible = False
            End With
            With .Columns(Cols.Pdc)
                .HeaderText = "comanda"
                .Width = 60
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#"
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.Fch)
                .HeaderText = "data"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "dd/MM/yy"
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.Clx)
                .HeaderText = "destinatari"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            End With
            With .Columns(Cols.amt)
                .HeaderText = "Import"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.VatIncluded)
                .HeaderText = "IVA incl."
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
        End With

        If oTb.Rows.Count > 0 Then
            DataGridView1.CurrentCell = DataGridView1.Rows(0).Cells(Cols.Clx)
            DataGridView1.CurrentRow.Selected = True
        End If
        SetContextMenu(oNode)
        mAllowEvents = OldAllowEvents

    End Sub

    Private Sub DataGridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.DoubleClick
        Dim oNode As maxisrvr.TreeNodeObj = CurrentNode()
        'Dim oFrm As New Frm_Pdc_Client(CurrentPdc)
        'oFrm.Show()
    End Sub


    Private Sub DataGridView1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.SelectionChanged
        If mAllowEvents Then
            SetContextMenu(TreeView1.SelectedNode)
        End If
    End Sub


    Private Sub DataGridView2_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView2.DoubleClick
        Dim oFrm As New Frm_Art(CurrentArt)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub DataGridView2_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView2.SelectionChanged
        If mAllowEvents Then
            SetContextMenuArts()
        End If
    End Sub


    Private Function CurrentArt() As Art
        Dim oArt As Art = Nothing

        Dim oRow As DataGridViewRow = DataGridView2.CurrentRow
        If oRow IsNot Nothing Then
            Dim iId As Integer = oRow.Cells(ColsArt.Id).Value
            oArt = MaxiSrvr.Art.FromNum(BLL.BLLApp.Emp, iId)
        End If

        Return oArt
    End Function

    Private Function CurrentPdc() As Pdc
        Dim oPdc As Pdc = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            Dim LngId As Long = DataGridView1.CurrentRow.Cells(Cols.Pdc).Value
            Dim iYea As Integer = DataGridView1.CurrentRow.Cells(Cols.Yea).Value
            oPdc = Pdc.FromNum(BLL.BLLApp.Emp, iYea, LngId)
        End If
        Return oPdc
    End Function

    Private Function CurrentPdcs() As Pdcs
        Dim oPdcs As New Pdcs

        If DataGridView1.SelectedRows.Count > 0 Then
            Dim IntYea As Integer
            Dim LngId As Integer
            Dim oPdc As Pdc
            Dim oRow As DataGridViewRow
            For Each oRow In DataGridView1.SelectedRows
                LngId = oRow.Cells(Cols.Pdc).Value
                IntYea = oRow.Cells(Cols.Yea).Value
                oPdc = Pdc.FromNum(mEmp, IntYea, LngId)
                oPdcs.Add(oPdc)
            Next
            oPdcs.Sort()
        Else
            Dim oPdc As Pdc = CurrentPdc()
            If oPdc IsNot Nothing Then
                oPdcs.Add(CurrentPdc)
            End If
        End If
        Return oPdcs
    End Function

    Private Function CurrentNode() As maxisrvr.TreeNodeObj
        Return TreeView1.SelectedNode
    End Function

    Private Sub SetContextMenu(ByVal oNode As maxisrvr.TreeNodeObj)
        Dim oContextMenu As New ContextMenuStrip

        Dim oMenuItem As ToolStripMenuItem = Nothing

        Dim oPdcs As Pdcs = CurrentPdcs()
        If oPdcs.Count = 1 Then
            oMenuItem = New ToolStripMenuItem("comanda...")
            oContextMenu.Items.Add(oMenuItem)

            Dim oMenu_Pdc As New Menu_Pdc(oPdcs)
            AddHandler oMenu_Pdc.AfterUpdate, AddressOf RefreshRequest
            oMenuItem.DropDownItems.AddRange(oMenu_Pdc.Range)

            oMenuItem = New ToolStripMenuItem("client...")
            oContextMenu.Items.Add(oMenuItem)

            Dim oMenu_Contact As New Menu_Contact(oPdcs(0).Client)
            oMenuItem.DropDownItems.AddRange(oMenu_Contact.Range)
        End If

        If oNode Is Nothing Then
        ElseIf oNode Is mNodes(Nodes.Validats) Then
            oMenuItem = New ToolStripMenuItem("enviar a ROCHE", My.Resources.pc, AddressOf Deliver)
            oContextMenu.Items.Add(oMenuItem)
        ElseIf oNode.Parent Is mNodes(Nodes.Retinguts) Then
            Dim oErrCod As Roche.Errors = CType(oNode.Cod, Roche.Errors)
            Select Case oErrCod
                Case Errors.Transferencia_previa
                    oMenuItem = New ToolStripMenuItem("avisat", My.Resources.tel, AddressOf AvisarTransferenciaPrevia)
                    oContextMenu.Items.Add(oMenuItem)
                Case Errors.Transferencia_avisats
                    oMenuItem = New ToolStripMenuItem("retrocedir avis", My.Resources.UNDO, AddressOf RetrocedirAvisTransferenciaPrevia)
                    oContextMenu.Items.Add(oMenuItem)
                    oMenuItem = New ToolStripMenuItem("cobrar", My.Resources.DollarOrange2, AddressOf CobrarTransferenciaPrevia)
                    oContextMenu.Items.Add(oMenuItem)
            End Select
        ElseIf oNode Is mNodes(Nodes.Per_confirmar) Then
            oMenuItem = New ToolStripMenuItem("retrocedir envio", My.Resources.UNDO, AddressOf UnSave)
            oContextMenu.Items.Add(oMenuItem)
        End If


        oMenuItem = New ToolStripMenuItem("excel", My.Resources.Excel, AddressOf Excel)
        oContextMenu.Items.Add(oMenuItem)

        oMenuItem = New ToolStripMenuItem("refresca", My.Resources.refresca, AddressOf RefreshRequest)
        oContextMenu.Items.Add(oMenuItem)

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub


    Private Sub SetContextMenuArts()
        Dim oGrid As DataGridView = DataGridView2

        Dim oContextMenu As New ContextMenuStrip
        Dim oMenuItem As ToolStripMenuItem = Nothing


        Dim oArt As Art = CurrentArt()
        If oArt IsNot Nothing Then
            oMenuItem = New ToolStripMenuItem("article...")
            oContextMenu.Items.Add(oMenuItem)

            Dim oMenu_art As New Menu_Art(oArt)
            AddHandler oMenu_art.AfterUpdate, AddressOf RefreshRequest
            oMenuItem.DropDownItems.AddRange(oMenu_art.Range)
        End If

        oMenuItem = New ToolStripMenuItem("refresca", My.Resources.refresca, AddressOf RefreshRequest)
        oContextMenu.Items.Add(oMenuItem)

        oGrid.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oGrid As DataGridView = DataGridView1
        Dim i As Integer = oGrid.CurrentRow.Index
        Dim j As Integer = oGrid.CurrentCell.ColumnIndex
        Dim iFirstRow As Integer = oGrid.FirstDisplayedScrollingRowIndex()

        Refresca()

        Try
            oGrid.FirstDisplayedScrollingRowIndex() = iFirstRow
            mAllowEvents = True

            If i > oGrid.Rows.Count - 1 Then
                oGrid.CurrentCell = DataGridView1.Rows(oGrid.Rows.Count - 1).Cells(j)
            Else
                oGrid.CurrentCell = oGrid.Rows(i).Cells(j)
            End If
        Catch ex As Exception

        End Try

        If IsNodeMissingArtEans(TreeView1.SelectedNode) Then
            RefreshRequestArts(sender, e)
        End If
    End Sub

    Private Sub RefreshRequestArts(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oGrid As DataGridView = DataGridView2
        Dim i As Integer = oGrid.CurrentRow.Index
        Dim j As Integer = oGrid.CurrentCell.ColumnIndex
        Dim iFirstRow As Integer = oGrid.FirstDisplayedScrollingRowIndex()

        LoadArts(Roche.MissingArtEans)

        Try
            oGrid.FirstDisplayedScrollingRowIndex() = iFirstRow
            mAllowEvents = True

            If i > oGrid.Rows.Count - 1 Then
                oGrid.CurrentCell = DataGridView1.Rows(oGrid.Rows.Count - 1).Cells(j)
            Else
                oGrid.CurrentCell = oGrid.Rows(i).Cells(j)
            End If
        Catch ex As Exception

        End Try
    End Sub


    Private Sub LoadArts(ByVal oArts As Arts)
        Dim OldAllowEvents As Boolean = mAllowEvents
        mAllowEvents = False

        Dim oTb As New DataTable
        Dim oRow As DataRow = Nothing
        With oTb.Columns
            .Add("ID", System.Type.GetType("System.Int32"))
            .Add("NOM", System.Type.GetType("System.String"))
        End With

        For Each oArt As Art In oArts
            oRow = oTb.NewRow
            oRow(ColsArt.Id) = oArt.Id
            oRow(ColsArt.Nom) = oArt.Nom_ESP
            oTb.Rows.Add(oRow)
        Next

        With DataGridView2
            .DataSource = oTb
            With .RowTemplate
                '.Height = DataGridView1.Font.Height * 1.3
            End With
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = False
            .AllowUserToResizeRows = False
            .AllowUserToResizeColumns = True

            With .Columns(ColsArt.Id)
                .Visible = False
            End With
            With .Columns(ColsArt.Nom)
                .HeaderText = "producte"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            End With
        End With
        SetContextMenuArts()
        mAllowEvents = OldAllowEvents
    End Sub

    Private Sub UnSave(ByVal sender As Object, ByVal e As System.EventArgs)
        Roche.UnSave(CurrentPdc)
        RefreshRequest(sender, e)
    End Sub

    Private Function IsNodeMissingArtEans(ByVal oNode As maxisrvr.TreeNodeObj) As Boolean
        Dim RetVal As Boolean = False
        If oNode.Parent Is mNodes(Nodes.Retinguts) Then
            Dim oErrCod As Roche.Errors = oNode.Cod
            If oErrCod = Errors.Articles_sense_codi_EAN Then
                RetVal = True
            End If
        End If
        Return RetVal
    End Function

    Private Sub TreeView1_NodeMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles TreeView1.NodeMouseClick
        mAllowEvents = False
        Dim oNode As maxisrvr.TreeNodeObj = e.Node
        'Application.DoEvents()

        ToolStripStatusTitle.Text = oNode.Text

        LoadGridFromPdcs(oNode)

        If IsNodeMissingArtEans(oNode) Then
            LoadArts(Roche.MissingArtEans)
            SplitContainerGrids.Panel2Collapsed = False
        Else
            SplitContainerGrids.Panel2Collapsed = True
        End If
        mAllowEvents = True
    End Sub


    Private Sub RetrocedirAvisTransferenciaPrevia()
        Dim oPdc As Pdc = CurrentPdc()
        Dim sTit As String = "RETROCEDIR AVIS DE TRANSFERENCIA COMANDA " & oPdc.Id & " A " & oPdc.Client.Clx
        Dim sObs As String = InputBox("Observacions:", sTit, oPdc.Obs)

        oPdc.CashStatus = Pdc.CashStatuss.Pendent_de_avisar
        oPdc.Obs = sObs
        Dim exs as New List(Of exception)
        If oPdc.Update(exs) Then
            RefreshRequest(Nothing, EventArgs.Empty)
        Else
            MsgBox("error al desar la comanda" & vbCrLf & BLL.Defaults.ExsToMultiline(exs), MsgBoxStyle.Exclamation)
        End If
    End Sub

    Private Sub AvisarTransferenciaPrevia()
        Dim oPdc As Pdc = CurrentPdc()
        Dim sTit As String = "AVIS DE TRANSFERENCIA COMANDA " & oPdc.Id & " A " & oPdc.Client.Clx
        Dim sObs As String = InputBox("Observacions:", sTit, oPdc.Obs)

        oPdc.CashStatus = Pdc.CashStatuss.avisat
        oPdc.Obs = sObs
        Dim exs as New List(Of exception)
        If oPdc.Update(exs) Then
            RefreshRequest(Nothing, EventArgs.Empty)
        Else
            MsgBox("error al desar la comanda" & vbCrLf & BLL.Defaults.ExsToMultiline(exs), MsgBoxStyle.Exclamation)
        End If
    End Sub

    Private Sub CobrarTransferenciaPrevia()
        Dim oPdc As Pdc = CurrentPdc()
        Dim oFrm As New Frm_Cobrament_TransferenciaPrevia(oPdc)
        AddHandler oFrm.AfterUpdate, AddressOf AfterCobroTransferencia
        oFrm.Show()
    End Sub

    Private Sub AfterCobroTransferencia(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As Frm_Cobrament_TransferenciaPrevia = sender
        Dim oPdc As Pdc = oFrm.pdc
        oPdc.CashStatus = Pdc.CashStatuss.cobrat

        Dim exs as New List(Of exception)
        If oPdc.Update(exs) Then
            Refresca()
        Else
            MsgBox("error al desar la comanda" & vbCrLf & BLL.Defaults.ExsToMultiline(exs), MsgBoxStyle.Exclamation)
        End If

    End Sub

    Private Sub Deliver()
        'envia validats
        Dim exs as new list(Of Exception)
        Roche.Deliver( exs)
        Refresca()
    End Sub

    Private Sub Excel(ByVal sender As Object, ByVal e As System.EventArgs)
        MatExcel.GetExcelFromDataGridView(DataGridView1).Visible = True
    End Sub


    Private Sub LoadImageList()
        With ImageList1.Images
            .Add(My.Resources.Folder32)
            .Add(My.Resources.Folder32Warn)
            .Add(My.Resources.Folder32WarnEmpty)
            .Add(My.Resources.Folder32Green)
            .Add(My.Resources.Folder32UpRed)
            .Add(My.Resources.Folder32BlueDown)
            .Add(My.Resources.Folder32BlueBallLight)
            .Add(My.Resources.Folder32BlueBallMedium)
            .Add(My.Resources.Folder32BlueBallFull)
            .Add(My.Resources.Folder32Notes)
            .Add(My.Resources.Folder32Locked)
            .Add(My.Resources.Folder32Wait)
        End With
    End Sub

    Private Sub ToolStripSplitButtonFitxers_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ToolStripSplitButtonFitxers.Click
        'Dim oFrm As New Frm_FlatFile
        'oFrm.Show()
    End Sub
End Class