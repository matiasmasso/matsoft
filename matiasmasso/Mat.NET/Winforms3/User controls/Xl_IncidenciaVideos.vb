Public Class Xl_IncidenciaVideos
    Inherits _Xl_ReadOnlyDatagridview

    Private _Incidencia As DTOIncidencia
    Private _Videos As List(Of DTODocFile)

    Private _Filter As String
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)
    Public Event OnItemSelected(sender As Object, e As MatEventArgs)

    Private Enum Cols
        Id
    End Enum

    Public Shadows Sub Load(oIncidencia As DTOIncidencia)
        _Incidencia = oIncidencia
        _Videos = oIncidencia.videos

        Static PropertiesSet As Boolean
        If Not PropertiesSet Then
            SetProperties()
            PropertiesSet = True
        End If

        Refresca()
    End Sub

    Public Function Values() As List(Of DTODocFile)
        Dim retval As List(Of DTODocFile) = _ControlItems.Select(Function(x) x.Source).ToList()
        Return retval
    End Function

    Private Sub Refresca()
        _AllowEvents = False
        Dim oFilteredValues As List(Of DTODocFile) = FilteredValues()
        _ControlItems = New ControlItems
        For Each oItem As DTODocFile In oFilteredValues
            Dim oControlItem As New ControlItem(oItem)
            _ControlItems.Add(oControlItem)
        Next


        Dim oCell As DTODatagridviewCell = UIHelper.DataGridviewCurrentCell(Me)
        MyBase.DataSource = _ControlItems
        UIHelper.SetDataGridviewCurrentCell(Me, oCell)


        SetContextMenu()
        _AllowEvents = True
    End Sub

    Private Function FilteredValues() As List(Of DTODocFile)
        Dim retval As List(Of DTODocFile)
        If _Filter = "" Then
            retval = _Videos
        Else
            retval = _Videos.FindAll(Function(x) x.nom.ToLower.Contains(_Filter.ToLower))
        End If
        Return retval
    End Function


    Public Property Filter As String
        Get
            Return _Filter
        End Get
        Set(value As String)
            _Filter = value
            If _Videos IsNot Nothing Then Refresca()
        End Set
    End Property

    Public Sub ClearFilter()
        If _Filter > "" Then
            _Filter = ""
            Refresca()
        End If
    End Sub

    Public ReadOnly Property Value As DTODocFile
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As DTODocFile = oControlItem.Source
            Return retval
        End Get
    End Property

    Private Sub SetProperties()
        MyBase.RowTemplate.Height = MyBase.Font.Height * 1.3
        'MyBase.RowTemplate.DefaultCellStyle.BackColor = Color.Transparent

        MyBase.AutoGenerateColumns = False
        MyBase.Columns.Clear()

        MyBase.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        MyBase.ColumnHeadersVisible = False
        MyBase.RowHeadersVisible = False
        MyBase.MultiSelect = True
        MyBase.AllowUserToResizeRows = False
        MyBase.AllowDrop = False
        MyBase.ReadOnly = True

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Id)
            .DataPropertyName = "Id"
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

    Private Function SelectedItems() As List(Of DTODocFile)
        Dim retval As New List(Of DTODocFile)
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
            oContextMenu.Items.Add("Zoom", Nothing, AddressOf Do_Zoom)
            oContextMenu.Items.Add("Eliminar", Nothing, AddressOf Do_Remove)
            oContextMenu.Items.Add("-")
        End If
        oContextMenu.Items.Add("afegir", Nothing, AddressOf Do_AddNew)

        MyBase.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Do_Zoom()
        Dim oCurrentControlItem As ControlItem = CurrentControlItem()
        Dim url = "https://youtu.be/" & oCurrentControlItem.Source.hash
        UIHelper.ShowHtml(url)
    End Sub

    Private Sub Do_Remove()
        Dim oCurrentControlItem As ControlItem = CurrentControlItem()
        If oCurrentControlItem IsNot Nothing Then
            _ControlItems.Remove(oCurrentControlItem)
            RaiseEvent AfterUpdate(Me, New MatEventArgs())
        End If
    End Sub

    Private Sub Do_AddNew()
        Dim oFrm As New Frm_YouTubeId(_Incidencia)
        AddHandler oFrm.AfterUpdate, AddressOf onItemAdded
        oFrm.Show()
    End Sub

    Private Sub onItemAdded(sender As Object, e As MatEventArgs)
        Dim oDocfile As DTODocFile = e.Argument
        Dim oControlItem As New ControlItem(oDocfile)
        _ControlItems.Add(oControlItem)
        RaiseEvent AfterUpdate(Me, New MatEventArgs())
    End Sub

    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles MyBase.DoubleClick
        Do_Zoom()
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles MyBase.SelectionChanged
        If _AllowEvents Then
            RaiseEvent ValueChanged(Me, New MatEventArgs(CurrentControlItem.Source))
            SetContextMenu()
        End If
    End Sub


    Protected Class ControlItem
        Property Source As DTODocFile

        Property Id As String
        Public Sub New(value As DTODocFile)
            MyBase.New()
            _Source = value
            With value
                _Id = .hash
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class


