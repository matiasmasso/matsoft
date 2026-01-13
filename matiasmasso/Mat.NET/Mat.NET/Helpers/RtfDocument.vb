Imports Microsoft.Office.Interop

Public Class RtfDocument
    Private _Word As Word.Application
    Private _Document As Word.Document = Nothing
    Private _Pages As RtfPages

    Public Function Open(sFilename As String, ByRef exs as list(Of Exception)) As Boolean
        Dim retval As Boolean
        Try
            _Word = New Word.Application
            _Word.Visible = False
            '_Document = _Word.Documents.Open(sFilename, Visible:=False, [ReadOnly]:=True)
            _Document = _Word.Documents.Open(sFilename)

            Dim iPagesCount As Integer = _Document.Range.Information(Word.WdInformation.wdNumberOfPagesInDocument)
            For iPageIdx As Integer = 1 To iPagesCount
                Dim oPageRange As Word.Range = _Word.Selection.GoTo(What:=Word.WdGoToItem.wdGoToPage, Which:=Word.WdGoToDirection.wdGoToAbsolute, Count:=iPageIdx)
                oPageRange.End = _Word.Selection.Bookmarks("\Page").Range.End
                Dim oShapeRange As Word.ShapeRange = oPageRange.ShapeRange
                Dim oRtfPage As New RtfPage(oShapeRange)
                Me.Pages.Add(oRtfPage)
            Next
            retval = True


        Catch ex As Exception
            exs.Add(ex)
        End Try
        Return retval
    End Function

    Public Sub Close()
        If _Document IsNot Nothing Then
            _Document.Close(Word.WdSaveOptions.wdDoNotSaveChanges)
        End If
        _Word = Nothing
    End Sub

    Public ReadOnly Property Pages As RtfPages
        Get
            If _Pages Is Nothing Then _Pages = New RtfPages
            Return _Pages
        End Get
    End Property

    Public Function PageCount() As Integer
        Dim retval As Integer = _Document.Range.Information(Word.WdInformation.wdNumberOfPagesInDocument)
        Return retval
    End Function

End Class

Public Class RtfPage
    Public Property Shapes As List(Of Word.Shape)
    Public Property TextShapes As List(Of String)

    Public Sub New(oShapeRange As Word.ShapeRange)
        MyBase.New()

        _Shapes = New List(Of Word.Shape)
        _TextShapes = New List(Of String)

        For Each oShape As Word.Shape In oShapeRange
            'Dim o As Microsoft.Office.Core.MsoAutoShapeType = oShape.AutoShapeType
            'If oShape.Type = Microsoft.Office.Core.MsoShapeType.msoTextBox Then
            Try
                Dim oTextFrame As Word.TextFrame = oShape.TextFrame
                If oTextFrame.HasText Then
                    Dim oTextRange As Word.Range = oTextFrame.TextRange
                    _TextShapes.Add(oTextRange.Text)
                    _Shapes.Add(oShape)
                End If
            Catch ex As Exception
                Stop
            End Try
        Next
    End Sub



End Class

Public Class RtfPages
    Inherits System.ComponentModel.BindingList(Of RtfPage)
End Class


