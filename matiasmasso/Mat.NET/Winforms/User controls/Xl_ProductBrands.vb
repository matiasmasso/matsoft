Public Class Xl_ProductBrands

    Private _Values As List(Of DTOProductBrand)
    Private _ControlItems As ControlItems
    Private _SelectionMode As DTOProduct.SelectionModes
    Private _IncludeNullValue As Boolean
    Private _DefaultValue As DTOProductBrand
    Private _AllowEvents As Boolean

    Property ShowObsolets As Boolean

    Public Event ValueChanged(sender As Object, e As MatEventArgs)
    Public Event RequestToRefresh(sender As Object, e As MatEventArgs)
    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)
    Public Event RequestToToggleObsoletos(sender As Object, e As MatEventArgs)
    Public Event OnItemSelected(sender As Object, e As MatEventArgs)

    Private Enum Cols
        Nom
    End Enum

    Public Shadows Sub Load(values As List(Of DTOProductBrand), Optional oSelectionMode As DTOProduct.SelectionModes = DTOProduct.SelectionModes.Browse, Optional IncludeNullValue As Boolean = False, Optional oDefaultValue As DTOProductBrand = Nothing, Optional DisplayObsoletos As Boolean = False)
        _Values = values
        _SelectionMode = oSelectionMode
        _IncludeNullValue = IncludeNullValue
        _DefaultValue = oDefaultValue
        _ShowObsolets = DisplayObsoletos

        Static PropertiesSet As Boolean
        If Not PropertiesSet Then
            SetProperties()
            PropertiesSet = True
        End If

        _DefaultValue = oDefaultValue
        LoadControlItems()
    End Sub

    Public Sub Clear()
        _ControlItems = New ControlItems
    End Sub

    Public ReadOnly Property Value As DTOProductBrand
        Get
            Dim retval As DTOProductBrand = Nothing
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

        For Each oItem As DTOProductBrand In _Values
            If _ShowObsolets Or oItem.Obsoleto = False Then
                Dim oControlItem As New ControlItem(oItem)
                _ControlItems.Add(oControlItem)
            End If
        Next

        DataGridView1.DataSource = _ControlItems


        If _DefaultValue Is Nothing Then
            If _ControlItems.Count > 0 Then
                If DataGridView1.Rows.Count > rowIdx Then
                    DataGridView1.CurrentCell = DataGridView1.Rows(rowIdx).Cells(Cols.Nom)
                End If
            End If
        Else
            Dim oControlItem As ControlItem = _ControlItems.ToList.Find(Function(x) x.Source.Equals(_DefaultValue))
            If _ControlItems.IndexOf(oControlItem) >= 0 Then
                rowIdx = _ControlItems.IndexOf(oControlItem)
            End If
            If DataGridView1.Rows.Count > rowIdx Then
                DataGridView1.CurrentCell = DataGridView1.Rows(rowIdx).Cells(Cols.Nom)
            End If
        End If

        If FirstDisplayedScrollingRowIndex > 0 Then
            DataGridView1.FirstDisplayedScrollingRowIndex = FirstDisplayedScrollingRowIndex
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
                .HeaderText = Current.Session.Lang.Tradueix("Marcas", "Marques", "Brands")
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

    Private Sub SetContextMenu()
        Dim previousAllowEvents = _AllowEvents
        _AllowEvents = False
        Dim oContextMenu As New ContextMenuStrip
        Dim oControlItem As ControlItem = CurrentControlItem()

        If oControlItem IsNot Nothing Then
            Dim item As DTOProductBrand = oControlItem.Source
            Dim oMenu_Brand As New Menu_ProductBrand(item)
            AddHandler oMenu_Brand.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_Brand.Range)
            oContextMenu.Items.Add("-")
        End If

        Dim oMenuItemObsolets As New ToolStripMenuItem("inclou obsolets")
        oMenuItemObsolets.CheckOnClick = True
        AddHandler oMenuItemObsolets.CheckedChanged, AddressOf onMenuItemObsoletsCheckedChanged
        oContextMenu.Items.Add(oMenuItemObsolets)

        oContextMenu.Items.Add("afegir", Nothing, AddressOf Do_AddNew)

        _AllowEvents = False
        oMenuItemObsolets.Checked = _ShowObsolets
        _AllowEvents = previousAllowEvents

        DataGridView1.ContextMenuStrip = oContextMenu
        _AllowEvents = previousAllowEvents
    End Sub

    Private Sub onMenuItemObsoletsCheckedChanged(sender As Object, e As EventArgs)
        If _AllowEvents Then
            RaiseEvent RequestToToggleObsoletos(Me, MatEventArgs.Empty)
        End If
    End Sub


    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles DataGridView1.DoubleClick
        Dim oSelectedValue As DTOProductBrand = CurrentControlItem.Source
        Select Case _SelectionMode
            Case DTOProduct.SelectionModes.SelectAny, DTOProduct.SelectionModes.SelectBrand
                RaiseEvent OnItemSelected(Me, New MatEventArgs(Me.Value))
            Case Else
                Dim oFrm As New Frm_ProductBrand(oSelectedValue)
                AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                oFrm.Show()
        End Select
    End Sub

    Private Sub DataGridView1_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles DataGridView1.RowPrePaint
        Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
        Dim oControlItem As ControlItem = oRow.DataBoundItem
        Dim item As DTOProductBrand = oControlItem.Source
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

    Private Sub RefreshRequest()
        RaiseEvent RequestToRefresh(Me, MatEventArgs.Empty)
    End Sub

    Protected Class ControlItem
        Public Property Source As DTOProductBrand

        Public Property Nom As String

        Public Sub New(oBrand As DTOProductBrand)
            MyBase.New()
            _Source = oBrand
            _Nom = oBrand.nom.Tradueix(Current.Session.Lang)
        End Sub

        Public Sub New()
            MyBase.New()
            _Nom = Current.Session.Lang.Tradueix("(todas las marcas)", "(totes les marques)", "(all brands)")
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class




