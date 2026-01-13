Public Class Frm_FileDocuments

    Private Sub Frm_FileDocuments_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim oFileDocuments As FileDocuments = FileDocuments.All
        Xl_FileDocuments1.Load(oFileDocuments)
    End Sub
End Class