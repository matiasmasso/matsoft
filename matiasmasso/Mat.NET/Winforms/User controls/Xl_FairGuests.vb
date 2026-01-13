Public Class Xl_FairGuests
    Private _values As List(Of DTOFairGuest)
    Private _filter As String

    'Private _SelectionMode As BLL.Defaults.SelectionModes
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event RequestToRefresh(sender As Object, e As MatEventArgs)
    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)

    Private Enum Cols
        Fch
        FirstName
        LastName
        RaoSocial
        Location
    End Enum

    Public Shadows Sub Load(values As List(Of DTOFairGuest))
        _values = values
        refresca()
    End Sub

    Public ReadOnly Property Value As DTOFairGuest
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As DTOFairGuest = oControlItem.Source
            Return retval
        End Get
    End Property


    Public ReadOnly Property Count As Integer
        Get
            Dim retval As Integer = 0
            If _ControlItems IsNot Nothing Then
                retval = _ControlItems.Count
            End If
            Return retval
        End Get
    End Property

    Public Property Filter As String
        Get
            Return _filter
        End Get
        Set(value As String)
            _filter = value
            refresca()
        End Set
    End Property

    Public Sub ClearFilter()
        If _filter > "" Then
            _filter = ""
            refresca()
        End If
    End Sub

    Private Sub refresca()
        _ControlItems = New ControlItems
        If _values IsNot Nothing Then

            For Each oItem As DTOFairGuest In _values
                Dim blProcede As Boolean
                If _filter = "" Then
                    blProcede = True
                Else
                    blProcede = (oItem.FirstName.ToUpper.Contains(_filter.ToUpper) Or
                             oItem.LastName.ToUpper.Contains(_filter.ToUpper) Or
                             oItem.RaoSocial.ToUpper.Contains(_filter.ToUpper) Or
                             oItem.Location.ToUpper.Contains(_filter.ToUpper))
                End If
                If blProcede Then
                    Dim oControlItem As New ControlItem(oItem)
                    _ControlItems.Add(oControlItem)
                End If
            Next
            LoadGrid()
        End If

    End Sub

    Private Sub LoadGrid()
        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
            End With

            .AutoGenerateColumns = False
            .Columns.Clear()

            .DataSource = _ControlItems
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = True
            .AllowUserToResizeRows = False
            .AllowDrop = False
            .ReadOnly = True

            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Fch)
                .HeaderText = "Data"
                .DataPropertyName = "FchCreated"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 100
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.FirstName)
                .HeaderText = "Nom"
                .DataPropertyName = "FirstName"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.LastName)
                .HeaderText = "Cognoms"
                .DataPropertyName = "LastName"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.RaoSocial)
                .HeaderText = "Rao Social"
                .DataPropertyName = "RaoSocial"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Location)
                .HeaderText = "Població"
                .DataPropertyName = "Location"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
        End With
        SetContextMenu()
        _AllowEvents = True
    End Sub

    Private Function SelectedControlItems() As ControlItems
        Dim retval As New ControlItems
        For Each oRow As DataGridViewRow In DataGridView1.SelectedRows
            Dim oControlItem As ControlItem = oRow.DataBoundItem
            retval.Add(oControlItem)
        Next

        If retval.Count = 0 Then retval.Add(CurrentControlItem)
        Return retval
    End Function

    Private Function SelectedItems() As List(Of DTOFairGuest)
        Dim retval As New List(Of DTOFairGuest)
        For Each oRow As DataGridViewRow In DataGridView1.SelectedRows
            Dim oControlItem As ControlItem = oRow.DataBoundItem
            retval.Add(oControlItem.Source)
        Next

        If retval.Count = 0 Then retval.Add(CurrentControlItem.Source)
        Return retval
    End Function

    Private Function CurrentControlItem() As ControlItem
        Dim retval As ControlItem = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            retval = oRow.DataBoundItem
        End If
        Return retval
    End Function

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oControlItem As ControlItem = CurrentControlItem()

        If oControlItem IsNot Nothing Then
            Dim oMenu_FairGuest As New Menu_FairGuest(SelectedItems.First)
            AddHandler oMenu_FairGuest.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_FairGuest.Range)
            oContextMenu.Items.Add("-")
        End If
        oContextMenu.Items.Add("afegir", Nothing, AddressOf Do_AddNew)
        oContextMenu.Items.Add("refresca", Nothing, AddressOf RefreshRequest)
        oContextMenu.Items.Add("Excel", Nothing, AddressOf Do_Excel)

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Do_AddNew()
        RaiseEvent RequestToAddNew(Me, MatEventArgs.Empty)
    End Sub

    Private Sub Do_Excel()
        Dim oExcelSheet As DTOExcelSheet = BLL.BLLFairGuests.Excel(_values)
        UIHelper.ShowExcel(oExcelSheet)
    End Sub

    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles DataGridView1.DoubleClick

        Dim oSelectedValue As DTOFairGuest = CurrentControlItem.Source
        Dim oFrm As New Frm_FairGuest(oSelectedValue)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView1.SelectionChanged
        If _AllowEvents Then
            RaiseEvent ValueChanged(Me, New MatEventArgs(CurrentControlItem.Source))
            SetContextMenu()
        End If
    End Sub

    Private Sub RefreshRequest()
        RaiseEvent RequestToRefresh(Me, MatEventArgs.Empty)
    End Sub

    Protected Class ControlItem
        Property Source As DTOFairGuest

        Property FirstName As String
        Property LastName As String
        Property RaoSocial As String
        Property Location As String
        Property FchCreated As Date

        Public Sub New(value As DTOFairGuest)
            MyBase.New()
            _Source = value

            With value
                _FchCreated = .FchCreated
                _FirstName = .FirstName
                _LastName = .LastName
                _RaoSocial = .RaoSocial
                _Location = .Location
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class

