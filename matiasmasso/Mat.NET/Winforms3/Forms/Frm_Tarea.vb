Public Class Frm_Tarea

    Public Enum results
        waiting
        success
        fail
    End Enum

    Public Sub New(sCaption As String)
        MyBase.New()
        InitializeComponent()

        LabelDsc.Text = sCaption
    End Sub

    Public Sub Fin(oResult As results, Optional ByVal sCaption As String = "")
        Cursor = Cursors.Default
        Select Case oResult
            Case results.success
                PictureBox1.Image = My.Resources.vb
            Case results.fail
                PictureBox1.Image = My.Resources.warn_16
        End Select
        LabelDsc.Text = sCaption
        ProgressBar1.Visible = False
        ButtonExit.Enabled = True
    End Sub


    Private Sub ButtonExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonExit.Click
        Me.Close()
    End Sub
End Class