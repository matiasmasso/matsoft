Public Class Xl_ZonasIsoPais
    Inherits DataGridView

    Private _Values As List(Of DTOZona)
    Private _DefaultValue As DTOZona
    Private _PendingDelete As DTOZona
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse

    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event RequestToRefresh(sender As Object, e As MatEventArgs)
    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)
    Public Event RequestToDelete(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)
    Public Event onItemSelected(sender As Object, e As MatEventArgs)


    Private Enum Cols
        Iso
        nom
    End Enum

    Public Shadows Sub Load(values As List(Of DTOZona), Optional oDefaultValue As DTOZona = Nothing, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse)
        Static PropertiesSet As Boolean
        If Not PropertiesSet Then
            SetProperties()
            PropertiesSet = True
        End If

        _Values = values
        _DefaultValue = oDefaultValue
        _SelectionMode = oSelectionMode

        Refresca()
    End Sub

    ReadOnly Property Values As List(Of DTOZona)
        Get
            Return _Values
        End Get
    End Property

    Private Sub Refresca()
        _AllowEvents = False
        _ControlItems = New ControlItems
        For Each oItem As DTOZona In _Values
            Dim oControlItem As New ControlItem(oItem)
            _ControlItems.Add(oControlItem)
        Next

        MyBase.DataSource = _ControlItems
        If _ControlItems.Count > 0 Then
            MyBase.CurrentCell = MyBase.FirstDisplayedCell
        End If

        If _DefaultValue IsNot Nothing Then
            Dim oControlItem As ControlItem = _ControlItems.ToList.Find(Function(x) x.Source.Equals(_DefaultValue))
            Dim rowIdx As Integer = _ControlItems.IndexOf(oControlItem)
            If rowIdx >= 0 Then
                MyBase.CurrentCell = MyBase.Rows(rowIdx).Cells(Cols.nom)
            End If
        End If

        SetContextMenu()
        _AllowEvents = True
    End Sub

    Public Sub Clear()
        _Values = New List(Of DTOZona)
        Refresca()
    End Sub

    Public ReadOnly Property Value As DTOZona
        Get
            Dim retval As DTOZona = Nothing
            Dim oControlItem As ControlItem = CurrentControlItem()
            If oControlItem IsNot Nothing Then
                retval = oControlItem.Source
            End If
            Return retval
        End Get
    End Property

    Private Sub SetProperties()
        MyBase.RowTemplate.Height = MyBase.Font.Height * 1.3

        MyBase.AutoGenerateColumns = False
        MyBase.Columns.Clear()

        MyBase.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        MyBase.ColumnHeadersVisible = True
        MyBase.RowHeadersVisible = False
        MyBase.MultiSelect = True
        MyBase.AllowUserToResizeRows = False
        MyBase.AllowDrop = False
        MyBase.ReadOnly = True
        MyBase.AllowUserToDeleteRows = True

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Iso)
            .HeaderText = "Pais"
            .DataPropertyName = "Iso"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.nom)
            .HeaderText = "Zones"
            .DataPropertyName = "Nom"
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

    Private Function SelectedItems() As List(Of DTOZona)
        Dim retval As New List(Of DTOZona)
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
            oContextMenu.Items.Add("zoom", Nothing, AddressOf Do_Zoom)
            oContextMenu.Items.Add("eliminar", Nothing, AddressOf Do_Delete)
        End If
        oContextMenu.Items.Add("afegir", Nothing, AddressOf Do_AddNew)

        MyBase.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Do_Zoom()
        Dim oControlItem As ControlItem = CurrentControlItem()
        Dim oZona As DTOZona = oControlItem.Source
        Dim oFrm As New Frm_Zona(oZona)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Do_Delete()
        Dim oControlItem As ControlItem = CurrentControlItem()
        Dim oZona As DTOZona = oControlItem.Source
        RaiseEvent RequestToDelete(Me, New MatEventArgs(oZona))
    End Sub

    Private Sub Do_AddNew()
        RaiseEvent RequestToAddNew(Me, MatEventArgs.Empty)
    End Sub


    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles MyBase.DoubleClick
        Dim oCurrentControlItem As ControlItem = CurrentControlItem()
        If oCurrentControlItem IsNot Nothing Then
            Dim oSelectedValue As DTOZona = CurrentControlItem.Source
            Select Case _SelectionMode
                Case DTO.Defaults.SelectionModes.Browse
                    Dim oFrm As New Frm_Zona(oSelectedValue)
                    AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                    oFrm.Show()
                Case DTO.Defaults.SelectionModes.Selection
                    RaiseEvent onItemSelected(Me, New MatEventArgs(Me.Value))
            End Select

        End If
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles MyBase.SelectionChanged
        If _AllowEvents Then
            RaiseEvent ValueChanged(Me, New MatEventArgs(CurrentControlItem.Source))
            SetContextMenu()
        End If
    End Sub

    Private Sub RefreshRequest()
        RaiseEvent RequestToRefresh(Me, MatEventArgs.Empty)
    End Sub


    Private Sub Xl_ZonasIsoPais_UserDeletingRow(sender As Object, e As DataGridViewRowCancelEventArgs) Handles Me.UserDeletingRow
        Dim oControlItem As ControlItem = e.Row.DataBoundItem
        Dim oTrpZon As DTOZona = oControlItem.Source
        Dim sMsg As String = String.Format("Retirem {0} de la llista?", oTrpZon.Nom)
        Dim rc As MsgBoxResult = MsgBox(sMsg, MsgBoxStyle.OkCancel)
        If rc = MsgBoxResult.Ok Then
            _PendingDelete = oTrpZon
        Else
            e.Cancel = True
        End If
    End Sub

    Private Sub Xl_ZonasIsoPais_UserDeletedRow(sender As Object, e As DataGridViewRowEventArgs) Handles Me.UserDeletedRow
        RaiseEvent RequestToDelete(Me, New MatEventArgs(_PendingDelete))
        _PendingDelete = Nothing
    End Sub

    Protected Class ControlItem
        Property Source As DTOZona

        Property ISO As String
        Property Nom As String

        Public Sub New(value As DTOZona)
            MyBase.New()
            _Source = value
            With value
                _ISO = .Country.ISO
                _Nom = .Nom
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class
