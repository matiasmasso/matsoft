Public Class SpriteHelper

    Public Const MAXCOLUMNS As Integer = 10

    Shared Function Factory(items As List(Of Byte()), itemWidth As Integer, itemHeight As Integer) As Byte()
        Dim iCols = Math.Min(MAXCOLUMNS, items.Count)
        Dim iRows = Math.Ceiling(items.Count / iCols)
        Dim retval = BlankBitmap(itemWidth, itemHeight, iCols, iRows)

        Dim iCol, iRow As Integer
        Using GraphicsObject As Graphics = Graphics.FromImage(retval)
            For Each item As Byte() In items
                Dim oLegacyImage = ImageHelper.FromBytes(item)
                Dim oRectangle = DrawingRectangle(itemWidth, itemHeight, iCol, iRow)
                GraphicsObject.FillRectangle(Brushes.White, oRectangle)
                If item IsNot Nothing Then GraphicsObject.DrawImage(oLegacyImage, oRectangle)

                NextPos(iCols, iCol, iRow)
            Next
        End Using

        Return retval.Bytes
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

    Shared Function Extract(ByVal oSpriteBytes As Byte(), idx As Integer, count As Integer) As Byte()
        Dim retval As Byte() = Nothing
        If oSpriteBytes IsNot Nothing Then
            Dim oSprite = ImageHelper.Converter(oSpriteBytes)
            retval = Extract(oSprite, idx, count)
        End If
        Return retval
    End Function

    Shared Function Extract(ByVal oSprite As Image, idx As Integer, count As Integer) As Byte()
        Dim retval As Byte() = Nothing
        If oSprite IsNot Nothing Then

            Dim iCols = Math.Min(SpriteHelper.MAXCOLUMNS, count)
            Dim iRows = Math.Ceiling(count / iCols)
            Dim itemWidth As Integer = oSprite.Width / iCols
            Dim itemHeight As Integer = oSprite.Height / iRows
            Dim iRow = Math.Truncate(idx / iCols)
            Dim iCol = idx - (iCols * iRow)
            Dim X = iCol * itemWidth
            Dim Y = iRow * itemHeight
            Dim oRectangle As New Rectangle(X, Y, itemWidth, itemHeight)

            Using oBitmap As New Bitmap(oSprite)
                Using cropped As Bitmap = oBitmap.Clone(oRectangle, oBitmap.PixelFormat)
                    Using ms As New IO.MemoryStream
                        cropped.Save(ms, Imaging.ImageFormat.Jpeg)
                        retval = ms.ToArray
                    End Using
                End Using
            End Using
        End If
        Return retval
    End Function


End Class
