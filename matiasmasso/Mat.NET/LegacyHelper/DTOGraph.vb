Public Class DTOGraph
    Property Title As String
    Property Values As List(Of Decimal)
    Property Width As Integer = 300
    Property Height As Integer = 200
    Property PaddingTop As Integer = 15
    Property PaddingRight As Integer = 15
    Property PaddingLeft As Integer = 15
    Property PaddingBottom As Integer = 15
    Property StartColumn As Integer
    Property Pen As New Pen(Brushes.Red, 3)

    Property FontFamily As String = "Arial"

    Private _EndColumn As Integer

    Private _ColumnsCount As Integer

    Property xAxisLabels As List(Of String)

    Property EndColumn As Integer
        Get
            If _EndColumn = 0 Then _EndColumn = _StartColumn + _Values.Count - 1
            Return _EndColumn
        End Get
        Set(value As Integer)
            _EndColumn = value
        End Set
    End Property

    Property ColumnsCount As Integer
        Get
            If _ColumnsCount = 0 Then _ColumnsCount = _Values.Count
            Return _ColumnsCount
        End Get
        Set(value As Integer)
            _ColumnsCount = value
        End Set
    End Property

    Public Function ColsWidth() As Decimal
        Dim retval As Decimal = UsefullWidth() / ColumnsCount
        Return retval
    End Function

    Public Function ColCenter(idx As Integer) As Decimal
        Dim retval As Decimal = _PaddingLeft + ColsWidth() * idx + ColsWidth() / 2
        Return retval
    End Function

    Public Function MaxValue() As Decimal
        Dim retval As Decimal = Values.Max
        Return retval
    End Function

    Public Function UsefullHeight() As Decimal
        Dim retval As Decimal = _Height - _PaddingTop - _PaddingBottom
        Return retval
    End Function
    Public Function UsefullWidth() As Decimal
        Dim retval As Decimal = _Width - _PaddingLeft - _PaddingRight
        Return retval
    End Function

    Public Function VerticalFactor() As Decimal
        Dim retval As Decimal = UsefullHeight() / MaxValue()
        Return retval
    End Function

    Public Function FontLabels() As System.Drawing.Font
        Dim emSize As Decimal = _PaddingBottom * 0.7
        Dim retval As New Font(_FontFamily, emSize, GraphicsUnit.Pixel)
        Return retval
    End Function

    Public Function FontTitle() As System.Drawing.Font
        Dim emSize As Decimal = _PaddingTop * 0.8
        Dim retval As New Font(_FontFamily, emSize, GraphicsUnit.Pixel)
        Return retval
    End Function



    Public Function Bitmap() As Byte()
        Dim retval As Byte() = Nothing
        Dim oImage As New Bitmap(_Width, _Height)
        Dim oGraphics As Graphics = Graphics.FromImage(oImage)
        oGraphics.FillRectangle(Brushes.White, oGraphics.ClipBounds)

        DrawTitle(Me, oGraphics)
        DrawResult(Me, oGraphics)
        DrawVerticalLabels(Me, oGraphics)
        DrawBottomLabels(Me, oGraphics)
        DrawVerticalMarks(Me, oGraphics)

        Using ms As New IO.MemoryStream
            oImage.Save(ms, Imaging.ImageFormat.Jpeg)
            retval = ms.ToArray()
        End Using
        Return retval
    End Function

    Shared Sub DrawResult(oGraph As DTOGraph, oGraphics As Graphics)
        Dim oPoints As Point()
        ReDim oPoints(oGraph.EndColumn - oGraph.StartColumn)
        For j As Integer = oGraph.StartColumn To oGraph.EndColumn
            Dim Y As Integer = oGraph.PaddingTop + oGraph.UsefullHeight - oGraph.Values(j) * oGraph.VerticalFactor
            Dim X As Integer = oGraph.ColCenter(j)
            oPoints(j) = New Point(X, Y)
        Next
        oGraphics.DrawLines(oGraph.Pen, oPoints)
    End Sub

    Shared Sub DrawTitle(oGraph As DTOGraph, oGraphics As Graphics)
        Dim Y As Integer = oGraph.PaddingTop
        Dim X As Integer = oGraph.PaddingLeft + (oGraph.UsefullWidth - oGraphics.MeasureString(oGraph.Title, oGraph.FontTitle).Width) / 2
        oGraphics.DrawString(oGraph.Title, oGraph.FontTitle, Brushes.Navy, X, Y)
    End Sub

    Shared Sub DrawBottomLabels(oGraph As DTOGraph, oGraphics As Graphics)
        Dim oFont As Font = oGraph.FontLabels
        Dim Y As Integer = oGraph.PaddingTop + oGraph.UsefullHeight
        For j As Integer = 0 To oGraph.ColumnsCount - 1
            Dim Label As String = oGraph.xAxisLabels(j)
            Dim LabelWidth As Decimal = oGraphics.MeasureString(Label, oFont).Width
            Dim X As Decimal = oGraph.ColCenter(j) - LabelWidth / 2
            Dim oPoint As New Point(X, Y)
            oGraphics.DrawString(Label, oFont, Brushes.Black, oPoint)
        Next
    End Sub

    Shared Sub DrawVerticalLabels(oGraph As DTOGraph, oGraphics As Graphics)
        Dim oFont As Font = oGraph.FontLabels
        Dim dcMaxValue As Decimal = oGraph.MaxValue
        Dim CheckValue As Integer
        For i = 8 To 1 Step -1
            CheckValue = 10 ^ i
            If dcMaxValue > CheckValue Then Exit For
        Next
        Dim intervalValue As Decimal = CheckValue
        Dim intervalHeight As Decimal = intervalValue * oGraph.VerticalFactor

        Dim X As Integer = 0
        Dim Y As Decimal = oGraph.PaddingTop + oGraph.UsefullHeight
        Dim LabelValue As Integer = 0
        Do While Y - intervalHeight > oGraph.PaddingTop + oFont.GetHeight
            LabelValue += intervalValue
            Y -= intervalHeight
            Dim Label As String = NumericLabel(LabelValue)
            oGraphics.DrawString(s:=Label, font:=oFont, brush:=Brushes.Black, x:=X, y:=Y)
        Loop
    End Sub

    Shared Sub DrawVerticalMarks(oGraph As DTOGraph, oGraphics As Graphics)
        Dim oPen As New Pen(Brushes.Gray, 0.5)
        Dim Y As Integer = oGraph.PaddingTop + oGraph.UsefullHeight
        For j As Integer = 0 To oGraph.ColumnsCount - 1
            Dim ColCenter As Decimal = oGraph.PaddingLeft + oGraph.ColsWidth * j + oGraph.ColsWidth / 2
            Dim X As Decimal = ColCenter
            oGraphics.DrawLine(oPen, X, oGraph.PaddingTop, X, oGraph.PaddingTop + oGraph.UsefullHeight)
        Next
    End Sub

    Shared Function NumericLabel(value As Decimal) As String
        Dim retval As String = ""
        If value >= 1000000 Then
            retval = Math.Round(value / 1000000, 0, MidpointRounding.AwayFromZero) & "M"
        ElseIf value >= 1000 Then
            retval = Math.Round(value / 1000, 0, MidpointRounding.AwayFromZero) & "K"
        Else
            retval = Math.Round(value, 0, MidpointRounding.AwayFromZero)
        End If
        Return retval
    End Function

    Shared Function xAxisMonths(oLang As DTOLang) As List(Of String)
        Dim retval As New List(Of String)
        For i = 0 To 11
            Dim iMes As Integer = i + 1
            retval.Add(oLang.MesAbr(iMes))
        Next
        Return retval
    End Function

    Shared Function Image(items As List(Of DTOProductMonthQtySalepoint)) As Byte()
        Dim oLang = DTOLang.ENG
        Dim oProveidor As DTOProveidor = DTOProveidor.Wellknown(DTOProveidor.Wellknowns.Mayborn)
        Dim lastmonth As Integer = DTO.GlobalVariables.Today().AddMonths(-1).Month
        Dim year As Integer = DTO.GlobalVariables.Today().AddMonths(-1).Year

        Dim DcValues(11) As Decimal

        Dim oGraph As New DTOGraph
        For i = 0 To 11
            Dim iMonth As Integer = i + 1
            DcValues(i) = items.Where(Function(x) x.Month = iMonth).Sum(Function(x) x.Eur)
        Next
        With oGraph
            .Title = "Volumes " & year
            .Values = DcValues.ToList
            .ColumnsCount = 12
            .EndColumn = lastmonth - 1
            .xAxisLabels = DTOGraph.xAxisMonths(oLang)
        End With
        Dim retval = oGraph.Bitmap()
        Return retval
    End Function

End Class

