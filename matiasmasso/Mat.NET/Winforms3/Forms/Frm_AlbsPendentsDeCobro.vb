Public Class Frm_AlbsPendentsDeCobro
    Private _RetencioCod As DTODelivery.RetencioCods
    Private _AllowEvents As Boolean

    Public Sub New(oRetencioCod As DTODelivery.RetencioCods)
        InitializeComponent()
        _RetencioCod = oRetencioCod
    End Sub

    Private Async Sub Frm_AlbsTransferenciaPrevia_Load(sender As Object, e As EventArgs) Handles Me.Load
        ComboBoxRetencioCod.SelectedIndex = _RetencioCod
        Await refresca()
        _AllowEvents = True
    End Sub

    Private Async Function refresca() As Task
        Dim exs As New List(Of Exception)
        Dim oDeliveries = Await FEB.Deliveries.Headers(exs, Current.Session.Emp, pendentsDeCobro:=CurrentRetencioCod)
        If exs.Count = 0 Then
            Xl_Deliveries1.Load(oDeliveries, Xl_Deliveries.Purposes.PendentsDeCobro)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Async Sub Xl_Deliveries1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_Deliveries1.RequestToRefresh
        Await refresca()
    End Sub

    Private Async Sub ComboBoxRetencioCod_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxRetencioCod.SelectedIndexChanged
        If _AllowEvents Then
            Await refresca()
        End If
    End Sub

    Private Function CurrentRetencioCod() As DTODelivery.RetencioCods
        Dim retval As DTODelivery.RetencioCods = ComboBoxRetencioCod.SelectedIndex
        Return retval
    End Function

End Class