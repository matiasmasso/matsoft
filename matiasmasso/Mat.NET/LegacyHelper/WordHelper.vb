Imports System.IO
Imports DocumentFormat.OpenXml.Office
Imports DocumentFormat.OpenXml.Packaging
Imports DocumentFormat.OpenXml.Wordprocessing
Imports Microsoft.Office.Interop


Public Class WordHelper

    Protected Enum DocumentSections
        Header
        Body
        Footer
    End Enum

    Public Shared Function GetWordFileFromStream(ByVal oByte() As Byte, sFilename As String, exs As List(Of Exception)) As String
        If sFilename = "" Then
            sFilename = System.Guid.NewGuid.ToString & ".docx"
            Dim sTmpFolder As String = FileSystemHelper.TmpFolder & "\"
            sFilename = System.IO.Path.Combine(sTmpFolder, sFilename)
        End If

        FileSystemHelper.SaveStream(oByte, exs, sFilename)
        Return sFilename
    End Function

    Public Shared Function GetXpsFileNameFromWordFileName(ByVal sWordFilename As Object, Optional ByVal sXpsFilename As String = "") As String
        Dim wordApp As New Word.Application
        Dim wordDoc As Word.Document = wordApp.Documents.Open(sWordFilename)

        If sXpsFilename = "" Then sXpsFilename = sWordFilename & ".xps"
        wordDoc.ExportAsFixedFormat(sXpsFilename, Word.WdExportFormat.wdExportFormatXPS, False, Word.WdExportOptimizeFor.wdExportOptimizeForOnScreen, Word.WdExportRange.wdExportAllDocument)

        Return sXpsFilename
    End Function

    Shared Function GetImgFromWordFirstPage(ByVal oStream As Byte(), exs As List(Of Exception)) As Byte()
        Dim retval As Byte() = Nothing
        Dim sWordFilename As Object = WordHelper.GetWordFileFromStream(oStream, "", exs)
        If exs.Count = 0 Then
            Dim sXpsFilename As String = WordHelper.GetXpsFileNameFromWordFileName(sWordFilename)
            Dim oXPSHelper As XPSHelper = XPSHelper.FromFilename(sXpsFilename)
            retval = oXPSHelper.GenerateThumbnail()
        End If
        Return retval
    End Function



    Public Class WordBookmark
        Property Name As String
        Property Value As String
        Friend Property BookmarkStart As BookmarkStart

        Public Sub New(bookmark As String, value As String)
            MyBase.New
            _Name = bookmark
            _Value = value
        End Sub
    End Class



    Protected Shared Sub ProcessBookmarksPart(ByVal values As IDictionary(Of String, String), documentSection As DocumentSections, section As Object)
        Dim bookmarks As IEnumerable(Of DocumentFormat.OpenXml.OpenXmlElement) = Nothing
        Select Case documentSection
            Case DocumentSections.Header
                bookmarks = DirectCast(section, HeaderPart).RootElement.Descendants()
            Case DocumentSections.Body
                bookmarks = DirectCast(section, MainDocumentPart).Document.Body.Descendants()
            Case DocumentSections.Footer
                bookmarks = DirectCast(section, FooterPart).RootElement.Descendants()
        End Select

        For Each bmStart As DocumentFormat.OpenXml.OpenXmlElement In bookmarks
            If values.ContainsKey(bmStart.LocalName) Then
                Dim bmText = values(bmStart.LocalName)
                Dim bmEnd As BookmarkEnd = Nothing

                Select Case documentSection
                    Case DocumentSections.Header
                        'bmEnd = bookmarks.Where(Function(x) x. = bmStart.Id.ToString())).FirstOrDefault()
                    Case DocumentSections.Body
                    Case DocumentSections.Footer
                End Select
            End If
        Next
    End Sub


End Class

