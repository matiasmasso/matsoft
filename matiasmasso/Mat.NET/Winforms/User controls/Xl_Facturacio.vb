Public Class Xl_Facturacio

    Inherits _Xl_ReadOnlyDatagridview

    Private _Values As List(Of DTOInvoice)
    Private _LastInvoicesEachSerie As List(Of DTOInvoice)
    Private _NextNum As Integer
    Private _NextRct As Integer
    Private _NextSmp As Integer

    Private _NextNumBackup As Integer
    Private _NextRctBackup As Integer
    Private _NextSmpBackup As Integer

    Private _MinFch As DateTimeOffset
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse

    Private _Filter As String
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean
    Private _MenuItemCheckAll As ToolStripMenuItem
    Private _MenuItemCheckNone As ToolStripMenuItem

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)
    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)
    Public Event onItemSelected(sender As Object, e As MatEventArgs)

    Private Enum Cols
        checked
        Fch
        Num
        Nom
        Amt
        ico
    End Enum

    Public Shadows Sub Load(values As List(Of DTOInvoice), oLastInvoicesEachSerie As List(Of DTOInvoice), DtMinFch As DateTimeOffset, Optional oDefaultValue As DTOInvoice = Nothing)
        _Values = values
        _NextNum = 1
        _NextRct = 1
        _NextSmp = 1

        Dim lastInvoice = oLastInvoicesEachSerie.FirstOrDefault(Function(x) x.Serie = DTOInvoice.Series.standard)
        If lastInvoice IsNot Nothing Then _NextNum = lastInvoice.Num + 1
        lastInvoice = oLastInvoicesEachSerie.FirstOrDefault(Function(x) x.Serie = DTOInvoice.Series.rectificativa)
        If lastInvoice IsNot Nothing Then _NextRct = lastInvoice.Num + 1
        lastInvoice = oLastInvoicesEachSerie.FirstOrDefault(Function(x) x.Serie = DTOInvoice.Series.simplificada)
        If lastInvoice IsNot Nothing Then _NextSmp = lastInvoice.Num + 1

        _NextNumBackup = _NextNum
        _NextRctBackup = _NextRct
        _NextSmpBackup = _NextSmp

        _MinFch = DtMinFch

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
            Dim oControlItem As New ControlItem(oItem, True)
            _ControlItems.Add(oControlItem)
        Next

        'SetFchs()

        Dim oCell As DTODatagridviewCell = UIHelper.DataGridviewCurrentCell(Me)
        MyBase.DataSource = _ControlItems
        UIHelper.SetDataGridviewCurrentCell(Me, oCell)

        SetContextMenu()
        _AllowEvents = True
    End Sub

    Private Sub SetFchs()
        _NextNum = _NextNumBackup
        _NextRct = _NextRctBackup
        _NextSmp = _NextSmpBackup

        For Each oControlItem As ControlItem In _ControlItems
            Dim oInvoice As DTOInvoice = oControlItem.Source
            If oControlItem.Checked Then
                If Not oControlItem.FixedNum Then
                    If oInvoice.BaseImponible.isNegative Then
                        oInvoice.Num = _NextRct
                        oInvoice.Serie = DTOInvoice.Series.rectificativa
                        _NextRct += 1
                    Else
                        oInvoice.Num = _NextNum
                        oInvoice.Serie = DTOInvoice.Series.standard
                        _NextNum += 1
                    End If
                End If
            Else
                oInvoice.Num = 0
            End If

            If Not oControlItem.FixedFch Then
                Dim DtFirstDeliveryFch As Date = oInvoice.Deliveries.Min(Function(x) x.Fch)
                _MinFch = {_MinFch, DtFirstDeliveryFch}.Max
                If oInvoice.Fch <> _MinFch.LocalDateTime Then
                    oInvoice.Fch = _MinFch.LocalDateTime
                    DTOInvoice.setVto(oInvoice)
                End If
            End If

            oControlItem.Num = oInvoice.Num
            oControlItem.Fch = oInvoice.Fch
        Next
    End Sub

    Private Function NextNum(oSerie As DTOInvoice.Series) As Integer
        Dim retval As Integer = 1
        Select Case oSerie
            Case DTOInvoice.Series.standard
                retval = _NextNum + 1
            Case DTOInvoice.Series.rectificativa
                retval = _NextRct + 1
            Case DTOInvoice.Series.simplificada
                retval = _NextSmp + 1
        End Select
        Return retval
    End Function

    Private Sub RecalcNumAndFch()

    End Sub

    Private Function FilteredValues() As List(Of DTOInvoice)
        Dim retval As List(Of DTOInvoice)
        If _Filter = "" Then
            retval = _Values
        Else
            retval = _Values.FindAll(Function(x) x.Customer.Nom.ToLower.Contains(_Filter.ToLower))
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

    Public ReadOnly Property Invoices As List(Of DTOInvoice)
        Get
            Dim retval As New List(Of DTOInvoice)
            For Each oControlItem As ControlItem In _ControlItems.Where(Function(x) x.Checked = True)
                retval.Add(oControlItem.Source)
            Next
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
        With DirectCast(MyBase.Columns(Cols.checked), DataGridViewCheckBoxColumn)
            .DataPropertyName = "Checked"
            .HeaderText = ""
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 20
            '.DefaultCellStyle.NullValue = Nothing
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
            .HeaderText = "Numero"
            .DataPropertyName = "Num"
            .Width = 60
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Nom)
            .HeaderText = "Nom"
            .DataPropertyName = "Nom"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
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
        End With
        MyBase.Columns.Add(New DataGridViewImageColumn(False))
        With DirectCast(MyBase.Columns(Cols.ico), DataGridViewImageColumn)
            .CellTemplate = New DataGridViewImageCellBlank(False)
            .HeaderText = ""
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 16
            .DefaultCellStyle.NullValue = Nothing
            .ReadOnly = True
        End With

        _MenuItemCheckAll = New ToolStripMenuItem("sel·lecciona-ho tot", Nothing, AddressOf CheckAll)
        _MenuItemCheckNone = New ToolStripMenuItem("sel·lecciona res", Nothing, AddressOf CheckNone)
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
            Dim oInvoice As DTOInvoice = oControlItem.Source
            If oInvoice.Exceptions IsNot Nothing Then
                If oInvoice.Exceptions.Count <> 0 Then
                    oContextMenu.Items.Add(MenuItem_Exceptions(oInvoice))
                End If
            End If


            Dim oMenuItem_Invoice As New ToolStripMenuItem("factura")
            oContextMenu.Items.Add(oMenuItem_Invoice)

            Dim oMenu_Invoice As New Menu_Invoice(SelectedItems)
            AddHandler oMenu_Invoice.AfterUpdate, AddressOf RefreshRequest
            oMenuItem_Invoice.DropDownItems.AddRange(oMenu_Invoice.Range)

            Dim oMenuItem_Contact As New ToolStripMenuItem("client")
            oContextMenu.Items.Add(oMenuItem_Contact)
            If SelectedItems.Count = 1 Then
                Dim oMenu_Contact As New Menu_Contact(SelectedItems.First.Customer)
                AddHandler oMenu_Contact.AfterUpdate, AddressOf RefreshRequest
                oMenuItem_Contact.DropDownItems.AddRange(oMenu_Contact.Range)
            Else
                oMenuItem_Contact.Enabled = False
            End If

            oContextMenu.Items.Add("-")
        End If

        _MenuItemCheckAll.Enabled = CheckedValues.Count < _ControlItems.Count
        _MenuItemCheckNone.Enabled = CheckedValues.Count > 0

        oContextMenu.Items.Add(_MenuItemCheckAll)
        oContextMenu.Items.Add(_MenuItemCheckNone)

        MyBase.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub CheckAll()
        For Each item As ControlItem In _ControlItems
            item.Checked = True
        Next
        _MenuItemCheckAll.Enabled = False
        _MenuItemCheckNone.Enabled = _ControlItems.Count > 0
        SetFchs()
        MyBase.Refresh()
    End Sub

    Private Sub CheckNone()
        For Each item As ControlItem In _ControlItems
            item.Checked = False
        Next
        _MenuItemCheckAll.Enabled = _ControlItems.Count > 0
        _MenuItemCheckNone.Enabled = False
        SetFchs()
        MyBase.Refresh()
    End Sub

    Private Function MenuItem_Exceptions(oInvoice As DTOInvoice) As ToolStripMenuItem
        Dim retval As New ToolStripMenuItem("error")
        retval.ForeColor = Color.Red
        For Each oException As DTOInvoiceException In oInvoice.Exceptions
            retval.DropDownItems.Add(MenuItem_Exception(oException))
        Next
        Return retval
    End Function

    Private Function MenuItem_Exception(oException As DTOInvoiceException) As ToolStripMenuItem
        Dim retval As New ToolStripMenuItem(oException.Message)
        Select Case oException.Cod
            Case DTOInvoiceException.Cods.MultipleDeliveries
            Case DTOInvoiceException.Cods.WrongNif
            Case DTOInvoiceException.Cods.MissingPaymentTerms
            Case DTOInvoiceException.Cods.MissingIban
            Case DTOInvoiceException.Cods.UncompleteBank
                Dim oMenuItem As New ToolStripMenuItem("completar", Nothing, AddressOf Do_BankEdit)
                retval.DropDownItems.Add(oMenuItem)
            Case DTOInvoiceException.Cods.ObsTooLong
                Dim oMenuItem As New ToolStripMenuItem("rectificar", Nothing, AddressOf Do_InvoiceEdit)
                retval.DropDownItems.Add(oMenuItem)
            Case Else
        End Select
        Return retval
    End Function


    Private Sub Do_BankEdit()
        Dim oControlItem As ControlItem = CurrentControlItem()
        Dim oInvoice As DTOInvoice = oControlItem.Source
        Dim oIban As DTOIban = oInvoice.Customer.PaymentTerms.Iban
        Dim oBankBranch As DTOBankBranch = oIban.BankBranch
        Dim oFrm As New Frm_BankBranch(oBankBranch)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Do_InvoiceEdit()
        Dim oControlItem As ControlItem = CurrentControlItem()
        Dim oInvoice As DTOInvoice = oControlItem.Source
        Dim oFrm As New Frm_Invoice(oInvoice)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub


    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles MyBase.DoubleClick
        Dim oCell As DataGridViewCell = MyBase.CurrentCell
        Select Case oCell.ColumnIndex
            Case Cols.checked
            Case Else
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
        End Select
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles MyBase.SelectionChanged
        If _AllowEvents Then
            RaiseEvent ValueChanged(Me, New MatEventArgs(CurrentControlItem.Source))
            SetContextMenu()
        End If
    End Sub

    Private Sub Xl_Facturacio_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles Me.CellFormatting
        Select Case e.ColumnIndex
            Case Cols.ico
                Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
                Dim oControlItem As ControlItem = oRow.DataBoundItem
                Dim oInvoice As DTOInvoice = oControlItem.Source
                If oInvoice.Exceptions IsNot Nothing Then
                    If oInvoice.Exceptions.Count > 0 Then
                        e.Value = My.Resources.warning
                    End If
                End If
        End Select
    End Sub

    Public Function CheckedValues() As List(Of DTOInvoice)
        Dim retval As List(Of DTOInvoice) = _ControlItems.Where(Function(x) x.Checked = True).Select(Of DTOInvoice)(Function(y) y.Source).ToList
        Return retval
    End Function

    Private Sub DataGridView1_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles MyBase.CellValueChanged
        Select Case e.ColumnIndex
            Case Cols.checked
                If _AllowEvents Then
                    SetFchs()
                    MyBase.Refresh()
                    RaiseEvent AfterUpdate(Me, New MatEventArgs(CheckedValues))
                End If
        End Select
    End Sub

    Private Sub DataGridView1_CurrentCellDirtyStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.CurrentCellDirtyStateChanged
        'provoca CellValueChanged a cada clic sense sortir de la casella
        Select Case MyBase.CurrentCell.ColumnIndex
            Case Cols.checked
                MyBase.CommitEdit(DataGridViewDataErrorContexts.Commit)
        End Select
    End Sub

    Protected Class ControlItem
        Property Source As DTOInvoice

        Property Checked As Boolean
        Property Fch As Date
        Property Num As String
        Property Nom As String
        Property Amt As Decimal

        Property FixedFch As Boolean
        Property FixedNum As Boolean

        Public Sub New(value As DTOInvoice, BlChecked As Boolean)
            MyBase.New()
            _Source = value
            With value
                _Checked = BlChecked
                _Fch = .Fch
                _Num = .Num
                _Nom = .Customer.FullNom

                If .Total IsNot Nothing Then
                    _Amt = .Total.Eur
                End If
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class


