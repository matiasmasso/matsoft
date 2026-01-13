Public Class RecallCli
    Inherits _FeblBase

    Shared Async Function Find(exs As List(Of Exception), oGuid As Guid) As Task(Of DTORecallCli)
        Return Await Api.Fetch(Of DTORecallCli)(exs, "RecallCli", oGuid.ToString())
    End Function

    Shared Function Load(exs As List(Of Exception), ByRef oRecallCli As DTORecallCli) As Boolean
        If Not oRecallCli.IsLoaded And Not oRecallCli.IsNew Then
            Dim pRecallCli = Api.FetchSync(Of DTORecallCli)(exs, "RecallCli", oRecallCli.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTORecallCli)(pRecallCli, oRecallCli, exs)
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Update(exs As List(Of Exception), oRecallCli As DTORecallCli) As Task(Of Boolean)
        Return Await Api.Update(Of DTORecallCli)(oRecallCli, exs, "RecallCli")
        oRecallCli.IsNew = False
    End Function

    Shared Async Function Delete(exs As List(Of Exception), oRecallCli As DTORecallCli) As Task(Of Boolean)
        Return Await Api.Delete(Of DTORecallCli)(oRecallCli, exs, "RecallCli")
    End Function

    Shared Async Function Factory(exs As List(Of Exception), oUser As DTOUser, oRecall As DTORecall) As Task(Of DTORecallCli)
        Dim oCustomers = Await Customers.FromUser(exs, oUser)
        Dim retval As New DTORecallCli
        With retval
            .UsrLog = DTOUsrLog.Factory(oUser)
            .Recall = oRecall
            .ContactEmail = oUser.EmailAddress
            .ContactNom = oUser.NickName

            If oCustomers.Count > 0 Then
                .Customer = oCustomers.First
            Else
                Dim oContacts = Await Contacts.All(exs, oUser)
                If exs.Count = 0 AndAlso oContacts.Count > 0 Then
                    .Customer = DTOCustomer.FromContact(oContacts.First)
                End If
            End If

            If .Customer IsNot Nothing Then
                Contact.Load(.Customer, exs)
                .ContactTel = Await Contact.Tel(exs, .Customer)
                Dim oAddress As DTOAddress = .Customer.Address
                .Address = oAddress.Text
                .Location = DTOAddress.Location(oAddress).Nom
                .Country = DTOAddress.Country(oAddress)
            End If
        End With
        Return retval
    End Function

    Shared Async Function MailMessage(oRecallCli As DTORecallCli, oPdfStream As Byte(), exs As List(Of Exception)) As Task(Of DTOMailMessage)
        Dim retval As DTOMailMessage = Nothing
        Dim sFilename As String = "etiquetas de envío.pdf"
        If FileSystemHelper.SaveStream(oPdfStream, exs, sFilename) Then
            retval = DTOMailMessage.Factory(oRecallCli.ContactEmail)
            retval.AddAttachment(sFilename)
            Dim oTxt = Await Txt.Find(DTOTxt.Ids.MailRecall, exs)
            If exs.Count = 0 Then
                retval.body = oTxt.ToHtml(DTOLang.ESP)
                retval.subject = String.Format("{0} recall. Confirmación de registro y etiquetas de envío", oRecallCli.Recall.Nom)
            End If
        End If
        Return retval
    End Function

    Shared Async Function MailMessageToVivace(oRecallCli As DTORecallCli, oPdfStream As Byte(), exs As List(Of Exception)) As Task(Of DTOMailMessage)
        Dim retval As DTOMailMessage = Nothing
        retval = DTOMailMessage.Factory("transportelaroca@vivacelogistica.com")
        Dim oTxt = Await Txt.Find(DTOTxt.Ids.MailRecallToVivace, exs)
        retval.body = oTxt.ToHtml(DTOLang.ESP,
                                    oRecallCli.Customer.NomComercialOrDefault(),
                                    oRecallCli.Address,
                                    DTORecallCli.RemiteLocation(oRecallCli),
                                    oRecallCli.ContactNom & " " & oRecallCli.ContactTel,
                                    oRecallCli.Bultos,
                                    Await PesosyMedidas(oRecallCli),
                                    oRecallCli.Recall.Nom)

        retval.subject = String.Format("{0} recall. Confirmación de registro y etiquetas de envío", oRecallCli.Recall.Nom)
        Return retval
    End Function

    Shared Async Function PesosyMedidas(value As DTORecallCli) As Task(Of String)
        Dim exs As New List(Of Exception)
        Dim oDualfix = Await ProductCategory.Find(exs, New Guid("A37DD979-7B0B-46F7-9A3E-21A4CFF36EEA"))
        Dim sDimensions As String = String.Format("{0} x {1} x {2} mm {3} Kg", oDualfix.DimensionL, oDualfix.DimensionW, oDualfix.DimensionH, oDualfix.KgBrut)
        Dim sb As New Text.StringBuilder
        Select Case value.Bultos
            Case <= 0
            Case 1
                sb.AppendFormat("{0}", sDimensions)
            Case Else
                sb.AppendFormat("{0} cajas iguales de {1} cada una", value.Bultos, sDimensions)
        End Select
        Dim retval As String = sb.ToString
        Return retval
    End Function
End Class

Public Class RecallClis
    Inherits _FeblBase

    Shared Async Function All(exs As List(Of Exception), oRecall As DTORecall) As Task(Of List(Of DTORecallCli))
        Return Await Api.Fetch(Of List(Of DTORecallCli))(exs, "RecallClis", oRecall.Guid.ToString())
    End Function

End Class
