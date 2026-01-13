Public Class Xl_Invoices
    Inherits _Xl_ReadOnlyDatagridview

    Private _Values As List(Of DTOInvoice)
    Private _DefaultValue As DTOInvoice
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse
    Private _ShowProgress As ProgressBarHandler

    Private _Filter As String
    Private _ControlItems As ControlItems
    Private _HideCustomerColumn As Boolean
    Private _SiiFilterMenuItem As ToolStripMenuItem
    Private _AllowEvents As Boolean

    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)
    Public Event onItemSelected(sender As Object, e As MatEventArgs)

    Private Enum Cols
        Pdf
        Sii
        Prnt
        Id
        Fch
        Nom
        Eur
        Fpg
        L2
        L9
        Trascendencia
        Concepte
    End Enum

    Public Shadows Sub Load(values As List(Of DTOInvoice), Optional oDefaultValue As DTOInvoice = Nothing,
                            Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse,
                            Optional HideCustomerColumn As Boolean = False,
                            Optional ShowProgress As ProgressBarHandler = Nothing)
        _Values = values
        _HideCustomerColumn = HideCustomerColumn
        _SelectionMode = oSelectionMode
        _ShowProgress = ShowProgress

        _SiiFilterMenuItem = New ToolStripMenuItem("Filtra pendents Sii", Nothing, AddressOf Do_FilterSiiPending)
        _SiiFilterMenuItem.CheckOnClick = True

        Static PropertiesSet As Boolean
        If Not PropertiesSet Then
            SetProperties()
            PropertiesSet = True
        End If

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

        If _DefaultValue Is Nothing Then
            UIHelper.SetDataGridviewCurrentCell(Me, oCell)
        Else
            Dim oControlItem As ControlItem = _ControlItems.ToList.Find(Function(x) x.Source.Equals(_DefaultValue))
            Dim rowIdx As Integer = _ControlItems.IndexOf(oControlItem)
            If rowIdx >= 0 Then
                MyBase.CurrentCell = MyBase.Rows(rowIdx).Cells(Cols.Nom)
            End If
        End If

        SetContextMenu()
        _AllowEvents = True
    End Sub

    Private Function FilteredValues() As List(Of DTOInvoice)
        Dim retval As List(Of DTOInvoice)
        If _Filter = "" Then
            retval = _Values
        Else
            retval = _Values.FindAll(Function(x) (x.Customer.FullNom <> Nothing AndAlso x.Customer.FullNom.ToLower.Contains(_Filter.ToLower)) Or x.Num.ToString.Contains(_Filter)).ToList
        End If
        If _SiiFilterMenuItem.Checked Then
            retval = retval.Where(Function(x) x.SiiLog.Result <> DTOInvoice.SiiResults.Correcto).ToList
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

    Public ReadOnly Property Value As DTOInvoice
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As DTOInvoice = oControlItem.Source
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

        MyBase.Columns.Add(New DataGridViewImageColumn(False))
        With DirectCast(MyBase.Columns(Cols.Pdf), DataGridViewImageColumn)
            .HeaderText = "F"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 16
            .DefaultCellStyle.NullValue = Nothing
            .ReadOnly = True
        End With

        MyBase.Columns.Add(New DataGridViewImageColumn(False))
        With DirectCast(MyBase.Columns(Cols.Sii), DataGridViewImageColumn)
            .HeaderText = "S"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 16
            .DefaultCellStyle.NullValue = Nothing
            .ReadOnly = True
        End With

        MyBase.Columns.Add(New DataGridViewImageColumn(False))
        With DirectCast(MyBase.Columns(Cols.Prnt), DataGridViewImageColumn)
            .HeaderText = "P"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 16
            .DefaultCellStyle.NullValue = Nothing
            .ReadOnly = True
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Id)
            .HeaderText = "Factura"
            .DataPropertyName = "Num"
            .Width = 50
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
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
        With MyBase.Columns(Cols.Nom)
            .HeaderText = "Nom"
            .DataPropertyName = "Nom"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            If _HideCustomerColumn Then .Visible = False
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Eur)
            .HeaderText = "Import"
            .DataPropertyName = "Eur"
            .Width = 70
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Fpg)
            .HeaderText = "Pagament"
            .DataPropertyName = "Fpg"
            .Width = 100
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.L2)
            .HeaderText = "F"
            .DataPropertyName = "L2"
            .Width = 30
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.L9)
            .HeaderText = "E"
            .DataPropertyName = "L9"
            .Width = 30
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Trascendencia)
            .HeaderText = "T"
            .DataPropertyName = "Trascendencia"
            .Width = 30
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Concepte)
            .HeaderText = "Concepte"
            .DataPropertyName = "Concepte"
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
            Dim oSelectedItems As List(Of DTOInvoice) = SelectedItems()
            Dim oMenu_Invoice As New Menu_Invoice(oSelectedItems, _ShowProgress)
            AddHandler oMenu_Invoice.AfterUpdate, AddressOf RefreshRequest
            AddHandler oMenu_Invoice.RequestToToggleProgressBar, AddressOf ToggleProgressBarRequest

            If oSelectedItems.Count > 1 Then
                Dim dcSum As Decimal = oSelectedItems.Sum(Function(x) x.Total.Eur)
                oContextMenu.Items.Add(String.Format("total {0:#,###.00} €", dcSum))
                oContextMenu.Items.Add("-")
            End If
            oContextMenu.Items.AddRange(oMenu_Invoice.Range)
            oContextMenu.Items.Add("-")
        End If

        oContextMenu.Items.Add("Excel", Nothing, AddressOf Do_Excel)
        oContextMenu.Items.Add(_SiiFilterMenuItem)

        MyBase.ContextMenuStrip = oContextMenu
    End Sub


    Private Sub Do_Excel()
        Dim oLang As DTOLang = DTOApp.current.lang
        Dim oSheet As MatHelper.Excel.Sheet
        If SelectedItems.Count > 1 Then
            oSheet = FEB.Invoices.Excel(SelectedItems)
        Else
            oSheet = FEB.Invoices.Excel(_Values)
        End If
        Dim exs As New List(Of Exception)
        If Not UIHelper.ShowExcel(oSheet, exs) Then
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Do_FilterSiiPending()
        Refresca()
    End Sub



    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles MyBase.DoubleClick
        Dim oCurrentControlItem As ControlItem = CurrentControlItem()
        If oCurrentControlItem IsNot Nothing Then
            Dim oSelectedValue As DTOInvoice = CurrentControlItem.Source
            Select Case _SelectionMode
                Case DTO.Defaults.SelectionModes.Browse
                    Dim oFrm As New Frm_Invoice(oSelectedValue)
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

    Private Sub Xl_Invoices_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles Me.CellFormatting
        Select Case e.ColumnIndex
            Case Cols.Pdf
                Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
                Dim oControlItem As ControlItem = oRow.DataBoundItem
                Dim oInvoice As DTOInvoice = oControlItem.Source
                If oInvoice.DocFile IsNot Nothing Then
                    e.Value = My.Resources.pdf
                End If
            Case Cols.Sii
                Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
                Dim oControlItem As ControlItem = oRow.DataBoundItem
                Dim oInvoice As DTOInvoice = oControlItem.Source
                Select Case oInvoice.SiiLog.Result
                    Case DTOInvoice.SiiResults.Correcto
                        e.Value = My.Resources.sheriff
                    Case DTOInvoice.SiiResults.AceptadoConErrores
                        e.Value = My.Resources.warn
                    Case DTOInvoice.SiiResults.Incorrecto
                        e.Value = My.Resources.WarnRed16
                End Select
            Case Cols.Prnt
                Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
                Dim oControlItem As ControlItem = oRow.DataBoundItem
                Dim oInvoice As DTOInvoice = oControlItem.Source
                If oInvoice.PrintMode > 0 Then
                    e.Value = IconHelper.PrintModeIcon(oInvoice.printMode)
                End If
        End Select
    End Sub

    Private Sub Xl_Invoices_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles Me.RowPrePaint
        If _HideCustomerColumn Then
            Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
            Dim oControlItem As ControlItem = oRow.DataBoundItem
            If oControlItem.Fch.Year < DTO.GlobalVariables.Today().Year Then
                oRow.DefaultCellStyle.BackColor = Color.LightGray
            End If
        End If
    End Sub

    Private Sub Xl_Invoices_CellToolTipTextNeeded(sender As Object, e As DataGridViewCellToolTipTextNeededEventArgs) Handles Me.CellToolTipTextNeeded
        If e.RowIndex >= 0 Then
            Select Case e.ColumnIndex
                Case Cols.L2
                    Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
                    Dim oControlItem As ControlItem = oRow.DataBoundItem
                    Dim oInvoice As DTOInvoice = oControlItem.Source
                    Dim sTipoFra As String = oInvoice.TipoFactura
                    If sTipoFra.Length > 3 Then
                        sTipoFra = sTipoFra.Substring(3).Replace("_", " ")
                    End If
                    e.ToolTipText = sTipoFra
                Case Cols.L9
                    Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
                    Dim oControlItem As ControlItem = oRow.DataBoundItem
                    Dim oInvoice As DTOInvoice = oControlItem.Source
                    Select Case oInvoice.SiiL9
                        Case "E2"
                            e.ToolTipText = "Art.21 (exportació extracomunitaria)"
                        Case "E5"
                            e.ToolTipText = "Art.25 (exportació intracomunitaria)"
                    End Select
                Case Cols.Trascendencia
                    Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
                    Dim oControlItem As ControlItem = oRow.DataBoundItem
                    Dim oInvoice As DTOInvoice = oControlItem.Source
                    Select Case oInvoice.RegimenEspecialOTrascendencia
                        Case "01"
                            e.ToolTipText = "destinació nacional"
                        Case "15"
                            e.ToolTipText = "exportació"
                        Case "16"
                            e.ToolTipText = "declaració primer semestre 2017"
                    End Select
            End Select
        End If

    End Sub

    Protected Class ControlItem
        Property Source As DTOInvoice

        Property Nom As String
        Property Num As String
        Property Fch As Date
        Property Eur As Decimal
        Property Fpg As String
        Property L2 As String
        Property L9 As String
        Property Trascendencia As String
        Property Concepte As String


        Public Sub New(value As DTOInvoice)
            MyBase.New()
            _Source = value
            With value
                _Nom = .customer.FullNom
                _Num = .NumeroYSerie()
                _Fch = .fch
                _Eur = .Total.Eur
                _Fpg = .Fpg
                _L2 = .TipoFactura
                _L9 = .SiiL9
                _Trascendencia = .RegimenEspecialOTrascendencia
                _Concepte = .Concepte.ToString
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class


