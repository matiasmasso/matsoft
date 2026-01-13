Public Class Frm_QR
    Private mQR As maxisrvr.QR_Code
    Private mAllowEvents As Boolean

    Public Sub New(oQR As maxisrvr.QR_Code)
        MyBase.New()
        Me.InitializeComponent()
        mQR = oQR
        TextBoxValue.Text = mQR.Value
        refresca()
        mAllowEvents = True
    End Sub

    Private Sub refresca()
        mQR.ModuleSize = NumericUpDown1.Value
        Dim sPixels As String = Format(mQR.Pixels, "#,##0")
        Dim sCm As String = Format(mQR.mm / 10, "0.0")
        TextBoxPixels.Text = sPixels & "x" & sPixels & " pixels"
        TextBoxCm.Text = sCm & "x" & sCm & " cm"
        PictureBox1.Image = mQR.Image
    End Sub

    Private Sub NumericUpDown1_ValueChanged(sender As Object, e As System.EventArgs) Handles NumericUpDown1.ValueChanged
        If mAllowEvents Then
            refresca()
        End If
    End Sub

    Private Sub TextBoxValue_TextChanged(sender As Object, e As System.EventArgs) Handles TextBoxValue.TextChanged
        If mAllowEvents Then
            If ButtonRefresh.Enabled Then
            Else
                ButtonRefresh.Enabled = True
                TextBoxPixels.Clear()
                TextBoxCm.Clear()
                PictureBox1.Image = Nothing
            End If
        End If
    End Sub

    Private Sub ButtonRefresh_Click(sender As Object, e As System.EventArgs) Handles ButtonRefresh.Click
        refresca()
        ButtonRefresh.Enabled = False
    End Sub

    Private Sub ButtonOk_Click(sender As Object, e As System.EventArgs) Handles ButtonOk.Click
        Dim oDlg As New SaveFileDialog
        With oDlg
            .Title = "Guardar codi QR en alta resolució (300 ppp)"
            .Filter = "imatges jpg (*.jpg)|*.jpg"
            If .ShowDialog And .FileName > "" Then
                Dim oImage As Image = mQR.ImageHighRes
                oImage.Save(.FileName)
            End If
        End With
    End Sub
End Class