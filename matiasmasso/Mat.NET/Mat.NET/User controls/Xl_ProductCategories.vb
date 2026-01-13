Public Class Xl_ProductCategories

    Private _Values As List(Of DTOProductCategory)
    Private _ControlItems As ControlItems
    Private _SelectionMode As BLL.Defaults.SelectionModes = BLL.Defaults.SelectionModes.Browse
    Private _IncludeNullValue As Boolean
    Private _MenuItem_Obsolets As ToolStripMenuItem
    Private _AllowEvents As Boolean

    Public Event ValueChanged(sender As Object, e As MatEventArgs)
    Public Event RequestToRefresh(sender As Object, e As MatEventArgs)
    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)
    Public Event OnItemSelected(sender As Object, e As MatEventArgs)

    Private Enum Cols
        Nom
    End Enum

    Public Shadows Sub Load(values As List(Of DTOProductCategory), Optional oSelectionMode As BLL.Defaults.SelectionModes = BLL.Defaults.SelectionModes.Browse, Optional IncludeNullValue As Boolean = False)
        _Values = values
        _SelectionMode = oSelectionMode
        _IncludeNullValue = IncludeNullValue
        _MenuItem_Obsolets = MenuItem_Obsolets()
        LoadControlItems()
    End Sub

    Public Sub Clear()
        _ControlItems = New ControlItems
        LoadGrid()
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
        _ControlItems = New ControlItems
        If _IncludeNullValue Then
            Dim oNoItem As New ControlItem()
            _ControlItems.Add(oNoItem)
        End If

        For Each oItem As DTOProductCategory In _Values
            If _MenuItem_Obsolets.Checked Or oItem.Obsoleto = False Then
                Dim oControlItem As New ControlItem(oItem)
                _ControlItems.Add(oControlItem)
            End If
        Next

        LoadGrid()
    End Sub

    Private Sub LoadGrid()
        _AllowEvents = False
        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
            End With

            .AutoGenerateColumns = False
            .Columns.Clear()

            .DataSource = _ControlItems
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = True
            .AllowUserToResizeRows = False
            .AllowDrop = False
            .ReadOnly = True

            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Nom)
                .HeaderText = BLL.BLLSession.Current.Lang.Tradueix("Categorías", "Categories", "Categories")
                .DataPropertyName = "Nom"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
        End With
        SetContextMenu()
        _AllowEvents = True
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
        Dim oContextMenu As New ContextMenuStrip
        Dim oControlItem As ControlItem = CurrentControlItem()

        If oControlItem IsNot Nothing Then
            Dim oCategory As DTOProductCategory = oControlItem.Source
            Dim oMenu_Category As New Menu_ProductCategory(oCategory)
            AddHandler oMenu_Category.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_Category.Range)
        End If
        oContextMenu.Items.Add(_MenuItem_Obsolets)
        oContextMenu.Items.Add("afegir", Nothing, AddressOf Do_AddNew)

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Function MenuItem_Obsolets() As ToolStripMenuItem
        Dim retval As New ToolStripMenuItem
        With retval
            .Text = "Inclou obsolets"
            .CheckOnClick = True
        End With
        AddHandler retval.Click, AddressOf LoadControlItems
        Return retval
    End Function


    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles DataGridView1.DoubleClick
        Dim oSelectedValue As DTOProductCategory = CurrentControlItem.Source
        Select Case _SelectionMode
            Case BLL.Defaults.SelectionModes.Browse
                Dim oFrm As New Frm_ProductCategory(oSelectedValue)
                AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                oFrm.Show()
            Case BLL.Defaults.SelectionModes.Selection
                RaiseEvent OnItemSelected(Me, New MatEventArgs(Me.Value))
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
                _Nom = .Nom
            End With
        End Sub

        Public Sub New()
            MyBase.New()
            _Nom = BLL.BLLSession.Current.Lang.Tradueix("(todas las categorías)", "(totes les categories)", "(all categories)")
        End Sub
    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class
End Class
