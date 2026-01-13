Public Class Xl_CustomerTarifaDtos
    Private _values As List(Of DTOCustomerTarifaDto)
    Private _SelectionMode As DTO.Defaults.SelectionModes
    Private _ControlItems As ControlItems
    Private _HideSrc As Boolean
    Private _AllowEvents As Boolean

    Private _Iva As DTOTax

    Public Event RequestToRefresh(sender As Object, e As MatEventArgs)
    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)
    Public Event AfterUpdate(sender As Object, e As MatEventArgs)
    Public Event onItemSelected(sender As Object, e As MatEventArgs)

    Private Enum Cols
        Fch
        DtoBrut
        DtoNet
        Product
        Src
    End Enum

    Public Shadows Sub Load(values As List(Of DTOCustomerTarifaDto), Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse, Optional HideSrc As Boolean = False)
        _SelectionMode = oSelectionMode
        _values = values
        _Iva = DTOTax.Closest(DTOTax.Codis.Iva_Standard, Today)
        _HideSrc = HideSrc

        Static PropertiesSet As Boolean
        If Not PropertiesSet Then
            SetProperties()
            PropertiesSet = True
        End If
        Refresca()
    End Sub

    Private Sub Refresca()
        _AllowEvents = False
        _ControlItems = New ControlItems
        For Each oItem As DTOCustomerTarifaDto In _values
            Dim oControlItem As New ControlItem(oItem, _Iva)
            _ControlItems.Add(oControlItem)
        Next

        DataGridView1.DataSource = _ControlItems
        DataGridView1.ClearSelection()
        SetContextMenu()
        _AllowEvents = True
    End Sub


    Public ReadOnly Property Value As DTOCustomerTarifaDto
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As DTOCustomerTarifaDto = oControlItem.Source
            Return retval
        End Get
    End Property


    Private Sub SetProperties()
        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
            End With

            .AutoGenerateColumns = False
            .Columns.Clear()


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
                .DataPropertyName = "Fch"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 100
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                .DefaultCellStyle.Format = "dd/MM/yy hh:mm:ss"
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.DtoBrut)
                .HeaderText = "Dto"
                .DataPropertyName = "DtoBrut"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#.00\%;-#.00\%;#"
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.DtoNet)
                .HeaderText = "Dto"
                .DataPropertyName = "DtoNet"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#.00\%;-#.00\%;#"
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Product)
                .HeaderText = "Producte"
                .DataPropertyName = "Product"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Src)
                .HeaderText = "Font"
                .DataPropertyName = "Src"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                If _HideSrc Then .Visible = False
            End With

        End With
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

    Private Function SelectedItems() As List(Of DTOCustomerTarifaDto)
        Dim retval As New List(Of DTOCustomerTarifaDto)
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
            Dim oMenu_CustomerDto As New Menu_CustomerDto(SelectedItems.First)
            AddHandler oMenu_CustomerDto.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_CustomerDto.Range)
            oContextMenu.Items.Add("-")
            oContextMenu.Items.Add("retirar", Nothing, AddressOf Do_Remove)
        End If
        oContextMenu.Items.Add("afegir", Nothing, AddressOf Do_AddNew)

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Do_AddNew()
        RaiseEvent RequestToAddNew(Me, MatEventArgs.Empty)
    End Sub

    Private Async Sub Do_Remove()
        Dim oControlItem As ControlItem = CurrentControlItem()
        Dim oDto As DTOCustomerTarifaDto = oControlItem.Source
        Dim exs As New List(Of Exception)
        If Await FEB2.CustomerTarifaDto.Delete(oDto, exs) Then
            RaiseEvent RequestToRefresh(Me, EventArgs.Empty)
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub


    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles DataGridView1.DoubleClick

        Select Case _SelectionMode
            Case DTO.Defaults.SelectionModes.Selection
                Dim oControlItem As ControlItem = CurrentControlItem()
                If oControlItem IsNot Nothing Then
                    Dim oCustomerDto As DTOCustomerTarifaDto = SelectedItems.First
                    RaiseEvent onItemSelected(Me, New MatEventArgs(oCustomerDto))
                End If
            Case Else
                Dim oSelectedValue As DTOCustomerTarifaDto = CurrentControlItem.Source
                Dim oFrm As New Frm_CustomerDto(oSelectedValue)
                AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                oFrm.Show()
        End Select
    End Sub

    Private Sub DataGridView1_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles DataGridView1.RowPrePaint
        Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
        Dim oControlItem As ControlItem = oRow.DataBoundItem
        If IsActive(oControlItem) Then
        Else
            oRow.DefaultCellStyle.BackColor = Color.LightGray
        End If
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView1.SelectionChanged
        If _AllowEvents Then
            SetContextMenu()
        End If
    End Sub

    Private Function IsActive(oControl As ControlItem) As Boolean
        Dim oDto As DTOCustomerTarifaDto = oControl.Source
        Dim oList As List(Of ControlItem) = _ControlItems.ToList

        Dim outdated As Boolean
        If oDto.Product Is Nothing Then
            outdated = oList.Exists(Function(x) x.fch > oDto.Fch And x.Source.Product Is Nothing)
        Else
            outdated = oList.Exists(Function(x) x.fch > oDto.Fch And (x.Source.Product Is Nothing Or oDto.Product.Equals(x.Source.Product)))
        End If
        Dim retval As Boolean = Not outdated
        Return retval
    End Function

    Private Sub RefreshRequest()
        RaiseEvent RequestToRefresh(Me, MatEventArgs.Empty)
    End Sub

    Protected Class ControlItem
        Property Source As DTOCustomerTarifaDto

        Property fch As Date
        Property DtoBrut As Decimal
        Property DtoNet As Decimal
        Property Product As String
        Property Src As String

        Public Sub New(value As DTOCustomerTarifaDto, oIva As DTOTax)
            MyBase.New()
            _Source = value
            With value
                _fch = .Fch
                _DtoBrut = .Dto
                _DtoNet = 100 - (100 + oIva.Tipus) * (100 - .Dto) / 100
                If .Product Is Nothing Then
                    _Product = "(totes les marques)"
                Else
                    _Product = .Product.FullNom()
                End If
                _Src = .Src.ToString
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class

