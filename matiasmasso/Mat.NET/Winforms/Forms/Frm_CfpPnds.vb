Public Class Frm_CfpPnds

    Private _Values As List(Of DTOPnd)

    Private Async Sub Frm_Load(sender As Object, e As EventArgs) Handles Me.Load
        Await refresca()
    End Sub

    Private Async Function refresca() As Task
        Dim exs As New List(Of Exception)
        _Values = Await FEB2.Pnds.Pending(exs, Current.Session.Emp)
        If exs.Count = 0 Then
            LoadMaster()
            LoadDetail()
        Else
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Sub LoadMaster()
        Xl_CfpPndsMaster1.Load(_Values)
    End Sub

    Private Sub LoadDetail()
        Dim oCfp As DTOPnd.FormasDePagament = Xl_CfpPndsMaster1.Value
        Xl_CfpPndsDetail1.Load(_Values.FindAll(Function(x) x.Cfp = oCfp))
    End Sub

    Private Sub Xl_CfpPndsMaster1_onItemSelected(sender As Object, e As MatEventArgs) Handles Xl_CfpPndsMaster1.ValueChanged
        LoadDetail()
    End Sub

    Private Async Sub Xl_CfpPndsDetail1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_CfpPndsDetail1.RequestToRefresh
        Await refresca()
    End Sub

End Class