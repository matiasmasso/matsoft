Public Class Xl_ProductCategoriesCheckedList

    Inherits _Xl_ReadOnlyDatagridview

    Private _allValues As List(Of DTOProductCategory)
    Private _selectedValues As List(Of DTOProductCategory)

    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Private Enum Cols
        Chk
        Nom
    End Enum

    Public Function CheckedValues() As IEnumerable(Of DTOProductCategory)
        Dim oCheckedControlItems = _ControlItems.Where(Function(x) x.LinCod = ControlItem.LinCods.Category And x.Checked)
        Dim retval As IEnumerable(Of DTOProductCategory) = oCheckedControlItems.Select(Function(y) DirectCast(y.Source, DTOProductCategory))
        Return retval
    End Function

    Public Shadows Sub Load(allValues As List(Of DTOProductCategory), selectedValues As List(Of DTOProductCategory))
        _allValues = allValues
        _selectedValues = selectedValues


        Static PropertiesSet As Boolean
        If Not PropertiesSet Then
            SetProperties()
            PropertiesSet = True
        End If

        Refresca()
    End Sub

    Private Sub Refresca()
        _AllowEvents = False
        _ControlItems = New ControlItems
        Dim oBrand As New DTOProductBrand
        If _allValues IsNot Nothing Then
            For Each Value In _allValues
                If Value.Brand.UnEquals(oBrand) Then
                    oBrand = Value.Brand
                    Dim oBrandControlitem As New ControlItem(oBrand)
                    oBrandControlitem.Checked = _selectedValues.Any(Function(y) y.Brand.Equals(oBrand))
                    _ControlItems.Add(oBrandControlitem)
                End If
                Dim oControlitem As New ControlItem(Value)
                oControlitem.Checked = _selectedValues.Any(Function(x) x.Equals(oControlitem.Source))
                _ControlItems.Add(oControlitem)
            Next
        End If

        MyBase.DataSource = _ControlItems
        MyBase.ClearSelection()
        _AllowEvents = True
    End Sub

    Private Sub SetProperties()
        MyBase.RowTemplate.Height = MyBase.Font.Height * 1.35
        'MyBase.RowRol.DefaultCellStyle.BackColor = Color.Transparent

        MyBase.AutoGenerateColumns = False
        MyBase.Columns.Clear()

        MyBase.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        MyBase.ColumnHeadersVisible = True
        MyBase.RowHeadersVisible = False
        MyBase.MultiSelect = True
        MyBase.AllowUserToResizeRows = False
        MyBase.AllowDrop = False
        MyBase.ReadOnly = False

        MyBase.Columns.Add(New DataGridViewCheckBoxColumn(False))
        With DirectCast(MyBase.Columns(Cols.Chk), DataGridViewCheckBoxColumn)
            .HeaderText = ""
            .DataPropertyName = "Checked"
            .Width = 20
            .DefaultCellStyle.SelectionBackColor = Color.White
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Nom)
            .HeaderText = "Nom"
            .DataPropertyName = "Nom"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End With
    End Sub

    Private Function SelectedItems() As List(Of DTOProductCategory)
        Dim retval As New List(Of DTOProductCategory)
        For Each oRow As DataGridViewRow In MyBase.SelectedRows
            Dim oControlItem As ControlItem = oRow.DataBoundItem
            retval.Add(oControlItem.Source)
        Next

        If retval.Count = 0 Then retval.Add(CurrentControlItem.Source)
        Return retval
    End Function

    Private Function CurrentControlItem() As ControlItem
        Dim retval As ControlItem = Nothing
        Dim oRow As DataGridViewRow = MyBase.CurrentRow
        If oRow IsNot Nothing Then
            retval = oRow.DataBoundItem
        End If
        Return retval
    End Function

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oControlItem As ControlItem = CurrentControlItem()

        If oControlItem IsNot Nothing Then
            Select Case oControlItem.LinCod
                Case ControlItem.LinCods.Brand
                    'Dim oMenu_ProductBrand As New Menu_ProductBrand(SelectedItems)
                    'AddHandler oMenu_ProductBrand.AfterUpdate, AddressOf RefreshRequest
                    'oContextMenu.Items.AddRange(oMenu_ProductBrand.Range)
                Case ControlItem.LinCods.Category
                    Dim oMenu_ProductCategory As New Menu_ProductCategory(SelectedItems)
                    AddHandler oMenu_ProductCategory.AfterUpdate, AddressOf RefreshRequest
                    oContextMenu.Items.AddRange(oMenu_ProductCategory.Range)
            End Select
            oContextMenu.Items.Add("-")
        End If

        oContextMenu.Items.Add("activa-ho tot", Nothing, AddressOf Do_CheckAll)
        oContextMenu.Items.Add("activa nomes la selecció", Nothing, AddressOf Do_CheckSelection)
        oContextMenu.Items.Add("desactiva-ho tot", Nothing, AddressOf Do_CheckNone)
        MyBase.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Do_CheckAll()
        For Each controlitem In _ControlItems
            controlitem.Checked = True
        Next
        MyBase.Refresh()
    End Sub

    Private Sub Do_CheckSelection()
        For Each controlitem In _ControlItems
            controlitem.Checked = False
        Next
        For Each oRow As DataGridViewRow In MyBase.SelectedRows
            Dim controlItem As ControlItem = oRow.DataBoundItem
            controlItem.Checked = True
        Next
        MyBase.Refresh()
    End Sub

    Private Sub Do_CheckNone()
        For Each controlitem In _ControlItems
            controlitem.Checked = False
        Next
        MyBase.Refresh()
    End Sub

    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles MyBase.DoubleClick
        Dim oCurrentControlItem As ControlItem = CurrentControlItem()
        If oCurrentControlItem IsNot Nothing Then
            Dim oSelectedValue As DTOProductCategory = CurrentControlItem.Source
            Dim oFrm As New Frm_Stp(oSelectedValue, Defaults.SelectionModes.browse)
            'Dim oFrm As New Frm_ProductCategory(oSelectedValue)
            AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
            oFrm.Show()
        End If
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles MyBase.SelectionChanged
        If _AllowEvents Then
            SetContextMenu()
        End If
    End Sub

    Private Sub DataGridView1_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles MyBase.CellValueChanged
        If _AllowEvents Then
            Select Case e.ColumnIndex
                Case Cols.Chk
                    Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
                    Dim oControlitem As ControlItem = oRow.DataBoundItem
                    Select Case oControlitem.LinCod
                        Case ControlItem.LinCods.Brand
                            Dim oBrand As DTOProductBrand = oControlitem.Source
                            For Each categoryControl In _ControlItems.Where(Function(x) x.LinCod = ControlItem.LinCods.Category).Where(Function(y) DirectCast(y.Source, DTOProductCategory).Brand.Guid.Equals(oBrand.Guid))
                                categoryControl.Checked = oControlitem.Checked
                            Next
                            MyBase.Refresh()
                    End Select
                    RaiseEvent AfterUpdate(sender, New MatEventArgs(CurrentControlItem.Source))
            End Select
        End If
    End Sub

    Private Sub DataGridView1_CurrentCellDirtyStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.CurrentCellDirtyStateChanged
        'provoca CellValueChanged a cada clic sense sortir de la casella
        Select Case MyBase.CurrentCell.ColumnIndex
            Case Cols.Chk
                MyBase.CommitEdit(DataGridViewDataErrorContexts.Commit)
        End Select
    End Sub

    Private Sub Xl_ProductCategoriesCheckedList_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles Me.RowPrePaint
        Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
        Dim oControl As ControlItem = oRow.DataBoundItem
        If oControl.LinCod = ControlItem.LinCods.Brand Then
            oRow.DefaultCellStyle.BackColor = Color.LightBlue
        End If
    End Sub

    Protected Class ControlItem
        Property Source As DTOProduct

        Property LinCod As LinCods
        Property Checked As Boolean
        Property Nom As String

        Public Enum LinCods
            Category
            Brand
        End Enum

        Public Sub New(value As DTOProduct)
            MyBase.New()
            _Source = value

            If TypeOf value Is DTOProductBrand Then
                _LinCod = LinCods.Brand
            Else
                _LinCod = LinCods.Category
            End If

            With value
                _Nom = .nom.Tradueix(Current.Session.Lang)
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class


