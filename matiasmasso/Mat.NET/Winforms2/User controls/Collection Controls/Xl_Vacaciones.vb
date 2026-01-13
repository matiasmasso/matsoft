Public Class Xl_Vacaciones

    Inherits _Xl_ReadOnlyDatagridview

    Private _Values As List(Of DTOVacacion)
    Private _DefaultValue As DTOVacacion
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse

    Private _Filter As String
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)
    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)
    Public Event onItemSelected(sender As Object, e As MatEventArgs)

    Private Enum Cols
        Txt
    End Enum

    Public Shadows Sub Load(values As List(Of DTOVacacion), Optional oDefaultValue As DTOVacacion = Nothing, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse)
        Static PropertiesSet As Boolean
        If Not PropertiesSet Then
            SetProperties()
            PropertiesSet = True
        End If

        _Values = values
        _SelectionMode = oSelectionMode
        Refresca()
    End Sub

    Private Sub Refresca()
        _AllowEvents = False
        Dim oFilteredValues As List(Of DTOVacacion) = _Values
        _ControlItems = New ControlItems
        For Each oItem As DTOVacacion In oFilteredValues
            Dim oControlItem As New ControlItem(oItem)
            _ControlItems.Add(oControlItem)
        Next

        Dim oCell As DTODatagridviewCell = UIHelper.DataGridviewCurrentCell(Me)
        MyBase.DataSource = _ControlItems
        UIHelper.SetDataGridviewCurrentCell(Me, oCell)

        SetContextMenu()
        _AllowEvents = True
    End Sub



    Public ReadOnly Property Values As List(Of DTOVacacion)
        Get
            Return _Values
        End Get
    End Property
    Public ReadOnly Property Value As DTOVacacion
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As DTOVacacion = oControlItem.Source
            Return retval
        End Get
    End Property

    Private Sub SetProperties()
        MyBase.RowTemplate.Height = MyBase.Font.Height * 1.3
        'MyBase.RowVacacion.DefaultCellStyle.BackColor = Color.Transparent

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
        With MyBase.Columns(Cols.Txt)
            .HeaderText = "Aplaçaments per vacances"
            .DataPropertyName = "Txt"
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

    Private Function SelectedItems() As List(Of DTOVacacion)
        Dim retval As New List(Of DTOVacacion)
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
            Dim oMenu_Vacacion As New Menu_Vacacion(SelectedItems.First)
            AddHandler oMenu_Vacacion.AfterUpdate, AddressOf Do_Refresh
            AddHandler oMenu_Vacacion.RequestToDelete, AddressOf Do_Delete
            oContextMenu.Items.AddRange(oMenu_Vacacion.Range)
            oContextMenu.Items.Add("-")
        End If
        oContextMenu.Items.Add("afegir", Nothing, AddressOf Do_AddNew)

        MyBase.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Do_AddNew()
        Dim oVacacion As New DTOVacacion
        Dim oFrm As New Frm_Vacacion(oVacacion)
        AddHandler oFrm.AfterUpdate, AddressOf onAddNew
        oFrm.Show()
    End Sub

    Private Sub onAddNew(sender As Object, e As MatEventArgs)
        _Values.Add(e.Argument)
        Refresca()
        RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
    End Sub

    Private Sub Do_Refresh()
        Refresca()
        RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
    End Sub

    Private Sub Do_Delete(sender As Object, e As MatEventArgs)
        _Values.Remove(e.Argument)
        Refresca()
        RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
    End Sub


    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles MyBase.DoubleClick
        Dim oCurrentControlItem As ControlItem = CurrentControlItem()
        If oCurrentControlItem IsNot Nothing Then
            Dim oSelectedValue As DTOVacacion = CurrentControlItem.Source
            Select Case _SelectionMode
                Case DTO.Defaults.SelectionModes.Browse
                    Dim oFrm As New Frm_Vacacion(oSelectedValue)
                    AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                    AddHandler oFrm.RequestToDelete, AddressOf Do_Delete
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



    Protected Class ControlItem
        Property Source As DTOVacacion

        Property Txt As String

        Public Sub New(value As DTOVacacion)
            MyBase.New()
            _Source = value
            With value
                _Txt = DTOVacacion.Text(value, Current.Session.Lang)
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class


