Public Class DTOImportacio
    Inherits DTOBaseGuid

    Property Emp As DTOEmp
    Property Id As Integer
    Property Yea As Integer
    Property Proveidor As DTOProveidor
    Property PlataformaDeCarga As DTOContact
    Property FchETD As DateTime = DateTime.MinValue
    Property FchETA As DateTime = DateTime.MinValue
    Property FchAvisTrp As DateTime
    Property Transportista As DTOTransportista
    Property Bultos As Integer
    Property Kg As Decimal
    Property M3 As Decimal
    Property Obs As String
    Property AvisCamio As Xml.XmlDocument
    Property ConfirmacioCamio As Xml.XmlDocument
    Property Intrastat As DTOIntrastat
    Property Amt As DTOAmt
    Property Goods As Decimal
    Property IncoTerm As DTOProveidor.Incoterms
    Property CountryOrigen As DTOCountry
    Property Matricula As String
    Property Week As Integer
    Property Arrived As Boolean
    Property Disabled As Boolean
    Property Exists As Boolean


    Property Items As List(Of DTOImportacioItem)

    Property TrpCost As DTOAmt

    Property Previsions As List(Of DTOImportPrevisio)
    Property Validacions As List(Of DTOImportValidacio)

    Property Deliveries As List(Of DTODelivery)

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
    End Sub

    Shared Function Factory(oEmp As DTOEmp, Optional oProveidor As DTOProveidor = Nothing) As DTOImportacio
        Dim retval As New DTOImportacio()
        Dim exs As New List(Of Exception)
        With retval
            .Emp = oEmp
            .FchETD = Today
            .FchETA = Today
            .Yea = Today.Year
            .Week = DTOImportacio.AvailableWeek(.FchETA)
            .Amt = DTOAmt.Empty
            If oProveidor IsNot Nothing Then
                .Proveidor = oProveidor
                .IncoTerm = oProveidor.IncoTerm
                .CountryOrigen = DTOAddress.Country(oProveidor.Address)
            End If
        End With
        Return retval
    End Function

    Shared Function FormattedId(oimportacio As DTOImportacio) As String
        Dim retval As String = String.Format("{0:0000}{1:0000}", oimportacio.Yea, oimportacio.Id)
        Return retval
    End Function

    Shared Function PlataformaDeCargaOProveidor(oImportacio As DTOImportacio) As DTOContact
        Dim retval = oImportacio.PlataformaDeCarga
        If retval Is Nothing Then retval = oImportacio.Proveidor
        Return retval
    End Function

    Shared Function CostMercancia(oImportacio As DTOImportacio) As DTOAmt
        Dim retval = DTOAmt.Empty
        For Each item As DTOImportacioItem In oImportacio.Items
            If item.SrcCod = DTOImportacioItem.SourceCodes.Fra Then
                retval.Add(item.Amt)
            End If
        Next
        Return retval
    End Function

    Shared Function CostTransport(oImportacio As DTOImportacio) As DTOAmt
        Dim retval = DTOAmt.Empty
        For Each item As DTOImportacioItem In oImportacio.Items
            If item.SrcCod = DTOImportacioItem.SourceCodes.FraTrp Then
                retval.Add(item.Amt)
            End If
        Next
        Return retval
    End Function

    Shared Function AvailableWeek(DtArrivalFch As Date) As Integer
        Dim oWeekday As DayOfWeek = DtArrivalFch.DayOfWeek
        Dim retval As Integer = DatePart(DateInterval.WeekOfYear, DtArrivalFch, , FirstWeekOfYear.FirstFourDays)
        If oWeekday > 3 Then retval = DatePart(DateInterval.WeekOfYear, DtArrivalFch.AddDays(7))
        Return retval
    End Function

    Shared Function Excel(oImportacions As List(Of DTOImportacio), oLang As DTOLang) As MatHelperStd.ExcelHelper.Sheet
        Dim retval As New MatHelperStd.ExcelHelper.Sheet("Remeses de Importacio")
        With retval
            .AddColumn(oLang.tradueix("remesa", "remesa", "Id"))
            .AddColumn("ETD", MatHelperStd.ExcelHelper.Sheet.NumberFormats.DDMMYY)
            .AddColumn("ETA", MatHelperStd.ExcelHelper.Sheet.NumberFormats.DDMMYY)
            .AddColumn(oLang.tradueix("valor", "valor", "value"), MatHelperStd.ExcelHelper.Sheet.NumberFormats.Euro)
            .AddColumn(oLang.tradueix("bultos", "bultos", "packages"))
            .AddColumn(oLang.tradueix("peso", "pes", "weight"), MatHelperStd.ExcelHelper.Sheet.NumberFormats.Kg)
            .AddColumn(oLang.tradueix("volumen", "volum", "volume"), MatHelperStd.ExcelHelper.Sheet.NumberFormats.m3trimmed)
            .AddColumn(oLang.tradueix("transporte", "transport", "transport"), MatHelperStd.ExcelHelper.Sheet.NumberFormats.Euro)
            .AddColumn(oLang.tradueix("matricula", "matricula", "plate"), MatHelperStd.ExcelHelper.Sheet.NumberFormats.Euro)

        End With
        For Each item In oImportacions
            Dim oCostMercancia As DTOAmt = DTOImportacio.CostMercancia(item)
            Dim oCostTransport As DTOAmt = DTOImportacio.CostTransport(item)

            Dim oRow As MatHelperStd.ExcelHelper.Row = retval.AddRow
            With oRow
                .AddCell(item.Id)
                .AddCell(item.FchETD)
                .AddCell(item.FchETA)
                .AddCell(oCostMercancia.eur)
                .AddCell(item.Bultos)
                .AddCell(item.Kg)
                .AddCell(item.M3)
                .AddCell(oCostTransport.eur)
                .AddCell(item.Matricula)
            End With
        Next

        Return retval
    End Function

End Class


Public Class DTOImportacioItem
    Inherits DTOBaseGuid

    Property SrcCod As SourceCodes

    Property Amt As DTOAmt
    Property DocFile As DTODocFile
    Property Descripcio As String
    Property Parent As DTOImportacio

    Property Tag As Object

    Public Enum SourceCodes
        NotSet
        Alb
        Fra
        Cmr
        FraTrp
        PackingList
        Proforma
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
    End Sub


    Shared Function Factory(oImportacio As DTOImportacio, oSrcCod As DTOImportacioItem.SourceCodes, Optional oGuid As Guid = Nothing) As DTOImportacioItem
        Dim retval As DTOImportacioItem = Nothing

        If oGuid = Nothing Then
            retval = New DTOImportacioItem
        Else
            retval = New DTOImportacioItem(oGuid)
        End If
        With retval
            .Parent = oImportacio
            .SrcCod = oSrcCod
        End With
        Return retval
    End Function

    Shared Function GetConcept(oImportacioItem As DTOImportacioItem) As String
        Dim s As String = ""
        Select Case oImportacioItem.SrcCod
            Case DTOImportacioItem.SourceCodes.Cmr
                s = "CMR"
            Case DTOImportacioItem.SourceCodes.Alb
                'Dim oAlb As DTODelivery = FEBL.Delivery.Find(oImportacioItem.Guid)
                If oImportacioItem.Tag Is Nothing Then
                    s = "(albará eliminat)"
                Else
                    Dim oAlb As DTODelivery = oImportacioItem.Tag.toobject(Of DTODelivery)
                    s = DTODelivery.Caption(oAlb)
                End If
            Case DTOImportacioItem.SourceCodes.Fra
                If oImportacioItem.Tag Is Nothing Then
                    s = "(error)"
                Else
                    Dim oCca As DTOCca = Nothing
                    Dim oTag = oImportacioItem.Tag
                    If TypeOf oTag Is DTOCca Then
                        oCca = oTag
                    Else
                        oCca = oTag.toobject(Of DTOCca)
                    End If
                    If oCca Is Nothing Then
                        s = "(factura eliminada)"
                    Else
                        s = oCca.Concept
                    End If

                End If
            Case DTOImportacioItem.SourceCodes.FraTrp
                Dim oCca As DTOCca = Nothing
                Dim oTag = oImportacioItem.Tag
                If TypeOf oTag Is DTOCca Then
                    oCca = oTag
                Else
                    oCca = oTag.toobject(Of DTOCca)
                End If
                If oCca Is Nothing Then
                    s = "(factura de transport eliminada)"
                Else
                    s = "transport: " & oCca.Concept
                End If
        End Select
        Return s
    End Function
End Class
