Public Class Xl_BriqQuestions

    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)
    Public Event RequestToRefresh(sender As Object, e As MatEventArgs)

    Private Enum Cols
        Question
        AnswerTrue
        AnswerFalse
    End Enum

    Public Shadows Sub Load(value As BriqQuestions)
        _ControlItems = New ControlItems
        For Each oItem As BriqQuestion In value
            Dim oControlItem As New ControlItem(oItem)
            _ControlItems.Add(oControlItem)
        Next
        LoadGrid()
    End Sub

    Public ReadOnly Property Value As BriqQuestion
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As BriqQuestion = oControlItem.Source
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
            With .Columns(Cols.Question)
                .HeaderText = "Pregunta"
                .DataPropertyName = "Question"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            .Columns.Add(New DataGridViewImageColumn)
            With CType(.Columns(Cols.AnswerTrue), DataGridViewImageColumn)
                .DataPropertyName = "AnswerTrue"
                .HeaderText = "si"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 20
                .DefaultCellStyle.NullValue = Nothing
            End With
            .Columns.Add(New DataGridViewImageColumn)
            With CType(.Columns(Cols.AnswerFalse), DataGridViewImageColumn)
                .DataPropertyName = "AnswerFalse"
                .HeaderText = "no"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 20
                .DefaultCellStyle.NullValue = Nothing
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

    Private Function SelectedItems() As BriqQuestions
        Dim retval As New BriqQuestions
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
            Dim oMenu_BriqQuestion As New Menu_BriqQuestion(SelectedItems.First)
            AddHandler oMenu_BriqQuestion.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_BriqQuestion.Range)
            oContextMenu.Items.Add("-")
        End If
        oContextMenu.Items.Add("afegir nova pregunta", Nothing, AddressOf Do_AddNew)

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles DataGridView1.DoubleClick
        Dim oQuestion As BriqQuestion = CurrentControlItem.Source
        Dim oFrm As New Frm_BriqQuestion(oQuestion)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView1.SelectionChanged
        If _AllowEvents Then
            SetContextMenu()
        End If
    End Sub

    Private Sub Do_AddNew()
        RaiseEvent RequestToAddNew(Me, MatEventArgs.Empty)
    End Sub

    Private Sub RefreshRequest()
        RaiseEvent RequestToRefresh(Me, MatEventArgs.Empty)
    End Sub

    Protected Class ControlItem
        Public Property Source As BriqQuestion

        Public Property Question As String
        Public Property AnswerTrue As Image
        Public Property AnswerFalse As Image

        Public Sub New(oBriqQuestion As BriqQuestion)
            MyBase.New()
            _Source = oBriqQuestion
            With oBriqQuestion
                _Question = .Text
                If .Answer = MaxiSrvr.TriState.Verdadero Then
                    _AnswerTrue = My.Resources.Checked13
                    _AnswerFalse = My.Resources.UnChecked13
                Else
                    _AnswerTrue = My.Resources.UnChecked13
                    _AnswerFalse = My.Resources.Checked13
                End If
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class
