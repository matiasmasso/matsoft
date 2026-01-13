Public Class Frm_Picture

    Public Sub New(oImage As Image)
        MyBase.New
        InitializeComponent()

        PictureBox1.Image = oImage
    End Sub
End Class