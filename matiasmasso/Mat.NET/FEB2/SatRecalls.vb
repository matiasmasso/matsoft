Public Class SatRecall
    Shared Async Function Find(oGuid As Guid, exs As List(Of Exception)) As Task(Of DTOSatRecall)
        Dim retval = Await Api.Fetch(Of DTOSatRecall)(exs, "SatRecall", oGuid.ToString())
        If retval.Incidencia IsNot Nothing Then
            retval.Incidencia.restoreObjects()
        End If
        Return retval
    End Function

    Shared Function FindSync(exs As List(Of Exception), oGuid As Guid) As DTOSatRecall
        Dim retval = Api.FetchSync(Of DTOSatRecall)(exs, "SatRecall", oGuid.ToString())
        If retval IsNot Nothing Then
            If retval.Incidencia IsNot Nothing Then
                retval.Incidencia.restoreObjects()
            End If
        End If
        Return retval
    End Function

    Shared Async Function fromIncidencia(exs As List(Of Exception), oIncidencia As DTOIncidencia) As Task(Of DTOSatRecall)
        Dim retval = Await Api.Fetch(Of DTOSatRecall)(exs, "SatRecall/FromIncidencia", oIncidencia.Guid.ToString())
        If retval IsNot Nothing Then
            retval.Incidencia.restoreObjects()
        End If
        Return retval
    End Function



    Shared Function Load(ByRef oSatRecall As DTOSatRecall, exs As List(Of Exception)) As Boolean
        If Not oSatRecall.IsLoaded And Not oSatRecall.IsNew Then
            Dim pSatRecall = FindSync(exs, oSatRecall.Guid)
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOSatRecall)(pSatRecall, oSatRecall, exs)
                oSatRecall.Incidencia.restoreObjects()
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Update(oSatRecall As DTOSatRecall, exs As List(Of Exception)) As Task(Of Boolean)
        Dim retval As Boolean
        Dim oSatRecallTrimmed = DTOBaseGuid.Trim(oSatRecall, exs)
        If exs.Count = 0 Then
            retval = Await Api.Update(Of DTOSatRecall)(oSatRecallTrimmed, exs, "SatRecall")
        End If
        oSatRecall.IsNew = False
        Return retval
    End Function

    Shared Async Function Delete(oSatRecall As DTOSatRecall, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOSatRecall)(oSatRecall, exs, "SatRecall")
    End Function



    Shared Async Function MailMessageToCustomer(oSatRecall As DTOSatRecall, recallFormFilename As String, exs As List(Of Exception)) As Task(Of DTOMailMessage)
        Dim retval As DTOMailMessage = Nothing
        If FEB2.SatRecall.Load(oSatRecall, exs) Then
            If FEB2.Product.Load(oSatRecall.Incidencia.Product, exs) Then
                Dim sSubject = DTOSatRecall.MessageSubject(oSatRecall, DTOSatRecall.MailTargets.Customer)
                Dim sBody As String = Await MessageBody(oSatRecall, DTOSatRecall.MailTargets.Customer, exs)
                retval = DTOMailMessage.Factory(oSatRecall.Incidencia.EmailAdr, sSubject, sBody)
                retval.bodyFormat = DTOMailMessage.MessageBodyFormats.Html
                AddAttachments(retval, oSatRecall, DTOSatRecall.MailTargets.Customer, recallFormFilename, exs)
            End If
        End If

        Return retval
    End Function


    Shared Async Function MailMessageToManufacturer(oEmp As DTOEmp, oSatRecall As DTOSatRecall, recallFormFilename As String, exs As List(Of Exception)) As Task(Of DTOMailMessage)
        Dim retval As DTOMailMessage = Nothing
        If FEB2.SatRecall.Load(oSatRecall, exs) Then
            If FEB2.Product.Load(oSatRecall.Incidencia.Product, exs) Then
                Dim oManufacturer As DTOProveidor = DTOProduct.proveidor(oSatRecall.Incidencia.Product)
                Dim sRecipients = Await FEB2.Subscriptors.Recipients(exs, oEmp, DTOSubscription.Wellknowns.AvisRecollidesServeiTecnic, oManufacturer)
                Dim sSubject = DTOSatRecall.MessageSubject(oSatRecall, DTOSatRecall.MailTargets.Provider)
                Dim sBody As String = Await MessageBody(oSatRecall, DTOSatRecall.MailTargets.Provider, exs)
                retval = DTOMailMessage.Factory(sRecipients, sSubject, sBody)
                AddAttachments(retval, oSatRecall, DTOSatRecall.MailTargets.Provider, recallFormFilename, exs)
            End If
        End If
        Return retval
    End Function

    Protected Shared Async Function MessageBody(oSatRecall As DTOSatRecall, mailtarget As DTOSatRecall.MailTargets, exs As List(Of Exception)) As Task(Of String)
        Dim retval As String = ""
        Select Case mailtarget
            Case DTOSatRecall.MailTargets.Customer
                Dim oLang = oSatRecall.Incidencia.customer.lang
                Dim oDomain = DTOWebDomain.Factory(oLang, True)
                Dim oSku As DTOProductSku = oSatRecall.Incidencia.product
                Dim oTxt = Await FEB2.Txt.Find(DTOTxt.Ids.SatRecallCustomer, exs)
                retval = oTxt.ToHtml(
                    oLang,
                    DTOProductSku.FullNom(oSku),
                    oSku.GetUrl(oLang))

            Case DTOSatRecall.MailTargets.Provider
                Dim oProveidor = DTOProduct.proveidor(oSatRecall.Incidencia.product)
                If FEB2.Proveidor.Load(oProveidor, exs) Then
                    Dim oLang = oProveidor.Lang
                    Dim oCustomer = oSatRecall.Incidencia.Customer
                    FEB2.Customer.Load(oCustomer, exs)
                    Dim oSku = Await FEB2.ProductSku.Find(exs, oSatRecall.Incidencia.product.Guid)
                    Dim oTxt = Await FEB2.Txt.Find(DTOTxt.Ids.SatRecallManufacturer, exs)
                    retval = oTxt.ToHtml(
                    oLang,
                    String.Format("{0} {1}", oSku.RefProveidor, oSku.NomProveidor),
                    oCustomer.NomComercialOrDefault(),
                    DTOAddress.FullHtml(oCustomer.Address))
                End If
        End Select

        Return retval
    End Function


    Shared Function AddAttachments(ByRef oMailMessage As DTOMailMessage, oSatRecall As DTOSatRecall, mailTarget As DTOSatRecall.MailTargets, recallFormFilename As String, exs As List(Of Exception)) As Boolean
        'FEB2.Product.Load(oSatRecall.Incidencia.GetProduct, exs)
        If exs.Count = 0 Then
            Select Case mailTarget
                Case DTOSatRecall.MailTargets.Customer
                    oMailMessage.AddAttachment(recallFormFilename, "Formulario de recogida a adjuntar.docx")

                    Dim oLabelStream = LegacyHelper.PdfRecallLabel.Factory(oSatRecall)
                    oMailMessage.AddAttachment("Etiqueta para el bulto.pdf", oLabelStream)
                Case DTOSatRecall.MailTargets.Provider
                    oMailMessage.AddAttachment(recallFormFilename, "Questionnaire for returned goods.docx")
            End Select
        End If
        Return exs.Count = 0
    End Function



End Class

Public Class SatRecalls
    Inherits _FeblBase

    Shared Async Function All(exs As List(Of Exception), oEmp As DTOEmp, mode As DTOSatRecall.Modes) As Task(Of List(Of DTOSatRecall))
        Dim retval = Await Api.Fetch(Of List(Of DTOSatRecall))(exs, "SatRecalls", oEmp.Id, mode)
        If retval IsNot Nothing Then
            For Each oRecall In retval
                If oRecall.Incidencia IsNot Nothing Then
                    oRecall.Incidencia.restoreObjects()
                End If
            Next
        End If
        Return retval
    End Function

    Shared Function Excel(items As List(Of DTOSatRecall)) As ExcelHelper.Sheet
        Dim retval As New ExcelHelper.Sheet("Recollides fabricant", "Recollides fabricant")
        With retval
            .AddColumn("Incidencia")
            .AddColumn("Client")
            .AddColumn("Producte")
            .AddColumn("Punt de recollida")
            .AddColumn("Avis al client", ExcelHelper.Sheet.NumberFormats.DDMMYY)
            .AddColumn("Avis al proveidor", ExcelHelper.Sheet.NumberFormats.DDMMYY)
            .AddColumn("Ref.Recollida")
            .AddColumn("Abonament")
            .AddColumn("Data abonament", ExcelHelper.Sheet.NumberFormats.DDMMYY)
            .AddColumn("Observacions")
        End With
        For Each item In items
            Dim oRow = retval.AddRow
            oRow.AddCell(item.Incidencia.Num, FEB2.UrlHelper.Factory(True, item.Incidencia.UrlSegment))
            oRow.AddCell(item.Incidencia.Customer.FullNom)
            oRow.addCell(item.Incidencia.product.FullNom())
            oRow.AddCell(item.PickupFrom.ToString())
            oRow.AddCell(item.FchCustomer)
            oRow.AddCell(item.FchManufacturer)
            oRow.AddCell(item.PickupRef)
            oRow.AddCell(item.CreditNum)
            oRow.AddCell(item.CreditFch)
            oRow.AddCell(item.Obs)
        Next
        Return retval
    End Function

End Class

