Public Class Frm_CustomMsgBox
    Property LastButtonClicked As Buttons

    Public Enum Buttons
        Button1
        Button2
    End Enum

    Public Sub New(sQuestion As String, sButton1 As String, sButton2 As String, Optional sTitle As String = "")
        MyBase.New
        InitializeComponent()
        Me.Text = sTitle
        Label1.Text = sQuestion
        Button1.Text = sButton1
        Button2.Text = sButton2
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        _LastButtonClicked = Buttons.Button1
        Me.Close()
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        _LastButtonClicked = Buttons.Button2
        Me.Close()
    End Sub
End Class