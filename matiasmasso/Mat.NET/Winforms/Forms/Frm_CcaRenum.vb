Public Class Frm_CcaRenum

    Private _CancelRequest As Boolean
    Private Sub Frm_CcaRenum_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        With NumericUpDown1
            .Maximum = Today.Year
            .Minimum = 1985
            .Value = Today.Year
        End With
    End Sub

    Private Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        AddHandler BLL.BLLExercici.ReportProgress, AddressOf Do_Progress
        LabelStatus.Visible = True
        ButtonCancel.Enabled = True
        ButtonOk.Enabled = False
        ProgressBar1.Visible = True
        Dim oExercici As DTOExercici = BLL.BLLExercici.FromYear(NumericUpDown1.Value)
        Dim exs As New List(Of Exception)
        If BLL.BLLExercici.RenumeraAssentaments(oExercici, exs) Then
            MsgBox("assentaments renumerats", MsgBoxStyle.Information)
            Me.Close()
        Else
            UIHelper.WarnError(exs)
            ButtonCancel.Enabled = False
            ButtonOk.Enabled = True
            ProgressBar1.Visible = False
        End If
    End Sub

    Public Sub Do_Progress(ByVal min As Integer, ByVal max As Integer, ByVal value As Integer, ByRef label As String, ByRef CancelRequest As Boolean)
        Static StartTime As DateTime
        If StartTime = Nothing Then StartTime = Now
        With ProgressBar1
            .Minimum = min
            .Maximum = max
            .Value = value + 1
        End With
        Dim spanDone As TimeSpan = Now - StartTime
        Dim spanTot As New TimeSpan(spanDone.Seconds / value * max)
        Dim spanLeft As TimeSpan = spanTot - spanDone
        CancelRequest = _CancelRequest
        LabelStatus.Text = String.Format("assentament {0:000,000}.  Temps previst fins acabar: {1:hh\:mm\:ss}", value, spanLeft)
        Application.DoEvents()
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        ButtonCancel.Enabled = False
        ButtonOk.Enabled = True
        _CancelRequest = True
        Application.DoEvents()
    End Sub
End Class