Public Class Xl_XecsEnCartera
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event RequestToRefresh(sender As Object, e As MatEventArgs)
    Public Event ItemCheck(ByVal sender As Object, ByVal e As ItemCheckEventArgs)

    Private Enum Cols
        Check
        Vto
        Amt
        XecNum
        Banc
        Client
    End Enum

    Public WriteOnly Property DataSource As List(Of DTOXec)
        Set(value As List(Of DTOXec))
            _ControlItems = New ControlItems
            For Each oItem As DTOXec In value
                Dim oControlItem As New ControlItem(oItem)
                _ControlItems.Add(oControlItem)
            Next
            LoadGrid()
        End Set
    End Property

    Public ReadOnly Property SelectedValues As List(Of DTOXec)
        Get
            Dim retval As New List(Of DTOXec)
            For Each oItem As ControlItem In _ControlItems
                If oItem.CheckState = CheckState.Checked Then
                    retval.Add(oItem.Source)
                End If
            Next
            Return retval
        End Get
    End Property


    Private Sub LoadGrid()
        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
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
            .ReadOnly = True


            .Columns.Add(New DataGridViewImageColumn)
            With DirectCast(.Columns(Cols.Check), DataGridViewImageColumn)
                .HeaderText = ""
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 16
                .DefaultCellStyle.NullValue = Nothing
            End With

            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Vto)
                .DataPropertyName = "Vto"
                .HeaderText = "venciment"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 70
                .DefaultCellStyle.Format = "dd/MM/yy"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With

            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Amt)
                .DataPropertyName = "Amt"
                .HeaderText = "import"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 100
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With

            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.XecNum)
                .DataPropertyName = "XecNum"
                .HeaderText = "num.xec"
                .DefaultCellStyle.Padding = New Padding(10, 0, 0, 0)
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 100
            End With

            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Banc)
                .DataPropertyName = "Banc"
                .HeaderText = "Entitat"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 100
            End With

            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Client)
                .DataPropertyName = "Client"
                .HeaderText = "client"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With


        End With
        SetContextMenu()
        _AllowEvents = True
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

    Private Function SelectedItems() As List(Of DTOXec)
        Dim retval As New List(Of DTOXec)
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
            Dim oMenu_Xec As New Menu_Xec(SelectedItems.First)
            AddHandler oMenu_Xec.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_Xec.Range)
            oContextMenu.Items.Add("-")
        End If
        oContextMenu.Items.Add("Excel", My.Resources.Excel, AddressOf Do_Excel)

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub DataGridView1_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        Select Case e.ColumnIndex
            Case Cols.Check
                Dim oControlItem As ControlItem = DataGridView1.Rows(e.RowIndex).DataBoundItem
                If oControlItem.CheckState = CheckState.Checked Then
                    e.Value = My.Resources.Checked13
                Else
                    e.Value = My.Resources.UnChecked13
                End If
        End Select
    End Sub

    Private Sub DataGridView1_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView1.CellMouseClick
        Select Case e.ColumnIndex
            Case Cols.Check
                Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                Dim oControlItem As ControlItem = oRow.DataBoundItem
                Dim oCurrentCheckState As CheckState = oControlItem.CheckState
                Dim oNewCheckState As CheckState = IIf(oControlItem.CheckState = CheckState.Checked, CheckState.Unchecked, CheckState.Checked)
                oControlItem.CheckState = oNewCheckState
                DataGridView1.Refresh()
                Dim oArgs As New ItemCheckEventArgs(e.RowIndex, oNewCheckState, oCurrentCheckState)
                RaiseEvent ItemCheck(Me, oArgs)
        End Select
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView1.SelectionChanged
        If _AllowEvents Then
            SetContextMenu()
        End If
    End Sub

    Private Sub Do_Excel()
        Dim oSheet = UIHelper.GetExcelFromDataGridView(DataGridView1)
        Dim exs As New List(Of Exception)
        If Not UIHelper.ShowExcel(oSheet, exs) Then
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub RefreshRequest()
        RaiseEvent RequestToRefresh(Me, MatEventArgs.Empty)
    End Sub

    Protected Class ControlItem

        Public Property Source As DTOXec

        Public Property CheckState As CheckState
        Public Property vto As Date
        Public Property Amt As Decimal
        Public Property XecNum As String
        Public Property Banc As String
        Public Property Client As String

        Public Sub New(oXec As DTOXec)
            MyBase.New()
            _Source = oXec
            _CheckState = CheckState.Unchecked

            With oXec
                _vto = .Vto
                _Amt = .Amt.Eur
                _XecNum = .XecNum
                _Banc = DTOIban.BankNom(.Iban)
                _Client = .Lliurador.FullNom
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class


End Class

