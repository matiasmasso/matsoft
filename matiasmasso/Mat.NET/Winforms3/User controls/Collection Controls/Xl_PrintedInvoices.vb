Public Class Xl_PrintedInvoices

    Inherits _Xl_ReadOnlyDatagridview

    Private _Values As List(Of DTOInvoice)
    Private _DefaultValue As DTOInvoice
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse

    Private _Filter As String
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)
    Public Event onItemSelected(sender As Object, e As MatEventArgs)

    Private Enum Cols
        Id
        Fch
        CliNom
        Ico
        FchLastPrinted
        User
    End Enum

    Public Shadows Sub Load(values As List(Of DTOInvoice), Optional oDefaultValue As DTOInvoice = Nothing, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse)
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
        Dim oFilteredValues As List(Of DTOInvoice) = FilteredValues()
        _ControlItems = New ControlItems
        For Each oItem As DTOInvoice In oFilteredValues
            Dim oControlItem As New ControlItem(oItem)
            _ControlItems.Add(oControlItem)
        Next

        Dim oCell As DTODatagridviewCell = UIHelper.DataGridviewCurrentCell(Me)
        MyBase.DataSource = _ControlItems
        UIHelper.SetDataGridviewCurrentCell(Me, oCell)

        SetContextMenu()
        _AllowEvents = True
    End Sub


    Public ReadOnly Property Value As DTOInvoice
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As DTOInvoice = oControlItem.Source
            Return retval
        End Get
    End Property

    Private Function FilteredValues() As List(Of DTOInvoice)
        Dim retval As List(Of DTOInvoice)
        If _Filter = "" Then
            retval = _Values
        Else
            retval = _Values.FindAll(Function(x) x.Customer.FullNom.ToLower.Contains(_Filter.ToLower) Or x.Num.ToString = _Filter)
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

    Private Sub SetProperties()
        MyBase.RowTemplate.Height = MyBase.Font.Height * 1.3
        'MyBase.RowPrintedInvoice.DefaultCellStyle.BackColor = Color.Transparent

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
        With MyBase.Columns(Cols.Id)
            .HeaderText = "Numero"
            .DataPropertyName = "Id"
            .Width = 60
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Fch)
            .HeaderText = "Data"
            .DataPropertyName = "Fch"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 60
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            .DefaultCellStyle.Format = "dd/MM/yy"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.CliNom)
            .HeaderText = "Nom"
            .DataPropertyName = "CliNom"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End With
        MyBase.Columns.Add(New DataGridViewImageColumn(False))
        With DirectCast(MyBase.Columns(Cols.Ico), DataGridViewImageColumn)
            .CellTemplate = New DataGridViewImageCellBlank(False)
            .HeaderText = ""
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 16
            .DefaultCellStyle.NullValue = Nothing
            .ReadOnly = True
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.FchLastPrinted)
            .HeaderText = "Enviament"
            .DataPropertyName = "FchLastPrinted"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 90
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            '.DefaultCellStyle.Format = "dd/MM/yy HH:mm"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.User)
            .HeaderText = "Usuari"
            .DataPropertyName = "user"
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

    Private Function SelectedItems() As List(Of DTOInvoice)
        Dim retval As New List(Of DTOInvoice)
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
            Dim oMenu_Fra As New Menu_Invoice(SelectedItems)
            AddHandler oMenu_Fra.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_Fra.Range)
        End If

        MyBase.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Do_AddNew()
        RaiseEvent RequestToAddNew(Me, MatEventArgs.Empty)
    End Sub


    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles MyBase.DoubleClick
        Dim oCurrentControlItem As ControlItem = CurrentControlItem()
        If oCurrentControlItem IsNot Nothing Then
            Dim oSelectedValue As DTOInvoice = CurrentControlItem.Source
            Select Case _SelectionMode
                Case DTO.Defaults.SelectionModes.Browse
                    'Dim oFrm As New Frm_PrintedInvoice(oSelectedValue)
                    'AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                    'oFrm.Show()
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

    Private Sub Xl_PrintedInvoices_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles Me.CellFormatting
        Select Case e.ColumnIndex
            Case Cols.Ico
                Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
                Dim oControlItem As ControlItem = oRow.DataBoundItem
                If oControlItem IsNot Nothing Then
                    e.Value = IconHelper.PrintModeIcon(oControlItem.PrintMode)
                End If
        End Select
    End Sub

    Protected Class ControlItem
        Property Source As DTOInvoice

        Property Fch As DateTime
        Property FchLastPrinted As Nullable(Of DateTime)
        Property Id As Integer
        Property CliNom As String
        Property User As String
        Property PrintMode As DTOInvoice.PrintModes

        Public Sub New(value As DTOInvoice)
            MyBase.New()
            _Source = value
            With value
                _Id = .Num
                _Fch = .Fch
                _CliNom = .Customer.FullNom
                _PrintMode = .PrintMode
                _FchLastPrinted = .FchLastPrinted
                _User = DTOUser.NicknameOrElse(.UserLastPrinted)
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class


