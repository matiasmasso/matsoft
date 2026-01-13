Imports System.IO
Imports Microsoft.Office.Interop

Public Class WordHelper

    Public Shared Function GetWordFileFromStream(ByVal oByte() As Byte, Optional ByVal sFilename As String = "") As String
        If sFilename = "" Then
            sFilename = System.Guid.NewGuid.ToString & ".docx"
            Dim sTmpFolder As String = MaxiSrvr.TmpFolder
            sFilename = System.IO.Path.Combine(sTmpFolder, sFilename)
        End If

        Dim exs as New List(Of exception)
        If Not BLL.FileSystemHelper.SaveStream(oByte, exs, sFilename) Then
            UIHelper.WarnError( exs, "error al desar el fitxer")
        End If
        Return sFilename
    End Function

    Public Shared Function GetXpsFileNameFromWordFileName(ByVal sWordFilename As Object, Optional ByVal sXpsFilename As String = "") As String
        Dim wordApp As New Word.Application
        Dim wordDoc As Word.Document = wordApp.Documents.Open(sWordFilename)

        If sXpsFilename = "" Then sXpsFilename = sWordFilename & ".xps"
        wordDoc.ExportAsFixedFormat(sXpsFilename, Word.WdExportFormat.wdExportFormatXPS, False, Word.WdExportOptimizeFor.wdExportOptimizeForOnScreen, Word.WdExportRange.wdExportAllDocument)

        Return sXpsFilename
    End Function

    Shared Function GetImgFromWordFirstPage(ByVal oStream As Byte()) As Image
        Dim sWordFilename As Object = WordHelper.GetWordFileFromStream(oStream)
        Dim sXpsFilename As String = WordHelper.GetXpsFileNameFromWordFileName(sWordFilename)
        Dim oXPSHelper As XPSHelper = XPSHelper.FromFilename(sXpsFilename)
        Dim oImg As Image = oXPSHelper.GenerateThumbnail()
        Return oImg
    End Function
End Class
