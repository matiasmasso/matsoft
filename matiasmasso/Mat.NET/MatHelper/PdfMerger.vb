

Imports System
    Imports System.Collections.Generic
    Imports System.IO
    Imports iTextSharp.text
    Imports iTextSharp.text.pdf


Public Class PdfMerger
        Shared Function MergeFiles(ByVal sourceFiles As List(Of Byte())) As Byte()
            Dim document As Document = New Document()

            Using ms As MemoryStream = New MemoryStream()
                Dim copy As PdfCopy = New PdfCopy(document, ms)
                document.Open()
                Dim documentPageCounter As Integer = 0

                For fileCounter As Integer = 0 To sourceFiles.Count - 1
                    Dim reader As PdfReader = New PdfReader(sourceFiles(fileCounter))
                    Dim numberOfPages As Integer = reader.NumberOfPages

                    For currentPageIndex As Integer = 1 To numberOfPages
                        documentPageCounter += 1
                        Dim importedPage As PdfImportedPage = copy.GetImportedPage(reader, currentPageIndex)
                        Dim pageStamp As PdfCopy.PageStamp = copy.CreatePageStamp(importedPage)
                        ColumnText.ShowTextAligned(pageStamp.GetOverContent(), Element.ALIGN_CENTER, New Phrase("PDF Merger by Helvetic Solutions"), importedPage.Width / 2, importedPage.Height - 30, If(importedPage.Width < importedPage.Height, 0, 1))
                        ColumnText.ShowTextAligned(pageStamp.GetOverContent(), Element.ALIGN_CENTER, New Phrase(String.Format("Page {0}", documentPageCounter)), importedPage.Width / 2, 30, If(importedPage.Width < importedPage.Height, 0, 1))
                        pageStamp.AlterContents()
                        copy.AddPage(importedPage)
                    Next

                    copy.FreeReader(reader)
                    reader.Close()
                Next

                document.Close()
                Return ms.GetBuffer()
            End Using
        End Function
    End Class


