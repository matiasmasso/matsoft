Public Class SpriteBuilder


    Public Const MAXCOLUMNS As Integer = 10

    'exclusively use on server due to legacy reference system.drawing not supported by .Net Standard

    Shared Function Factory(items As List(Of Byte()), Optional itemWidth As Integer = 0, Optional itemHeight As Integer = 0) As Byte()
        Dim retval As Byte() = Nothing
        If items.Count > 0 Then
            Dim iCols = Math.Min(MAXCOLUMNS, items.Count)
            Dim iRows = Math.Ceiling(items.Count / iCols)
            If items.Count > 0 And itemWidth = 0 And itemHeight = 0 Then
                itemWidth = 150 ' items.First.Width
                itemHeight = 150 ' items.First.Height
            End If

            Dim oLegacySprite = BlankBitmap(itemWidth, itemHeight, iCols, iRows)

            Dim iCol, iRow As Integer
            Using GraphicsObject As Graphics = Graphics.FromImage(oLegacySprite)
                For Each item In items

                    Dim oRectangle = DrawingRectangle(itemWidth, itemHeight, iCol, iRow)
                    GraphicsObject.FillRectangle(Brushes.White, oRectangle)


                    If item IsNot Nothing Then
                        Dim oThumbnail As System.Drawing.Image = Nothing
                        If item.Length = 0 Then
                            oThumbnail = New System.Drawing.Bitmap(itemWidth, itemHeight)
                        Else
                            Dim oLegacyImage As Image = Nothing
                            Try
                                oLegacyImage = ImageHelper.FromBytes(item)
                            Catch ex As Exception
                                oLegacyImage = My.Resources.cartadeajuste
                            End Try
                            oThumbnail = ImageHelper.GetThumbnailToFill(oLegacyImage, itemWidth, itemHeight)
                        End If
                        GraphicsObject.DrawImage(oThumbnail, oRectangle)
                    End If

                    NextPos(iCols, iCol, iRow)
                Next
            End Using

            retval = oLegacySprite.Bytes()
        End If
        Return retval
    End Function

    Private Shared Function DrawingRectangle(itemWidth As Integer, itemHeight As Integer, iCol As Integer, iRow As Integer) As System.Drawing.RectangleF
        Dim X As Integer = iCol * itemWidth
        Dim Y As Integer = iRow * itemHeight
        Dim retval As New System.Drawing.RectangleF(X, Y, itemWidth, itemHeight)
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

End Class
