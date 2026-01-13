Public Class Importacio

    Shared Function Find(oGuid As Guid) As DTOImportacio
        Dim retval As DTOImportacio = ImportacioLoader.Find(oGuid)
        Return retval
    End Function

    Shared Function FromDelivery(oDelivery As DTODelivery) As DTOImportacio
        Dim retVal As DTOImportacio = ImportacioLoader.FromDelivery(oDelivery)
        Return retVal
    End Function

    Shared Function Load(ByRef oImportacio As DTOImportacio) As Boolean
        Return ImportacioLoader.Load(oImportacio)
    End Function

    Shared Function Update(oImportacio As DTOImportacio, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = ImportacioLoader.Update(oImportacio, exs)
        BEBL.ServerCache.NotifyUpdate(Models.ServerCache.Tables.SkuPrevisions)
        Return retval
    End Function

    Shared Function Entrada(oDelivery As DTODelivery, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = ImportacioLoader.Entrada(oDelivery, exs)
        BEBL.ServerCache.NotifyUpdate(Models.ServerCache.Tables.SkuPrevisions)
        Return retval
    End Function
    Shared Function ValidateCamion(oConfirmation As DTOImportacio.Confirmation, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = ImportacioLoader.ValidateCamion(oConfirmation, exs)
        Return retval
    End Function


    Shared Function LogAvisTrp(ByRef oImportacio As DTOImportacio, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = ImportacioLoader.LogAvisTrp(oImportacio, exs)
        Return retval
    End Function

    Shared Function Delete(oImportacio As DTOImportacio, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = ImportacioLoader.Delete(oImportacio, exs)
        BEBL.ServerCache.NotifyUpdate(Models.ServerCache.Tables.SkuPrevisions)
        Return retval
    End Function

    Shared Function RevertPrevisions(exs As List(Of Exception), oImportacio As DTOImportacio) As Boolean
        Return ImportacioLoader.RevertPrevisions(exs, oImportacio)
    End Function

    Shared Function UnConfirm(exs As List(Of Exception), oImportacio As DTOImportacio) As Boolean
        BEBL.Importacio.Load(oImportacio)
        If BEBL.InvoicesReceived.ClearConfirmation(exs, oImportacio) Then
            For i As Integer = oImportacio.items.Count - 1 To 0 Step -1
                Dim item = oImportacio.items(i)
                Select Case item.srcCod
                    Case DTOImportacioItem.SourceCodes.alb
                        Dim oDelivery As New DTODelivery(item.Guid)
                        If BEBL.Delivery.Delete(exs, oDelivery) Then
                            oImportacio.items.RemoveAt(i)
                        End If

                    Case DTOImportacioItem.SourceCodes.fra
                        Dim oCca = BEBL.Cca.Find(item.Guid)
                        If BEBL.Cca.Delete(oCca, exs) Then
                            oImportacio.items.RemoveAt(i)
                        End If
                End Select
            Next
            BEBL.Importacio.Update(oImportacio, exs)
            BEBL.ServerCache.NotifyUpdate(Models.ServerCache.Tables.SkuPrevisions)
        End If
        Return exs.Count = 0
    End Function

    Shared Function Confirm(exs As List(Of Exception), oConfirmacio As DTOImportacio.Confirmation) As Boolean

        Dim oImportacio As DTOImportacio = oConfirmacio.Importacio
        BEBL.Importacio.Load(oImportacio)
        Dim oProveidor = oImportacio.proveidor

        Dim oInvoicesReceived As List(Of DTOInvoiceReceived) = BEBL.InvoicesReceived.All(oImportacio)
        Dim oItem As DTOImportacioItem = Nothing

        For Each invoice In oInvoicesReceived
            BEBL.InvoiceReceived.Load(invoice)

            'check if duplicated
            Dim oDuplicada As DTOCca = BEBL.Proveidor.CheckFacturaAlreadyExists(oProveidor, DTOExercici.Current(oConfirmacio.User.Emp), invoice.DocNum)
            If oDuplicada IsNot Nothing Then
                Dim sWarn As String = "aquesta factura ja está entrada" & vbCrLf
                sWarn = sWarn & "per " & DTOUser.NicknameOrElse(oDuplicada.UsrLog.UsrCreated) & " el " & oDuplicada.UsrLog.FchCreated.ToShortDateString & " a las " & Format(oDuplicada.UsrLog.FchCreated, "HH:mm")
                sWarn = sWarn & vbCrLf & " (assentament " & oDuplicada.Id.ToString & ")"
                sWarn = sWarn & vbCrLf & oDuplicada.Concept
                exs.Add(New Exception(sWarn))
            Else
                'genera l'albarà d'entrada a partir de la factura rebuda per Edi
                Dim oDelivery As DTODelivery = BEBL.InvoiceReceived.Delivery(invoice, oConfirmacio.User)
                oDelivery.Importacio = oImportacio

                'desa l'albarà generat
                If BEBL.Delivery.Update(oDelivery, exs) Then
                    'afegeix l'albarà a la llista d'albarans de la importació
                    oItem = DTOImportacioItem.Factory(oImportacio, DTOImportacioItem.SourceCodes.alb, oDelivery.Guid)
                    oItem.amt = oDelivery.baseImponible
                    oImportacio.items.Add(oItem)

                    'assigna la importació a la factura (TO DEPRECATE, això ja es fa al generar les previsions)
                    invoice.Importacio = New DTOGuidNom(oImportacio.Guid)
                    If Not BEBL.InvoiceReceived.Update(exs, invoice) Then
                        exs.Add(New Exception("error al desar el numero de remesa de importació a la factura " & invoice.DocNum))
                    End If

                    'genera el Pdf de la factura
                    Dim oStream As Byte() = LegacyHelper.PdfEdiInvoiceReceived.Factory(invoice, oConfirmacio.User.Emp)
                    Dim oDocFile = LegacyHelper.DocfileHelper.Factory(exs, oStream)

                    'genera l'assentament de la factura
                    Dim oPlan = DTOApp.Current.PgcPlan
                    Dim oCtas = BEBL.PgcCtas.All(oPlan)
                    If oCtas Is Nothing Then
                        exs.Add(New Exception("error al llegir els comptes"))
                    Else
                        Dim oCca = DTOCca.Factory(invoice.Fch, oConfirmacio.User, DTOCca.CcdEnum.FacturaProveidor)
                        With oCca
                            .Concept = String.Format("fra.{0} (R.{1}) de {2}", invoice.DocNum, oImportacio.id, oProveidor.Nom)
                            .DocFile = oDocFile
                            .BookFra = DTOBookFra.Factory(oCca, oProveidor)
                            With .BookFra
                                .tipoFra = "F1" 'rectificar a ma si no es aixi
                                .cta = oCtas.FirstOrDefault(Function(x) x.Codi = DTOPgcPlan.Ctas.Compras)
                                .contact = oProveidor
                                .fraNum = invoice.DocNum
                                .dsc = .cta.Nom.Esp

                                If DTOContact.isIVASujeto(oProveidor) Then
                                    Dim DcTipusIva = DTOTax.closest(DTOTax.Codis.iva_Standard, invoice.Fch).tipus
                                    .claveRegimenEspecialOTrascendencia = "01"
                                    Dim oSujeto As New DTOBaseQuota(invoice.TaxBase, DcTipusIva)
                                    oSujeto.source = oCtas.FirstOrDefault(Function(x) x.Codi = DTOPgcPlan.Ctas.Compras)
                                    .ivaBaseQuotas.Add(oSujeto)
                                Else
                                    .claveRegimenEspecialOTrascendencia = DTOContact.claveRegimenEspecialOTrascendencia(oProveidor)
                                    Dim oExento As New DTOBaseQuota(invoice.TaxBase)
                                    oExento.source = oCtas.FirstOrDefault(Function(x) x.Codi = DTOPgcPlan.Ctas.Compras)
                                    .ivaBaseQuotas.Add(oExento)
                                    .claveExenta = DTOContact.claveCausaExempcio(oProveidor)
                                End If

                                AddIvas(oCca, .ivaBaseQuotas, oProveidor, oCtas)

                                If .import = DTOInvoice.ExportCods.intracomunitari Then
                                    Dim oBaseExempta As DTOBaseQuota = .ivaBaseQuotas.FirstOrDefault(Function(x) x.tipus = 0)
                                    If oBaseExempta IsNot Nothing Then
                                        AddIvaIntracomunitari(oCca, oBaseExempta.baseImponible, oCtas)
                                    End If
                                End If
                            End With
                        End With

                        Dim oCtaTotalCod = DTOPgcCta.getCtaProveedors(invoice.Cur)
                        Dim oCtaTotal = BEBL.PgcCta.FromCod(oCtaTotalCod, oConfirmacio.User.Emp)
                        If oCtaTotal Is Nothing Then
                            exs.Add(New Exception("error al llegir el compte del total"))
                        Else
                            AddTotal(oCca, oCtaTotal, oProveidor, invoice.TaxBase)

                            Dim oPnds As List(Of DTOPnd) = Nothing
                            Dim oPnd As DTOPnd = Nothing
                            oPnd = DTOPnd.Factory(oConfirmacio.User.Emp)
                            With oPnd
                                .Contact = oProveidor
                                .Fch = invoice.Fch
                                .Cta = oCtaTotal
                                .Cca = oCca
                                .Vto = DTOPaymentTerms.vto(oProveidor.paymentTerms, invoice.Fch)
                                .Amt = DTOAmt.Factory(invoice.TaxBase)
                                '.Cfp = ComboBoxCfp.SelectedValue
                                .Cfp = oProveidor.paymentTerms.Cod
                                .Yef = oCca.Fch.Year
                                .FraNum = invoice.DocNum
                                .Fpg = DTOPaymentTerms.Text(oProveidor.paymentTerms, oProveidor.Lang, .Vto)
                                .Cod = DTOPnd.Codis.Creditor
                                .Status = DTOPnd.StatusCod.pendent
                            End With
                            oPnds = New List(Of DTOPnd)
                            oPnds.Add(oPnd)

                            'afegeix l'assentament a la llista de factures de la importació
                            oItem = DTOImportacioItem.Factory(oImportacio, DTOImportacioItem.SourceCodes.fra, oCca.Guid)
                            oItem.amt = oCca.BookFra.baseDevengada
                            oImportacio.items.Add(oItem)

                            'desa l'assentament i deixa'l pendent de pagament
                            If Not BEBL.Proveidor.SaveFactura(exs, oCca, oPnds, oImportacio) Then
                                exs.Add(New Exception("Error al desar la factura"))
                            End If
                        End If
                    End If

                Else
                    exs.Add(New Exception("error al desar l'albarà de entrada corresponent a la factura " & invoice.DocNum))
                End If
            End If
        Next

        'desa el camp de quantitat confirmada a cada item de factura rebuda
        ImportacioLoader.Confirm(exs, oConfirmacio)
        BEBL.ServerCache.NotifyUpdate(Models.ServerCache.Tables.SkuPrevisions)

        Return exs.Count = 0
    End Function

    Private Shared Sub AddIvaIntracomunitari(ByRef oCca As DTOCca, oBaseImponible As DTOAmt, oCtas As List(Of DTOPgcCta))
        Dim oTaxIva = DTOApp.Current.Taxes.FirstOrDefault(Function(x) x.codi = DTOTax.Codis.iva_Standard)
        If oTaxIva IsNot Nothing Then
            If oBaseImponible IsNot Nothing AndAlso oBaseImponible.IsNotZero Then
                Dim oQuota As DTOAmt = oBaseImponible.Percent(oTaxIva.tipus)
                oCca.AddDebit(oQuota, oCtas.FirstOrDefault(Function(x) x.Codi = DTOPgcPlan.Ctas.IvaSoportatIntracomunitari))
                oCca.AddCredit(oQuota, oCtas.FirstOrDefault(Function(x) x.Codi = DTOPgcPlan.Ctas.IvaRepercutitIntracomunitari))
            End If
        End If
    End Sub

    Private Shared Sub AddIvas(ByRef oCca As DTOCca, oBaseIvas As List(Of DTOBaseQuota), oProveidor As DTOProveidor, oCtas As List(Of DTOPgcCta))
        For Each item As DTOBaseQuota In oBaseIvas
            Dim oCta As DTOPgcCta = item.source
            oCca.AddDebit(item.baseImponible, oCta, oProveidor)
            If item.quota IsNot Nothing Then
                If item.quota.IsNotZero Then
                    oCca.AddDebit(item.quota, oCtas.FirstOrDefault(Function(x) x.Codi = DTOPgcPlan.Ctas.IvaSoportatNacional))
                End If
            End If
        Next
    End Sub

    Private Shared Sub AddTotal(ByRef oCca As DTOCca, ByVal oCta As DTOPgcCta, ByVal oContact As DTOContact, oAmt As DTOAmt)
        If oAmt.IsNotZero Then
            oCca.AddSaldo(oCta, oContact)
        End If
    End Sub
End Class

Public Class Importacions
    Shared Function Weeks(oEmp As DTOEmp) As List(Of DTOImportacio)
        Dim retval As List(Of DTOImportacio) = ImportacionsLoader.Weeks(oEmp)
        Return retval
    End Function

    Shared Function All(oEmp As DTOEmp, year As Integer, Optional oProveidor As DTOProveidor = Nothing) As List(Of DTOImportacio)
        Dim retval As List(Of DTOImportacio) = ImportacionsLoader.All(oEmp, year, oProveidor)
        Return retval
    End Function

    Shared Function Transits(oEmp As DTOEmp) As List(Of DTOImportTransit)
        Dim retval As List(Of DTOImportTransit) = ImportacionsLoader.Transits(oEmp)
        Return retval
    End Function

End Class
