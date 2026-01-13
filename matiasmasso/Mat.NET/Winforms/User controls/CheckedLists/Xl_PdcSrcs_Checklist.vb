Public Class Xl_PdcSrcs_Checklist

    Inherits DataGridView

    Private _allPurchaseOrders As List(Of DTOPurchaseOrder)
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean


    Public Event RequestToRefresh(sender As Object, e As MatEventArgs)
    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Private Enum Cols
        checked
        Nom
        Count
        Quota
    End Enum

    Public Shadows Sub Load(allPurchaseOrders As List(Of DTOPurchaseOrder), Optional selectedSources As List(Of DTOPurchaseOrder.Sources) = Nothing)
        If selectedSources Is Nothing Then selectedSources = New List(Of DTOPurchaseOrder.Sources)
        Static PropertiesSet As Boolean
        If Not PropertiesSet Then
            SetProperties()
            PropertiesSet = True
        End If

        _allPurchaseOrders = allPurchaseOrders
        Refresca(selectedSources)
    End Sub

    Private Sub Refresca(selectedSources As List(Of DTOPurchaseOrder.Sources))
        If selectedSources IsNot Nothing Then

            _AllowEvents = False

            Dim query = _allPurchaseOrders.GroupBy(Function(g) New With {Key g.Source}).
            Select(Function(group) New With {.source = group.Key.Source, .ordersCount = group.Count})

            _ControlItems = New ControlItems
            For Each oItem In query
                Dim oControlItem As New ControlItem(oItem.source)
                With oControlItem
                    .Checked = selectedSources.Exists(Function(x) x.Equals(oItem))
                    .Count = oItem.ordersCount
                    .Quota = .Count / _allPurchaseOrders.Count
                End With
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
        With MyBase.Columns(Cols.Nom)
            .HeaderText = "Fonts"
            .DataPropertyName = "Nom"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.[Count])
            .HeaderText = "Comandes"
            .DataPropertyName = "Count"
            .Width = 60
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#"
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Quota)
            .HeaderText = "Quota"
            .DataPropertyName = "Quota"
            .Width = 60
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#\%;-#\%;#"
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

    Public Function SelectedValues() As List(Of DTOPurchaseOrder.Sources)
        Dim retval As New List(Of DTOPurchaseOrder.Sources)
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
                    Dim oSources As List(Of DTOPurchaseOrder.Sources) = SelectedValues()
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
        Property Source As DTOPurchaseOrder.Sources
        Property Checked As Boolean
        Property Nom As String
        Property Count As Integer
        Property Quota As Decimal


        Public Sub New(value As DTOPurchaseOrder.Sources)
            MyBase.New()
            _Source = value

            Dim sRawNom As String = [Enum].Parse(GetType(DTOPurchaseOrder.Sources), value).ToString
            _Nom = sRawNom.Replace("_", " ")
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class




