Imports System.Drawing

Public Class PdfController
    Inherits _MatController

    ''' <summary>
    ''' Gets a Pdf file and returns a DTODocfile with Base64 thumbnail
    ''' </summary>
    ''' <returns></returns>
    Public Function Thumbnail() As ActionResult
        Dim exs As New List(Of Exception)
        Dim retval As ActionResult = Nothing

        Dim oFiles = MyBase.PostedFiles()
        If oFiles.Count > 0 Then
            Dim oFileBytes = oFiles.First()
            Dim value = LegacyHelper.GhostScriptHelper.Pdf2Docfile(oFileBytes)
            retval = Json(value, JsonRequestBehavior.AllowGet)
        End If
        Return retval
    End Function

End Class
