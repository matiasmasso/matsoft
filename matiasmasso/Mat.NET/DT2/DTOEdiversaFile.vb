Imports System.Text.RegularExpressions
Public Class DTOEdiversaFile
    Inherits DTOBaseGuid
    Property FileName As String
    Property Source As DTOBaseGuid
    Property Tag As String
    Property Fch As Date
    Property FchCreated As Date
    Property Docnum As String
    Property Amount As DTOAmt
    Property Sender As DTOEdiversaContact
    Property Receiver As DTOEdiversaContact
    Property Result As Results
    Property ResultBaseGuid As DTOBaseGuid
    Property Stream As String
    Property Exceptions As List(Of DTOEdiversaException)

    Property IOCod As IOcods
    Property Segments As List(Of DTOEdiversaSegment)

    Public Interlocutor As Interlocutors

    Public Const PrefixBritax = "4000984"
    Public Const PrefixElCorteIngles = "8422416"
    Public Const PrefixElCorteInglesPt = "56000000"
    Public Const PrefixSonae = "84365316"
    Public Const PrefixAmazon = "5450534"
    Public Const PrefixCarrefour = "8480015"
    Public Const PrefixAlcampo = "XX"


    Public Enum Interlocutors
        Unknown
        Britax
        ElCorteIngles
        Amazon
        Carrefour
        Alcampo
        Sonae
    End Enum

    Public Enum Results
        Pending
        Processed
        Deleted
    End Enum

    Public Enum IOcods
        NotSet
        Inbox
        Outbox
    End Enum

    Public Enum Tags
        DESADV_D_96A_UN_EAN008
        GENRAL_D_96A_UN_EAN003
        INVRPT_D_96A_UN_EAN008
        INVOIC_D_01B_UN_EAN010
        INVOIC_D_93A_UN_EAN007
        INVOIC_D_96A_UN_EAN008
        REMADV_D_96A_UN_EAN003
        SLSRPT_D_96A_UN_EAN004
        ORDERS_D_96A_UN_EAN008
        ORDRSP_D_96A_UN_EAN005
        APERAK_D_01B_UN_EAN003
    End Enum

    Public Sub New()
        MyBase.New()
        _Segments = New List(Of DTOEdiversaSegment)
        _Exceptions = New List(Of DTOEdiversaException)
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
        _Segments = New List(Of DTOEdiversaSegment)
        _Exceptions = New List(Of DTOEdiversaException)
    End Sub

    Shared Function Factory(filePath As String) As DTOEdiversaFile
        Dim oFile As New System.IO.FileInfo(filePath)
        Dim retval = New DTOEdiversaFile
        With retval
            .FileName = System.IO.Path.GetFileName(oFile.FullName)
            .FchCreated = oFile.CreationTime
            .Stream = System.IO.File.ReadAllText(oFile.FullName)
        End With
        Return retval
    End Function

    Shared Function ReadInterlocutor(Gln As DTOEan) As DTOEdiversaFile.Interlocutors
        Dim retval As DTOEdiversaFile.Interlocutors = DTOEdiversaFile.Interlocutors.Unknown
        If Gln IsNot Nothing Then
            Dim sEan As String = Gln.Value
            If sEan.Length = 13 Then
                If sEan.StartsWith(DTOEdiversaFile.PrefixBritax) Then
                    retval = DTOEdiversaFile.Interlocutors.Britax
                ElseIf sEan.StartsWith(DTOEdiversaFile.PrefixElCorteIngles) Then
                    retval = DTOEdiversaFile.Interlocutors.ElCorteIngles
                ElseIf sEan.StartsWith(DTOEdiversaFile.PrefixElCorteInglesPt) Then
                    retval = DTOEdiversaFile.Interlocutors.ElCorteIngles
                ElseIf sEan.StartsWith(DTOEdiversaFile.PrefixAmazon) Then
                    retval = DTOEdiversaFile.Interlocutors.Amazon
                ElseIf sEan.StartsWith(DTOEdiversaFile.PrefixCarrefour) Then
                    retval = DTOEdiversaFile.Interlocutors.Carrefour
                ElseIf sEan.StartsWith(DTOEdiversaFile.PrefixSonae) Then
                    retval = DTOEdiversaFile.Interlocutors.Sonae
                ElseIf sEan.StartsWith(DTOEdiversaFile.PrefixAlcampo) Then
                    retval = DTOEdiversaFile.Interlocutors.Alcampo
                End If
            End If
        End If

        Return retval
    End Function


    Public Sub LoadSegments()
        _Segments = New List(Of DTOEdiversaSegment)
        Dim sLines() As String = Regex.Split(_Stream, Environment.NewLine)
        If sLines.Length > 0 Then
            _Tag = sLines(0)
            For Each sLine As String In sLines
                Dim oSegment As DTOEdiversaSegment = DTOEdiversaSegment.Factory(sLine)
                _Segments.Add(oSegment)
            Next
        End If
    End Sub

    Public Function FieldValue(segmentTag As String, fieldIdx As Integer) As String
        Dim retval As String = ""
        Dim oSegment As DTOEdiversaSegment = _Segments.FirstOrDefault(Function(x) x.Fields(0) = segmentTag)
        If oSegment IsNot Nothing Then
            If oSegment.Fields.Count > fieldIdx Then
                retval = oSegment.Fields(fieldIdx)
            End If
        End If
        Return retval
    End Function

    Public Function TagNom() As String
        Dim r As New Regex("^[a-zA-Z0-9_.-]*", RegexOptions.IgnoreCase)
        Dim oMatch As System.Text.RegularExpressions.Match = r.Match(_Stream)
        Dim retval As String = oMatch.Value
        Return retval
    End Function

    Public Function TagCod() As DTOEdiversaFile.Tags
        Dim sTag As String = Me.TagNom()
        Dim retval As DTOEdiversaFile.Tags = [Enum].Parse(GetType(DTOEdiversaFile.Tags), sTag)
        Return retval
    End Function

    Public Function GetFch(exs As List(Of DTOEdiversaException)) As Date
        Dim sFch As String = Me.FieldValue("DTM", 1)
        Dim retval As Date = ParseFch(sFch, exs)
        Return retval
    End Function

    Shared Function ParseEAN(src As String, exs As List(Of DTOEdiversaException)) As DTOEan
        Dim retval As DTOEan = Nothing
        If src > "" Then
            Dim pattern As String = "[^ -~]+" 'selecciona tots els caracters entre l'espai i la tilde (ASCII 32 - ASCII 126)
            Dim reg_exp As New System.Text.RegularExpressions.Regex(pattern)
            src = reg_exp.Replace(src, " ") '(clean non ASCII chars)

            If src.Length = 13 Then
                retval = DTOEan.Factory(src)
            Else
                exs.Add(DTOEdiversaException.Factory(DTOEdiversaException.Cods.BadFormatEAN, src, src & " EAN no valid"))
            End If
        Else
            exs.Add(DTOEdiversaException.Factory(DTOEdiversaException.Cods.MissingEan, src, src & " falta EAN"))
        End If

        Return retval
    End Function

    Shared Function ParseFch(src As String, exs As List(Of DTOEdiversaException)) As Date
        Dim retval As Date = Nothing
        Dim sFormat As String = ""
        Dim provider As Globalization.CultureInfo = Globalization.CultureInfo.InvariantCulture
        Try
            Select Case src.Length
                Case 0
                Case 6
                    retval = Date.ParseExact(src, "yyMMdd", provider)
                Case 8
                    retval = Date.ParseExact(src, "yyyyMMdd", provider)
                Case 12
                    retval = Date.ParseExact(src, "yyyyMMddHHmm", provider)
                Case Else
                    exs.Add(DTOEdiversaException.Factory(DTOEdiversaException.Cods.BadFormatFch, src, "No es pot convertir " & src & " en una data"))
            End Select

        Catch ex As Exception
            exs.Add(DTOEdiversaException.Factory(DTOEdiversaException.Cods.BadFormatFch, src))
        End Try

        Return retval
    End Function

    Shared Function ParseAmt(src As String, exs As List(Of DTOEdiversaException)) As DTOAmt
        Dim retval As DTOAmt = Nothing
        Dim provider As Globalization.CultureInfo = Globalization.CultureInfo.InvariantCulture
        Try
            Dim DcValue As Decimal = Decimal.Parse(src, System.Globalization.CultureInfo.InvariantCulture)
            retval = DTOAmt.factory(DcValue)

        Catch ex As Exception
            exs.Add(DTOEdiversaException.Factory(DTOEdiversaException.Cods.BadFormatDecimal, src, src & " no identificat com a import"))
        End Try

        Return retval
    End Function


    Public Function GetDocNum() As String
        Dim retval As String = ""
        Select Case _Tag
            Case DTOEdiversaFile.Tags.ORDERS_D_96A_UN_EAN008.ToString
                retval = Me.FieldValue("ORD", 1)

            Case DTOEdiversaFile.Tags.INVOIC_D_93A_UN_EAN007.ToString,
                 DTOEdiversaFile.Tags.INVOIC_D_96A_UN_EAN008.ToString,
                 DTOEdiversaFile.Tags.INVOIC_D_01B_UN_EAN010.ToString
                retval = Me.FieldValue("INV", 1)

            Case DTOEdiversaFile.Tags.REMADV_D_96A_UN_EAN003.ToString,
                 DTOEdiversaFile.Tags.DESADV_D_96A_UN_EAN008.ToString,
                 DTOEdiversaFile.Tags.ORDRSP_D_96A_UN_EAN005.ToString,
                 DTOEdiversaFile.Tags.SLSRPT_D_96A_UN_EAN004.ToString,
                 DTOEdiversaFile.Tags.APERAK_D_01B_UN_EAN003.ToString

                retval = Me.FieldValue("BGM", 1)

        End Select
        Return retval
    End Function

    Public Function GetAmount(exs As List(Of DTOEdiversaException)) As DTOAmt
        Dim retval As DTOAmt = Nothing
        Dim sEur As String = Me.FieldValue("MOARES", 1)
        If Not String.IsNullOrEmpty(sEur) Then
            TryParseAmt(sEur, retval, exs)
        End If
        Return retval
    End Function

    Shared Function TryParseAmt(src As String, ByRef oAmt As DTOAmt, exs As List(Of DTOEdiversaException)) As Boolean
        Dim retval As Boolean
        Dim provider As Globalization.CultureInfo = Globalization.CultureInfo.InvariantCulture
        Try
            Dim DcValue As Decimal = Decimal.Parse(src, System.Globalization.CultureInfo.InvariantCulture)
            oAmt = DTOAmt.factory(DcValue)
            retval = True
        Catch ex As Exception
            exs.Add(DTOEdiversaException.Factory(DTOEdiversaException.Cods.BadFormatDecimal, Nothing, src & " no identificat com a import"))
        End Try

        Return retval
    End Function

    Shared Function EdiFormat(oAmt As DTOAmt) As String
        Dim retval As String = EdiFormat(oAmt.Eur)
        Return retval
    End Function

    Shared Function EdiFormat(DcValue As Decimal) As String
        Dim sValue As String = Format(DcValue, "#0.00")
        Dim retval As String = sValue.Replace(",", ".")
        Return retval
    End Function

    Shared Function EdiFormat(DtFch As Date) As String
        Dim retval As String = Format(DtFch, "yyyyMMdd")
        Return retval
    End Function

    Public Function GetIOCod(oOrg As DTOContact, exs As List(Of DTOEdiversaException)) As DTOEdiversaFile.IOcods
        Dim oOrgEan As DTOEan = oOrg.GLN
        Dim oSenderEan As DTOEan = Me.Sender.Ean
        Dim retval As DTOEdiversaFile.IOcods
        If oOrgEan.Equals(oSenderEan) Then
            retval = DTOEdiversaFile.IOcods.Outbox
        Else
            retval = DTOEdiversaFile.IOcods.Inbox
        End If
        Return retval
    End Function


    Shared Function GetSenderFromSegments(oFile As DTOEdiversaFile, Optional oInterlocutors As List(Of DTOContact) = Nothing) As DTOEdiversaContact
        Dim retval As DTOEdiversaContact = Nothing
        Dim sEan As String = oFile.FieldValue("NADMS", 1)
        If sEan = "" Then
            Select Case oFile.TagCod()
                Case DTOEdiversaFile.Tags.DESADV_D_96A_UN_EAN008
                    sEan = oFile.FieldValue("NADSU", 1) 'suplier
                Case DTOEdiversaFile.Tags.GENRAL_D_96A_UN_EAN003
                Case DTOEdiversaFile.Tags.INVOIC_D_01B_UN_EAN010
                    sEan = oFile.FieldValue("NADSU", 1) 'suplier
                Case DTOEdiversaFile.Tags.INVOIC_D_93A_UN_EAN007
                    sEan = oFile.FieldValue("NADSU", 1) 'suplier
                Case DTOEdiversaFile.Tags.INVOIC_D_96A_UN_EAN008
                    sEan = oFile.FieldValue("NADSU", 1) 'suplier
                Case DTOEdiversaFile.Tags.INVRPT_D_96A_UN_EAN008
                    sEan = oFile.FieldValue("NADSU", 1) 'suplier
                Case DTOEdiversaFile.Tags.ORDERS_D_96A_UN_EAN008
                    sEan = oFile.FieldValue("NADBY", 1) 'buyer
                Case DTOEdiversaFile.Tags.ORDRSP_D_96A_UN_EAN005
                    sEan = oFile.FieldValue("NADSU", 1) 'suplier
                Case DTOEdiversaFile.Tags.REMADV_D_96A_UN_EAN003
                    sEan = oFile.FieldValue("NADPR", 1) 'emisor del pago
                Case DTOEdiversaFile.Tags.SLSRPT_D_96A_UN_EAN004
                    sEan = oFile.FieldValue("NADFR", 1)
            End Select
        End If

        If sEan <> "" Then
            retval = New DTOEdiversaContact()
            retval.Ean = DTOEan.Factory(sEan)
            If oInterlocutors Is Nothing Then
            Else
                retval.Contact = oInterlocutors.FirstOrDefault(Function(x) x.GLN.Value = sEan)
            End If
        End If
        Return retval

    End Function

    Shared Function GetReceiverFromSegments(oFile As DTOEdiversaFile, Optional oInterlocutors As List(Of DTOContact) = Nothing) As DTOEdiversaContact
        Dim retval As DTOEdiversaContact = Nothing
        Dim sEan As String = oFile.FieldValue("NADMR", 1)
        If sEan = "" Then
            Select Case oFile.TagCod()
                Case DTOEdiversaFile.Tags.DESADV_D_96A_UN_EAN008
                    sEan = oFile.FieldValue("NADBY", 1) 'buyer
                Case DTOEdiversaFile.Tags.GENRAL_D_96A_UN_EAN003
                Case DTOEdiversaFile.Tags.INVOIC_D_01B_UN_EAN010
                    sEan = oFile.FieldValue("NADBY", 1) 'buyer
                Case DTOEdiversaFile.Tags.INVOIC_D_93A_UN_EAN007
                    sEan = oFile.FieldValue("NADBY", 1) 'buyer
                Case DTOEdiversaFile.Tags.INVOIC_D_96A_UN_EAN008
                    sEan = oFile.FieldValue("NADBY", 1) 'buyer
                Case DTOEdiversaFile.Tags.INVRPT_D_96A_UN_EAN008
                    sEan = oFile.FieldValue("NADBY", 1) 'buyer
                Case DTOEdiversaFile.Tags.ORDERS_D_96A_UN_EAN008
                    sEan = oFile.FieldValue("NADSU", 1) 'suplier
                Case DTOEdiversaFile.Tags.ORDRSP_D_96A_UN_EAN005
                    sEan = oFile.FieldValue("NADBY", 1) 'buyer
                Case DTOEdiversaFile.Tags.REMADV_D_96A_UN_EAN003
                    sEan = oFile.FieldValue("NADPE", 1) 'receptor del pago
                Case DTOEdiversaFile.Tags.SLSRPT_D_96A_UN_EAN004
            End Select
        End If
        If sEan <> "" Then
            retval = New DTOEdiversaContact()
            retval.Ean = DTOEan.Factory(sEan)
            If oInterlocutors IsNot Nothing Then
                retval.Contact = oInterlocutors.FirstOrDefault(Function(x) x.GLN.Value = sEan)
            End If
        End If
        Return retval
    End Function

    Shared Function ReadDocNum(oFile As DTOEdiversaFile) As String
        Dim retval As String = ""
        Select Case oFile.Tag
            Case DTOEdiversaFile.Tags.ORDERS_D_96A_UN_EAN008.ToString
                retval = oFile.FieldValue("ORD", 1)

            Case DTOEdiversaFile.Tags.INVOIC_D_93A_UN_EAN007.ToString,
                 DTOEdiversaFile.Tags.INVOIC_D_96A_UN_EAN008.ToString,
                 DTOEdiversaFile.Tags.INVOIC_D_01B_UN_EAN010.ToString
                retval = oFile.FieldValue("INV", 1)

            Case DTOEdiversaFile.Tags.REMADV_D_96A_UN_EAN003.ToString,
                 DTOEdiversaFile.Tags.DESADV_D_96A_UN_EAN008.ToString,
                 DTOEdiversaFile.Tags.ORDRSP_D_96A_UN_EAN005.ToString,
                 DTOEdiversaFile.Tags.SLSRPT_D_96A_UN_EAN004.ToString,
                 DTOEdiversaFile.Tags.APERAK_D_01B_UN_EAN003.ToString
                retval = oFile.FieldValue("BGM", 1)

        End Select
        Return retval
    End Function

    Shared Sub AddSegment(ByRef sb As Text.StringBuilder, tag As String, ParamArray fields() As Object)
        Dim segment As String = DTOEdiversaSegment.Factory(tag, fields)
        sb.AppendLine(segment)
    End Sub

    Shared Function Excel(oFiles As List(Of DTOEdiversaFile)) As MatHelperStd.ExcelHelper.Sheet
        Dim retval As New MatHelperStd.ExcelHelper.Sheet(oFiles.First.Tag, "Edi")
        With retval
            .AddColumn("fitxer", MatHelperStd.ExcelHelper.Sheet.NumberFormats.PlainText)
            .AddColumn("rebut", MatHelperStd.ExcelHelper.Sheet.NumberFormats.DDMMYY)
            .AddColumn("document", MatHelperStd.ExcelHelper.Sheet.NumberFormats.PlainText)
            .AddColumn("data doc", MatHelperStd.ExcelHelper.Sheet.NumberFormats.DDMMYY)
            .AddColumn("import", MatHelperStd.ExcelHelper.Sheet.NumberFormats.Euro)
            .AddColumn("remitent/destinatari", MatHelperStd.ExcelHelper.Sheet.NumberFormats.PlainText)

            For Each oFile As DTOEdiversaFile In oFiles
                Dim oRow As MatHelperStd.ExcelHelper.Row = .AddRow()
                oRow.AddCell(oFile.FileName)
                oRow.AddCell(oFile.FchCreated)
                oRow.AddCell(oFile.Fch)

                If oFile.Amount Is Nothing Then
                    oRow.AddCell()
                Else
                    oRow.AddCellAmt(oFile.Amount)
                End If

                Select Case oFile.IOCod
                    Case DTOEdiversaFile.IOcods.Inbox
                        If oFile.Sender IsNot Nothing Then
                            If oFile.Sender.Contact Is Nothing Then
                                oRow.AddCell()
                            Else
                                oRow.AddCell(oFile.Sender.Contact.nom)
                            End If
                        End If
                    Case DTOEdiversaFile.IOcods.Outbox
                        If oFile.Receiver IsNot Nothing Then
                            If oFile.Receiver.Contact Is Nothing Then
                                oRow.AddCell()
                            Else
                                oRow.AddCell(oFile.Receiver.Contact.nom)
                            End If
                        End If
                End Select

                oRow.AddCell(oFile.Docnum)

            Next
        End With
        Return retval
    End Function

End Class

Public Class DTOEdiversaSegment
    Property Fields As List(Of String)

    Public Sub New()
        MyBase.New
        _Fields = New List(Of String)
    End Sub

    Public Function ParseString(iField As Integer, exs As List(Of DTOEdiversaException)) As String
        Dim retval As String = ""
        If _Fields.Count > iField Then
            retval = _Fields(iField)
        End If
        Return retval
    End Function

    Public Function ParseDecimal(iField As Integer, exs As List(Of DTOEdiversaException)) As Integer
        Dim retval As Decimal = 0
        If _Fields.Count > iField Then
            retval = Decimal.Parse(_Fields(iField), Globalization.CultureInfo.InvariantCulture)
        End If
        Return retval
    End Function

    Public Function ParseInteger(iField As Integer, exs As List(Of DTOEdiversaException)) As Integer
        Dim retval As Integer = 0
        If _Fields.Count > iField Then
            retval = CInt(_Fields(iField))
        End If
        Return retval
    End Function

    Public Function ParseFch(iField As Integer, exs As List(Of DTOEdiversaException)) As Date
        Dim retval As Date
        If _Fields.Count > iField Then
            Dim src As String = _Fields(iField)
            Dim sFormat As String = ""
            Dim provider As Globalization.CultureInfo = Globalization.CultureInfo.InvariantCulture
            Try
                Select Case src.Length
                    Case 6
                        retval = Date.ParseExact(src, "yyMMdd", provider)
                    Case 8
                        retval = Date.ParseExact(src, "yyyyMMdd", provider)
                    Case Else
                        exs.Add(DTOEdiversaException.Factory(DTOEdiversaException.Cods.BadFormatFch, String.Format("No es pot convertir {0} en una data", src)))
                End Select

            Catch ex As Exception
                exs.Add(DTOEdiversaException.Factory(DTOEdiversaException.Cods.BadFormatFch, src))
            End Try
        End If

        Return retval
    End Function

    Public Function ParseEan(iField As Integer, exs As List(Of DTOEdiversaException)) As DTOEan
        Dim retval As DTOEan = Nothing
        If _Fields.Count > iField Then
            retval = DTOEan.Factory(_Fields(iField))
        End If
        Return retval
    End Function

    Shared Function Factory(src As String) As DTOEdiversaSegment
        Dim retval As New DTOEdiversaSegment
        Dim sFields() As String = Regex.Split(src, "\|")
        For Each sField As String In sFields
            retval.Fields.Add(sField)
        Next
        Return retval
    End Function

    Public Shadows Function ToString() As String
        Dim retval As String = String.Join("|", _Fields.ToArray)
        Return retval
    End Function

    Shared Function Factory(tag As String, ParamArray fields As Object()) As String
        Dim sb As New Text.StringBuilder
        sb.Append(tag)
        For Each field In fields
            sb.Append("|")
            If TypeOf field Is String Or TypeOf field Is Integer Then
                sb.Append(field)
            ElseIf TypeOf field Is Date Then
                sb.Append(DTOEdiversaFile.EdiFormat(CDate(field)))
            ElseIf TypeOf field Is DTOAmt Then
                sb.Append(DTOEdiversaFile.EdiFormat(CType(field, DTOAmt)))
            ElseIf TypeOf field Is Decimal Then
                sb.Append(DTOEdiversaFile.EdiFormat(CType(field, Decimal)))
            End If
        Next
        Dim retval As String = sb.ToString
        Return retval
    End Function


End Class

Public Class DTOEdiversaContact
    Property Cod As Cods
    Property Ean As DTOEan
    Property Nom As String
    Property DadesRegistrals As String
    Property Domicili As String
    Property Poblacio As String
    Property Zip As String
    Property Nif As String

    Property Contact As DTOContact
    Public Enum Cods
        NotSet
        NADSCO '_Proveidor
        NADBCO '_Comprador
        NADSU '_Proveidor
        NADBY '_Comprador
        NADII '_EmisorFactura
        NADIV '_ReceptorFactura
        NADMS '_EmisorMissatge
        NADMR '_ReceptorMissatge
        NADDP '_ReseptorMercancia
        NADPR '_Pagador
        NADPE '_Cobrador
        NADPW '_PuntDeRecollida
        NADUD '_ClientFinal
        NADFW '_Forwarder
        NADSE '_Venedor
        NADRE '_Cobrador
        NADCO '_Venedor
    End Enum

End Class

Public Class DTOEdiversaException
    Inherits DTOBaseGuid
    Property Cod As Cods
    Property Tag As DTOBaseGuid
    Property TagCod As TagCods
    Property Msg As String

    Public Enum TagCods
        NotSet
        Sku
        PurchaseOrder
        EdiversaOrder
        Contact
        EdiversaFile
        EdiversaOrderItem
    End Enum

    Public Enum Cods
        NotSet
        SkuNotFound
        WrongPrice
        MissingPrice
        WrongDiscount
        BadFormatFch
        MissingSegmentFields
        ContactCompradorNotFound
        ReceptorMercanciaNotFound
        PlatformNotFound
        PlatformNoValid
        BadFormatDecimal
        SkuObsolet
        DuplicatedOrder
        InterlocutorNotFound
        MissingSkuEAN
        BadFormatEAN
        MissingEan
        PurchaseOrderNotFound
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
    End Sub

    Shared Function Factory(oCod As Cods, oTag As Object, Optional sMsg As String = "") As DTOEdiversaException
        Dim retval As New DTOEdiversaException
        With retval
            .Cod = oCod
            .Tag = oTag
            .Msg = sMsg
        End With
        Return retval
    End Function

    Shared Function FromSystemExceptions(exs As List(Of Exception)) As List(Of DTOEdiversaException)
        Dim retval As New List(Of DTOEdiversaException)
        For Each item In exs
            Dim sMsg As String = item.Message
            retval.Add(DTOEdiversaException.Factory(DTOEdiversaException.Cods.NotSet, Nothing, sMsg))
        Next
        Return retval
    End Function

    Shared Function ToSystemExceptions(exs As List(Of DTOEdiversaException)) As List(Of Exception)
        Dim retval As New List(Of Exception)
        For Each item In exs
            retval.Add(New Exception(item.Msg))
        Next
        Return retval
    End Function
End Class
