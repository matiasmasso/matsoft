Public Class Xl_SelloutFilters
    Inherits _Xl_ReadOnlyDatagridview

    Private _SellOut As DTOSellOut

    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)
    Public Event RequestToFilter(sender As Object, e As MatEventArgs)

    Private Enum Cols
        Chk
        Nom
    End Enum

    Public Function Values() As List(Of DTOSellOut.Filter)
        Dim retval As List(Of DTOSellOut.Filter) = DTOSellOut.AllFilters(_SellOut.Lang)

        Dim oCheckedControlItems = _ControlItems.ToList.Where(Function(x) x.Checked AndAlso x.LinCod = ControlItem.LinCods.Value)

        For Each oControlItem In oCheckedControlItems
            Dim oFilter = retval.First(Function(x) x.Cod = oControlItem.Source.Cod)
            oFilter.Values.Add(oControlItem.Value)
        Next
        Return retval
    End Function

    Public Shadows Sub Load(oSellout As DTOSellOut)
        _SellOut = oSellout

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
        For Each oFilter In _SellOut.Filters
            Dim oControlitem As New ControlItem(oFilter)
            oControlitem.Checked = oFilter.Values.Count > 0
            _ControlItems.Add(oControlitem)
            For Each value In oFilter.Values
                oControlitem = New ControlItem(oFilter, value)
                oControlitem.Checked = True
                _ControlItems.Add(oControlitem)
            Next
        Next

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
            .HeaderText = "Filtres"
            .DataPropertyName = "Nom"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            .ReadOnly = True
        End With
    End Sub

    Private Function SelectedItems() As List(Of DTOSellOut.Filter)
        Dim retval As New List(Of DTOSellOut.Filter)
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
            'Dim oMenu_SelloutFilter As New Menu_SelloutFilter(SelectedItems)
            'AddHandler oMenu_SelloutFilter.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.Add("afegir filtre", Nothing, AddressOf Do_AddFilter)
            If oControlItem.LinCod = ControlItem.LinCods.Value Then
                oContextMenu.Items.Add("retirar filtre", Nothing, AddressOf Do_RemoveFilter)
            End If
        End If

        MyBase.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Do_AddFilter()
        Dim oCurrentControlItem As ControlItem = CurrentControlItem()
        If oCurrentControlItem IsNot Nothing Then
            Dim oSelectedValue As DTOSellOut.Filter = CurrentControlItem.Source
            RaiseEvent RequestToFilter(Me, New MatEventArgs(oSelectedValue))
        End If
    End Sub

    Private Sub Do_RemoveFilter()
        Dim oCurrentControlItem As ControlItem = CurrentControlItem()
        _ControlItems.Remove(oCurrentControlItem)
        'RaiseEvent AfterUpdate(Me, New MatEventArgs(Me.Values))
    End Sub

    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles MyBase.DoubleClick
        Dim oCurrentControlItem As ControlItem = CurrentControlItem()
        If oCurrentControlItem IsNot Nothing Then
            Dim oSelectedValue As DTOSellOut.Filter = CurrentControlItem.Source
            RaiseEvent RequestToFilter(Me, New MatEventArgs(oSelectedValue))
        End If
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles MyBase.SelectionChanged
        If _AllowEvents Then
            SetContextMenu()
            MyBase.ClearSelection()
        End If
    End Sub

    Private Sub DataGridView1_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles MyBase.CellValueChanged
        If _AllowEvents Then
            Select Case e.ColumnIndex
                Case Cols.Chk
                    Application.DoEvents()

                    Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
                    Dim oControlitem As ControlItem = oRow.DataBoundItem
                    If oControlitem.Checked Then
                        RaiseEvent RequestToFilter(Me, New MatEventArgs(oControlitem.Source))
                    Else
                        Select Case oControlitem.LinCod
                            Case ControlItem.LinCods.Filter
                                Dim oCod = oControlitem.Source.Cod
                                For i As Integer = _ControlItems.Count - 1 To 0 Step -1
                                    If _ControlItems(i).Source.Cod = oCod Then
                                        _ControlItems.RemoveAt(i)
                                    End If
                                Next
                                _ControlItems.ToList.RemoveAll(Function(x) x.Source.Cod = oCod)
                            Case ControlItem.LinCods.Value
                                _ControlItems.Remove(oControlitem)
                        End Select
                    End If

                    If IsDirty() Then
                        Application.DoEvents()
                        RaiseEvent AfterUpdate(sender, New MatEventArgs(Me.Values))
                    End If
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


    Private Sub Xl_SelloutFilters_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles Me.RowPrePaint
        Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
        Dim oControlItem As ControlItem = oRow.DataBoundItem

        Select Case oControlItem.LinCod
            Case ControlItem.LinCods.Filter
                oRow.DefaultCellStyle.BackColor = Color.LightGray
        End Select
    End Sub

    Private Function IsDirty() As Boolean
        Dim originalSet = New HashSet(Of DTOSellOut.Filter)(_SellOut.Filters)
        Dim currentSet = New HashSet(Of DTOSellOut.Filter)(Me.Values)
        Dim retval As Boolean = Not originalSet.SetEquals(currentSet)
        Return retval
    End Function

    Protected Class ControlItem
        Property Source As DTOSellOut.Filter
        Property Value As DTOBaseGuid
        Property Checked As Boolean
        Property Nom As String
        Property LinCod As LinCods

        Public Enum LinCods
            Filter
            Value
        End Enum

        Public Sub New(value As DTOSellOut.Filter)
            MyBase.New()
            _Source = value
            With value
                _Nom = .Caption(DTOLang.ESP)
            End With
        End Sub

        Public Sub New(oFilter As DTOSellOut.Filter, value As DTOGuidNom)
            MyBase.New()
            _Source = oFilter
            _Value = value
            _Nom = value.Nom
            _LinCod = LinCods.Value
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class

