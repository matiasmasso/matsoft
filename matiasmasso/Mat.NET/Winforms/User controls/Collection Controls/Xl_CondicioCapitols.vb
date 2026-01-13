Public Class Xl_CondicioCapitols

    Inherits _Xl_ReadOnlyDatagridview

    Private _Values As DTOCondicio.Capitol.Collection
    Private _Lang As DTOLang
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)

    Private Enum Cols
        Ord
        Title
        Fch
    End Enum

    Public Shadows Sub Load(values As DTOCondicio.Capitol.Collection, oLang As DTOLang)
        Static PropertiesSet As Boolean
        If Not PropertiesSet Then
            SetProperties()
            PropertiesSet = True
        End If

        _Values = values
        _Lang = oLang
        Refresca()
    End Sub

    Public WriteOnly Property Lang As DTOLang
        Set(value As DTOLang)
            _Lang = value
            Refresca()
        End Set
    End Property

    Public ReadOnly Property Values As DTOCondicio.Capitol.Collection
        Get
            Return _Values
        End Get
    End Property

    Private Sub Refresca()
        _AllowEvents = False
        Dim oFilteredValues As DTOCondicio.Capitol.Collection = _Values
        _ControlItems = New ControlItems
        For Each oItem As DTOCondicio.Capitol In oFilteredValues
            Dim oControlItem As New ControlItem(_ControlItems.Count + 1, oItem, _Lang)
            _ControlItems.Add(oControlItem)
        Next

        Dim oCell As DTODatagridviewCell = UIHelper.DataGridviewCurrentCell(Me)
        MyBase.DataSource = _ControlItems
        UIHelper.SetDataGridviewCurrentCell(Me, oCell)

        SetContextMenu()
        _AllowEvents = True
    End Sub


    Public ReadOnly Property Value As DTOCondicio.Capitol
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As DTOCondicio.Capitol = oControlItem.Source
            Return retval
        End Get
    End Property

    Private Sub SetProperties()
        MyBase.RowTemplate.Height = MyBase.Font.Height * 1.3
        'MyBase.RowCondicioCapitol.DefaultCellStyle.BackColor = Color.Transparent

        MyBase.AutoGenerateColumns = False
        MyBase.Columns.Clear()

        MyBase.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        MyBase.ColumnHeadersVisible = True
        MyBase.RowHeadersVisible = False
        MyBase.MultiSelect = True
        MyBase.AllowUserToResizeRows = False
        MyBase.AllowDrop = False
        MyBase.ReadOnly = True


        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Ord)
            .HeaderText = "Ordre"
            .DataPropertyName = "Ord"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 50
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.BottomRight
            .DefaultCellStyle.Format = "#"
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Title)
            .HeaderText = "Capitol"
            .DataPropertyName = "Caption"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Fch)
            .HeaderText = "Ult.esmena"
            .DataPropertyName = "FchLastEdited"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 70
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            .DefaultCellStyle.Format = "dd/MM/yy"
        End With

    End Sub

    Private Function SelectedControlItems() As ControlItems
        Dim retval As New ControlItems
        For Each oRow As DataGridViewRow In MyBase.SelectedRows
            Dim oControlItem As ControlItem = oRow.DataBoundItem
            retval.Add(oControlItem)
        Next

        If retval.Count = 0 Then retval.Add(CurrentControlItem)
        Return retval
    End Function

    Private Function SelectedItems() As DTOCondicio.Capitol.Collection
        Dim retval As New DTOCondicio.Capitol.Collection
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
            Dim oMenu_CondicioCapitol As New Menu_CondicioCapitol(SelectedItems.First)
            AddHandler oMenu_CondicioCapitol.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_CondicioCapitol.Range)
            oContextMenu.Items.Add("-")
        End If
        oContextMenu.Items.Add("afegir", Nothing, AddressOf Do_AddNew)

        MyBase.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Do_AddNew()
        RaiseEvent RequestToAddNew(Me, MatEventArgs.Empty)
    End Sub


    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles MyBase.DoubleClick
        Dim oCurrentControlItem As ControlItem = CurrentControlItem()
        If oCurrentControlItem IsNot Nothing Then
            Dim oSelectedValue As DTOCondicio.Capitol = CurrentControlItem.Source
            Dim oFrm As New Frm_CondicioCapitol(oSelectedValue)
            AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                    oFrm.Show()

        End If
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles MyBase.SelectionChanged
        If _AllowEvents Then
            RaiseEvent ValueChanged(Me, New MatEventArgs(CurrentControlItem.Source))
            SetContextMenu()
        End If
    End Sub



    Protected Class ControlItem
        Property Source As DTOCondicio.Capitol

        Property Ord As Integer
        Property Caption As String
        Property FchLastEdited As Date

        Public Sub New(ord As Integer, value As DTOCondicio.Capitol, oLang As DTOLang)
            MyBase.New()
            _Source = value
            With value
                _Ord = .Ord
                _Caption = .Caption.Tradueix(oLang)
                _FchLastEdited = .UsrLog.FchLastEdited
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class
