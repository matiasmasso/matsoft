Public Class Frm_EdiRemadvs

    Private Sub Frm_EdiRemadvs_Load(sender As Object, e As EventArgs) Handles Me.Load
        refrescaHeaders()
    End Sub

    Private Sub refrescaHeaders()
        Dim oEdiRemadvs As EdiRemadvs = EdiRemadvsLoader.All()
        Xl_EdiRemadvs1.Load(oEdiRemadvs)
        refrescaItems()
    End Sub

    Private Sub refrescaItems()
        Dim oItems As New EdiRemadvItems
        Dim oEdiRemadv As EdiRemadv = Xl_EdiRemadvs1.Value
        If oEdiRemadv IsNot Nothing Then
            oItems = oEdiRemadv.Items
        End If
        Xl_EdiRemadvItems1.Load(oItems)
    End Sub

    Private Sub Xl_EdiRemadvs1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_EdiRemadvs1.RequestToRefresh
        refrescaHeaders()
    End Sub

    Private Sub Xl_EdiRemadvs1_ValueChanged(sender As Object, e As MatEventArgs) Handles Xl_EdiRemadvs1.ValueChanged
        refrescaItems()
    End Sub
End Class