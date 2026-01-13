Public Class Xl_GeoSearch
    Inherits _Xl_ReadOnlyDatagridview

    Private _Values As List(Of DTOArea)
    Private _DefaultValue As DTOArea
    Private _SelectionMode As DTOArea.SelectModes

    Private _Filter As String
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)
    Public Event OnItemSelected(sender As Object, e As MatEventArgs)

    Private Enum Cols
        Country
        Zona
        Location
        Zip
    End Enum

    Public Shadows Sub Load(values As List(Of DTOArea), Optional oDefaultValue As DTOArea = Nothing, Optional oSelectionMode As DTOArea.SelectModes = DTOArea.SelectModes.Browse)
        _Values = values
        _SelectionMode = oSelectionMode
        _DefaultValue = oDefaultValue

        Static PropertiesSet As Boolean
        If Not PropertiesSet Then
            SetProperties()
            PropertiesSet = True
        End If

        Refresca()
    End Sub

    Private Sub Refresca()
        _AllowEvents = False
        Dim oFilteredValues As List(Of DTOArea) = FilteredValues()
        _ControlItems = New ControlItems
        For Each oItem As DTOArea In oFilteredValues
            Dim oControlItem As New ControlItem(oItem)
            _ControlItems.Add(oControlItem)
        Next


        Dim oCell As DTODatagridviewCell = UIHelper.DataGridviewCurrentCell(Me)
        MyBase.DataSource = _ControlItems
        UIHelper.SetDataGridviewCurrentCell(Me, oCell)

        If _DefaultValue IsNot Nothing Then
            Dim oControlItem As ControlItem = _ControlItems.ToList.Find(Function(x) x.Source.Equals(_DefaultValue))
            Dim rowIdx As Integer = _ControlItems.IndexOf(oControlItem)
            If rowIdx >= 0 Then
                MyBase.CurrentCell = MyBase.Rows(rowIdx).Cells(Cols.Country)
            End If
        End If

        SetContextMenu()
        _AllowEvents = True
    End Sub

    Private Function FilteredValues() As List(Of DTOArea)
        Dim retval As List(Of DTOArea)
        If _Filter = "" Then
            retval = _Values
        Else
            retval = _Values.FindAll(Function(x) x.Nom.ToLower.Contains(_Filter.ToLower))
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
        'MyBase.RowArea.DefaultCellStyle.BackColor = Color.Transparent

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
        With MyBase.Columns(Cols.Country)
            .HeaderText = "Pais"
            .DataPropertyName = "Country"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Zona)
            .HeaderText = "Zona"
            .DataPropertyName = "Zona"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Location)
            .HeaderText = "Població"
            .DataPropertyName = "Location"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Zip)
            .HeaderText = "Codi postal"
            .DataPropertyName = "Zip"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 100
        End With

        Select Case _SelectionMode
            Case DTOArea.SelectModes.SelectCountry
                MyBase.Columns(Cols.Zona).Visible = False
                MyBase.Columns(Cols.Location).Visible = False
                MyBase.Columns(Cols.Zip).Visible = False
            Case DTOArea.SelectModes.SelectZona
                MyBase.Columns(Cols.Location).Visible = False
                MyBase.Columns(Cols.Zip).Visible = False
            Case DTOArea.SelectModes.SelectLocation
                MyBase.Columns(Cols.Zip).Visible = False
        End Select

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
            Dim oMenu_Area As New Menu_Area(SelectedItems.First)
            AddHandler oMenu_Area.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_Area.Range)
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
            Dim oArea As DTOArea = CurrentControlItem.Source
            Select Case _SelectionMode
                Case DTOArea.SelectModes.SelectAny,
                 DTOArea.SelectModes.SelectCountry And oArea.Cod = DTOArea.Cods.Country,
                 DTOArea.SelectModes.SelectZona And oArea.Cod = DTOArea.Cods.Zona,
                 DTOArea.SelectModes.SelectLocation And oArea.Cod = DTOArea.Cods.Location,
                 DTOArea.SelectModes.SelectZip And oArea.Cod = DTOArea.Cods.Zip

                    RaiseEvent OnItemSelected(Me, New MatEventArgs(Me.Value))

                Case Else

                    Select Case oArea.Cod
                        Case DTOArea.Cods.Country
                            Dim oFrm As New Frm_Country(oArea)
                            AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                            oFrm.Show()
                        Case DTOArea.Cods.Zona
                            Dim oFrm As New Frm_Zona(oArea)
                            AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                            oFrm.Show()
                        Case DTOArea.Cods.Location
                            Dim oFrm As New Frm_Location(oArea)
                            AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                            oFrm.Show()
                        Case DTOArea.Cods.Zip
                            Dim oFrm As New Frm_Zip(oArea)
                            AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                            oFrm.Show()
                    End Select
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
        Property Source As DTOArea
        Property Country As String
        Property Zona As String
        Property Location As String
        Property Zip As String

        Public Sub New(value As DTOArea)
            MyBase.New()
            _Source = value
            Select Case value.Cod
                Case DTOArea.Cods.Country
                    Dim oCountry = CType(value, DTOCountry)
                    _Country = oCountry.LangNom.Tradueix(Current.Session.Lang)
                Case DTOArea.Cods.Zona
                    Dim oZona = CType(value, DTOZona)
                    _Country = oZona.Country.LangNom.Tradueix(Current.Session.Lang)
                    _Zona = oZona.Nom
                Case DTOArea.Cods.Location
                    Dim oLocation = CType(value, DTOLocation)
                    _Country = oLocation.Zona.Country.LangNom.Tradueix(Current.Session.Lang)
                    _Zona = oLocation.Zona.Nom
                    _Location = oLocation.Nom
                Case DTOArea.Cods.Zip
                    Dim oZip = CType(value, DTOZip)
                    _Country = oZip.Location.Zona.Country.LangNom.Tradueix(Current.Session.Lang)
                    _Zona = oZip.Location.Zona.Nom
                    _Location = oZip.Location.Nom
                    _Zip = oZip.ZipCod
            End Select
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class

