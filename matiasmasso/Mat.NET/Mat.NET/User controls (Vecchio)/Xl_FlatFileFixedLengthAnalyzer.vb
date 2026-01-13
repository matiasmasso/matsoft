Imports System.ComponentModel

Public Class Xl_FlatFileFixedLengthAnalyzer
    Private _DataSource As List(Of String)
    Private _Graphics As Graphics

    Private _CharWidth As Integer = 15
    Private _CharHeight As Integer = 15
    Private _GridSize As Size
    Private _GridColor As Color = Color.LightGray
    Private _GridLineWidth As Integer = 1
    Private _GridPen As New Pen(Brushes.LightGray, _GridLineWidth)
    Private _FontFamily As String = "Courier New"
    Private _FontSize As Integer = 10
    Private _Font As New Font(_FontFamily, _FontSize)
    Private _ToolTip As New ToolTip

    Public Sub New(oDataSource As List(Of String))
        MyBase.New()
        Me.InitializeComponent()
        _DataSource = oDataSource
        Refresca()
    End Sub

    Public WriteOnly Property DataSource As List(Of String)
        Set(value As List(Of String))
            _DataSource = value
            Refresca()
        End Set
    End Property

    Private Sub Refresca()
        PictureBox1.Image = Bitmap()
    End Sub

    Private Function Bitmap() As Bitmap
        Dim YPos As Integer = 0
        Dim XPos As Integer = 0
        _GridSize = GridSize()

        Dim retval As New Bitmap(_GridSize.Width, _GridSize.Height)
        _Graphics = Graphics.FromImage(retval)
        _Graphics.FillRectangle(Brushes.White, 0, 0, _GridSize.Width, _GridSize.Height)

        For iRow As Integer = 0 To _DataSource.Count - 1
            DrawHorizontalGridLine(iRow)
            For iCol As Integer = 0 To MaxCharsLength() - 1
                If iRow = 0 Then DrawVerticalGridLine(iCol)
                DrawChar(iRow, iCol)
            Next
        Next

        Return retval
    End Function

    Private Sub DrawHorizontalGridLine(iRow As Integer)
        Dim YPos As Integer = VPos(iRow)
        _Graphics.DrawLine(_GridPen, 0, YPos, _GridSize.Width, YPos)
    End Sub

    Private Sub DrawVerticalGridLine(iCol As Integer)
        Dim XPos As Integer = HPos(iCol)
        _Graphics.DrawLine(_GridPen, XPos, 0, XPos, _GridSize.Height)
    End Sub

    Private Sub DrawChar(iRow As Integer, iCol As Integer)
        Dim sLine As String = _DataSource(iRow)
        If sLine.Length > iCol Then
            Dim sChar As String = sLine.Substring(iCol, 1)
            _Graphics.DrawString(sChar, _Font, Brushes.Black, HPos(iCol), VPos(iRow))
        End If
    End Sub

    Private Function VPos(iRow As Integer) As Integer
        Dim retval As Integer = iRow * _CharHeight
        Return retval
    End Function

    Private Function HPos(iCol As Integer) As Integer
        Dim retval As Integer = iCol * _CharWidth
        Return retval
    End Function

    Private Function Row(VPos As Integer) As Integer
        Dim retval As Integer
        Dim iMargin As Integer = 3
        Dim iMod As Integer = VPos Mod _CharHeight
        If (iMod < iMargin Or iMod > _CharHeight - iMargin) Then
            retval = -1
        Else
            retval = Int(VPos / _CharHeight)
        End If
        Return retval
    End Function

    Private Function Col(HPos As Integer) As Integer
        Dim retval As Integer
        Dim iMargin As Integer = 3
        Dim iMod As Integer = HPos Mod _CharWidth
        If (iMod < iMargin Or iMod > _CharWidth - iMargin) Then
            retval = -1
        Else
            retval = Int(HPos / _CharWidth)
        End If
        Return retval
    End Function

    Private Function MaxCharsLength() As Integer
        Dim retval As Integer
        For Each sLine As String In _DataSource
            If sLine.Length > retval Then
                retval = sLine.Length
            End If
        Next
        Return retval
    End Function

    Private Function GridSize() As Size
        Dim iWidth As Integer = MaxCharsLength() * _CharWidth
        Dim iHeight = _DataSource.Count * _CharHeight
        Dim retval As New Size(iWidth, iHeight)
        Return retval
    End Function

    Private Sub PictureBox1_MouseLeave(sender As Object, e As EventArgs) Handles PictureBox1.MouseLeave
        _ToolTip1.Hide(PictureBox1)
    End Sub

    Private Sub PictureBox1_MouseMove(sender As Object, e As MouseEventArgs) Handles PictureBox1.MouseMove
        Dim iCol As Integer = Col(e.X)
        Dim iRow As Integer = Row(e.Y)
        If iCol < 0 Or iRow < 0 Then
            _ToolTip.Hide(PictureBox1)
        Else
            _ToolTip.SetToolTip(PictureBox1, iRow.ToString & "," & iCol.ToString)
        End If
    End Sub

End Class
