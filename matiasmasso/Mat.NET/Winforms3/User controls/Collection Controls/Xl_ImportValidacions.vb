Public Class Xl_ImportValidacions


    Inherits _Xl_ReadOnlyDatagridview

    Private _Validacions As List(Of DTOImportValidacio)
    Private _DefaultValue As DTOImportPrevisio

    Private _Filter As String
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)
    Public Event RequestToImportValidation(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)
    Public Event onItemSelected(sender As Object, e As MatEventArgs)
    Public Event onPurchaseOrderItemUpdateRequest(sender As Object, e As MatEventArgs)
    Public Event RequestToExportValidacions(sender As Object, e As MatEventArgs)


    Private Enum Cols
        Ref
        Txt
        Qty
        Cfm
        Ico
    End Enum

    Public Shadows Sub Load(Validacions As List(Of DTOImportValidacio))
        Static PropertiesSet As Boolean
        If Not PropertiesSet Then
            SetProperties()
            PropertiesSet = True
        End If

        _Validacions = Validacions
        Refresca()
    End Sub

    Public ReadOnly Property Values As List(Of DTOImportValidacio)
        Get
            Return _Validacions
        End Get
    End Property

    Private Sub Refresca()
        _AllowEvents = False
        Dim oFilteredValues As List(Of DTOImportValidacio) = FilteredValues()
        _ControlItems = New ControlItems
        Dim oControlItem As ControlItem = Nothing
        For Each oItem As DTOImportValidacio In oFilteredValues
            oControlItem = New ControlItem(oItem)
            _ControlItems.Add(oControlItem)
        Next



        Dim oCell As DTODatagridviewCell = UIHelper.DataGridviewCurrentCell(Me)
        MyBase.DataSource = _ControlItems
        UIHelper.SetDataGridviewCurrentCell(Me, oCell)

        SetContextMenu()
        _AllowEvents = True
    End Sub

    Private Function FilteredValues() As List(Of DTOImportValidacio)
        Dim retval As List(Of DTOImportValidacio)
        If _Filter = "" Then
            retval = _Validacions
        Else
            retval = _Validacions.FindAll(Function(x) x.Sku.nomLlarg.Contains(_Filter.ToLower))
        End If
        Return retval
    End Function


    Public Property Filter As String
        Get
            Return _Filter
        End Get
        Set(value As String)
            _Filter = value
            If _Validacions IsNot Nothing Then Refresca()
        End Set
    End Property

    Public Sub ClearFilter()
        If _Filter > "" Then
            _Filter = ""
            Refresca()
        End If
    End Sub

    Public ReadOnly Property Value As DTOImportValidacio
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As DTOImportValidacio = oControlItem.Source
            Return retval
        End Get
    End Property

    Private Sub SetProperties()
        MyBase.RowTemplate.Height = MyBase.Font.Height * 1.3

        MyBase.AutoGenerateColumns = False
        MyBase.Columns.Clear()

        MyBase.DataSource = _ControlItems
        MyBase.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        MyBase.ColumnHeadersVisible = True
        MyBase.RowHeadersVisible = False
        MyBase.MultiSelect = True
        MyBase.AllowUserToResizeRows = False
        MyBase.AllowDrop = False
        MyBase.ReadOnly = True

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Ref)
            .HeaderText = "Referencia"
            .DataPropertyName = "Ref"
            .Width = 70
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Txt)
            .HeaderText = "Concepte"
            .DataPropertyName = "Txt"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Qty)
            .HeaderText = "Previst"
            .DataPropertyName = "Qty"
            .Width = 60
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,###0;-#,###0;#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Cfm)
            .HeaderText = "Confirmat"
            .DataPropertyName = "Cfm"
            .Width = 60
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,###0;-#,###0;#"
        End With
        MyBase.Columns.Add(New DataGridViewImageColumn)
        With DirectCast(MyBase.Columns(Cols.Ico), DataGridViewImageColumn)
            .DataPropertyName = "Ico"
            .HeaderText = ""
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 16
            .DefaultCellStyle.NullValue = Nothing
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

    Private Function SelectedItems() As List(Of DTOImportValidacio)
        Dim retval As New List(Of DTOImportValidacio)
        For Each oRow As DataGridViewRow In MyBase.SelectedRows
            Dim oControlItem As ControlItem = oRow.DataBoundItem
            retval.Add(oControlItem.Source)
        Next

        If retval.Count = 0 Then
            retval.Add(CurrentControlItem.Source)
        End If
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
            Dim oItems As List(Of DTOImportValidacio) = SelectedItems()
            If oItems.Count > 0 Then
                Dim oMenu_Art As New Menu_ProductSku(oItems.First.Sku)
                AddHandler oMenu_Art.AfterUpdate, AddressOf Refreshrequest
                oContextMenu.Items.AddRange(oMenu_Art.Range)
                oContextMenu.Items.Add("-")

            End If
        End If
        oContextMenu.Items.Add("importar XML validació magatzem", Nothing, AddressOf Do_ImportValidation)
        oContextMenu.Items.Add("exportar Excel", Nothing, AddressOf Do_ExportExcel)
        oContextMenu.Items.Add("refresca", Nothing, AddressOf Refreshrequest)

        MyBase.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Do_ImportValidation()
        RaiseEvent RequestToImportValidation(Me, MatEventArgs.Empty)
    End Sub

    Private Sub Do_ExportExcel()
        RaiseEvent RequestToExportValidacions(Me, New MatEventArgs(_Validacions))
    End Sub

    Private Sub Do_PurchaseOrderItemUpdateRequest(sender As Object, e As MatEventArgs)
        RaiseEvent onPurchaseOrderItemUpdateRequest(Me, e)
    End Sub

    Private Shadows Sub Refreshrequest(sender As Object, e As System.EventArgs)
        MyBase.RefreshRequest(Me, MatEventArgs.Empty)
    End Sub


    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles MyBase.DoubleClick
        Dim oCurrentControlItem As ControlItem = CurrentControlItem()
        If oCurrentControlItem IsNot Nothing Then
            Dim oSelectedValue As DTOImportValidacio = CurrentControlItem.Source
            'Dim oFrm As New Frm_ImportPrevisio(oSelectedValue)
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

    Private Sub Xl_ImportPrevisio_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles Me.CellFormatting
        Select Case e.ColumnIndex
            Case Cols.Ico
                Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
                Dim oControlitem As ControlItem = oRow.DataBoundItem
                If oControlitem.Qty <> oControlitem.Cfm Then
                    e.Value = My.Resources.warn
                End If
        End Select
    End Sub


    Protected Class ControlItem
        Property Source As DTOImportValidacio

        Property Ref As String
        Property Txt As String
        Property Qty As Integer
        Property Cfm As Integer


        Public Sub New(value As DTOImportValidacio)
            MyBase.New()
            _Source = value
            With value
                _Ref = .Sku.id
                _Txt = .Sku.nomLlarg.Tradueix(Current.Session.Lang)
                _Qty = .Qty
                _Cfm = .Cfm
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class

