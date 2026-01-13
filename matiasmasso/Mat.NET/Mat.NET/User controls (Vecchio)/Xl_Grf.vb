Public Class Xl_Grf
    Private _Grf As DTOGrf
    Private _fchFrom As Date
    Private _fchTo As Date
    Private _Graphics As Graphics
    Private _Pen As Pen
    Private _BarWidth As Integer
    Private _Bottom As Integer
    Private _Top As Integer
    Private _TopMargin As Integer = 10
    Private _BottomMargin As Integer = 10
    Private _LeftMargin As Integer = 10
    Private _RightMargin As Integer = 10
    Private _Counts As List(Of Integer)
    Private _MaxHeight As Integer
    Private _MaxWidth As Integer
    Private _Factor As Decimal
    Private _LastHoverItem As ControlItem

    Private _ControlItems As ControlItems


    Public Shadows Sub Load(oGrf As DTOGrf)
        _Grf = oGrf
        If oGrf.FchFrom = Nothing Then
            _fchFrom = oGrf.Items.Min(Function(x) x.Fch)
        Else
            _fchFrom = oGrf.FchFrom
        End If

        If oGrf.FchTo = Nothing Then
            _fchTo = oGrf.Items.Max(Function(x) x.Fch)
        Else
            _fchTo = oGrf.FchTo
        End If

        '_Graphics = Graphics.FromHwnd(MyBase.Handle)
        _Pen = New Pen(Brushes.Black)
        _Top = _TopMargin
        _Counts = New List(Of Integer)

        Dim iCount As Integer = DateDiff(_Grf.DateInterval, _fchFrom, _fchTo)

        _ControlItems = New ControlItems
        For i As Integer = 0 To iCount - 1
            Dim minFch As Date = _fchFrom.AddHours(i)
            Dim maxFch As Date = minFch.AddHours(1).AddMilliseconds(-1) 'TODO Ojo quan interval no son hores
            Dim iValue As Integer = _Grf.Items.Where(Function(item) (item.Fch >= minFch And item.Fch <= maxFch)).Count
            Dim oItem As New ControlItem(minFch, iValue)
            _ControlItems.Add(oitem)
        Next

        refresca()
    End Sub

    Private Sub Xl_Grf_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        refresca()
    End Sub


    Private Sub refresca()
        If _ControlItems IsNot Nothing Then
            If _ControlItems.Count > 0 Then
                _Graphics = Graphics.FromHwnd(MyBase.Handle)
                _MaxWidth = MyBase.Width - _LeftMargin - _RightMargin
                _BarWidth = _MaxWidth / _ControlItems.Count
                _Bottom = MyBase.Height - _BottomMargin
                _MaxHeight = _Bottom - _Top
                _Factor = _ControlItems.MaxVal / _MaxHeight

                For Each oControlItem In _ControlItems
                    Draw(oControlItem, Color.Blue)
                Next
            End If
            'Me.Refresh
        End If
    End Sub

    Private Sub Draw(oControlItem As ControlItem, oColor As System.Drawing.Color)
        Dim oRectangle As Rectangle = Rectangle(oControlItem)
        Dim oBrush As New SolidBrush(oColor)
        _Graphics.FillRectangle(oBrush, oRectangle)
    End Sub

    Private Function Rectangle(oControlItem As ControlItem) As Rectangle
        Dim idx As Integer = _ControlItems.IndexOf(oControlItem)
        Dim iHeight As Integer
        If _Factor > 0 Then
            iHeight = oControlItem.Value / _Factor
        End If
        Dim X As Integer = _LeftMargin + idx * _BarWidth
        Dim Y As Integer = _Bottom - iHeight
        Dim retval As New Rectangle(X, Y, _BarWidth, iHeight)
        Return retval
    End Function

    Private Sub Xl_Grf_MouseMove(sender As Object, e As MouseEventArgs) Handles Me.MouseMove
        If _LastHoverItem IsNot Nothing Then
            Draw(_LastHoverItem, Color.Blue)
        End If
        If _BarWidth > 0 Then
            Dim oPoint As System.Drawing.Point = Me.PointToClient(Control.MousePosition)
            Dim idx As Integer = (e.X - _LeftMargin) / _BarWidth
            If idx >= 0 And idx < _ControlItems.Count Then
                Dim oControlItem As ControlItem = _ControlItems(idx)
                Label1.Text = Format(oControlItem.Fch, "dd/MM/yy HH:mm") & " " & Format(oControlItem.Value, "#,##0") & " participants"
                Draw(oControlItem, Color.Red)
                _LastHoverItem = oControlItem
            Else
                Label1.Text = ""
            End If
        End If
    End Sub



    Protected Class ControlItem
        Property Fch As Date
        Property Value As Integer

        Public Sub New(DtFch As Date, iValue As Integer)
            MyBase.New()
            _Fch = DtFch
            _Value = iValue
        End Sub

    End Class

    Protected Class ControlItems
        Inherits List(Of ControlItem)

        Public Function MaxVal() As Integer
            Dim retval As Integer = MyBase.Max(Function(x) x.Value)
            Return retval
        End Function

    End Class


End Class
