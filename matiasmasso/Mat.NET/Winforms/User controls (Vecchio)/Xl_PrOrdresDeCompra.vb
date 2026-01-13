
Imports Excel = Microsoft.Office.Interop.Excel

Public Class Xl_PrOrdresDeCompra
    Private mRevista As PrRevista
    Private mEditorial As PrEditorial
    Private mSource As Sources
    Private mDs As DataSet
    Private mAllowEvents As Boolean = False

    Private Enum Sources
        editorial
        revista
    End Enum

    Private Enum Cols
        Guid
        Fch
        Num
        Eur
    End Enum

    Public WriteOnly Property Editorial() As PrEditorial
        Set(ByVal value As PrEditorial)
            mEditorial = value
            mSource = Sources.editorial
            LoadGrid()
            SetContextMenu()
        End Set
    End Property

    Public WriteOnly Property Revista() As PrRevista
        Set(ByVal value As PrRevista)
            mRevista = value
            mSource = Sources.revista
            LoadGrid()
            SetContextMenu()
        End Set
    End Property

    Private Sub LoadGrid()
        Dim SQL As String = ""

        Select Case mSource
            Case Sources.editorial
                SQL = "SELECT O.GUID,O.FCH,O.NUM,0 FROM PRORDREDECOMPRA O " _
                & "WHERE O.EMP=@EMP AND O.EDITORIAL=@EDITORIAL " _
                & "ORDER BY O.FCH DESC, O.NUM DESC"
                mDs = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi, "@EMP", mEditorial.Emp.Id, "@EDITORIAL", mEditorial.Id)
            Case Sources.revista
                SQL = "SELECT O.GUID,O.FCH,O.NUM,0 FROM PRORDREDECOMPRA O " _
                & "WHERE O.REVISTA LIKE @REVISTA " _
                & "ORDER BY O.FCH DESC, O.NUM DESC"
                mDs = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi, "@REVISTA", mRevista.Guid.ToString)
        End Select

        Dim oTb As DataTable = mDs.Tables(0)
        mAllowEvents = False
        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
            End With
            .DataSource = oTb
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = False
            .RowHeadersVisible = False
            .MultiSelect = True
            .AllowUserToAddRows = False
            .AllowUserToResizeRows = False

            With .Columns(Cols.Guid)
                .Visible = False
            End With
            With .Columns(Cols.Fch)
                .HeaderText = "Data"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 65
                .DefaultCellStyle.Format = "dd/MM/yy"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
            With .Columns(Cols.Num)
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            With .Columns(Cols.Eur)
                .HeaderText = "Import"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 70
                .DefaultCellStyle.Format = "#,##0.00;-#,##0.00;"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
        End With
        mAllowEvents = True
    End Sub

    Private Function CurrentItem() As PrOrdreDeCompra
        Dim oRetVal As PrOrdreDeCompra = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            oRetVal = New PrOrdreDeCompra(New Guid(oRow.Cells(Cols.Guid).Value.ToString))
        End If
        Return oRetVal
    End Function

    Private Function CurrentItems() As PrOrdresDeCompra
        Dim oItems As New PrOrdresDeCompra
        Dim oItem As PrOrdreDeCompra = Nothing

        If DataGridView1.SelectedRows.Count > 0 Then
            Dim oRow As DataGridViewRow
            Dim sGuid As String = ""
            For Each oRow In DataGridView1.SelectedRows
                sGuid = oRow.Cells(Cols.Guid).Value.ToString
                oItem = New PrOrdreDeCompra(New Guid(sGuid))
                oItems.Add(oItem)
            Next
        Else
            oItem = CurrentItem()
            If oItem IsNot Nothing Then
                oItems.Add(oItem)
            End If
        End If
        Return oItems
    End Function


    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oMenuItem As ToolStripMenuItem = Nothing
        Dim oOrdre As PrOrdreDeCompra = CurrentItem()

        If oOrdre IsNot Nothing Then
            oMenuItem = New ToolStripMenuItem("Zoom", Nothing, AddressOf Zoom)
            oContextMenu.Items.Add(oMenuItem)

            oMenuItem = New ToolStripMenuItem("Excel", My.Resources.Excel, AddressOf Do_Excel)
            oContextMenu.Items.Add(oMenuItem)

            oMenuItem = New ToolStripMenuItem("Eliminar", My.Resources.del, AddressOf Do_Delete)
            oMenuItem.Enabled = oOrdre.allowDelete
            oContextMenu.Items.Add(oMenuItem)

        End If

        oMenuItem = New ToolStripMenuItem("Afegir nova", Nothing, AddressOf AddNew)
        oContextMenu.Items.Add(oMenuItem)

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Zoom(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oOrdre As PrOrdreDeCompra = CurrentItem()
        Dim oFrm As New Frm_PrOrdreDeCompra(oOrdre)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Do_Delete(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oOrdre As PrOrdreDeCompra = CurrentItem()
        Dim rc As MsgBoxResult = MsgBox("eliminem aquesta ordre de compra?", MsgBoxStyle.OkCancel, "MAT.NET")
        If rc = MsgBoxResult.Ok Then
            If oOrdre.DELETE Then
                MsgBox("Ordre eliminada correctament", MsgBoxStyle.Information, "MAT.NET")
                RefreshRequest(sender, e)
            Else
                MsgBox("Operacio no efectuada per estar relacionada amb dades preexistents", MsgBoxStyle.Exclamation, "MAT.NET")
            End If
        Else
            MsgBox("Operacio no efectuada per instruccions del usuari", MsgBoxStyle.Exclamation, "MAT.NET")
        End If
    End Sub

    Private Sub AddNew(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oOrdre As PrOrdreDeCompra = Nothing
        Select Case mSource
            Case Sources.editorial
                oOrdre = mEditorial.NewOrdreDeCompra()
            Case Sources.revista
                oOrdre = mRevista.NewOrdreDeCompra()
        End Select
        Dim oFrm As New Frm_PrOrdreDeCompra(oOrdre)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim i As Integer
        Dim j As Integer
        Dim iFirstRow As Integer
        Dim iFirstVisibleCell As Integer = Cols.Fch
        Dim oGrid As DataGridView = DataGridView1

        If oGrid.CurrentRow IsNot Nothing Then
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


    Private Sub DataGridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.DoubleClick
        If DataGridView1.CurrentRow Is Nothing Then
            AddNew(sender, e)
        Else
            Zoom(sender, e)
        End If
    End Sub

    Private Sub DataGridView1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.SelectionChanged
        If mAllowEvents Then
            SetContextMenu()
        End If
    End Sub


    Private Sub Do_Excel(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oApp As New Excel.Application()
        oApp.UserControl = True
        Dim oldCI As System.Globalization.CultureInfo = _
            System.Threading.Thread.CurrentThread.CurrentCulture
        System.Threading.Thread.CurrentThread.CurrentCulture = _
            New System.Globalization.CultureInfo("en-US")

        Dim oWb As Excel.Workbook = oApp.Workbooks.Add()
        Dim oSheet As Excel.Worksheet = oWb.ActiveSheet
        With oSheet
            .Cells.Font.Size = 9
        End With

        Dim oItems As PrOrdresDeCompra = CurrentItems()
        Dim oItem As PrOrdreDeCompra = Nothing

        Dim i As Integer = 1
        'Dim j As Integer

        For Each oItem In oItems
            i = i + 1
            oSheet.Cells(i, 1) = "http://www.matiasmasso.es/doc?Id=" & oItem.Guid.ToString
            oSheet.Cells(i, 2) = oItem.Num
            oSheet.Cells(i, 3) = oItem.Fch
            oSheet.Cells(i, 4) = oItem.NetAmt.Eur
        Next

        System.Threading.Thread.CurrentThread.CurrentCulture = oldCI
        oApp.Visible = True
    End Sub

End Class
