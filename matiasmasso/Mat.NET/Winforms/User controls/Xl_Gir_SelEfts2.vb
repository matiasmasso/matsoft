Public Class Xl_Gir_SelEfts2
    Private Shared _Descuadres As List(Of DTOCcd)
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event RequestToRefresh(sender As Object, e As MatEventArgs)
    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)

    Private Enum Cols
        Chk
        Eur
        Warn
        Vto
        Clx
    End Enum

    Public Enum Modes
        SEPAB2B
        Norma58
        NoDomiciliats
    End Enum

    Public Shadows Sub Load(values As List(Of DTOCsb), oDescuadres As List(Of DTOCcd))
        _Descuadres = oDescuadres
        _ControlItems = New ControlItems
        For Each oItem As DTOCsb In values
            Dim oControlItem As New ControlItem(oItem)
            _ControlItems.Add(oControlItem)
        Next
        LoadGrid()
    End Sub

    Public ReadOnly Property Values As List(Of DTOCsb)
        Get
            Dim retval As New List(Of DTOCsb)
            For Each oItem As ControlItem In CheckedItems()
                retval.Add(oItem.Source)
            Next
            Return retval
        End Get
    End Property

    Public Sub SelectAmount(oAmt As DTOAmt)
        Dim tot As Decimal
        For Each oControlItem In _ControlItems
            tot += oControlItem.Eur
            oControlItem.Checked = tot < oAmt.eur
        Next
        DataGridView1.Refresh()

    End Sub


    Public ReadOnly Property Value As DTOCsb
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As DTOCsb = oControlItem.Source
            Return retval
        End Get
    End Property

    Public Sub SetAmt(oAmt As DTOAmt)
        Dim DcSum As Decimal
        Dim BlExceeded As Boolean
        If _ControlItems IsNot Nothing Then
            Dim oControlList As List(Of ControlItem) = _ControlItems.ToList
            For Each oItem As ControlItem In _ControlItems
                Dim oContactGuid As Guid = DirectCast(oItem.Source, DTOCsb).Contact.Guid
                Dim BlNegative As Boolean = oControlList.Exists(Function(x) DirectCast(x.Source, DTOCsb).Contact.Guid.Equals(oContactGuid) And DirectCast(x.Source, DTOCsb).Amt.Eur < 0)
                If BlNegative Then
                    oItem.Checked = False
                Else
                    If DcSum + oItem.Eur > oAmt.Eur Then BlExceeded = True
                    If oItem.Eur > 0 And Not BlExceeded Then
                        oItem.Checked = True
                        DcSum += oItem.Eur
                    Else
                        oItem.Checked = False
                    End If
                End If

            Next
            DataGridView1.Refresh()
            RaiseEvent ValueChanged(Me, MatEventArgs.Empty)
        End If
    End Sub


    Private Function CheckedItems() As List(Of ControlItem)
        Dim retval As New List(Of ControlItem)
        For Each oItem As ControlItem In _ControlItems
            If oItem.Checked Then
                retval.Add(oItem)
            End If
        Next
        Return retval
    End Function

    Private Sub LoadGrid()
        With DataGridView1
            With .RowTemplate
                '.Height = DataGridView1.Font.Height * 1.3
            End With

            .AutoGenerateColumns = False
            .Columns.Clear()

            .DataSource = _ControlItems
            .SelectionMode = DataGridViewSelectionMode.CellSelect

            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = True
            .AllowUserToResizeRows = False
            .AllowDrop = False
            '.ReadOnly = True

            .Columns.Add(New DataGridViewCheckBoxColumn)
            With DirectCast(.Columns(Cols.Chk), DataGridViewCheckBoxColumn)
                .DataPropertyName = "Checked"
                .HeaderText = ""
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 20
                '.DefaultCellStyle.NullValue = Nothing
            End With

            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Eur)
                .HeaderText = "Nominal"
                .DataPropertyName = "Eur"
                .Width = 70
                .Width = 65
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
            End With

            .Columns.Add(New DataGridViewImageColumn)
            With DirectCast(.Columns(Cols.Warn), DataGridViewImageColumn)
                .HeaderText = ""
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 16
                .DefaultCellStyle.NullValue = Nothing
            End With

            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Vto)
                .HeaderText = "Venciment"
                .DataPropertyName = "Vto"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                .DefaultCellStyle.Format = "dd/MM/yy"
            End With

            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Clx)
                .HeaderText = "Lliurat"
                .DataPropertyName = "Clx"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
        End With
        SetContextMenu()
        _AllowEvents = True
    End Sub

    Private Sub DataGridView1_CellToolTipTextNeeded(sender As Object, e As DataGridViewCellToolTipTextNeededEventArgs) Handles DataGridView1.CellToolTipTextNeeded
        If e.RowIndex >= 0 Then
            Select Case e.ColumnIndex
                Case Cols.Warn

                    Dim oRow As DataGridViewRow = DirectCast(sender, DataGridView).Rows(e.RowIndex)
                    Dim oControlitem As ControlItem = oRow.DataBoundItem
                    Select Case oControlitem.Err
                        Case ControlItem.Errs.None
                            e.ToolTipText = ""
                        Case ControlItem.Errs.MissingIban
                            e.ToolTipText = "falta Iban"
                        Case ControlItem.Errs.MissingIbanDoc
                            e.ToolTipText = "falta mandat del iban"
                        Case ControlItem.Errs.Descuadre
                            e.ToolTipText = "no cuadra"
                    End Select
            End Select
        End If
    End Sub

    Private Sub DataGridView1_RowPrePaint(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowPrePaintEventArgs) Handles DataGridView1.RowPrePaint
        Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
        Dim oControlItem As ControlItem = oRow.DataBoundItem
        Dim oContactGuid As Guid = DirectCast(oControlItem.Source, DTOCsb).Contact.Guid
        Dim BlNegative As Boolean = _ControlItems.ToList.Exists(Function(x) DirectCast(x.Source, DTOCsb).Contact.Guid.Equals(oContactGuid) And DirectCast(x.Source, DTOCsb).Amt.Eur < 0)
        If BlNegative Then
            oRow.DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 153)
        Else
            oRow.DefaultCellStyle.BackColor = Color.WhiteSmoke
        End If
    End Sub

    Private Sub DataGridView1_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        Select Case e.ColumnIndex
            Case Cols.Warn
                Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                Dim oItem As ControlItem = oRow.DataBoundItem
                Select Case oItem.Err
                    Case ControlItem.Errs.None
                        e.Value = My.Resources.empty
                    Case Else
                        e.Value = My.Resources.warn_16
                End Select
        End Select
    End Sub

    Private Sub DataGridView1_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellValueChanged
        Select Case e.ColumnIndex
            Case Cols.Chk
                RaiseEvent ValueChanged(Me, MatEventArgs.Empty)
        End Select
    End Sub

    Private Sub DataGridView1_CurrentCellDirtyStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.CurrentCellDirtyStateChanged
        'provoca CellValueChanged a cada clic sense sortir de la casella
        Select Case DataGridView1.CurrentCell.ColumnIndex
            Case Cols.Chk
                DataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit)
        End Select
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

    Private Function SelectedItems() As List(Of DTOCsb)
        Dim retval As New List(Of DTOCsb)
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
            Dim oCsb As DTOCsb = oControlItem.Source

            If oControlItem IsNot Nothing Then
                Select Case DataGridView1.CurrentCell.ColumnIndex
                    Case Cols.Clx
                        Dim oMenuContact As New Menu_Contact(oCsb.Contact)
                        AddHandler oMenuContact.AfterUpdate, AddressOf RefreshRequest
                        oContextMenu.Items.AddRange(oMenuContact.Range)
                        oContextMenu.Items.Add("-")
                End Select
                'Dim oMenu_Csb As New Menu_Csb(SelectedItems.First)
            End If

            Dim oMenuItem As New ToolStripMenuItem("seleccionar")
            oContextMenu.Items.Add(oMenuItem)
            oMenuItem.DropDownItems.Add(New ToolStripMenuItem("sel.lecciona-l's", My.Resources.CheckedGrayed13, AddressOf SelectThese))
            oMenuItem.DropDownItems.Add(New ToolStripMenuItem("sel·lecciona-ho tot", My.Resources.Checked13, AddressOf SelectAll))
            oMenuItem.DropDownItems.Add(New ToolStripMenuItem("de-sel·lecciona la resta", My.Resources.CheckedGrayed13, AddressOf SelectRest))
            oMenuItem.DropDownItems.Add(New ToolStripMenuItem("de-sel·lecciona-ho tot", My.Resources.UnChecked13, AddressOf SelectNone))

        End If

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Do_AddNew()
        RaiseEvent RequestToAddNew(Me, MatEventArgs.Empty)
    End Sub


    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles DataGridView1.DoubleClick
        Dim oSelectedValue As DTOCsb = CurrentControlItem.Source
        'Dim oFrm As New Frm_Csb(oSelectedValue)
        'AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        'oFrm.Show()
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView1.SelectionChanged
        If _AllowEvents Then
            RaiseEvent ValueChanged(Me, MatEventArgs.Empty)
            SetContextMenu()
        End If
    End Sub

    Private Sub RefreshRequest()
        RaiseEvent RequestToRefresh(Me, MatEventArgs.Empty)
    End Sub


    Public Shadows Sub SelectAll()
        For Each oControlItem As ControlItem In _ControlItems
            oControlItem.Checked = True
        Next
        MyBase.Refresh()
        RaiseEvent ValueChanged(Me, MatEventArgs.Empty)
    End Sub

    Public Sub SelectNone()
        For Each oControlItem As ControlItem In _ControlItems
            oControlItem.Checked = False
        Next
        MyBase.Refresh()
        RaiseEvent ValueChanged(Me, MatEventArgs.Empty)
    End Sub

    Public Sub SelectThese()
        Dim selectedRowsIdxs = DataGridView1.SelectedCells.Cast(Of DataGridViewCell)().[Select](Function(x) x.RowIndex).Distinct().ToList()
        For Each oRow As DataGridViewRow In DataGridView1.Rows
            Dim oControlItem As ControlItem = oRow.DataBoundItem
            If selectedRowsIdxs.Contains(oRow.Index) Then
                oControlItem.Checked = True
            End If
        Next
        MyBase.Refresh()
        RaiseEvent ValueChanged(Me, MatEventArgs.Empty)
    End Sub

    Public Sub SelectRest()
        Dim selectedRowsIdxs = DataGridView1.SelectedCells.Cast(Of DataGridViewCell)().[Select](Function(x) x.RowIndex).Distinct().ToList()
        For Each oRow As DataGridViewRow In DataGridView1.Rows
            Dim oControlItem As ControlItem = oRow.DataBoundItem
            oControlItem.Checked = Not selectedRowsIdxs.Contains(oRow.Index)
        Next
        MyBase.Refresh()
        RaiseEvent ValueChanged(Me, MatEventArgs.Empty)
    End Sub

    Protected Class ControlItem
        Property Source As DTOCsb

        Property Checked As Boolean
        Property Eur As Decimal
        Property Vto As Date
        Property Clx As String
        Property Err As Errs

        Public Enum Errs
            None
            MissingIban
            MissingIbanDoc
            Descuadre
        End Enum

        Public Sub New(oCsb As DTOCsb)
            MyBase.New()
            _Source = oCsb
            With oCsb
                _Checked = False
                _Eur = .Amt.Eur
                _Vto = .Vto
                _Clx = .Contact.Nom
            End With

            If _Descuadres.ToList.Exists(Function(x) x.Contact.Guid.Equals(oCsb.Contact.Guid)) Then
                _Err = Errs.descuadre
            Else
                If oCsb.Iban Is Nothing Then
                    _Err = Errs.MissingIban
                Else
                    If oCsb.Iban.DocFile Is Nothing Then
                        _Err = Errs.MissingIbanDoc
                    Else
                        _Err = Errs.None
                    End If
                End If
            End If

        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class

