Public Class Xl_ExcelFileSelect
    Public Event afterupdate(sender As Object, e As MatEventArgs)

    Property Filename As String
    Property Book As MatHelper.Excel.Book

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim oDlg As New OpenFileDialog
        With oDlg
            .Filter = "Excel|*.xls;*.xlsx|Tots els fitxers|*.*"
            If oDlg.ShowDialog = DialogResult.OK Then
                _Filename = oDlg.FileName
                TextBox1.Text = _Filename

                Dim exs As New List(Of Exception)
                Dim oBook = MatHelper.Excel.ClosedXml.Read(exs, _Filename)
                If exs.Count = 0 Then
                    RaiseEvent afterupdate(Me, New MatEventArgs(oBook))
                Else
                    UIHelper.WarnError(exs)
                End If

            End If
        End With
    End Sub

End Class
