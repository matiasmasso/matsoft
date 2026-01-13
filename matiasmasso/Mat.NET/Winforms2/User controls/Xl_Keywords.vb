Public Class Xl_Keywords
    Private _KeywordList As List(Of String)
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Shadows Sub Load(KeywordList As List(Of String))
        ListBox1.Items.Clear()
        For Each s As String In KeywordList
            ListBox1.Items.Add(s)
        Next
        _AllowEvents = True
    End Sub

    Public ReadOnly Property Values As List(Of String)
        Get
            Dim retval As New List(Of String)
            For Each item In ListBox1.Items
                retval.Add(item)
            Next
            Return retval
        End Get
    End Property

    Private Sub ButtonAdd_Click(sender As Object, e As EventArgs) Handles ButtonAdd.Click
        ListBox1.Items.Add(TextBox1.Text)
        TextBox1.Clear()
        ButtonAdd.Enabled = False
        RaiseEvent AfterUpdate(Me, New MatEventArgs(TextBox1.Text))
    End Sub

    Private Sub ListBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles ListBox1.KeyDown
        If e.KeyCode = Keys.Delete Then
            ListBox1.Items.RemoveAt(ListBox1.SelectedIndex)
            RaiseEvent AfterUpdate(Me, New MatEventArgs(TextBox1.Text))
        End If
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        ButtonAdd.Enabled = True
    End Sub
End Class
