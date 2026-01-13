Public Class DTOEdiversaInvoicD96a
    Inherits DTOBaseGuid

    Property inv As String
    Property dtm As Date
    Property rffDq As String
    Property rffOn As String
    Property rffVn As String
    Property nadSu As String
    Property nadBy As String
    Property nadIv As String
    Property nadDp As String
    Property cur As String
    Property moaresNet As Decimal
    Property moaresBrut As Decimal
    Property moaresBase As Decimal
    Property moaresLiq As Decimal
    Property moaresTax As Decimal
    Property MoaresDto As Decimal
    Property moaresCharges As Decimal
    Property items As List(Of Item)

    Public Class Item
        Inherits DTOBaseGuid

        Property lin As Integer
        Property ean As String
        Property piaLin As String
        Property imdLin As String
        Property qtyLin As Integer
        Property priLinNet As Decimal
        Property priLinBrut As Decimal
        Property moaLinNet As Decimal

        Public Sub New()
            MyBase.New
        End Sub

        Public Sub New(oGuid As Guid)
            MyBase.New(oGuid)
        End Sub
    End Class

    Public Sub New()
        MyBase.New
        _items = New List(Of Item)
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
        _items = New List(Of Item)
    End Sub

    Shared Function Factory(src As String) As DTOEdiversaInvoicD96a
        Dim segments = src.Split(vbCrLf).ToList
        Dim retval As New DTOEdiversaInvoicD96a
        For Each segment In segments
            Dim fields = segment.Split("|").ToList
            Dim item As New Item
            If fields.Count > 1 Then
                Select Case fields.First
                    Case "INV"
                        retval.inv = Field(segment, 1)
                    Case "DTM"
                        retval.dtm = Field(segment, 1)
                    Case "RFF"
                        Select Case Field(segment, 1)
                            Case "DQ"
                                retval.rffDq = Field(segment, 2)
                            Case "ON"
                                retval.rffOn = Field(segment, 2)
                            Case "VN"
                                retval.rffVn = Field(segment, 2)
                        End Select
                    Case "NADSU"
                        retval.nadSu = Field(segment, 2)
                    Case "NADBY"
                        retval.nadBy = Field(segment, 2)
                    Case "NADIV"
                        retval.nadIv = Field(segment, 2)
                    Case "NADDP"
                        retval.nadDp = Field(segment, 2)
                    Case "CUX"
                        retval.Cur = Field(segment, 2)
                    Case "ALC"
                    Case "MOARES"
                        retval.moaresNet = parseDecimal(segment, 1)
                        retval.moaresBrut = parseDecimal(segment, 2)
                        retval.moaresBase = parseDecimal(segment, 3)
                        retval.moaresLiq = parseDecimal(segment, 4, 10)
                        retval.moaresTax = parseDecimal(segment, 5)
                        retval.MoaresDto = parseDecimal(segment, 6, 13)
                        retval.moaresCharges = parseDecimal(segment, 9)

                    Case "LIN"
                        item = New Item
                        item.ean = Field(segment, 1)
                        item.lin = parseInt(segment, 3)
                    Case "PIALIN"
                        item.piaLin = Field(segment, 1)
                    Case "IMDLIN"
                        item.imdLin = Field(segment, 1)
                    Case "QTYLIN"
                        item.qtyLin = parseInt(segment, 2)
                    Case "MOALIN"
                        item.moaLinNet = parseDecimal(segment, 3)
                    Case "PRILIN"
                        Select Case Field(segment, 2)
                            Case "AAA"
                                item.priLinNet = parseDecimal(segment, 2)
                            Case "AAB"
                                item.priLinBrut = parseDecimal(segment, 2)
                        End Select
                End Select
            End If
        Next
        Return retval
    End Function

    Shared Function parseInt(segment As String, fieldIdx As Integer, Optional alternativeIdx As Integer = 0) As Integer
        Dim retval As Integer = 0
        Dim value As String = Field(segment, fieldIdx, alternativeIdx)
        If TextHelper.VbIsNumeric(value) Then
            retval = value
        End If
        Return retval
    End Function

    Shared Function parseDecimal(segment As String, fieldIdx As Integer, Optional alternativeIdx As Integer = 0) As Decimal
        Dim retval As Integer = 0
        Dim value As String = Field(segment, fieldIdx, alternativeIdx)
        Try
            retval = Decimal.Parse(value, System.Globalization.CultureInfo.InvariantCulture)
        Catch ex As Exception
        End Try
        Return retval
    End Function

    Shared Function ParseFch(segment As String, fieldIdx As Integer, Optional alternativeIdx As Integer = 0) As Date
        Dim retval As Date = Nothing
        Dim value As String = Field(segment, fieldIdx, alternativeIdx)
        Dim sFormat As String = ""
        Dim provider As Globalization.CultureInfo = Globalization.CultureInfo.InvariantCulture
        Try
            Select Case value.Length
                Case 0
                Case 6
                    retval = Date.ParseExact(value, "yyMMdd", provider)
                Case 8
                    retval = Date.ParseExact(value, "yyyyMMdd", provider)
                Case 12
                    retval = Date.ParseExact(value, "yyyyMMddHHmm", provider)
                Case Else
                    'exs.Add(DTOEdiversaException.Factory(DTOEdiversaException.Cods.BadFormatFch, src, "No es pot convertir " & src & " en una data"))
            End Select

        Catch ex As Exception
            'exs.Add(DTOEdiversaException.Factory(DTOEdiversaException.Cods.BadFormatFch, src))
        End Try

        Return retval
    End Function

    Shared Function Field(segment As String, fieldIdx As Integer, Optional alternativeIdx As Integer = 0) As String
        Dim retval As String = ""
        Dim fields = segment.Split("|").ToList
        If fields.Count > fieldIdx Then
            retval = fields(fieldIdx)
        End If
        If retval = "" And alternativeIdx > 0 Then retval = Field(segment, alternativeIdx)
        Return retval
    End Function
End Class
