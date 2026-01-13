Public Class Xl_DistributionChannels_Checklist
    Inherits DataGridView

    Private _allDistributionChannels As List(Of DTODistributionChannel)
    Private _ContDistributionChannelItems As ContDistributionChannelItems
    Private _AllowEvents As Boolean


    Public Event RequestToRefresh(sender As Object, e As MatEventArgs)
    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Private Enum Cols
        checked
        Nom
    End Enum

    Public Shadows Sub Load(allDistributionChannels As List(Of DTODistributionChannel), selectedDistributionChannels As List(Of DTODistributionChannel))
        Static PropertiesSet As Boolean
        If Not PropertiesSet Then
            SetProperties()
            PropertiesSet = True
        End If

        _allDistributionChannels = allDistributionChannels
        Refresca(selectedDistributionChannels)
    End Sub

    Private Sub Refresca(selectedDistributionChannels As List(Of DTODistributionChannel))
        If selectedDistributionChannels IsNot Nothing Then

            _AllowEvents = False

            _ContDistributionChannelItems = New ContDistributionChannelItems
            For Each oItem As DTODistributionChannel In _allDistributionChannels
                Dim oContDistributionChannelItem As New ContDistributionChannelItem(oItem)
                oContDistributionChannelItem.Checked = selectedDistributionChannels.Exists(Function(x) x.Equals(oItem))
                _ContDistributionChannelItems.Add(oContDistributionChannelItem)
            Next

            MyBase.DataSource = _ContDistributionChannelItems
            If _ContDistributionChannelItems.Count > 0 Then
                MyBase.CurrentCell = MyBase.FirstDisplayedCell
            End If

            _AllowEvents = True
        End If
    End Sub

    Private Sub SetProperties()
        'MyBase.RowTemplate.Height = MyBase.Font.Height * 1.3

        MyBase.AutoGenerateColumns = False
        MyBase.Columns.Clear()

        MyBase.DataSource = _ContDistributionChannelItems
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
            .HeaderText = "Canals de distribució"
            .DataPropertyName = "Nom"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End With
    End Sub

    Private Function SelectedControls() As ContDistributionChannelItems
        Dim retval As New ContDistributionChannelItems
        For Each oRow As DataGridViewRow In MyBase.SelectedRows
            Dim oContDistributionChannelItem As ContDistributionChannelItem = oRow.DataBoundItem
            retval.Add(oContDistributionChannelItem)
        Next

        If retval.Count = 0 Then retval.Add(CurrentContDistributionChannelItem)
        Return retval
    End Function

    Public Function SelectedValues() As List(Of DTODistributionChannel)
        Dim retval As New List(Of DTODistributionChannel)
        For Each oRow As DataGridViewRow In MyBase.Rows
            Dim oContDistributionChannelItem As ContDistributionChannelItem = oRow.DataBoundItem
            If oContDistributionChannelItem.Checked Then
                retval.Add(oContDistributionChannelItem.Source)
            End If
        Next
        Return retval
    End Function

    Private Function CurrentContDistributionChannelItem() As ContDistributionChannelItem
        Dim retval As ContDistributionChannelItem = Nothing
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
                    Dim oDistributionChannel As DTODistributionChannel = _allDistributionChannels(e.RowIndex)
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

    Protected Class ContDistributionChannelItem
        Property Source As DTODistributionChannel
        Property Checked As Boolean
        Property Nom As String

        Public Sub New(value As DTODistributionChannel)
            MyBase.New()
            _Source = value
            With value
                _Nom = .LangText.Tradueix(Current.Session.Lang)
            End With
        End Sub

    End Class

    Protected Class ContDistributionChannelItems
        Inherits SortableBindingList(Of ContDistributionChannelItem)
    End Class

End Class



