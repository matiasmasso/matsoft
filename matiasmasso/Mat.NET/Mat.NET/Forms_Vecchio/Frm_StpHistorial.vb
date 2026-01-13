
Imports Excel = Microsoft.Office.Interop.Excel

Public Class Frm_StpHistorial
    Private mStp As Stp
    Private mAllowEvents As Boolean

    Private Enum Cols
        Guid
        Fch
        Src
        Ico
        Clx
        Qty
    End Enum

    Public Sub New(oStp As Stp)
        MyBase.New()
        Me.InitializeComponent()
        mStp = oStp
        LoadGrid()
    End Sub

    Private Sub LoadGrid()
        Dim SQL As String = "SELECT PDC.Guid, PDC.FchCreated, PDC.src, CLX.clx, SUM(PNC.qty) AS Qty " _
        & "FROM            PNC INNER JOIN " _
        & "ART ON PNC.ArtGuid= ART.Guid INNER JOIN " _
        & "STP ON STP.Guid = ART.Category INNER JOIN " _
        & "PDC ON PNC.PdcGuid = PDC.Guid INNER JOIN " _
        & "CLX ON PDC.Emp = CLX.Emp AND PDC.cli = CLX.cli " _
        & "WHERE  STP.Guid=@Guid AND PNC.Cod = 2 " _
        & "GROUP BY PDC.Guid, PDC.FchCreated, PDC.src, CLX.clx " _
        & "ORDER BY PDC.FchCreated DESC"

        Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi, "@Guid", mStp.Guid.ToString)
        Dim oTb As DataTable = oDs.Tables(0)

        Dim oColSrc As DataColumn = oTb.Columns.Add("ICOSRC", System.Type.GetType("System.Byte[]"))
        oColSrc.SetOrdinal(Cols.Ico)

        mAllowEvents = False
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

            With .Columns(Cols.Guid)
                .Visible = False
            End With

            With .Columns(Cols.Fch)
                .HeaderText = "registre"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomLeft
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.BottomLeft
                .Width = 100
            End With

            With .Columns(Cols.Src)
                .Visible = False
            End With

            With .Columns(Cols.Ico)
                .HeaderText = ""
                .Width = 16
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With

            With .Columns(Cols.Clx)
                .HeaderText = "client"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With

            With .Columns(Cols.Qty)
                .HeaderText = "unitats"
                .Width = 120
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                '.DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
            End With

        End With
        SetContextMenu()
        mAllowEvents = True
    End Sub

    Private Sub DataGridView1_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        Select Case e.ColumnIndex
            Case Cols.Ico
                Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                Dim oCod As DTOPurchaseOrder.Codis = CType(oRow.Cells(Cols.Src).Value, DTOPurchaseOrder.Codis)
                e.Value = BLL.BLLPurchaseOrder.SrcIcon(oCod)
        End Select
    End Sub

    Private Function CurrentItm() As Pdc
        Dim oItm As Pdc = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            Dim oGuid As Guid = CType(oRow.Cells(Cols.Guid).Value, Guid)
            oItm = New Pdc(oGuid)
        End If
        Return oItm
    End Function

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oMenuItem As ToolStripMenuItem = Nothing

        Dim oRows As DataGridViewSelectedRowCollection = DataGridView1.SelectedRows
        If oRows.Count > 1 Then
            oMenuItem = New ToolStripMenuItem("Excel")
            AddHandler oMenuItem.Click, AddressOf onExcel
            oContextMenu.Items.Add(oMenuItem)

        Else
            Dim oPdc As Pdc = CurrentItm()
            If oPdc IsNot Nothing Then
                Dim oMenu_Pdc As New Menu_Pdc(oPdc)
                oContextMenu.Items.AddRange(oMenu_Pdc.Range)
            End If
        End If


        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub onExcel()
        Dim oRows As DataGridViewSelectedRowCollection = DataGridView1.SelectedRows
        Dim oExcel As Excel.Application = MatExcel.GetExcel

        Dim oWb As Excel.Workbook = oExcel.Workbooks.Add()
        Dim oSheet = oWb.ActiveSheet
        Dim iRow As Integer = 1
        For Each oRow As DataGridViewRow In oRows
            Dim oRange As Excel.Range = oSheet.Cells(iRow, 1)
            oRange.Value = oRow.Cells(Cols.Clx).Value
            iRow += 1
        Next
        oExcel.Visible = True

    End Sub

    Private Sub DataGridView1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.SelectionChanged
        If mAllowEvents Then
            SetContextMenu()
        End If
    End Sub

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim i As Integer
        Dim j As Integer
        Dim iFirstRow As Integer
        Dim iFirstVisibleCell As Integer = Cols.Clx

        Dim oGrid As DataGridView = DataGridView1

        Dim oRow As DataGridViewRow = oGrid.CurrentRow
        If oRow IsNot Nothing Then
            i = oRow.Index
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