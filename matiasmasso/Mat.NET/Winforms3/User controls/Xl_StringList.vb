Public Class Xl_StringList
    Inherits DataGridView

    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Shadows Sub Load(values As List(Of String))
        SetProperies()
        If values IsNot Nothing Then
            For Each value In values
                Dim oRow As String() = {value}
                MyBase.Rows.Add(oRow)
            Next
            MyBase.ClearSelection()
        End If
        _AllowEvents = True
    End Sub

    Public ReadOnly Property values As List(Of String)
        Get
            Dim retval As New List(Of String)
            For Each oRow As DataGridViewRow In MyBase.Rows
                Dim s As String = oRow.Cells(0).Value
                If Not String.IsNullOrEmpty(s) Then
                    retval.Add(s)
                End If
            Next
            Return retval
        End Get
    End Property

    Private Sub SetProperies()
        MyBase.AllowUserToAddRows = True
        MyBase.AllowUserToDeleteRows = True
        MyBase.ReadOnly = False
        MyBase.RowTemplate.Height = MyBase.Font.Height * 1.3
        MyBase.RowHeadersVisible = False
        MyBase.ColumnHeadersVisible = False
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(0)
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End With
    End Sub

    Private Sub Xl_StringList_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Delete Then
            MyBase.Rows.RemoveAt(MyBase.CurrentCell.RowIndex)
        End If
    End Sub

    Private Sub Xl_StringList_UserAddedRow(sender As Object, e As DataGridViewRowEventArgs) Handles Me.UserAddedRow, Me.UserDeletedRow
        If _AllowEvents Then
            RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
        End If
    End Sub

    Private Sub Xl_StringList_CellValueChanged(sender As Object, e As DataGridViewCellValidatingEventArgs) Handles Me.CellValidating
        If _AllowEvents Then
            Dim amendedValue = e.FormattedValue
            Dim previousValue = MyBase.Rows(e.RowIndex).Cells(0).Value
            If amendedValue <> previousValue Then
                RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
            End If
        End If
    End Sub
End Class

