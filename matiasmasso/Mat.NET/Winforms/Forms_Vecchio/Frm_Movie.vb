

Public Class Frm_Movie
    Private mMovie As maxisrvr.Movie
    Private mAllowEvents As Boolean

    Public WriteOnly Property Movie() As Movie
        Set(ByVal value As Movie)
            If value IsNot Nothing Then
                mMovie = value
                refresca()
                TextBoxNom.Text = mMovie.Nom
                TextBoxDsc.Text = mMovie.Dsc
                mAllowEvents = True
            End If
        End Set
    End Property

    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        With mMovie
            .Nom = TextBoxNom.Text
            .Dsc = TextBoxDsc.Text
            .Update()
        End With
        Me.Close()
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub Control_Changed(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
    TextBoxNom.TextChanged, _
     TextBoxDsc.TextChanged
        mAllowEvents = True
        If mAllowEvents Then
            ButtonOk.Enabled = True

        End If
    End Sub

    Private Sub refresca()
        PictureBox1.BackgroundImage = mMovie.Image
        PictureBox1.BackgroundImageLayout = ImageLayout.Center
        TextBoxFeatures.Text = mMovie.Features
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
                ButtonOk.Enabled = True
            End If
        End With
    End Sub

    Private Sub PlayToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles PlayToolStripMenuItem.Click
        Dim sUrl As String = "http://www.matiasmasso.es/doc.aspx?movie=" & mMovie.Guid.ToString
        Process.Start("IExplore.exe", sUrl)
    End Sub
End Class