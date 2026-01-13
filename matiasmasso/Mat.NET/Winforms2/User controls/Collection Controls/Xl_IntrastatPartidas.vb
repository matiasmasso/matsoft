Public Class Xl_IntrastatPartidas

    Inherits _Xl_ReadOnlyDatagridview

    Private _Intrastat As DTOIntrastat
    Private _Values As List(Of DTOIntrastat.Partida)

    Private _Filter As String
    Private _FilterWarnings As Boolean
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)
    Public Event onItemSelected(sender As Object, e As MatEventArgs)

    Private Enum Cols
        ico
        Fch
        Num
        Nom
        Country
        Incoterm
        CodiMercancia
        MadeIn
        Kg
        Amt
    End Enum

    Public Shadows Sub Load(oIntrastat As DTOIntrastat)
        _Intrastat = oIntrastat
        _Values = oIntrastat.Partidas

        'Static PropertiesSet As Boolean
        'If Not PropertiesSet Then
        SetProperties()
        'PropertiesSet = True
        'End If

        Refresca()
    End Sub

    Public ReadOnly Property Warn As Boolean
        Get
            Dim retval As Boolean = _ControlItems.Any(Function(x) x.Warn)
            Return retval
        End Get
    End Property

    Private Sub Refresca()
        _AllowEvents = False
        Dim oFilteredValues As List(Of DTOIntrastat.Partida) = FilteredValues()
        _ControlItems = New ControlItems
        _ControlItems.Add(ControlItem.Summary(oFilteredValues))
        For Each oItem As DTOIntrastat.Partida In oFilteredValues
            Dim oControlItem As New ControlItem(_Intrastat, oItem)
            _ControlItems.Add(oControlItem)
        Next

        Dim oCell As DTODatagridviewCell = UIHelper.DataGridviewCurrentCell(Me)
        MyBase.DataSource = _ControlItems
        UIHelper.SetDataGridviewCurrentCell(Me, oCell)


        SetContextMenu()
        _AllowEvents = True
    End Sub

    Private Function FilteredValues() As List(Of DTOIntrastat.Partida)
        Dim retval As New List(Of DTOIntrastat.Partida)
        If _Filter = "" Then
            retval = _Values
        Else
            Select Case _Intrastat.Flujo
                Case DTOIntrastat.Flujos.introduccion
                    retval = _Values 'toDo
                Case DTOIntrastat.Flujos.expedicion
                    retval = _Values.FindAll(Function(x) DirectCast(x.Tag, DTOInvoice).Customer.Nom.ToLower.Contains(_Filter.ToLower))
            End Select
        End If

        If _FilterWarnings Then
            retval = retval.Where(Function(x) DTOIntrastat.Partida.Warn(_Intrastat.Flujo, x.Kg, x.CodiMercancia, x.MadeIn))
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

    Public Sub FilterWarnings(value As Boolean)
        _FilterWarnings = value
    End Sub

    Public ReadOnly Property Value As DTOIntrastat.Partida
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As DTOIntrastat.Partida = oControlItem.Source
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
        With MyBase.Columns(Cols.Num)
            .HeaderText = IIf(_Intrastat.Flujo = DTOIntrastat.Flujos.Introduccion, "Entrada", "Factura")
            .DataPropertyName = "Num"
            .Width = 50
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Nom)
            .HeaderText = IIf(_Intrastat.Flujo = DTOIntrastat.Flujos.Introduccion, "Proveidor", "Client")
            .DataPropertyName = "Nom"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Country)
            .HeaderText = "Destí"
            .DataPropertyName = "Country"
            .Width = 30
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Incoterm)
            .HeaderText = "Incoterm"
            .DataPropertyName = "Incoterm"
            .Width = 40
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.CodiMercancia)
            .HeaderText = "Codi Mercancia"
            .DataPropertyName = "CodiMercancia"
            .Width = 70
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.MadeIn)
            .HeaderText = "Made In"
            .DataPropertyName = "MadeIn"
            .Width = 30
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Visible = _Intrastat.Flujo = DTOIntrastat.Flujos.Introduccion
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Kg)
            .HeaderText = "Pes Net"
            .DataPropertyName = "Kg"
            .Width = 80
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,##0.00 Kg;-#,##0.00 Kg;#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Amt)
            .HeaderText = "Import"
            .DataPropertyName = "Amt"
            .Width = 80
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
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

    Private Function SelectedItems() As List(Of DTOIntrastat.Partida)
        Dim retval As New List(Of DTOIntrastat.Partida)
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


        If oControlItem IsNot Nothing AndAlso oControlItem.LinCod = ControlItem.LinCods.item Then

            Dim oMenu As New Menu_IntrastatPartida(oControlItem.Source)
            AddHandler oMenu.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu.Range)

        End If

        MyBase.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Do_AddNew()
        RaiseEvent RequestToAddNew(Me, MatEventArgs.Empty)
    End Sub


    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles MyBase.DoubleClick
        Dim oCurrentControlItem As ControlItem = CurrentControlItem()
        If oCurrentControlItem IsNot Nothing Then
            Dim oSelectedValue As DTOIntrastat.Partida = CurrentControlItem.Source
            'Dim oFrm As New Frm_Template(oSelectedValue)
            'AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
            'oFrm.Show()

        End If
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles MyBase.SelectionChanged
        If _AllowEvents Then
            RaiseEvent ValueChanged(Me, New MatEventArgs(CurrentControlItem.Source))
            SetContextMenu()
        End If
    End Sub



    Private Sub MyBase_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles MyBase.CellFormatting
        Select Case e.ColumnIndex
            Case Cols.ico
                Dim oRow As DataGridViewRow = DirectCast(sender, DataGridView).Rows(e.RowIndex)
                Dim oControlItem As ControlItem = oRow.DataBoundItem
                If oControlItem.LinCod = ControlItem.LinCods.item Then
                    If oControlItem.Warn Then
                        e.Value = My.Resources.warn
                    End If
                End If
        End Select
    End Sub


    Protected Class ControlItem
        Property Source As DTOIntrastat.Partida

        Property Fch As Nullable(Of Date)
        Property Num As Integer
        Property Nom As String
        Property Country As String
        Property Incoterm As String
        Property CodiMercancia As String
        Property MadeIn As String
        Property Kg As Decimal
        Property Amt As Decimal
        Property LinCod As LinCods
        Property Warn As Boolean

        Public Enum LinCods
            item
            summary
        End Enum

        Private Sub New(values As List(Of DTOIntrastat.Partida))
            MyBase.New
            _LinCod = LinCods.summary
            _Nom = "totals"
            _Kg = values.Sum(Function(x) x.Kg)
            _Amt = values.Sum(Function(x) x.ImporteFacturado)
        End Sub

        Public Sub New(oIntrastat As DTOIntrastat, value As DTOIntrastat.Partida)
            MyBase.New()
            _Source = value
            _LinCod = LinCods.item
            With value
                If TypeOf value.Tag Is DTOInvoice Then
                    Dim oInvoice As DTOInvoice = value.Tag
                    _Fch = oInvoice.Fch
                    _Num = oInvoice.Num
                    _Nom = oInvoice.Customer.Nom

                ElseIf TypeOf value.Tag Is DTODelivery Then
                    Dim oDelivery As DTODelivery = value.Tag
                    _Fch = oDelivery.Fch
                    _Num = oDelivery.Id
                    _Nom = oDelivery.Contact.Nom
                End If
                _Country = .Country.ISO

                If .Incoterm IsNot Nothing Then
                    _Incoterm = .Incoterm.Id.ToString
                End If
                '_Incoterm =
                If .CodiMercancia IsNot Nothing Then
                    _CodiMercancia = .CodiMercancia.Id
                End If
                If .MadeIn IsNot Nothing Then
                    _MadeIn = .MadeIn.ISO
                End If

                'If _MadeIn < "0" Then Stop

                _Kg = .Kg
                _Amt = .ImporteFacturado
                _Warn = DTOIntrastat.Partida.Warn(oIntrastat.Flujo, _Kg, .CodiMercancia, .MadeIn)
            End With
        End Sub

        Shared Function Summary(values As List(Of DTOIntrastat.Partida)) As ControlItem
            Dim retval As New ControlItem(values)
            Return retval
        End Function

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class


