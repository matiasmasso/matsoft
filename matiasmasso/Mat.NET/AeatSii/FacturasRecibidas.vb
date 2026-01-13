Imports AeatSii.SoapFacturasRecibidas

Public Class FacturasRecibidas
    Public Const EMISOR_NOM As String = "MATIAS MASSO, S.A."
    Public Const EMISOR_NIF As String = "A58007857"


    Shared Function Send(entorno As Defaults.Entornos, oBookFras As List(Of DTOBookFra), oTaxes As List(Of DTOTax), oCert As Security.Cryptography.X509Certificates.X509Certificate2, exs As List(Of Exception)) As Boolean
        If oBookFras.Count > 0 Then
            Try

                Dim resp As RespuestaLRFRecibidasType
                Dim Ws As siiSOAPClient = CurrentWebService(entorno, oCert)

                Dim oSuministroLRFacturasRecibidas As SuministroLRFacturasRecibidas = FromBookFras(oBookFras, oTaxes, exs)

                If exs.Count = 0 Then
                    Dim logfilename = My.Computer.FileSystem.SpecialDirectories.MyDocuments & "\tmp\request.xml"
                    Serialize(oSuministroLRFacturasRecibidas, logfilename) 'Opcional: per debug crea un fitxer amb el resultat

                    resp = Ws.SuministroLRFacturasRecibidas(oSuministroLRFacturasRecibidas)
                    Serialize(resp, My.Computer.FileSystem.SpecialDirectories.MyDocuments & "\tmp\response.xml") 'Opcional: per debug crea un fitxer amb el resultat

                    Dim DtNow As Date = DTO.GlobalVariables.Now()
                    For Each o As RespuestaRecibidaType In resp.RespuestaLinea
                        'Dim sFch As String = o.IDFactura.FechaExpedicionFacturaEmisor
                        'Dim DtFch As Date = DateTime.Parse(sFch, System.Globalization.CultureInfo.GetCultureInfo("es-ES"))
                        Dim oBookFra As DTOBookFra = oBookFras.FirstOrDefault(Function(x) x.cca.fch.Date = o.IDFactura.FechaExpedicionFacturaEmisor And x.fraNum = o.IDFactura.NumSerieFacturaEmisor)
                        If oBookFra IsNot Nothing Then
                            Select Case o.EstadoRegistro
                                Case EstadoRegistroType.Correcto
                                    oBookFra.siiLog = New DTOSiiLog
                                    With oBookFra.siiLog
                                        .Result = DTOInvoice.SiiResults.correcto
                                        .Fch = DtNow
                                        .Csv = IIf(o.CSV > "", o.CSV, resp.CSV)
                                    End With
                                Case EstadoRegistroType.AceptadoConErrores
                                    oBookFra.siiLog = New DTOSiiLog
                                    With oBookFra.siiLog
                                        .Result = DTOInvoice.SiiResults.aceptadoConErrores
                                        .Fch = DtNow
                                        .Csv = IIf(o.CSV > "", o.CSV, resp.CSV)
                                        .ErrMsg = String.Format("Factura {0}: Error {1}. {2}", o.IDFactura.NumSerieFacturaEmisor, o.CodigoErrorRegistro, o.DescripcionErrorRegistro)
                                        exs.Add(New Exception(.ErrMsg))
                                    End With
                                Case EstadoRegistroType.Incorrecto
                                    If o.CodigoErrorRegistro = 3000 Then 'duplicada
                                        oBookFra.siiLog = New DTOSiiLog
                                        With oBookFra.siiLog
                                            .Result = DTOInvoice.SiiResults.correcto
                                            .Fch = Nothing 'resp.DatosPresentacion.TimestampPresentacion
                                            .Csv = o.CSV
                                            .ErrMsg = ""
                                        End With

                                        'SetInvoiceSiiLog(entorno, oBookFra, o.CSV, exs)
                                        exs.Add(New Exception("factura " & o.IDFactura.NumSerieFacturaEmisor & " duplicada"))
                                    Else
                                        oBookFra.siiLog = New DTOSiiLog
                                        With oBookFra.siiLog
                                            .Result = DTOInvoice.SiiResults.incorrecto
                                            .Fch = DtNow
                                            .ErrMsg = String.Format("Factura {0}: Error {1}. {2}", o.IDFactura.NumSerieFacturaEmisor, o.CodigoErrorRegistro, o.DescripcionErrorRegistro)
                                            exs.Add(New Exception(.ErrMsg))
                                        End With

                                    End If

                            End Select



                            'If o.CSV Is Nothing Then
                            'oBookFra.Csv = resp.CSV
                            'Else
                            'oBookFra.Csv = o.CSV
                            'End If

                        End If
                    Next
                Else
                    exs.Add(New Exception("AEATSII.FacturasRecibidas.Send: Error al generar SuministroLRFacturasRecibidas a partir de BookFras"))
                End If

            Catch ex As Exception
                exs.Add(New Exception("AEATSII.FacturasRecibidas.Send: Error"))
                exs.Add(ex)
            End Try

        End If
        Return exs.Count = 0
    End Function

    Shared Function FromBookFras(oBookFras As List(Of DTOBookFra), oTaxes As List(Of DTOTax), exs As List(Of Exception)) As SuministroLRFacturasRecibidas
        Dim values As New List(Of LRFacturasRecibidasType)

        For Each oBookFra As DTOBookFra In oBookFras
            values.Add(LRFacturaRecibidaType(oBookFra, oTaxes, exs))
        Next

        Dim retval As New SuministroLRFacturasRecibidas
        retval.Cabecera = Cabecera()
        retval.RegistroLRFacturasRecibidas = values.ToArray
        Return retval
    End Function

    Shared Function LRFacturaRecibidaType(value As DTOBookFra, oTaxes As List(Of DTOTax), exs As List(Of Exception)) As LRFacturasRecibidasType
        Dim retval As New LRFacturasRecibidasType
        With retval
            .PeriodoLiquidacion = New SoapFacturasRecibidas.RegistroSiiPeriodoLiquidacion
            With .PeriodoLiquidacion
                .Ejercicio = value.Cca.Fch.Year
                .Periodo = value.Cca.Fch.Month - 1
            End With
            .IDFactura = IDFactura(value)
            .FacturaRecibida = FacturaRecibida(value, oTaxes, exs)
        End With
        Return retval
    End Function

    Shared Function IDFactura(value As DTOBookFra) As IDFacturaRecibidaType
        Dim retval As New IDFacturaRecibidaType
        With retval
            .IDEmisorFactura = New IDFacturaRecibidaTypeIDEmisorFactura
            .IDEmisorFactura.Item = ContraparteID(value)
            .NumSerieFacturaEmisor = value.fraNum
            .FechaExpedicionFacturaEmisor = FormatFch(value.Cca.Fch)
        End With
        Return retval
    End Function

    Shared Function FacturaRecibida(value As DTOBookFra, oTaxes As List(Of DTOTax), exs As List(Of Exception)) As FacturaRecibidaType
        Dim retval As New FacturaRecibidaType
        With retval
            .TipoFactura = TipoFactura(value)
            .TipoRectificativa = ClaveTipoRectificativa(value, exs)
            .TipoRectificativaSpecified = .TipoRectificativa > 0

            If value.ClaveRegimenEspecialOTrascendencia > "" Then
                Dim sClave = String.Format("Item{0:00}", value.ClaveRegimenEspecialOTrascendencia)
                Dim oClave = [Enum].Parse(GetType(IdOperacionesTrascendenciaTributariaType), sClave)
                .ClaveRegimenEspecialOTrascendencia = oClave
            End If

            '.ImporteTotal = FormatNumber(value.total.Eur)
            .ImporteTotal = FormatNumber(value.TotalSinIrpf().Eur)
            .DescripcionOperacion = value.Dsc
            .DesgloseFactura = DesgloseFactura(value, oTaxes)

            .Contraparte = New SoapFacturasRecibidas.PersonaFisicaJuridicaType
            .Contraparte.NombreRazon = Left(value.contact.Nom, 40)
            .Contraparte.Item = ContraparteID(value)

            If value.Cca.Fch > value.Cca.UsrLog.FchCreated Then
                'FchRegContable no pot ser anterior a la data del devengo
                .FechaRegContable = FormatFch(value.Cca.Fch)
            Else
                .FechaRegContable = FormatFch(value.Cca.UsrLog.FchCreated)
            End If
            .CuotaDeducible = FormatNumber(value.QuotaDeducible(oTaxes))
        End With
        Return retval
    End Function

    Private Shared Function ClaveTipoRectificativa(value As DTOBookFra, exs As List(Of Exception)) As ClaveTipoRectificativaType
        Dim retval As ClaveTipoRectificativaType
        If value.TipoFra.StartsWith("R") Then
            Dim src As String = "I"
            If Not [Enum].TryParse(src, retval) Then
                exs.Add(New Exception("ClaveTipoRectificativa'" & src & "' desconeguda."))
            End If
        End If
        Return retval
    End Function


    Shared Function ContraparteID(value As DTOBookFra) As Object
        Dim retval As Object
        Dim oCountry As DTOCountry = value.contact.Address.Zip.Location.Zona.Country

        If oCountry.ISO = "ES" Then
            retval = value.contact.PrimaryNifValue()
        ElseIf oCountry.ExportCod = DTOInvoice.ExportCods.intracomunitari Then
            retval = New SoapFacturasRecibidas.IDOtroType
            With retval
                .CodigoPais = DirectCast([Enum].Parse(GetType(SoapFacturasRecibidas.CountryType2), oCountry.ISO), SoapFacturasRecibidas.CountryType2)
                .CodigoPaisSpecified = True
                .ID = value.contact.PrimaryNifValue()
                If value.contact.IsEstrangerResident() Then 'cas especial de Windeln, empressa Alemanya amb residencia i Nif a Espanya
                    .IDType = SoapFacturasRecibidas.PersonaFisicaJuridicaIDTypeType.Item05 'DOCUMENTO OFICIAL IDENT. PAIS RESIDENCIA (canviat 25/3/21 de Item04 a item05 d'acord amb la Sole
                Else
                    .IDType = SoapFacturasRecibidas.PersonaFisicaJuridicaIDTypeType.Item02 'NIF
                End If
            End With
        Else
            Dim oPais = DirectCast([Enum].Parse(GetType(SoapFacturasRecibidas.CountryType2), oCountry.ISO), SoapFacturasRecibidas.CountryType2)
            If oPais = CountryType2.ES Then
                'Canarias, Ceuta o Melilla com si fos nacional
                retval = value.contact.PrimaryNifValue()
            Else
                retval = New SoapFacturasRecibidas.IDOtroType
                With retval
                    .CodigoPais = DirectCast([Enum].Parse(GetType(SoapFacturasRecibidas.CountryType2), oCountry.ISO), SoapFacturasRecibidas.CountryType2)
                    .CodigoPaisSpecified = True
                    .IDType = SoapFacturasRecibidas.PersonaFisicaJuridicaIDTypeType.Item04 'DOCUMENTO OFICIAL IDENT. PAIS RESIDENCIA
                    .ID = value.contact.PrimaryNifValue()
                End With
            End If
        End If
        Return retval
    End Function



    Shared Function TipoFactura(value As DTOBookFra) As ClaveTipoFacturaType
        Dim retval As ClaveTipoFacturaType
        Select Case value.TipoFra
            Case "F1"
                retval = ClaveTipoFacturaType.F1
            Case "F2"
                retval = ClaveTipoFacturaType.F2
            Case "F3"
                retval = ClaveTipoFacturaType.F3
            Case "F4"
                retval = ClaveTipoFacturaType.F4
            Case "F5"
                retval = ClaveTipoFacturaType.F5
            Case "F6"
                retval = ClaveTipoFacturaType.F6
            Case "R1"
                retval = ClaveTipoFacturaType.R1
            Case "R2"
                retval = ClaveTipoFacturaType.R2
            Case "R3"
                retval = ClaveTipoFacturaType.R3
        End Select
        Return retval
    End Function

    Shared Function DesgloseFactura(value As DTOBookFra, oTaxes As List(Of DTOTax)) As DesgloseFacturaRecibidasType
        Dim tipoIvas As New List(Of DetalleIVARecibidaType)

        If value.ClaveRegimenEspecialOTrascendencia = "09" Then 'intracomunitari
            Dim oTaxIva = oTaxes.FirstOrDefault(Function(x) x.Codi = DTOTax.Codis.Iva_Standard)
            If oTaxIva IsNot Nothing Then
                Dim dcTaxIvaTipus As Decimal = oTaxIva.Tipus
                Dim tipoIva As DetalleIVARecibidaType = New DetalleIVARecibidaType()

                tipoIva.BaseImponible = FormatNumber(value.BaseDevengada)
                tipoIva.TipoImpositivo = FormatNumber(dcTaxIvaTipus)
                tipoIva.CuotaSoportada = FormatNumber(value.BaseDevengada.Eur * dcTaxIvaTipus / 100)
                tipoIvas.Add(tipoIva)

            End If
        Else
            For Each item As DTOBaseQuota In value.IvaBaseQuotas
                Dim tipoIva As DetalleIVARecibidaType = New DetalleIVARecibidaType()
                tipoIva.BaseImponible = FormatNumber(item.baseImponible)
                tipoIva.TipoImpositivo = FormatNumber(item.tipus)
                tipoIva.CuotaSoportada = FormatNumber(item.quota)
                tipoIvas.Add(tipoIva)
            Next
        End If

        Dim retval As New DesgloseFacturaRecibidasType
        retval.DesgloseIVA = tipoIvas.ToArray
        Return retval
    End Function


    Shared Function Cabecera() As CabeceraSii
        Dim retval As New CabeceraSii
        With retval
            .Titular = New PersonaFisicaJuridicaESType
            With .Titular
                .NIF = EMISOR_NIF
                .NombreRazon = EMISOR_NOM
            End With
            .TipoComunicacion = ClaveTipoComunicacionType.A0
            .IDVersionSii = VersionSiiType.Item11
            ' .IDVersionSii = VersionSiiType.Item07
        End With
        Return retval
    End Function

    Shared Function PeriodoImpositivo(DtFch As Date) As RegistroSiiPeriodoLiquidacion
        Dim retval As New RegistroSiiPeriodoLiquidacion
        With retval
            .Ejercicio = DtFch.Year
            .Periodo = DtFch.Month - 1
        End With
        Return retval
    End Function

    Shared Function CurrentWebService(entorno As Defaults.Entornos, oCert As Security.Cryptography.X509Certificates.X509Certificate2) As siiSOAPClient
        Dim retval As siiSOAPClient = Nothing
        Select Case entorno
            Case Defaults.Entornos.Produccion
                retval = New siiSOAPClient("SuministroFactRecibidas")
            Case Defaults.Entornos.Pruebas
                retval = New siiSOAPClient("SuministroFactRecibidasPruebas")
        End Select

        With retval
            .ClientCredentials.ClientCertificate.Certificate = oCert
            .ClientCredentials.UseIdentityConfiguration = True
        End With
        Return retval
    End Function

    Shared Function ConsultaRecibidas(entorno As Defaults.Entornos,
                                      oCert As Security.Cryptography.X509Certificates.X509Certificate2,
                                      oExercici As DTOExercici,
                                      periodo As Integer,
                                      exs As List(Of Exception)) As RegistroRespuestaConsultaRecibidasType()

        Dim oLRConsultaRecibidas As New SoapFacturasRecibidas.LRConsultaRecibidasType()
        With oLRConsultaRecibidas
            .Cabecera = New CabeceraConsultaSii()
            .Cabecera.IDVersionSii = VersionSiiType.Item11
            .Cabecera.Titular = New PersonaFisicaJuridicaUnicaESType
            .Cabecera.Titular.NIF = oExercici.Emp.Org.PrimaryNifValue()
            .Cabecera.Titular.NombreRazon = oExercici.Emp.Org.nom
            .FiltroConsulta = New LRFiltroRecibidasType
            .FiltroConsulta.PeriodoLiquidacion = New RegistroSiiPeriodoLiquidacion
            .FiltroConsulta.PeriodoLiquidacion.Ejercicio = oExercici.Year
            .FiltroConsulta.PeriodoLiquidacion.Periodo = periodo - 1
        End With

        'Serialize(oLRConsultaRecibidas, My.Computer.FileSystem.SpecialDirectories.MyDocuments & "\_tmp\request.xml") 'Opcional: per debug crea un fitxer amb el resultat

        Dim Ws As siiSOAPClient = CurrentWebService(entorno, oCert)
        Dim resp As RespuestaConsultaLRFacturasRecibidasType = Ws.ConsultaLRFacturasRecibidas(oLRConsultaRecibidas)
        'Serialize(resp, My.Computer.FileSystem.SpecialDirectories.MyDocuments & "\_tmp\response.xml") 'Opcional: per debug crea un fitxer amb el resultat
        Dim retval As RegistroRespuestaConsultaRecibidasType() = resp.RegistroRespuestaConsultaLRFacturasRecibidas
        Return retval
    End Function

    Shared Function QuerySiiLog(entorno As Defaults.Entornos, oCert As Security.Cryptography.X509Certificates.X509Certificate2, oOrg As DTOContact, ByRef oBookFra As DTOBookFra, exs As List(Of Exception)) As DTOSiiLog
        Dim retval As DTOSiiLog = Nothing

        Dim sNif As String = oBookFra.contact.PrimaryNifValue()
        Dim sFraNum As String = oBookFra.FraNum
        Dim oSiiLogs = Consulta(entorno, oCert, oOrg, oBookFra.YearMonth).
            Where(Function(x) x.Nif = sNif And x.FraNum = sFraNum).
            ToList

        Select Case oSiiLogs.Count
            Case 0
                exs.Add(New Exception("Nif: " & sNif & " factura " & sFraNum & " no trobada"))
            Case 1
                retval = oSiiLogs.First
            Case Else
                exs.Add(New Exception("trobades " & oSiiLogs.Count & " factures amb el Nif: " & sNif & " i numero " & sFraNum & " "))
        End Select

        Return retval
    End Function

    Shared Function Consulta(entorno As Defaults.Entornos, oCert As Security.Cryptography.X509Certificates.X509Certificate2, oOrg As DTOContact, oYearMonth As DTOYearMonth) As List(Of DTOSiiLog)
        Dim retval As New List(Of DTOSiiLog)

        Dim oLRConsultaRecibidas As New SoapFacturasRecibidas.LRConsultaRecibidasType()
        With oLRConsultaRecibidas
            .Cabecera = New CabeceraConsultaSii()
            .Cabecera.IDVersionSii = VersionSiiType.Item11
            .Cabecera.Titular = New PersonaFisicaJuridicaUnicaESType
            .Cabecera.Titular.NIF = oOrg.PrimaryNifValue()
            .Cabecera.Titular.NombreRazon = oOrg.nom
            .FiltroConsulta = New LRFiltroRecibidasType
            .FiltroConsulta.PeriodoLiquidacion = New RegistroSiiPeriodoLiquidacion
            .FiltroConsulta.PeriodoLiquidacion.Ejercicio = oYearMonth.year
            .FiltroConsulta.PeriodoLiquidacion.Periodo = oYearMonth.month - 1

            '.FiltroConsulta.PeriodoLiquidacion.Periodo = New TipoPeriodoType
            '.FiltroConsulta.PeriodoLiquidacion.Periodo = TipoPeriodoType.Item07
        End With

        Dim resp As New RespuestaConsultaLRFacturasRecibidasType
        Dim Ws As siiSOAPClient = CurrentWebService(entorno, oCert)
        resp = Ws.ConsultaLRFacturasRecibidas(oLRConsultaRecibidas)

        For Each o In resp.RegistroRespuestaConsultaLRFacturasRecibidas
            Dim oSiiLog As New DTOSiiLog

            With oSiiLog
                Select Case o.EstadoFactura.EstadoRegistro
                    Case EstadoRegistroSIIType.Correcta
                        .Result = DTOInvoice.SiiResults.Correcto
                        .ErrMsg = ""
                    Case EstadoRegistroSIIType.AceptadaConErrores
                        .Result = DTOInvoice.SiiResults.AceptadoConErrores
                        .ErrMsg = String.Format("err.{0} {1}", o.EstadoFactura.CodigoErrorRegistro, o.EstadoFactura.DescripcionErrorRegistro)
                    Case EstadoRegistroSIIType.Anulada
                        .Result = DTOInvoice.SiiResults.Incorrecto
                        .ErrMsg = String.Format("err.{0} {1}", o.EstadoFactura.CodigoErrorRegistro, o.EstadoFactura.DescripcionErrorRegistro)
                End Select
                .Nif = Nif(o)
                .FraNum = o.IDFactura.NumSerieFacturaEmisor
                .Csv = o.DatosPresentacion.CSV
                .Fch = o.DatosPresentacion.TimestampPresentacion
            End With

            retval.Add(oSiiLog)
        Next
        Return retval
    End Function


    Shared Function Nif(oFactura As SoapFacturasRecibidas.RegistroRespuestaConsultaRecibidasType) As String
        Dim retval As String
        Dim oEmisor As IDFacturaRecibidaTypeIDEmisorFactura = oFactura.IDFactura.IDEmisorFactura
        If TypeOf oEmisor.Item Is IDOtroType Then
            Dim oIDOtroType As IDOtroType = oEmisor.Item
            retval = oIDOtroType.ID
        Else
            retval = oEmisor.Item
        End If
        Return retval
    End Function

    Shared Function DatosPresentacion(entorno As Defaults.Entornos, oCert As Security.Cryptography.X509Certificates.X509Certificate2, oExercici As DTOExercici, periodo As Integer, exs As List(Of Exception)) As List(Of DatosPresentacion2Type)
        Dim oFacturasRecibidas As AeatSii.SoapFacturasRecibidas.RegistroRespuestaConsultaRecibidasType() = AeatSii.FacturasRecibidas.ConsultaRecibidas(entorno, oCert, oExercici, periodo, exs)
        Dim retval As List(Of DatosPresentacion2Type) = oFacturasRecibidas.
            GroupBy(Function(g) New With {Key g.DatosPresentacion.CSV, Key g.DatosPresentacion.TimestampPresentacion, Key g.DatosPresentacion.NIFPresentador}).
            Select(Function(group) New DatosPresentacion2Type With {.CSV = group.Key.CSV, .TimestampPresentacion = group.Key.TimestampPresentacion, .NIFPresentador = group.Key.NIFPresentador}).ToList
        Return retval
    End Function
End Class
