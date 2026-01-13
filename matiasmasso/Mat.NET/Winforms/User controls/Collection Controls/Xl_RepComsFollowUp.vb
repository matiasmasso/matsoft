Public Class Xl_RepComsFollowUp
    Inherits _Xl_ReadOnlyDatagridview

    Private _Values As List(Of DTOPurchaseOrder)
    Private _Lang As DTOLang

    Private _Filter As String
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)
    Public Event onItemSelected(sender As Object, e As MatEventArgs)

    Private Enum Cols
        Ico
        Period
        Ordered
        Delivered
        Invoiced
        Liquid
    End Enum

    Public Shadows Sub Load(values As List(Of DTOPurchaseOrder))
        Static PropertiesSet As Boolean
        If Not PropertiesSet Then
            SetProperties()
            PropertiesSet = True
        End If

        _Values = values
        _Lang = Current.Session.Lang
        Refresca()
    End Sub

    Private Sub Refresca()
        _AllowEvents = False

        Dim oFollowUps As List(Of DTORepComFollowUp) = DTORepComFollowUp.Months(_Values, _Lang)

        _ControlItems = New ControlItems
        For Each oFollowUp As DTORepComFollowUp In oFollowUps
            Dim oControlItem As New ControlItem(oFollowUp)
            _ControlItems.Add(oControlItem)
        Next

        Dim oCell As DTODatagridviewCell = UIHelper.DataGridviewCurrentCell(Me)
        MyBase.DataSource = _ControlItems
        UIHelper.SetDataGridviewCurrentCell(Me, oCell)

        SetContextMenu()
        _AllowEvents = True
    End Sub

    Public ReadOnly Property Value As DTORepComFollowUp
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As DTORepComFollowUp = oControlItem.Source
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

        MyBase.Columns.Add(New DataGridViewImageColumn)
        With DirectCast(MyBase.Columns(Cols.ico), DataGridViewImageColumn)
            .DataPropertyName = "Ico"
            .HeaderText = ""
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 16
            .DefaultCellStyle.NullValue = Nothing
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Period)
            .HeaderText = "Periode"
            .DataPropertyName = "Period"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Ordered)
            .HeaderText = "Comandes"
            .DataPropertyName = "Ordered"
            .Width = 70
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Delivered)
            .HeaderText = "Enviat"
            .DataPropertyName = "Delivered"
            .Width = 70
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Invoiced)
            .HeaderText = "Facturat"
            .DataPropertyName = "Invoiced"
            .Width = 70
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Liquid)
            .HeaderText = "Liquidat"
            .DataPropertyName = "Liquid"
            .Width = 70
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

    Private Function SelectedItems() As List(Of DTORepComFollowUp)
        Dim retval As New List(Of DTORepComFollowUp)
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
            If oControlItem.Level = DTORepComFollowUp.Levels.Order Then
                Dim oFollowUp As DTORepComFollowUp = SelectedItems.First
                Dim oOrder As DTOPurchaseOrder = oFollowUp.Source
                Dim oOrders As New List(Of DTOPurchaseOrder)
                oOrders.Add(oOrder)
                Dim oMenu_Pdc As New Menu_Pdc(oOrders)
                AddHandler oMenu_Pdc.AfterUpdate, AddressOf RefreshRequest
                oContextMenu.Items.AddRange(oMenu_Pdc.Range)
            End If
        End If

        MyBase.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Do_AddNew()
        RaiseEvent RequestToAddNew(Me, MatEventArgs.Empty)
    End Sub


    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles MyBase.DoubleClick
        Dim oControlItem As ControlItem = CurrentControlItem()
        If oControlItem IsNot Nothing Then
            Select Case oControlItem.Level
                Case DTORepComFollowUp.Levels.Month, DTORepComFollowUp.Levels.Day
                    oControlItem.Expanded = Not oControlItem.Expanded
                    If oControlItem.Expanded Then
                        Expand()
                    Else
                        Collapse()
                    End If
                Case DTORepComFollowUp.Levels.Order
                    Dim oFollowUp As DTORepComFollowUp = oControlItem.Source
                    Dim oOrder As DTOPurchaseOrder = oFollowUp.Source
                    Dim oFrm As New Frm_RepPdcFollowUp(oOrder)
                    oFrm.Show()
            End Select
        End If
    End Sub

    Private Sub Expand()
        Dim oControlItem As ControlItem = CurrentControlItem()
        Dim oParent As DTORepComFollowUp = oControlItem.Source

        Dim oFollowUps As List(Of DTORepComFollowUp) = Nothing
        Select Case oControlItem.Level
            Case DTORepComFollowUp.Levels.Month
                oFollowUps = DTORepComFollowUp.Days(_Values, oParent, _Lang)
            Case DTORepComFollowUp.Levels.Day
                oFollowUps = DTORepComFollowUp.Orders(_Values, oParent)
        End Select

        Dim index As Integer = _ControlItems.IndexOf(oControlItem)
        For Each oFollowup As DTORepComFollowUp In oFollowUps
            Dim oChild As New ControlItem(oFollowup)
            index += 1
            _ControlItems.Insert(index, oChild)
        Next
    End Sub

    Private Sub Collapse()
        Dim oControlItem As ControlItem = CurrentControlItem()
        Dim oParent As DTORepComFollowUp = oControlItem.Source
        For index As Integer = _ControlItems.Count - 1 To 0 Step -1
            If oParent.Equals(_ControlItems(index).Source.Parent) Then
                _ControlItems.RemoveAt(index)
            End If
        Next
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles MyBase.SelectionChanged
        If _AllowEvents Then
            RaiseEvent ValueChanged(Me, New MatEventArgs(CurrentControlItem.Source))
            SetContextMenu()
        End If
    End Sub

    Private Sub Xl_RepComsFollowUp_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles Me.CellFormatting
        Select Case e.ColumnIndex
            Case Cols.ico
                Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
                Dim oControlItem As ControlItem = oRow.DataBoundItem
                If oControlItem.Expanded Then
                    e.Value = My.Resources.minus
                Else
                    e.Value = My.Resources.PLUS
                End If
            Case Cols.Liquid
                Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
                If e.Value <> oRow.Cells(Cols.Invoiced).Value Then e.CellStyle.BackColor = Color.Yellow
            Case Cols.Invoiced
                Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
                If e.Value <> oRow.Cells(Cols.Delivered).Value Then e.CellStyle.BackColor = Color.Yellow
            Case Cols.Delivered
                Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
                If e.Value <> oRow.Cells(Cols.Ordered).Value Then e.CellStyle.BackColor = Color.Yellow
        End Select
    End Sub

    Protected Class ControlItem
        Property Source As DTORepComFollowUp

        Property Year As Integer
        Property Month As Integer
        Property Day As Integer
        Property Period As String
        Property Ordered As Decimal
        Property Delivered As Decimal
        Property Invoiced As Decimal
        Property Liquid As Decimal
        Property Level As DTORepComFollowUp.Levels
        Property Expanded As Boolean


        Public Sub New(oFollowUp As DTORepComFollowUp)
            MyBase.New()

            _Source = oFollowUp

            With oFollowUp
                _Level = .Level
                _Period = .Period
                _Ordered = .Ordered
                _Delivered = .Delivered
                _Invoiced = .Invoiced
                _Liquid = .Liquid
            End With

        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class


