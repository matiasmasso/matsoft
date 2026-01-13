Public Class EdiHelper


    Shared Function IsEdiFile(src As String) As Boolean
        Dim pattern = "ORDERS:D:96A|ORDRSP:D:96A|DESADV:D:96A|INVOIC:D:96A"
        Dim retval As Boolean = TextHelper.RegexMatch(src, pattern)
        Return retval
    End Function

    Shared Function EdiFileType(src As String) As EdiFile.FileTypes
        Dim retval = EdiFile.FileTypes.None
        If TextHelper.RegexMatch(src, "ORDERS:D:96A") Then
            retval = EdiFile.FileTypes.Orders
        End If
        Return retval
    End Function

    Public Class EdiFile
        Private _src As String
        Property FileType As FileTypes

        Public Enum FileTypes
            None
            Orders
            OrdrSp
            Desadv
            Invoic
            Genral
        End Enum

        Public Sub New(src As String, fileType As FileTypes)
            _src = src
            _FileType = fileType
        End Sub

        Protected Function Segments() As List(Of Segment)
            Dim lines = TextHelper.GetArrayListFromSplitCharSeparatedString(_src, "'").ToList()
            Dim retval As New List(Of Segment)
            For Each line In lines
                If Not String.IsNullOrEmpty(line) Then
                    Dim lineNumber = retval.Count + 1
                    Dim oSegment As New Segment(line, lineNumber)
                    retval.Add(oSegment)
                End If
            Next
            Return retval
        End Function

        Public Class Segment
            Property Fields As List(Of String)
            Property LineNumber As Integer

            Public Sub New(src As String, lineNumber As Integer)
                _LineNumber = lineNumber
                _Fields = src.Split(New Char() {"+"c, ":"c}).ToList()
            End Sub

            Public Function Tag() As String
                Dim retval = Fields.First()
                Return retval
            End Function

            Public Function FieldValue(exs As List(Of Exception), fieldIdx As Integer) As String
                Dim retval As String = ""
                If _Fields.Count > fieldIdx Then
                    retval = _Fields(fieldIdx)
                Else
                    Dim msg = String.Format("Missing field {0} in segment {1} tag {2}", fieldIdx, _LineNumber, Tag())
                    exs.Add(New Exception(msg))
                End If
                Return retval
            End Function

            Public Function FieldInteger(exs As List(Of Exception), fieldIdx As Integer) As Integer
                Dim retval As Integer = 0
                Dim src As String = FieldValue(exs, fieldIdx)
                If exs.Count = 0 Then
                    If TextHelper.VbIsNumeric(src) Then
                        retval = CInt(src)
                    Else
                        Dim msg = String.Format("Expected integer but found '{0}' on field {1} in segment {2} tag {3}", src, fieldIdx, _LineNumber, Tag())
                        exs.Add(New Exception(msg))
                    End If
                End If
                Return retval
            End Function

            Public Function FieldDecimal(exs As List(Of Exception), fieldIdx As Integer) As Decimal
                Dim retval As Decimal = 0
                Dim src As String = FieldValue(exs, fieldIdx)
                Try
                    retval = Decimal.Parse(src, System.Globalization.CultureInfo.InvariantCulture)
                Catch ex As Exception
                    Dim msg = String.Format("Expected decimal but found '{0}' on field {1} in segment {2} tag {3}", src, fieldIdx, _LineNumber, Tag())
                    exs.Add(New Exception(msg))
                End Try
                Return retval
            End Function

            Public Function FieldDate(exs As List(Of Exception), fieldIdx As Integer) As Date
                Dim retval As Date = Nothing
                Dim src As String = FieldValue(exs, fieldIdx)
                Try
                    Dim year As Integer = src.Substring(0, 4)
                    Dim month As Integer = src.Substring(4, 2)
                    Dim day As Integer = src.Substring(6, 2)
                    retval = New Date(year, month, day)
                Catch ex As Exception
                    Dim msg = String.Format("Bad date format '{0}' in segment {1} field {2}", src, _LineNumber, fieldIdx)
                    exs.Add(New Exception(msg))
                End Try
                Return retval
            End Function
        End Class

        Public Class Order
            Inherits EdiFile

            Property Buyer As String
            Property Supplier As String
            Property InvoiceTo As String
            Property DeliverTo As String
            Property DocNum As String
            Property FchDoc As Date

            Property FchDeliveryMin As Date
            Property FchDeliveryMax As Date
            Property CustomerSupplierNumber As String
            Property CustomerNif As String
            Property Currency As String

            Property Items As List(Of Item)

            Private Sub New(src As String)
                MyBase.New(src, FileTypes.Orders)
                _Items = New List(Of Item)
            End Sub

            Shared Function Factory(exs As List(Of Exception), src As String) As Order
                Dim retval As New Order(src)
                Dim oSegments = retval.Segments()
                Dim msg As String = ""
                For Each segment In oSegments
                    Select Case segment.Tag
                        Case "BGM"
                            retval.DocNum = segment.FieldValue(exs, 2)
                        Case "DTM"
                            Select Case segment.FieldValue(exs, 1)
                                Case "137"
                                    retval.FchDoc = segment.FieldDate(exs, 2).Date
                                Case "63"
                                    retval.FchDeliveryMax = segment.FieldDate(exs, 2).Date
                                Case "64"
                                    retval.FchDeliveryMin = segment.FieldDate(exs, 2).Date
                            End Select
                        Case "NAD"
                            Select Case segment.FieldValue(exs, 1)
                                Case "SU"
                                    retval.Supplier = segment.FieldValue(exs, 2)
                                Case "BY"
                                    retval.Buyer = segment.FieldValue(exs, 2)
                                Case "DP"
                                    retval.DeliverTo = segment.FieldValue(exs, 2)
                                Case "IV"
                                    retval.InvoiceTo = segment.FieldValue(exs, 2)
                            End Select
                        Case "RFF"
                            Select Case segment.FieldValue(exs, 1)
                                Case "CR"
                                    retval.CustomerSupplierNumber = segment.FieldValue(exs, 2)
                                Case "VA"
                                    retval.CustomerNif = segment.FieldValue(exs, 2)
                            End Select
                        Case "CUX"
                            retval.Currency = segment.FieldValue(exs, 2)
                        Case "LIN"
                            Dim item As New Item
                            With item
                                .LineNum = segment.FieldInteger(exs, 1)
                                .Sku = segment.FieldValue(exs, 3)
                            End With
                            retval.Items.Add(item)
                        Case "QTY"
                            If retval.Items.Count > 0 Then
                                Dim item = retval.Items.Last()
                                item.Qty = segment.FieldInteger(exs, 2)
                            Else
                                msg = String.Format("missing LIN segment for QTY segment #{0}", segment.LineNumber)
                                exs.Add(New Exception(msg))
                            End If
                        Case "PRI"
                            If retval.Items.Count > 0 Then
                                Dim item = retval.Items.Last()
                                Select Case segment.FieldValue(exs, 1)
                                    Case "AAB" 'Preu brut abans de descomptes
                                        item.GrossPrice = segment.FieldDecimal(exs, 2)
                                    Case "AAA" 'Preu brut despres de descomptes pero abans de impostos (Amazon)
                                        item.NetPrice = segment.FieldDecimal(exs, 2)
                                End Select
                            Else
                                msg = String.Format("missing LIN segment for PRI segment #{0}", segment.LineNumber)
                                exs.Add(New Exception(msg))
                            End If
                        Case "CNT"
                            Select Case segment.FieldValue(exs, 1)
                                Case "2"
                                    If retval.Items.Count <> segment.FieldInteger(exs, 2) Then
                                        msg = String.Format("items count {0} does not match segment CNT ({1})", retval.Items.Count, segment.FieldInteger(exs, 2))
                                        exs.Add(New Exception(msg))
                                    End If
                            End Select
                        Case "UNT"
                            Dim iSegmentsCount = oSegments.Where(Function(x) x.Tag <> "UNB" And x.Tag <> "UNZ").Count()
                            If iSegmentsCount <> segment.FieldInteger(exs, 1) Then
                                msg = String.Format("segments count {0} does not match segment UNT ({1})", oSegments.Count, segment.FieldInteger(exs, 1))
                                exs.Add(New Exception(msg))
                            End If
                    End Select
                Next
                Return retval
            End Function

            Public Class Item
                Property LineNum As Integer
                Property Sku As String
                Property Qty As Integer
                Property NetPrice As Decimal
                Property GrossPrice As Decimal
                Property Dto As Decimal
            End Class
        End Class

    End Class


End Class
