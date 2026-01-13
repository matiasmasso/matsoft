Imports System.IO
Imports Microsoft.Office.Interop

Imports DocumentFormat.OpenXml.Packaging
Imports DocumentFormat.OpenXml.Wordprocessing


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

    Shared Function GetImgFromWordFirstPage(ByVal oStream As Byte(), exs As List(Of Exception)) As System.Drawing.Image
        Dim retval As Image = Nothing
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

    Shared Function FillBookmarks(ByVal templateFilename As String, ByRef targetFilename As String, bookmarks As List(Of WordBookmark), exs As List(Of Exception)) As Boolean
        If String.IsNullOrEmpty(targetFilename) Then targetFilename = FileSystemHelper.PathToTmp & "\" & Guid.NewGuid.ToString & "." & MimeCods.Docx.ToString


        If bookmarks Is Nothing Then
            exs.Add(New Exception("Missing bookmark values"))
        ElseIf Not File.Exists(templateFilename) Then
            exs.Add(New Exception("No s'ha trobat cap plantilla a '" & templateFilename & "'"))
        Else

            Try
                File.Copy(templateFilename, targetFilename)

                Using doc As WordprocessingDocument = WordprocessingDocument.Open(targetFilename, True)
                    Dim bookmarkMap As New Dictionary(Of BookmarkStart, String)
                    Dim bs As BookmarkStart
                    For Each bs In doc.MainDocumentPart.RootElement.Descendants(Of BookmarkStart)()
                        Dim bookmark As WordBookmark = bookmarks.FirstOrDefault(Function(x) x.Name = bs.Name)
                        If bookmark IsNot Nothing Then
                            bookmark.BookmarkStart = bs
                        End If
                    Next

                    For Each bookmark As WordBookmark In bookmarks.Where(Function(x) x.BookmarkStart IsNot Nothing).ToList
                        Dim bookmarkStart As BookmarkStart = bookmark.BookmarkStart
                        Dim bsText As DocumentFormat.OpenXml.OpenXmlElement = bookmarkStart.NextSibling
                        If Not bsText Is Nothing Then
                            If TypeOf bsText Is BookmarkEnd Then
                                'Add Text element after start bookmark
                                bookmarkStart.Parent.InsertAfter(New Run(New Text(bookmark.Value)), bookmarkStart)
                            Else
                                'Change Bookmark Text
                                If TypeOf bsText Is Run Then
                                    If bsText.GetFirstChild(Of Text)() Is Nothing Then
                                        bsText.InsertAt(New Text(bookmark.Value), 0)
                                    Else
                                        bsText.GetFirstChild(Of Text)().Text = bookmark.Value
                                    End If
                                End If
                            End If

                        End If
                    Next

                    doc.MainDocumentPart.RootElement.Save()
                    doc.Close()
                End Using

            Catch ex0 As OpenXmlPackageException
                exs.Add(New Exception(String.Format("error al obrir la plantilla {0}", templateFilename)))
            Catch ex1 As UnauthorizedAccessException
                exs.Add(New Exception(String.Format("error UnauthorizedAccessException al desar {0}", targetFilename)))
            Catch ex2 As DirectoryNotFoundException
                exs.Add(New Exception(String.Format("error DirectoryNotFoundException al desar {0}", targetFilename)))
            Catch ex3 As IOException
                exs.Add(New Exception(String.Format("error al desar {0}", targetFilename)))
            Catch ex As Exception
                exs.Add(ex)
            End Try
        End If

        Return exs.Count = 0
    End Function



    Protected Shared Sub ProcessBookmarksPart(ByVal values As IDictionary(Of String, String), documentSection As DocumentSections, section As Object)
        Dim bookmarks As IEnumerable(Of DocumentFormat.OpenXml.OpenXmlElement) = Nothing
        Select Case documentSection
            Case DocumentSections.Header
                bookmarks = CType(section, HeaderPart).RootElement.Descendants()
            Case DocumentSections.Body
                bookmarks = CType(section, MainDocumentPart).Document.Body.Descendants()
            Case DocumentSections.Footer
                bookmarks = CType(section, FooterPart).RootElement.Descendants()
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

