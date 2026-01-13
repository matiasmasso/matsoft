Public Class Frm_Raffles

    Private Sub Frm_Raffles_Load(sender As Object, e As EventArgs) Handles Me.Load
        refresca()
    End Sub

    Private Sub Xl_Raffles1_RefreshToRequest(sender As Object, e As MatEventArgs) Handles Xl_Raffles1.RequestToRefresh
        refresca()
    End Sub

    Private Sub refresca()
        Dim oContestBases As List(Of DTORaffle) = BLL.BLLRaffles.AllRaffleHeaders
        Xl_Raffles1.Load(oContestBases)
    End Sub

End Class