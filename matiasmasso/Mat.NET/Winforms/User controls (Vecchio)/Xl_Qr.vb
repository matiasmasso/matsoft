

Public Class Xl_Qr
    Private mQR As maxisrvr.QR_Code

    Public Property Value As maxisrvr.QR_Code
        Get
            Return mQR
        End Get
        Set(value As maxisrvr.QR_Code)
            mQR = value
            If mQR IsNot Nothing Then
                PictureBox1.Image = mQR.Image
            End If
        End Set
    End Property

    Private Sub ViewToolStripMenuItem_Click(sender As Object, e As System.EventArgs) Handles ViewToolStripMenuItem.Click
        UIHelper.ShowHtml(mQR.Value)
    End Sub

    Private Sub CopiarEnllaçToolStripMenuItem_Click(sender As Object, e As System.EventArgs) Handles CopiarEnllaçToolStripMenuItem.Click
        Clipboard.SetDataObject(mQR.Value, True)
    End Sub

    Private Sub GuardarToolStripMenuItem_Click(sender As Object, e As System.EventArgs) Handles GuardarToolStripMenuItem.Click
        Dim oFrm As New Frm_QR(mQR)
        oFrm.Show()
    End Sub

End Class
