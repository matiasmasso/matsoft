Public Class DTOAsciiRow
    Private mEx As Exception
    Private mFields As ArrayList

    Public Enum FchFormats
        DDMMYY
        YYYYMMDD
        DDMMYYYY
    End Enum

    Public Sub New()
        MyBase.New()
        mFields = New ArrayList
    End Sub

    Public Property Fields() As ArrayList
        Get
            Return mFields
        End Get
        Set(ByVal value As ArrayList)
            mFields = value
        End Set
    End Property

    Private Sub AddFld(ByVal sTxt As String, ByVal iLen As Integer)
        Select Case sTxt.Length
            Case Is < iLen
                sTxt = sTxt.PadRight(iLen)
            Case Is > iLen
                mEx = New Exception("text fora de rango", New System.Exception)
        End Select
        mFields.Add(sTxt)
    End Sub

    Public Sub AddTxt(ByVal sSource As String, ByVal iLen As Integer)
        Dim sTxt As String = Depura(sSource)

        Select Case sTxt.Length
            Case Is < iLen
                sTxt = sTxt.PadRight(iLen)
            Case Is > iLen
                sTxt = TextHelper.VbLeft(sTxt, iLen)
        End Select
        AddFld(sTxt, iLen)
    End Sub

    Public Sub AddPlaceHolder(ByVal iLen As Integer)
        AddFld("", iLen)
    End Sub

    Public Sub AddInt(ByVal iInteger As Integer, ByVal iLen As Integer)
        Dim sTxt As String = CStr(iInteger)
        sTxt = sTxt.PadLeft(iLen, "0")
        AddFld(sTxt, iLen)
    End Sub

    Public Sub AddDec(ByVal DcNum As Decimal, ByVal iEnteros As Integer, Optional ByVal iDecimals As Integer = 0)
        Dim sTxt As String
        Dim sWholeFormat = New String("0", iEnteros)
        Dim sFractionFormat = New String("0", iDecimals)
        Dim wholePart = Math.Truncate(DcNum)
        If iDecimals = 0 Then
            sTxt = TextHelper.VbFormat(wholePart, sWholeFormat)
        Else
            Dim fractionPart = CInt((DcNum - wholePart) * 10 ^ iDecimals)
            sTxt = TextHelper.VbFormat(wholePart, sWholeFormat) & TextHelper.VbFormat(fractionPart, sFractionFormat)
        End If
        Dim iLen As Integer = iEnteros + iDecimals
        AddFld(sTxt, iLen)
    End Sub

    Public Sub AddFch(ByVal DtFch As Date, Optional ByVal oFormat As FchFormats = FchFormats.DDMMYY)
        Dim sTxt As String = ""
        Dim iLen As Integer = 0
        Select Case oFormat
            Case FchFormats.DDMMYY
                sTxt = TextHelper.VbFormat(DtFch, "ddMMyy")
                iLen = 6
            Case FchFormats.YYYYMMDD
                sTxt = TextHelper.VbFormat(DtFch, "yyyyMMdd")
                iLen = 8
            Case FchFormats.DDMMYYYY
                sTxt = TextHelper.VbFormat(DtFch, "ddMMyyyy")
                iLen = 8
        End Select
        AddFld(sTxt, iLen)
    End Sub

    Public Function FullText() As String
        Dim sRetVal As String = ""
        Dim s As String
        For Each s In mFields
            sRetVal = sRetVal & s
        Next
        Return sRetVal
    End Function

    Protected Function Depura(ByVal source As String) As String
        If source Is Nothing Then Return ""
        source = source.Replace("ñ", "n")
        source = source.Replace("Ñ", "N")
        source = source.Replace("Á", "A")
        source = source.Replace("É", "E")
        source = source.Replace("Í", "I")
        source = source.Replace("Ó", "O")
        source = source.Replace("Ú", "U")
        source = source.Replace("á", "a")
        source = source.Replace("é", "e")
        source = source.Replace("í", "i")
        source = source.Replace("ó", "o")
        source = source.Replace("ú", "u")
        source = source.Replace("ç", "c")
        source = source.Replace("Ç", "C")
        source = source.Replace("ä", "a")
        source = source.Replace("ë", "e")
        source = source.Replace("ï", "i")
        source = source.Replace("ö", "o")
        source = source.Replace("ü", "u")
        source = source.Replace("ª", "a")
        source = source.Replace("º", "o")
        source = source.Replace("à", "a")
        source = source.Replace("è", "e")
        source = source.Replace("ì", "i")
        source = source.Replace("ò", "o")
        source = source.Replace("ù", "u")
        source = source.Replace("`", "'")
        source = source.Replace("´", "'")
        source = source.Replace("&", "-")

        Dim oAscii As New System.Text.ASCIIEncoding()
        Dim EncodedBytes As Byte() = oAscii.GetBytes(source)
        Dim sTxt As String = oAscii.GetString(EncodedBytes)

        Return sTxt
    End Function

End Class

