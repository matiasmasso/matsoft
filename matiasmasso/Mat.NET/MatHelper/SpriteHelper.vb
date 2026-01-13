Public Class SpriteHelper

    Public Const MAXCOLUMNS As Integer = 10

    'DEPRECATED - using SpriteBuilder on server due to legacy reference system.drawing
    Shared Function Factory(items As List(Of Image), itemWidth As Integer, itemHeight As Integer) As Image
        Dim iCols = Math.Min(MAXCOLUMNS, items.Count)
        Dim iRows = Math.Ceiling(items.Count / iCols)
        Dim retval = BlankBitmap(itemWidth, itemHeight, iCols, iRows)

        Dim iCol, iRow As Integer
        Using GraphicsObject As Graphics = Graphics.FromImage(retval)
            For Each item As Image In items

                Dim oRectangle = DrawingRectangle(itemWidth, itemHeight, iCol, iRow)
                GraphicsObject.FillRectangle(Brushes.White, oRectangle)
                If item IsNot Nothing Then GraphicsObject.DrawImage(item, oRectangle)

                NextPos(iCols, iCol, iRow)
            Next
        End Using

        Return retval
    End Function


    Shared Function Factory(url As String, itemWidth As Integer, itemHeight As Integer) As Sprite
        Dim retval As New Sprite
        With retval
            .url = url
            .itemWidth = itemWidth
            .itemHeight = itemHeight
            .items = New List(Of Sprite.Item)
        End With
        Return retval
    End Function


    Shared Function extract(ByVal oSprite As Image, idx As Integer, count As Integer) As Image
        Dim iCols = Math.Min(MAXCOLUMNS, count)
        Dim iRows = Math.Ceiling(count / iCols)
        Dim itemWidth As Integer = oSprite.Width / iCols
        Dim itemHeight As Integer = oSprite.Height / iRows
        Dim iRow = Fix(idx / iCols)
        Dim iCol = idx - (iCols * iRow)
        Dim X = iCol * itemWidth
        Dim Y = iRow * itemHeight
        Dim retval As New Bitmap(itemWidth, itemHeight)
        Using grp = Graphics.FromImage(retval)
            Dim srcRect As New Rectangle(X, Y, itemWidth, itemHeight)
            Dim destRect As New Rectangle(0, 0, itemWidth, itemHeight)
            grp.DrawImage(oSprite, destRect, srcRect, GraphicsUnit.Pixel)
        End Using

        Return retval
    End Function

    Shared Function Coordenadas(idx As Integer, count As Integer, itemWidth As Integer, itemHeight As Integer) As Point
        Dim iCols = Math.Min(MAXCOLUMNS, count)
        Dim iRows = Math.Ceiling(count / iCols)
        Dim iRow = Fix(idx / iCols)
        Dim iCol = idx - (iCols * iRow)
        Dim retval As New Point(iCol * itemWidth, iRow * itemHeight)
        Return retval
    End Function

    Private Shared Function DrawingRectangle(itemWidth As Integer, itemHeight As Integer, iCol As Integer, iRow As Integer) As RectangleF
        Dim X As Integer = iCol * itemWidth
        Dim Y As Integer = iRow * itemHeight
        Dim retval As New RectangleF(X, Y, itemWidth, itemHeight)
        Return retval
    End Function

    Private Shared Function BlankBitmap(itemWidth As Integer, itemHeight As Integer, iCols As Integer, iRows As Integer) As Bitmap
        Dim CombinedWidth As Integer = itemWidth * iCols
        Dim CombinedHeight As Integer = itemHeight * iRows
        Dim retval As New Bitmap(CombinedWidth, CombinedHeight)
        Return retval
    End Function

    Private Shared Sub NextPos(ByVal iCols As Integer, ByRef iCol As Integer, ByRef iRow As Integer)
        iCol += 1
        If iCol >= iCols Then
            iRow += 1
            iCol = 0
        End If
    End Sub

    Public Class Sprite
        Property url As String
        Property itemWidth As Integer
        Property itemHeight As Integer
        Property items As List(Of Item)

        Private _col As Integer
        Private _row As Integer


        Public Function addItem(caption As String, url As String) As Item
            Dim retval As New Item
            With retval
                .idx = _items.Count
                .width = _itemWidth
                .height = _itemHeight
                .caption = caption
                .url = url
                .col = _col
                .row = Fix(.idx / MAXCOLUMNS)
            End With
            _items.Add(retval)
            _col = IIf(_col + 1 = MAXCOLUMNS, 0, _col + 1)
            Return retval
        End Function


        Public Class Item
            Property idx As Integer
            Property col As Integer
            Property row As Integer
            Property width As Integer
            Property height As Integer
            Property caption As String
            Property url As String

            Public Function offsetX() As Integer
                Return -_col * _width
            End Function

            Public Function offsetY() As Integer
                Return -_row * _height
            End Function
        End Class
    End Class
End Class