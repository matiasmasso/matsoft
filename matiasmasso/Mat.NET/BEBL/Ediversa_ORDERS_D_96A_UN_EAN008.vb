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

    Shared Function Factory(oOrder As DTOPurchaseOrder, exs As List(Of Exception)) As String
        PurchaseOrderLoader.Load(oOrder)
        EmpLoader.Load(oOrder.Emp)
        ContactLoader.Load(oOrder.Emp.Org)

        Dim sb As New System.Text.StringBuilder
        Dim oNosaltres As DTOContact = oOrder.Emp.Org

        Select Case oOrder.cod
            Case DTOPurchaseOrder.Codis.proveidor
                Dim oProveidor As DTOProveidor = oOrder.Proveidor
                Contact.Load(oProveidor)

                Dim sObs As String = oOrder.Obs

                Dim oProveidorEAN As DTOEan = oProveidor.GLN
                If oProveidorEAN Is Nothing Then
                    exs.Add(New Exception("falta codi GLN del proveidor " & oProveidor.NomComercialOrDefault()))
                Else
                    Dim oValidationResult As DTOEan.ValidationResults = DTOEan.validate(oProveidorEAN)
                    If oValidationResult <> DTOEan.ValidationResults.Ok Then
                        exs.Add(New Exception("Codi GLN no valid del proveidor " & oProveidor.NomComercialOrDefault()))
                    End If
                End If

                Dim oCodTax As TaxCods = TaxCods.EXT
                If oProveidor.Address.Zip.Location.Zona.Country.Equals(DTOCountry.Wellknown(DTOCountry.Wellknowns.Spain)) Then
                    oCodTax = TaxCods.VAT
                End If

                Dim oCodTrp As TdtCods = TdtCods.Carretera

                Dim oIncoterm = oOrder.Incoterm
                If oIncoterm Is Nothing Then oIncoterm = DTOIncoterm.Factory("EXW")
                Dim oPortes As Portes = Portes.PU

                AddSegment(sb, "ORDERS_D_96A_UN_EAN008")
                AddSegment(sb, "ORD", oOrder.formattedId(), "220", "9")

                If oOrder.fchDeliveryMax = Nothing Then
                    AddSegment(sb, "DTM", oOrder.fch, oOrder.fchDeliveryMin)
                Else
                    AddSegment(sb, "DTM", oOrder.fch, "", "", oOrder.fchDeliveryMax, oOrder.fchDeliveryMin) 'fch doc, fch requested delivery, fch limit (cancel line if not met), fch max delivery, fch min delivery
                End If
                AddSegment(sb, "PAI", 42) 'Pagament per Transferencia

                If sObs > "" Then
                    AddSegment(sb, "FTX", "AAI", 0, sObs)
                End If

                AddSegment(sb, "NADMS", oNosaltres.GLN.Value) 'Sender
                AddSegment(sb, "NADMR", oProveidor.GLN.Value) 'Receiver
                AddSegment(sb, "NADSU", oProveidor.GLN.Value) 'Proveedor
                AddSegment(sb, "NADBY", oNosaltres.GLN.Value) 'Comprador
                If oOrder.DeliverTo Is Nothing Then
                    AddSegment(sb, "NADDP", oNosaltres.GLN.Value) 'ReceptorMercancies
                Else
                    Contact.Load(oOrder.DeliverTo)
                    AddSegment(sb, "NADDP", oOrder.DeliverTo.GLN.Value) 'ReceptorMercancies
                End If
                AddSegment(sb, "NADPW", oProveidor.GLN.Value) 'PuntDeExpedicio
                AddSegment(sb, "NADIV", oNosaltres.GLN.Value) 'ReceptorFactura

                'Impostos
                If oCodTax <> TaxCods.Unknown Then
                    If oCodTax = TaxCods.EXT Then
                        AddSegment(sb, "TAX", oCodTax.ToString())
                    Else
                        Dim DcPorcentaje, DcImporte, DcBaseImponible As Decimal
                        AddSegment(sb, "TAX", oCodTax.ToString, DcPorcentaje, DcImporte, DcBaseImponible)
                    End If
                End If

                'Divisa
                If oOrder.Cur IsNot Nothing Then
                    AddSegment(sb, "CUX", oOrder.Cur.Tag)
                End If

                'Transport
                If oCodTrp <> TdtCods.Unknown Then
                    AddSegment(sb, "TDT", CInt(oCodTrp))
                End If

                If oIncoterm IsNot Nothing Then
                    AddSegment(sb, "TOD", oIncoterm.Id.ToString, oPortes.ToString())
                End If

                Dim linIdx As Integer
                Dim totalNet As Decimal
                Dim totalBrut As Decimal

                For Each item In oOrder.Items
                    linIdx += 1
                    If item.Sku.Ean13 Is Nothing Then
                        exs.Add(New Exception(String.Format("article {0} sense codi EAN: {1}", item.Sku.Id, item.Sku.RefYNomLlarg.Esp)))
                    Else
                        AddSegment(sb, "LIN", item.Sku.Ean13.Value, "EN", linIdx)
                        AddSegment(sb, "PIALIN", "IN", item.Sku.Id) 'RefComprador
                        If item.Sku.RefProveidor > "" Then
                            AddSegment(sb, "PIALIN", "SA", item.Sku.RefProveidor) 'RefProveedor
                        End If
                        AddSegment(sb, "IMDLIN", "F", "M", "CU", item.Sku.nomPrvOrMyd().Truncate(70)) 'Description
                        AddSegment(sb, "QTYLIN", 21, item.Qty, "PCE") 'pedidas
                        If item.ChargeCod = DTOPurchaseOrderItem.ChargeCods.FOC Or item.Price IsNot Nothing AndAlso item.Price.IsZero Then
                            AddSegment(sb, "QTYLIN", 192, item.Qty, "PCE") 'GratuitoIncluido
                        End If
                        AddSegment(sb, "PRILIN", "AAB", item.Price) 'Preu brut
                        If item.Dto <> 0 Then
                            AddSegment(sb, "PRILIN", "AAA", item.PreuNet) ' Preu net
                            If item.Dto <> 0 Then
                                AddSegment(sb, "ALCLIN", "A", 1, "TD", "", item.Dto)
                            End If
                        End If
                        AddSegment(sb, "MOALIN", item.Amount) 'Import
                        totalBrut += item.Qty * item.Price.Val
                        totalNet += item.Amount.Val
                    End If
                Next


                AddSegment(sb, "MOARES", totalNet, totalBrut)
                AddSegment(sb, "CNTRES", "", "", "", linIdx)
        End Select

        Dim retval As String = sb.ToString
        Return retval

    End Function

    Shared Sub AddSegment(ByRef sb As Text.StringBuilder, tag As String, ParamArray fields() As Object)
        Dim segment As String = DTOEdiversaSegment.Factory(tag, fields)
        sb.AppendLine(segment)
    End Sub
End Class
