Public Class Ediversa_ORDERS_D_96A_UN_EAN008
    Public Enum PaymentCods
        Unknown
        Efectivo = 10
        Cheque = 20
        Banco = 42
        Pagare = 60
    End Enum

    Public Enum TaxCods
        Unknown
        VAT
        IGI
        ENV
        EXT
        ACT
        RE
        RET
        OTH
    End Enum

    Public Enum TdtCods
        Unknown
        Maritimo = 10
        Ferroviario = 20
        Carretera = 30
        Aereo = 40
    End Enum


    Public Enum Portes
        Unknown
        PP 'pagats per el venedor
        PU 'pagats per el comprador
    End Enum

    Public Enum Tipos
        Unknown
        PedidoNormal = 220
    End Enum

    Public Enum Funcions
        Unknown
        Cancelacion = 3
        Original = 9
    End Enum

    Public Enum FTXQualifiers
        Unknown
        DEL
        PUR
        INV
        ZZZ
        AAI
        PAC
        TXD
        GEN
        SUR
        AAB
    End Enum

    Shared Function Factory(oEmp As DTOEmp, oOrder As DTOPurchaseOrder, exs As List(Of Exception)) As String
        FEB2.PurchaseOrder.Load(oOrder, exs)

        Dim sb As New System.Text.StringBuilder
        Dim oNosaltres As DTOContact = oEmp.Org

        Select Case oOrder.cod
            Case DTOPurchaseOrder.Codis.proveidor
                Dim oProveidor As DTOProveidor = oOrder.Proveidor
                FEB2.Contact.Load(oProveidor, exs)

                Dim sObs As String = oOrder.Obs

                Dim oProveidorEAN As DTOEan = oProveidor.GLN
                If oProveidorEAN Is Nothing Then
                    exs.Add(New Exception("falta codi GLN del proveidor " & oProveidor.NomComercialOrDefault()))
                Else
                    Dim oValidationResult = DTOEan.validate(oProveidorEAN)
                    If oValidationResult <> DTOEan.ValidationResults.Ok Then
                        exs.Add(New Exception("Codi GLN no valid del proveidor " & oProveidor.NomComercialOrDefault()))
                    End If
                End If

                Dim oCodTax As TaxCods = TaxCods.EXT
                If oProveidor.Address.Zip.Location.Zona.Country.Equals(oEmp.DefaultCountry) Then
                    oCodTax = TaxCods.VAT
                End If

                Dim oCodTrp As TdtCods = TdtCods.Carretera
                Dim oIncoterm = oOrder.Incoterm
                If oIncoterm Is Nothing Then oIncoterm = DTOIncoterm.Factory("EXW")
                Dim oPortes As Portes = Portes.PU

                DTOEdiversaFile.AddSegment(sb, "ORDERS_D_96A_UN_EAN008")
                DTOEdiversaFile.AddSegment(sb, "ORD", DTOPurchaseOrder.FormattedId(oOrder), "220", "9")
                DTOEdiversaFile.addSegment(sb, "DTM", oOrder.fch, oOrder.fchDeliveryMin)
                DTOEdiversaFile.AddSegment(sb, "PAI", 42) 'Pagament per Transferencia

                If sObs > "" Then
                    DTOEdiversaFile.AddSegment(sb, "FTX", "AAI", 0, sObs)
                End If

                DTOEdiversaFile.AddSegment(sb, "NADMS", oNosaltres.GLN.Value) 'Sender
                DTOEdiversaFile.AddSegment(sb, "NADMR", oProveidor.GLN.Value) 'Receiver
                DTOEdiversaFile.AddSegment(sb, "NADSU", oProveidor.GLN.Value) 'Proveedor
                DTOEdiversaFile.AddSegment(sb, "NADBY", oNosaltres.GLN.Value) 'Comprador
                If oOrder.Platform Is Nothing Then
                    DTOEdiversaFile.AddSegment(sb, "NADDP", oNosaltres.GLN.Value) 'ReceptorMercancies
                Else
                    FEB2.Contact.Load(oOrder.Platform, exs)
                    DTOEdiversaFile.AddSegment(sb, "NADDP", oOrder.Platform.GLN.Value) 'ReceptorMercancies
                End If
                DTOEdiversaFile.AddSegment(sb, "NADPW", oProveidor.GLN.Value) 'PuntDeExpedicio
                DTOEdiversaFile.AddSegment(sb, "NADIV", oNosaltres.GLN.Value) 'ReceptorFactura

                'Impostos
                If oCodTax <> TaxCods.Unknown Then
                    If oCodTax = TaxCods.EXT Then
                        DTOEdiversaFile.AddSegment(sb, "TAX", oCodTax.ToString())
                    Else
                        Dim DcPorcentaje, DcImporte, DcBaseImponible As Decimal
                        DTOEdiversaFile.AddSegment(sb, "TAX", oCodTax.ToString, DcPorcentaje, DcImporte, DcBaseImponible)
                    End If
                End If

                'Divisa
                If oOrder.Cur IsNot Nothing Then
                    DTOEdiversaFile.AddSegment(sb, "CUX", oOrder.Cur.Tag)
                End If

                'Transport
                If oCodTrp <> TdtCods.Unknown Then
                    DTOEdiversaFile.AddSegment(sb, "TDT", CInt(oCodTrp))
                End If

                If oIncoterm IsNot Nothing Then
                    DTOEdiversaFile.addSegment(sb, "TOD", oIncoterm.Id.ToString, oPortes.ToString())
                End If

                Dim linIdx As Integer
                Dim totalNet As Decimal
                Dim totalBrut As Decimal

                For Each item In oOrder.Items
                    linIdx += 1
                    If item.Sku.Ean13 Is Nothing Then
                        exs.Add(New Exception(String.Format("article {0} sense codi EAN: {1}", item.sku.id, item.sku.nomLlarg.Esp)))
                    Else
                        DTOEdiversaFile.AddSegment(sb, "LIN", item.Sku.Ean13.Value, "EN", linIdx)
                        DTOEdiversaFile.addSegment(sb, "PIALIN", "IN", item.Sku.Id) 'RefComprador
                        If item.Sku.RefProveidor > "" Then
                            DTOEdiversaFile.addSegment(sb, "PIALIN", "SA", item.Sku.RefProveidor) 'RefProveedor
                        End If
                        DTOEdiversaFile.addSegment(sb, "IMDLIN", "F", "M", "CU", TextHelper.VbLeft(DTOProductSku.nomPrvOrMyd(item.sku), 70)) 'Description
                        DTOEdiversaFile.AddSegment(sb, "QTYLIN", 21, item.Qty, "PCE") 'pedidas
                        If item.ChargeCod = DTOPurchaseOrderItem.ChargeCods.FOC Or item.Price IsNot Nothing AndAlso item.Price.IsZero Then
                            DTOEdiversaFile.AddSegment(sb, "QTYLIN", 192, item.Qty, "PCE") 'GratuitoIncluido
                        End If
                        DTOEdiversaFile.AddSegment(sb, "PRILIN", "AAB", item.Price) 'Preu brut
                        If item.Dto <> 0 Then
                            DTOEdiversaFile.AddSegment(sb, "PRILIN", "AAA", item.PreuNet) ' Preu net
                            If item.Dto <> 0 Then
                                DTOEdiversaFile.AddSegment(sb, "ALCLIN", "A", 1, "TD", "", item.Dto)
                            End If
                        End If
                        DTOEdiversaFile.addSegment(sb, "RFFLIN", "", "", item.Guid.ToString("N")) 'RefLinia
                        DTOEdiversaFile.addSegment(sb, "MOALIN", item.Amount) 'Import
                        totalBrut += item.Qty * item.Price.Val
                        totalNet += item.Amount.Val
                    End If
                Next


                DTOEdiversaFile.AddSegment(sb, "MOARES", totalNet, totalBrut)
                DTOEdiversaFile.AddSegment(sb, "CNTRES", "", "", "", linIdx)
        End Select

        Dim retval As String = sb.ToString
        Return retval

    End Function

End Class

