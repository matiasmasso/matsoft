Public Class Xl_InvoicesToPrint
    Inherits _Xl_ReadOnlyDatagridview

    Private _Values As List(Of DTOInvoice)
    Private _DefaultValue As DTOInvoice
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse

    Private _Filter As String
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)
    Public Event RequestToCheckOut(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)
    Public Event onItemSelected(sender As Object, e As MatEventArgs)

    Private Enum Cols
        Checked
        Fra
        Mode
        Fch
        Amt
        Nom
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

    Public ReadOnly Property CheckedValues() As List(Of DTOInvoice)
        Get
            Dim retval As New List(Of DTOInvoice)
            For Each oControlItem As ControlItem In _ControlItems
                If oControlItem.Checked Then
                    retval.Add(oControlItem.Source)
                End If
            Next
            Return retval
        End Get
    End Property

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

    Private Function FilteredValues() As List(Of DTOInvoice)
        Dim retval As List(Of DTOInvoice)
        If _Filter = "" Then
            retval = _Values
        Else
            retval = _Values.FindAll(Function(x) x.Customer.FullNom.ToLower.Contains(_Filter.ToLower))
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
        'MyBase.RowTemplate.Height = MyBase.Font.Height * 1.3
        'MyBase.RowInvoice.DefaultCellStyle.BackColor = Color.Transparent

        MyBase.AutoGenerateColumns = False
        MyBase.Columns.Clear()

        MyBase.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        MyBase.ColumnHeadersVisible = True
        MyBase.RowHeadersVisible = False
        MyBase.MultiSelect = True
        MyBase.AllowUserToResizeRows = False
        MyBase.AllowDrop = False
        MyBase.ReadOnly = False

        MyBase.Columns.Add(New DataGridViewCheckBoxColumn)
        With DirectCast(MyBase.Columns(Cols.Checked), DataGridViewCheckBoxColumn)
            .DataPropertyName = "Checked"
            .HeaderText = ""
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 20
            '.DefaultCellStyle.NullValue = Nothing
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Fra)
            .HeaderText = "Factura"
            .DataPropertyName = "Fra"
            .Width = 60
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#"
            .ReadOnly = True
        End With
        MyBase.Columns.Add(New DataGridViewImageColumn(False))
        With DirectCast(MyBase.Columns(Cols.Mode), DataGridViewImageColumn)
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
            .ReadOnly = True
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Amt)
            .HeaderText = "Import"
            .DataPropertyName = "Amt"
            .Width = 70
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
            .ReadOnly = True
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Nom)
            .HeaderText = "Nom"
            .DataPropertyName = "Nom"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            .ReadOnly = True
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
            Dim oMenuItem As New ToolStripMenuItem("factura")
            Dim oMenu_Invoice As New Menu_Invoice(SelectedItems)
            AddHandler oMenu_Invoice.AfterUpdate, AddressOf RefreshRequest
            oMenuItem.DropDownItems.AddRange(oMenu_Invoice.Range)
            oContextMenu.Items.Add(oMenuItem)

            oMenuItem = New ToolStripMenuItem("seleccionar")
            oContextMenu.Items.Add(oMenuItem)
            oMenuItem.DropDownItems.Add(New ToolStripMenuItem("sel·lecciona-ho tot", My.Resources.Checked13, AddressOf SelectAll))
            oMenuItem.DropDownItems.Add(New ToolStripMenuItem("de-sel·lecciona-ho tot", My.Resources.UnChecked13, AddressOf SelectNone))
            oMenuItem.DropDownItems.Add(New ToolStripMenuItem("de-sel·lecciona la resta", My.Resources.CheckedGrayed13, AddressOf SelectRest))
            oMenuItem.DropDownItems.Add(New ToolStripMenuItem("descarta", My.Resources.aspa, AddressOf CheckOut))


        End If

        MyBase.ContextMenuStrip = oContextMenu
    End Sub

    Public Shadows Sub SelectAll()
        For Each oControlItem As ControlItem In _ControlItems
            oControlItem.Checked = True
        Next
        MyBase.Refresh()
    End Sub

    Public Sub SelectNone()
        For Each oControlItem As ControlItem In _ControlItems
            oControlItem.Checked = False
        Next
        MyBase.Refresh()
    End Sub

    Public Sub SelectRest()
        For Each oRow As DataGridViewRow In MyBase.Rows
            Dim oControlItem As ControlItem = oRow.DataBoundItem
            oControlItem.Checked = oRow.Selected
        Next
        MyBase.Refresh()
    End Sub

    Public Sub CheckOut()
        Dim DtFch As Date = Now
        Dim oInvoices As New List(Of DTOInvoice)
        For Each oRow As DataGridViewRow In MyBase.SelectedRows
            Dim oControlItem As ControlItem = oRow.DataBoundItem
            Dim oInvoice As DTOInvoice = oControlItem.Source
            oInvoices.Add(oInvoice)
        Next

        RaiseEvent RequestToCheckOut(Me, New MatEventArgs(oInvoices))
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

    Private Sub DataGridView1_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Me.CellValueChanged
        Select Case e.ColumnIndex
            Case Cols.Checked
                If _AllowEvents Then
                    RaiseEvent ValueChanged(Me, MatEventArgs.Empty)
                End If
        End Select
    End Sub

    Private Sub DataGridView1_CurrentCellDirtyStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.CurrentCellDirtyStateChanged
        'provoca CellValueChanged a cada clic sense sortir de la casella
        Select Case MyBase.CurrentCell.ColumnIndex
            Case Cols.Checked
                MyBase.CommitEdit(DataGridViewDataErrorContexts.Commit)
        End Select
    End Sub

    Private Sub Xl_InvoicesToPrint_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles Me.CellFormatting
        Select Case e.ColumnIndex
            Case Cols.Mode
                Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
                Dim oControlItem As ControlItem = oRow.DataBoundItem
                Select Case oControlItem.Mode
                    Case DTOInvoice.PrintModes.Printer
                        e.Value = My.Resources.printer
                    Case DTOInvoice.PrintModes.Email
                        e.Value = My.Resources.MailSobreGroc
                    Case DTOInvoice.PrintModes.Edi
                        e.Value = My.Resources.edi
                End Select
        End Select
    End Sub

    Protected Class ControlItem
        Property Source As DTOInvoice

        Property Checked As Boolean
        Property Fra As Integer
        Property Fch As Date
        Property Amt As Decimal
        Property Nom As String
        Property Mode As DTOInvoice.PrintModes

        Public Sub New(value As DTOInvoice)
            MyBase.New()
            _Source = value
            With value
                _Fra = .Num
                _Fch = .Fch
                _Amt = .Total.Eur
                _Nom = .Customer.FullNom
                _Mode = .PrintMode
                ' _Checked = True
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class


