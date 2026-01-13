Imports System.Drawing.Drawing2D

Public Class Xl_StatGraph
    Inherits Panel

    Private _GridHeight As Integer
    Private _GridWidth As Integer
    Private _GridBottomMargin As Integer = 10
    Private _GridLeftMargin As Integer = 20
    Private _XGap As Decimal
    Private _g As Graphics
    Private _Pens = {Pens.Blue, Pens.Red, Pens.Green}
    Private _YearMonthsCount As Integer
    Private _YearMonthFrom As DTOYearMonth
    Private _YearMonthTo As DTOYearMonth

    Private _Tooltip As ToolTip
    Private _Paths As New List(Of System.Drawing.Drawing2D.GraphicsPath)


    Property Kpis As List(Of DTOKpi)


    Public Sub Load(YearFrom As Integer)
        _YearMonthTo = DTOYearMonth.current
        _YearMonthFrom = DTOYearMonth.FromFch(New Date(YearFrom, 1, 1))

        _YearMonthsCount = DTOYearMonth.Range(_YearMonthFrom, _YearMonthTo).Count
        _Kpis = New List(Of DTOKpi)
    End Sub

    Private Sub Xl_StatGraph_Paint(sender As Object, e As PaintEventArgs) Handles Me.Paint
        _g = e.Graphics
        drawTemplate()
    End Sub

    Private Sub drawTemplate()
        Static done As Boolean
        If Not done Then
            done = True
            _GridHeight = Me.Height - _GridBottomMargin
            _GridWidth = Me.Width - _GridLeftMargin
            _XGap = _GridWidth / _YearMonthsCount

            Dim oGridPen As New Pen(Brushes.Black, 1)
            _g.DrawLine(oGridPen, _GridLeftMargin, _GridHeight, Me.Width, _GridHeight)
            _g.DrawLine(oGridPen, _GridLeftMargin, 0, _GridLeftMargin, _GridHeight)

            _Tooltip = New ToolTip
            _Tooltip.IsBalloon = True

            Dim Y = _GridHeight
            For i As Integer = 0 To _YearMonthsCount - 1
                Dim X = _GridLeftMargin + i * _XGap
                If i Mod 12 = 0 Then
                    _g.DrawLine(oGridPen, X, 0, X, Y + 5)
                Else
                    _g.DrawLine(oGridPen, X, Y - 5, X, Y + 5)
                End If
            Next
        End If
    End Sub


    Public Sub AddKpis(oKpis As List(Of DTOKpi))
        _Paths = New List(Of System.Drawing.Drawing2D.GraphicsPath)
        Dim maxValue As Decimal = oKpis.Max(Function(x) x.MaxValue)
        For Each oKpi In oKpis
            _Kpis.Add(oKpi)
            oKpi.YFactor = _GridHeight / maxValue

            Dim oPath = GetPath(oKpi)
            If oPath IsNot Nothing Then
                _Paths.Add(oPath)
                drawKpi(oKpi, oPath, _Pens(_Kpis.Count - 1))
            End If
        Next
    End Sub

    Private Sub drawKpi(oKpi As DTOKpi, oPath As GraphicsPath, oPen As Pen)
        'oKpi.Path = GetPath(oKpi)
        _g = Me.CreateGraphics()
        _g.DrawPath(oPen, oPath)
    End Sub

    Private Function GetPath(oKpi As DTOKpi) As GraphicsPath
        Dim oPoints As New List(Of PointF)
        For idx As Integer = 0 To _YearMonthsCount - 1
            Dim oPoint = GetPoint(oKpi, idx)
            oPoints.Add(oPoint)
        Next

        Dim retval As New GraphicsPath
        retval.AddLines(oPoints.ToArray)
        Return retval
    End Function

    Private Function GetPoint(oKpi As DTOKpi, idx As Integer) As PointF
        Dim X As Single = _GridLeftMargin + _XGap * idx
        Dim Y As Single = 0
        Dim oYearMonth = _YearMonthFrom.AddMonths(idx)
        Dim oKpiYearMonth = oKpi.YearMonths.FirstOrDefault(Function(z) z.Equals(oYearMonth))
        If oKpiYearMonth IsNot Nothing Then
            Y = _GridHeight - oKpi.YFactor * oKpiYearMonth.Eur
        End If
        Dim retval As New PointF(X, Y)
        Return retval
    End Function

    Private Sub Xl_StatGraph_MouseMove(sender As Object, e As MouseEventArgs) Handles Me.MouseMove
        If _Tooltip IsNot Nothing Then
            If _Kpis IsNot Nothing Then
                Dim oMousePoint = New Point(e.X, e.Y)
                Dim oWidePen As New Pen(Brushes.Red, 10)
                If _Paths IsNot Nothing Then
                    If _Paths.Any(Function(x) x.IsOutlineVisible(oMousePoint, oWidePen)) Then
                        'estas tocant una linia
                        For i = 0 To _Paths.Count - 1
                            If _Paths(i).IsOutlineVisible(oMousePoint, oWidePen) Then
                                SetToolTip(_Kpis(i), oMousePoint)
                            End If
                        Next
                    Else
                        SetToolTip(oMousePoint)
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub SetToolTip(oKpi As DTOKpi, oMousePoint As Point)
        Dim value = (_GridHeight - oMousePoint.Y) / oKpi.YFactor
        Dim msg As String = ""

        Select Case oKpi.format
            Case DTOKpi.Formats.Eur
                msg = String.Format("{0}{1}{2}", "hola", Environment.NewLine, DTOAmt.CurFormatted(value))
            Case DTOKpi.Formats.Decimal
                msg = String.Format("{0}{1}{2}", "hola", Environment.NewLine, value)
        End Select
        _Tooltip.SetToolTip(Me, msg)
    End Sub

    Private Sub SetToolTip(oMousePoint As Point)
        Dim oRange = DTOYearMonth.Range(_YearMonthFrom, _YearMonthTo)
        Dim idx = YearMonthIdx(oMousePoint)
        Dim oYearMonth = oRange(idx)
        Dim sYearMonth = oYearMonth.Caption(Current.Session.Lang)
        Dim msg As String = sYearMonth

        If isFonsDeManiobra() Then
            Dim oActivoCorriente = _Kpis.First(Function(x) x.Id = DTOKpi.Ids.Activo_Corriente)
            Dim oPasivoCorriente = _Kpis.First(Function(x) x.Id = DTOKpi.Ids.Pasivo_Corriente)
            Dim dcActivoCorriente = oActivoCorriente.YearMonths(idx).Eur
            Dim dcPasivoCorriente = oPasivoCorriente.YearMonths(idx).Eur
            Dim dcFonsDeManiobra = dcActivoCorriente - dcPasivoCorriente
            Dim dcRatioLiquidez = dcActivoCorriente / dcPasivoCorriente
            Dim sb As New Text.StringBuilder
            sb.AppendFormat("Actiu corrent:   {0}", DTOAmt.CurFormatted(dcActivoCorriente))
            sb.Append(Environment.NewLine)
            sb.AppendFormat("Passiu corrent:  {0}", DTOAmt.CurFormatted(dcPasivoCorriente))
            sb.Append(Environment.NewLine)
            sb.AppendFormat("Fons maniobra:   {0}", DTOAmt.CurFormatted(dcFonsDeManiobra))
            sb.Append(Environment.NewLine)
            sb.AppendFormat("Rati liquidesa:  {0}", dcRatioLiquidez)
            msg = sb.ToString
        Else
        End If
        _Tooltip.SetToolTip(Me, msg)
    End Sub

    Private Function YearMonthIdx(oMousePoint As Point) As Integer
        Dim retval = oMousePoint.X / (_GridLeftMargin + _XGap)
        'Dim oRange = DTOYearMonth.Range(_YearMonthFrom, _YearMonthTo)
        'Dim retval = oRange(idx)
        Return retval
    End Function

    Private Function isFonsDeManiobra() As Boolean
        Dim isActivoCorriente = _Kpis.Any(Function(x) x.Id = DTOKpi.Ids.Activo_Corriente)
        Dim isPasivoCorriente = _Kpis.Any(Function(x) x.Id = DTOKpi.Ids.Pasivo_Corriente)
        Return isActivoCorriente And isPasivoCorriente
    End Function
End Class
