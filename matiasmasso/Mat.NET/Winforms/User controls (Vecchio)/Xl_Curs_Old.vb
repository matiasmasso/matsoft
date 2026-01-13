Public Class Xl_Curs_Old

    Private _DataSource As List(Of DTOCur)
    Private _DefaultItem As DTOCur
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean
    Public Event onItemSelected(sender As Object, e As MatEventArgs)

    Private Enum Cols
        Id
        Nom
        Exchange
        ReverseExchange
    End Enum

    Public Shadows Sub Load(oDataSource As List(Of DTOCur), Optional oDefaultItem As DTOCur = Nothing)
        _DataSource = oDataSource
        _DefaultItem = oDefaultItem
        LoadGrid()
    End Sub

    Public Function SelectedItem() As DTOCur
        Dim oControlItem As ControlItem = CurrentItem()
        Dim retval As DTOCur = Nothing
        If oControlItem IsNot Nothing Then
            retval = oControlItem.Source
        End If
        Return retval
    End Function

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oCur As DTOCur = CurrentItem.Source
        Dim oMenu As New Menu_Cur(oCur)
        oContextMenu.Items.AddRange(oMenu.Range)
        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Function CurrentItem() As ControlItem
        Dim retval As ControlItem = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            retval = oRow.DataBoundItem
        End If
        Return retval
    End Function



    Private Sub LoadGrid()
        _AllowEvents = False

        _ControlItems = New ControlItems
        Dim oSelectedItem As ControlItem = Nothing
        For Each oCur As DTOCur In _DataSource
            Dim oControlItem As New ControlItem(oCur)
            If oCur.Equals(_DefaultItem) Then oSelectedItem = oControlItem
            _ControlItems.Add(oControlItem)
        Next

        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
            End With

            .ReadOnly = True
            .AllowUserToResizeRows = False
            .AllowUserToResizeColumns = False
            .RowHeadersVisible = False
            .ColumnHeadersVisible = True
            .AutoGenerateColumns = False
            .Columns.Clear()
            .DataSource = _ControlItems
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect


            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Id)
                .DataPropertyName = "Id"
                .HeaderText = "ISO"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 40
            End With

            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Nom)
                .DataPropertyName = "Nom"
                .HeaderText = "Divisa"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With

            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Exchange)
                .DataPropertyName = "Exchange"
                .HeaderText = "Euros"
                .Width = 50
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###0.0000 €;-#,###0.0000 €;#"
            End With

            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.ReverseExchange)
                .DataPropertyName = "ReverseExchange"
                .HeaderText = "Euro"
                .Width = 50
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###0.0000;-#,###0.0000;#"
            End With
        End With

        If oSelectedItem IsNot Nothing Then
            Dim iRow As Integer = _ControlItems.IndexOf(oSelectedItem)
            DataGridView1.CurrentCell = DataGridView1.Rows(iRow).Cells(Cols.Id)
        End If

        _AllowEvents = True
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs)
        If _AllowEvents Then
            SetContextMenu()
        End If
    End Sub

    Private Sub DataGridView1_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        'If e.ColumnIndex <> Cols.Id Then
        'e.CellStyle.SelectionBackColor = e.CellStyle.BackColor
        'e.CellStyle.SelectionForeColor = e.CellStyle.ForeColor
        'End If
    End Sub

    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles DataGridView1.DoubleClick
        Dim oControlItem As ControlItem = CurrentItem()
        Dim oCur As DTOCur = oControlItem.Source
        Dim oEventArgs As New MatEventArgs(oCur)
        RaiseEvent onItemSelected(Me, oEventArgs)
    End Sub

    Private Sub RefreshRequest(sender As Object, e As EventArgs)
        For Each oControlItem In _ControlItems
            If oControlItem.Source.Equals(sender) Then
                _DefaultItem = oControlItem.Source
                Exit For
            End If
        Next

        LoadGrid()
    End Sub



    Private Sub DataGridView1_SelectionChanged1(sender As Object, e As EventArgs) Handles DataGridView1.SelectionChanged
        If _AllowEvents Then
            SetContextMenu()
        End If
    End Sub

    Protected Class ControlItem
        Public Property Source As DTOCur
        Public Property Id As String
        Public Property Nom As String
        Public Property Exchange As Decimal
        Public Property ReverseExchange As Decimal

        Public Sub New(oCur As DTOCur)
            MyBase.New()
            _Source = oCur
            With oCur
                _Id = .Id
                _Nom = .Id.ToString
                '_Exchange = .Euros
                '_ReverseExchange = 1 / .Euros
            End With
        End Sub
    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class
