Public Class Xl_LangResources
    Inherits DataGridView

    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Private _DirtyCell As Boolean

    Public Event onItemSelected(sender As Object, e As MatEventArgs)
    Public Event RequestToRemove(sender As Object, e As MatEventArgs)
    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Private Enum Cols
        Src
        Esp
        Cat
        Eng
        Por
    End Enum

    Public Shadows Sub Load(values As List(Of DTOLangText))
        _ControlItems = New ControlItems
        For Each value In values
            Dim oControlItem As New ControlItem(value)
            _ControlItems.Add(oControlItem)
        Next

        LoadGrid()
    End Sub

    Public Function Values() As List(Of DTOLangText)
        Dim retval As New List(Of DTOLangText)
        For Each oControlItem In _ControlItems
            Dim value = oControlItem.Source
            With value
                .Esp = oControlItem.Esp
                .Cat = oControlItem.Cat
                .Eng = oControlItem.Eng
                .Por = oControlItem.Por
            End With
            retval.Add(value)
        Next
        Return retval
    End Function

    Private Sub LoadGrid()
        With MyBase.RowTemplate
            '.Height = MyBase.Font.Height * 1.3
        End With

        MyBase.ReadOnly = False
        MyBase.AllowUserToResizeRows = False
        MyBase.AllowUserToResizeColumns = False
        MyBase.AllowUserToAddRows = False
        MyBase.AllowUserToDeleteRows = True
        MyBase.RowHeadersVisible = False
        MyBase.ColumnHeadersVisible = True
        MyBase.AutoGenerateColumns = False
        MyBase.Columns.Clear()

        Dim oBindingSource As New BindingSource
        oBindingSource.AllowNew = True
        oBindingSource.DataSource = _ControlItems
        MyBase.DataSource = oBindingSource

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Src)
            .HeaderText = "Font"
            .DataPropertyName = "Src"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 50
            .ReadOnly = True
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Esp)
            .HeaderText = "Espanyol"
            .DataPropertyName = "Esp"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Cat)
            .HeaderText = "Català"
            .DataPropertyName = "Cat"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Eng)
            .HeaderText = "Anglès"
            .DataPropertyName = "Eng"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Por)
            .HeaderText = "Portuguès"
            .DataPropertyName = "Por"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End With

        _AllowEvents = True
    End Sub

    Private Sub Xl_FilterItems_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles Me.CellValueChanged
        If e.RowIndex >= 0 Then
            Dim oControlItem = _ControlItems(e.RowIndex)
            Dim oLangText = oControlItem.Source
            With oLangText
                .Esp = oControlItem.Esp
                .Cat = oControlItem.Cat
                .Eng = oControlItem.Eng
                .Por = oControlItem.Por
            End With
            RaiseEvent AfterUpdate(Me, New MatEventArgs(oLangText))
        End If
    End Sub

    Private Sub Xl_LangResources_DoubleClick(sender As Object, e As EventArgs) Handles Me.DoubleClick
        Dim oControlItem = _ControlItems(Me.CurrentRow.Index)
        Dim oLangText = oControlItem.Source
        Dim oFrm As New Frm_LangResource(oLangText)
        AddHandler oFrm.AfterUpdate, AddressOf onUpdate
        oFrm.Show()
    End Sub

    Private Sub onUpdate(sender As Object, e As MatEventArgs)
        Dim oControlItem = _ControlItems.FirstOrDefault(Function(x) x.Source.Equals(e.Argument))
        If oControlItem IsNot Nothing Then
            _ControlItems(_ControlItems.IndexOf(oControlItem)) = New ControlItem(e.Argument)
        End If
    End Sub

    Protected Class ControlItem
        Public Property Source As DTOLangText
        Public Property Src As String
        Public Property Esp As String
        Public Property Cat As String
        Public Property Eng As String
        Public Property Por As String

        Public Sub New()
            _Source = New DTOLangText
        End Sub

        Public Sub New(item As DTOLangText)
            MyBase.New()
            _Source = item
            With _Source
                _Src = .Src.ToString()
                _Esp = .Esp
                _Cat = .Cat
                _Eng = .Eng
                _Por = .Por
            End With
        End Sub

    End Class


    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

    Private Sub DataGridView1_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.Delete
                'Dim oRow As DataGridViewRow = MyBase.CurrentRow
                'If oRow IsNot Nothing Then
                ' _ControlItems.Remove(oRow.DataBoundItem)
                ' RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
                ' End If
        End Select
    End Sub


End Class

