Public Class Xl_ProgressBar
    Private _CancelRequest As Boolean
    Private _ModeSortida As Boolean

    Public Event RequestToExit(sender As Object, e As MatEventArgs)

    Public Shadows Sub Load(status As String)
        With ProgressBar1
            .Minimum = 0
            .Maximum = 1000
            .Value = 0
            .Style = ProgressBarStyle.Blocks
        End With
        Me.Visible = True
        LabelStatus.Text = status
    End Sub

    Public Sub ShowMarquee(Optional ByVal label As String = "")
        ProgressBar1.Style = ProgressBarStyle.Marquee
        ButtonCancel.Visible = False
        If label > "" Then LabelStatus.Text = label & "..."
        Application.DoEvents()
    End Sub

    Public Sub ShowStart(ByVal label As String)
        MyBase.Visible = True
        With ProgressBar1
            .Minimum = 0
            .Maximum = 1000
            .Value = 0
            .Style = ProgressBarStyle.Marquee
        End With

        ButtonCancel.Visible = True
        LabelStatus.Text = label & "..."
        Application.DoEvents()
    End Sub

    Public Sub ShowProgress(ByVal min As Long, ByVal max As Long, ByVal value As Long, ByVal label As String, ByRef CancelRequest As Boolean)
        MyBase.Visible = True
        value += 1

        With ProgressBar1
            .Style = ProgressBarStyle.Blocks
            If value < min Then
                min = value
            ElseIf value > max Then
                max = value
            End If
            .Minimum = min
            .Maximum = max
            .Value = value
            .Style = ProgressBarStyle.Blocks
        End With

        Dim share As Integer = 100 * value / max
        label = label & " " & share & "%"
        LabelStatus.Text = label
        ButtonCancel.Visible = True
        Application.DoEvents()
        CancelRequest = _CancelRequest
        _CancelRequest = False
    End Sub

    Public Sub ShowEnd(label As String)
        LabelStatus.Text = label
        ButtonCancel.Visible = False
    End Sub
    Public Sub ShowEndSortida(label As String)
        LabelStatus.Text = label
        ButtonCancel.Visible = True
        ButtonCancel.Text = "Sortida"
        _ModeSortida = True
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        _CancelRequest = True
        If _ModeSortida Then
            RaiseEvent RequestToExit(Me, MatEventArgs.Empty)
        End If
    End Sub
End Class
