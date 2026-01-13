Public Class Frm_FairGuests
    Private _Evento As DTOEvento

    Private Sub Frm_FairGuests_Load(sender As Object, e As EventArgs) Handles Me.Load
        _Evento = BLL.BLLEvento.Find(New Guid("b3cfe56a-e923-47f3-b5d9-fc0c22ee672a"))
        TextBoxEvento.Text = _Evento.NomEsp
        refresca()
    End Sub

    Private Sub Xl_FairGuests1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_FairGuests1.RequestToAddNew
        Dim oFairGuest As New DTOFairGuest(_Evento)
        Dim oFrm As New Frm_FairGuest(oFairGuest)
        AddHandler oFrm.AfterUpdate, AddressOf refresca
        oFrm.Show()
    End Sub

    Private Sub Xl_FairGuests1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_FairGuests1.RequestToRefresh
        refresca()
    End Sub

    Private Sub refresca()
        Dim oFairGuests As List(Of DTOFairGuest) = BLL.BLLFairGuests.All(_Evento)
        Xl_FairGuests1.Load(oFairGuests)
        TextBoxCount.Text = Xl_FairGuests1.Count
    End Sub

    Private Sub TextBoxSearch_TextChanged(sender As Object, e As EventArgs) Handles TextBoxSearch.TextChanged
        Dim sSearchKey As String = TextBoxSearch.Text
        If sSearchKey.Length > 3 Then
            TextBoxSearch.ForeColor = Color.Black
            Xl_FairGuests1.Filter = sSearchKey
        Else
            Xl_FairGuests1.ClearFilter()
            TextBoxSearch.ForeColor = Color.Gray
        End If
        TextBoxCount.Text = Xl_FairGuests1.Count

    End Sub
End Class