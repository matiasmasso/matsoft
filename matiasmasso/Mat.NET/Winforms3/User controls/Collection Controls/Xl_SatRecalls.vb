Public Class Xl_SatRecalls

    Inherits _Xl_ReadOnlyDatagridview

    Private _Values As List(Of DTOSatRecall)
    Private _DefaultValue As DTOSatRecall
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse

    Private _Filter As String
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)
    Public Event onItemSelected(sender As Object, e As MatEventArgs)

    Private Enum Cols
        ico
        incidencia
        cli
        sku
        PickupFrom
        FchCustomer
        FchManufacturer
        PickupRef
        CreditNum
    End Enum

    Public Shadows Sub Load(values As List(Of DTOSatRecall), Optional oDefaultValue As DTOSatRecall = Nothing, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse)
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
        Dim oFilteredValues As List(Of DTOSatRecall) = FilteredValues()
        _ControlItems = New ControlItems
        For Each oItem As DTOSatRecall In oFilteredValues
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
                MyBase.CurrentCell = MyBase.Rows(rowIdx).Cells(Cols.incidencia)
            End If
        End If

        SetContextMenu()
        _AllowEvents = True
    End Sub

    Public Function FilteredValues() As List(Of DTOSatRecall)
        Dim retval As List(Of DTOSatRecall)
        If _Filter = "" Then
            retval = _Values
        Else
            retval = _Values.FindAll(Function(x) x.Incidencia.Customer.FullNom.ToLower.Contains(_Filter.ToLower) _
                Or (x.Incidencia.Product IsNot Nothing AndAlso x.Incidencia.Product.Nom.Contains(_Filter.ToLower)) _
                Or x.Incidencia.AsinAndNum.ToString.Contains(_Filter))
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

    Public ReadOnly Property Value As DTOSatRecall
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As DTOSatRecall = oControlItem.Source
            Return retval
        End Get
    End Property

    Private Sub SetProperties()
        MyBase.RowTemplate.Height = MyBase.Font.Height * 1.3
        'MyBase.RowSatRecall.DefaultCellStyle.BackColor = Color.Transparent

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
        With MyBase.Columns(Cols.incidencia)
            .HeaderText = "Incidencia"
            .DataPropertyName = "incidencia"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.cli)
            .HeaderText = "Client"
            .DataPropertyName = "cli"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.sku)
            .HeaderText = "Producte"
            .DataPropertyName = "sku"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.PickupFrom)
            .HeaderText = "Punt de recollida"
            .DataPropertyName = "pickupfrom"
            .Width = 60
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.FchCustomer)
            .HeaderText = "Avis al client"
            .DataPropertyName = "FchCustomer"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 60
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            .DefaultCellStyle.Format = "dd/MM/yy"
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.FchManufacturer)
            .HeaderText = "Avis al proveidor"
            .DataPropertyName = "FchManufacturer"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 60
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            .DefaultCellStyle.Format = "dd/MM/yy"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.PickupRef)
            .HeaderText = "Ref.Recollida"
            .DataPropertyName = "PickupRef"
            .Width = 60
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.CreditNum)
            .HeaderText = "Abonament"
            .DataPropertyName = "CreditNum"
            .Width = 60
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Visible = _Values.Count > 0 AndAlso _Values.First.Mode = DTOSatRecall.Modes.PerAbonar
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

    Private Function SelectedItems() As List(Of DTOSatRecall)
        Dim retval As New List(Of DTOSatRecall)
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
            Dim oMenu_SatRecall As New Menu_SatRecall(SelectedItems.First)
            AddHandler oMenu_SatRecall.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_SatRecall.Range)
            'oContextMenu.Items.Add("-")
        End If
        'oContextMenu.Items.Add("afegir", Nothing, AddressOf Do_AddNew)
        'oContextMenu.Items.Add("importar de Excel", Nothing, AddressOf Do_ImportExcel)

        MyBase.ContextMenuStrip = oContextMenu
    End Sub



    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles MyBase.DoubleClick
        Dim oCurrentControlItem As ControlItem = CurrentControlItem()
        If oCurrentControlItem IsNot Nothing Then
            Dim oSelectedValue As DTOSatRecall = CurrentControlItem.Source
            Select Case _SelectionMode
                Case DTO.Defaults.SelectionModes.browse
                    Dim oFrm As New Frm_SatRecall(oSelectedValue)
                    AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                    oFrm.Show()
                Case DTO.Defaults.SelectionModes.selection
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

    Private Sub DataGridView1_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles MyBase.RowPrePaint
        Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
        Dim oControlItem As ControlItem = oRow.DataBoundItem
        Dim oSatRecall As DTOSatRecall = oControlItem.Source

        'If oSatRecall.Obsoleto Then
        'oRow.DefaultCellStyle.BackColor = Color.LightGray
        'Else
        'oRow.DefaultCellStyle.BackColor = Color.WhiteSmoke
        'End If
    End Sub

    Private Sub DataGridView1_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles MyBase.CellFormatting
        Select Case e.ColumnIndex
            Case Cols.ico
                Dim oRow As DataGridViewRow = DirectCast(sender, DataGridView).Rows(e.RowIndex)
                Dim oControlItem As ControlItem = oRow.DataBoundItem
                Dim oSatRecall As DTOSatRecall = oControlItem.Source
                'Dim oDocFile As DTODocFile = oSatRecall.DocFile
                'If oDocFile IsNot Nothing Then
                ' e.Value = iconHelper.GetIconFromMimeCod(oDocFile.Mime)
                ' End If
        End Select
    End Sub


    Protected Class ControlItem
        Property Source As DTOSatRecall

        Property Incidencia As String
        Property Cli As String
        Property Sku As String
        Property PickupFrom As String
        Property FchCustomer As Nullable(Of Date)
        Property FchManufacturer As Nullable(Of Date)
        Property PickupRef As String
        Property CreditNum As String

        Public Sub New(value As DTOSatRecall)
            MyBase.New()
            _Source = value
            With value
                _Incidencia = .Incidencia.AsinOrNum()
                _Cli = .Incidencia.Customer.FullNom
                If .Incidencia.Product IsNot Nothing Then
                    '_Sku = .Incidencia.Product.FullNom(Current.Session.Lang)
                    _Sku = .Incidencia.Product.Nom.Esp
                End If
                _PickupFrom = .PickupFrom.ToString
                If .FchCustomer <> Nothing Then
                    _FchCustomer = .FchCustomer
                End If
                If .FchManufacturer <> Nothing Then
                    _FchManufacturer = .FchManufacturer
                End If
                _PickupRef = .PickupRef
                _CreditNum = .CreditNum
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class

