Public Class Frm_PncDescuadres
    Private Async Sub Frm_PncDescuadres_Load(sender As Object, e As EventArgs) Handles Me.Load
        Await refresca()
    End Sub

    Private Async Function refresca() As Task
        Dim exs As New List(Of Exception)
        Dim items = Await FEB.PurchaseOrderItems.Descuadres(exs, Current.Session.User)
        If exs.Count = 0 Then
            Xl_PncDescuadres1.Load(items)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Async Sub Xl_PncDescuadres1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_PncDescuadres1.RequestToRefresh
        Await refresca()
    End Sub
End Class