Public Class Ean13Helper
    Private Shared Areas(7, 2) As Integer
    Private Shared AreaLength As Integer() = {8, 3, 42, 5, 42, 3, 8}
    Private Shared mLineWidth As Integer = 1
    Private Shared mFont As New Font("arial", 8, FontStyle.Regular)

    Private Enum Area
        LeftPadding
        LeftGuard
        LeftData
        Center
        RightData
        RightGuard
        RightPadding
    End Enum

    Private Enum Dims
        init
        [end]
    End Enum

    Shared Function Bitmap(oEan As DTOEan, Optional ByVal BlDrawDigits As Boolean = True, Optional iHeight As Integer = 40, Optional oBgGolor As Color = Nothing) As Bitmap
        Dim oBmp As Bitmap = Nothing
        SetAreaDefinition()
        If DTOEan.isValid(oEan) Then
            Dim sPadding As String = "00000000"
            Dim sCleanDigits As String = DTOEan.CleanDigits(oEan.Value)
            Dim s1 As String = Encoded(sCleanDigits)
            Dim s2 As String = sPadding & s1 & sPadding
            oBmp = DrawData(s2, BlDrawDigits, sCleanDigits, iHeight, oBgGolor)
        End If
        Return oBmp
    End Function

    Private Shared Sub SetAreaDefinition()
        Dim LastPos As Integer = -1
        For i As Integer = 0 To 6
            Areas(i, Dims.init) = LastPos + 1
            Areas(i, Dims.end) = LastPos + 1 + AreaLength(i)
            LastPos += AreaLength(i)
        Next
    End Sub

    Private Shared Function Encoded(ByVal strDataToEncode As String) As String

        Dim i As Long

        Dim lngOddSum As Long
        Dim lngEvenSum As Long
        Dim lngCheckDigit As Long

        Dim lngLenEncode As Long
        Dim lngNumberSystem As Long
        Dim lngParity As Long
        Dim strParitySequence As String = ""

        Dim aryEANCharSet(3) As Object
        Dim aryMaskedSet As Object

        Dim aryLeftEvenSet(10) As String
        Dim aryLeftOddSet(10) As String
        Dim aryRightSet(10) As String

        Const cstrGuard As String = "101"
        Const cstrCenter As String = "01010"

        lngLenEncode = Len(strDataToEncode)

        'fill array elements with proper character sets

        aryLeftEvenSet(0) = "0001101"
        aryLeftEvenSet(1) = "0011001"
        aryLeftEvenSet(2) = "0010011"
        aryLeftEvenSet(3) = "0111101"
        aryLeftEvenSet(4) = "0100011"
        aryLeftEvenSet(5) = "0110001"
        aryLeftEvenSet(6) = "0101111"
        aryLeftEvenSet(7) = "0111011"
        aryLeftEvenSet(8) = "0110111"
        aryLeftEvenSet(9) = "0001011"

        aryLeftOddSet(0) = "0100111"
        aryLeftOddSet(1) = "0110011"
        aryLeftOddSet(2) = "0011011"
        aryLeftOddSet(3) = "0100001"
        aryLeftOddSet(4) = "0011101"
        aryLeftOddSet(5) = "0111001"
        aryLeftOddSet(6) = "0000101"
        aryLeftOddSet(7) = "0010001"
        aryLeftOddSet(8) = "0001001"
        aryLeftOddSet(9) = "0010111"

        aryRightSet(0) = "1110010"
        aryRightSet(1) = "1100110"
        aryRightSet(2) = "1101100"
        aryRightSet(3) = "1000010"
        aryRightSet(4) = "1011100"
        aryRightSet(5) = "1001110"
        aryRightSet(6) = "1010000"
        aryRightSet(7) = "1000100"
        aryRightSet(8) = "1001000"
        aryRightSet(9) = "1110100"

        'populate the entire character set

        aryEANCharSet(0) = aryLeftEvenSet
        aryEANCharSet(1) = aryLeftOddSet
        aryEANCharSet(2) = aryRightSet

        If lngLenEncode = 12 Then
            'get check digit

            For i = 1 To 12
                If i Mod 2 Then
                    lngOddSum = lngOddSum + CLng(Mid$(strDataToEncode, i, 1))
                Else
                    lngEvenSum = lngEvenSum + CLng(Mid$(strDataToEncode, i, 1))
                End If
            Next

            'check digit is some number + (lngEvenSum + lngOddSum * 3) = value evenly divisible by 10

            lngCheckDigit = 10 - ((lngEvenSum * 9 + lngOddSum * 3) Mod 10)
            If lngCheckDigit = 10 Then lngCheckDigit = 0

            'add the check digit to the end of the barcode

            strDataToEncode = strDataToEncode & lngCheckDigit

        End If

        'get the number system digit

        lngNumberSystem = CLng(Left$(strDataToEncode, 1))

        'remove the leading number system digit

        strDataToEncode = Right$(strDataToEncode, 12)

        'set the parity sequence for use in encoding digits based on the number system.
        'this parity sequence mask will return the number pattern from the aryEANCharSet
        'array which holds the left even, left odd, and right bar code data for numbers
        '0 through 9.

        Select Case lngNumberSystem
            Case 0 : strParitySequence = "000000222222"
            Case 1 : strParitySequence = "001011222222"
            Case 2 : strParitySequence = "001101222222"
            Case 3 : strParitySequence = "001110222222"
            Case 4 : strParitySequence = "010011222222"
            Case 5 : strParitySequence = "011001222222"
            Case 6 : strParitySequence = "011100222222"
            Case 7 : strParitySequence = "010101222222"
            Case 8 : strParitySequence = "010110222222"
            Case 9 : strParitySequence = "011010222222"
        End Select

        'encode the number's based on the number system parity mask above

        Dim sRetVal As String = ""
        For i = 1 To 12
            lngParity = CLng(Mid$(strParitySequence, i, 1))
            aryMaskedSet = aryEANCharSet(lngParity)
            sRetVal = sRetVal & aryMaskedSet(CLng(Mid$(strDataToEncode, i, 1)))
            If i = 6 Then sRetVal = sRetVal & cstrCenter 'add center guard
        Next

        'put the guards on either side

        sRetVal = cstrGuard & sRetVal & cstrGuard
        Return sRetVal
    End Function

    Private Shared Function DrawData(ByVal sDigits As String, BlDrawDigits As Boolean, sCleanDigits As String, Optional iHeight As Integer = 0, Optional oBgGolor As Color = Nothing) As Bitmap
        Dim IntChars As Integer = Len(sDigits)
        Dim oBgColor As System.Drawing.Color = Nothing

        Dim iWidth As Integer = IntChars * mLineWidth
        Dim xPos As Integer
        Dim iLen As Integer

        Dim oBitmap As New Bitmap(iWidth, iHeight)
        Dim oGraphics As Graphics = Graphics.FromImage(oBitmap)


        If oBgColor <> Nothing Then
            oGraphics.FillRectangle(Brushes.White, 0, 0, iWidth, iHeight)
        End If

        Dim oPen As New Pen(Color.Black, mLineWidth)
        Dim i As Integer
        For i = 0 To IntChars - 1
            If sDigits.Substring(i, 1) = "1" Then
                xPos = mLineWidth * i
                iLen = GetBarLen(i, iHeight, BlDrawDigits)
                oGraphics.DrawLine(oPen, xPos, 0, xPos, iLen)
            End If
        Next

        If BlDrawDigits Then
            DrawCode(oGraphics, sCleanDigits, Area.LeftPadding, iHeight)
            DrawCode(oGraphics, sCleanDigits, Area.LeftData, iHeight)
            DrawCode(oGraphics, sCleanDigits, Area.RightData, iHeight)
        End If

        Return oBitmap
    End Function

    Private Shared Sub DrawCode(ByVal ographics As Graphics, sDigits As String, ByVal oArea As Area, iHeight As Integer)
        Dim s As String = ""
        Dim X As Integer = 0
        Dim Y As Integer = iHeight - mFont.Height
        Dim iTextWidth As Integer
        Select Case oArea
            Case Area.LeftPadding
                s = sDigits.Substring(0, 1)
                iTextWidth = ographics.MeasureString(s, mFont).Width
                X = Areas(Area.LeftPadding, Dims.end) - iTextWidth
                Y = Y - mFont.Height
            Case Area.LeftData
                s = sDigits.Substring(1, 6)
                iTextWidth = ographics.MeasureString(s, mFont).Width
                X = Areas(Area.LeftData, Dims.init) + (Areas(Area.LeftData, Dims.end) - Areas(Area.LeftData, Dims.init) - iTextWidth) / 2
            Case Area.RightData
                s = sDigits.Substring(7)
                iTextWidth = ographics.MeasureString(s, mFont).Width
                X = Areas(Area.RightData, Dims.init) + (Areas(Area.RightData, Dims.end) - Areas(Area.RightData, Dims.init) - iTextWidth) / 2
        End Select
        ographics.DrawString(s, mFont, Brushes.Black, X, Y)
    End Sub

    Private Shared Function GetBarLen(ByVal i As Integer, iHeight As Integer, BlDrawDigits As Boolean) As Integer
        Dim iLen As Integer
        If BlDrawDigits Then
            Dim iLongBar As Integer = iHeight - mFont.Height / 2
            Dim iShortBar As Integer = iHeight - mFont.Height
            Select Case i
                Case Areas(Area.LeftGuard, Dims.init) To Areas(Area.LeftGuard, Dims.end),
                     Areas(Area.Center, Dims.init) To Areas(Area.Center, Dims.end),
                     Areas(Area.RightGuard, Dims.init) To Areas(Area.RightGuard, Dims.end)
                    iLen = iLongBar
                Case Areas(Area.LeftData, Dims.init) To Areas(Area.LeftData, Dims.end),
                     Areas(Area.RightData, Dims.init) To Areas(Area.RightData, Dims.end)
                    iLen = iShortBar
                Case Else
                    iLen = 0
            End Select
        Else
            iLen = iHeight
        End If
        Return iLen
    End Function

End Class
