Imports System.Text
Imports iText.IO.Image
Imports iText.Kernel.Font
Imports iText.Kernel.Pdf
Imports iText.Kernel.Pdf.Canvas.Parser
Imports iText.Kernel.Pdf.Canvas.Parser.Listener
Imports iText.Kernel.Utils
Imports iText.Layout


Public Class iTextPdfHelper

    Shared Function splitFileIntoPages(exs As List(Of Exception), sourceFilename As String) As List(Of String)
        Return MatPdfSplitter.splitFileIntoPages(exs, sourceFilename)
    End Function

    Shared Function readText(filename As String, exs As List(Of Exception)) As String
        Dim sb As New Text.StringBuilder()

        If System.IO.File.Exists(filename) Then
            Using reader As New PdfReader(filename)
                Using pdf As New PdfDocument(reader)
                    For page As Integer = 1 To pdf.GetNumberOfPages
                        Dim strategy As ITextExtractionStrategy = New SimpleTextExtractionStrategy()
                        Dim oPage = pdf.GetPage(page)
                        Dim currentText As String = PdfTextExtractor.GetTextFromPage(oPage, strategy)
                        Dim res = Encoding.UTF8.GetString(ASCIIEncoding.Convert(Encoding.[Default], Encoding.UTF8, Encoding.[Default].GetBytes(currentText)))
                        sb.Append(res)
                    Next
                End Using
            End Using


        End If
        Return sb.ToString
    End Function

    Shared Function readText(oByteArray As Byte(), exs As List(Of Exception)) As String
        Dim sb As New Text.StringBuilder()

        'If oByteArray IsNot Nothing Then
        '    Dim oMemoryStream As New System.IO.MemoryStream(oByteArray)
        '    Using reader As New PdfReader(oMemoryStream)
        '        Using pdf As New PdfDocument(reader)
        '            For page As Integer = 1 To pdf.GetNumberOfPages
        '                Dim strategy As ITextExtractionStrategy = New SimpleTextExtractionStrategy()
        '                Dim oPage = pdf.GetPage(page)
        '                Dim currentText As String = PdfTextExtractor.GetTextFromPage(oPage, strategy)
        '                Dim res = Encoding.UTF8.GetString(ASCIIEncoding.Convert(Encoding.[Default], Encoding.UTF8, Encoding.[Default].GetBytes(currentText)))
        '                sb.Append(res)
        '            Next
        '        End Using
        '    End Using

        'End If
        Return sb.ToString
    End Function


    Shared Function write(exs As List(Of Exception), srcFilename As String, destFilename As String, oObjectsToAdd As IEnumerable(Of MatPdfObject)) As Boolean
        If System.IO.File.Exists(srcFilename) Then
            Try

                ' Modify PDF located at "source" And save to "target"
                Dim PdfDocument As New PdfDocument(New PdfReader(srcFilename), New PdfWriter(destFilename))
                ' Document to add layout elements: paragraphs, images etc
                Dim Document As New Document(PdfDocument)

                For Each oObject In oObjectsToAdd
                    If TypeOf oObject Is MatPdfText Then
                        Dim oPdfText As MatPdfText = oObject
                        Dim oFont As PdfFont = PdfFontFactory.CreateFont(oPdfText.font.ToString()) 'oPdfText.font.ToString())
                        Dim oText As New iText.Layout.Element.Text(oPdfText.text)
                        'oText.SetFont(oFont)
                        'Dim p As New Element.Paragraph()
                        'p.Add(oText)
                        'p.SetMarginTop(oPdfText.rectangle.Y)
                        'p.SetMarginLeft(oPdfText.rectangle.X)
                        'Document.Add(p)
                    ElseIf TypeOf oObject Is MatPdfImage Then
                        Dim oPdfImage As MatPdfImage = oObject
                        Dim oImageByteArray = ImageHelper.GetByteArrayFromImg(oPdfImage.Image)
                        Dim ImageData As ImageData = ImageDataFactory.Create(oImageByteArray)

                        ' Create layout image object And provide parameters. Page number = 1
                        'Dim oCropBox As iText.Kernel.Geom.Rectangle = Document.GetPdfDocument.GetPage(1).GetCropBox
                        'Dim Image As New iText.Layout.Element.Image(ImageData)
                        'Image.ScaleAbsolute(oPdfImage.rectangle.Width, oPdfImage.rectangle.Height)
                        'Image.SetFixedPosition(1, oPdfImage.rectangle.Left, oCropBox.GetHeight - oPdfImage.rectangle.Top)

                        '' This adds the image to the page
                        'Document.Add(Image)
                    End If
                Next

                ' Don't forget to close the document.
                ' When you use Document, you should close it rather than PdfDocument instance
                'Document.Close()
            Catch ex As Exception
                exs.Add(ex)
            End Try
        End If
        Return exs.Count = 0
    End Function


    Public Class MatPdfObject
        Property rectangle As Rectangle

        Public Enum Fonts
            COURIER
            COURIER_BOLD
            COURIER_OBLIQUE
            COURIER_BOLDOBLIQUE
            Helvetica
            HELVETICA_BOLD
            HELVETICA_OBLIQUE
            HELVETICA_BOLDOBLIQUE
            SYMBOL
            TIMES_ROMAN
            TIMES_BOLD
            TIMES_ITALIC
            TIMES_BOLDITALIC
            ZAPFDINGBATS
        End Enum
    End Class

    Public Class MatPdfText
        Inherits MatPdfObject

        Property text As String
        Property font As Fonts


        Shared Function Factory(text As String, rectangle As Rectangle, Optional font As Fonts = Fonts.Helvetica) As MatPdfText
            Dim retval As New MatPdfText
            With retval
                .text = text
                .rectangle = rectangle
                .font = font
            End With
            Return retval
        End Function

    End Class

    Public Class MatPdfImage
        Inherits MatPdfObject

        Property Image As Image

        Shared Function Factory(oImage As Image, rectangle As Rectangle) As MatPdfImage
            Dim retval As New MatPdfImage
            With retval
                .Image = oImage
                .rectangle = rectangle
            End With
            Return retval
        End Function

    End Class

    Private Class MatPdfSplitter
        Inherits PdfSplitter

        Private ReadOnly _sourceFilename As String
        Property destFilenames As List(Of String)

        Shared Function splitFileIntoPages(exs As List(Of Exception), sourceFilename As String) As List(Of String)
            Dim retval As New List(Of String)
            If System.IO.File.Exists(sourceFilename) Then
                Try
                    Dim destinationFolder = System.IO.Path.GetDirectoryName(sourceFilename)
                    Using reader As New PdfReader(sourceFilename)
                        Using pdfSource As New PdfDocument(reader)
                            Dim oSplitter As New MatPdfSplitter(pdfSource, sourceFilename)
                            Dim oSplittedDocs = oSplitter.SplitByPageCount(1)
                            retval = oSplitter.destFilenames

                            For Each oSplittedDoc In oSplittedDocs
                                oSplittedDoc.Close()
                                'oSplittedDoc = Nothing
                            Next

                            'oSplittedDocs = Nothing
                            'oSplitter = Nothing
                        End Using
                    End Using
                Catch ex As Exception
                    exs.Add(ex)
                End Try
            Else
                exs.Add(New Exception(String.Format("File '{0}' not found", sourceFilename)))
            End If
            Return retval
        End Function

        Public Sub New(ByVal pdfDocument As PdfDocument, ByVal sourceFilename As String)
            MyBase.New(pdfDocument)
            _sourceFilename = sourceFilename
            _destFilenames = New List(Of String)
        End Sub

        Protected Overrides Function GetNextPdfWriter(ByVal documentPageRange As PageRange) As PdfWriter
            Dim pageIdx = _destFilenames.Count + 1
            Dim destfilename = _sourceFilename.Replace(".pdf", String.Format(".Page {0}.pdf", pageIdx))
            _destFilenames.Add(destfilename)
            Return New PdfWriter(destfilename)
        End Function
    End Class


End Class




