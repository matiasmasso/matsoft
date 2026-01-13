Imports MatHelperStd

Public Class Xl_PremiumCustomers

    Inherits _Xl_ReadOnlyDatagridview

    Private _Values As List(Of DTOPremiumCustomer)
    Private _DefaultValue As DTOPremiumCustomer
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse

    Private _Filter As String
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)
    Public Event onItemSelected(sender As Object, e As MatEventArgs)

    Private Enum Cols
        ico
        Country
        Zona
        Location
        Nom
        pdf
        FchLastEdited
    End Enum

    Public Shadows Sub Load(values As List(Of DTOPremiumCustomer), Optional oDefaultValue As DTOPremiumCustomer = Nothing, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse)
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
        Dim oFilteredValues As List(Of DTOPremiumCustomer) = FilteredValues()
        _ControlItems = New ControlItems
        For Each oItem As DTOPremiumCustomer In oFilteredValues
            Dim oControlItem As New ControlItem(oItem)
            _ControlItems.Add(oControlItem)
        Next

        Dim oCell As DTODatagridviewCell = UIHelper.DataGridviewCurrentCell(Me)
        MyBase.DataSource = _ControlItems
        UIHelper.SetDataGridviewCurrentCell(Me, oCell)

        SetContextMenu()
        _AllowEvents = True
    End Sub

    Private Function FilteredValues() As List(Of DTOPremiumCustomer)
        Dim retval As List(Of DTOPremiumCustomer)
        If _Filter = "" Then
            retval = _Values
        Else
            retval = _Values.FindAll(Function(x) x.Customer.NomAndNomComercial().ToLower.Contains(_Filter.ToLower))
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

    Public ReadOnly Property Value As DTOPremiumCustomer
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As DTOPremiumCustomer = oControlItem.Source
            Return retval
        End Get
    End Property

    Private Sub SetProperties()
        MyBase.RowTemplate.Height = MyBase.Font.Height * 1.3
        'MyBase.RowPremiumCustomer.DefaultCellStyle.BackColor = Color.Transparent

        MyBase.AutoGenerateColumns = False
        MyBase.Columns.Clear()

        MyBase.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        MyBase.ColumnHeadersVisible = False
        MyBase.RowHeadersVisible = False
        MyBase.MultiSelect = True
        MyBase.AllowUserToResizeRows = False
        MyBase.AllowDrop = False
        MyBase.ReadOnly = True

        MyBase.Columns.Add(New DataGridViewImageColumn(False))
        With DirectCast(MyBase.Columns(Cols.ico), DataGridViewImageColumn)
            .CellTemplate = New DataGridViewImageCellBlank(False)
            .HeaderText = ""
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 16
            .DefaultCellStyle.NullValue = Nothing
            .ReadOnly = True
        End With


        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Country)
            .HeaderText = "Pais"
            .DataPropertyName = "Country"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 16
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Zona)
            .HeaderText = "Zona"
            .DataPropertyName = "Zona"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 40
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Location)
            .HeaderText = "Poblacion"
            .DataPropertyName = "Location"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 40
        End With


        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Nom)
            .HeaderText = "Nom"
            .DataPropertyName = "Nom"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End With

        MyBase.Columns.Add(New DataGridViewImageColumn(False))
        With DirectCast(MyBase.Columns(Cols.pdf), DataGridViewImageColumn)
            .CellTemplate = New DataGridViewImageCellBlank(False)
            .HeaderText = ""
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 16
            .DefaultCellStyle.NullValue = Nothing
            .ReadOnly = True
        End With


        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.FchLastEdited)
            .HeaderText = "Data"
            .DataPropertyName = "Fch"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 80
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            .DefaultCellStyle.Format = "dd/MM/yy"
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

    Private Function SelectedItems() As List(Of DTOPremiumCustomer)
        Dim retval As New List(Of DTOPremiumCustomer)
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
            Dim oMenu_PremiumCustomer As New Menu_PremiumCustomer(SelectedItems.First)
            AddHandler oMenu_PremiumCustomer.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_PremiumCustomer.Range)
            oContextMenu.Items.Add("-")
        End If
        oContextMenu.Items.Add("Excel", Nothing, AddressOf Do_Excel)
        oContextMenu.Items.Add("afegir", Nothing, AddressOf Do_AddNew)

        MyBase.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Do_AddNew()
        RaiseEvent RequestToAddNew(Me, MatEventArgs.Empty)
    End Sub

    Private Sub Do_Excel()
        Dim oBook As New ExcelHelper.Book("Premium Line")
        Dim included = _Values.Where(Function(x) x.Codi = DTOPremiumCustomer.Codis.Included).ToList
        Dim excluded = _Values.Where(Function(x) x.Codi = DTOPremiumCustomer.Codis.Excluded).ToList
        oBook.Sheets.Add(ExcelSheet(included, "Incluidos"))
        oBook.Sheets.Add(ExcelSheet(excluded, "Excluidos"))

        Dim exs As New List(Of Exception)
        If Not UIHelper.ShowExcel(oBook, exs) Then
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Function ExcelSheet(oValues As List(Of DTOPremiumCustomer), sName As String) As ExcelHelper.Sheet
        Dim retval As New ExcelHelper.Sheet(sName)
        With retval
            .AddColumn("pais", ExcelHelper.Sheet.NumberFormats.W50)
            .AddColumn("zona", ExcelHelper.Sheet.NumberFormats.W50)
            .AddColumn("población", ExcelHelper.Sheet.NumberFormats.W50)
            .AddColumn("cliente", ExcelHelper.Sheet.NumberFormats.W50)
            .AddColumn("dirección", ExcelHelper.Sheet.NumberFormats.W50)
        End With

        For Each oValue In oValues
            Dim oRow As ExcelHelper.Row = retval.AddRow
            With oValue
                oRow.AddCell()
                oRow.AddCell(DTOAddress.Country(.Customer.address).ISO)
                oRow.AddCell(DTOAddress.Zona(.Customer.address).nom)
                oRow.AddCell(DTOAddress.Location(.Customer.address).nom)
                oRow.AddCell(.Customer.NomAndNomComercial())
                oRow.AddCell(.Customer.Address.Text)
            End With
        Next

        Return retval
    End Function


    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles MyBase.DoubleClick
        Dim oCurrentControlItem As ControlItem = CurrentControlItem()
        If oCurrentControlItem IsNot Nothing Then
            Dim oSelectedValue As DTOPremiumCustomer = CurrentControlItem.Source
            Select Case _SelectionMode
                Case DTO.Defaults.SelectionModes.Browse
                    Dim oFrm As New Frm_PremiumCustomer(oSelectedValue)
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

    Private Sub Xl_PremiumCustomers_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles Me.CellFormatting
        Select Case e.ColumnIndex
            Case Cols.ico
                Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
                Dim oControlItem As ControlItem = oRow.DataBoundItem
                Select Case oControlItem.Source.Codi
                    Case DTOPremiumCustomer.Codis.Included
                        e.Value = My.Resources.vb
                    Case DTOPremiumCustomer.Codis.Excluded
                        e.Value = My.Resources.aspa
                End Select
            Case Cols.pdf
                Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
                Dim oControlItem As ControlItem = oRow.DataBoundItem
                If oControlItem.Source.DocFile IsNot Nothing Then
                    e.Value = My.Resources.pdf
                End If
        End Select
    End Sub

    Protected Class ControlItem
        Property Source As DTOPremiumCustomer

        Property Country As String
        Property Zona As String
        Property Location As String
        Property Nom As String
        Property Fch As String

        Public Sub New(value As DTOPremiumCustomer)
            MyBase.New()
            _Source = value
            With value
                _Country = DTOAddress.Country(.Customer.address).ISO
                _Zona = DTOAddress.Zona(.Customer.address).nom
                _Location = DTOAddress.Location(.Customer.address).nom
                _Nom = .Customer.NomAndNomComercial()
                _Fch = .UsrLog.fchLastEdited.Date
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class


