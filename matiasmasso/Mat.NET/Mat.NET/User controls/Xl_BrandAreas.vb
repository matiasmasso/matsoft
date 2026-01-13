Public Class Xl_BrandAreas
    Private _values As List(Of DTOBrandArea)
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event RequestToRefresh(sender As Object, e As MatEventArgs)
    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)

    Private Enum Cols
        Area
        FchFrom
        FchTo
    End Enum

    Public Shadows Sub Load(values As List(Of DTOBrandArea))
        Static PropertiesSet As Boolean
        If Not PropertiesSet Then
            SetProperties()
            PropertiesSet = True
        End If

        _Values = values
        refresca()
    End Sub

    Sub refresca()
        Dim RowIndex, ColIndex As Integer
        If DataGridView1.CurrentCell IsNot Nothing Then
            RowIndex = DataGridView1.CurrentCell.RowIndex
            ColIndex = DataGridView1.CurrentCell.ColumnIndex
        End If

        _ControlItems = New ControlItems
        For Each oItem As DTOBrandArea In _values
            Dim oControlItem As New ControlItem(oItem)
            _ControlItems.Add(oControlItem)
        Next

        If _ControlItems.Count > 0 Then
            DataGridView1.DataSource = _ControlItems
            If DataGridView1.Rows.Count > RowIndex Then
                DataGridView1.CurrentCell = DataGridView1.Rows(RowIndex).Cells(ColIndex)
            ElseIf DataGridView1.Rows.Count > 0 Then
                DataGridView1.CurrentCell = DataGridView1.Rows(DataGridView1.Rows.Count - 1).Cells(ColIndex)
            End If
        End If

        SetContextMenu()
        _AllowEvents = True
    End Sub

    Public ReadOnly Property Value As DTOBrandArea
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As DTOBrandArea = oControlItem.Source
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

            .DataSource = _ControlItems
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = True
            .AllowUserToResizeRows = False
            .AllowDrop = False
            .ReadOnly = True


            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Area)
                .HeaderText = "Area"
                .DataPropertyName = "Area"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.FchFrom)
                .HeaderText = "Des de"
                .DataPropertyName = "FchFrom"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                .DefaultCellStyle.Format = "dd/MM/yy"
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.FchTo)
                .HeaderText = "Fins"
                .DataPropertyName = "FchTo"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                .DefaultCellStyle.Format = "dd/MM/yy"
                .DefaultCellStyle.NullValue = ""
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

    Private Function SelectedItems() As List(Of DTOBrandArea)
        Dim retval As New List(Of DTOBrandArea)
        For Each oRow As DataGridViewRow In DataGridView1.SelectedRows
            Dim oControlItem As ControlItem = oRow.DataBoundItem
            retval.Add(oControlItem.Source)
        Next

        If retval.Count = 0 Then retval.Add(CurrentControlItem.Source)
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
        Dim oControlItem As ControlItem = CurrentControlItem()

        If oControlItem IsNot Nothing Then
            Dim oMenu_BrandArea As New Menu_BrandArea(SelectedItems.First)
            AddHandler oMenu_BrandArea.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_BrandArea.Range)
            oContextMenu.Items.Add("-")
        End If
        oContextMenu.Items.Add("afegir", Nothing, AddressOf Do_AddNew)

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Do_AddNew()
        RaiseEvent RequestToAddNew(Me, MatEventArgs.Empty)
    End Sub

    Private Sub DataGridView1_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        Select Case e.ColumnIndex
            Case Cols.FchTo
                Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                Dim oControlItem As ControlItem = oRow.DataBoundItem
                Dim DtFch As Date = oControlItem.FchTo
                If DtFch = Nothing Then
                    e.Value = Nothing
                End If
        End Select
    End Sub

    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles DataGridView1.DoubleClick
        Dim oControlItem As ControlItem = CurrentControlItem()
        Dim oBrandArea As DTOBrandArea = oControlItem.Source
        Dim oFrm As New Frm_BrandArea(oBrandArea)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub


    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView1.SelectionChanged
        If _AllowEvents Then
            RaiseEvent ValueChanged(Me, New MatEventArgs(CurrentControlItem.Source))
            SetContextMenu()
        End If
    End Sub

    Private Sub RefreshRequest()
        RaiseEvent RequestToRefresh(Me, MatEventArgs.Empty)
    End Sub

    Protected Class ControlItem
        Property Source As DTOBrandArea

        Property Area As String
        Property FchFrom As Date
        Property FchTo As Nullable(Of Date)

        Public Sub New(item As DTOBrandArea)
            MyBase.New()
            _Source = item
            With item
                _Area = BLL.BLLArea.FullNomSegmentedReversed(.Area, BLL.BLLSession.Current.User.Lang)
                _FchFrom = .FchFrom
                _FchTo = .FchTo
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class