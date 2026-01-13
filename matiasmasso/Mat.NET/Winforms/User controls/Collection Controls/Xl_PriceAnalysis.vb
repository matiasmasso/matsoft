Public Class Xl_PriceAnalysis
    Inherits _Xl_ReadOnlyDatagridview

    Private _Customer As DTOCustomer
    Private _Sku As DTOProductSku
    Private _Fch As Date

    Private _PropertiesSet As Boolean
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Private Enum Cols
        Caption
        Value
    End Enum

    Public Shadows Async Sub Load(oCustomer As DTOCustomer, oSku As DTOProductSku, Optional DtFch As Date = Nothing)
        _Customer = oCustomer
        _Sku = oSku
        _Fch = IIf(DtFch = Nothing, Today, DtFch)

        If Not _PropertiesSet Then
            SetProperties()
            _PropertiesSet = True
        End If

        Await Refresca()
    End Sub

    Private Async Function Refresca() As Task
        _AllowEvents = False
        _ControlItems = New ControlItems

        Dim exs As New List(Of Exception)
        Dim item As DTOPricelistItemCustomer = Nothing
        Dim sCaption As String = ""
        Dim sValue As String = ""
        Dim oTag As Object = Nothing
        Dim oControlitem As ControlItem = Nothing

        Dim oCustomCosts = Await FEB2.PriceListItemsCustomer.Active(exs, _Customer, _Fch)
        Dim oCustomCost As DTOPricelistItemCustomer = oCustomCosts.FirstOrDefault(Function(x) x.Sku.Equals(_Sku))
        If oCustomCost Is Nothing Then
            item = Await FEB2.PriceListItemCustomer.Search(exs, _Sku, _Fch)
            If exs.Count = 0 Then
                sCaption = String.Format("PVP (tarifa {0} del {1:dd/MM/yy})", item.Parent.Concepte, item.Parent.Fch)
                sValue = DTOAmt.CurFormatted(item.Retail)
                oTag = item
                oControlitem = New ControlItem(sCaption, sValue, oTag)
                _ControlItems.Add(oControlitem)

                Dim oDtos = Await FEB2.CustomerTarifaDtos.Active(exs, _Customer, _Fch)
                Dim oDto As DTOCustomerTarifaDto = DTOProductSku.GetCustomerDto(oDtos, _Sku)
                If oDto Is Nothing Then
                    sCaption = "sense descompte registrat sobre PVP"
                    sValue = ""
                    oTag = oDtos
                    oControlitem = New ControlItem(sCaption, sValue, oTag)
                    _ControlItems.Add(oControlitem)
                Else
                    sCaption = "descompte sobre PVP Iva inclós"
                    sValue = String.Format("{0}%", oDto.dto)
                    oTag = oDtos
                    oControlitem = New ControlItem(sCaption, sValue, oTag)
                    _ControlItems.Add(oControlitem)

                    sCaption = "tarifa de cost"
                    sValue = DTOAmt.CurFormatted(DTOAmt.Factory(item.Retail.Eur * (100 - oDto.Dto) / 100))
                    oTag = Nothing
                    oControlitem = New ControlItem(sCaption, sValue, oTag)
                    _ControlItems.Add(oControlitem)
                End If
            Else
                UIHelper.WarnError(exs)
            End If

        Else
            sCaption = "tarifa de cost net {0} del {1:dd/MM/yy}"
            sValue = DTOAmt.CurFormatted(oCustomCost.Retail)
            oTag = oCustomCost
            oControlitem = New ControlItem(sCaption, sValue, oTag)
            _ControlItems.Add(oControlitem)
        End If

        Dim oCliProductDtos As List(Of DTOCliProductDto) = Nothing
        If _Customer.GlobalDto = 0 Then
            oCliProductDtos = Await FEB2.CliProductDtos.All(_Customer, exs)
            If exs.Count = 0 Then
                Dim oCliProductDto As DTOCliProductDto = _Sku.CliProductDto(oCliProductDtos)
                If oCliProductDto IsNot Nothing Then
                    sCaption = "descompte especific del producte"
                    sValue = String.Format("{0}%", oCliProductDTO)
                    oTag = oCliProductDtos
                    oControlitem = New ControlItem(sCaption, sValue, oTag)
                    _ControlItems.Add(oControlitem)
                End If
            Else
                UIHelper.WarnError(exs)
            End If
        Else
            sCaption = "descompte global"
            sValue = String.Format("{0}%", _Customer.GlobalDto)
            oTag = oCliProductDtos
            oControlitem = New ControlItem(sCaption, sValue, oTag)
            _ControlItems.Add(oControlitem)
        End If


        MyBase.DataSource = _ControlItems
        MyBase.ClearSelection()
        _AllowEvents = True
    End Function


    Public ReadOnly Property Value As DTOTemplate
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As DTOTemplate = oControlItem.Source
            Return retval
        End Get
    End Property

    Private Sub SetProperties()
        MyBase.RowTemplate.Height = MyBase.Font.Height * 1.3
        'MyBase.RowTemplate.DefaultCellStyle.BackColor = Color.Transparent

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
        With MyBase.Columns(Cols.Caption)
            .HeaderText = "Concepte"
            .DataPropertyName = "Caption"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Value)
            .HeaderText = "Valor"
            .DataPropertyName = "Value"
            .Width = 90
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
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

    Private Function SelectedItems() As List(Of DTOTemplate)
        Dim retval As New List(Of DTOTemplate)
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
            Dim oTag = oControlItem.Source.tag
            If TypeOf oTag Is DTOPricelistItemCustomer Then
                Dim oMenu As New Menu_PricelistItemCustomer(oTag)
                AddHandler oMenu.AfterUpdate, AddressOf RefreshRequest
                oContextMenu.Items.AddRange(oMenu.Range)
            ElseIf TypeOf oTag Is list(Of DTOCliProductDto) Or TypeOf oTag Is List(Of DTOCustomerTarifaDto) Then
                oContextMenu.Items.Add("fitxa de descomptes", Nothing, AddressOf Do_ShowDtos)
            End If
        End If

        MyBase.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Do_ShowDtos()
        Dim oFrm As New Frm_CustomerTarifa(_Customer, Frm_CustomerTarifa.Tabs.Dto)
        oFrm.Show()
    End Sub

    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles MyBase.DoubleClick
        Dim oControlItem As ControlItem = CurrentControlItem()
        If oControlItem IsNot Nothing Then
            Dim oTag = oControlItem.Source
            If TypeOf oTag Is DTOPricelistItemCustomer Then
                Dim oFrm As New Frm_PricelistItemCustomer(oTag)
                oFrm.Show()
            ElseIf TypeOf oTag Is List(Of DTOCliProductDto) Or TypeOf oTag Is List(Of DTOCustomerTarifaDto) Then
                Do_ShowDtos()
            End If
        End If
    End Sub


    Protected Class ControlItem
        Property Source As Object

        Property Caption As String
        Property Value As String

        Public Sub New(sCaption As String, sValue As String, Optional oTag As Object = Nothing)
            MyBase.New()
            _Source = oTag
            With Value
                _Caption = sCaption
                _Value = sValue
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class

