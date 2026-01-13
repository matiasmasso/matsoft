Public Class Xl_Locations
    Inherits _Xl_ReadOnlyDatagridview

    Private _Values As IEnumerable(Of DTOLocation)
    Private _DefaultValue As DTOLocation
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse

    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)
    Public Event onItemSelected(sender As Object, e As MatEventArgs)


    Private Enum Cols
        nom
    End Enum

    Public Shadows Sub Load(values As IEnumerable(Of DTOLocation), Optional oDefaultValue As DTOLocation = Nothing, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse)
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

    Private Sub Refresca()
        _AllowEvents = False
        _ControlItems = New ControlItems
        For Each oItem As DTOLocation In _Values
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
        _Values = New List(Of DTOLocation)
        Refresca()
    End Sub

    Public ReadOnly Property Values As List(Of DTOLocation)
        Get
            Return _Values
        End Get
    End Property
    Public ReadOnly Property Value As DTOLocation
        Get
            Dim retval As DTOLocation = Nothing
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

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Nom)
            .HeaderText = "Poblacions"
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

    Private Function SelectedItems() As List(Of DTOLocation)
        Dim retval As New List(Of DTOLocation)
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
            Dim oMenu_Location As New Menu_Location(SelectedItems.First)
            AddHandler oMenu_Location.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_Location.Range)
            oContextMenu.Items.Add("-")
        End If

        Dim oReLocateMenuItem As New ToolStripMenuItem("canviar de zona", Nothing, AddressOf Do_reLocate)
        oReLocateMenuItem.Enabled = _Values.All(Function(x) x.Zona.Equals(_Values.First.Zona))
        oContextMenu.Items.Add(oReLocateMenuItem)

        oContextMenu.Items.Add("afegir", Nothing, AddressOf Do_AddNew)

        MyBase.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Do_AddNew()
        RaiseEvent RequestToAddNew(Me, MatEventArgs.Empty)
    End Sub

    Private Sub Do_reLocate()
        Dim oFrm As New Frm_Geo(DTOArea.SelectModes.SelectZona)
        AddHandler oFrm.onItemSelected, AddressOf onReLocate
        oFrm.Show()
    End Sub

    Private Async Sub onReLocate(sender As Object, e As MatEventArgs)
        Dim exs As New List(Of Exception)
        Dim oLocateTo As DTOZona = e.Argument
        Dim iCount As Integer = Await FEB.Locations.reLocate(exs, oLocateTo, SelectedItems)
        If exs.Count = 0 Then
            MsgBox(String.Format("reassignades {0} poblacions a {1}", iCount, oLocateTo.FullNom(Current.Session.Lang)))
            MyBase.RefreshRequest(Me, e)
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles MyBase.DoubleClick
        Dim oCurrentControlItem As ControlItem = CurrentControlItem()
        If oCurrentControlItem IsNot Nothing Then
            Dim oSelectedValue As DTOLocation = CurrentControlItem.Source
            Select Case _SelectionMode
                Case DTO.Defaults.SelectionModes.Browse
                    Dim oFrm As New Frm_Location(oSelectedValue)
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

    Protected Class ControlItem
        Property Source As DTOLocation

        Property Nom As String

        Public Sub New(value As DTOLocation)
            MyBase.New()
            _Source = value
            With value
                _Nom = .Nom
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class

