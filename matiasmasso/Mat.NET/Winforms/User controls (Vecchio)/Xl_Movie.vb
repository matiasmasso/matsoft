

Public Class Xl_Movie
    Private mMovie As Movie
    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Property Movie() As Movie
        Get
            Return mMovie
        End Get
        Set(ByVal value As Movie)
            If value IsNot Nothing Then
                mMovie = value
                refresca()
            End If
        End Set
    End Property

    Private Sub refresca()
        PictureBox1.BackgroundImage = mMovie.Image
        PictureBox1.BackgroundImageLayout = ImageLayout.Center
        TextBox1.Text = mMovie.Features
    End Sub

    Private Sub ImportarToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ImportarToolStripMenuItem.Click
        GetNewFile()
    End Sub

    Private Sub GetNewFile()
        Dim oDialog As New OpenFileDialog
        With oDialog
            .Filter = "windows media video (.wmv)|*.wmv"
            If .ShowDialog Then
                mMovie = New Movie(.FileName)
                refresca()
                RaiseEvent AfterUpdate(mMovie, New System.EventArgs)
            End If
        End With
    End Sub

    Private Sub PlayToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles PlayToolStripMenuItem.Click

    End Sub
End Class
