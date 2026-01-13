Public Class Frm_FlatFile
    Private _FlatFile As FlatFile
    Private Sub ImportarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ImportarToolStripMenuItem.Click
        Dim oDlg As New OpenFileDialog
        With oDlg
            If .ShowDialog = DialogResult.OK Then
                _FlatFile = FlatFile.Factory(.FileName, fieldLengths())
                Xl_FlatFileSegments1.Load(_FlatFile)
            End If
        End With
    End Sub

    Private Sub TextBoxFieldLengths_TextChanged(sender As Object, e As EventArgs) Handles TextBoxFieldLengths.TextChanged
        If _FlatFile IsNot Nothing Then
            _FlatFile.FieldLengths = fieldLengths()
            Xl_FlatFileSegments1.Load(_FlatFile)
        End If
    End Sub

    Private Function fieldLengths() As List(Of Integer)
        Dim src = TextBoxFieldLengths.Text
        If src = "" Then src = 10
        Dim retval As New List(Of Integer)
        For Each s In src.Split(",")
            If IsNumeric(s) Then retval.Add(CInt(s))
        Next
        Return retval
    End Function
End Class