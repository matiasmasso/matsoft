Imports System.Drawing.Drawing2D

Public Class Frm_WtbolChart
    Private Sub Frm_WtbolChart_Load(sender As Object, e As EventArgs) Handles Me.Load
        Xl_Chart1.Visible = False
        drawLine()
    End Sub

    Private Sub drawLine()
        Dim myPen As New System.Drawing.Pen(System.Drawing.Color.Red, 3)
        Dim formGraphics As System.Drawing.Graphics
        formGraphics = Me.CreateGraphics()
        formGraphics.DrawLine(myPen, 0, 0, 200, 200)
        myPen.Dispose()
        formGraphics.Dispose()
    End Sub
    Private Sub drawRect()
        Dim myGraphics As Graphics

        Dim myRectangle As Rectangle

        Dim myPen As New Pen(Color.Blue)

        'return the current form as a drawing surface

        myGraphics = Graphics.FromHwnd(ActiveForm().Handle)

        'create a rectangle based on x,y coordinates, width, & height

        myRectangle = New Rectangle(x:=5, y:=5, width:=10, height:=40)

        'draw rectangle from pen and rectangle objects

        myGraphics.DrawRectangle(pen:=myPen, rect:=myRectangle)

        'create a rectangle based on Point and Size objects

        myRectangle = New Rectangle(location:=New Point(10, 10), size:=New Size(width:=20, height:=60))

        'draw another rectangle from Pen and new Rectangle object

        myGraphics.DrawRectangle(pen:=myPen, rect:=myRectangle)

        'draw a rectangle from a Pen object, a rectangle's x & y, 

        ' width, & height

        myGraphics.DrawRectangle(pen:=myPen, x:=20, y:=20, width:=30, height:=80)
    End Sub
    Private Sub DrawSerps()
        Dim oSerps = BLLWtbolSerps.All()
        Dim oCtrs = BLLWtbolCtrs.All()
        Dim oSerpFchs = oSerps.Select(Function(x) x.Fch)
        Dim oCtrFchs = oCtrs.Select(Function(x) x.Fch)
        'Dim q = values.GroupBy(Function(g) New With {Key g.Fch, Key g.Hour}).
        'Select Case (Function(group) New With {group.Key.Date, group.Key.Hour, group.Count()})
        Dim q = oSerpFchs.GroupBy(Function(g) New With {Key g.Date.Date(), Key g.Day}).
            Select(Function(group) New With {group.Key.Date, group.Count()}).ToList
        Dim oKpiSerps As New Xl_Chart.Kpi("Serps", Color.Navy)
        For Each o In q
            oKpiSerps.AddItem(o.Date, o.Count, Nothing)
        Next
        Dim t = oCtrFchs.GroupBy(Function(g) New With {Key g.Date.Date(), Key g.Day}).
            Select(Function(group) New With {group.Key.Date, group.Count()}).ToList
        Dim oKpiCtrs As New Xl_Chart.Kpi("Ctrs", Color.Red)
        For Each o In t
            oKpiCtrs.AddItem(o.Date, o.Count, Nothing)
        Next

        Dim oKpis As New List(Of Xl_Chart.Kpi)
        oKpis.Add(oKpiSerps)
        oKpis.Add(oKpiCtrs)

        'Xl_Chart1.Load(oKpis)
        draw(oKpis)

    End Sub

    Private Sub draw(values As List(Of Xl_Chart.Kpi))

        Dim _values = values
        Dim _factor As Decimal
        Dim _module As Integer = 2 'pixels que separen un dia del següent

        Dim iHeight As Integer = MyBase.Size.Height
        Dim maxQty As Integer = _values.SelectMany(Function(x) x.Items).Max(Function(y) y.Qty)
        If maxQty > 0 Then _factor = iHeight / maxQty


        Dim _graphics = Me.CreateGraphics
        Dim minFch As Date = _values.SelectMany(Function(x) x.Items).Min(Function(y) y.Fch)
        For Each oKpi In values
            Dim points As New List(Of Point)
            Dim DtFch As Date = Today
            Do Until DtFch < minFch
                Dim item As Xl_Chart.Kpi.Item = oKpi.Items.FirstOrDefault(Function(x) x.Fch = DtFch)
                Dim xPos As Integer = MyBase.Size.Width - _module * DateDiff(DateInterval.Day, DtFch, Today)
                Dim yPos As Integer = 0
                If item IsNot Nothing Then
                    yPos = item.Qty * _factor
                End If
                Dim point As New Point(xPos, yPos)
                points.Add(point)
                DtFch = DtFch.AddDays(-1)
            Loop
            Dim path As New GraphicsPath
            path.AddLines(points.ToArray)
            Dim pen As New Pen(oKpi.Color, 1)
            _graphics.DrawPath(pen, path)
        Next
    End Sub



End Class