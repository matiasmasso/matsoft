

Public Class Frm_WebLog

    Private _Logs As List(Of DTOWebLog)
    Private _AllowEvents As Boolean

    Private Sub Frm_WebLog_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ComboBox1.SelectedIndex = 0
        DateTimePicker1.Value = "1/1/" & Today.Year
        DateTimePicker2.Value = Today.AddDays(+1)
        _AllowEvents = True
        LoadResum()
    End Sub

    Private Async Sub LoadResum()
        Dim exs As New List(Of Exception)
        _Logs = Await FEB2.WebLogs.All(exs, DateTimePicker1.Value, DateTimePicker2.Value)
        If exs.Count = 0 Then
            Xl_Weblogs2Summary1.Load(_Logs)
            LoadDetall()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub LoadDetall()
        Dim oCod = CurrentLogCode()
        Dim oLogs = _Logs.Where(Function(x) x.Cod = oCod).ToList
        Xl_WebLogs21.Load(oLogs, Xl_WebLogs2.Modes.UsersPerCod)
    End Sub


    Private Function CurrentLogCode() As DTOWebLog.LogCodes
        Dim retval As DTOWebLog.LogCodes = Xl_Weblogs2Summary1.Value
        Return retval
    End Function

    Private Function CurrentUser() As DTOUser
        Dim retval As DTOUser = Xl_WebLogs21.Value.User
        Return retval
    End Function

    Private Sub DataGridView2_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs)
        If CurrentUser() IsNot Nothing Then
            Dim oFrm As New Frm_Contact_Email(CurrentUser)
            oFrm.Show()
        End If
    End Sub

    Private Sub ButtonRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonRefresh.Click
        LoadResum()
    End Sub

    Private Sub Xl_Weblogs2Summary1_ValueChanged(sender As Object, e As MatEventArgs) Handles Xl_Weblogs2Summary1.ValueChanged
        LoadDetall()
    End Sub
End Class