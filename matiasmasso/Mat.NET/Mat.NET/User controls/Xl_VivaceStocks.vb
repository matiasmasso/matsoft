Public Class Xl_VivaceStocks
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event RequestToRefresh(sender As Object, e As MatEventArgs)
    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)

    Private Enum Cols
        Referencia
        Descripcio
        Stock
        Ubicacio
        FchEntrada
        LastMove
    End Enum

    Public Shadows Sub Load(values As List(Of DTOVivaceStock))
        _ControlItems = New ControlItems
        For Each oItem As DTOVivaceStock In values
            Dim oControlItem As New ControlItem(oItem)
            _ControlItems.Add(oControlItem)
        Next
        LoadGrid()
    End Sub

    Public ReadOnly Property Value As DTOVivaceStock
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As DTOVivaceStock = oControlItem.Source
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
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = True
            .AllowUserToResizeRows = False
            .AllowDrop = False
            .ReadOnly = True

            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Referencia)
                .HeaderText = "Referencia"
                .DataPropertyName = "Referencia"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 100
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Descripcio)
                .HeaderText = "Descripcio"
                .DataPropertyName = "Descripcio"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Stock)
                .HeaderText = "Stock"
                .DataPropertyName = "Stock"
                .Width = 60
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#"
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Ubicacio)
                .HeaderText = "Ubicacio"
                .DataPropertyName = "Ubicacio"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 100
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.FchEntrada)
                .HeaderText = "Entrada"
                .DataPropertyName = "FchEntrada"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                .DefaultCellStyle.Format = "dd/MM/yy"
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.LastMove)
                .HeaderText = "Ult.Moviment"
                .DataPropertyName = "LastMove"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                .DefaultCellStyle.Format = "dd/MM/yy"
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

    Private Function SelectedItems() As List(Of DTOVivaceStock)
        Dim retval As New List(Of DTOVivaceStock)
        For Each oRow As DataGridViewRow In DataGridView1.SelectedRows
            Dim oControlItem As ControlItem = oRow.DataBoundItem
            retval.Add(oControlItem.Source)
        Next

        If retval.Count = 0 Then
            Dim oItem As ControlItem = CurrentControlItem()
            If oItem IsNot Nothing Then
                retval.Add(CurrentControlItem.Source)
            End If
        End If
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
        Dim oSelectedItems As List(Of DTOVivaceStock) = SelectedItems()

        Select Case oSelectedItems.Count
            Case 0
            Case 1
                Dim oControlItem As ControlItem = CurrentControlItem()
                If IsNumeric(oControlItem.Referencia) Then
                    Dim oArt As Art = Art.FromNum(BLL.BLLApp.Emp, oControlItem.Referencia)
                    Dim oMenu As New Menu_Art(oArt)
                    oContextMenu.Items.AddRange(oMenu.Range)
                End If
            Case Else
                Dim oMenu As New Menu_VivaceStock(oSelectedItems)
                oContextMenu.Items.AddRange(oMenu.Range)
        End Select
        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub DataGridView1_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        Select Case e.ColumnIndex
            Case Cols.LastMove
                If e.Value < Today.AddDays(-365) Then
                    e.CellStyle.BackColor = Color.Yellow
                End If
        End Select
    End Sub


    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles DataGridView1.DoubleClick

    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView1.SelectionChanged
        If _AllowEvents Then
            RaiseEvent ValueChanged(Me, New MatEventArgs(CurrentControlItem.Source))
            SetContextMenu()
        End If
    End Sub

    Private Sub RefreshRequest()
        RaiseEvent RequestToRefresh(Me, MatEventArgs.Empty)
    End Sub

    Protected Class ControlItem
        Property Source As DTOVivaceStock

        Property Referencia As String
        Property Descripcio As String
        Property Stock As Integer
        Property Ubicacio As String
        Property FchEntrada As Date
        Property LastMove As Date

        Public Sub New(item As DTOVivaceStock)
            MyBase.New()
            _Source = item
            With item
                _Referencia = .Referencia
                _Descripcio = .Descripcio
                _Stock = .Stock
                _Ubicacio = .Ubicacio
                _FchEntrada = .FchEntrada
                _LastMove = .LastMove
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class

