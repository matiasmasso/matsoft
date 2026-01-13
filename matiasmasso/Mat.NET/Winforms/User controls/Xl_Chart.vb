Imports System.Drawing.Drawing2D

Public Class Xl_Chart
    Inherits Panel

    Private _values As List(Of DTOKpi)
    Private _factor As Decimal
    Private _module As Integer = 2 'pixels que separen un dia del següent
    Private _graphics As Graphics

    Public Shadows Sub Load(values As List(Of DTOKpi))
        _values = values
        _factor = Factor()

        _graphics = Me.CreateGraphics
        Dim minFch As Date = _values.SelectMany(Function(x) x.Items).Min(Function(y) y.Fch)
        For Each oKpi In values
            Dim points As New List(Of Point)
            Dim DtFch As Date = Today
            Do Until DtFch < minFch
                Dim item As DTOKpi.Item = oKpi.Items.FirstOrDefault(Function(x) x.Fch = DtFch)
                Dim xPos As Integer = MyBase.Size.Width - _module * DateDiff(DateInterval.Day, DtFch, Today)
                Dim yPos As Integer = 0
                If item IsNot Nothing Then
                    yPos = item.Qty * _factor
                End If
                Dim point As New Point(xPos, yPos)
                points.Add(point)
                DtFch = DtFch.AddDays(-1)
            Loop
            Dim path As New GraphicsPath()
            path.AddLines(points.ToArray)
            Dim pen As New Pen(oKpi.Color, 1)
            _graphics.DrawPath(pen, path)
        Next
    End Sub

    Private Function Factor() As Decimal
        Dim retval As Decimal
        Dim iHeight As Integer = MyBase.Size.Height
        Dim maxQty As Integer = _values.SelectMany(Function(x) x.Items).Max(Function(y) y.Qty)
        If maxQty > 0 Then retval = iHeight / maxQty
        Return retval
    End Function

    Public Class Kpi

        Property Caption As String
        Property Color As Color
        Property Items As List(Of Item)
        Property Tag As Object

        Public Sub New(Caption As String, color As Color)
            MyBase.New
            _Caption = Caption
            _Color = color
            _Items = New List(Of Item)
        End Sub

        Public Sub AddItem(fch As Date, qty As Integer, tag As Object)
            Dim item As New Item()
            With item
                .Fch = fch
                .Qty = qty
                .Tag = tag
            End With
            _Items.Add(item)
        End Sub

        Public Class Item
            Property Fch As Date
            Property Qty As Integer
            Property Tag As Object
        End Class
    End Class

End Class
