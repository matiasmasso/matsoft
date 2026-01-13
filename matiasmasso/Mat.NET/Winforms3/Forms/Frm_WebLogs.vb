Public Class Frm_WebLogs
    Public Event RequestToReset(sender As Object, e As MatEventArgs)
    Public Event RequestToRefresh(sender As Object, e As MatEventArgs)

    Public Sub New(oLogs As List(Of DTOWebLogBrowse), Optional sTitle As String = "")
        MyBase.New()
        Me.InitializeComponent()
        Me.Text = "Logs " & sTitle
        Xl_WebLogs1.Load(oLogs)
    End Sub

    Public Sub RefreshLogs(oLogs As List(Of DTOWebLogBrowse))
        Xl_WebLogs1.Load(oLogs)
    End Sub

    Private Sub Xl_WebLogs1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_WebLogs1.RequestToRefresh
        RaiseEvent RequestToRefresh(Me, e)
    End Sub

    Private Sub Xl_WebLogs1_RequestToReset(sender As Object, e As MatEventArgs) Handles Xl_WebLogs1.RequestToReset
        RaiseEvent RequestToReset(Me, e)
    End Sub
End Class