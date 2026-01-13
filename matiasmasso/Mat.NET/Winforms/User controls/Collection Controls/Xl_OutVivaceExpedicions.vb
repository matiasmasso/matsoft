Public Class Xl_OutVivaceExpedicions
    Inherits _Xl_ReadOnlyDatagridview

    Private _Values As List(Of DTOOutVivaceLog.expedicion)
    Private _DefaultValue As DTOOutVivaceLog.expedicion
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse

    Private _Filter As String
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)
    Public Event onItemSelected(sender As Object, e As MatEventArgs)

    Private Enum Cols
        id
        fch
        client
        valor
        cost
        bultos
        m3
        kg
        transportista
    End Enum

    Public Shadows Sub Load(values As List(Of DTOOutVivaceLog.expedicion), Optional oDefaultValue As DTOOutVivaceLog.expedicion = Nothing, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse)
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
        Dim oFilteredValues As List(Of DTOOutVivaceLog.expedicion) = FilteredValues()
        _ControlItems = New ControlItems
        For Each oItem As DTOOutVivaceLog.expedicion In oFilteredValues
            Dim oControlItem As New ControlItem(oItem)
            _ControlItems.Add(oControlItem)
        Next

        Dim oCell As DTODatagridviewCell = UIHelper.DataGridviewCurrentCell(Me)
        MyBase.DataSource = _ControlItems
        UIHelper.SetDataGridviewCurrentCell(Me, oCell)

        SetContextMenu()
        _AllowEvents = True
    End Sub

    Private Function FilteredValues() As List(Of DTOOutVivaceLog.expedicion)
        Dim retval As List(Of DTOOutVivaceLog.expedicion)
        If _Filter = "" Then
            retval = _Values
        Else
            retval = New List(Of DTOOutVivaceLog.expedicion)
            ' retval = _Values.FindAll(Function(x) x.Nom.ToLower.Contains(_Filter.ToLower))
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

    Public ReadOnly Property Value As DTOOutVivaceLog.expedicion
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As DTOOutVivaceLog.expedicion = oControlItem.Source
            Return retval
        End Get
    End Property

    Private Sub SetProperties()
        MyBase.RowTemplate.Height = MyBase.Font.Height * 1.3
        'MyBase.RowOutVivaceLog.expedicion.DefaultCellStyle.BackColor = Color.Transparent

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
        With MyBase.Columns(Cols.id)
            .HeaderText = "Expedició"
            .DataPropertyName = "Id"
            .Width = 90
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.fch)
            .HeaderText = "Data"
            .DataPropertyName = "Fch"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 60
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            .DefaultCellStyle.Format = "dd/MM/yy"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.client)
            .HeaderText = "Destinatari"
            .DataPropertyName = "Client"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.valor)
            .HeaderText = "Valor"
            .DataPropertyName = "Valor"
            .Width = 70
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.cost)
            .HeaderText = "Cost"
            .DataPropertyName = "Cost"
            .Width = 70
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.bultos)
            .HeaderText = "Bultos"
            .DataPropertyName = "Bultos"
            .Width = 60
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.m3)
            .HeaderText = "Volum"
            .DataPropertyName = "m3"
            .Width = 60
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "0.0000 m3;-0.0000 m3;#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.kg)
            .HeaderText = "Pes"
            .DataPropertyName = "Kg"
            .Width = 60
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,### Kg;-#,### Kg;#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.transportista)
            .HeaderText = "Transportista"
            .DataPropertyName = "transportista"
            .Width = 100
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
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

    Private Function SelectedItems() As List(Of DTOOutVivaceLog.expedicion)
        Dim retval As New List(Of DTOOutVivaceLog.expedicion)
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
            Dim oMenu_OutVivaceExpedicion As New Menu_OutVivaceExpedicion(SelectedItems.First)
            AddHandler oMenu_OutVivaceExpedicion.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_OutVivaceExpedicion.Range)
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
            Dim oSelectedValue As DTOOutVivaceLog.expedicion = CurrentControlItem.Source
            Dim oFrm As New Frm_OutVivaceExpedicio(oSelectedValue)
            oFrm.Show()
        End If
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles MyBase.SelectionChanged
        If _AllowEvents Then
            RaiseEvent ValueChanged(Me, New MatEventArgs(CurrentControlItem.Source))
            SetContextMenu()
        End If
    End Sub



    Protected Class ControlItem
        Property Source As DTOOutVivaceLog.expedicion

        Property id As String
        Property fch As Date
        Property client As String
        Property valor As Decimal
        Property cost As Decimal
        Property bultos As Integer
        Property m3 As Decimal
        Property kg As Integer
        Property transportista As String


        Public Sub New(value As DTOOutVivaceLog.expedicion)
            MyBase.New()
            _Source = value
            With value
                _id = .vivace
                If .albaranes.Count > 0 Then
                    _fch = .albaranes.Last.delivery.Fch
                    _client = .albaranes.Last.delivery.Nom
                    _valor = .albaranes.Sum(Function(x) x.delivery.Import.Eur)
                    Dim DcCostXExp As Decimal = .cargos.Sum(Function(y) y.unidades * y.precio)
                    Dim DcCostXAlb As Decimal = .albaranes.Sum(Function(x) x.cargos.Sum(Function(y) y.unidades * y.precio))
                    _cost = DcCostXExp + DcCostXAlb
                End If
                _bultos = .bultos
                _m3 = .m3
                _kg = .kg
                If .Trp Is Nothing Then
                    _transportista = .transpnif
                Else
                    _transportista = .Trp.Nom
                End If
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class


