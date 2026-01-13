Public Class Frm_EdiversaRemadvs

    Private Async Sub Frm_EdiversaRemadvs_Load(sender As Object, e As EventArgs) Handles Me.Load
        Await refrescaHeaders()
    End Sub

    Private Async Function refrescaHeaders() As Task
        Dim exs As New List(Of Exception)
        Dim oEdiversaRemadvs = Await FEB.EdiversaRemadvs.All(Xl_EdiRemadvs1.DisplayObsolets, exs)
        If exs.Count = 0 Then
            Xl_EdiRemadvs1.Load(oEdiversaRemadvs)
            Await refrescaItems()
        Else
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Async Function refrescaItems() As Task
        Dim oItems As New List(Of DTOEdiversaRemadvItem)
        Dim oEdiversaRemadv As DTOEdiversaRemadv = Xl_EdiRemadvs1.Value
        If oEdiversaRemadv IsNot Nothing Then
            oItems = oEdiversaRemadv.Items
        End If
        Await Xl_EdiRemadvItems1.Load(oItems)
    End Function

    Private Async Sub Xl_EdiRemadvItems1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_EdiRemadvItems1.RequestToRefresh
        Await refrescaItems()
    End Sub

    Private Async Sub Xl_EdiversaRemadvs1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_EdiRemadvs1.RequestToRefresh
        Await refrescaHeaders()
        Await refrescaItems()
    End Sub

    Private Async Sub Xl_EdiversaRemadvs1_ValueChanged(sender As Object, e As MatEventArgs) Handles Xl_EdiRemadvs1.ValueChanged
        Await refrescaItems()
    End Sub
End Class