

Public Class Frm_PdfPageSelect
    Private mPageCount As Integer
    Private mPdfRender As PdfRender

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Public WriteOnly Property PdfStream() As Byte()
        Set(ByVal value As Byte())
            mPdfRender = New PdfRender(value)
            mPageCount = mPdfRender.PageCount
            For i As Integer = 1 To mPageCount
                CheckedListBox1.Items.Add("pag." & Format(i, "#00"))
            Next
        End Set
    End Property

    Private Sub CheckedListBox1_ItemCheck(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemCheckEventArgs) Handles CheckedListBox1.ItemCheck
        Dim iPage As Integer = e.Index + 1
        Dim oImg As Image = mPdfRender.Thumbnail(PictureBox1.Width, PictureBox1.Height, iPage)
        PictureBox1.Image = oImg
    End Sub


    Private Sub CheckedListBox1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckedListBox1.SelectedIndexChanged
        'Dim iPage As Integer = CheckedListBox1.SelectedIndex + 1
        'Dim oImg As Image = mPdfRender.Thumbnail(PictureBox1.Width, PictureBox1.Height, iPage)
        'PictureBox1.Image = oImg
    End Sub

    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        Dim oArrayPagesToDelete As New ArrayList 'base zero
        For i As Integer = 0 To mPageCount - 1
            If Not CheckedListBox1.GetItemChecked(i) Then
                oArrayPagesToDelete.Add(i)
            End If
        Next

        Dim oStream As Byte() = mPdfRender.RemovePages(oArrayPagesToDelete)
        RaiseEvent AfterUpdate(oStream, New System.EventArgs)
        Me.Close()
    End Sub


    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub
End Class