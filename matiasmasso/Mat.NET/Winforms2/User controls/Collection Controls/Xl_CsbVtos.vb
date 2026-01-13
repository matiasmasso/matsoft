Public Class Xl_CsbVtos
    Inherits DataGridView

    Private _Csbs As List(Of DTOCsb)
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event RequestToRefresh(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)

    Private Enum Cols
        Ico
        Fch
        Amt
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

    Private Sub Refresca()

        _AllowEvents = False

        _ControlItems = New ControlItems
        Dim oControlItem As New ControlItem()
        For Each item As DTOCsb In _Csbs
            If item.Vto <> oControlItem.Fch Then
                oControlItem = New ControlItem(item)
                _ControlItems.Add(oControlItem)
            End If

            oControlItem.AddCsb(item)
        Next

        Dim oCell As DTODatagridviewCell = UIHelper.DataGridviewCurrentCell(Me)
        MyBase.DataSource = _ControlItems
        UIHelper.SetDataGridviewCurrentCell(Me, oCell)

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

        MyBase.Columns.Add(New DataGridViewImageColumn)
        With DirectCast(MyBase.Columns(Cols.Ico), DataGridViewImageColumn)
            .DataPropertyName = "Ico"
            .HeaderText = ""
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 16
            .DefaultCellStyle.NullValue = Nothing
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Fch)
            .HeaderText = "Venciment"
            .DataPropertyName = "Fch"
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

    End Sub

    Public ReadOnly Property SelectedVtoCsbs As List(Of DTOCsb)
        Get
            Dim retval As New List(Of DTOCsb)
            Dim oControlitem As ControlItem = CurrentControlItem()
            If oControlitem IsNot Nothing Then
                retval = oControlitem.Source
            End If
            Return retval
        End Get
    End Property

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


    Private Sub RefreshRequest()
        RaiseEvent RequestToRefresh(Me, MatEventArgs.Empty)
    End Sub

    Private Sub Xl_CsbVtos_Checklist_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles Me.CellFormatting
        Select Case e.ColumnIndex
            Case Cols.Ico
                Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
                Dim oControlitem As ControlItem = oRow.DataBoundItem
                Dim oCsbs As List(Of DTOCsb) = oControlitem.Source
                Dim warn As Boolean = oCsbs.Exists(Function(x) x.ExceptionCode <> DTOCsb.ExceptionCodes.Success)
                If warn Then
                    e.Value = My.Resources.warn
                End If
        End Select
    End Sub

    Private Sub DataGridView_SelectionChanged(sender As Object, e As EventArgs) Handles MyBase.SelectionChanged
        If _AllowEvents Then
            RaiseEvent ValueChanged(Me, New MatEventArgs(CurrentControlItem.Source))
        End If
    End Sub

    Private Sub Xl_CsbVtos_CellToolTipTextNeeded(sender As Object, e As DataGridViewCellToolTipTextNeededEventArgs) Handles Me.CellToolTipTextNeeded
        If e.RowIndex >= 0 Then
            Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
            Dim oControlitem As ControlItem = oRow.DataBoundItem
            Dim oCsbs As List(Of DTOCsb) = oControlitem.Source
            Dim Query = oCsbs.GroupBy(Function(x) New With {Key x.ExceptionCode}).Select(Function(group) New With {.ExceptionCode = group.Key.ExceptionCode, .Count = group.Count})
            Dim sb As New System.Text.StringBuilder
            For Each oItem In Query
                If oItem.ExceptionCode <> DTOCsb.ExceptionCodes.Success Then
                    sb.AppendLine(String.Format("{0} x {1}", oItem.Count, DTOCsb.ValidationText(oItem.ExceptionCode)))
                End If
            Next
            e.ToolTipText = sb.ToString
        End If
    End Sub

    Protected Class ControlItem
        Property Source As List(Of DTOCsb)
        Property Fch As Date
        Property Amt As Decimal

        Public Sub New(Optional value As DTOCsb = Nothing)
            MyBase.New()
            _Source = New List(Of DTOCsb)
            If value IsNot Nothing Then
                _Fch = value.Vto
            End If
        End Sub

        Public Sub AddCsb(item As DTOCsb)
            _Source.Add(item)
            _Amt += item.Amt.Eur


        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class


