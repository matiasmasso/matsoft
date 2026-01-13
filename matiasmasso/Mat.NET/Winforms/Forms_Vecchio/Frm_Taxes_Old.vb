

Public Class Frm_Taxes_Old
    Private mAllowEvents As Boolean

    Private Enum Cols
        Guid
        Codi
        Fch
        Tipus
    End Enum

    Private Sub Frm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        LoadGrid()
        SetContextMenu()
        mAllowEvents = True
    End Sub

    Private Sub LoadGrid()
        Dim SQL As String = "SELECT Guid, Codi, Fch, Tipus FROM Tax ORDER BY Codi, Fch DESC"
        Dim oDs As DataSet = DAL.SQLHelper.GetDataset(SQL, New List(Of Exception))
        Dim oTb As DataTable = oDs.Tables(0)
        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
            End With
            .DataSource = oTb.DefaultView
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = False
            .AllowUserToResizeRows = False

            With .Columns(Cols.Guid)
                .Visible = False
                .DataPropertyName = "Guid"
            End With

            With .Columns(Cols.Codi)
                .HeaderText = "codi"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With

            With .Columns(Cols.Fch)
                .HeaderText = "data"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Width = 100
            End With

            With .Columns(Cols.Tipus)
                .HeaderText = "Tipus"
                .DataPropertyName = "Tipus"
                .Width = 100
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "0.00 %"
            End With

        End With
    End Sub

    Private Function CurrentItm() As Tax
        Dim retval As Tax = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            Dim oGuid As Guid = CType(oRow.Cells(Cols.Guid).Value, Guid)
            retval = New Tax(oGuid)
        End If
        Return retval
    End Function

    Private Sub SetContextMenu()
        Dim oContextMenuStrip As New ContextMenuStrip
        Dim oMenuItem As ToolStripMenuItem = Nothing

        Dim oItm As Tax = CurrentItm()
        If oItm IsNot Nothing Then
            oMenuItem = New ToolStripMenuItem("zoom", Nothing, AddressOf Zoom)
            oContextMenuStrip.Items.Add(oMenuItem)

            oMenuItem = New ToolStripMenuItem("eliminar", Nothing, AddressOf Delete)
            oContextMenuStrip.Items.Add(oMenuItem)
        End If

        oMenuItem = New ToolStripMenuItem("afegir...")
        oContextMenuStrip.Items.Add(oMenuItem)
        For Each oCodi As DTO.DTOTax.Codis In [Enum].GetValues(GetType(DTO.DTOTax.Codis))
            If oCodi <> DTO.DTOTax.Codis.Exempt Then
                Dim sText As String = Tax.GetNomFromCodi(oCodi)
                oMenuItem.DropDownItems.Add(sText, Nothing, AddressOf AddNewItm)
            End If
        Next

        DataGridView1.ContextMenuStrip = oContextMenuStrip
    End Sub

    Private Sub DataGridView1_CellFormatting(sender As Object, e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        Select Case e.ColumnIndex
            Case Cols.Codi
                Dim oCodi As DTO.DTOTax.Codis = CType(e.Value, DTO.DTOTax.Codis)
                Dim sText As String = oCodi.ToString.Replace("_", " ")
                e.Value = sText
            Case Cols.Tipus
                e.Value = e.Value / 100
        End Select
    End Sub

    Private Sub DataGridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.DoubleClick
        Zoom()
    End Sub

    Private Sub DataGridView1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.SelectionChanged
        If mAllowEvents Then
            SetContextMenu()
        End If
    End Sub

    Private Sub AddNewItm(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oMenuItem As ToolStripMenuItem = sender
        Dim sMenuText As String = oMenuItem.Text
        Dim sCodiText As String = sMenuText.Replace(" ", "_")
        Dim oCodi As DTOTax.Codis = [Enum].Parse(GetType(DTOTax.Codis), sCodiText)

        Dim oTax As DTOTax = New DTOTax
        With oTax
            .Codi = oCodi
            .Fch = Today
        End With

        Dim oFrm As New Frm_Tax(oTax)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Zoom()
        Dim oFrm As New Frm_Tax(CurrentItm)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Delete()
        Dim oItm As Tax = CurrentItm()
        Dim rc As MsgBoxResult = MsgBox("eliminem " & oItm.Codi.ToString & " del " & oItm.Fch.ToShortDateString & "?", MsgBoxStyle.OkCancel, "MAT.NET")
        If rc = MsgBoxResult.Ok Then
            oItm.Delete()
            RefreshRequest(Nothing, EventArgs.Empty)
        End If
    End Sub

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim i As Integer
        Dim j As Integer
        Dim iFirstRow As Integer
        Dim iFirstVisibleCell As Integer = Cols.Codi
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