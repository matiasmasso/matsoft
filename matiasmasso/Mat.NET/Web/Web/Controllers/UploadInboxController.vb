Public Class UploadInboxController
    Inherits _MatController

    Function Index() As ActionResult
        Dim retval As ActionResult = View()
        Return retval
    End Function

    <HttpPost>
    <ValidateAntiForgeryToken>
    Public Sub FileUpload(files As IEnumerable)
        If files IsNot Nothing Then
            For Each oFile As HttpPostedFileBase In files
                If oFile.ContentLength > 0 Then
                    Dim item As New DTOUploadInbox
                    With item
                        .Filename = System.IO.Path.GetFileName(oFile.FileName)
                        .Text = GetFileContent(oFile)
                    End With
                    Dim exs As New List(Of Exception)
                    If BLLUploadInbox.Update(item, exs) Then
                    Else
                    End If
                End If
            Next
        End If
    End Sub

    Private Function GetFileContent(oFile As HttpPostedFileBase) As String
        Dim sr As New System.IO.StreamReader(oFile.InputStream)
        Dim retval As String = sr.ReadToEnd
        sr.Close()
        Return retval
    End Function


End Class
