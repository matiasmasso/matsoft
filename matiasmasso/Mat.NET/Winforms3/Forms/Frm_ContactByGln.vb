Public Class Frm_ContactByGln
    Private Async Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        Dim exs As New List(Of Exception)
        Dim searchterm As String = TextBox1.Text
        If searchterm.Length = 13 Then
            Dim oContact = Await FEB.Contact.FromGln(searchterm, exs)
            If exs.Count = 0 Then
                If Not Await Xl_Contact21.Load(exs, oContact) Then
                    UIHelper.WarnError(exs)
                End If
            Else
                UIHelper.WarnError(exs)
            End If
        End If
    End Sub
End Class