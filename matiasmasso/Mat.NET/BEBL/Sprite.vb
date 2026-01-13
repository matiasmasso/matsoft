Imports SixLabors.ImageSharp
Public Class Sprite


#Region "CRUD"
    Shared Function Find(sHash As String) As DTOSprite
        Dim retval As DTOSprite = SpriteLoader.Find(sHash)
        Return retval
    End Function


    Shared Function Delete(oSprite As DTOSprite, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = SpriteLoader.Delete(oSprite, exs)
        Return retval
    End Function
#End Region

    Shared Function Image(oSkus As List(Of DTOProductSku), iItemWidth As Integer) As Image
        Dim ColumnsCount As Integer = DTOSprite.MAXCOLUMNS
        Dim iRows As Integer = Fix((oSkus.Count - 1) / ColumnsCount) + 1
        Dim iCols As Integer = IIf(oSkus.Count > ColumnsCount, ColumnsCount, oSkus.Count)
        Dim iThumbnailHeight As Integer = 400 * iItemWidth / 350
        Dim oBitmap As New Bitmap(iCols * iItemWidth, iRows * iThumbnailHeight)
        ProductSkusLoader.LoadImages(oSkus)

        Dim iCol As Integer = 0
        Dim iRow As Integer = 0
        Using GraphicsObject As Graphics = Graphics.FromImage(oBitmap)
            ' uncomment for higher quality output
            'graph.InterpolationMode = InterpolationMode.High;
            'graph.CompositingQuality = CompositingQuality.HighQuality;
            'graph.SmoothingMode = SmoothingMode.AntiAlias;
            For Each oSku As DTOProductSku In oSkus
                Dim X As Integer = iCol * iItemWidth
                Dim Y As Integer = iRow * iThumbnailHeight
                Dim oRectangle As New RectangleF(X, Y, iItemWidth, iThumbnailHeight)
                GraphicsObject.FillRectangle(Brushes.White, oRectangle)
                If oSku.image Is Nothing Then
                    GraphicsObject.DrawImage(My.Resources.NoImg140, oRectangle)
                Else
                    GraphicsObject.DrawImage(oSku.image, oRectangle)
                End If

                iCol += 1
                If iCol >= ColumnsCount Then
                    iRow += 1
                    iCol = 0
                End If
            Next
        End Using

        Dim retval As Image = oBitmap
        Return retval
    End Function

    Shared Function Factory(items As List(Of DTOProductCategory), iItemWidth As Integer, sHash As String, exs As List(Of Exception)) As DTOSprite

        Dim iCols As Integer = DTOSprite.MAXCOLUMNS
        Dim iRows As Integer = Fix((items.Count - 1) / iCols) + 1
        Dim iThumbnailHeight As Integer = 100 * iItemWidth / 130
        Dim oBitmap As New Bitmap(iCols * iItemWidth, iRows * iThumbnailHeight)
        ProductCategoriesLoader.LoadImages(items)

        Dim iCol As Integer = 0
        Dim iRow As Integer = 0
        Using GraphicsObject As Graphics = Graphics.FromImage(oBitmap)
            ' uncomment for higher quality output
            'graph.InterpolationMode = InterpolationMode.High;
            'graph.CompositingQuality = CompositingQuality.HighQuality;
            'graph.SmoothingMode = SmoothingMode.AntiAlias;
            For Each item As DTOProductCategory In items
                Dim X As Integer = iCol * iItemWidth
                Dim Y As Integer = iRow * iThumbnailHeight
                Dim oRectangle As New RectangleF(X, Y, iItemWidth, iThumbnailHeight)
                GraphicsObject.FillRectangle(Brushes.White, oRectangle)
                If item.thumbnail Is Nothing Then
                    GraphicsObject.DrawImage(My.Resources.NoImg140, oRectangle)
                Else
                    GraphicsObject.DrawImage(item.thumbnail, oRectangle)
                End If

                iCol += 1
                If iCol >= iCols Then
                    iRow += 1
                    iCol = 0
                End If
            Next
        End Using

        Dim retval As New DTOSprite(sHash)
        retval.Image = oBitmap

        SpriteLoader.Update(retval, exs)
        Return retval
    End Function


    Shared Function Factory(oSkus As List(Of DTOProductSku), iItemWidth As Integer, sHash As String, exs As List(Of Exception)) As DTOSprite
        Dim retval As New DTOSprite(sHash)
        retval.Image = Image(oSkus, iItemWidth)

        SpriteLoader.Update(retval, exs)
        Return retval
    End Function


    Shared Function Factory(oBancs As List(Of DTOBanc), iItemWidth As Integer, sHash As String, exs As List(Of Exception)) As DTOSprite

        Dim ColumnsCount As Integer = DTOSprite.MAXCOLUMNS
        Dim iRows As Integer = Fix((oBancs.Count - 1) / ColumnsCount) + 1
        Dim iCols As Integer = IIf(oBancs.Count > ColumnsCount, ColumnsCount, oBancs.Count)
        Dim iThumbnailHeight As Integer = iItemWidth 'es quadrat
        Dim oBitmap As New Bitmap(iCols * iItemWidth, iRows * iThumbnailHeight)
        Dim items As List(Of Image) = BancsLoader.Logos(oBancs)

        Dim iCol As Integer = 0
        Dim iRow As Integer = 0
        Using GraphicsObject As Graphics = Graphics.FromImage(oBitmap)
            ' uncomment for higher quality output
            'graph.InterpolationMode = InterpolationMode.High;
            'graph.CompositingQuality = CompositingQuality.HighQuality;
            'graph.SmoothingMode = SmoothingMode.AntiAlias;
            For Each item As Image In items
                Dim X As Integer = iCol * iItemWidth
                Dim Y As Integer = iRow * iThumbnailHeight
                Dim oRectangle As New RectangleF(X, Y, iItemWidth, iThumbnailHeight)
                GraphicsObject.FillRectangle(Brushes.White, oRectangle)
                If item Is Nothing Then
                    GraphicsObject.DrawImage(My.Resources.NoImg140, oRectangle)
                Else
                    GraphicsObject.DrawImage(item, oRectangle)
                End If

                iCol += 1
                If iCol >= ColumnsCount Then
                    iRow += 1
                    iCol = 0
                End If
            Next
        End Using

        Dim retval As New DTOSprite(sHash)
        retval.Image = oBitmap

        SpriteLoader.Update(retval, exs)
        Return retval
    End Function

    Shared Function Factory(items As List(Of DTOStaff), iItemWidth As Integer, sHash As String, exs As List(Of Exception)) As DTOSprite

        Dim ColumnsCount As Integer = DTOSprite.MAXCOLUMNS
        Dim iRows As Integer = Fix((items.Count - 1) / ColumnsCount) + 1
        Dim iCols As Integer = IIf(items.Count > ColumnsCount, ColumnsCount, items.Count)
        Dim iThumbnailHeight As Integer = 400 * iItemWidth / 350
        Dim oBitmap As New Bitmap(iCols * iItemWidth, iRows * iThumbnailHeight)

        Dim iCol As Integer = 0
        Dim iRow As Integer = 0
        Using GraphicsObject As Graphics = Graphics.FromImage(oBitmap)
            ' uncomment for higher quality output
            'graph.InterpolationMode = InterpolationMode.High;
            'graph.CompositingQuality = CompositingQuality.HighQuality;
            'graph.SmoothingMode = SmoothingMode.AntiAlias;
            For Each oStaff As DTOStaff In items
                Dim X As Integer = iCol * iItemWidth
                Dim Y As Integer = iRow * iThumbnailHeight
                Dim oRectangle As New RectangleF(X, Y, iItemWidth, iThumbnailHeight)
                GraphicsObject.FillRectangle(Brushes.White, oRectangle)
                If oStaff.avatar Is Nothing Then
                    GraphicsObject.DrawImage(My.Resources.NoImg140, oRectangle)
                Else
                    GraphicsObject.DrawImage(oStaff.avatar, oRectangle)
                End If

                iCol += 1
                If iCol >= ColumnsCount Then
                    iRow += 1
                    iCol = 0
                End If
            Next
        End Using

        Dim retval As New DTOSprite(sHash)
        retval.Image = oBitmap

        SpriteLoader.Update(retval, exs)
        Return retval
    End Function

    Shared Function Factory(items As List(Of DTOIncentiu), iItemWidth As Integer, sHash As String, exs As List(Of Exception)) As DTOSprite
        Dim oDefaultImage As Image = Nothing
        Dim ColumnsCount As Integer = DTOSprite.MAXCOLUMNS
        Dim iRows As Integer = Fix((items.Count - 1) / ColumnsCount) + 1
        Dim iCols As Integer = IIf(items.Count > ColumnsCount, ColumnsCount, items.Count)
        Dim iThumbnailHeight As Integer = 400 * iItemWidth / 350
        Dim oBitmap As New Bitmap(iCols * iItemWidth, iRows * iThumbnailHeight)

        Dim iCol As Integer = 0
        Dim iRow As Integer = 0
        Using GraphicsObject As Graphics = Graphics.FromImage(oBitmap)
            ' uncomment for higher quality output
            'graph.InterpolationMode = InterpolationMode.High;
            'graph.CompositingQuality = CompositingQuality.HighQuality;
            'graph.SmoothingMode = SmoothingMode.AntiAlias;
            For Each item As DTOIncentiu In items
                Dim X As Integer = iCol * iItemWidth
                Dim Y As Integer = iRow * iThumbnailHeight
                Dim oRectangle As New RectangleF(X, Y, iItemWidth, iThumbnailHeight)
                GraphicsObject.FillRectangle(Brushes.White, oRectangle)
                If item.Thumbnail Is Nothing Then
                    If oDefaultImage Is Nothing Then
                        oDefaultImage = DefaultImageLoader.Image(DTO.Defaults.ImgTypes.Incentiu)
                    End If
                    GraphicsObject.DrawImage(oDefaultImage, oRectangle)
                Else
                    GraphicsObject.DrawImage(item.Thumbnail, oRectangle)
                End If

                iCol += 1
                If iCol >= ColumnsCount Then
                    iRow += 1
                    iCol = 0
                End If
            Next
        End Using

        Dim retval As New DTOSprite(sHash)
        retval.Image = oBitmap

        SpriteLoader.Update(retval, exs)
        Return retval
    End Function

    Shared Function Factory(oIncidencia As DTOIncidencia, iItemWidth As Integer, exs As List(Of Exception)) As DTOSprite
        Dim ColumnsCount As Integer = DTOSprite.MAXCOLUMNS

        IncidenciaLoader.Load(oIncidencia)
        Dim itemsCount As Integer = oIncidencia.DocFileImages.Count + oIncidencia.PurchaseTickets.Count
        Dim iRows As Integer = Fix((itemsCount - 1) / ColumnsCount) + 1
        Dim iCols As Integer = IIf(itemsCount > ColumnsCount, ColumnsCount, itemsCount)
        Dim iThumbnailHeight As Integer = iItemWidth 'es quadrat
        Dim oBitmap As New Bitmap(iCols * iItemWidth, iRows * iThumbnailHeight)

        Dim items As New List(Of Image)
        For Each oDocFile As DTODocFile In oIncidencia.Attachments()
            items.Add(oDocFile.Thumbnail)
        Next

        Dim iCol As Integer = 0
        Dim iRow As Integer = 0
        Using GraphicsObject As Graphics = Graphics.FromImage(oBitmap)
            ' uncomment for higher quality output
            'graph.InterpolationMode = InterpolationMode.High;
            'graph.CompositingQuality = CompositingQuality.HighQuality;
            'graph.SmoothingMode = SmoothingMode.AntiAlias;
            For Each item As Image In items
                Dim X As Integer = iCol * iItemWidth
                Dim Y As Integer = iRow * iThumbnailHeight
                Dim oRectangle As New RectangleF(X, Y, iItemWidth, iThumbnailHeight)
                GraphicsObject.FillRectangle(Brushes.White, oRectangle)
                If item Is Nothing Then
                    GraphicsObject.DrawImage(My.Resources.NoImg140, oRectangle)
                Else
                    GraphicsObject.DrawImage(item, oRectangle)
                End If

                iCol += 1
                If iCol >= ColumnsCount Then
                    iRow += 1
                    iCol = 0
                End If
            Next
        End Using

        Dim retval As New DTOSprite()
        retval.Image = oBitmap

        SpriteLoader.Update(retval, exs)
        Return retval
    End Function


End Class
