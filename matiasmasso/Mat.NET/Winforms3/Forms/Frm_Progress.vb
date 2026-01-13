Public Class Frm_Progress

    Public Event requestToProceed(sender As Object, e As MatEventArgs)

    Public Sub New(sTitle As String, sDsc As String)
        MyBase.New
        InitializeComponent()
        Me.Text = sTitle
        TextBox1.Text = sDsc
    End Sub

    Public Sub SetStart()
        Xl_ProgressBar1.Visible = True
        ButtonStart.Visible = False
    End Sub
    Public Sub SetEnd(sText As String)
        TextBox1.Text += vbCrLf & vbCrLf & sText
        Xl_ProgressBar1.Visible = False
        ButtonStart.Text = "Sortir"
        ButtonStart.Visible = True
    End Sub

    Public Sub ShowProgress(ByVal min As Integer, ByVal max As Integer, ByVal value As Integer, ByVal label As String, ByRef CancelRequest As Boolean)
        Xl_ProgressBar1.ShowProgress(min, max, value, label, CancelRequest)
    End Sub

    Private Sub ButtonStart_Click(sender As Object, e As EventArgs) Handles ButtonStart.Click
        If ButtonStart.Text = "Sortir" Then
            Me.Close()
        Else
            Xl_ProgressBar1.Visible = True
            ButtonStart.Visible = False
            RaiseEvent requestToProceed(Me, MatEventArgs.Empty)
        End If
    End Sub
End Class