

Public Class Frm_LangResourcess
    Private mAllowEvents As Boolean

    Private Enum Cols
        Guid
        Src
        Clau
        Esp
    End Enum

    Private Sub Frm_LangResources_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        LoadGrid()
    End Sub

    Private Sub LoadGrid()
        Dim SQL As String = "SELECT GUID, SRC, CLAU, ESP FROM LANG_RESOURCE"
        If CheckBoxFiltre.Checked Then
            SQL = SQL & " WHERE CLAU LIKE '" & TextboxClau.Text & "'"
        End If
        Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi)
        Dim oTb As DataTable = oDs.Tables(0)
        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
            End With
            .DataSource = oTb
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = True
            .AllowUserToResizeRows = False
            .AllowDrop = False
            With .Columns(Cols.Guid)
                .HeaderText = "Guid"
                .Width = 50
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With
            With .Columns(Cols.Src)
                .HeaderText = "src"
                .Width = 30
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#"
            End With
            With .Columns(Cols.Clau)
                .HeaderText = "Clau"
                .Width = 65
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft
            End With
            With .Columns(Cols.Esp)
                .HeaderText = "Texte"
                .Width = 65
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft
            End With
        End With
        mAllowEvents = True
    End Sub

    Private Function CurrentResource() As LangResource
        Dim oLangResource As LangResource = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            Dim oGuid As System.Guid = oRow.Cells(Cols.Guid).Value
            Dim oSrc As LangResource.Srcs = oRow.Cells(Cols.Src).Value
            oLangResource = New LangResource(oSrc, oGuid)
        End If
        Return oLangResource
    End Function

    Private Sub DataGridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.DoubleClick
        Dim oFrm As New Frm_LangResource
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        With oFrm
            .LangResource = CurrentResource()
            .Show()
        End With
    End Sub

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim i As Integer
        Dim j As Integer
        Dim iFirstRow As Integer
        Dim iFirstVisibleCell As Integer = Cols.Clau
        Dim oGrid As DataGridView = DataGridView1

        If oGrid.Rows.Count > 0 Then
            i = oGrid.CurrentRow.Index
            j = oGrid.CurrentCell.ColumnIndex
            iFirstRow = oGrid.FirstDisplayedScrollingRowIndex()
        End If

        LoadGrid()

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

    Private Sub SetContextMenu()
        Dim oContextMenuStrip As New ContextMenuStrip
        Dim oResource As LangResource = CurrentResource()
        If oResource IsNot Nothing Then
            oContextMenuStrip.Items.Add(New ToolStripMenuItem("zoom", My.Resources.binoculares, AddressOf Zoom))
        End If
        oContextMenuStrip.Items.Add(New ToolStripMenuItem("afegir...", My.Resources.clip, AddressOf AddNewItm))
        DataGridView1.ContextMenuStrip = oContextMenuStrip
    End Sub

    Private Sub DataGridView1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.SelectionChanged
        If mAllowEvents Then
            SetContextMenu()
        End If
    End Sub

    Private Sub AddNewItm(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oLangResource As New LangResource(LangResource.Srcs.Resource)
        Dim oFrm As New Frm_LangResource
        With oFrm
            .LangResource = oLangResource
            .Show()
        End With
    End Sub

    Private Sub Zoom()
        Dim oFrm As New Frm_LangResource
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        With oFrm
            .LangResource = CurrentResource()
            .Show()
        End With
    End Sub

    Private Sub CheckBoxFiltre_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBoxFiltre.CheckedChanged
        TextboxClau.Visible = CheckBoxFiltre.Checked
        ButtonFiltre.Visible = CheckBoxFiltre.Checked
        If CheckBoxFiltre.Checked Then
            TextboxClau.Select()
        End If
    End Sub

    Private Sub ButtonFiltre_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonFiltre.Click
        LoadGrid()
    End Sub
End Class