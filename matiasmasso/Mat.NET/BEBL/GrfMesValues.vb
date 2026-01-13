Public Class GrfMesValues


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


    Private Shared mMaxProductLabels As Integer
    Private Shared mLang As DTOLang

    Shared Function Url(oUser As DTOUser, Optional AbsoluteUrl As Boolean = False) As String
        Dim retval As String = ""
        If oUser IsNot Nothing Then
            retval = UrlHelper.Image(DTO.Defaults.ImgTypes.SalesGrafic, oUser.Guid, AbsoluteUrl)
        End If
        Return retval
    End Function


    Shared Function Image(oUser As DTOUser, ByVal iWidth As Integer, ByVal iHeight As Integer) As Byte()
        Dim retval As Byte() = Nothing

        UserLoader.Load(oUser)
        EmpLoader.Load(oUser.Emp) 'perque requereix Org

        Dim oSellout As DTOSellOut = SellOut.Factory(oUser, DTOYearMonth.Current, DTOSellOut.ConceptTypes.product, DTOSellOut.Formats.amounts)
        SellOut.Load(oSellout)

        Dim items As List(Of DTOSelloutItem) = oSellout.Items
        Select Case oUser.Rol.id
            Case DTORol.Ids.manufacturer
                'display categories en lloc de marques comercials
                items = oSellout.Items.SelectMany(Function(x) x.Items).ToList
        End Select

        If items.Count > 0 Then
            retval = LegacyHelper.GrfMesValues.Image(oUser, oSellout, iWidth, iHeight)
        End If
        Return retval
    End Function

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


    Private Shared Function TotalMes(values As List(Of DTOGrfMesValue), mes As Integer) As Decimal
        'If mes = 11 Then Stop
        Dim retval As Decimal
        Try
            retval = values.Sum(Function(x) x.Mesos(mes).Eur)
        Catch ex As Exception

        End Try
        Return retval
    End Function

End Class

