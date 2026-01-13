

Public Class Frm_Aeat_Mods
    Private mEmp as DTOEmp = BLL.BLLApp.Emp
    Private mGranEmpresaYea As Integer

    Private Enum ColsMain
        Guid
        Nom
        Dsc
    End Enum

    Private Enum ColsDetail
        Id
        Text
        Thumb
    End Enum

    Private Sub Frm_Aeat_Mods_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim sGranEmpresaYea As String = BLL.BLLDefault.EmpValue(DTODefault.Codis.GranEmpresaDesde)
        If IsNumeric(sGranEmpresaYea) Then
            mGranEmpresaYea = sGranEmpresaYea
        End If

        LoadYeas()
        LoadGridMain()
        SetContextMenu()
    End Sub

    Private Sub LoadGridMain()
        Dim BlPyme As Boolean = (mGranEmpresaYea = 0 Or mGranEmpresaYea > CurrentYea())
        Dim SQL As String = "SELECT GUID,MOD,DSC FROM AEAT_MOD WHERE "
        If BlPyme Then
            SQL = SQL & " PYME=1 "
        Else
            SQL = SQL & " GRANEMPRESA=1 "
        End If
        SQL = SQL & "ORDER BY Mod"
        Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi)
        Dim oTb As DataTable = oDs.Tables(0)

        If oTb.Rows.Count > 0 Then
            With DataGridView1
                With .RowTemplate
                    .Height = DataGridView1.Font.Height * 1.3
                End With
                .DataSource = oTb
                .SelectionMode = DataGridViewSelectionMode.FullRowSelect
                .ColumnHeadersVisible = False
                .RowHeadersVisible = False
                .MultiSelect = False
                .AllowUserToResizeRows = False
                .AllowDrop = False

                With .Columns(ColsMain.Guid)
                    .Visible = False
                End With
                With .Columns(ColsMain.Nom)
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                    .Width = 100
                End With
                With .Columns(ColsMain.Dsc)
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                End With
            End With
        End If

    End Sub

    Private Function CurrentModel() As AEAT_Mod
        Dim oAEAT_Mod As AEAT_Mod = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            Dim oGuid As Guid = GuidHelper.GetGuid(oRow.Cells(ColsMain.Guid).Value)
            oAEAT_Mod = New AEAT_Mod(oGuid)
        End If
        Return oAEAT_Mod
    End Function


    Private Sub DataGridView1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.SelectionChanged
        SetContextMenu()
        LoadGridDetail()
    End Sub

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oAEAT_Mod As AEAT_Mod = CurrentModel()

        Dim oMenu_AEAT_Mod As New Menu_Aeat_Mod(oAEAT_Mod)
        AddHandler oMenu_AEAT_Mod.AfterUpdate, AddressOf RefreshRequestMain
        oContextMenu.Items.Add(New ToolStripMenuItem("afegir...", Nothing, AddressOf AddNewMain))

        If oAEAT_Mod IsNot Nothing Then
            oContextMenu.Items.Add("-")
            oContextMenu.Items.AddRange(oMenu_AEAT_Mod.Range)

            oContextMenu.Items.Add(New ToolStripMenuItem("web", My.Resources.iExplorer, AddressOf Do_Web))
            oContextMenu.Items.Add(New ToolStripMenuItem("copiar enllaç", My.Resources.Copy, AddressOf Do_CopyLink))
            oContextMenu.Items.Add(New ToolStripMenuItem("pdf", My.Resources.pdf, AddressOf Do_Pdf))
        End If

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub


    Private Function CurrentYeaUrl() As String
        Dim sUrl As String = "http://www.matiasmasso.es/pro/wAeatModels.aspx"
        sUrl += "?yea=" & GuidHelper.GetGuidFromYea(CurrentYea).ToString
        sUrl += "&mod=" & CurrentModel.Guid.ToString
        Return sUrl
    End Function

    Private Sub Do_Web(ByVal sender As Object, ByVal e As System.EventArgs)
        Process.Start("IExplore.exe", CurrentYeaUrl)
    End Sub

    Private Sub Do_CopyLink(ByVal sender As Object, ByVal e As System.EventArgs)
        Clipboard.SetDataObject(CurrentYeaUrl, True)
    End Sub


    Private Sub AddNewMain(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_Aeat_mod
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequestMain
        Dim oAeat_mod As New AEAT_Mod
        With oAeat_mod
            .Pyme = True
            .GranEmpresa = True
        End With
        With oFrm
            .Aeat_mod = oAeat_mod
            .Show()
        End With
    End Sub

    Private Sub RefreshRequestMain(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim i As Integer
        Dim j As Integer
        Dim iFirstRow As Integer
        Dim iFirstVisibleCell As Integer = ColsMain.Dsc
        Dim oGrid As DataGridView = DataGridView1

        If oGrid.Rows.Count > 0 Then
            i = oGrid.CurrentRow.Index
            j = oGrid.CurrentCell.ColumnIndex
            iFirstRow = oGrid.FirstDisplayedScrollingRowIndex()
        End If

        LoadGridMain()

        If oGrid.Rows.Count = 0 Then
        Else
            oGrid.FirstDisplayedScrollingRowIndex() = iFirstRow

            If i > oGrid.Rows.Count - 1 Then
                oGrid.CurrentCell = oGrid.Rows(oGrid.Rows.Count - 1).Cells(j)
            Else
                oGrid.CurrentCell = oGrid.Rows(i).Cells(iFirstVisibleCell)
            End If
        End If
    End Sub

    Private Function CurrentYea() As Integer
        Return ToolStripComboBoxYea.Text
    End Function

    Private Sub LoadYeas()
        Dim StartYea As Integer = 1985
        Dim EndYea As Integer = Today.Year
        For i As Integer = EndYea To StartYea Step -1
            ToolStripComboBoxYea.Items.Add(i.ToString)
        Next
        ToolStripComboBoxYea.SelectedIndex = 0
        EnableYeaButtons()
    End Sub

    Private Sub LoadGridDetail()
        Dim SQL As String = "SELECT PERIOD FROM AEAT " _
        & "WHERE EMP=" & mEmp.Id & " AND " _
        & "MODEL='" & CurrentModel.Guid.ToString & "' AND " _
        & "YEA=" & CurrentYea() & " " _
        & "ORDER BY PERIOD"
        Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi)
        Dim oTb As DataTable = oDs.Tables(0)

        Dim oColTxt As DataColumn = oTb.Columns.Add("TEXT", System.Type.GetType("System.String"))
        Dim oColImg As DataColumn = oTb.Columns.Add("IMG", System.Type.GetType("System.Byte[]"))

        With DataGridView2
            With .RowTemplate
                .Height = 48 ' DataGridView1.Font.Height * 1.3
                '.DefaultCellStyle.BackColor = Color.Transparent
            End With
            .DataSource = oTb
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .DefaultCellStyle.SelectionBackColor = Color.AliceBlue
            .DefaultCellStyle.SelectionForeColor = Color.Black
            .DefaultCellStyle.WrapMode = DataGridViewTriState.True
            .ColumnHeadersVisible = False
            .RowHeadersVisible = False
            .MultiSelect = False
            .AllowUserToAddRows = False
            .AllowUserToResizeRows = False

            If oTb.Rows.Count > 0 Then
                With .Columns(ColsDetail.Id)
                    .Visible = False
                End With
                With .Columns(ColsDetail.Text)
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft

                End With
                With .Columns(ColsDetail.Thumb)
                    .Width = 48
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter
                End With
            End If
        End With
    End Sub

    Private Sub DataGridView2_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DataGridView2.CellFormatting
        Select Case e.ColumnIndex
            Case ColsDetail.Text
                Dim oRow As DataGridViewRow = DataGridView2.Rows(e.RowIndex)
                Dim iPeriod As Integer = oRow.Cells(ColsDetail.Id).Value
                Dim oAeat As New AEAT(mEmp, CurrentYea, iPeriod, CurrentModel)
                e.Value = oAeat.TextMultiLine
            Case ColsDetail.Thumb
                Dim oRow As DataGridViewRow = DataGridView2.Rows(e.RowIndex)
                Dim iPeriod As Integer = oRow.Cells(ColsDetail.Id).Value
                Dim oAeat As New AEAT(mEmp, CurrentYea, iPeriod, CurrentModel)
                e.Value = oAeat.Thumbnail
                'Dim oNumero As New PrNumero(oRow.Cells(Cols.Id).Value)
                'Dim oThumbnail As Image = Nothing
                'If oNumero.PdfExists Then
                ' oThumbnail = oRender.Thumbnail()
                ' Else
                ' oThumbnail = My.Resources.empty
                ' End If
                'e.Value = oThumbnail
                'If oRow.Cells(Cols.PdfExists).Value = 1 Then
                'e.Value = My.Resources.pdf
                'Else
                'e.Value = My.Resources.empty
                'End If
        End Select
    End Sub

    Private Function CurrentAeat() As AEAT
        Dim oAEAT As AEAT = Nothing
        Dim oRow As DataGridViewRow = DataGridView2.CurrentRow
        If oRow IsNot Nothing Then
            Dim iPeriod As Integer = oRow.Cells(ColsDetail.Id).Value
            oAEAT = New AEAT(mEmp, CurrentYea, iPeriod, CurrentModel)
        End If
        Return oAEAT
    End Function

    Private Sub DataGridView2_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView2.SelectionChanged
        Dim oRow As DataGridViewRow = DataGridView2.CurrentRow
        If oRow Is Nothing Then
            ZoomToolStripMenuItem.Enabled = False
            ExportarToolStripMenuItem.Enabled = False
            EmailToolStripMenuItem.Enabled = False
            WebToolStripMenuItem.Enabled = True
        Else
            ZoomToolStripMenuItem.Enabled = True
            ExportarToolStripMenuItem.Enabled = True
            EmailToolStripMenuItem.Enabled = True
            WebToolStripMenuItem.Enabled = True
        End If
    End Sub

    Private Sub RefreshRequestDetail(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim i As Integer
        Dim j As Integer
        Dim iFirstRow As Integer
        Dim iFirstVisibleCell As Integer = ColsDetail.Text
        Dim oGrid As DataGridView = DataGridView2

        If oGrid.Rows.Count > 0 Then
            If oGrid.CurrentRow IsNot Nothing Then
                i = oGrid.CurrentRow.Index
                j = oGrid.CurrentCell.ColumnIndex
                iFirstRow = oGrid.FirstDisplayedScrollingRowIndex()
            End If
        End If

        LoadGridDetail()

        If oGrid.Rows.Count = 0 Then
        Else
            oGrid.FirstDisplayedScrollingRowIndex() = iFirstRow

            If i > oGrid.Rows.Count - 1 Then
                oGrid.CurrentCell = oGrid.Rows(oGrid.Rows.Count - 1).Cells(j)
            Else
                oGrid.CurrentCell = oGrid.Rows(i).Cells(iFirstVisibleCell)
            End If
        End If
    End Sub

    Private Sub ZoomToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ZoomToolStripMenuItem.Click
        Dim oFrm As New Frm_Aeat(CurrentAeat)
        AddHandler oFrm.afterupdate, AddressOf RefreshRequestDetail
        oFrm.Show()
    End Sub


    Private Sub AddNewToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles AddNewToolStripMenuItem.Click
        Dim oFrm As New Frm_Aeat(NextAeat)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequestDetail
        oFrm.Show()
    End Sub

    Private Function NextAeat() As AEAT
        Dim oAeat As AEAT = Nothing

        Dim SQL As String = "SELECT MAX(PERIOD) as LASTPERIOD FROM AEAT WHERE " _
        & "EMP=" & mEmp.Id & " AND " _
        & "YEA=" & CurrentYea() & " AND " _
        & "MODEL='" & CurrentModel.Guid.ToString & "'"

        Dim iPeriod As Integer = 0
        Dim oDrd As SqlClient.SqlDataReader = maxisrvr.GetDataReader(SQL, maxisrvr.Databases.Maxi)
        oDrd.Read()
        If Not IsDBNull(oDrd("LASTPERIOD")) Then
            iPeriod = oDrd("LASTPERIOD")
        End If
        oDrd.Close()

        oAeat = New AEAT(mEmp, CurrentYea, -1, CurrentModel)
        Select Case CurrentModel.TPeriod
            Case AEAT_Mod.PeriodTypes.Mensual
                If iPeriod < 12 Then
                    iPeriod += 1
                    oAeat = New AEAT(mEmp, CurrentYea, iPeriod, CurrentModel)
                    oAeat.Fch = New Date(CurrentYea, iPeriod, Date.DaysInMonth(CurrentYea, iPeriod))
                End If
            Case AEAT_Mod.PeriodTypes.Trimestral
                If iPeriod < 4 Then
                    iPeriod += 1
                    Dim iMes As Integer = CInt(Math.Round(iPeriod / 3 + 0.45))
                    oAeat = New AEAT(mEmp, CurrentYea, iPeriod, CurrentModel)
                    oAeat.Fch = New Date(CurrentYea, iMes, Date.DaysInMonth(CurrentYea, iMes))
                End If
            Case AEAT_Mod.PeriodTypes.Anual
                oAeat.Fch = New Date(CurrentYea, 12, 31)
            Case AEAT_Mod.PeriodTypes.Altres
                oAeat.Period += 1
                oAeat.Fch = Today
        End Select

        Return oAeat
    End Function

    Private Sub ToolStripComboBoxYea_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ToolStripComboBoxYea.SelectedIndexChanged
        EnableYeaButtons()
        If CurrentModel() IsNot Nothing Then
            RefreshRequestMain(sender, e)
            LoadGridDetail()
        End If
    End Sub


    Private Sub EnableYeaButtons()
        Dim Idx As Integer = ToolStripComboBoxYea.SelectedIndex
        Dim iYeas As Integer = ToolStripComboBoxYea.Items.Count
        AnyanteriorToolStripButton.Enabled = (Idx < iYeas - 1)
        AnysegüentToolStripButton.Enabled = (Idx > 0)
    End Sub

    Private Sub AnyanteriorToolStripButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles AnyanteriorToolStripButton.Click
        Dim Idx As Integer = ToolStripComboBoxYea.SelectedIndex
        Dim iYeas As Integer = ToolStripComboBoxYea.Items.Count
        Idx = Idx + 1
        ToolStripComboBoxYea.SelectedIndex = Idx
        EnableYeaButtons()
    End Sub

    Private Sub AnysegüentToolStripButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles AnysegüentToolStripButton.Click
        Dim Idx As Integer = ToolStripComboBoxYea.SelectedIndex
        Idx = Idx - 1
        ToolStripComboBoxYea.SelectedIndex = Idx
        EnableYeaButtons()
    End Sub

    Private Sub WebToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles WebToolStripMenuItem.Click
        Dim oAeat As AEAT = CurrentAeat()
        UIHelper.ShowStream(oAeat.DocFile)
    End Sub

    Private Sub CopyLinkToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CopyLinkToolStripMenuItem.Click
        Dim oAeat As AEAT = CurrentAeat()
        Dim sUrl As String = BLL.BLLDocFile.DownloadUrl(oAeat.DocFile, True)

        Dim data_object As New DataObject
        data_object.SetData(DataFormats.Text, True, sUrl)
        Clipboard.SetDataObject(data_object, True)
        MsgBox("enllaç copiat al portapapers", MsgBoxStyle.Information, "MAT.NET")
    End Sub

    Private Sub Do_Pdf(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim SQL As String = "SELECT HASH FROM AEAT " _
        & "WHERE EMP=@EMP AND " _
        & "MOD=@MOD AND " _
        & "YEA=@YEA AND " _
        & "HASH IS NOT NULL " _
        & "ORDER BY PERIOD"
        Dim oDrd As SqlClient.SqlDataReader = MaxiSrvr.GetDataReader(SQL, MaxiSrvr.Databases.Maxi, "@EMP", mEmp.Id, "@MODEL", CurrentModel.Guid.ToString, "@YEA", CurrentYea)
        Dim oAEAT As AEAT = Nothing

        Dim oDocFiles As New List(Of DTODocFile)
        Do While oDrd.Read
            Dim sHash As String = oDrd("Hash")
            Dim oDocFile As DTODocFile = new DTODocFile(sHash)
            oDocFiles.Add(oDocFile)
        Loop

        Dim oStream As Byte() = PdfHelper.MergePdfs(oDocFiles)
        root.ShowPdf(oStream)
    End Sub

    Private Sub ExportarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExportarToolStripMenuItem.Click
        Dim oAeat As AEAT = CurrentAeat()
        Dim oDocFile As DTODocFile = oAeat.DocFile
        If oAeat.DocFile Is Nothing Then
            MsgBox("el document está buit", MsgBoxStyle.Exclamation)
        Else
            BLL.BLLDocFile.Load(oDocFile, False)
            Dim sMime As String = oAeat.DocFile.Mime.ToString
            Dim oDlg As New SaveFileDialog
            With oDlg
                .Title = "exportar document"
                .Filter = "documents " & sMime & " (*." & sMime & ")|*." & sMime & "| tots els documents (*.*)|*.*"
                If .ShowDialog = Windows.Forms.DialogResult.OK Then
                    BLL.BLLDocFile.Load(oAeat.DocFile, True)
                    Dim exs As New List(Of Exception)
                    If Not BLL.FileSystemHelper.SaveStream(oAeat.DocFile.Stream, exs, .FileName) Then
                        UIHelper.WarnError(exs, "error al desar el fitxer")
                    End If
                End If
            End With
        End If

    End Sub
End Class