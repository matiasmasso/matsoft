Public Class Xl_PriceList_Customers_Vigent
    Private _AllowEvents As Boolean

    Private Enum Cols
        ArtGuid
        PriceList
        Brand
        Category
        Art
        TarifaA
        TarifaB
        Retail
        Fch
    End Enum

    Private Sub Xl_PriceList_Customers_Vigent_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        DateTimePicker1.Value = Today
        LoadGrid()
    End Sub

    Private Sub LoadGrid()
        Dim SQL As String = "SELECT ART.Guid, P1.Guid AS P1Guid, TPA.DSC AS Brand, STP.dsc AS Category, ART.ord, P2.TarifaA, P2.TarifaB, P2.Retail, P1.Fch " _
        & "FROM            PriceList_Customer AS P1 INNER JOIN " _
        & "PricelistItem_Customer AS P2 ON P2.PriceList = P1.Guid INNER JOIN " _
        & "ART ON P2.Art = ART.Guid INNER JOIN " _
        & "STP ON ART.Category = STP.Guid INNER JOIN " _
        & "TPA ON STP.Brand = TPA.GUID INNER JOIN " _
        & "(SELECT MAX(PriceList_Customer.Fch) AS FCH, PricelistItem_Customer.Art " _
                               & "FROM            PriceList_Customer INNER JOIN " _
                               & "PricelistItem_Customer ON PriceList_Customer.Guid = PricelistItem_Customer.PriceList " _
                               & "WHERE        PriceList_Customer.Fch <= '" & Format(DateTimePicker1.Value, "yyyyMMdd") & "' " _
                               & "GROUP BY PricelistItem_Customer.Art) AS X ON P1.Fch = X.FCH AND P2.Art = X.Art " _
        & "WHERE STP.obsoleto = 0 AND TPA.OBSOLETO = 0 AND ART.obsoleto = 0 AND ART.noTarifa = 0 "

        If TextBoxSearch.Text > "" Then
            SQL = SQL & "AND (TPA.DSC+STP.dsc+ART.ord) LIKE '%" & TextBoxSearch.Text & "%' "
        End If
        SQL = SQL & "ORDER BY TPA.ORD, STP.ord, ART.ord"

        Dim oDs As DataSet = MaxiSrvr.GetDataset(SQL, MaxiSrvr.Databases.Maxi)
        Dim oTb As DataTable = oDs.Tables(0)

        'Dim oItems As New PriceListItems_Customer
        'Dim oPriceList AS DTOPricelistCustomer = Nothing
        'Dim oDrd As SqlClient.SqlDataReader = GetDataReader(SQL, MaxiSrvr.Databases.Maxi, "@Fch", DateTimePicker1.Value)
        'Do While oDrd.Read
        '    Dim oArt As New Art(
        'Dim oItem As New DTOPricelistItemCustomer()
        'With oItem
        ' .Parent = New DTOPricelistCustomer(CType(oDrd("P1Guid"), Guid))
        '
        '       End With
        '       Loop
        '      oDrd.Close()

        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
            End With
            .DataSource = oTb
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = False
            .AllowUserToResizeRows = False
            .AllowDrop = False

            With .Columns(Cols.ArtGuid)
                .Visible = False
            End With
            With .Columns(Cols.PriceList)
                .Visible = False
            End With
            With .Columns(Cols.Brand)
                .HeaderText = "marca comercial"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            With .Columns(Cols.Category)
                .HeaderText = "categoría"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            With .Columns(Cols.Art)
                .HeaderText = "producte"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            With .Columns(Cols.TarifaA)
                .HeaderText = "tarifa A"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.TarifaB)
                .HeaderText = "tarifa B"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.Retail)
                .HeaderText = "PVP recomenat"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.Fch)
                .HeaderText = "data"
                .Width = 65
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "dd/MM/yy"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
        End With
        SetContextMenu()
        _AllowEvents = True
    End Sub


    Private Function CurrentItem() As DTOPricelistItemCustomer
        Dim retval As DTOPricelistItemCustomer = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            Dim oPriceListGuid As Guid = oRow.Cells(Cols.PriceList).Value
            Dim oPriceList As New DTOPricelistCustomer(oPriceListGuid)
            Dim oArtGuid As Guid = oRow.Cells(Cols.ArtGuid).Value
            Dim oSku As New DTOProductSku(oArtGuid)
            retval = BLL.BLLPricelistItemCustomer.Find(oPriceList, oSku)
        End If
        Return retval
    End Function

    Private Function CurrentArt() As Art
        Dim retval As Art = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            Dim oGuid As Guid = oRow.Cells(Cols.ArtGuid).Value
            retval = New Art(oGuid)
        End If
        Return retval
    End Function

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim i As Integer
        Dim j As Integer
        Dim iFirstRow As Integer
        Dim iFirstVisibleCell As Integer = Cols.Fch

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

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip

        Dim oCurrentItem As DTOPricelistItemCustomer = CurrentItem()
        If oCurrentItem IsNot Nothing Then
            Dim oMenuItem As New Menu_PricelistItemCustomer(oCurrentItem)
            AddHandler oMenuItem.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenuItem.Range)
        End If

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub


    Private Sub DoZoom(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_PricelistItemCustomer(CurrentItem)
        'Dim oFrm As New Frm_PriceList_Customer(CurrentItem.Parent)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub


    Private Sub DataGridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.DoubleClick
        DoZoom(sender, e)
    End Sub

    Private Sub DataGridView1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.SelectionChanged
        If _AllowEvents Then
            SetContextMenu()
        End If
    End Sub

    Private Sub DateTimePicker1_ValueChanged(sender As Object, e As System.EventArgs) Handles DateTimePicker1.ValueChanged
        If _AllowEvents Then
            ButtonRefresh.Enabled = True
        End If
    End Sub

    Private Sub ButtonRefresh_Click(sender As Object, e As System.EventArgs) Handles ButtonRefresh.Click
        LoadGrid()
        ButtonRefresh.Enabled = False
    End Sub

    Private Sub ToolStripButtonPdf_Click(sender As System.Object, e As System.EventArgs) Handles ToolStripButtonPdf.Click

    End Sub

 
    Private Sub ToolStripButtonExcel_Click(sender As Object, e As EventArgs) Handles ToolStripButtonExcel.Click
        MatExcel.GetExcelFromDataGridView(DataGridView1).Visible = True
    End Sub

    Private Sub TextBoxSearch_TextChanged(sender As Object, e As EventArgs) Handles TextBoxSearch.TextChanged
        LoadGrid()
    End Sub
End Class