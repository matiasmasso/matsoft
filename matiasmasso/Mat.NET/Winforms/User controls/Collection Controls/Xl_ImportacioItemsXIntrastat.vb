Public Class Xl_ImportacioItemsXIntrastat
    Inherits DataGridView

    Private _allImportacioItems As List(Of DTOImportacioItem)
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean


    Public Event RequestToRefresh(sender As Object, e As MatEventArgs)
    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Private Enum Cols
        checked
        num
        fch
        amt
        text
    End Enum

    Public Shadows Sub Load(allImportacioItems As List(Of DTOImportacioItem), selectedImportacioItems As List(Of DTOImportacioItem))
        Static PropertiesSet As Boolean
        If Not PropertiesSet Then
            SetProperties()
            PropertiesSet = True
        End If

        _allImportacioItems = allImportacioItems
        Refresca(selectedImportacioItems)
    End Sub

    Private Sub Refresca(selectedImportacioItems As List(Of DTOImportacioItem))
        If selectedImportacioItems IsNot Nothing Then

            _AllowEvents = False

            _ControlItems = New ControlItems
            For Each oItem As DTOImportacioItem In _allImportacioItems
                Dim oControlItem As New ControlItem(oItem)
                oControlItem.Checked = selectedImportacioItems.Exists(Function(x) x.Equals(oItem))
                _ControlItems.Add(oControlItem)
            Next

            MyBase.DataSource = _ControlItems
            If _ControlItems.Count > 0 Then
                MyBase.CurrentCell = MyBase.FirstDisplayedCell
            End If

            _AllowEvents = True
        End If
    End Sub

    Private Sub SetProperties()
        MyBase.RowTemplate.Height = MyBase.Font.Height * 1.5

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
        With MyBase.Columns(Cols.Num)
            .HeaderText = "Remesa"
            .DataPropertyName = "Num"
            .Width = 60
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.fch)
            .HeaderText = "Data"
            .DataPropertyName = "Fch"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 60
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            .DefaultCellStyle.Format = "dd/MM/yy"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.amt)
            .HeaderText = "Import"
            .DataPropertyName = "Eur"
            .Width = 70
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.text)
            .HeaderText = "Concepte"
            .DataPropertyName = "Txt"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End With
    End Sub

    Private Function SelectedControls() As ControlItems
        Dim retval As New ControlItems
        For Each oRow As DataGridViewRow In MyBase.SelectedRows
            Dim oControlItem As ControlItem = oRow.DataBoundItem
            retval.Add(oControlItem)
        Next

        If retval.Count = 0 Then retval.Add(CurrentControlItem)
        Return retval
    End Function

    Public Function SelectedValues() As List(Of DTOImportacioItem)
        Dim retval As New List(Of DTOImportacioItem)
        For Each oRow As DataGridViewRow In MyBase.Rows
            Dim oControlItem As ControlItem = oRow.DataBoundItem
            If oControlItem.Checked Then
                retval.Add(oControlItem.Source)
            End If
        Next
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


    Private Sub DataGridView1_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles MyBase.CellValueChanged
        Select Case e.ColumnIndex
            Case Cols.checked
                If _AllowEvents Then
                    Dim oImportacioItem As DTOImportacioItem = _allImportacioItems(e.RowIndex)
                    RaiseEvent AfterUpdate(Me, New MatEventArgs(SelectedValues))
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
        RaiseEvent RequestToRefresh(Me, MatEventArgs.Empty)
    End Sub

    Protected Class ControlItem
        Property Source As DTOImportacioItem
        Property Checked As Boolean
        Property Num As Integer
        Property Fch As Date
        Property Eur As Decimal
        Property Txt As String

        Public Sub New(value As DTOImportacioItem)
            MyBase.New()
            _Source = value
            With value
                _Num = .Parent.Id
                _Fch = DirectCast(.Tag, DTOCca).Fch
                _Eur = .Amt.Eur
                _Txt = .Descripcio
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class



