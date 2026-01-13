Imports Microsoft.Office.Interop
Public Class RtfHelper

    Shared Function Open(exs As List(Of Exception), sFilename As String, Optional ShowProgress As ProgressBarHandler = Nothing) As RtfDoc
        Dim retval As New RtfDoc

        Dim cancelRequest As Boolean
        If ShowProgress IsNot Nothing Then
            ShowProgress(0, 1000, 0, "Obrint Microsoft Word per llegir el document", cancelRequest)
        End If

        Dim oWord As New Word.Application
        oWord.Visible = False
        Dim oDocument As Word.Document = oWord.Documents.Open(sFilename)
        Dim iPagesCount As Integer = oDocument.Range.Information(Word.WdInformation.wdNumberOfPagesInDocument)
        For iPageIdx As Integer = 1 To iPagesCount
            If ShowProgress IsNot Nothing Then
                ShowProgress(1, iPagesCount, iPageIdx, "llegint pàgina " & iPageIdx, cancelRequest)
                If cancelRequest Then Exit For
            End If

            Dim oPage As New RtfDoc.Page(oDocument, iPageIdx)
            retval.Pages.Add(oPage)
        Next

        If oDocument IsNot Nothing Then
            oDocument.Close(Word.WdSaveOptions.wdDoNotSaveChanges)
        End If
        Return retval
    End Function

    Public Class RtfDoc
        Public Property Pages As List(Of Page)

        Public Sub New()
            MyBase.New
            _Pages = New List(Of Page)
        End Sub

        Public Class Page
            Protected _Document As Word.Document
            Property Idx As Integer
            Property TextShapes As List(Of String)
            Property PdfStream As Byte()
            Property exs As List(Of Exception)

            Public Sub New(oDocument As Word.Document, iPageIdx As Integer)
                MyBase.New
                _Document = oDocument
                _Idx = iPageIdx
                _exs = New List(Of Exception)
                _TextShapes = GetTextShapes()
                _PdfStream = GetPdfStream()
            End Sub

            Private Function Selection() As Word.Selection
                Dim retval = _Document.Application.Selection
                Return retval
            End Function

            Private Function PageRange() As Word.Range
                Dim oSelection = Me.Selection
                Dim retval As Word.Range = oSelection.GoTo(What:=Word.WdGoToItem.wdGoToPage, Which:=Word.WdGoToDirection.wdGoToAbsolute, Count:=_Idx)
                retval.End = oSelection.Bookmarks("\Page").Range.End
                Return retval
            End Function

            Private Function GetTextShapes() As List(Of String)
                Dim retval As New List(Of String)
                For Each oShape As Word.Shape In PageRange.ShapeRange
                    Dim oTextFrame As Word.TextFrame = oShape.TextFrame
                    If oTextFrame.HasText Then
                        Dim oTextRange As Word.Range = oTextFrame.TextRange
                        retval.Add(oTextRange.Text)
                    End If
                Next
                Return retval
            End Function

            Public Function GetText(iShapeIndex As Integer, iStartChar As Integer, iLen As Integer, exs As List(Of Exception)) As String
                Dim retval As String = ""
                If iShapeIndex > Me.TextShapes.Count Then
                    exs.Add(New Exception(String.Format("ShapeIndex {0} excedit en pag.{1}", iShapeIndex, _Idx)))
                Else
                    Dim sTextShape As String = _TextShapes(iShapeIndex)
                    If sTextShape.Length > iStartChar Then
                        If sTextShape.Length > iStartChar + iLen Then
                            retval = sTextShape.Substring(iStartChar, iLen)
                        Else
                            retval = sTextShape.Substring(iStartChar)
                        End If
                    End If
                End If
                Return retval.Trim
            End Function


            Private Function GetPdfStream() As Byte()
                Dim oPageCopy As Word.Document = Me.Clon()

                Dim sFilename As String = FileSystemHelper.GetPdfTmpFileName(exs:=_exs)

                Try
                    oPageCopy.SaveAs(sFilename, Word.WdSaveFormat.wdFormatPDF)
                    oPageCopy.Close(Word.WdSaveOptions.wdDoNotSaveChanges)
                Catch ex As Exception
                    _exs.Add(ex)
                End Try

                Dim retval As Byte() = Nothing
                FileSystemHelper.GetStreamFromFile(sFilename, retval, _exs)
                Return retval
            End Function

            Public Function Clon() As Word.Document
                Dim retval As Word.Document = Nothing
                Try
                    Me.PageRange.ShapeRange.Select()
                    Me.Selection.Copy()
                    retval = _Document.Application.Documents.Add()
                    retval.Range.Paste()
                Catch ex As Exception
                    _exs.Add(ex)
                End Try
                Return retval
            End Function
        End Class
    End Class
End Class
