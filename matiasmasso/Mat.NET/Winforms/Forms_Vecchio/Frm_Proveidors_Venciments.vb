Imports System.Data.SqlClient

Imports Excel = Microsoft.Office.Interop.Excel


Public Class Frm_Proveidors_Venciments

    Private mEmp as DTOEmp = BLL.BLLApp.Emp
    Private mDs As DataSet

    Private Enum CodisDeLinia
        Blank
        Epigraf
        Itm
    End Enum

    Private Enum Fields
        Guid
        Id
        Vto
        Cli
    End Enum

    Private Enum Cols
        Guid
        Id
        Nom
        Amt
        Cur
        Fra
        Fch
        Obs
        CodiDeLinia
    End Enum


    Private Sub Frm_Proveidors_Venciments_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        LoadGrid()
    End Sub

    Private Sub LoadGrid()
        Dim Sql As String = "SELECT Pnd.Guid, PND.Id, PND.vto, PND.cli " _
        & "FROM PND INNER JOIN CLX ON PND.ContactGuid = Clx.Guid " _
        & "WHERE PND.EMP=" & mEmp.Id & " AND " _
        & "PND.AD='A' AND " _
        & "PND.STATUS=" & Pnd.StatusCod.pendent & " " _
        & "ORDER BY PND.VTO,CLX.CLX,PND.CLI,FRA"
        Dim oDs As DataSet =  DAL.SQLHelper.GetDataset(SQL, New List(Of Exception))
        Dim oTb1 As DataTable = oDs.Tables(0)
        Dim oTb2 As DataTable = CreateDataTable()
        mDs = New DataSet
        mDs.Tables.Add(oTb2)
        Dim oRow As DataRow
        Dim Vto As Date = DateTime.MinValue
        Dim oPnd As Pnd
        Dim oSum As DTOAmt = Nothing
        Dim Firstrec As Boolean = True
        Dim oRowVto As DataRow = Nothing

        For Each oRow In oTb1.Rows
            If Vto <> oRow(Fields.Vto) Then
                If Firstrec Then
                    Firstrec = False
                Else
                    If Not oSum Is Nothing Then
                        oRowVto(Cols.Amt) = oSum.Eur
                        oRowVto(Cols.Cur) = "EUR"
                    End If
                End If
                oSum = BLLApp.EmptyAmt
                Vto = oRow(Fields.Vto)
                oRowVto = AddRowVto(Vto)
            End If
            oPnd = New Pnd(oRow(Cols.Id))
            oSum.Add(oPnd.Amt)
            AddRowItm(oPnd)
        Next

        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
                .DefaultCellStyle.BackColor = Color.Transparent
            End With
            .DataSource = oTb2
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = True
            .AllowUserToResizeRows = False

            With .Columns(Cols.Guid)
                .Visible = False
            End With
            With .Columns(Cols.Id)
                .Visible = False
            End With
            With .Columns(Cols.Nom)
                .HeaderText = "proveidor"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            With .Columns(Cols.Amt)
                .HeaderText = "import"
                .Width = 80
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.Cur)
                .Visible = False
            End With
            With .Columns(Cols.Fra)
                .HeaderText = "factura"
                .Width = 80
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.Fch)
                .HeaderText = "data"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "dd/MM/yy"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
            With .Columns(Cols.Obs)
                .HeaderText = "observacions"
                .Width = 150
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With
            With .Columns(Cols.CodiDeLinia)
                .Visible = False
            End With
        End With
    End Sub

    Private Function AddRowVto(ByVal DtVto As DateTime) As DataRow
        Dim oTb As DataTable = mDs.Tables(0)
        Dim oRow As DataRow = oTb.NewRow
        oRow(Cols.CodiDeLinia) = CodisDeLinia.Blank
        oRow(Cols.Nom) = ""
        oTb.Rows.Add(oRow)

        oRow = oTb.NewRow
        oRow(Cols.CodiDeLinia) = CodisDeLinia.Epigraf
        oRow(Cols.Nom) = "Vto." & DtVto.ToShortDateString
        oTb.Rows.Add(oRow)
        Return oRow
    End Function

    Private Sub AddRowItm(ByVal oPnd As Pnd)
        Dim oTb As DataTable = mDs.Tables(0)
        Dim oRow As DataRow = oTb.NewRow
        oRow(Cols.CodiDeLinia) = CodisDeLinia.Itm
        oRow(Cols.Guid) = oPnd.guId
        oRow(Cols.Id) = oPnd.Id
        oRow(Cols.Nom) = oPnd.Contact.Clx
        oRow(Cols.Amt) = oPnd.Amt.Val
        oRow(Cols.Cur) = oPnd.Amt.Cur.Tag
        oRow(Cols.Fra) = oPnd.FraNum
        oRow(Cols.Fch) = CDate(oPnd.Fch).ToShortDateString
        oRow(Cols.Obs) = oPnd.Fpg
        oTb.Rows.Add(oRow)
    End Sub

    Private Function CreateDataTable() As DataTable
        Dim oTb As New DataTable
        With oTb.Columns
            .Add(New DataColumn("Guid", System.Type.GetType("System.Guid")))
            .Add(New DataColumn("ID", System.Type.GetType("System.Int32")))
            .Add(New DataColumn("NOM", System.Type.GetType("System.String")))
            .Add(New DataColumn("AMT", System.Type.GetType("System.Decimal")))
            .Add(New DataColumn("CUR", System.Type.GetType("System.String")))
            .Add(New DataColumn("FRA", System.Type.GetType("System.String")))
            .Add(New DataColumn("FCH", System.Type.GetType("System.String")))
            .Add(New DataColumn("OBS", System.Type.GetType("System.String")))
            .Add(New DataColumn("CLN", System.Type.GetType("System.Int32")))
        End With
        Return oTb
    End Function

    Private Sub ExcelToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExcelToolStripButton.Click

        Dim oApp As New Excel.Application()
        oApp.UserControl = True
        Dim oldCI As System.Globalization.CultureInfo = _
            System.Threading.Thread.CurrentThread.CurrentCulture
        System.Threading.Thread.CurrentThread.CurrentCulture = _
            New System.Globalization.CultureInfo("en-US")


        Dim oWb As Excel.Workbook = oApp.Workbooks.Add()
        Dim oSheet As Excel.Worksheet = oWb.ActiveSheet
        Dim oRow As DataGridViewRow
        Dim i As Integer
        Dim VGap As Integer = 5
        Dim oTb As DataTable = mDs.Tables(0)

        Dim j As Integer
        Dim iFirstRow As Integer
        Dim iLastRow As Integer


        If DataGridView1.SelectedRows.Count > 1 Then
            Dim DblSdo As Decimal = 0

            iFirstRow = DataGridView1.SelectedRows(0).Index
            iLastRow = DataGridView1.SelectedRows(DataGridView1.SelectedRows.Count - 1).Index
            If iFirstRow > iLastRow Then
                Dim iTmp As Integer = iFirstRow
                iFirstRow = iLastRow
                iLastRow = iTmp
            End If

            j = VGap
        Else
            j = VGap
            iFirstRow = 0
            iLastRow = mDs.Tables(0).Rows.Count - 1
        End If

        Dim RowFirst As Integer = j

        Dim sTxt As String = ""
        Dim sUrl As String = ""
        Dim oRange As Excel.Range
        Dim oFraRange As Excel.Range
        Dim sNom As String = ""
        Dim iRowProveidor As Integer = 0
        Dim oPnd As Pnd
        For i = iFirstRow To iLastRow
            oRow = DataGridView1.Rows(i)

            If Not IsDBNull(oRow.Cells(Cols.Id).Value) Then
                If oRow.Cells(Cols.Nom).Value <> sNom Then
                    j += 1
                    sNom = oRow.Cells(Cols.Nom).Value
                    iRowProveidor = j
                    oSheet.Cells(j, 1) = sNom
                    oSheet.Cells(j, 5) = 0
                    j += 1
                End If


                oPnd = New Pnd(oRow.Cells(Cols.Id).Value)
                If oPnd.Cca Is Nothing Then
                    oSheet.Cells(j, 3) = oRow.Cells(Cols.Fra).Value
                Else
                    Dim oCca As Cca = oPnd.Cca
                    If oCca.DocExists Then
                        oFraRange = oSheet.Cells(j, 3)
                        sUrl = BLL.BLLDocFile.DownloadUrl(oCca.DocFile, True)
                        oSheet.Hyperlinks.Add(oFraRange, sUrl, , , oRow.Cells(Cols.Fra).Value)
                    Else
                        oSheet.Cells(j, 3) = oRow.Cells(Cols.Fra).Value
                    End If
                End If

                oSheet.Cells(j, 2) = oPnd.Fch.ToShortDateString
                oRange = oSheet.Cells(j, 4)
                oRange.Value2 = oRow.Cells(Cols.Amt).Value
                Dim sCur As String = IIf(IsDBNull(oRow.Cells(Cols.Cur).Value), "", oRow.Cells(Cols.Cur).Value)
                Select Case sCur.ToUpper
                    Case "EUR"
                        oRange.NumberFormat = "#,##0.00 €;-#,##0.00 €;#"
                    Case "GBP"
                        oRange.NumberFormat = "£ #,##0.00;£ -#,##0.00;#"
                    Case "USD"
                        oRange.NumberFormat = "$ #,##0.00;$ -#,##0.00;#"
                    Case Else
                        oRange.NumberFormat = "#,##0.00 " & sCur & ";-#,##0.00 " & sCur & ";#"
                End Select
                If Not IsDBNull(oRow.Cells(Cols.Amt).Value) Then
                    oSheet.Cells(iRowProveidor, 5) = oSheet.Cells(iRowProveidor, 5).value + oRow.Cells(Cols.Amt).Value
                End If
                'oSheet.Cells(j, 2) = oRow.Cells(Cols.Fch).Value
                'oSheet.Cells(j, 2) = oRow.Cells(Cols.Data).Value
                j += 1
            End If
        Next i

        i = 3
        oSheet.Cells(i, 1) = "proveidor"
        oSheet.Cells(i, 2) = "data"
        oSheet.Cells(i, 3) = "factura"
        oSheet.Cells(i, 4) = "import"
        oSheet.Cells(i, 5) = "suma"
        oSheet.Columns(2).numberformat = "dd/MM/yyyy"
        oSheet.Columns(3).numberformat = "@"
        'oSheet.Columns(5).numberformat = "#,##0.00;-#,##0.00;#"
        'oSheet.Range("A" & i & ":F" & i).Style = "Epigrafs"
        oSheet.Cells(i, 2).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter
        oSheet.Cells(i, 3).HorizontalAlignment = Excel.XlHAlign.xlHAlignRight
        'oSheet.Cells(i, 6).HorizontalAlignment = Excel.XlHAlign.xlHAlignRight

        oSheet.Cells.Font.Size = 9

        oApp.Visible = True

        System.Threading.Thread.CurrentThread.CurrentCulture = oldCI

    End Sub

    Private Function CurrentPnd() As DTOPnd
        Dim retval As DTOPnd = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            If Not IsDBNull(oRow.Cells(Cols.Guid).Value) Then
                Dim oGuid As Guid = oRow.Cells(Cols.Guid).Value
                retval = New DTOPnd(oGuid)
            End If
        End If
        Return retval
    End Function

    Private Function CurrentProveidor() As DTOProveidor
        Dim retval As DTOProveidor = Nothing
        Dim oPnd As DTOPnd = CurrentPnd()
        If oPnd IsNot Nothing Then
            retval = New DTOProveidor(oPnd.Contact.Guid)
        End If
        Return retval
    End Function

    Private Sub PagarToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles PagarToolStripMenuItem.Click
        Dim oFrm As New Frm_Pagament(CurrentProveidor)
        oFrm.Show()
    End Sub

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip

        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            Dim oColType As CodisDeLinia = oRow.Cells(Cols.CodiDeLinia).Value
            Select Case oColType
                Case CodisDeLinia.Itm
                    Dim oPnd As New Pnd(oRow.Cells(Cols.Id).Value)
                    Dim oMenu_Pnd As New Menu_Pnd(oPnd)
                    AddHandler oMenu_Pnd.AfterUpdate, AddressOf RefreshRequest
                    oContextMenu.Items.AddRange(oMenu_Pnd.Range)
            End Select
        End If
        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim i As Integer
        Dim j As Integer
        Dim iFirstRow As Integer
        Dim iFirstVisibleCell As Integer = Cols.Fra

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
        SetContextMenu()
    End Sub

    Private Sub DataGridView1_RowPrePaint(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowPrePaintEventArgs) Handles DataGridView1.RowPrePaint
        Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)

        Dim oColType As CodisDeLinia = oRow.Cells(Cols.CodiDeLinia).Value
        If oColType = CodisDeLinia.Epigraf Then
            PaintGradientRowBackGround(e, Color.AliceBlue)
        Else
            oRow.DefaultCellStyle.BackColor = Color.WhiteSmoke
        End If
    End Sub

    Private Sub PaintGradientRowBackGround(ByVal e As System.Windows.Forms.DataGridViewRowPrePaintEventArgs, ByVal oColor As System.Drawing.Color)

        ' Do not automatically paint the focus rectangle.
        e.PaintParts = e.PaintParts And Not DataGridViewPaintParts.Focus


        ' Determine whether the cell should be painted with the 
        ' custom selection background.
        Dim oBgColor As System.Drawing.Color = Color.WhiteSmoke
        'If (e.State And DataGridViewElementStates.Selected) = _
        'DataGridViewElementStates.Selected Then
        'oBgColor = DataGridView1.DefaultCellStyle.SelectionBackColor
        'End If

        ' Calculate the bounds of the row.
        Dim rowBounds As New Rectangle( _
            0, e.RowBounds.Top, _
            Me.DataGridView1.Columns.GetColumnsWidth( _
            DataGridViewElementStates.Visible) - _
            Me.DataGridView1.HorizontalScrollingOffset + 1, _
            e.RowBounds.Height)

        ' Paint the custom selection background.
        Dim backbrush As New System.Drawing.Drawing2D.LinearGradientBrush( _
        rowBounds, _
        oColor, _
        oBgColor, _
        System.Drawing.Drawing2D.LinearGradientMode.Horizontal)
        'System.Drawing.Drawing2D.LinearGradientBrush(rowBounds, _
        'e.InheritedRowStyle.BackColor, _
        'oColor, _
        'System.Drawing.Drawing2D.LinearGradientMode.Horizontal)
        Try
            e.Graphics.FillRectangle(backbrush, rowBounds)
        Finally
            backbrush.Dispose()
        End Try
    End Sub

    Private Sub DataGridView1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.SelectionChanged
        SetContextMenu()
    End Sub

    Private Sub DataGridView1_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        Select Case e.ColumnIndex
            Case Cols.Amt
                If Not IsDBNull(e.Value) Then
                    Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                    Dim sCur As String = oRow.Cells(Cols.Cur).Value
                    Select Case sCur
                        Case "EUR"
                            e.Value = Format(CDbl(e.Value), "#,##0.00") & " €"
                        Case "GBP"
                            e.Value = Format(CDbl(e.Value), "#,##0.00") & " £"
                        Case "USD"
                            e.Value = Format(CDbl(e.Value), "#,##0.00") & " $"
                        Case Else
                            e.Value = Format(CDbl(e.Value), "#,##0.00") & " " & sCur
                    End Select
                End If
        End Select
    End Sub


    Private Sub DataGridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.DoubleClick
        Dim oPnd As DTOPnd = CurrentPnd()
        If oPnd IsNot Nothing Then
            Dim oFrm As New Frm_Contact_Pnd(oPnd)
            AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
            oFrm.Show()
        End If
    End Sub


End Class