

Public Class Frm_ArtxStp_Ranking
    Private mStp As Stp
    Private mTpa As Tpa
    Private mDs As DataSet
    Private mAllowEvents As Boolean
    Private mTot As Decimal
    Private mMode As Modes

    Private Enum Modes
        StpsxTpa
        ArtsxStp
    End Enum

    Private Enum Cols
        Guid
        Nom
        Qty
        Pct
    End Enum

    Public Sub New(ByVal oStp As Stp)
        MyBase.New()
        Me.InitializeComponent()
        mMode = Modes.ArtsxStp
        mStp = oStp
        Me.Text = "RANKING " & mStp.LangResource.GetLangText(BLL.BLLApp.Lang)
        DateTimePickerFrom.Value = "1/1/" & Year(Today)
        DateTimePickerTo.Value = Today
        refresca()
        mAllowEvents = True
    End Sub

    Public Sub New(ByVal oTpa As Tpa)
        MyBase.New()
        Me.InitializeComponent()
        mMode = Modes.StpsxTpa
        mTpa = oTpa
        Me.Text = "RANKING " & mTpa.Nom
        DateTimePickerFrom.Value = "1/1/" & Year(Today)
        DateTimePickerTo.Value = Today
        refresca()
        mAllowEvents = True
    End Sub

    Private Sub refresca()
        LoadGrid()
        ToolStripButtonRefresca.Enabled = False
    End Sub

    Private Sub LoadGrid()
        Dim sFromFch As String = Format(DateTimePickerFrom.Value, "yyyyMMdd")
        Dim sToFch As String = Format(DateTimePickerTo.Value, "yyyyMMdd")
        Dim SQL As String = ""

        Select Case mMode
            Case Modes.ArtsxStp
                SQL = "SELECT PNC.ArtGuid, ART.ord, SUM(PNC.qty) AS QTY " _
                & "FROM PNC INNER JOIN " _
                & "ART ON PNC.ArtGuid= ART.Guid INNER JOIN " _
                & "PDC ON PNC.PdcGuid = PDC.Guid " _
                & "WHERE ART.Category='" & mStp.Guid.ToString & "' AND " _
                & "PNC.cod=" & DTOPurchaseOrder.Codis.client & " AND " _
                & "(PDC.FCH BETWEEN '" & sFromFch & "' AND '" & sToFch & "') "

                If CheckBoxNoExtras.Checked Then
                    SQL = SQL & " AND PDC.EXTRA=0 "
                End If

                SQL = SQL & "GROUP BY PNC.ArtGuid, ART.ord " _
                & "ORDER BY SUM(PNC.qty) DESC"

            Case Modes.StpsxTpa
                SQL = "SELECT STP.Guid, STP.DSC, SUM(PNC.qty*PNC.Eur*(100-PNC.dto)/100) AS EUR " _
                & "FROM PNC INNER JOIN " _
                & "ART ON PNC.ArtGuid= ART.Guid INNER JOIN " _
                & "Stp ON Stp.Guid = Art.Category INNER JOIN " _
                & "PDC ON PNC.PdcGuid = PDC.Guid " _
                & "WHERE ART.EMP=" & mTpa.emp.Id & " AND " _
                & "ART.tpa =" & mTpa.Id & " AND " _
                & "PNC.cod=" & DTOPurchaseOrder.Codis.client & " AND " _
                & "(PDC.FCH BETWEEN '" & sFromFch & "' AND '" & sToFch & "') "

                If CheckBoxNoExtras.Checked Then
                    SQL = SQL & " AND PDC.EXTRA=0 "
                End If

                SQL = SQL & "GROUP BY STP.Guid, STP.DSC " _
                & "ORDER BY EUR DESC"

        End Select
        mDs = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi)


        'afegeix columna percentatje
        Dim oTb As DataTable = mDs.Tables(0)
        Dim oColPct As New DataColumn("PCT", System.Type.GetType("System.Int32"))
        oTb.Columns.Add(oColPct)

        'calcula totals
        Dim oRow As DataRow
        Dim i As Integer
        For i = 0 To oTb.Rows.Count - 1
            oRow = oTb.Rows(i)
            mTot = mTot + oRow(Cols.Qty)
        Next

        'asigna percentatje
        For i = 0 To oTb.Rows.Count - 1
            oRow = oTb.Rows(i)
            oRow("PCT") = Math.Round(100 * oRow(Cols.Qty) / mTot)
        Next

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
            With .Columns(Cols.Nom)
                .HeaderText = "producte"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            With .Columns(Cols.Pct)
                .HeaderText = "Cuota"
                .Width = 60
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#\%"
            End With
            With .Columns(Cols.Qty)
                .HeaderText = "venuts"
                .Width = 80
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                Select Case mMode
                    Case Modes.ArtsxStp
                        .DefaultCellStyle.Format = "#"
                    Case Modes.StpsxTpa
                        .DefaultCellStyle.Format = "#,##0.00 €"
                End Select

            End With
        End With
    End Sub

    Private Sub Control_Changed(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
    DateTimePickerFrom.ValueChanged, _
    DateTimePickerTo.ValueChanged, _
    CheckBoxNoExtras.CheckedChanged
        If mAllowEvents Then
            SetDirty()
        End If
    End Sub

    Private Function CurrentArt() As Art
        Dim oArt As Art = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            Dim oGuid As Guid = DataGridView1.CurrentRow.Cells(Cols.Guid).Value
            oArt = New Art(oGuid)
        End If
        Return oArt
    End Function

    Private Sub SetDirty()
        With DataGridView1
            .BackgroundColor = System.Drawing.Color.LightGray
            .ForeColor = System.Drawing.Color.Gray
        End With
        Me.ToolStripButtonRefresca.Enabled = True
    End Sub

    Private Sub MenuItemExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ToolStripButtonExcel.Click
        MatExcel.GetExcelFromDataset(mDs).Visible = True
    End Sub

    Private Function GetFooterClis() As String
        Dim s As String = ""
        Dim SQL As String = ""
        Select Case mMode
            Case Modes.ArtsxStp
                SQL = "SELECT  COUNT (DISTINCT CLI) AS CLIS " _
                & "FROM PNC INNER JOIN " _
                & "ART ON PNC.ArtGuid= ART.Guid INNER JOIN " _
                & "PDC ON PdcGuid = PDC.Guid " _
                & "WHERE ART.Category='" & mStp.Guid.ToString & "' AND " _
                & "PNC.cod=" & DTOPurchaseOrder.Codis.client & " AND " _
                & "(PDC.FCH BETWEEN '" & Format(DateTimePickerFrom.Value, "yyyyMMdd") & "' AND '" & Format(DateTimePickerTo.Value, "yyyyMMdd") & "') "
            Case Modes.StpsxTpa
                SQL = "SELECT  COUNT (DISTINCT CLI) AS CLIS " _
                & "FROM PNC INNER JOIN " _
                & "ART ON PNC.ArtGuid= ART.Guid INNER JOIN " _
                & "STP ON STP.Guid=ART.Category INNER JOIN " _
                & "PDC ON PNC.PdcGuid = PDC.Guid " _
                & "WHERE Stp.Brand ='" & mTpa.Guid.ToString & "' AND " _
                & "PNC.cod=" & DTOPurchaseOrder.Codis.client & " AND " _
                & "(PDC.FCH BETWEEN '" & Format(DateTimePickerFrom.Value, "yyyyMMdd") & "' AND '" & Format(DateTimePickerTo.Value, "yyyyMMdd") & "') "
        End Select

        Dim oDrd As SqlClient.SqlDataReader = maxisrvr.GetDataReader(SQL, maxisrvr.Databases.Maxi)
        If oDrd.Read Then
            s = "total " & oDrd("CLIS") & " clients"
        End If
        oDrd.Close()
        Return s
    End Function

    Private Sub ExcelToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButtonExcel.Click
        MatExcel.GetExcelFromDataset(mDs).Visible = True
    End Sub

    Private Sub RefrescaToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButtonRefresca.Click
        refresca()
        With DataGridView1
            .BackgroundColor = System.Drawing.Color.White
            .ForeColor = System.Drawing.Color.Black
        End With
    End Sub


    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oArt As Art = CurrentArt()

        If oArt IsNot Nothing Then
            Dim oMenu_Art As New Menu_Art(oArt)
            AddHandler oMenu_Art.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_Art.Range)
        End If

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim i As Integer
        Dim j As Integer
        Dim iFirstRow As Integer
        Dim iFirstVisibleCell As Integer = Cols.Nom

        If DataGridView1.Rows.Count > 0 Then
            i = DataGridView1.CurrentRow.Index
            j = DataGridView1.CurrentCell.ColumnIndex
            iFirstRow = DataGridView1.FirstDisplayedScrollingRowIndex()
        End If

        LoadGrid()

        If DataGridView1.Rows.Count = 0 Then
        Else
            DataGridView1.FirstDisplayedScrollingRowIndex() = iFirstRow

            If i > DataGridView1.Rows.Count - 1 Then
                DataGridView1.CurrentCell = DataGridView1.Rows(DataGridView1.Rows.Count - 1).Cells(j)
            Else
                DataGridView1.CurrentCell = DataGridView1.Rows(i).Cells(iFirstVisibleCell)
            End If
        End If
    End Sub

End Class