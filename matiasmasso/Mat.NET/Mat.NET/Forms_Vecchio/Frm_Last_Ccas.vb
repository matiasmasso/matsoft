
Imports System.Drawing

Public Class Frm_Last_Ccas
    Private mDs As DataSet
    Private mEmp as DTOEmp = BLL.BLLApp.Emp
    Private mDsYeas As DataSet
    Private mAllowEvents As Boolean

    Private Enum Cols
        Guid
        Id
        Fch
        Pdf
        Ico
        Txt
        Usr
    End Enum

    Private Sub Frm_Last_Ccas_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetYeas()
        Refresca()
        EnableYeaButtons()
        mAllowEvents = True
    End Sub

    Private Sub Refresca()
        LoadGrid()
        SetContextMenu()
    End Sub

    Private Sub LoadGrid()
        Dim SQL As String = "SELECT " _
        & "CCA.Guid,CCA.CCA,CCA.FCH, " _
        & "(CASE WHEN CCA.Hash IS NULL THEN 0 ELSE 1 END) AS PDF, " _
        & "CCA.TXT, " _
        & "(CASE WHEN Email.Adr IS NULL THEN (CASE WHEN USR.LOGIN IS NULL THEN CAST(Cca.USRCREATED AS VARCHAR) ELSE USR.LOGIN END) ELSE EMAIL.ADR END) AS USR " _
        & "FROM CCA LEFT OUTER JOIN " _
        & "EMPUSR ON CCA.UsrCreatedGuid=EMPUSR.ContactGuid LEFT OUTER JOIN " _
        & "USR ON EmpUsr.UsrGuid = Usr.Guid " _
        & "LEFT OUTER JOIN Email ON Cca.UsrCreatedGuid = Email.Guid " _
        & "WHERE " _
        & "CCA.EMP = @Emp AND " _
        & "CCA.YEA = @Yea " _
        & "ORDER BY CCA.CCA DESC"

        mDs = MaxiSrvr.GetDataset(SQL, MaxiSrvr.Databases.Maxi, "@Emp", App.Current.Emp.Id, "@Yea", CurrentYea)
        Dim oTb As DataTable = mDs.Tables(0)

        'afegeix columna pdf
        Dim oCol As DataColumn = oTb.Columns.Add("PDFICO", System.Type.GetType("System.Byte[]"))
        oCol.SetOrdinal(Cols.Ico)

        mAllowEvents = False
        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
            End With
            .DataSource = mDs.Tables(0)
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = True
            .AllowUserToResizeRows = False
            .AllowDrop = True

            With .Columns(Cols.Guid)
                .Visible = False
            End With
            With .Columns(Cols.Id)
                .HeaderText = "registre"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 45
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.Fch)
                .HeaderText = "Data"
                .Width = 65
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "dd/MM/yy"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
            With .Columns(Cols.Pdf)
                .Visible = False
            End With
            With .Columns(Cols.Ico)
                .HeaderText = "doc"
                .Width = 16
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With
            With .Columns(Cols.Txt)
                .HeaderText = "concepte"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            With .Columns(Cols.Usr)
                .HeaderText = "Usuari"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 50
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft
            End With
        End With
        mAllowEvents = True
    End Sub

    Private Sub SetYeas()
        Dim SQL As String = "SELECT YEA FROM CCA " _
        & "WHERE EMP=" & mEmp.Id & " " _
        & "GROUP BY YEA " _
        & "ORDER BY YEA DESC"

        mDsYeas = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi)
        Dim oTb As DataTable = mDsYeas.Tables(0)
        Dim oRow As DataRow
        With ToolStripComboBoxYea
            .BeginUpdate()
            For Each oRow In oTb.Rows
                .Items.Add(oRow("YEA"))
            Next
            .EndUpdate()
            If oTb.Rows.Count > 0 Then .SelectedIndex = 0
        End With
    End Sub

    Private Function CurrentYea() As Integer
        Dim iYea As Integer = ToolStripComboBoxYea.SelectedItem
        Return iYea
    End Function

    Private Function CurrentCca() As Cca
        Dim retval As Cca = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            Dim oGuid As Guid = DataGridView1.CurrentRow.Cells(Cols.Guid).Value
            retval = New Cca(oGuid)
        End If
        Return retval
    End Function

    Private Function CurrentCcas() As Ccas
        Dim oCcas As New Ccas

        If DataGridView1.SelectedRows.Count > 0 Then
            Dim oRow As DataGridViewRow
            For Each oRow In DataGridView1.SelectedRows
                Dim oGuid As Guid = oRow.Cells(Cols.Guid).Value
                Dim oCca As New Cca(oGuid)
                oCcas.Add(oCca)
            Next
        Else
            Dim oCca As Cca = CurrentCca()
            If oCca IsNot Nothing Then
                oCcas.Add(CurrentCca)
            End If
        End If
        Return oCcas
    End Function


    Private Sub EnableYeaButtons()
        Dim Idx As Integer = ToolStripComboBoxYea.SelectedIndex
        Dim iYeas As Integer = mDsYeas.Tables(0).Rows.Count
        AnyanteriorToolStripButton.Enabled = (Idx < iYeas - 1)
        AnysegüentToolStripButton.Enabled = (Idx > 0)
    End Sub

    Private Sub AnyanteriorToolStripButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles AnyanteriorToolStripButton.Click
        Dim Idx As Integer = ToolStripComboBoxYea.SelectedIndex
        Dim iYeas As Integer = mDsYeas.Tables(0).Rows.Count
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

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oCcas As Ccas = CurrentCcas()

        If oCcas.count > 0 Then
            Dim oMenu_Cca As New Menu_Cca(oCcas)
            AddHandler oMenu_Cca.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_Cca.Range)
        End If

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As MatEventArgs)
        Dim i As Integer
        Dim j As Integer
        Dim iFirstRow As Integer
        Dim iFirstVisibleCell As Integer = Cols.Id
        Dim oGrid As DataGridView = DataGridView1

        If oGrid.Rows.Count > 0 Then
            i = oGrid.CurrentRow.Index
            j = oGrid.CurrentCell.ColumnIndex
            iFirstRow = oGrid.FirstDisplayedScrollingRowIndex()
        End If

        Refresca()

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

    Private Sub DataGridView1_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        Select Case e.ColumnIndex
            Case Cols.Ico
                Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                If oRow.Cells(Cols.Pdf).Value = 1 Then
                    e.Value = My.Resources.pdf
                Else
                    e.Value = My.Resources.empty
                End If
        End Select
    End Sub

    Private Sub DataGridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.DoubleClick
        ShowCca()
    End Sub

    Private Sub DataGridView1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DataGridView1.KeyDown
        If e.KeyCode = Keys.Enter Then
            ShowCca()
            e.Handled = True
        End If
    End Sub

    Private Sub ShowCca()
        Dim oCca As Cca = CurrentCca()
        Dim oFrm As New Frm_Cca(oCca)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.show
    End Sub

    Private Sub DataGridView1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.SelectionChanged
        If mAllowEvents Then
            SetContextMenu()
        End If
    End Sub

    Private Sub ToolStripComboBoxYea_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ToolStripComboBoxYea.SelectedIndexChanged
        If mAllowEvents Then
            EnableYeaButtons()
            Refresca()
        End If
    End Sub

    Private Sub ToolStripButtonRefresca_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButtonRefresca.Click
        Refresca()
    End Sub

    Private Sub NouToolStripButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles NouToolStripButton.Click
        Dim oCca As new cca(BLL.BLLApp.emp)
        With oCca
            .fch = Today
        End With
        Dim oFrm As New Frm_Cca(oCca)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.show
    End Sub


    Private Sub DataGridView1_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles DataGridView1.DragEnter
        e.Effect = DragDropEffects.Copy
    End Sub

    Private Sub DataGridView1_DragOver(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles DataGridView1.DragOver
        Dim oPoint = DataGridView1.PointToClient(New Point(e.X, e.Y))
        Dim hit As DataGridView.HitTestInfo = DataGridView1.HitTest(oPoint.X, oPoint.Y)
        If hit.Type = DataGridViewHitTestType.Cell Then
            Dim oclickedCell As DataGridViewCell = DataGridView1.Rows(hit.RowIndex).Cells(hit.ColumnIndex)
            DataGridView1.CurrentCell = oclickedCell
        End If
    End Sub

    Private Sub DataGridView1_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles DataGridView1.DragDrop
        Dim oDocFiles As List(Of DTODocFile) = Nothing
        Dim oTargetCell As DataGridViewCell = Nothing
        Dim exs as New List(Of exception)
        If DragDropHelper.GetDatagridDropDocFiles(sender, e, oDocFiles, oTargetCell, exs) Then
            If oTargetCell IsNot Nothing Then
                Dim oRow As DataGridViewRow = DataGridView1.Rows(oTargetCell.RowIndex)
                Dim oGuid As Guid = DataGridView1.CurrentRow.Cells(Cols.Guid).Value
                Dim oCca As New Cca(oGuid)
                ImportPdf(oCca, oDocFiles.First)
            Else

            End If
        Else
            UIHelper.WarnError( exs, "error al importar document")
        End If
    End Sub

    Private Sub ImportPdf(ByVal oCca As Cca, ByVal oDocFile As DTODocFile)
        oCca.DocFile = oDocFile
        Dim exs as New List(Of exception)
        If oCca.Update( exs) Then
            RefreshRequest(oCca, New System.EventArgs)
        Else
            UIHelper.WarnError( exs, "error al desar l'assentament")
        End If
    End Sub

End Class