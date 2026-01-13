Imports AeatSii.SoapFacturasEmitidas
Public Class Test

    Shared Sub Test()
        Dim oFacturaEmitida As New LRfacturasEmitidasType

        oFacturaEmitida.PeriodoLiquidacion = New RegistroSiiPeriodoLiquidacion
        oFacturaEmitida.PeriodoLiquidacion.Ejercicio = 2017
        oFacturaEmitida.PeriodoLiquidacion.Periodo = 12

        oFacturaEmitida.IDFactura = New IDFacturaExpedidaType
        oFacturaEmitida.IDFactura.IDEmisorFactura = New IDFacturaExpedidaTypeIDEmisorFactura
        oFacturaEmitida.IDFactura.IDEmisorFactura.NIF = "A58000001"
        oFacturaEmitida.IDFactura.NumSerieFacturaEmisor = "1234"
        oFacturaEmitida.IDFactura.FechaExpedicionFacturaEmisor = "13-12-2017"

        oFacturaEmitida.FacturaExpedida = New FacturaExpedidaType
        oFacturaEmitida.FacturaExpedida.TipoFactura = ClaveTipoFacturaType.F1
        oFacturaEmitida.FacturaExpedida.ClaveRegimenEspecialOTrascendencia = "01"
        oFacturaEmitida.FacturaExpedida.ImporteTotal = "125.50"
        oFacturaEmitida.FacturaExpedida.DescripcionOperacion = "Ventas"

        oFacturaEmitida.FacturaExpedida.Contraparte = New PersonaFisicaJuridicaType
        oFacturaEmitida.FacturaExpedida.Contraparte.Item = New SoapFacturasEmitidas.IDOtroType
        oFacturaEmitida.FacturaExpedida.Contraparte.Item.CodigoPais = CountryType2.ES
        oFacturaEmitida.FacturaExpedida.Contraparte.Item.CodigoPaisSpecified = True
        oFacturaEmitida.FacturaExpedida.Contraparte.Item.IDType = SoapFacturasEmitidas.PersonaFisicaJuridicaIDTypeType.Item02
        oFacturaEmitida.FacturaExpedida.Contraparte.Item.ID = "ESN00000002"

        oFacturaEmitida.FacturaExpedida.TipoDesglose = New FacturaExpedidaTypeTipoDesglose
        oFacturaEmitida.FacturaExpedida.TipoDesglose.Item = New TipoConDesgloseType
        oFacturaEmitida.FacturaExpedida.TipoDesglose.Item.Entrega = New TipoSinDesgloseType
        oFacturaEmitida.FacturaExpedida.TipoDesglose.Item.Entrega.Sujeta = New SujetaType

        oFacturaEmitida.FacturaExpedida.TipoDesglose.Item.Entrega.Sujeta.NoExenta = New SujetaTypeNoExenta
        oFacturaEmitida.FacturaExpedida.TipoDesglose.Item.Entrega.Sujeta.NoExenta.TipoNoExenta = TipoOperacionSujetaNoExentaType.S1

        Dim oDesgloseIva(0) As DetalleIVAEmitidaType
        Dim oDetalleIvaEmitida As New DetalleIVAEmitidaType
        oDetalleIvaEmitida.TipoImpositivo = "21"
        oDetalleIvaEmitida.BaseImponible = "103.72"
        oDetalleIvaEmitida.CuotaRepercutida = "21.78"
        ReDim Preserve oDesgloseIva(oDesgloseIva.Length - 1)
        oDesgloseIva(oDesgloseIva.Length - 1) = oDetalleIvaEmitida
        'oDesgloseIva(0) = oDetalleIvaEmitida
        oFacturaEmitida.FacturaExpedida.TipoDesglose.Item.Entrega.Sujeta.NoExenta.DesgloseIVA = oDesgloseIva

        Dim oSuministroLRFacturasEmitidas As New List(Of LRfacturasEmitidasType)
        oSuministroLRFacturasEmitidas.Add(oFacturaEmitida)

        Serialize(oSuministroLRFacturasEmitidas, My.Computer.FileSystem.SpecialDirectories.MyDocuments & "\_tmp\serialXML_request.xml")
    End Sub

End Class
