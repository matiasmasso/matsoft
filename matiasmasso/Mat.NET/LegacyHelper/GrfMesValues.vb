Public Class GrfMesValues

    Private Shared _Colors As List(Of Color) = ColorPaletteHelper.Colors(ColorPaletteHelper.Palettes.colorsBrightPastel)

    Private Shared mWidth As Integer
    Private Shared mHeight As Integer

    Private Shared mMarginTop As Integer = 10
    Private Shared mMarginLeft As Integer = 10
    Private Shared mMarginBottom As Integer = 35
    Private Shared mMarginRight As Integer = 10

    Private Shared mTitsBottom As Integer
    Private Shared mTitsHeight As Integer = 16

    Private Shared mBottomHeaderGap As Integer = 16

    Private Shared mGrTop As Integer = mMarginTop + mBottomHeaderGap
    Private Shared mGrLeft As Integer = mMarginLeft
    Private Shared mGrRight As Integer
    Private Shared mGrBottom As Integer
    Private Shared mGrWidth As Integer
    Private Shared mGrHeight As Integer

    Private Shared mLgTop As Integer = mMarginTop
    Private Shared mLgLeft As Integer
    Private Shared mLgRight As Integer = mWidth - mMarginRight
    Private Shared mLgLeftMargin As Integer = 10

    Private Shared mColWidth As Integer
    Private Shared mFactorHeight As Decimal

    Private Shared mGraphics As Graphics
    Private Shared mPenBlack As New Pen(Color.Black, 1)
    Private Shared mFontLg As New Font("Microsoft Sans Serif", CSng(8.25), FontStyle.Regular)
    Private Shared mFontSum As New Font("Microsoft Sans Serif", CSng(7), FontStyle.Regular)
    Private Shared mLgColorSquareWidth As Integer = mFontLg.Height - 1

    Private Shared mBgColor As Color
    Private Shared mMaxProductLabels As Integer
    Private Shared mLang As DTOLang



    Shared Function Image(oUser As DTOUser, oSellout As DTOSellOut, iWidth As Integer, iHeight As Integer) As Byte()
        Dim retval As Byte() = Nothing
        Dim oImage As System.Drawing.Image = Nothing
        Dim items As List(Of DTOSelloutItem) = oSellout.Items
        Select Case oUser.Rol.id
            Case DTORol.Ids.manufacturer
                'display categories en lloc de marques comercials
                items = oSellout.Items.SelectMany(Function(x) x.Items).ToList
        End Select

        If items.Count = 0 Then
        Else
            Dim oSortedItems As List(Of DTOSelloutItem) = items.OrderByDescending(Function(x) x.Tot()).ToList

            mWidth = iWidth
            mHeight = iHeight
            mTitsBottom = mHeight - mMarginBottom
            mGrBottom = mTitsBottom - mTitsHeight + 1 '(EL 1 ES PER EVITAR TREPITXAR LA LINIA DE DALT)
            mGrHeight = mGrBottom - mGrTop - 1
            mMaxProductLabels = (mGrBottom - mGrTop) / mFontLg.Height - 1
            mLang = oUser.Lang
            mBgColor = Color.White

            oImage = New Bitmap(mWidth, mHeight)
            mGraphics = Graphics.FromImage(oImage)

            DrawTemplate(oSortedItems, oSellout.YearMonths)

            Dim iMaxValue As Decimal
            For i = 0 To oSellout.YearMonths.Count - 1
                Dim iMes As Integer = i
                Dim TotalMes As Decimal = oSortedItems.Sum(Function(x) x.Values(iMes))
                If TotalMes > iMaxValue Then iMaxValue = TotalMes
            Next

            If iMaxValue > 0 Then

                mFactorHeight = mGrHeight / iMaxValue

                For mesIdx As Integer = 0 To oSellout.YearMonths.Count - 1
                    FillColumn(oSortedItems, mesIdx)
                    DrawColumnSum(oSortedItems, mesIdx)
                Next

                DrawSum(oSortedItems)
            End If
        End If
        Return oImage.Bytes()

    End Function


    Private Shared Sub FillColumn(oCategories As List(Of DTOSelloutItem), mesIdx As Integer)
        Dim yPos As Integer = mGrBottom
        For i As Integer = 0 To oCategories.Count - 1
            Try
                Dim oColor As Color
                If i >= mMaxProductLabels Then
                    oColor = Color.Gray
                ElseIf i >= _Colors.Count Then
                    Dim idx As Integer = i Mod _Colors.Count - 1
                    oColor = _Colors(idx)
                Else
                    oColor = _Colors(i)
                End If

                Dim DcEur As Decimal = oCategories(i).Values(mesIdx)
                If DcEur > 0 Then
                    FillColumnSection(DcEur, mesIdx, oColor, yPos)
                End If
            Catch ex As Exception
                'Stop

            End Try
        Next
    End Sub

    Private Shared Sub FillColumnSection(DcEur As Decimal, mesIdx As Integer, oColor As Color, ByRef yPos As Integer)
        Dim oPen As New Pen(oColor, 1)
        Dim oBrush As Brush = oPen.Brush
        Dim xPos As Integer = mGrLeft + (mesIdx * mColWidth)
        Dim IntHeight As Integer = mFactorHeight * DcEur
        Dim oRectangle As New Rectangle(xPos + 1, yPos - IntHeight, mColWidth - 2, IntHeight)
        mGraphics.DrawRectangle(oPen, oRectangle)
        mGraphics.FillRectangle(oBrush, oRectangle)
        yPos = yPos - IntHeight
    End Sub


    Private Shared Sub DrawColumnSum(oCategories As List(Of DTOSelloutItem), mesIdx As Integer)
        Dim DcValue As Decimal = oCategories.Sum(Function(x) x.Values(mesIdx))
        Dim sValue As String = EurFormatted(DcValue)
        Dim valueWidth As Integer = mGraphics.MeasureString(sValue, mFontSum).Width
        Dim yPos As Integer = mGrTop - mFontSum.Height - 2
        Dim xPos As Integer = mGrLeft + (mesIdx * mColWidth) + (mColWidth - valueWidth) / 2
        mGraphics.DrawString(sValue, mFontSum, Brushes.Black, xPos, yPos)
    End Sub

    Private Shared Sub DrawSum(oCategories As List(Of DTOSelloutItem))
        Dim yPos As Integer = mGrTop - mFontSum.Height - 2
        Dim xPos As Integer = mLgLeft
        Dim value As Decimal = oCategories.Sum(Function(x) x.Values.Sum)
        Dim sValue As String = "total: " & EurFormatted(value)
        mGraphics.DrawString(sValue, mFontSum, Brushes.Black, xPos, yPos)
    End Sub

    Private Shared Function ContextInfo(ByVal X As Integer, ByVal Y As Integer) As String
        Dim iMes As Integer = (X - mGrLeft) / mColWidth
        If iMes > 0 And iMes <= 12 Then
            Return mLang.Mes(iMes)
        Else
            Return "mes " & iMes & " fora de interval"
        End If
    End Function

    Private Shared Function EurFormatted(DcEur As Decimal) As String
        Dim retval As String = ""
        Dim wholePart As Decimal
        Dim FractionalPart As Decimal
        If DcEur > 1000000 Then
            wholePart = Math.Truncate(DcEur / 1000000)
            FractionalPart = Math.Truncate((DcEur - wholePart * 1000))
            retval = Left(Format(wholePart, "#,###") & "M" & FractionalPart, 4)
        ElseIf DcEur > 1000 Then
            wholePart = Math.Truncate(DcEur / 1000)
            'FractionalPart = (DcEur - wholePart) * 1000
            FractionalPart = (DcEur - wholePart * 1000)
            retval = Left(Format(wholePart, "#,###") & "K" & FractionalPart, 4)
        Else
            retval = Format(DcEur, "#,###")
        End If
        Return retval
    End Function


    Private Shared Sub DrawTemplate(oCategories As List(Of DTOSelloutItem), oYearMonths As List(Of DTOYearMonth))
        Dim iMaxLgWidth As Integer = oCategories.Max(Function(x) mGraphics.MeasureString(x.Concept, mFontLg).Width)

        mLgRight = mWidth - mMarginRight
        mLgLeft = mLgRight - iMaxLgWidth - mLgLeftMargin - mLgColorSquareWidth

        Dim oRectangle As New Rectangle(0, 0, mWidth, mHeight)
        mGraphics.DrawRectangle(mPenBlack, oRectangle)
        mGraphics.FillRectangle(Brushes.White, oRectangle)

        mGrRight = mLgLeft - mLgLeftMargin
        mColWidth = (mGrRight - mGrLeft) / 12
        mGrWidth = 12 * mColWidth
        mGrRight = mGrLeft + mGrWidth


        mGraphics.DrawLine(mPenBlack, mGrLeft, mGrBottom + 1, mGrRight, mGrBottom + 1)
        mGraphics.DrawLine(mPenBlack, mGrLeft, mGrTop - 1, mGrRight, mGrTop - 1)
        mGraphics.DrawRectangle(mPenBlack, mGrLeft, mGrTop - mBottomHeaderGap, mGrWidth, mGrHeight + 2 * mBottomHeaderGap)

        DrawMesos(oYearMonths)
        DrawSideLabels(oCategories)
    End Sub

    Private Shared Sub DrawSideLabels(oCategories As List(Of DTOSelloutItem))
        Dim yPos As Integer = mGrBottom
        Dim idx As Integer
        For Each oCategory In oCategories
            yPos = yPos - mFontLg.Height
            DrawLgColorSquare(_Colors(idx), yPos)
            DrawLgText(oCategory.Concept, yPos)
            idx += 1
            If idx >= mMaxProductLabels Then Exit For
        Next
    End Sub

    Private Shared Sub DrawMesos(oYearMonths As List(Of DTOYearMonth))

        Dim yPos As Integer = mGrBottom
        For idx As Integer = 0 To oYearMonths.Count - 1
            Dim iMes As Integer = oYearMonths(idx).MonthNum
            Dim s As String = mLang.MesAbr(iMes)

            Dim xPos As Integer = mGrLeft + (idx * mColWidth)
            mGraphics.DrawString(s, mFontLg, Brushes.Black, xPos + ColPos(s), mGrBottom + 2)
            If idx > 0 Then mGraphics.DrawLine(mPenBlack, xPos, mGrTop - mBottomHeaderGap, xPos, mTitsBottom)
        Next
    End Sub

    Private Shared Sub DrawLgColorSquare(oColor As Color, ByVal yPos As Integer)
        Dim oPen As New Pen(oColor)
        Dim oBrush As Brush = oPen.Brush
        Dim X As Integer = mLgLeft
        Dim Y As Integer = yPos
        Dim iWidth As Integer = mLgColorSquareWidth - 1
        Dim iHeight As Integer = mLgColorSquareWidth - 1
        Dim oRectangle As New Rectangle(X, Y, iWidth, iHeight)
        mGraphics.FillRectangle(oBrush, oRectangle)
        mGraphics.DrawRectangle(oPen, oRectangle)
    End Sub

    Private Shared Sub DrawLgText(ByVal s As String, ByVal yPos As Integer)
        Dim X As Integer = mLgLeft + mLgColorSquareWidth + mLgLeftMargin
        Dim Y As Integer = yPos
        mGraphics.DrawString(s, mFontLg, Brushes.Black, X, Y)
    End Sub

    Private Shared Function TotalMes(values As List(Of DTOGrfMesValue), mes As Integer) As Decimal
        'If mes = 11 Then Stop
        Dim retval As Decimal
        Try
            retval = values.Sum(Function(x) x.Mesos(mes).Eur)
        Catch ex As Exception

        End Try
        Return retval
    End Function


    Private Shared Function ColPos(ByVal s As String) As Integer
        Dim w As Integer = mGraphics.MeasureString(s, mFontLg).Width
        Return (mColWidth - w) / 2
    End Function

    Private Shared Function ColRightAlign(ByVal s As String) As Integer
        Dim w As Integer = mGraphics.MeasureString(s, mFontSum).Width
        Return (mColWidth - w) / 2
    End Function

End Class

