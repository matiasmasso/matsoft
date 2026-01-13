
Imports System.Data.SqlClient

Public Class Frm_FlatFileFixLens
    Private mAllowEvents As Boolean

    Private Enum Cols
        Id
        Nom
    End Enum

    Private Sub Frm_FlatFileFixLens_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        LoadGrid()
    End Sub

    Private Sub LoadGrid()
        Dim SQL As String = "SELECT Id,Nom From FlatFileFixLen order by Id"
        Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi)

        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
                '.DefaultCellStyle.BackColor = Color.Transparent
            End With
            .DataSource = oDs.Tables(0).DefaultView
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = False
            .RowHeadersVisible = False
            .MultiSelect = True
            .AllowUserToResizeRows = False

            With .Columns(Cols.Id)
                .Visible = False
            End With
 
            With .Columns(Cols.Nom)
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
        End With
        SetContextMenu()
        mAllowEvents = True
    End Sub

    Private Function CurrentItem() As FlatFileFixLen
        Dim retval As FlatFileFixLen = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            Dim id As DTO.DTOFlatFile.ids = CType(oRow.Cells(Cols.Id).Value, DTO.DTOFlatFile.ids)
            retval = New FlatFileFixLen(id)
        End If
        Return retval
    End Function

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oFlatfile As FlatFileFixLen = CurrentItem()
        If oFlatfile IsNot Nothing Then
            Dim oMenu_File As New Menu_FlatFileFixLen(oFlatfile)
            oContextMenu.Items.AddRange(oMenu_File.range)
        End If

        Dim oMenuItem As New ToolStripMenuItem("afegir", Nothing, AddressOf Do_AddNew)
        oContextMenu.Items.Add(oMenuItem)

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub DataGridView1_DoubleClick(sender As Object, e As System.EventArgs) Handles DataGridView1.DoubleClick
        Do_Zoom()
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As System.EventArgs) Handles DataGridView1.SelectionChanged
        If mAllowEvents Then
            SetContextMenu()
        End If
    End Sub

    Private Sub Do_Zoom()
        Dim oFlatFile As FlatFileFixLen = CurrentItem()
        Dim oFrm As New Frm_FlatFileFixLenType(oFlatFile)
        AddHandler oFrm.afterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Do_AddNew()
        Dim oFile As New FlatFileFixLen(DTO.DTOFlatFile.ids.NotSet)
        With oFile
            .Guid = System.Guid.NewGuid
            .Lines = New ArrayList
        End With
        Dim oFrm As New Frm_FlatFileFixLenType(oFile)
        AddHandler oFrm.afterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim i As Integer
        Dim j As Integer
        Dim iFirstRow As Integer
        Dim iFirstVisibleCell As Integer = Cols.Nom
        Dim oGrid As DataGridView = DataGridView1

        If oGrid.Rows.Count > 0 Then
            i = oGrid.CurrentRow.Index
            j = oGrid.CurrentCell.ColumnIndex
            iFirstRow = oGrid.FirstDisplayedScrollingRowIndex()
        End If

        LoadGrid()

        If oGrid.CurrentRow Is Nothing Then
        Else
            oGrid.FirstDisplayedScrollingRowIndex() = iFirstRow

            If i > oGrid.Rows.Count - 1 Then
                oGrid.CurrentCell = oGrid.Rows(oGrid.Rows.Count - 1).Cells(j)
            Else
                oGrid.CurrentCell = oGrid.Rows(i).Cells(iFirstVisibleCell)
            End If
        End If
    End Sub
End Class
