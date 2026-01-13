Public Class Xl_Csbs_Checklist

    Inherits DataGridView

    Private _Csbs As List(Of DTOCsb)
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event RequestToRefresh(sender As Object, e As MatEventArgs)
    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Private Enum Cols
        checked
        Amt
        Nom
        Ico
        Bank
    End Enum

    Public Shadows Sub Load(oCsbs As List(Of DTOCsb))
        Static PropertiesSet As Boolean
        If Not PropertiesSet Then
            SetProperties()
            PropertiesSet = True
        End If

        _Csbs = oCsbs
        Refresca()
    End Sub

    Public ReadOnly Property Values() As List(Of DTOCsb)
        Get
            Dim retval As New List(Of DTOCsb)
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

        _ControlItems = New ControlItems
        For Each oItem As DTOCsb In _Csbs
            Dim oControlItem As New ControlItem(oItem)
            _ControlItems.Add(oControlItem)
        Next

        Dim oCell As DTODatagridviewCell = UIHelper.DataGridviewCurrentCell(Me)
        MyBase.DataSource = _ControlItems
        UIHelper.SetDataGridviewCurrentCell(Me, oCell)

        SetContextMenu()
        _AllowEvents = True
    End Sub

    Private Sub SetProperties()
        'MyBase.RowTemplate.Height = MyBase.Font.Height * 1.3

        MyBase.AutoGenerateColumns = False
        MyBase.Columns.Clear()

        MyBase.DataSource = _ControlItems
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
        With MyBase.Columns(Cols.Amt)
            .HeaderText = "Import"
            .DataPropertyName = "Amt"
            .ReadOnly = True
            .Width = 70
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Nom)
            .HeaderText = "Lliurat"
            .DataPropertyName = "Nom"
            .ReadOnly = True
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End With
        MyBase.Columns.Add(New DataGridViewImageColumn)
        With DirectCast(MyBase.Columns(Cols.Ico), DataGridViewImageColumn)
            .DataPropertyName = "Ico"
            .HeaderText = ""
            .ReadOnly = True
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 16
            .DefaultCellStyle.NullValue = Nothing
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Bank)
            .HeaderText = "Banc"
            .DataPropertyName = "Bank"
            .ReadOnly = True
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
            Dim oCsb As DTOCsb = oControlItem.Source
            If oCsb.ExceptionCode <> DTOCsb.ExceptionCodes.Success Then
                Dim oMenuItemErrors As New ToolStripMenuItem("Error")
                oMenuItemErrors.ForeColor = Color.Red
                oContextMenu.Items.Add(oMenuItemErrors)

                Dim oMenuItemError As New ToolStripMenuItem(DTOCsb.ValidationText(oCsb.ExceptionCode))
                oMenuItemErrors.DropDownItems.Add(oMenuItemError)

                Select Case oCsb.ExceptionCode
                    Case DTOCsb.ExceptionCodes.NoBn1, DTOCsb.ExceptionCodes.NoBn2
                        Dim oMenuItemEditIban As New ToolStripMenuItem("editar fitxa del mandat bancari", Nothing, AddressOf Do_EditIban)
                        oMenuItemEditIban.Tag = oCsb
                        oMenuItemError.DropDownItems.Add(oMenuItemEditIban)
                    Case DTOCsb.ExceptionCodes.WrongBIC
                        Dim oMenuItemEditBIC As New ToolStripMenuItem("editar fitxa bancaria", Nothing, AddressOf Do_EditBank)
                        oMenuItemEditBIC.Tag = oCsb.Iban.BankBranch.Bank
                        oMenuItemError.DropDownItems.Add(oMenuItemEditBIC)
                End Select
            End If

            Dim oMenuCsb As New Menu_Csb2(oCsb)
            AddHandler oMenuCsb.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenuCsb.Range)
            oContextMenu.Items.Add("-")
        End If
        oContextMenu.Items.Add("seleccionar tot", Nothing, AddressOf Do_SelectAll)
        oContextMenu.Items.Add("deseleccionar tot", Nothing, AddressOf Do_SelectNone)
        oContextMenu.Items.Add("refresca", Nothing, AddressOf Refresca)

        MyBase.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Do_SelectAll()
        For Each oRow As DataGridViewRow In MyBase.SelectedRows
            DirectCast(oRow.Cells(Cols.checked), DataGridViewCheckBoxCell).Value = True
        Next
    End Sub

    Private Sub Do_SelectNone()
        For Each oRow As DataGridViewRow In MyBase.SelectedRows
            DirectCast(oRow.Cells(Cols.checked), DataGridViewCheckBoxCell).Value = False
        Next
    End Sub

    Private Sub Do_EditIban(sender As Object, e As System.EventArgs)
        Dim oMenuItem As ToolStripMenuItem = sender
        Dim oCsb As DTOCsb = oMenuItem.Tag
        Dim oFrm As New Frm_Iban(oCsb.Iban)
        AddHandler oFrm.AfterUpdate, AddressOf OnIbanUpdated
        oFrm.Show()
    End Sub

    Private Sub Do_EditBank(sender As Object, e As System.EventArgs)
        Dim oMenuItem As ToolStripMenuItem = sender
        Dim oBank As DTOBank = oMenuItem.Tag
        Dim oFrm As New Frm_Bank(oBank)
        AddHandler oFrm.AfterUpdate, AddressOf OnBankUpdated
        oFrm.Show()
    End Sub

    Private Sub OnIbanUpdated(sender As Object, e As MatEventArgs)
        Dim oIban As DTOIban = e.Argument
        Dim oControlItems As List(Of ControlItem) = _ControlItems.ToList.FindAll(Function(x) x.Source.Iban.Equals(oIban))
        For Each oControlItem As ControlItem In oControlItems
            DTOCsb.Validate(oControlItem.Source, DTOCsa.FileFormats.SepaCore)
            With oControlItem
                .Warning = .Source.ExceptionCode <> DTOCsb.ExceptionCodes.Success
                .WarningText = DTOCsb.ValidationText(.Source.ExceptionCode)
            End With
        Next
        Refresca()
    End Sub

    Private Sub OnBankUpdated(sender As Object, e As MatEventArgs)
        Dim oBank As DTOBank = e.Argument
        For Each oControlItem As ControlItem In _ControlItems
            If oControlItem.Source.Iban IsNot Nothing Then
                If oControlItem.Source.Iban.BankBranch IsNot Nothing Then
                    If oControlItem.Source.Iban.BankBranch.Bank.Equals(oBank) Then
                        oControlItem.Source.Iban.BankBranch.Bank = oBank
                        DTOCsb.Validate(oControlItem.Source, DTOCsa.FileFormats.SepaCore)
                        With oControlItem
                            .Warning = .Source.ExceptionCode <> DTOCsb.ExceptionCodes.Success
                            .WarningText = DTOCsb.ValidationText(.Source.ExceptionCode)
                        End With
                    End If
                End If
            End If
        Next
        Refresca()
    End Sub

    Private Sub DataGridView1_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles MyBase.CellValueChanged
        Select Case e.ColumnIndex
            Case Cols.checked
                If _AllowEvents Then
                    RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
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

    Private Sub RefreshRequest()
        Refresca()
    End Sub

    Private Sub Xl_Csbs_Checklist_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles Me.CellFormatting
        Select Case e.ColumnIndex
            Case Cols.Ico
                Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
                Dim oControlitem As ControlItem = oRow.DataBoundItem
                If oControlitem.Warning Then
                    e.Value = My.Resources.warn
                End If
        End Select
    End Sub

    Private Sub Xl_Csbs_Checklist_CellToolTipTextNeeded(sender As Object, e As DataGridViewCellToolTipTextNeededEventArgs) Handles Me.CellToolTipTextNeeded
        If e.RowIndex >= 0 Then
            Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
            Dim oControlitem As ControlItem = oRow.DataBoundItem
            e.ToolTipText = oControlitem.WarningText
        End If
    End Sub

    Private Sub Xl_Csbs_Checklist_SelectionChanged(sender As Object, e As EventArgs) Handles Me.SelectionChanged
        If _AllowEvents Then
            SetContextMenu()
        End If
    End Sub

    Protected Class ControlItem
        Property Source As DTOCsb
        Property Checked As Boolean
        Property Amt As Decimal
        Property Nom As String
        Property Bank As String
        Property Warning As Boolean
        Property WarningText As String

        Public Sub New(value As DTOCsb)
            MyBase.New()
            _Source = value
            With value
                _Amt = .Amt.Eur
                _Nom = .Contact.Nom
                _Bank = DTOIban.BankNom(.Iban)
                _Checked = .ExceptionCode = DTOCsb.ExceptionCodes.Success
                _Warning = .ExceptionCode <> DTOCsb.ExceptionCodes.Success
                _WarningText = DTOCsb.ValidationText(.ExceptionCode)
            End With

        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class



