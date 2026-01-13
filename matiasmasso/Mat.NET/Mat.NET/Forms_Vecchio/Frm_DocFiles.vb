Public Class Frm_DocFiles

    Private Sub Frm_DocFiles_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim oDocFiles As List(Of DTODocFile) = BLL.BLLDocFiles.All
        Xl_DocFiles1.Load(oDocFiles)
    End Sub
End Class