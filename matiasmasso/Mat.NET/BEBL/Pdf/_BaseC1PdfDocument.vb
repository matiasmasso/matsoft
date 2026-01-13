Public Class _BaseC1PdfDocument
    Inherits C1PdfHelper.Document

    Protected _PageCount As Integer
    Protected X As Integer
    Protected Y As Integer
    Protected YHeader As Integer
    Protected YTitle As Integer

    Protected Ymax As Integer
    Protected _FontEpg As Font
    Protected _LeftMargin As Integer
    Protected _RightMargin As Integer
    Protected _BottomMargin As Integer

    Protected _YColumnHeaders As Integer

    Protected _Columns As List(Of PdfColumn)

    Property Filename As String
    Property Lang As DTOLang
    Property Font As Font

    Public Sub New(Optional sFilename As String = "")
        MyBase.New
        _Filename = sFilename
        _Font = New Font("Helvetica", 9, FontStyle.Regular)
        _FontEpg = New Font("Tahoma", 8, FontStyle.Regular)

        _BottomMargin = MyBase.PageRectangle.Bottom - 20
        _RightMargin = MyBase.PageRectangle.Right - 20
        _LeftMargin = 90
        YHeader = 120
        YTitle = YHeader + 10 * _Font.Height

        _Columns = New List(Of PdfColumn)
        _YColumnHeaders = 300
    End Sub

    Protected Sub DrawDestination(oContact As DTOContact)
        Dim iWidth As Integer = 300
        X = _LeftMargin
        Y = YHeader
        DrawString(oContact.nom, iWidth)

        X = _LeftMargin
        Y += _Font.Height
        DrawString(oContact.address.text, iWidth)

        X = _LeftMargin
        Y = Y + _Font.Height
        DrawString(oContact.address.zip.FullNom(_Lang), iWidth)
    End Sub

    Protected Sub DrawTitle(sTitle As String)
        Y = YTitle
        X = _LeftMargin
        Dim PageUtilWidth As Integer = _RightMargin - _LeftMargin
        DrawString(sTitle, PageUtilWidth, PdfColumn.HorizontalAlignments.Center)
    End Sub

    Protected Sub AddColumn(oType As PdfColumn.Types, Optional sCaption As String = "", Optional iWidth As Integer = 0, Optional oHorizontalAlignment As PdfColumn.HorizontalAlignments = PdfColumn.HorizontalAlignments.NotSet)
        Dim oColumn As New PdfColumn(oType)
        With oColumn
            If iWidth = 0 Then
                Select Case oType
                    Case PdfColumn.Types.Fch, PdfColumn.Types.Number, PdfColumn.Types.Import, PdfColumn.Types.Text
                        .Width = 80
                End Select
            Else
                .Width = iWidth
            End If

            If oHorizontalAlignment = PdfColumn.HorizontalAlignments.NotSet Then
                Select Case oType
                    Case PdfColumn.Types.Fch
                        .HorizontalAlignment = PdfColumn.HorizontalAlignments.Center
                    Case PdfColumn.Types.Number, PdfColumn.Types.Import
                        .HorizontalAlignment = PdfColumn.HorizontalAlignments.Right
                    Case Else
                        .HorizontalAlignment = PdfColumn.HorizontalAlignments.Left
                End Select
            Else
                .HorizontalAlignment = oHorizontalAlignment
            End If

            .Caption = sCaption
        End With
        _Columns.Add(oColumn)
    End Sub


    Protected Sub DrawColumnHeaders()
        Dim iCol As Integer = 0
        Y = _YColumnHeaders
        For Each oColumn As PdfColumn In _Columns
            DrawCell(oColumn, oColumn.Caption)
        Next
        Y = Y + (_Font.Height * 1.5)
    End Sub

    Protected Sub DrawRow(ParamArray Values() As Object)

        Dim iCol As Integer = 0
        For Each Value As Object In Values
            DrawCell(_Columns(iCol), Value)
            iCol += 1
        Next
        Y = Y + _Font.Height
    End Sub

    Protected Sub DrawMergedCellsRow(Optional src As String = "")

        If src <> "" Then
            X = _LeftMargin
            DrawString(src, ColumnsWidth)
        End If

        Y = Y + _Font.Height
    End Sub

    Protected Sub DrawCell(oColumn As PdfColumn, oValue As Object)
        If oValue IsNot Nothing Then
            X = ColumnXPos(oColumn)
            Dim src As String = ""
            If TypeOf oValue Is DTOAmt Then
                src = DTOAmt.CurFormatted(CType(oValue, DTOAmt))
            ElseIf IsDate(oValue) Then
                src = Format(oValue, "dd/MM/yy")
            Else
                src = oValue.ToString
            End If
            DrawString(src, oColumn.Width, oColumn.HorizontalAlignment)
        End If
    End Sub

    Protected Overloads Sub DrawString(ByVal sTxt As String, ByVal iWidth As Integer, Optional ByVal oAlign As PdfColumn.HorizontalAlignments = PdfColumn.HorizontalAlignments.Left, Optional ByVal oFont As Font = Nothing, Optional ByVal oBrush As SolidBrush = Nothing)
        If oFont Is Nothing Then oFont = _Font
        If oBrush Is Nothing Then oBrush = Brushes.Black

        Dim xPos As Integer
        Dim iStringWidth As Integer
        Select Case oAlign
            Case PdfColumn.HorizontalAlignments.Left
                xPos = X
                iStringWidth = iWidth
            Case PdfColumn.HorizontalAlignments.Center
                iStringWidth = MyBase.MeasureString(sTxt, oFont).Width
                xPos = X + (iWidth - iStringWidth) / 2
            Case PdfColumn.HorizontalAlignments.Right
                iStringWidth = MyBase.MeasureString(sTxt, oFont).ToSize.Width
                xPos = X + iWidth - iStringWidth
        End Select

        Dim oRc As New Rectangle(xPos, Y, iStringWidth, oFont.Height)
        Dim rc As New RectangleF(xPos, Y, iWidth, oFont.Height)
        MyBase.DrawString(sTxt, oFont, oBrush, rc)
        X = X + iWidth
    End Sub

    Protected Function ColumnXPos(oColumn As PdfColumn) As Integer
        Dim retval As Integer = FirstColumnLeftPos()
        Dim iColIndex As Integer = _Columns.IndexOf(oColumn)
        If iColIndex > 0 Then
            For i As Integer = 0 To iColIndex - 1
                If i > 0 Then retval += _Columns(i).MarginLeft
                retval += _Columns(i).Width + _Columns(i).MarginRight
            Next
            retval += _Columns(iColIndex).MarginLeft
        End If
        Return retval
    End Function

    Protected Function FirstColumnLeftPos() As Integer
        Dim iPageWidth As Integer = MyBase.PageRectangle.Width
        Dim retval As Integer = (iPageWidth - ColumnsWidth()) / 2
        Return retval
    End Function

    Protected Function ColumnsWidth() As Integer
        Dim retval As Integer = _Columns.Sum(Function(x) x.Width + x.MarginLeft + x.MarginRight)
        retval = retval - _Columns.First.MarginLeft - _Columns.Last.MarginRight
        Return retval
    End Function

    Protected Function RequestNewPage(iLinesBefore As Integer) As Boolean
        Dim retval As Boolean = (Y + _Font.Height * iLinesBefore) >= _BottomMargin
        Return retval
    End Function


    Protected Class PdfColumn

        Property Type As Types
        Property Caption As String
        Property Width As Integer
        Property PaddingLeft As Integer
        Property PaddingRight As Integer

        Property MarginLeft As Integer
        Property MarginRight As Integer
        Property HorizontalAlignment As HorizontalAlignments

        Public Enum Types
            Text
            Number
            Import
            Fch
        End Enum

        Public Enum HorizontalAlignments
            NotSet
            Left
            Center
            Right
        End Enum

        Public Sub New(oType As Types)
            MyBase.New
            _Type = oType
        End Sub
    End Class
End Class

