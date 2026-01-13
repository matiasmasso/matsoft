Public Class Xl_LeadAreas

    Inherits DataGridView

    Private _DefaultValue As DTOLeadAreas
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse

    Private _Filter As String
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event ItemCheck(ByVal sender As Object, ByVal e As ItemCheckEventArgs)
    Public Event RequestToRefresh(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)

    Property AllValues As List(Of DTOLeadAreas)
    Property FilteredValues As List(Of DTOLeadAreas)
    Property ParentLeadArea As DTOLeadAreas

    Private Enum Cols
        Check
        Nom
        Leads
    End Enum

    Public Shadows Sub Load(AllValues As List(Of DTOLeadAreas), Optional oParentLeadArea As DTOLeadAreas = Nothing, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse)
        Static PropertiesSet As Boolean
        If Not PropertiesSet Then
            SetProperties()
            PropertiesSet = True
        End If

        _AllValues = AllValues
        _ParentLeadArea = oParentLeadArea
        _SelectionMode = oSelectionMode
        Refresca()
    End Sub



    Private Sub Refresca()
        _AllowEvents = False


        _FilteredValues = GetFilteredValues()
        _ControlItems = New ControlItems
        For Each oItem As DTOLeadAreas In _FilteredValues
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
                MyBase.CurrentCell = MyBase.Rows(rowIdx).Cells(Cols.Nom)
            End If
        End If

        SetContextMenu()
        _AllowEvents = True
    End Sub

    Private Function GetFilteredValues() As List(Of DTOLeadAreas)
        Dim retval As List(Of DTOLeadAreas) = Nothing
        If _ParentLeadArea Is Nothing Then
            retval = LeadCountries()
        ElseIf TypeOf _ParentLeadArea.Area Is DTOCountry Then
            retval = LeadZonas(_ParentLeadArea)
        ElseIf TypeOf _ParentLeadArea.Area Is DTOZona Then
            retval = LeadLocations(_ParentLeadArea)
        End If
        Return retval
    End Function


    Public ReadOnly Property Value As DTOLeadAreas
        Get
            Dim retval As DTOLeadAreas = Nothing
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

        MyBase.DataSource = _ControlItems
        MyBase.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        MyBase.ColumnHeadersVisible = False
        MyBase.RowHeadersVisible = False
        MyBase.MultiSelect = True
        MyBase.AllowUserToResizeRows = False
        MyBase.AllowUserToResizeColumns = False
        MyBase.AllowDrop = False
        MyBase.ReadOnly = True

        MyBase.Columns.Add(New DataGridViewImageColumn)
        With DirectCast(MyBase.Columns(Cols.Check), DataGridViewImageColumn)
            .HeaderText = ""
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 16
            .DefaultCellStyle.NullValue = Nothing
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Nom)
            .HeaderText = "Area"
            .DataPropertyName = "Nom"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Leads)
            .HeaderText = "Leads"
            .DataPropertyName = "Leads"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 70
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,##0"
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

    Private Function SelectedItems() As List(Of DTOLeadAreas)
        Dim retval As New List(Of DTOLeadAreas)
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
            oContextMenu.Items.Add("Seleccionar", My.Resources.Checked13, AddressOf Do_Check)
            oContextMenu.Items.Add("Deseleccionar", My.Resources.UnChecked13, AddressOf Do_UnCheck)
        End If

        MyBase.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Do_Check(sender As Object, e As System.EventArgs)
        For Each oRow As DataGridViewRow In MyBase.SelectedRows
            'Dim oControlitem As ControlItem = oRow.DataBoundItem
            'oControlitem.CheckState = CheckState.Checked
            MyBase.NotifyCurrentCellDirty(True)
        Next
        MyBase.Refresh()
    End Sub
    Private Sub Do_UnCheck(sender As Object, e As System.EventArgs)
        For Each oRow As DataGridViewRow In MyBase.SelectedRows
            'Dim oControlitem As ControlItem = oRow.DataBoundItem
            'oControlitem.CheckState = CheckState.Unchecked
            MyBase.NotifyCurrentCellDirty(True)
        Next
        MyBase.Refresh()

    End Sub

    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles MyBase.DoubleClick
        Dim oCurrentControlItem As ControlItem = CurrentControlItem()
        If oCurrentControlItem IsNot Nothing Then
            Dim oSelectedValue As DTOLeadAreas = CurrentControlItem.Source
            Select Case _SelectionMode
                Case DTO.Defaults.SelectionModes.Browse
                    'Dim oFrm As New Frm_LeadArea(oSelectedValue)
                    'AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                    'oFrm.Show()
                Case DTO.Defaults.SelectionModes.Selection
                    'RaiseEvent onItemSelected(Me, New MatEventArgs(Me.Value))
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

    Private Sub DataGridView1_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles MyBase.CellFormatting
        Select Case e.ColumnIndex
            Case Cols.Check
                Dim oControlItem As ControlItem = MyBase.Rows(e.RowIndex).DataBoundItem
                Select Case oControlItem.CheckState
                    Case CheckState.Checked
                        e.Value = My.Resources.Checked13
                    Case CheckState.Unchecked
                        e.Value = My.Resources.UnChecked13
                    Case CheckState.Indeterminate
                        e.Value = My.Resources.CheckedGrayed13
                End Select
        End Select
    End Sub




    Private Sub DataGridView1_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles MyBase.CellMouseClick
        Select Case e.ColumnIndex
            Case Cols.Check
                Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
                Dim oControlItem As ControlItem = oRow.DataBoundItem
                Dim oCurrentCheckState As CheckState = oControlItem.CheckState
                Dim oNewCheckState As CheckState = IIf(oControlItem.CheckState = CheckState.Checked, CheckState.Unchecked, CheckState.Checked)
                'MyBase.Refresh()
                Dim oArgs As New ItemCheckEventArgs(e.RowIndex, oNewCheckState, oCurrentCheckState)
                RaiseEvent ItemCheck(Me, oArgs)
        End Select
    End Sub

    Private Function LeadCountries() As List(Of DTOLeadAreas)
        Dim retval As New List(Of DTOLeadAreas)
        Dim oCountry As New DTOCountry
        Dim oLeadCountry As New DTOLeadArea(oCountry)
        For Each item As DTOLeadAreas In _AllValues
            If DirectCast(item.Area, DTOLocation).Zona.Country.UnEquals(oLeadCountry.Area) Then
                oLeadCountry = New DTOLeadArea(DirectCast(item.Area, DTOLocation).Zona.Country)
                retval.Add(oLeadCountry)
            End If
            oLeadCountry.Leads.AddRange(item.Leads)
        Next
        Return retval
    End Function

    Private Function LeadZonas(oLeadCountry As DTOLeadAreas) As List(Of DTOLeadAreas)
        Dim retval As New List(Of DTOLeadAreas)
        Dim oParent As DTOCountry = oLeadCountry.Area
        Dim oAreas As List(Of DTOLeadAreas) = _AllValues.FindAll(Function(x) DirectCast(x.Area, DTOLocation).Zona.Country.Equals(oParent))
        Dim oZona As New DTOZona
        Dim oLeadZona As New DTOLeadArea(oZona)
        For Each item As DTOLeadAreas In oAreas
            If DirectCast(item.Area, DTOLocation).Zona.UnEquals(oLeadZona.Area) Then
                oLeadZona = New DTOLeadArea(DirectCast(item.Area, DTOLocation).Zona)
                retval.Add(oLeadZona)
            End If
            oLeadZona.Leads.AddRange(item.Leads)
        Next
        Return retval
    End Function

    Private Function LeadLocations(oLeadZona As DTOLeadAreas) As List(Of DTOLeadAreas)
        Dim oParent As DTOZona = oLeadZona.Area
        Dim retval As List(Of DTOLeadAreas) = _AllValues.FindAll(Function(x) DirectCast(x.Area, DTOLocation).Zona.Equals(oParent))
        Return retval
    End Function

    Protected Class ControlItem
        Property Source As DTOLeadAreas
        Property Nom As String
        Property Leads As Integer

        Public Sub New(value As DTOLeadAreas)
            MyBase.New()
            _Source = value
            With value
                _Nom = DTOArea.NomOrDefault(.Area)
                _Leads = .Leads.Count
            End With
        End Sub

        ReadOnly Property CheckState As CheckState
            Get
                Dim retval As CheckState = CheckState.Indeterminate
                Dim CheckedExist As Boolean = _Source.Leads.Exists(Function(x) x.Checked = True)
                Dim UnCheckedExist As Boolean = _Source.Leads.Exists(Function(x) x.Checked = False)
                If CheckedExist Then
                    If Not UnCheckedExist Then retval = CheckState.Checked
                Else
                    If UnCheckedExist Then retval = CheckState.Unchecked
                End If
                Return retval
            End Get
        End Property
    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class


