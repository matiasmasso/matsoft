Public Class Xl_Areas
    Inherits DataGridView

    Private _Values As List(Of DTOArea)
    Private _Filter As String
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event RequestToRefresh(sender As Object, e As MatEventArgs)
    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)
    Public Event RequestToRemove(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)

    Private Enum Cols
        Nom
    End Enum

    Public Shadows Sub Load(values As List(Of DTOArea), Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse)
        Static PropertiesSet As Boolean
        If Not PropertiesSet Then
            SetProperties()
            PropertiesSet = True
        End If

        _Values = values
        Refresca()
    End Sub

    Public ReadOnly Property Values As List(Of DTOArea)
        Get
            Return _Values
        End Get
    End Property

    Private Sub Refresca()
        _AllowEvents = False
        Dim oFilteredValues As List(Of DTOArea) = FilteredValues()
        _ControlItems = New ControlItems
        For Each oItem As DTOArea In FilteredValues()
            Dim oControlItem As New ControlItem(oItem)
            _ControlItems.Add(oControlItem)
        Next

        If _ControlItems.Count > 0 Then
            MyBase.DataSource = _ControlItems
            MyBase.CurrentCell = MyBase.FirstDisplayedCell
            SetContextMenu()
        End If

        SetContextMenu()
        _AllowEvents = True
    End Sub

    Private Function FilteredValues() As List(Of DTOArea)
        Dim retval As List(Of DTOArea)
        If _Filter = "" Then
            retval = _Values
        Else
            retval = _Values.FindAll(Function(x) DTOArea.NomOrDefault(x).ToLower.Contains(_Filter.ToLower))
        End If
        Return retval
    End Function


    Public Property Filter As String
        Get
            Return _Filter
        End Get
        Set(value As String)
            _Filter = value
            If _Values IsNot Nothing Then Refresca()
        End Set
    End Property

    Public Sub ClearFilter()
        If _Filter > "" Then
            _Filter = ""
            Refresca()
        End If
    End Sub

    Public ReadOnly Property Value As DTOArea
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As DTOArea = oControlItem.Source
            Return retval
        End Get
    End Property

    Private Sub SetProperties()
        MyBase.RowTemplate.Height = MyBase.Font.Height * 1.3

        MyBase.AutoGenerateColumns = False
        MyBase.Columns.Clear()

        MyBase.DataSource = _ControlItems
        MyBase.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        MyBase.ColumnHeadersVisible = True
        MyBase.RowHeadersVisible = False
        MyBase.MultiSelect = True
        MyBase.AllowUserToResizeRows = False
        MyBase.AllowDrop = False
        MyBase.ReadOnly = True


        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Nom)
            .HeaderText = "Nom"
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

    Private Function SelectedItems() As List(Of DTOArea)
        Dim retval As New List(Of DTOArea)
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
            Dim oSelectedValue As DTOArea = oControlItem.Source
            If TypeOf oSelectedValue Is DTOCountry Then
                Dim oCountry As DTOCountry = oSelectedValue
                Dim oMenu_Country As New Menu_Country(SelectedItems.First)
                AddHandler oMenu_Country.AfterUpdate, AddressOf RefreshRequest
                oContextMenu.Items.AddRange(oMenu_Country.Range)
            ElseIf TypeOf oSelectedValue Is DTOZona Then
                Dim oZona As DTOZona = oSelectedValue
                Dim oMenu_Zona As New Menu_Zona(SelectedItems.First)
                AddHandler oMenu_Zona.AfterUpdate, AddressOf RefreshRequest
                oContextMenu.Items.AddRange(oMenu_Zona.Range)
            ElseIf TypeOf oSelectedValue Is DTOLocation Then
                Dim oLocation As DTOLocation = oSelectedValue
                Dim oMenu_Location As New Menu_Location(SelectedItems.First)
                AddHandler oMenu_Location.AfterUpdate, AddressOf RefreshRequest
                oContextMenu.Items.AddRange(oMenu_Location.Range)
            ElseIf TypeOf oSelectedValue Is DTOZip Then
                Dim oZip As DTOZip = oSelectedValue
                Dim oMenu_Zip As New Menu_Zip(SelectedItems.First)
                AddHandler oMenu_Zip.AfterUpdate, AddressOf RefreshRequest
                oContextMenu.Items.AddRange(oMenu_Zip.Range)
            End If


            oContextMenu.Items.Add("retirar", My.Resources.del, AddressOf Do_RemoveRow)
            oContextMenu.Items.Add("-")
        End If
        oContextMenu.Items.Add("afegir", Nothing, AddressOf Do_AddNew)

        MyBase.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Do_AddNew()
        RaiseEvent RequestToAddNew(Me, MatEventArgs.Empty)
    End Sub

    Private Sub Do_RemoveRow()
        RaiseEvent RequestToRemove(Me, New MatEventArgs(Me.Value))
    End Sub


    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles MyBase.DoubleClick
        Dim oSelectedValue As DTOArea = CurrentControlItem.Source
        If TypeOf oSelectedValue Is DTOCountry Then
            Dim oCountry As DTOCountry = oSelectedValue
            Dim oFrm As New Frm_Country(oSelectedValue)
            AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
            oFrm.Show()
        ElseIf TypeOf oSelectedValue Is DTOZona Then
            Dim oZona As DTOZona = oSelectedValue
            Dim oFrm As New Frm_Zona(oSelectedValue)
            AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
            oFrm.Show()
        ElseIf TypeOf oSelectedValue Is DTOLocation Then
            Dim oLocation As DTOLocation = oSelectedValue
            Dim oFrm As New Frm_Location(oSelectedValue)
            AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
            oFrm.Show()
        ElseIf TypeOf oSelectedValue Is DTOZip Then
            Dim oZip As DTOZip = oSelectedValue
            Dim oFrm As New Frm_Zip(oSelectedValue)
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

    Private Sub RefreshRequest()
        RaiseEvent RequestToRefresh(Me, MatEventArgs.Empty)
    End Sub

    Protected Class ControlItem
        Property Source As DTOArea

        Property Nom As String

        Public Sub New(value As DTOArea)
            MyBase.New()
            _Source = value
            With value
                _Nom = DTOArea.FullNomSegmentedReversed(value, Current.Session.User.Lang)
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class


