Public Class Xl_PriceListItems_Supplier
    Private _PriceList As DTOPriceListSupplier
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean
    Private _LastMouseDownRectangle As System.Drawing.Rectangle

    'per entrades a cataleg
    Private _LastPricelistCustomer As DTOPricelistCustomer
    Private _LastCategory As DTOProductCategory

    Public Event RequestToRefresh(sender As Object, e As MatEventArgs)
    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)
    Public Event RequestToAddClon(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)
    Public Event AfterUpdate(sender As Object, e As MatEventArgs)


    Private Enum Cols
        Ico
        Ref
        EAN
        Dsc
        Cost
        Eur
        RRPP
        InnerPack
    End Enum

    Public Shadows Sub Load(ByVal oPriceList As DTOPriceListSupplier, Optional sFilter As String = "")
        _PriceList = oPriceList
        Static PropertiesSet As Boolean
        If Not PropertiesSet Then
            SetProperties()
            PropertiesSet = True
        End If

        refresca()
        ApplyFilter(sFilter)
    End Sub

    Private Sub refresca()
        _AllowEvents = False


        Dim iFirstRow As Integer
        Dim iFirstVisibleCell As Integer = DataGridView1.Columns.GetFirstColumn(DataGridViewElementStates.Visible).Index
        Dim oGrid As DataGridView = DataGridView1
        Dim i As Integer
        Dim j As Integer
        If oGrid.CurrentRow IsNot Nothing Then
            i = oGrid.CurrentRow.Index
            j = oGrid.CurrentCell.ColumnIndex
            iFirstRow = oGrid.FirstDisplayedScrollingRowIndex()
        End If

        _ControlItems = New ControlItems
        For Each oItem As DTOPriceListItem_Supplier In _PriceList.Items
            Dim oControlItem As New ControlItem(oItem, _PriceList)
            _ControlItems.Add(oControlItem)
        Next

        DataGridView1.DataSource = _ControlItems

        If oGrid.Rows.Count = 0 Then
        Else
            oGrid.FirstDisplayedScrollingRowIndex() = iFirstRow

            If i > oGrid.Rows.Count - 1 Then
                oGrid.CurrentCell = oGrid.Rows(oGrid.Rows.Count - 1).Cells(j)
            Else
                oGrid.CurrentCell = oGrid.Rows(i).Cells(iFirstVisibleCell)
            End If
        End If


        SetContextMenu()
        _AllowEvents = True
    End Sub

    Public Sub ApplyFilter(sFilter As String)
        DataGridView1.CurrentCell = Nothing
        sFilter = sFilter.ToUpper
        For Each oRow As DataGridViewRow In DataGridView1.Rows
            Dim src As String = oRow.Cells(Cols.Ref).Value.ToString & "|" & oRow.Cells(Cols.EAN).Value.ToString & "|" & oRow.Cells(Cols.Dsc).Value.ToString
            If oRow.Visible <> src.ToUpper.Contains(sFilter) Then
                oRow.Visible = Not oRow.Visible
            End If
        Next
    End Sub

    Public ReadOnly Property Values As List(Of DTOPriceListItem_Supplier)
        Get
            Dim retval As New List(Of DTOPriceListItem_Supplier)
            For Each oControlItem As ControlItem In _ControlItems
                retval.Add(oControlItem.Source)
            Next
            Return retval
        End Get
    End Property

    Public ReadOnly Property Value As DTOPriceListItem_Supplier
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As DTOPriceListItem_Supplier = oControlItem.Source
            Return retval
        End Get
    End Property


    Private Sub SetProperties()
        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
            End With

            .AutoGenerateColumns = False
            .Columns.Clear()

            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = True
            .AllowUserToResizeRows = False
            .AllowDrop = False
            .ReadOnly = True

            .Columns.Add(New DataGridViewImageColumn)
            With DirectCast(.Columns(Cols.Ico), DataGridViewImageColumn)
                .DataPropertyName = "Ico"
                .HeaderText = ""
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 16
                .DefaultCellStyle.NullValue = Nothing
            End With

            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Ref)
                .DataPropertyName = "Ref"
                .HeaderText = "Referencia"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 100
                .SortMode = DataGridViewColumnSortMode.Automatic
            End With

            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.EAN)
                .DataPropertyName = "EAN"
                .HeaderText = "EAN"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 100
                .SortMode = DataGridViewColumnSortMode.Automatic
            End With

            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Dsc)
                .DataPropertyName = "Description"
                .HeaderText = "Descripció"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                .SortMode = DataGridViewColumnSortMode.Automatic
            End With

            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Cost)
                .DataPropertyName = "Cost"
                .HeaderText = "Cost"
                .Width = 90
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,##0.00;-#,##0.00;#"
            End With

            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Eur)
                .Visible = (_PriceList.Cur.UnEquals(DTOCur.Eur))
                .DataPropertyName = "Eur"
                .HeaderText = "Euros"
                .Width = 90
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
            End With

            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.RRPP)
                .DataPropertyName = "RRPP"
                .HeaderText = "RRPP"
                .Width = 90
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,##0.00;-#,##0.00;#"
            End With

            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.InnerPack)
                .DataPropertyName = "InnerPack"
                .HeaderText = "unitats/caixa"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 80
                .SortMode = DataGridViewColumnSortMode.Automatic
            End With

        End With

    End Sub

    Private Function SelectedControlItems() As ControlItems
        Dim retval As New ControlItems
        For Each oRow As DataGridViewRow In DataGridView1.SelectedRows
            Dim oControlItem As ControlItem = oRow.DataBoundItem
            retval.Add(oControlItem)
        Next

        If retval.Count = 0 Then retval.Add(CurrentControlItem)
        Return retval
    End Function

    Private Function SelectedItems() As List(Of DTOPriceListItem_Supplier)
        Dim retval As New List(Of DTOPriceListItem_Supplier)
        For Each oRow As DataGridViewRow In DataGridView1.SelectedRows
            Dim oControlItem As ControlItem = oRow.DataBoundItem
            retval.Add(oControlItem.Source)
        Next

        If retval.Count = 0 Then
            Dim oCurrentControlItem As ControlItem = CurrentControlItem()
            If oCurrentControlItem IsNot Nothing Then
                retval.Add(oCurrentControlItem.Source)
            End If
        End If
        Return retval
    End Function

    Private Function CurrentControlItem() As ControlItem
        Dim retval As ControlItem = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            retval = oRow.DataBoundItem
        End If
        Return retval
    End Function

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oSelectedItems As List(Of DTOPriceListItem_Supplier) = SelectedItems()

        If oSelectedItems.Count > 0 Then
            Dim oMenu As New Menu_PriceListItem_Supplier(SelectedItems)
            With oMenu
                .LastPricelistCustomer = _LastPricelistCustomer
                .LastCategory = _LastCategory
            End With
            AddHandler oMenu.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu.Range)

            Dim oMenuItemAddClon As New ToolStripMenuItem("afegir clon", My.Resources.clip, AddressOf AddClon)
            oContextMenu.Items.Add(oMenuItemAddClon)
            oContextMenu.Items.Add("-")
        End If

        oContextMenu.Items.Add("afegir linia", Nothing, AddressOf Do_AddNew)
        oContextMenu.Items.Add(IIf(oSelectedItems.Count = 1, "eliminar linia", "eliminar " & oSelectedItems.Count & " linies"), Nothing, AddressOf Do_Delete)
        oContextMenu.Items.Add("importar de fitxer Excel", Nothing, AddressOf Do_ImportFromExcel)
        oContextMenu.Items.Add("Excel", My.Resources.Excel, AddressOf Do_ExportToExcel)

        oContextMenu.Items.Add("refresca", Nothing, AddressOf Do_Refresca)

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Do_Refresca()
        RefreshRequest(Me, MatEventArgs.Empty)
    End Sub

    Private Sub Do_ImportFromExcel()
        Dim sTitle = String.Format("Importar tarifa de {0}", _PriceList.Proveidor.FullNom)
        Dim sFilename As String = ""
        If UIHelper.LoadExcelDialog(sFilename, sTitle) Then
            Dim sFields As String() = {"Codi proveidor", "Ean", "Descripció", "Preu", "Pvp", "Uds/pack"} 'REF / EAN / DESCRIPTION / PRICE / RETAIL / INNER PACK
            Dim oFrm As New Frm_ExcelColumsMapping(sFields, sFilename)
            If Not oFrm.Cancel Then
                AddHandler oFrm.AfterUpdate, AddressOf AfterImportFromExcel
                oFrm.Show()
            End If
        End If
    End Sub

    Private Async Sub AfterImportFromExcel(sender As Object, e As MatEventArgs)
        Dim exs As New List(Of Exception)
        Dim oSheet As MatHelperStd.ExcelHelper.Sheet = e.Argument
        Dim iRepes As Integer
        Dim iActualitzats As Integer
        Dim iAdded As Integer

        Dim oSrcForm As Frm_ExcelColumsMapping = sender

        Dim oItemsToAdd = Await FEB2.PriceListItemsSupplier.FromExcelSheet(exs, _PriceList, oSheet)
        For Each oItem As DTOPriceListItem_Supplier In oItemsToAdd
            Dim oExistingItem As ControlItem = _ControlItems.FirstOrDefault(Function(x) x.Ref = oItem.Ref)
            If oExistingItem Is Nothing Then
                _ControlItems.Add(New ControlItem(oItem, _PriceList))
                iAdded += 1
            Else
                If MatchValues(oExistingItem, oItem) Then
                    iRepes += 1
                Else
                    oExistingItem.Update(oItem, _PriceList)
                    iActualitzats += 1
                End If
            End If
        Next

        'refresca()

        DataGridView1.DataSource = _ControlItems


        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("llegits: " & oItemsToAdd.Count)
        sb.AppendLine("repetits: " & iRepes)
        sb.AppendLine("actualitzats: " & iActualitzats)
        sb.AppendLine("afegits: " & iAdded)
        MsgBox(sb.ToString, MsgBoxStyle.Information)

        If iActualitzats + iAdded > 0 Then
            RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
        End If
    End Sub

    Private Sub Do_ExportToExcel()
        Dim exs As New List(Of Exception)
        If FEB2.PriceListSupplier.Load(exs, _PriceList) Then
            Dim oSheet = DTOPriceListSupplier.Excel(_PriceList)
            If Not UIHelper.ShowExcel(oSheet, exs) Then
                UIHelper.WarnError(exs)
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub Do_Delete()
        Dim oSelectedItems As List(Of DTOPriceListItem_Supplier) = SelectedItems()
        Dim msg As String = IIf(oSelectedItems.Count = 1, "eliminem aquesta linia?", "eliminem " & oSelectedItems.Count & " linies?")
        Dim rc As MsgBoxResult = MsgBox(msg, MsgBoxStyle.OkCancel)
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If Await FEB2.PriceListItemsSupplier.Delete(exs, oSelectedItems) Then
                RefreshRequest(Me, MatEventArgs.Empty)
            Else
                UIHelper.WarnError(exs)
            End If
        End If
    End Sub

    Private Function MatchValues(oControlItem As ControlItem, oPriceListItem As DTOPriceListItem_Supplier) As Boolean
        Dim retval As Boolean = True
        With oControlItem.Source
            If .EAN <> oPriceListItem.EAN Then retval = False
            If .Price <> oPriceListItem.Price Then retval = False
            If .InnerPack <> oPriceListItem.InnerPack Then retval = False
            If .Retail <> oPriceListItem.Retail Then retval = False
        End With
        Return retval
    End Function

    Private Sub Do_AddNew()
        RaiseEvent RequestToAddNew(Me, MatEventArgs.Empty)
    End Sub

    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles DataGridView1.DoubleClick
        Dim oSelectedValue As DTOPriceListItem_Supplier = CurrentControlItem.Source
        Dim oFrm As New Frm_PriceListItem_Supplier(oSelectedValue)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView1.SelectionChanged
        If _AllowEvents Then
            RaiseEvent ValueChanged(Me, New MatEventArgs(CurrentControlItem.Source))
            SetContextMenu()
        End If
    End Sub

    Private Sub DataGridView1_RowsRemoved(sender As Object, e As DataGridViewRowsRemovedEventArgs) Handles DataGridView1.RowsRemoved
        RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
    End Sub

    Private Sub RefreshRequest(sender As Object, e As MatEventArgs)
        If TypeOf (e.Argument) Is DTOPricelistItemCustomer Then
            Dim item As DTOPricelistItemCustomer = e.Argument
            _LastPricelistCustomer = item.Parent
            _LastCategory = item.Sku.Category
        End If

        RaiseEvent RequestToRefresh(Me, MatEventArgs.Empty)
    End Sub

    Private Sub DataGridView1_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles DataGridView1.MouseDown
        Dim iInterval As Integer = 1
        If e.Button = System.Windows.Forms.MouseButtons.Left Then
            Dim hit As DataGridView.HitTestInfo = sender.HitTest(e.X, e.Y)
            If hit.RowIndex >= 0 And hit.ColumnIndex >= 0 Then
                _LastMouseDownRectangle = New Rectangle(e.X - iInterval, e.Y - iInterval, 2 * iInterval, 2 * iInterval)
            End If
        End If
    End Sub

    Private Sub DataGridView1_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles _
    DataGridView1.MouseMove
        If e.Button = System.Windows.Forms.MouseButtons.Left Then
            If Not _LastMouseDownRectangle.Contains(e.X, e.Y) Then
                Dim hit As DataGridView.HitTestInfo = sender.HitTest(_LastMouseDownRectangle.X, _LastMouseDownRectangle.Y)
                If hit.RowIndex >= 0 And hit.ColumnIndex >= 0 Then
                    Dim oItems As List(Of DTOPriceListItem_Supplier) = SelectedItems()
                    Dim oRc As DragDropEffects = sender.DoDragDrop(oItems, DragDropEffects.Copy)
                    Select Case oRc
                        Case DragDropEffects.Copy
                            'RefreshRequestArts(sender, e)
                        Case DragDropEffects.None
                            'falla dragdrop
                    End Select
                End If
            End If
        End If
    End Sub

    Private Sub DataGridView1_RowPrePaint(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowPrePaintEventArgs) Handles DataGridView1.RowPrePaint
        'Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)

        'Dim oItem As DTOPriceListItem_Supplier = oRow.DataBoundItem

        '        If oItem.Art Is Nothing Then
        'oRow.DefaultCellStyle.BackColor = Color.LightGray
        'e() 'lse
        'oRow.DefaultCellStyle.BackColor = Color.WhiteSmoke
        'End If
    End Sub


    Private Sub AddClon(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oSrc As DTOPriceListItem_Supplier = CurrentControlItem.Source
        Dim oClon = DTOPriceListItem_Supplier.Clon(oSrc)
        Dim oFrm As New Frm_PriceListItem_Supplier(oClon)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub


    Private Function CurrentItem() As DTOPriceListItem_Supplier
        Dim retval As DTOPriceListItem_Supplier = Nothing
        Dim oItems As List(Of DTOPriceListItem_Supplier) = DataGridView1.DataSource
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            Dim idx As Integer = oRow.Index
            retval = oItems(idx)
        End If
        Return retval
    End Function

    Private Sub DataGridView1_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        Select Case e.ColumnIndex
            Case Cols.Cost
                e.Value = Format(e.Value, _PriceList.Cur.FormatString)
        End Select
    End Sub

    Protected Class ControlItem
        Property Source As DTOPriceListItem_Supplier

        Property Ico As Image
        Property Ref As String
        Property EAN As String
        Property Description As String
        Property Cost As Decimal
        Property Eur As Decimal
        Property RRPP As Decimal
        Property InnerPack As Integer

        Public Sub New(oItem As DTOPriceListItem_Supplier, oPriceList As DTOPriceListSupplier)
            MyBase.New()
            _Source = oItem
            With oItem
                _Ico = IIf(.Sku Is Nothing, My.Resources.empty, My.Resources.vb)
                _Ref = .Ref
                _EAN = .EAN
                _Description = .Description
                _Cost = .Price
                _Eur = DTOAmt.Factory(oPriceList.Cur, _Cost).Eur
                _RRPP = .Retail
                _InnerPack = .InnerPack
            End With
        End Sub

        Public Sub Update(oItem As DTOPriceListItem_Supplier, oPriceList As DTOPriceListSupplier)
            _Source = oItem
            With oItem
                _Ico = IIf(.Sku Is Nothing, My.Resources.empty, My.Resources.vb)
                '_Ref = .Ref
                _EAN = .EAN
                _Description = .Description
                _Cost = .Price
                _Eur = DTOAmt.Factory(oPriceList.Cur, _Cost).Eur
                _RRPP = .Retail
                _InnerPack = .InnerPack
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class


End Class