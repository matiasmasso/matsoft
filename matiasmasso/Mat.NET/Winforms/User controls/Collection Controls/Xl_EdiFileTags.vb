Public Class Xl_EdiversaFileTags
    Inherits _Xl_ReadOnlyDatagridview

    Private _Values As List(Of String)
    Private _DefaultValue As String
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.browse

    Private _Filter As String
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)
    Public Event onItemSelected(sender As Object, e As MatEventArgs)

    Private Enum Cols
        tag
    End Enum

    Public Shadows Sub Load(values As List(Of String))
        _Values = values

        Static PropertiesSet As Boolean
        If Not PropertiesSet Then
            SetProperties()
            PropertiesSet = True
        End If

        Refresca()
    End Sub

    Public Shadows Sub Clear()
        _Values = New List(Of String)
        Refresca()
    End Sub

    Private Sub Refresca()
        _AllowEvents = False
        _ControlItems = New ControlItems
        For Each item As String In _Values
            Dim oControlItem As New ControlItem(item)
            _ControlItems.Add(oControlitem)
        Next

        Dim oCell As DTODatagridviewCell = UIHelper.DataGridviewCurrentCell(Me)
        MyBase.DataSource = _ControlItems

        If oCell Is Nothing Then
            Dim sDefaultTag As String = DTOEdiversaFile.Tags.ORDERS_D_96A_UN_EAN008.ToString
            For Each oRow As DataGridViewRow In MyBase.Rows
                If oRow.DataBoundItem.tag = sDefaultTag Then
                    MyBase.CurrentCell = oRow.Cells(Cols.tag)
                    Exit For
                End If
            Next
        Else
            UIHelper.SetDataGridviewCurrentCell(Me, oCell)
        End If

        SetContextMenu()
        _AllowEvents = True
    End Sub



    Public ReadOnly Property Value As String
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As String = ""
            If oControlItem IsNot Nothing Then
                retval = oControlItem.Tag
            End If
            Return retval
        End Get
    End Property


    Private Sub SetProperties()
        MyBase.RowTemplate.Height = MyBase.Font.Height * 1.3
        'MyBase.RowEdiversaFile.DefaultCellStyle.BackColor = Color.Transparent

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
        With MyBase.Columns(Cols.tag)
            .HeaderText = "Missatge"
            .DataPropertyName = "Tag"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
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

    Private Function SelectedItems() As List(Of String)
        Dim retval As New List(Of String)
        For Each oRow As DataGridViewRow In MyBase.SelectedRows
            Dim oControlItem As ControlItem = oRow.DataBoundItem
            retval.Add(oControlItem.Tag)
        Next

        If retval.Count = 0 Then retval.Add(CurrentControlItem.Tag)
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
            Dim oMenu_EdiversaFile As New Menu_EdiversaFileTag(SelectedItems)
            AddHandler oMenu_EdiversaFile.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_EdiversaFile.Range)
        End If

        MyBase.ContextMenuStrip = oContextMenu
    End Sub


    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles MyBase.SelectionChanged
        If _AllowEvents Then
            RaiseEvent ValueChanged(Me, New MatEventArgs(CurrentControlItem.Tag))
            SetContextMenu()
        End If
    End Sub



    Protected Class ControlItem
        Property Tag As String

        Public Sub New(sTag As String)
            MyBase.New
            _Tag = sTag
        End Sub
    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class

