Public Class Incidencia
    Inherits _FeblBase
    Shared Async Function Find(exs As List(Of Exception), oGuid As Guid, Optional includeThumbnails As Boolean = False) As Task(Of DTOIncidencia)
        Dim retval = Await Api.Fetch(Of DTOIncidencia)(exs, "Incidencia", oGuid.ToString())
        If exs.Count = 0 Then
            retval.restoreObjects()
            If includeThumbnails Then
                Dim oSpriteImage = Await Api.FetchImage(exs, "Incidencia/SpriteImage", oGuid.ToString())
                If exs.Count = 0 And oSpriteImage IsNot Nothing Then
                    Dim iCount = retval.DocFileImages.Count + retval.Videos.Count + retval.PurchaseTickets.Count
                    Dim idx As Integer
                    Dim oDocfiles = retval.Attachments()
                    For Each oDocfile In oDocfiles
                        oDocfile.Thumbnail = LegacyHelper.SpriteHelper.Extract(oSpriteImage, idx, iCount)
                        idx += 1
                    Next
                    For Each oTracking In retval.Trackings
                        oTracking.Target = retval.ToGuidNom()
                    Next
                End If
            End If
        End If

        Return retval
    End Function

    Shared Async Function Trackings(exs As List(Of Exception), oIncidencia As DTOIncidencia) As Task(Of DTOTracking.Collection)
        Dim retval = Await Api.Fetch(Of DTOTracking.Collection)(exs, "Incidencia/trackings", oIncidencia.Guid.ToString)
        Return retval
    End Function

    Shared Async Function FromNum(exs As List(Of Exception), oEmp As DTOEmp, id As Integer) As Task(Of DTOIncidencia)
        Dim retval = Await Api.Fetch(Of DTOIncidencia)(exs, "Incidencia/FromNum", oEmp.Id, id)
        If retval IsNot Nothing Then
            retval.restoreObjects()
        End If
        Return retval
    End Function

    Shared Function Load(exs As List(Of Exception), ByRef oIncidencia As DTOIncidencia, Optional includeThumbnails As Boolean = False) As Boolean
        If Not oIncidencia.IsLoaded And Not oIncidencia.IsNew Then
            Dim pIncidencia = Api.FetchSync(Of DTOIncidencia)(exs, "Incidencia", oIncidencia.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOIncidencia)(pIncidencia, oIncidencia, exs)
                oIncidencia.restoreObjects()
                If includeThumbnails Then
                    Dim oSpriteImage = Api.FetchImageSync(exs, "Incidencia/SpriteImage", oIncidencia.Guid.ToString())

                    If exs.Count = 0 And oSpriteImage IsNot Nothing Then
                        Dim iCount = oIncidencia.DocFileImages.Count + oIncidencia.PurchaseTickets.Count
                        Dim idx As Integer
                        For Each oDocfile In oIncidencia.Attachments
                            oDocfile.Thumbnail = LegacyHelper.SpriteHelper.Extract(oSpriteImage, idx, iCount)
                            idx += 1
                        Next
                    End If
                End If
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Update(exs As List(Of Exception), value As DTOIncidencia) As Task(Of DTOIncidencia)
        Dim retval As DTOIncidencia = value
        Dim serialized = Api.Serialize(value, exs)
        If exs.Count = 0 Then
            Dim oMultipart As New ApiHelper.MultipartHelper()
            oMultipart.AddStringContent("Serialized", serialized)
            Dim idx As Integer
            For Each oDocfile In value.Attachments
                idx += 1
                oMultipart.AddFileContent(String.Format("docfile_thumbnail_{0:000}", idx), oDocfile.Thumbnail)
                oMultipart.AddFileContent(String.Format("docfile_stream_{0:000}", idx), oDocfile.Stream)
            Next
            retval = Await Api.Upload(Of DTOIncidencia)(oMultipart, exs, "Incidencia")
            If retval Is Nothing Then
                retval = value
            Else
                retval.restoreObjects()
            End If
        End If
        Return retval
    End Function

    Shared Async Function Delete(exs As List(Of Exception), oIncidencia As DTOIncidencia) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOIncidencia)(oIncidencia, exs, "Incidencia")
    End Function

    Shared Function Summary(oIncidencia As DTOIncidencia, oLang As DTOLang) As Dictionary(Of String, String)
        Dim retval As New Dictionary(Of String, String)

        With oIncidencia
            If Not .IsNew Then
                retval.Add(oLang.Tradueix("Incidencia", "Incidencia", "Id"), .Num)
                retval.Add(oLang.Tradueix("Fecha", "Data", "Date"), TextHelper.VbFormat(.fch, "dd/MM/yy HH:mm"))
            End If

            retval.Add(oLang.Tradueix("Distribuidor", "Distribuïdor", "Distributor"), DTOContact.HtmlNameAndAddress(oIncidencia.Customer))
            retval.Add(oLang.Tradueix("Contacto", "Contacte", "Contact"), .ContactPerson & "<br/>" & .EmailAdr & "<br/>tel.: " & .Tel)
            retval.Add(oLang.Tradueix("Su referencia", "Referencia", "Your reference", “A sua referencia”), .CustomerRef)

            Dim sProduct As String = DirectCast(.product, DTOProduct).FullNom(oLang)

            If .serialNumber > "" Then
                sProduct = sProduct & "<br/>" & oLang.Tradueix("num.serie", "num.serie", "serial #") & ": " & .serialNumber
            End If

            If .ManufactureDate > "" Then
                sProduct = sProduct & "<br/>" & oLang.Tradueix("fecha fabricación", "data fabricació", "manufacturing date") & ": " & .ManufactureDate
            End If

            retval.Add(oLang.Tradueix("Producto", "Producte", "Product"), sProduct)

            retval.Add(oLang.Tradueix("Avería", "Avería", "Issue"), .Description)

            Dim sb As New System.Text.StringBuilder
            If .DocFileImages.Count > 0 Then
                For Each oDocfile As DTODocFile In .DocFileImages
                    sb.AppendLine("<a href='" & oDocfile.DownloadUrl(True) & "'><img src='" & oDocfile.ThumbnailUrl(True) & "' width='150px' height='auto'/></a>")
                Next
            End If
            retval.Add(oLang.Tradueix("Imágenes", "Imatges", "images"), If(.docFileImages.Count > 0, sb.ToString, "no"))

            sb = New System.Text.StringBuilder
            If .PurchaseTickets.Count > 0 Then
                For Each oDocfile As DTODocFile In .purchaseTickets
                    sb.AppendLine("<a href='" & oDocfile.downloadUrl(True) & "'><img src='" & oDocfile.ThumbnailUrl(True) & "' width='150px' height='auto'/></a>")
                Next
            End If
            retval.Add(oLang.Tradueix("Ticket", "Ticket", "Ticket"), If(.purchaseTickets.Count > 0, sb.ToString, "no"))

        End With
        Return retval
    End Function

    Shared Async Function Catalog(exs As List(Of Exception), oProcedencia As DTOIncidencia.Procedencias, oCustomer As DTOCustomer) As Task(Of DTOCatalog)
        Return Await Api.Fetch(Of DTOCatalog)(exs, "Incidencia/Catalog", oProcedencia, oCustomer.Guid.ToString())
    End Function

    Shared Async Function MailMessage(exs As List(Of Exception), oIncidencia As DTOIncidencia, oLang As DTOLang) As Task(Of DTOMailMessage)
        Dim oSsc = DTOSubscription.Wellknown(DTOSubscription.Wellknowns.NovaIncidencia)
        Dim oSubscriptors = Await FEB2.Subscription.Subscriptors(oSsc, exs)
        Dim retval = DTOMailMessage.Factory(oIncidencia.EmailAdr)
        With retval
            .Bcc = DTOSubscriptor.Recipients(oSubscriptors)
            .subject = String.Format("M+O: {0} {1} {2}", oLang.Tradueix("Registro de Incidencia Postventa", "Registre de Incidencia Postvenda", "Service incident", "Registo de Incidência Pós-venda"), oIncidencia.Num, oIncidencia.Customer.NomComercialOrDefault)
            .BodyUrl = FEB2.Mailing.BodyUrl(DTODefault.MailingTemplates.Incidencia, oIncidencia.Guid.ToString())
        End With
        Return retval
    End Function

    Shared Async Function SendMail(exs As List(Of Exception), oUser As DTOUser, oIncidencia As DTOIncidencia, oLang As DTOLang) As Task(Of Boolean)
        Dim oMailMessage = Await FEB2.Incidencia.MailMessage(exs, oIncidencia, oLang)
        If exs.Count = 0 Then
            Await FEB2.MailMessage.Send(exs, oUser, oMailMessage)
        End If
        Return exs.Count = 0
    End Function



End Class

Public Class Incidencias
    Inherits _FeblBase

    Shared Async Function Model(exs As List(Of Exception), oUser As DTOUser, onlyOpen As Boolean, Optional customer As DTOGuidNom.Compact = Nothing, Optional product As DTOGuidNom.Compact = Nothing, Optional year As Integer = 0) As Task(Of Models.IncidenciesModel)
        Dim oRequest As New Models.IncidenciesModel.Request
        With oRequest
            .UserGuid = oUser.Guid
            .OnlyOpen = onlyOpen
            If customer IsNot Nothing Then .CustomerGuid = customer.Guid
            If product IsNot Nothing Then .ProductGuid = product.Guid
            .Year = year
        End With
        Return Await Api.Execute(Of Models.IncidenciesModel.Request, Models.IncidenciesModel)(oRequest, exs, "Incidencias")
    End Function

    Shared Async Function Headers(exs As List(Of Exception), oUser As DTOUser, Optional oContact As DTOContact = Nothing) As Task(Of List(Of DTOIncidencia))
        'Dim retval = Await Api.Fetch(Of List(Of DTOIncidencia))(exs, "Incidencias/headers", oUser.Guid.ToString, OpcionalGuid(oContact))
        Dim oQuery = DTOIncidenciaQuery.Factory(oUser)
        oQuery.IncludeClosed = True
        oQuery.Year = DateTime.Today.Year
        oQuery = Await Api.Execute(Of DTOIncidenciaQuery, DTOIncidenciaQuery)(oQuery, exs, "Incidencias/query")
        Dim retval = oQuery.result
        For Each oIncidencia In retval
            oIncidencia.restoreObjects()
        Next
        Return retval
    End Function

    Shared Async Function Query(exs As List(Of Exception), oQuery As DTOIncidenciaQuery) As Task(Of DTOIncidenciaQuery)
        Dim retval = Await Api.Execute(Of DTOIncidenciaQuery, DTOIncidenciaQuery)(oQuery, exs, "Incidencias/query")
        For Each oIncidencia In retval.result
            oIncidencia.restoreObjects()
        Next
        Return retval
    End Function

    Shared Async Function CodisDeTancament(exs As List(Of Exception)) As Task(Of List(Of DTOIncidenciaCod))
        Return Await Api.Fetch(Of List(Of DTOIncidenciaCod))(exs, "Incidencias/CodisDeTancament")
    End Function

    Shared Async Function Reposicions(exs As List(Of Exception), oEmp As DTOEmp, year As Integer) As Task(Of List(Of DTOIncidencia))
        Dim retval = Await Api.Fetch(Of List(Of DTOIncidencia))(exs, "Incidencias/Reposicions", oEmp.Id, year)
        For Each oIncidencia In retval
            oIncidencia.restoreObjects()
        Next
        Return retval
    End Function

    Shared Async Function Ratios(exs As List(Of Exception), FchFrom As Date, FchTo As Date) As Task(Of List(Of Tuple(Of DTOProductCategory, Integer, Integer)))
        Return Await Api.Fetch(Of List(Of Tuple(Of DTOProductCategory, Integer, Integer)))(exs, "Incidencias/Ratios", FormatFch(FchFrom), FormatFch(FchTo))
    End Function
    Shared Async Function WithVideos(exs As List(Of Exception)) As Task(Of List(Of Guid))
        Return Await Api.Fetch(Of List(Of Guid))(exs, "Incidencias/withvideos")
    End Function


End Class