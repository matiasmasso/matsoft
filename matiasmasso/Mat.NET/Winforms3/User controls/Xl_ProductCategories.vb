Public Class Xl_ProductCategories

    Private _Values As List(Of DTOProductCategory)
    Private _ControlItems As ControlItems
    Private _SelectionMode As DTOProduct.SelectionModes
    Private _IncludeNullValue As Boolean
    Private _DefaultValue As DTOProductCategory
    Private _SortOrder As DTOProductCategory.SortOrders
    Private _dragAllowed As Boolean
    Private _dragRow As Integer = -1
    Private _dragLabel As Label
    Private _PropertiesSet As Boolean
    Private _AllowEvents As Boolean

    Property ShowObsolets As Boolean
    Property AllowRemoveOnContextMenu As Boolean

    Public Event ValueChanged(sender As Object, e As MatEventArgs)
    Public Event RequestToRefresh(sender As Object, e As MatEventArgs)
    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)
    Public Event RequestToRemove(sender As Object, e As MatEventArgs)
    Public Event RequestToToggleObsoletos(sender As Object, e As MatEventArgs)
    Public Event OnItemSelected(sender As Object, e As MatEventArgs)


    Private Enum Cols
        Nom
    End Enum

    'Dim oSortOrder = Await FEB.UserDefaults.GetInt(exs, Current.Session.User, DTOUserDefault.Cods.ProductCategoriesOrder)
    Public Shadows Sub load(values As List(Of DTOProductCategory), Optional oSelectionMode As DTOProduct.SelectionModes = DTOProduct.SelectionModes.Browse, Optional IncludeNullValue As Boolean = False, Optional oDefaultValue As DTOProductCategory = Nothing, Optional oSortOrder As DTOProductCategory.SortOrders = DTOProductCategory.SortOrders.Alfabetic, Optional DisplayObsoletos As Boolean = False)
        _AllowEvents = False
        _Values = values
        _SelectionMode = oSelectionMode
        _IncludeNullValue = IncludeNullValue
        _SortOrder = oSortOrder
        _ShowObsolets = DisplayObsoletos

        If Not _PropertiesSet Then
            SetProperties()
            _PropertiesSet = True
        End If

        If _SortOrder = DTOProductCategory.SortOrders.Custom Then
            _Values = _Values.OrderBy(Function(x) x.Ord).OrderBy(Function(x) x.Obsoleto).ToList
        Else
            Dim oLang = Current.Session.Lang
            _Values = _Values.OrderBy(Function(x) x.nom.Tradueix(oLang)).OrderBy(Function(x) x.codi).OrderBy(Function(x) x.obsoleto).ToList
        End If

        _DefaultValue = oDefaultValue
        LoadControlItems()
        _AllowEvents = True
    End Sub


    Public ReadOnly Property Values As List(Of DTOProductCategory)
        Get
            Return _Values
        End Get
    End Property

    Public Sub Clear()
        _ControlItems = New ControlItems
    End Sub

    Public ReadOnly Property Value As DTOProductCategory
        Get
            Dim retval As DTOProductCategory = Nothing
            Dim oControlItem As ControlItem = CurrentControlItem()
            If oControlItem IsNot Nothing Then
                retval = oControlItem.Source
            End If
            Return retval
        End Get
    End Property

    Private Sub LoadControlItems()
        _AllowEvents = False

        Dim rowIdx As Integer
        Dim FirstDisplayedScrollingRowIndex As Integer = DataGridView1.FirstDisplayedScrollingRowIndex
        If DataGridView1.CurrentRow IsNot Nothing Then
            rowIdx = DataGridView1.CurrentRow.Index
        End If


        _ControlItems = New ControlItems
        If _IncludeNullValue Then
            Dim oNoItem As New ControlItem()
            _ControlItems.Add(oNoItem)
        End If

        For Each oItem As DTOProductCategory In _Values
            If _ShowObsolets Or oItem.Obsoleto = False Then
                Dim oControlItem As New ControlItem(oItem)
                _ControlItems.Add(oControlItem)
            End If
        Next

        DataGridView1.DataSource = _ControlItems


        If _DefaultValue Is Nothing Then
            If _ControlItems.Count > 0 Then
                If rowIdx < DataGridView1.Rows.Count Then
                    DataGridView1.CurrentCell = DataGridView1.Rows(rowIdx).Cells(Cols.Nom)
                End If
            End If
        Else
            Dim oControlItem As ControlItem = _ControlItems.ToList.Find(Function(x) x.Source.Equals(_DefaultValue))
            If oControlItem IsNot Nothing Then
                rowIdx = _ControlItems.IndexOf(oControlItem)
                DataGridView1.CurrentCell = DataGridView1.Rows(rowIdx).Cells(Cols.Nom)
            End If
        End If

        If FirstDisplayedScrollingRowIndex > 0 Then
            If FirstDisplayedScrollingRowIndex < DataGridView1.RowCount Then
                DataGridView1.FirstDisplayedScrollingRowIndex = FirstDisplayedScrollingRowIndex
            End If
        End If

        SetContextMenu()
        _AllowEvents = True
    End Sub

    Private Sub SetProperties()
        _AllowEvents = False

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

            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Nom)
                .HeaderText = Current.Session.Lang.Tradueix("Categorías", "Categories", "Categories")
                .DataPropertyName = "Nom"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With

        End With
    End Sub


    Private Function CurrentControlItem() As ControlItem
        Dim retval As ControlItem = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            retval = oRow.DataBoundItem
        End If
        Return retval
    End Function

    Private Function SelectedItems() As List(Of DTOProductCategory)
        Dim retval As New List(Of DTOProductCategory)
        For Each oRow As DataGridViewRow In DataGridView1.SelectedRows
            Dim oControlItem As ControlItem = oRow.DataBoundItem
            retval.Add(oControlItem.Source)
        Next

        If retval.Count = 0 Then retval.Add(CurrentControlItem.Source)
        Return retval
    End Function

    Private Sub SetContextMenu()
        Dim previousAllowEvents = _AllowEvents
        _AllowEvents = False
        Dim oContextMenu As New ContextMenuStrip
        Dim oControlItem As ControlItem = CurrentControlItem()

        If oControlItem IsNot Nothing Then
            Dim oCategory As DTOProductCategory = oControlItem.Source
            Dim oMenu_Category As New Menu_ProductCategory(oCategory)
            AddHandler oMenu_Category.AfterUpdate, AddressOf onUpdate
            oContextMenu.Items.AddRange(oMenu_Category.Range)
            oContextMenu.Items.Add("-")
        End If

        Dim oSortMenuItem As ToolStripMenuItem = oContextMenu.Items.Add("ordre")
        Dim oSortMenuItemAlfa As ToolStripMenuItem = oSortMenuItem.DropDownItems.Add("alfabetic", Nothing, AddressOf Do_SortOrderAlfabetic)
        Dim oSortMenuItemCustom As ToolStripMenuItem = oSortMenuItem.DropDownItems.Add("personalitzat", Nothing, AddressOf Do_SortOrderCustom)
        Dim oSortMenuItemEdit As ToolStripMenuItem = oSortMenuItem.DropDownItems.Add("modificar", Nothing, AddressOf Do_SortOrderEdit)
        oSortMenuItemEdit.CheckOnClick = True

        If _SortOrder = DTOProductCategory.SortOrders.Alfabetic Then
            oSortMenuItemAlfa.Checked = True
            oSortMenuItemAlfa.Enabled = False
            oSortMenuItemCustom.Checked = False
            oSortMenuItemCustom.Enabled = True
            oSortMenuItemEdit.Enabled = False
        Else
            oSortMenuItemAlfa.Checked = False
            oSortMenuItemAlfa.Enabled = True
            oSortMenuItemCustom.Checked = True
            oSortMenuItemCustom.Enabled = False
        End If

        Select Case Current.Session.Rol.id
            Case DTORol.Ids.superUser, DTORol.Ids.admin, DTORol.Ids.marketing
                oSortMenuItemEdit.Enabled = True
            Case Else
                oSortMenuItemEdit.Enabled = False
        End Select


        Dim oMenuItemObsolets As New ToolStripMenuItem("inclou obsolets")
        oMenuItemObsolets.CheckOnClick = True
        AddHandler oMenuItemObsolets.CheckedChanged, AddressOf onMenuItemObsoletsCheckedChanged
        oContextMenu.Items.Add(oMenuItemObsolets)

        _AllowEvents = False
        oMenuItemObsolets.Checked = _ShowObsolets
        _AllowEvents = PreviousAllowEvents

        oContextMenu.Items.Add("afegir", Nothing, AddressOf Do_AddNew)

        If AllowRemoveOnContextMenu Then
            oContextMenu.Items.Add("retirar", Nothing, AddressOf Do_Remove)
        End If

        DataGridView1.ContextMenuStrip = oContextMenu
        _AllowEvents = previousAllowEvents
    End Sub

    Private Sub onUpdate()
        RaiseEvent RequestToRefresh(Me, MatEventArgs.Empty)
    End Sub

    Private Sub onMenuItemObsoletsCheckedChanged(sender As Object, e As EventArgs)
        If _AllowEvents Then
            RaiseEvent RequestToToggleObsoletos(Me, MatEventArgs.Empty)
        End If
    End Sub

    Private Async Sub Do_SortOrderAlfabetic()
        Dim exs As New List(Of Exception)
        If Await FEB.UserDefaults.SetValue(exs, Current.Session.User, DTOUserDefault.Cods.ProductCategoriesOrder, CInt(DTOProductCategory.SortOrders.Alfabetic)) Then
            _SortOrder = DTOProductCategory.SortOrders.Alfabetic
            RaiseEvent RequestToRefresh(Me, MatEventArgs.Empty)
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub Do_SortOrderCustom()
        Dim exs As New List(Of Exception)
        If Await FEB.UserDefaults.SetValue(exs, Current.Session.User, DTOUserDefault.Cods.ProductCategoriesOrder, CInt(DTOProductCategory.SortOrders.Custom)) Then
            _SortOrder = DTOProductCategory.SortOrders.Custom
            RaiseEvent RequestToRefresh(Me, MatEventArgs.Empty)
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Do_SortOrderEdit(sender As Object, e As EventArgs)
        Dim oMenuItem As ToolStripMenuItem = sender
        _dragAllowed = oMenuItem.Checked
        With DataGridView1
            .AllowDrop = _dragAllowed
            .MultiSelect = Not _dragAllowed
        End With
    End Sub


    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles DataGridView1.DoubleClick
        Dim oSelectedValue As DTOProductCategory = CurrentControlItem.Source
        Select Case _SelectionMode
            Case DTOProduct.SelectionModes.SelectAny, DTOProduct.SelectionModes.SelectCategory
                RaiseEvent OnItemSelected(Me, New MatEventArgs(Me.Value))
            Case Else
                Dim oFrm As New Frm_Stp(oSelectedValue, Defaults.SelectionModes.browse)
                AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                oFrm.Show()
        End Select
    End Sub

    Private Sub DataGridView1_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles DataGridView1.RowPrePaint
        Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
        Dim oControlItem As ControlItem = oRow.DataBoundItem
        Dim item As DTOProductCategory = oControlItem.Source
        If item.Obsoleto Then
            oRow.DefaultCellStyle.BackColor = Color.LightGray
        Else
            oRow.DefaultCellStyle.BackColor = Color.White
        End If
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView1.SelectionChanged
        If _AllowEvents Then
            Dim oControlItem As ControlItem = CurrentControlItem()
            If oControlItem IsNot Nothing Then
                RaiseEvent ValueChanged(Me, New MatEventArgs(oControlItem.Source))
            End If
            SetContextMenu()
        End If
    End Sub

    Private Sub Do_AddNew()
        RaiseEvent RequestToAddNew(Me, MatEventArgs.Empty)
    End Sub

    Private Sub Do_Remove()
        Dim oControlItem As ControlItem = CurrentControlItem()
        Dim oCategory As DTOProductCategory = oControlItem.Source
        RaiseEvent RequestToRemove(Me, New MatEventArgs(oCategory))
    End Sub

    Private Sub RefreshRequest()
        RaiseEvent RequestToRefresh(Me, MatEventArgs.Empty)
    End Sub

    Protected Class ControlItem
        Public Property Source As DTOProductCategory

        Public Property Nom As String

        Public Sub New(oCategory As DTOProductCategory)
            MyBase.New()
            _Source = oCategory
            With oCategory
                _Nom = .nom.Tradueix(Current.Session.Lang)
            End With
        End Sub

        Public Sub New()
            MyBase.New()
            _Nom = Current.Session.Lang.Tradueix("(todas las categorías)", "(totes les categories)", "(all categories)")
        End Sub
    End Class

#Region "DragToSort"

    Private Sub dataGridView1_CellMouseDown(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView1.CellMouseDown
        If e.ColumnIndex = 0 AndAlso e.RowIndex >= 0 AndAlso _dragAllowed Then
            _dragRow = e.RowIndex

            If _dragLabel Is Nothing Then _dragLabel = New Label()
            With _dragLabel
                .Text = DataGridView1(e.ColumnIndex, e.RowIndex).Value.ToString()
                .Parent = DataGridView1
                .Location = e.Location
            End With
        End If
    End Sub

    Private Sub dataGridView1_MouseMove(sender As Object, e As MouseEventArgs) Handles DataGridView1.MouseMove
        If e.Button = MouseButtons.Left AndAlso _dragLabel IsNot Nothing Then
            _dragLabel.Location = e.Location
            DataGridView1.ClearSelection()
        End If
    End Sub

    Private Async Sub dataGridView1_MouseUp(sender As Object, e As MouseEventArgs) Handles DataGridView1.MouseUp
        If _dragAllowed Then
            Dim oGrid As DataGridView = sender
            Dim hit = oGrid.HitTest(e.X, e.Y)
            Dim dropRow As Integer = -1
            If hit.Type <> DataGridViewHitTestType.None Then
                dropRow = hit.RowIndex
                If _dragRow >= 0 Then
                    Dim targetRow As Integer = dropRow + (If(_dragRow > dropRow, 1, 0))
                    If targetRow <> _dragRow Then
                        Dim item As ControlItem = _ControlItems(_dragRow)
                        _ControlItems.Remove(item)
                        _ControlItems.Insert(targetRow, item)

                        oGrid.ClearSelection()
                        oGrid.CurrentCell = oGrid.Rows(targetRow).Cells(Cols.Nom)

                        Dim idx As Integer
                        Dim itemsToSort As New List(Of DTOProductCategory)
                        For Each oControlItem As ControlItem In _ControlItems
                            Dim oCategory As DTOProductCategory = oControlItem.Source
                            If oCategory.Ord <> idx Then
                                oCategory.Ord = idx
                                itemsToSort.Add(oCategory)
                            End If
                            idx += 1
                        Next

                        Cursor = Cursors.WaitCursor
                        Application.DoEvents()
                        Dim exs As New List(Of Exception)
                        If Not Await FEB.ProductCategories.Move(exs, itemsToSort) Then
                            UIHelper.WarnError(exs, "error al moure categories")
                        End If
                        RaiseEvent RequestToRefresh(Me, MatEventArgs.Empty)
                        Cursor = Cursors.Default
                        Application.DoEvents()
                    End If
                End If
            Else
                If _ControlItems.Count > 0 Then
                    oGrid.CurrentCell = oGrid.Rows(_dragRow).Cells(Cols.Nom)
                    'oGrid.Rows(_dragRow).Cells(Cols.Nom).Selected = True
                End If
            End If

            If _dragLabel IsNot Nothing Then
                _dragLabel.Dispose()
                _dragLabel = Nothing
            End If
        End If
    End Sub


#End Region

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class
End Class
