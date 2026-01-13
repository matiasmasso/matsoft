Public Class Frm_TpvRedsys
    Private _TabsLoaded(10) As Boolean

    Public Enum Tabs
        Logs
        Configs
    End Enum

    Private Async Sub Frm_TpvRedsys_Load(sender As Object, e As EventArgs) Handles Me.Load
        Await RefrescaLogs()
    End Sub

    Private Async Function RefrescaLogs() As Task
        Dim exs As New List(Of Exception)
        Dim oLogs = Await FEB2.TpvLogs.All(exs)
        ProgressBar1.Visible = False
        If exs.Count = 0 Then
            Xl_TpvRedsysLogs1.Load(oLogs)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Async Function RefrescaConfigs() As Task
        Dim exs As New List(Of Exception)
        Dim oConfigs = Await FEB2.SermepaConfigs.All(exs)
        If exs.Count = 0 Then
            Xl_TpvRedsysConfigs1.Load(oConfigs)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Async Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged
        Dim oTab As Tabs = TabControl1.SelectedIndex
        If Not _TabsLoaded(oTab) Then
            Select Case TabControl1.SelectedIndex
                Case Tabs.Logs
                    Await RefrescaLogs()
                Case Tabs.Configs
                    Await RefrescaConfigs()
            End Select
            _TabsLoaded(oTab) = True
        End If
    End Sub

    Private Async Sub Xl_TpvRedsysLogs1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_TpvRedsysLogs1.RequestToRefresh
        Await RefrescaLogs()
    End Sub
End Class