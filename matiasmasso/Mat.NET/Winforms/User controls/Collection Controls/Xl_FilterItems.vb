Public Class Xl_FilterItems
    Inherits DataGridView

    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Private _DirtyCell As Boolean
    Private _LastValidatedObject As DTOContact

    Public Event onItemSelected(sender As Object, e As MatEventArgs)
    Public Event RequestToRemove(sender As Object, e As MatEventArgs)
    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Private Enum Cols
        Esp
        Cat
        Eng
        Por
    End Enum

    Public Shadows Sub Load(values As List(Of DTOFilter.Item))
        _ControlItems = New ControlItems
        For Each value In values
            Dim oControlItem As New ControlItem(value)
            _ControlItems.Add(oControlItem)
        Next

        LoadGrid()
        SetContextMenu()
    End Sub

    Public Function Values() As List(Of DTOFilter.Item)
        Dim retval As New List(Of DTOFilter.Item)
        For Each oControlItem In _ControlItems
            Dim value = oControlItem.Source
            With value.langText
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
        MyBase.AllowUserToAddRows = True
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

        'SetContextMenu()
        _AllowEvents = True
    End Sub

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip

        If MyBase.CurrentRow IsNot Nothing Then
            Dim iRowIdx As Integer = MyBase.CurrentRow.Index

            Dim oMenuUp = New ToolStripMenuItem("pujar", Nothing, AddressOf do_Up)
            oMenuUp.Enabled = (iRowIdx > 0)
            oContextMenu.Items.Add(oMenuUp)

            Dim oMenuDown = New ToolStripMenuItem("baixar", Nothing, AddressOf do_Down)
            oMenuDown.Enabled = (iRowIdx < _ControlItems.Count - 1)
            oContextMenu.Items.Add(oMenuDown)
        End If


        MyBase.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub do_Up()
        Dim iRowIdx As Integer = MyBase.CurrentRow.Index
        Dim oControlItem = _ControlItems(iRowIdx)
        _ControlItems(iRowIdx) = _ControlItems(iRowIdx - 1)
        _ControlItems(iRowIdx - 1) = oControlItem
        RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
    End Sub

    Private Sub do_Down()
        Dim iRowIdx As Integer = MyBase.CurrentRow.Index
        Dim oControlItem = _ControlItems(iRowIdx)
        _ControlItems(iRowIdx) = _ControlItems(iRowIdx + 1)
        _ControlItems(iRowIdx + 1) = oControlItem
        RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
    End Sub


    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles MyBase.SelectionChanged
        If _AllowEvents Then
            SetContextMenu()
        End If
    End Sub

    Private Sub Datagridview1_UserDeletedRow(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowEventArgs) Handles Me.UserDeletedRow
        RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
    End Sub

    Private Sub Datagridview1_UserAddedRow(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowEventArgs) Handles Me.UserAddedRow
        RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
    End Sub


    Protected Class ControlItem
        Public Property Source As DTOFilter.Item
        Public Property Esp As String
        Public Property Cat As String
        Public Property Eng As String
        Public Property Por As String

        Public Sub New()
            _Source = New DTOFilter.Item
        End Sub

        Public Sub New(item As DTOFilter.Item)
            MyBase.New()
            _Source = item
            With _Source.langText
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
                Dim oRow As DataGridViewRow = MyBase.CurrentRow
                If oRow IsNot Nothing Then
                    _ControlItems.Remove(oRow.DataBoundItem)
                    RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
                End If
        End Select
    End Sub

    Private Sub Xl_FilterItems_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles Me.CellValueChanged
        RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
    End Sub
End Class
