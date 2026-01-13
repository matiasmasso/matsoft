Public Class Xl_Ibans

    Inherits _Xl_ReadOnlyDatagridview

    Private _Values As List(Of DTOIban)
    Private _DefaultValue As DTOIban
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse

    Private _Filter As String
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)
    Public Event onItemSelected(sender As Object, e As MatEventArgs)

    Private _MenuItem_ShowApproved As ToolStripMenuItem
    Private _MenuItem_ShowCaducats As ToolStripMenuItem
    Private _PropertiesSet As Boolean

    Private Enum Cols
        ico
        FchFrom
        Titular
        Warn
        Iban
        FchDownloaded
        FchUploaded
        fchApproved
    End Enum

    Public Shadows Sub Load(values As List(Of DTOIban), Optional oDefaultValue As DTOIban = Nothing, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.browse)
        If Not _PropertiesSet Then
            SetProperties()
            _PropertiesSet = True
        End If

        _Values = values
        _SelectionMode = oSelectionMode
        Refresca()
    End Sub

    Private Sub Refresca()
        _AllowEvents = False
        Dim oFilteredValues As List(Of DTOIban) = FilteredValues()
        _ControlItems = New ControlItems

        If Not _MenuItem_ShowApproved.Checked Then
            oFilteredValues = oFilteredValues.Where(Function(x) x.FchApproved = Nothing).ToList
        End If

        If Not _MenuItem_ShowCaducats.Checked Then
            oFilteredValues = oFilteredValues.Where(Function(x) x.FchTo = Nothing).ToList
        End If

        For Each oItem As DTOIban In oFilteredValues
            Dim oControlItem As New ControlItem(oItem)
            _ControlItems.Add(oControlItem)
        Next

        Dim oCell As DTODatagridviewCell = UIHelper.DataGridviewCurrentCell(Me)
        MyBase.DataSource = _ControlItems
        UIHelper.SetDataGridviewCurrentCell(Me, oCell)

        SetContextMenu()
        _AllowEvents = True
    End Sub

    Private Function FilteredValues() As List(Of DTOIban)
        Dim retval As List(Of DTOIban)
        If _Filter = "" Then
            retval = _Values
        Else
            retval = _Values.FindAll(Function(x) x.Titular.FullNom.ToLower.Contains(_Filter.ToLower))
        End If
        Return retval
    End Function


    Public Property Filter As String
        Get
            Return _Filter
        End Get
        Set(value As String)
            _Filter = value
            'If _Values IsNot Nothing Then Refresca()
        End Set
    End Property


    Public ReadOnly Property Value As DTOIban
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As DTOIban = oControlItem.Source
            Return retval
        End Get
    End Property

    Private Sub SetProperties()
        MyBase.RowTemplate.Height = MyBase.Font.Height * 1.3
        'MyBase.RowIban.DefaultCellStyle.BackColor = Color.Transparent

        MyBase.AutoGenerateColumns = False
        MyBase.Columns.Clear()

        MyBase.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        MyBase.ColumnHeadersVisible = True
        MyBase.RowHeadersVisible = False
        MyBase.MultiSelect = True
        MyBase.AllowUserToResizeRows = False
        MyBase.AllowDrop = False
        MyBase.ReadOnly = True


        MyBase.Columns.Add(New DataGridViewImageColumn)
        With DirectCast(MyBase.Columns(Cols.ico), DataGridViewImageColumn)
            .DataPropertyName = "Ico"
            .HeaderText = ""
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 16
            .DefaultCellStyle.NullValue = Nothing
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.FchFrom)
            .HeaderText = "Des de"
            .DataPropertyName = "FchFrom"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 60
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            .DefaultCellStyle.Format = "dd/MM/yy"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Titular)
            .HeaderText = "Titular"
            .DataPropertyName = "Titular"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End With
        MyBase.Columns.Add(New DataGridViewImageColumn)
        With DirectCast(MyBase.Columns(Cols.Warn), DataGridViewImageColumn)
            .HeaderText = ""
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 16
            .DefaultCellStyle.NullValue = Nothing
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Iban)
            .HeaderText = "Iban"
            .DataPropertyName = "Iban"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 160
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.FchDownloaded)
            .HeaderText = "Baixat"
            .DataPropertyName = "FchDownloaded"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 60
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            .DefaultCellStyle.Format = "dd/MM/yy"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.FchUploaded)
            .HeaderText = "Pujat"
            .DataPropertyName = "FchUploaded"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 60
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            .DefaultCellStyle.Format = "dd/MM/yy"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.fchApproved)
            .HeaderText = "aprovat"
            .DataPropertyName = "fchApproved"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 60
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            .DefaultCellStyle.Format = "dd/MM/yy"
        End With

        _MenuItem_ShowApproved = New ToolStripMenuItem()
        AddHandler _MenuItem_ShowApproved.CheckedChanged, AddressOf Refresca
        With _MenuItem_ShowApproved
            .Text = "mostrar aprovats"
            .CheckOnClick = True
            .Checked = False
        End With

        _MenuItem_ShowCaducats = New ToolStripMenuItem()
        AddHandler _MenuItem_ShowCaducats.CheckedChanged, AddressOf Refresca
        With _MenuItem_ShowCaducats
            .Text = "mostrar caducats"
            .CheckOnClick = True
            .Checked = False
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

    Private Function SelectedItems() As List(Of DTOIban)
        Dim retval As New List(Of DTOIban)
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
            Dim oMenu_Iban As New Menu_Iban(SelectedItems.First)
            AddHandler oMenu_Iban.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_Iban.Range)
            oContextMenu.Items.Add("-")
        End If
        oContextMenu.Items.Add(_MenuItem_ShowApproved)
        oContextMenu.Items.Add(_MenuItem_ShowCaducats)
        oContextMenu.Items.Add("refresca", Nothing, AddressOf MyBase.RefreshRequest)

        MyBase.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Do_AddNew()
        RaiseEvent RequestToAddNew(Me, MatEventArgs.Empty)
    End Sub


    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles MyBase.DoubleClick
        Dim oCurrentControlItem As ControlItem = CurrentControlItem()
        If oCurrentControlItem IsNot Nothing Then
            Dim oSelectedValue As DTOIban = CurrentControlItem.Source
            Dim oFrm As New Frm_Iban(oSelectedValue)
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

    Private Sub Xl_Ibans_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles Me.CellFormatting
        Select Case e.ColumnIndex
            Case Cols.ico
                Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
                Dim oControlitem As ControlItem = oRow.DataBoundItem
                Dim oIban As DTOIban = oControlitem.Source
                If (oIban.FchTo > Nothing And oIban.FchTo < Today) Then
                    e.Value = My.Resources.aspa
                ElseIf oIban.FchApproved <> Nothing Then
                    e.Value = My.Resources.vb
                ElseIf oIban.DocFile IsNot Nothing Then
                    e.Value = My.Resources.pdf
                End If
            Case Cols.Warn
                Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
                Dim oControlitem As ControlItem = oRow.DataBoundItem
                Dim oIban As DTOIban = oControlitem.Source
                Dim exs As New List(Of DTOIban.Exceptions)
                If Not oIban.Validate(exs) Then
                    e.Value = My.Resources.warn
                End If
        End Select
    End Sub

    Protected Class ControlItem
        Property Source As DTOIban

        Property FchFrom As Nullable(Of Date)
        Property Titular As String
        Property Iban As String
        Property FchDownloaded As Nullable(Of Date)
        Property FchUploaded As Nullable(Of Date)
        Property FchApproved As Nullable(Of Date)



        Public Sub New(oIban As DTOIban)
            MyBase.New()
            _Source = oIban
            With oIban
                If .FchFrom > Date.MinValue Then
                    _FchFrom = .FchFrom
                End If
                _Titular = .Titular.FullNom
                _Iban = .Digits
                If .FchDownloaded > Date.MinValue Then
                    _FchDownloaded = .FchDownloaded
                End If
                If .FchUploaded > Date.MinValue Then
                    _FchUploaded = .FchUploaded
                End If
                If .FchApproved > Date.MinValue Then
                    _FchApproved = .FchApproved
                End If
            End With
        End Sub

    End Class



    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class


