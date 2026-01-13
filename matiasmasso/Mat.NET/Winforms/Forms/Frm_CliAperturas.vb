Public Class Frm_CliAperturas
    Private _Values As DTOCliApertura.Collection
    Private _AllowEvents As Boolean

    Private Sub Frm_CliAperturas_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        refresca()
        _AllowEvents = True
    End Sub


    Private Sub TextBoxSearch_TextChanged(sender As Object, e As EventArgs) Handles TextBoxSearch.TextChanged
        If _AllowEvents Then
            Dim sSearchKey As String = TextBoxSearch.Text
            If sSearchKey.Length > 3 Then
                TextBoxSearch.ForeColor = Color.Black
                Xl_CliAperturas1.Filter = sSearchKey
            Else
                Xl_CliAperturas1.ClearFilter()
                TextBoxSearch.ForeColor = Color.Gray
            End If
        End If
    End Sub

    Private Sub Xl_CliAperturas1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_CliAperturas1.RequestToRefresh
        refresca()
    End Sub

    Private Async Sub refresca()
        Dim exs As New List(Of Exception)
        _Values = Await FEB2.CliAperturas.All(Current.Session.User, exs)
        If exs.Count = 0 Then
            Xl_CliAperturas1.Load(_Values)
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub
End Class