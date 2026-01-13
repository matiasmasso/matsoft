Imports AeatSii.SoapFacturasEmitidas

Public Class FacturasEmitidas
    Public Const EMISOR_NOM As String = "MATIAS MASSO, S.A."
    Public Const EMISOR_NIF As String = "A58007857"

    Private Enum TiposDesglose
        Factura
        Operacion
    End Enum


    Shared Function Send(entorno As Defaults.Entornos, oInvoices As List(Of DTOInvoice), oCert As Security.Cryptography.X509Certificates.X509Certificate2, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean
        If oInvoices.Count > 0 Then

            Dim resp As New RespuestaLRFEmitidasType
            Dim Ws As siiSOAPClient = CurrentWebService(oCert)
            Dim oSuministroLRFacturasEmitidas As SuministroLRFacturasEmitidas = FromInvoices(oInvoices, exs)

            Serialize(oSuministroLRFacturasEmitidas, My.Computer.FileSystem.SpecialDirectories.MyDocuments & "\tmp\serialXML_request.xml") 'Opcional: per debug crea un fitxer amb el resultat

            Dim oInvoice As DTOInvoice = Nothing
            Try
                resp = Ws.SuministroLRFacturasEmitidas(oSuministroLRFacturasEmitidas)
                Dim DtNow As Date = DateTime.Now()

                Serialize(resp, My.Computer.FileSystem.SpecialDirectories.MyDocuments & "\tmp\serialXML_response.xml") 'Opcional: per debug crea un fitxer amb el resultat

                For Each o As RespuestaExpedidaType In resp.RespuestaLinea
                    Dim sFch As String = o.IDFactura.FechaExpedicionFacturaEmisor
                    Dim DtFch As Date = DateTime.Parse(sFch, System.Globalization.CultureInfo.GetCultureInfo("es-ES"))
                    oInvoice = oInvoices.FirstOrDefault(Function(x) x.fch.Date = o.IDFactura.FechaExpedicionFacturaEmisor And NumeroYSerie(x) = o.IDFactura.NumSerieFacturaEmisor)
                    If oInvoice IsNot Nothing Then
                        Select Case o.EstadoRegistro
                            Case EstadoRegistroType.Correcto
                                oInvoice.siiLog = New DTOSiiLog
                                With oInvoice.siiLog
                                    .Result = DTOSiiLog.Results.Correcto
                                    .Fch = DtNow
                                    .ErrMsg = ""
                                    .Csv = IIf(o.CSV > "", o.CSV, resp.CSV)
                                End With
                            Case EstadoRegistroType.AceptadoConErrores
                                oInvoice.siiLog = New DTOSiiLog
                                With oInvoice.siiLog
                                    .Result = DTOSiiLog.Results.Parcialmente_Correcto
                                    .Fch = DtNow
                                    .ErrMsg = String.Format("error {0}. {1}", o.CodigoErrorRegistro, o.DescripcionErrorRegistro)
                                    .Csv = IIf(o.CSV > "", o.CSV, resp.CSV)
                                End With
                                Dim sMsg As String = String.Format("Factura {0}: {1}", o.IDFactura.NumSerieFacturaEmisor, oInvoice.siiLog.ErrMsg)
                                exs.Add(New Exception(sMsg))
                            Case EstadoRegistroType.Incorrecto
                                If o.CodigoErrorRegistro = 3000 Then 'duplicada
                                    exs.Add(New Exception("factura " & oInvoice.num & " duplicada"))
                                    oInvoice.siiLog = Nothing
                                Else
                                    oInvoice.siiLog = New DTOSiiLog
                                    With oInvoice.siiLog
                                        .Result = DTOSiiLog.Results.Incorrecto
                                        .Fch = DtNow
                                        .ErrMsg = String.Format("error {0}. {1}", o.CodigoErrorRegistro, o.DescripcionErrorRegistro)
                                        .Csv = ""
                                    End With
                                    Dim sMsg As String = String.Format("Factura {0}: {1}", o.IDFactura.NumSerieFacturaEmisor, oInvoice.siiLog.ErrMsg)
                                    exs.Add(New Exception(sMsg))
                                End If
                        End Select
                    End If
                Next

            Catch ex As Exception
                Dim sMsg As String = String.Format("AeatSii.FacturasEmitidas.Send {0}", ex.Message)
                ex = New Exception(sMsg)
                exs.Add(ex)
            End Try

        Else
            exs.Add(New Exception("No hi han factures per enviar"))
        End If
        retval = True
        Return retval
    End Function

    Shared Function FromInvoices(oInvoices As List(Of DTOInvoice), exs As List(Of Exception)) As SoapFacturasEmitidas.SuministroLRFacturasEmitidas
        Dim retval As New SuministroLRFacturasEmitidas
        retval.Cabecera = New CabeceraSii
        retval.Cabecera.Titular = New PersonaFisicaJuridicaESType
        retval.Cabecera.Titular.NIF = EMISOR_NIF
        retval.Cabecera.Titular.NombreRazon = EMISOR_NOM
        retval.Cabecera.TipoComunicacion = ClaveTipoComunicacionType.A0
        retval.Cabecera.IDVersionSii = VersionSiiType.Item11
        retval.RegistroLRFacturasEmitidas = {}
        For Each oInvoice As DTOInvoice In oInvoices
            Dim oFacturaEmitida As New LRfacturasEmitidasType

            'V1.0:
            'oFacturaEmitida.PeriodoImpositivo = New RegistroSiiPeriodoImpositivo
            'oFacturaEmitida.PeriodoImpositivo.Ejercicio = oInvoice.Fch.Year
            'oFacturaEmitida.PeriodoImpositivo.Periodo = oInvoice.Fch.Month - 1

            'V1.1:
            oFacturaEmitida.PeriodoLiquidacion = New RegistroSiiPeriodoLiquidacion
            oFacturaEmitida.PeriodoLiquidacion.Ejercicio = oInvoice.Fch.Year
            oFacturaEmitida.PeriodoLiquidacion.Periodo = oInvoice.Fch.Month - 1

            oFacturaEmitida.IDFactura = New IDFacturaExpedidaType
            oFacturaEmitida.IDFactura.IDEmisorFactura = New IDFacturaExpedidaTypeIDEmisorFactura
            oFacturaEmitida.IDFactura.IDEmisorFactura.NIF = EMISOR_NIF
            oFacturaEmitida.IDFactura.NumSerieFacturaEmisor = NumeroYSerie(oInvoice)
            oFacturaEmitida.IDFactura.FechaExpedicionFacturaEmisor = FormatFch(oInvoice.Fch)

            oFacturaEmitida.FacturaExpedida = New FacturaExpedidaType
            oFacturaEmitida.FacturaExpedida.TipoFactura = ClaveTipoFacturaType(oInvoice, exs)
            oFacturaEmitida.FacturaExpedida.TipoRectificativa = ClaveTipoRectificativa(oInvoice, exs)
            oFacturaEmitida.FacturaExpedida.TipoRectificativaSpecified = oFacturaEmitida.FacturaExpedida.TipoRectificativa > 0
            oFacturaEmitida.FacturaExpedida.FechaOperacion = FormatFch(oInvoice.Deliveries.First.Fch)
            oFacturaEmitida.FacturaExpedida.ClaveRegimenEspecialOTrascendencia = ClaveRegimenEspecialOTrascendencia(oInvoice, exs)
            oFacturaEmitida.FacturaExpedida.ImporteTotal = FormatNumber(oInvoice.Total)
            oFacturaEmitida.FacturaExpedida.DescripcionOperacion = oInvoice.Concepte.ToString

            If oInvoice.Serie <> DTOInvoice.Series.simplificada And oInvoice.TipoFactura <> "R5" Then
                oFacturaEmitida.FacturaExpedida.Contraparte = New PersonaFisicaJuridicaType
                oFacturaEmitida.FacturaExpedida.Contraparte.NombreRazon = Left(oInvoice.Nom, 40)
                Select Case ExportCod(oInvoice)
                    Case DTOInvoice.ExportCods.nacional
                        If oInvoice.IsEstrangerResident() Then 'cas especial de Windeln, empressa Alemanya amb residencia i Nif a Espanya
                            oFacturaEmitida.FacturaExpedida.Contraparte.Item = New IDOtroType
                            oFacturaEmitida.FacturaExpedida.Contraparte.Item.CodigoPais = CodigoPais(oInvoice)
                            oFacturaEmitida.FacturaExpedida.Contraparte.Item.CodigoPaisSpecified = True
                            oFacturaEmitida.FacturaExpedida.Contraparte.Item.IDType = PersonaFisicaJuridicaIDTypeType.Item05 'DOCUMENTO OFICIAL IDENT. PAIS RESIDENCIA (canviat 25/3/21 de Item04 a item05 d'acord amb la Sole
                            oFacturaEmitida.FacturaExpedida.Contraparte.Item.ID = Nif(oInvoice)
                        Else
                            oFacturaEmitida.FacturaExpedida.Contraparte.Item = Nif(oInvoice)
                        End If
                    Case DTOInvoice.ExportCods.intracomunitari
                        oFacturaEmitida.FacturaExpedida.Contraparte.Item = New IDOtroType

                        'modified 14/11/22 for MC INTERNATIONAL TRADE, S.A.U. PORTUGAL
                        'oFacturaEmitida.FacturaExpedida.Contraparte.Item.CodigoPais = CodigoPais(oInvoice)
                        'oFacturaEmitida.FacturaExpedida.Contraparte.Item.CodigoPaisSpecified = True
                        oFacturaEmitida.FacturaExpedida.Contraparte.Item.CodigoPaisSpecified = False

                        oFacturaEmitida.FacturaExpedida.Contraparte.Item.IDType = PersonaFisicaJuridicaIDTypeType.Item02 'NIF
                        oFacturaEmitida.FacturaExpedida.Contraparte.Item.ID = Nif(oInvoice)
                    Case DTOInvoice.ExportCods.extracomunitari
                        Dim oPais = CodigoPais(oInvoice)
                        If oPais = CountryType2.ES Then
                            'Canarias, Ceuta o Melilla com si fos nacional
                            oFacturaEmitida.FacturaExpedida.Contraparte.Item = Nif(oInvoice)
                        Else
                            oFacturaEmitida.FacturaExpedida.Contraparte.Item = New IDOtroType
                            oFacturaEmitida.FacturaExpedida.Contraparte.Item.CodigoPais = oPais
                            oFacturaEmitida.FacturaExpedida.Contraparte.Item.CodigoPaisSpecified = True
                            oFacturaEmitida.FacturaExpedida.Contraparte.Item.IDType = PersonaFisicaJuridicaIDTypeType.Item04 'DOCUMENTO OFICIAL IDENT. PAIS RESIDENCIA
                            oFacturaEmitida.FacturaExpedida.Contraparte.Item.ID = Nif(oInvoice)
                        End If
                End Select
            End If

            oFacturaEmitida.FacturaExpedida.TipoDesglose = New FacturaExpedidaTypeTipoDesglose
            Select Case TipoDesglose(oInvoice)
                Case TiposDesglose.Factura
                    oFacturaEmitida.FacturaExpedida.TipoDesglose.Item = New TipoSinDesgloseType
                    oFacturaEmitida.FacturaExpedida.TipoDesglose.Item.Sujeta = New SujetaType

                    Select Case oInvoice.TipoSujeccionIva
                        Case DTOInvoice.TiposSujeccionIva.SujetoExento
                            Dim oExentas As New List(Of DetalleExentaType)
                            oExentas.Add(New DetalleExentaType With {.BaseImponible = FormatNumber(oInvoice.BaseImponible.Eur), .CausaExencion = CausaExencionIva(oInvoice, exs)})
                            oFacturaEmitida.FacturaExpedida.TipoDesglose.Item.Sujeta.Exenta = oExentas.ToArray

                        Case DTOInvoice.TiposSujeccionIva.SujetoNoExento
                            oFacturaEmitida.FacturaExpedida.TipoDesglose.Item.Sujeta.NoExenta = New SujetaTypeNoExenta
                            oFacturaEmitida.FacturaExpedida.TipoDesglose.Item.Sujeta.NoExenta.TipoNoExenta = TipoOperacionSujetaNoExentaType.S1
                            oFacturaEmitida.FacturaExpedida.TipoDesglose.Item.Sujeta.NoExenta.DesgloseIVA = DesgloseIVA(oInvoice).ToArray
                    End Select

                Case TiposDesglose.Operacion

                    oFacturaEmitida.FacturaExpedida.TipoDesglose.Item = New TipoConDesgloseType
                    oFacturaEmitida.FacturaExpedida.TipoDesglose.Item.Entrega = New TipoSinDesgloseType
                    oFacturaEmitida.FacturaExpedida.TipoDesglose.Item.Entrega.Sujeta = New SujetaType

                    Select Case oInvoice.TipoSujeccionIva
                        Case DTOInvoice.TiposSujeccionIva.SujetoExento
                            Dim oExentas As New List(Of DetalleExentaType)
                            oExentas.Add(New DetalleExentaType With {.BaseImponible = FormatNumber(oInvoice.BaseImponible.Eur), .CausaExencion = CausaExencionIva(oInvoice, exs), .CausaExencionSpecified = True})
                            oFacturaEmitida.FacturaExpedida.TipoDesglose.Item.Entrega.Sujeta.Exenta = oExentas.ToArray

                        Case DTOInvoice.TiposSujeccionIva.SujetoNoExento
                            oFacturaEmitida.FacturaExpedida.TipoDesglose.Item.Entrega.Sujeta.NoExenta = New SujetaTypeNoExenta
                            oFacturaEmitida.FacturaExpedida.TipoDesglose.Item.Entrega.Sujeta.NoExenta.TipoNoExenta = TipoOperacionSujetaNoExentaType.S1
                            oFacturaEmitida.FacturaExpedida.TipoDesglose.Item.Entrega.Sujeta.NoExenta.DesgloseIVA = DesgloseIVA(oInvoice).ToArray
                    End Select

            End Select

            AddFactura(retval.RegistroLRFacturasEmitidas, oFacturaEmitida)
        Next

        Return retval
    End Function

    Private Shared Sub AddFactura(ByRef oFacturas As LRfacturasEmitidasType(), oFactura As LRfacturasEmitidasType)
        Dim index As Integer = oFacturas.Length
        ReDim Preserve oFacturas(index)
        oFacturas(index) = oFactura
    End Sub

    Shared Function QuerySiiLog(entorno As Defaults.Entornos, oCert As Security.Cryptography.X509Certificates.X509Certificate2, ByRef oInvoice As DTOInvoice) As DTOSiiLog
        Dim retval As DTOSiiLog = Nothing

        Dim oLRConsultaEmitidas As New SoapFacturasEmitidas.LRConsultaEmitidasType()
        With oLRConsultaEmitidas
            .Cabecera = New CabeceraConsultaSii()
            .Cabecera.IDVersionSii = VersionSiiType.Item11
            .Cabecera.Titular = New PersonaFisicaJuridicaUnicaESType
            .Cabecera.Titular.NIF = "A58007857"
            .Cabecera.Titular.NombreRazon = "MATIAS MASSO, S.A."
            .FiltroConsulta = New LRFiltroEmitidasType
            '.FiltroConsulta.ClavePaginacion = New IDFacturaExpedidaBCType
            '.FiltroConsulta.ClavePaginacion.IDEmisorFactura = New IDFacturaExpedidaBCTypeIDEmisorFactura
            '.FiltroConsulta.ClavePaginacion.IDEmisorFactura.NIF = "A58007857"
            '.FiltroConsulta.ClavePaginacion.NumSerieFacturaEmisor = oInvoice.Num
            '.FiltroConsulta.ClavePaginacion.FechaExpedicionFacturaEmisor = FormatFch(oInvoice.Fch)
            .FiltroConsulta.PeriodoLiquidacion = New RegistroSiiPeriodoLiquidacion
            .FiltroConsulta.PeriodoLiquidacion.Ejercicio = oInvoice.fch.Year.ToString
            .FiltroConsulta.PeriodoLiquidacion.Periodo = New TipoPeriodoType
            .FiltroConsulta.PeriodoLiquidacion.Periodo = oInvoice.fch.Month - 1 ' TipoPeriodoType.Item07
            '.FiltroConsulta.Contraparte = New ContraparteConsultaType
        End With

        Dim resp As New RespuestaConsultaLRFacturasEmitidasType
        Dim Ws As siiSOAPClient = CurrentWebService(oCert)
        resp = Ws.ConsultaLRFacturasEmitidas(oLRConsultaEmitidas)
        Serialize(oLRConsultaEmitidas, My.Computer.FileSystem.SpecialDirectories.MyDocuments & "\_tmp\serialXML_request.xml") 'Opcional: per debug crea un fitxer amb el resultat
        Serialize(resp, My.Computer.FileSystem.SpecialDirectories.MyDocuments & "\_tmp\serialXML_response.xml") 'Opcional: per debug crea un fitxer amb el resultat

        If resp.RegistroRespuestaConsultaLRFacturasEmitidas IsNot Nothing Then
            Dim sInvoiceNum As String = oInvoice.num
            If oInvoice.serie = DTOInvoice.Series.Rectificativa Then
                sInvoiceNum = String.Format("R{0}", oInvoice.num)
            ElseIf oInvoice.serie = DTOInvoice.Series.simplificada Then
                sInvoiceNum = String.Format("S{0}", oInvoice.num)
            End If

            Dim o As RegistroRespuestaConsultaEmitidasType = resp.RegistroRespuestaConsultaLRFacturasEmitidas.FirstOrDefault(Function(x) x.IDFactura.NumSerieFacturaEmisor = sInvoiceNum)
            If o IsNot Nothing Then
                retval = New DTOSiiLog
                With retval
                    .Entorno = entorno
                    .Csv = o.DatosPresentacion.CSV
                    .Fch = o.DatosPresentacion.TimestampPresentacion
                    .TipoDeComunicacion = "A0"
                    .Contingut = DTOSiiLog.Continguts.Facturas_Emitidas
                    '.Result = DTOSiiLog.Results.Correcto
                End With

                Select Case o.EstadoFactura.EstadoRegistro
                    Case EstadoRegistroSIIType.Correcta
                        retval.Result = DTOInvoice.SiiResults.Correcto
                        retval.ErrMsg = ""
                    Case EstadoRegistroSIIType.AceptadaConErrores
                        retval.Result = DTOInvoice.SiiResults.AceptadoConErrores
                        retval.ErrMsg = String.Format("err.{0} {1}", o.EstadoFactura.CodigoErrorRegistro, o.EstadoFactura.DescripcionErrorRegistro)
                    Case EstadoRegistroSIIType.Anulada
                        retval.Result = DTOInvoice.SiiResults.Incorrecto
                        retval.ErrMsg = String.Format("err.{0} {1}", o.EstadoFactura.CodigoErrorRegistro, o.EstadoFactura.DescripcionErrorRegistro)
                End Select

                oInvoice.siiLog = retval
            End If
        End If

        Return retval
    End Function


    Shared Function NumeroYSerie(oInvoice As DTOInvoice) As String
        Dim retval As String = ""
        Select Case oInvoice.serie
            Case DTOInvoice.Series.rectificativa
                retval = String.Format("R{0}", oInvoice.num)
            Case DTOInvoice.Series.simplificada
                retval = String.Format("S{0}", oInvoice.num)
            Case Else
                retval = oInvoice.Num
        End Select
        Return retval
    End Function

    Private Shared Function Nif(oInvoice As DTOInvoice) As String
        Dim retval As String = oInvoice.Nifs.PrimaryNifValue()
        If ExportCod(oInvoice) = DTOInvoice.ExportCods.Nacional Then
            If retval.Length = 11 AndAlso retval.StartsWith("ES") Then
                retval = retval.Substring(2)
            End If
        End If
        Return retval
    End Function

    Private Shared Function Country(oInvoice As DTOInvoice) As DTOCountry
        Dim retval As DTOCountry = oInvoice.Zip.Location.Zona.Country
        Return retval
    End Function

    Private Shared Function CodigoPais(oInvoice As DTOInvoice) As CountryType2
        Dim oCountry As DTOCountry = Country(oInvoice)
        Dim retval As CountryType2 = [Enum].Parse(GetType(SoapFacturasEmitidas.CountryType2), oCountry.ISO)
        Return retval
    End Function

    Private Shared Function ExportCod(oInvoice As DTOInvoice) As DTOInvoice.ExportCods
        Return oInvoice.ExportCod
        'Dim retval As DTOInvoice.ExportCods = DTOInvoice.ExportCods.notSet
        'Dim oCountry As DTOCountry = Country(oInvoice)
        'If oCountry.ISO = "ES" Then
        '' retval = DTOInvoice.ExportCods.nacional
        'ElseIf oCountry.ExportCod = DTOInvoice.ExportCods.intracomunitari Then
        ' If oInvoice.Customer.PrimaryNifValue().StartsWith("ESN") Then
        ' retval = DTOInvoice.ExportCods.nacional 'No Residents
        ' Else
        ' retval = DTOInvoice.ExportCods.intracomunitari
        ' End If
        ' Else
        ' retval = DTOInvoice.ExportCods.extracomunitari
        ' End If
        ' Return retval
    End Function

    Private Shared Function CausaExencionIva(oInvoice As DTOInvoice, exs As List(Of Exception)) As CausaExencionType
        Dim retval As CausaExencionType = -1
        If oInvoice.SiiL9 = "" Then
            exs.Add(New Exception(String.Format("Falta codi exenció IVA en factura {0} de {1}", oInvoice.Num, oInvoice.Customer.Nom)))
        Else
            retval = [Enum].Parse(GetType(CausaExencionType), oInvoice.SiiL9)
        End If
        Return retval
    End Function

    Private Shared Function TipoDesglose(oInvoice As DTOInvoice) As TiposDesglose
        Dim retval As TiposDesglose
        If oInvoice.TipoSujeccionIva = DTOInvoice.TiposSujeccionIva.SujetoNoExento Then
            If oInvoice.Customer.PrimaryNifValue().StartsWith("ESN") Then
                retval = TiposDesglose.Operacion
            Else
                retval = TiposDesglose.Factura
            End If
        Else
            retval = TiposDesglose.Operacion
        End If
        Return retval
    End Function


    Shared Function DesgloseIVA(oInvoice As DTOInvoice) As List(Of DetalleIVAEmitidaType)
        Dim retval As New List(Of SoapFacturasEmitidas.DetalleIVAEmitidaType)
        For Each oIvaBaseQuota As DTOTaxBaseQuota In oInvoice.IvaBaseQuotas
            Select Case oIvaBaseQuota.Tax.Codi
                Case DTOTax.Codis.Iva_Standard
                    Dim oReqBaseQuota As DTOTaxBaseQuota = oInvoice.IvaBaseQuotas.FirstOrDefault(Function(x) x.Tax.Codi = DTOTax.Codis.Recarrec_Equivalencia_Standard)
                    retval.Add(DetalleIva(oIvaBaseQuota, oReqBaseQuota))
                Case DTOTax.Codis.Iva_Reduit
                    Dim oReqBaseQuota As DTOTaxBaseQuota = oInvoice.IvaBaseQuotas.FirstOrDefault(Function(x) x.Tax.Codi = DTOTax.Codis.Recarrec_Equivalencia_Reduit)
                    retval.Add(DetalleIva(oIvaBaseQuota, oReqBaseQuota))
                Case DTOTax.Codis.Iva_SuperReduit
                    Dim oReqBaseQuota As DTOTaxBaseQuota = oInvoice.IvaBaseQuotas.FirstOrDefault(Function(x) x.Tax.Codi = DTOTax.Codis.Recarrec_Equivalencia_SuperReduit)
                    retval.Add(DetalleIva(oIvaBaseQuota, oReqBaseQuota))
            End Select
        Next
        Return retval
    End Function

    Shared Function DetalleIva(oIvaBaseQuota As DTOTaxBaseQuota, Optional oReqBaseQuota As DTOTaxBaseQuota = Nothing) As SoapFacturasEmitidas.DetalleIVAEmitidaType
        Dim retval As New SoapFacturasEmitidas.DetalleIVAEmitidaType
        With retval
            .TipoImpositivo = FormatNumber(oIvaBaseQuota.tax.tipus)
            .BaseImponible = FormatNumber(oIvaBaseQuota.baseImponible)
            .CuotaRepercutida = FormatNumber(oIvaBaseQuota.quota)
            If oReqBaseQuota Is Nothing Then
                '.TipoRecargoEquivalencia = 0
                '.CuotaRecargoEquivalencia = 0
            Else
                .TipoRecargoEquivalencia = FormatNumber(oReqBaseQuota.Tax.Tipus)
                .CuotaRecargoEquivalencia = FormatNumber(oReqBaseQuota.Quota)
            End If
        End With
        Return retval
    End Function


    Shared Function CurrentWebService(oCert As Security.Cryptography.X509Certificates.X509Certificate2) As SoapFacturasEmitidas.siiSOAPClient
        'Dim retval As New SoapFacturasEmitidas.siiSOAPClient("SuministroFactEmitidasProduccion")
        Dim retval As New SoapFacturasEmitidas.siiSOAPClient("SuministroFactEmitidas")
        With retval
            .ClientCredentials.ClientCertificate.Certificate = oCert
            .ClientCredentials.UseIdentityConfiguration = True
        End With
        Return retval
    End Function

    Private Shared Function ClaveRegimenEspecialOTrascendencia(oInvoice As DTOInvoice, exs As List(Of Exception)) As IdOperacionesTrascendenciaTributariaType
        Dim retval As IdOperacionesTrascendenciaTributariaType
        '02= Exportación, 16 = primer semestre 2017

        If Not [Enum].TryParse(Of IdOperacionesTrascendenciaTributariaType)("Item" & oInvoice.RegimenEspecialOTrascendencia, retval) Then
            exs.Add(New Exception("Fra." & oInvoice.Num & ": Clau de Regim Especial o Trascendencia '" & oInvoice.RegimenEspecialOTrascendencia & "' desconeguda."))
        End If
        Return retval
    End Function

    'ClaveTipoFacturaType
    Private Shared Function ClaveTipoFacturaType(oInvoice As DTOInvoice, exs As List(Of Exception)) As ClaveTipoFacturaType
        Dim retval As ClaveTipoFacturaType
        '02= Exportación, 16 = primer semestre 2017

        If Not [Enum].TryParse(oInvoice.TipoFactura, retval) Then
            exs.Add(New Exception("Fra." & oInvoice.Num & ": Tipus de factura '" & oInvoice.TipoFactura & "' desconegut."))
        End If
        Return retval
    End Function

    '.TipoRectificativa = ClaveTipoRectificativaType.I
    Private Shared Function ClaveTipoRectificativa(oInvoice As DTOInvoice, exs As List(Of Exception)) As ClaveTipoRectificativaType
        Dim retval As ClaveTipoRectificativaType

        If oInvoice.Serie = DTOInvoice.Series.Rectificativa Then
            Dim src As String = "I"
            If Not [Enum].TryParse(src, retval) Then
                exs.Add(New Exception("ClaveTipoRectificativa'" & src & "' desconeguda."))
            End If
        End If

        Return retval
    End Function


End Class
