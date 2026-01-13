Public Class DTOIntrastat
    Inherits DTOBaseGuid

    Property Emp As DTOEmp
    Property Flujo As Flujos
    Property Yea As Integer
    Property Mes As Integer
    Property Ord As Integer
    Property PartidasCount As Integer
    Property Units As Integer
    Property Kg As Decimal
    Property Amt As DTOAmt
    Property Csv As String
    Property DocFile As DTODocFile

    Property Items As List(Of DTOImportacioItem)
    Property Partidas As List(Of DTOIntrastat.Partida)

    Property ExceptionSkus As List(Of DTOProductSku)

    Public Enum Flujos
        Introduccion
        Expedicion
    End Enum

    Public Shadows Function Trimmed() As DTOIntrastat
        Dim retval As New DTOIntrastat(MyBase.Guid)
        With retval
            .Emp = New DTOEmp(_Emp.Id)
            .Flujo = _Flujo
            .Yea = _Yea
            .Mes = _Mes
            .Ord = _Ord
            .PartidasCount = _PartidasCount
            .Units = _Units
            .Kg = _Kg
            .Amt = _Amt
            .Csv = _Csv
            .DocFile = _DocFile
            .Partidas = New List(Of DTOIntrastat.Partida)
            For Each partida As DTOIntrastat.Partida In _Partidas
                .Partidas.Add(partida.Trimmed)
            Next
        End With
        Return retval
    End Function

    Public Property YearMonth As DTOYearMonth
        Get
            Return New DTOYearMonth(_Yea, _Mes)
        End Get
        Set(value As DTOYearMonth)
            _Yea = value.Year
            _Mes = value.Month
        End Set
    End Property

    Public Sub New()
        MyBase.New()
        _Items = New List(Of DTOImportacioItem)
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
        _Items = New List(Of DTOImportacioItem)
    End Sub

    Shared Function Caption(oIntrastat As DTOIntrastat, oLang As DTOLang) As String
        Dim retval As String = ""
        If oIntrastat IsNot Nothing Then
            retval = oLang.MesAbr(oIntrastat.Mes) & "/" & oIntrastat.Yea
        End If
        Return retval
    End Function

    Shared Function DefaultFileName(oIntrastat As DTOIntrastat) As String
        Dim sb As New System.Text.StringBuilder
        sb.Append("M+O.Intrastat.")
        sb.Append(oIntrastat.Yea.ToString())
        sb.Append(".")
        sb.Append(Format(oIntrastat.Mes, "00"))
        sb.Append(".")
        Select Case oIntrastat.Flujo
            Case DTOIntrastat.Flujos.Introduccion
                sb.Append("import")
            Case DTOIntrastat.Flujos.Expedicion
                sb.Append("export")
        End Select
        sb.Append(".")
        sb.Append(oIntrastat.Ord)
        sb.Append(".txt")
        Return sb.ToString
    End Function

    Shared Function FileStringBuilder(oIntrastat As DTOIntrastat) As String
        'se importará la información de las partidas desde un fichero de texto generado al efecto, 
        'conteniendo un registro por cada partida de la declaración. 
        Dim sb As New System.Text.StringBuilder
        For Each oPartida In oIntrastat.Partidas
            sb.AppendLine(DTOIntrastat.Partida.Row(oPartida))
        Next
        Return sb.ToString
    End Function

    Shared Function SkusDeclarables(oIntrastat As DTOIntrastat) As List(Of DTOProductSku)
        Dim oAllSkus As List(Of DTOProductSku) = Nothing
        Select Case oIntrastat.Flujo
            Case DTOIntrastat.Flujos.Introduccion
                Dim oDeliveries = oIntrastat.Partidas.Select(Function(x) CType(x.Tag, DTODelivery)).Distinct.ToList
                oAllSkus = oDeliveries.SelectMany(Function(y) y.Items).Select(Function(z) z.Sku).ToList
            Case DTOIntrastat.Flujos.Expedicion
                Dim oInvoices = oIntrastat.Partidas.Select(Function(x) CType(x.Tag, DTOInvoice)).Distinct.ToList
                oAllSkus = oInvoices.SelectMany(Function(x) x.Deliveries).SelectMany(Function(y) y.Items).Select(Function(z) z.Sku).ToList
        End Select
        Dim retval = oAllSkus.Where(Function(p) DTOIntrastat.SkuIsDeclarable(p)).ToList
        Return retval
    End Function

    Shared Function SkuIsDeclarable(oSku As DTOProductSku) As Boolean
        Dim retval As Boolean
        Select Case oSku.Category.codi
            Case DTOProductCategory.Codis.Standard, DTOProductCategory.Codis.Accessories
                retval = True
                If oSku.isBundle Then retval = False
        End Select
        If oSku.NoStk Then retval = False
        Return retval
    End Function

    Shared Function SkuKg(oSku As DTOProductSku) As Decimal
        Return DTOProductSku.KgNetOrInheritedOrBrut(oSku)
    End Function

    Shared Function ExcelExport(oIntrastat As DTOIntrastat) As MatHelperStd.ExcelHelper.Book
        Dim sFilename As String = String.Format("Intrastat {0:0000}{1:00}", oIntrastat.Yea, oIntrastat.Mes)
        Dim retval As New MatHelperStd.ExcelHelper.Book(sFilename)
        With retval.Sheets
            .Add(ExcelPartides(oIntrastat))
            .Add(ExcelSkus(oIntrastat))
        End With
        Return retval
    End Function

    Shared Function ExcelPartides(oIntrastat As DTOIntrastat) As MatHelperStd.ExcelHelper.Sheet
        Dim retval As New MatHelperStd.ExcelHelper.Sheet("Partides")
        With retval
            .AddColumn("data", MatHelperStd.ExcelHelper.Sheet.NumberFormats.DDMMYY)
            .AddColumn("factura", MatHelperStd.ExcelHelper.Sheet.NumberFormats.W50)
            .AddColumn("client", MatHelperStd.ExcelHelper.Sheet.NumberFormats.PlainText)
            .AddColumn("pais", MatHelperStd.ExcelHelper.Sheet.NumberFormats.W50)
            .AddColumn("incoterm", MatHelperStd.ExcelHelper.Sheet.NumberFormats.W50)
            .AddColumn("codi", MatHelperStd.ExcelHelper.Sheet.NumberFormats.W50)
            If oIntrastat.Flujo = DTOIntrastat.Flujos.Introduccion Then
                .AddColumn("made in", MatHelperStd.ExcelHelper.Sheet.NumberFormats.W50)
            End If
            .AddColumn("pes net", MatHelperStd.ExcelHelper.Sheet.NumberFormats.Kg)
            .AddColumn("import", MatHelperStd.ExcelHelper.Sheet.NumberFormats.Euro)
        End With

        Select Case oIntrastat.Flujo
            Case DTOIntrastat.Flujos.Expedicion
                For Each item In oIntrastat.Partidas
                    Dim oInvoice As DTOInvoice = item.Tag
                    Dim oRow As MatHelperStd.ExcelHelper.Row = retval.AddRow
                    With oRow
                        .AddCell(oInvoice.fch)
                        .AddCell(oInvoice.num)
                        .AddCell(oInvoice.customer.nom)
                        .AddCell(DTOAddress.Country(oInvoice.customer.address).ISO)
                        .AddCell(item.Incoterm.ToString())
                        .AddCell(item.CodiMercancia.Id)
                        '.AddCell(item.MadeIn.ISO)
                        .AddCell(item.Kg)
                        .AddCell(item.ImporteFacturado)
                    End With
                Next
            Case DTOIntrastat.Flujos.Introduccion
                For Each item In oIntrastat.Partidas
                    Dim oDelivery As DTODelivery = item.Tag
                    Dim oRow As MatHelperStd.ExcelHelper.Row = retval.AddRow
                    With oRow
                        .AddCell(oDelivery.fch)
                        .AddCell(oDelivery.id)
                        .AddCell(oDelivery.Contact.nom)
                        .AddCell(DTOAddress.Country(oDelivery.address).ISO)
                        .AddCell(item.Incoterm.ToString())
                        .AddCell(item.CodiMercancia.Id)
                        .AddCell(item.MadeIn.ISO)
                        .AddCell(item.Kg)
                        .AddCell(item.ImporteFacturado)
                    End With
                Next
        End Select

        Return retval
    End Function

    Shared Function ExcelSkus(oIntrastat As DTOIntrastat) As MatHelperStd.ExcelHelper.Sheet
        Dim retval As New MatHelperStd.ExcelHelper.Sheet("Productes")
        With retval
            .AddColumn("marca comercial", MatHelperStd.ExcelHelper.Sheet.NumberFormats.W50)
            .AddColumn("categoria", MatHelperStd.ExcelHelper.Sheet.NumberFormats.W50)
            .AddColumn("producte", MatHelperStd.ExcelHelper.Sheet.NumberFormats.W50)
            .AddColumn("codi", MatHelperStd.ExcelHelper.Sheet.NumberFormats.W50)
            .AddColumn("made in", MatHelperStd.ExcelHelper.Sheet.NumberFormats.W50)
            .AddColumn("pes net", MatHelperStd.ExcelHelper.Sheet.NumberFormats.Kg)
        End With

        Dim oSkus = DTOIntrastat.SkusDeclarables(oIntrastat)

        For Each item In oSkus
            Dim oRow As MatHelperStd.ExcelHelper.Row = retval.AddRow
            With oRow
                .AddCell(item.category.Brand.nom)
                .AddCell(item.category.nom)
                .AddCell(item.nomCurt)
                .AddCell(item.codiMercancia.Id)
                .AddCell(item.madeIn.ISO)
                .AddCell(DTOProductSku.KgNetOrInheritedOrBrut(item))
            End With
        Next
        Return retval
    End Function


    Public Class Partida
        Property Lin As Integer
        Property Tag As DTOBaseGuid
        Property Country As DTOCountry
        Property Provincia As DTOAreaProvincia
        Property Incoterm As DTOProveidor.Incoterms
        Property NaturalezaTransaccion As NaturalezasTransaccion = NaturalezasTransaccion.CompraEnFirme
        Property CodiTransport As CodisTransport = CodisTransport.Carretera
        Property Port As String = ""
        Property CodiMercancia As DTOCodiMercancia
        Property MadeIn As DTOCountry
        Property RegimenEstadistico As RegimenesEstadisticos = RegimenesEstadisticos.DestinoFinalEuropa
        Property UnidadesSuplementarias As Decimal = 0
        Property Kg As Decimal
        Property ImporteFacturado As Decimal
        Property ValorEstadistico As Decimal
        Property Exceptions As List(Of DTOIntrastat.Exception)


        Public Enum NaturalezasTransaccion
            NotSet
            CompraEnFirme = 11
        End Enum

        Public Enum CodisTransport
            NotSet
            Maritimo
            Ferrocarril
            Carretera
            Aereo
            EnvioPostal
        End Enum

        Public Enum RegimenesEstadisticos
            NotSet
            DestinoFinalEuropa
        End Enum

        Shared Function Factory(oEmp As DTOEmp, oDelivery As DTODelivery) As List(Of DTOIntrastat.Partida)
            Dim oProveidor = oDelivery.Proveidor
            Dim oCountry = DTOAddress.Country(oDelivery.Address)
            Dim oProvincia = DTOAddress.Provincia(oEmp.Org.Address)
            Dim oIncoterm = IIf(oProveidor.IncoTerm = DTOProveidor.Incoterms.NotSet, DTOProveidor.Incoterms.CIF, oProveidor.IncoTerm)

            Dim items = oDelivery.Items.Where(Function(y) DTOIntrastat.SkuIsDeclarable(y.Sku)).ToList

            Dim oNoCodiMercancia As New DTOCodiMercancia("")
            Dim oNoMadein As New DTOCountry(Guid.Empty, "")
            For Each item In items
                ' If item.bundle.Count > 0 Then Stop
                If item.sku.codiMercancia Is Nothing Then
                    item.sku.codiMercancia = oNoCodiMercancia
                End If
                If item.sku.madeIn Is Nothing Then item.sku.madeIn = oNoMadein
                'item.Sku.MadeIn = oNoMadein 'Para flujo expedición no cumplimentar país de origen
            Next

            Dim retval = items.
            Where(Function(z) z.Sku.NoStk = False And z.Price IsNot Nothing AndAlso z.Price.Eur > 0).
            GroupBy(Function(g) New With {Key g.Sku.CodiMercancia.Id, Key g.Sku.MadeIn.Guid, Key g.Sku.MadeIn.ISO}).
            Select(Function(group) New DTOIntrastat.Partida With {
            .Country = oCountry,
            .Provincia = oProvincia,
            .Incoterm = oIncoterm,
            .CodiMercancia = New DTOCodiMercancia(group.Key.Id),
            .MadeIn = New DTOCountry(group.Key.Guid, group.Key.ISO),
            .Kg = group.Sum(Function(x) x.Qty * DTOIntrastat.SkuKg(x.Sku)),
            .UnidadesSuplementarias = group.Sum(Function(x) x.Qty),
            .ImporteFacturado = group.Sum(Function(x) DTOAmt.Import(x.Qty, x.Price, x.Dto).Eur),
            .ValorEstadistico = group.Sum(Function(x) DTOAmt.Import(x.Qty, x.Price, x.Dto).Eur),
            .Tag = oDelivery
                       }).
            ToList

            Return retval
        End Function


        Shared Function Factory(oEmp As DTOEmp, oInvoice As DTOInvoice) As List(Of DTOIntrastat.Partida)
            Dim oCustomer = oInvoice.Customer
            Dim oCountry = DTOAddress.Country(oInvoice.Deliveries.First.Address)
            Dim oProvincia = DTOAddress.Provincia(oEmp.Org.Address)
            Dim oIncoterm = IIf(oCustomer.Incoterm = DTOProveidor.Incoterms.NotSet, DTOProveidor.Incoterms.CIF, oCustomer.Incoterm)

            Dim items = oInvoice.Deliveries.SelectMany(Function(x) x.Items).Where(Function(y) DTOIntrastat.SkuIsDeclarable(y.Sku)).ToList

            Dim oNoCodiMercancia As New DTOCodiMercancia("")
            Dim oNoMadein As New DTOCountry(Guid.Empty, "")
            For Each item In items
                If item.Sku.CodiMercancia Is Nothing Then
                    item.Sku.CodiMercancia = oNoCodiMercancia
                End If
                'If item.Sku.MadeIn Is Nothing Then item.Sku.MadeIn = oNoMadein
                item.Sku.MadeIn = oNoMadein 'Para flujo expedición no cumplimentar país de origen
            Next

            Dim retval = items.
            Where(Function(z) z.Sku.NoStk = False And z.Price IsNot Nothing AndAlso z.Price.Eur > 0).
            GroupBy(Function(g) New With {Key g.Sku.CodiMercancia.Id, Key g.Sku.MadeIn.Guid, Key g.Sku.MadeIn.ISO}).
            Select(Function(group) New DTOIntrastat.Partida With {
            .Country = oCountry,
            .Provincia = oProvincia,
            .Incoterm = oIncoterm,
            .CodiMercancia = New DTOCodiMercancia(group.Key.Id),
            .MadeIn = New DTOCountry(group.Key.Guid, group.Key.ISO),
            .Kg = group.Sum(Function(x) x.Qty * DTOIntrastat.SkuKg(x.Sku)),
            .UnidadesSuplementarias = group.Sum(Function(x) x.Qty),
            .ImporteFacturado = group.Sum(Function(x) DTOAmt.Import(x.Qty, x.Price, x.Dto).Eur),
            .ValorEstadistico = group.Sum(Function(x) DTOAmt.Import(x.Qty, x.Price, x.Dto).Eur),
            .Tag = oInvoice
                       }).
            ToList

            Return retval
        End Function

        Shared Function Warn(Flujo As DTOIntrastat.Flujos, Kg As Decimal, CodiMercancia As DTOCodiMercancia, MadeIn As DTOCountry) As Boolean
            Dim retval As Boolean
            If Kg = 0 Or CodiMercancia Is Nothing Then
                retval = True
            End If
            If Flujo = DTOIntrastat.Flujos.Introduccion And MadeIn Is Nothing Then
                retval = True
            End If
            Return retval
        End Function

        Public Shadows Function Trimmed() As DTOIntrastat.Partida
            Dim retval As New DTOIntrastat.Partida
            With retval
                .Tag = New DTOBaseGuid(_Tag.Guid)
                If _Country IsNot Nothing Then
                    .Country = New DTOCountry(_Country.Guid)
                End If
                If _Provincia IsNot Nothing Then
                    .Provincia = New DTOAreaProvincia(_Provincia.Guid)
                End If
                .Incoterm = _Incoterm
                .NaturalezaTransaccion = _NaturalezaTransaccion
                .CodiTransport = _CodiTransport
                .Port = _Port
                If _CodiMercancia IsNot Nothing Then
                    .CodiMercancia = _CodiMercancia
                End If
                If _MadeIn IsNot Nothing Then
                    .MadeIn = New DTOCountry(_MadeIn.Guid)
                End If
                .RegimenEstadistico = _RegimenEstadistico
                .UnidadesSuplementarias = _UnidadesSuplementarias
                .Kg = _Kg
                .ImporteFacturado = _ImporteFacturado
                .ValorEstadistico = _ValorEstadistico
            End With
            Return retval
        End Function

        Shared Function Row(oPartida As DTOIntrastat.Partida) As String

            'Cada registro contendrá los siguientes campos, y en el siguiente orden, separados por el carácter ‘;’ (punto y coma):

            ' 1. E M Procedencia/Destino Código ISO del Estado Miembro de Procedencia/Destino de la mercancía, en formato alfanumérico de dos posiciones. 
            ' 2. Provincia de Origen/Destino Código de la Provincia española de Origen/Destino de la mercancía, en formato numérico de dos posiciones. 
            ' 3. Cond. Entrega Código de las Condiciones de Entrega, en formato alfanumérico de tres posiciones. 
            ' 4. Nat. Transacción Código de la Naturaleza de la Transacción, en formato numérico de dos posiciones. 
            ' 5. Modalidad de Transporte Código del Modo de Transporte, en formato numérico de una posición. 
            ' 6. Puerto/Aeropuerto  Carg/Desca Código del Puerto/Aeropuerto español de Carga/Descarga de la mercancía, en formato numérico de cuatro posiciones. 
            ' 7. Código Mercancia Código de Nomenclatura Combinada correspondiente a la mercancía, en formato numérico de ocho posiciones. 
            ' 8. País Origen Código ISO del país de origen, en formato alfanumérico de dos posiciones. 
            ' 9. Régimen Estadístico Código del Régimen Estadístico, en formato numérico de una posición. 
            '10. Masa Neta Masa Neta de la mercancía, expresado en kilogramos. Formato numérico, máximo doce enteros y tres decimales separados por coma. 
            '11. Unidades Suplementarias Cantidad de Unidades Suplementarias. Formato numérico, máximo doce enteros y tres decimales separados por coma. 
            '12. Importe Factura Importe de Factura de la mercancía, expresado en euros con dos decimales. Formato numérico, máximo trece enteros y dos decimales separados por coma. 
            '13. Valor Estadístico Valor Estadístico de la mercancía, expresado en euros con dos decimales. Formato numérico, máximo trece enteros y dos decimales separados por coma. 

            'No todos los campos son obligatorios,  pero incluso cuando no tenga valor un campo concreto, se pondrá el carácter separador (;). Veamos algunos ejemplos:

            'FR;31;FOB;11;3;;85182190;CN;1;115;162;15,37;15,37
            'DE;28;CIF;11;1;0811;85182190;US;1;2459;1982;4589,46;4589,46
            'IT;12;FOB;11;3;;02012030;;1;800;;987,00;890,45


            Dim s As String = ""

            Dim sb As New System.Text.StringBuilder

            ' 1. Codigo Iso del Estado Miembro de Procedencia
            sb.Append(oPartida.Country.ISO)
            sb.Append(";")

            ' 2. Provincia de destino
            sb.Append(oPartida.Provincia.Cod)
            sb.Append(";")

            ' 3. Condiciones de entrega
            sb.Append(oPartida.Incoterm.ToString())
            sb.Append(";")

            ' 4. Naturaleza de la transaccion (cod. 2 digitos)
            '11 compraventa en firme
            sb.Append(oPartida.NaturalezaTransaccion)
            sb.Append(";")

            ' 5. Modo de transporte (3->carretera)
            sb.Append(oPartida.CodiTransport)
            sb.Append(";")

            ' 6. Puerto/aeropuerto de descarga
            sb.Append(";")

            ' 7. Código Mercancia (8 digitos)
            sb.Append(oPartida.CodiMercancia.Id)
            sb.Append(";")

            ' 8. País Origen Código ISO del país de origen, en formato alfanumérico de dos posiciones. 
            'sb.Append("") 'oPartida.MadeIn.ISO no cal informar a exportacions
            sb.Append(oPartida.MadeIn.ISO) ' no cal informar a exportacions
            sb.Append(";")

            ' 9. Régimen Estadístico Código del Régimen Estadístico, en formato numérico de una posición. 
            sb.Append("1") '1-> destino final España
            sb.Append(";")

            '10. Masa Neta Masa Neta de la mercancía, expresado en kilogramos. Formato numérico, máximo doce enteros y tres decimales separados por coma. 
            sb.Append(Format(oPartida.Kg, "#0.00"))
            sb.Append(";")

            '11. Unidades Suplementarias Cantidad de Unidades Suplementarias. Formato numérico, máximo doce enteros y tres decimales separados por coma. 
            sb.Append(Format(oPartida.UnidadesSuplementarias, "0"))
            sb.Append(";")

            '12. Importe Factura Importe de Factura de la mercancía, expresado en euros con dos decimales. Formato numérico, máximo trece enteros y dos decimales separados por coma. 
            sb.Append(Format(oPartida.ImporteFacturado, "#0.00"))
            sb.Append(";")

            '13. Valor Estadístico Valor Estadístico de la mercancía, expresado en euros con dos decimales. Formato numérico, máximo trece enteros y dos decimales separados por coma. 
            sb.Append(Format(oPartida.ValorEstadistico, "#0.00"))

            s = sb.ToString

            Return s
        End Function

    End Class

    Public Class Exception
        Inherits System.Exception

        Private _Sku As DTOProductSku
        Private _Cod As Cods

        Public Sub New(oSku As DTOProductSku, oCod As Cods)
            MyBase.New
            _Sku = oSku
            _Cod = oCod
        End Sub
        Public Enum Cods
            None
            MissingMadeIn
            MissimgCodiMercancia
            MissingKg
        End Enum
    End Class
End Class
