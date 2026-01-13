Public Class SpvMailMessageHelper
    Shared Async Function MailMessage(oEmp As DTOEmp, oSpv As DTOSpv, exs As List(Of Exception)) As Task(Of DTOMailMessage)
        Dim retval As DTOMailMessage = Nothing
        If FEB.Spv.Load(oSpv, exs) Then
            FEB.Contact.Load(oSpv.Customer, exs)

            Dim sRecipients As New List(Of String)
            If oSpv.LabelEmailedTo > "" Then
                sRecipients.Add(oSpv.LabelEmailedTo)
            Else
                Dim oEmails = Await FEB.Emails.All(exs, oSpv.Customer)
                sRecipients = oEmails.Select(Function(x) x.EmailAddress).ToList
            End If

            retval = DTOMailMessage.Factory(sRecipients)
            If LoadAttachments(oEmp, retval, oSpv, exs) Then

                Dim oLang As DTOLang = oSpv.Customer.lang
                Dim oTxt = FEB.Txt.FindSync(DTOTxt.Ids.MailSpv, exs)
                Dim sIncidencia As String = ""
                If oSpv.Incidencia IsNot Nothing Then
                    sIncidencia = oSpv.incidencia.AsinOrNum()
                End If
                retval.body = oTxt.ToHtml(oLang, oSpv.id, oSpv.fchAvis.Year, oSpv.product.FullNom(), oSpv.contacto, oSpv.sRef, sIncidencia)
                retval.subject = oLang.Tradueix("REPARACION " & oSpv.Id & ": ETIQUETA DE ENVIO", "REPARACIO " & oSpv.Id & ": ETIQUETA PER L'ENVIAMENT", "SERVICE " & oSpv.Id & ": SHIPPING LABEL")
            End If
        End If
        Return retval

    End Function

    Shared Function LoadAttachments(oEmp As DTOEmp, ByRef oMailMessage As DTOMailMessage, oSpv As DTOSpv, exs As List(Of Exception)) As Boolean
        LoadLabelAttachment(oMailMessage, oSpv, exs)
        LoadDeliveryAttachment(oEmp, oMailMessage, oSpv, exs)
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Function LoadDeliveryAttachment(oEmp As DTOEmp, ByRef oMailMessage As DTOMailMessage, oSpv As DTOSpv, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean
        Dim oTrp = DTOTransportista.FromContact(FEB.Default.ContactSync(oEmp, DTODefault.Codis.SpvTrp, exs))
        Dim oTaller = DTOTaller.FromContact(FEB.Default.ContactSync(oEmp, DTODefault.Codis.Taller, exs))

        Dim oPdfStream As Byte() = LegacyHelper.PdfSpvCustomerDelivery.Factory(exs, oSpv, oTrp, oTaller)
        Dim sFilename As String = "albaran de entrega a transportista reparacion " & oSpv.Id & ".pdf"

        If FileSystemHelper.SaveStream(oPdfStream, exs, sFilename) Then
            oMailMessage.AddAttachment(sFilename)
            retval = True
        End If
        Return retval
    End Function

    Shared Function LoadLabelAttachment(ByRef oMailMessage As DTOMailMessage, oSpv As DTOSpv, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean


        'oSpv.Customer.Logo = FEB.Contact.LogoSync(exs, oSpv.Customer)
        Dim oPdfSpvInLabel As New LegacyHelper.PdfSpvInLabel(oSpv)
        Dim oPdfStream As Byte() = oPdfSpvInLabel.PdfStream(exs)
        If exs.Count = 0 Then
            Dim sFilename As String = "etiqueta envío reparacion " & oSpv.Id & ".pdf"

            If FileSystemHelper.SaveStream(oPdfStream, exs, sFilename) Then
                oMailMessage.AddAttachment(sFilename)
                retval = True
            End If

        End If

        Return retval
    End Function

End Class
