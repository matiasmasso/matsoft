

Public Class Frm_Cca_Descuadres
    Private mYea As Integer
    Private mDs As DataSet
    Private mEmp as DTOEmp = BLL.BLLApp.Emp

    Private Enum Cols
        Guid
        Id
        Fch
        Pdf
        Ico
        Txt
        Eur
    End Enum

    Public WriteOnly Property Yea() As Integer
        Set(ByVal value As Integer)
            mYea = value
            LoadGrid()
        End Set
    End Property

    Private Sub LoadGrid()
        Dim SQL As String = "SELECT Cca.Guid, CCA.cca, CCA.FCH, " _
        & "(CASE WHEN CCA.PDF IS NULL THEN 0 ELSE 1 END) AS PDF, " _
        & "CCA.txt, SUM(CASE WHEN CCB.DH = 1 THEN CCB.EUR ELSE - CCB.EUR END) - SUM(CASE WHEN CCB.DH = 2 THEN CCB.EUR ELSE - CCB.EUR END) AS DIF " _
        & "FROM CCB INNER JOIN " _
        & "CCA ON Ccb.CcaGuid = Cca.Guid " _
        & "WHERE CCA.EMP =" & BLLApp.Emp.Id & " And CCA.YEA =" & mYea & " " _
        & "GROUP BY CCA.cca, CCA.FCH, (CASE WHEN CCA.PDF IS NULL THEN 0 ELSE 1 END), CCA.txt " _
        & "HAVING (SUM(CASE WHEN CCB.DH = 1 THEN CCB.EUR ELSE - CCB.EUR END) <> SUM(CASE WHEN CCB.DH = 2 THEN CCB.EUR ELSE - CCB.EUR END)) " _
        & "ORDER BY CCA.FCH DESC"
        mDs =  DAL.SQLHelper.GetDataset(SQL, New List(Of Exception))
        Dim oTb As DataTable = mDs.Tables(0)

        'afegeix columna pdf
        Dim oCol As DataColumn = oTb.Columns.Add("PDFICO", System.Type.GetType("System.Byte[]"))
        oCol.SetOrdinal(Cols.Ico)

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
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 65
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
            With .Columns(Cols.Eur)
                .HeaderText = "diferència"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 50
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft
            End With
        End With
    End Sub

    Private Function CurrentCcas() As List(Of DTOCca)
        Dim oCcas As New List(Of DTOCca)
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            Dim oGuid As Guid = oRow.Cells(Cols.Guid).Value
            oCcas.Add(New DTOCca(oGuid))
        End If
        Return oCcas
    End Function

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As MatEventArgs)
        Dim i As Integer
        Dim j As Integer
        Dim iFirstRow As Integer
        Dim iFirstVisibleCell As Integer = Cols.Id

        If DataGridView1.Rows.Count > 0 Then
            i = DataGridView1.CurrentRow.Index
            j = DataGridView1.CurrentCell.ColumnIndex
            iFirstRow = DataGridView1.FirstDisplayedScrollingRowIndex()
        End If

        LoadGrid()
        If DataGridView1.Rows.Count > 0 Then
            DataGridView1.FirstDisplayedScrollingRowIndex() = iFirstRow
            If i > DataGridView1.Rows.Count - 1 Then
                DataGridView1.CurrentCell = DataGridView1.Rows(DataGridView1.Rows.Count - 1).Cells(j)
            Else
                DataGridView1.CurrentCell = DataGridView1.Rows(i).Cells(iFirstVisibleCell)
            End If
        End If
    End Sub


    Private Sub DataGridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.DoubleClick
        Dim oCcas As List(Of DTOCca) = CurrentCcas()
        If oCcas.Count > 0 Then
            Dim oFrm As New Frm_Cca(oCcas.First)
            AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
            oFrm.Show()
        End If
    End Sub


    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oCcas As List(Of DTOCca) = CurrentCcas()

        If oCcas.Count > 0 Then
            Dim oMenu_Cca As New Menu_Cca(oCcas)
            AddHandler oMenu_Cca.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_Cca.Range)
        End If

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub DataGridView1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.SelectionChanged
        SetContextMenu()
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

End Class
