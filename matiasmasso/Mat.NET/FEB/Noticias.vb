Public Class Noticia

    Shared Async Function Find(exs As List(Of Exception), oGuid As Guid) As Task(Of DTONoticia)
        Return Await Api.Fetch(Of DTONoticia)(exs, "Noticia", oGuid.ToString())
    End Function

    Shared Function FindSync(exs As List(Of Exception), oGuid As Guid) As DTONoticia
        Return Api.FetchSync(Of DTONoticia)(exs, "Noticia", oGuid.ToString())
    End Function

    Shared Async Function SearchByUrl(exs As List(Of Exception), sUrlFriendlySegment As String) As Task(Of DTONoticia)
        Return Await Api.Execute(Of String, DTONoticia)(sUrlFriendlySegment, exs, "Noticia/SearchByUrl")
    End Function

    Shared Async Function Image265x150(exs As List(Of Exception), oGuid As Guid) As Task(Of Byte())
        Return Await Api.FetchImage(exs, "Noticia/Image265x150", oGuid.ToString())
    End Function

    Shared Function Image265x150Sync(exs As List(Of Exception), oGuid As Guid) As Byte()
        Return Api.FetchImageSync(exs, "Noticia/Image265x150", oGuid.ToString())
    End Function

    Shared Function Load(exs As List(Of Exception), ByRef oNoticia As DTONoticia, Optional IncludeImages As Boolean = False) As Boolean
        Dim pNoticia As DTONoticia = Nothing
        If Not oNoticia.IsLoaded And Not oNoticia.IsNew Then
            If oNoticia.Src = DTOContent.Srcs.Eventos Then
                pNoticia = Api.FetchSync(Of DTOEvento)(exs, "Noticia", oNoticia.Guid.ToString())
                If exs.Count = 0 Then
                    DTOBaseGuid.CopyPropertyValues(Of DTOEvento)(pNoticia, oNoticia, exs)
                End If
                Dim oEvento As DTOEvento = oNoticia
                If oEvento.Area IsNot Nothing Then
                    Dim oArea As DTOArea = oEvento.Area.toobject(Of DTOArea)
                    Select Case oArea.Cod
                        Case DTOArea.Cods.Country
                            oEvento.Area = oEvento.Area.toobject(Of DTOCountry)
                        Case DTOArea.Cods.Zona
                            oEvento.Area = oEvento.Area.toobject(Of DTOZona)
                        Case DTOArea.Cods.Location
                            oEvento.Area = oEvento.Area.toobject(Of DTOLocation)
                        Case DTOArea.Cods.Zip
                            oEvento.Area = oEvento.Area.toobject(Of DTOZip)
                    End Select
                End If
            Else
                pNoticia = Api.FetchSync(Of DTONoticia)(exs, "Noticia", oNoticia.Guid.ToString())
                If exs.Count = 0 Then
                    DTOBaseGuid.CopyPropertyValues(Of DTONoticia)(pNoticia, oNoticia, exs)
                End If
            End If
            If exs.Count = 0 Then
                oNoticia.Image265x150 = Noticia.Image265x150Sync(exs, oNoticia.Guid)
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Update(exs As List(Of Exception), value As DTONoticia) As Task(Of Boolean)
        Dim retval As Boolean
        Dim serialized = Api.Serialize(value, exs)
        If exs.Count = 0 Then
            Dim oMultipart As New ApiHelper.MultipartHelper()
            oMultipart.AddStringContent("Serialized", serialized)
            oMultipart.AddFileContent("Image265x150", value.Image265x150)
            If TypeOf value Is DTOEvento Then
                retval = Await Api.Upload(oMultipart, exs, "Evento")
            Else
                retval = Await Api.Upload(oMultipart, exs, "Noticia")
            End If
        End If
        Return retval
    End Function

    Shared Async Function Delete(exs As List(Of Exception), oNoticia As DTONoticia) As Task(Of Boolean)
        Return Await Api.Delete(Of DTONoticia)(oNoticia, exs, "Noticia")
    End Function

    Shared Function UrlFriendly(oNoticia As DTONoticiaBase, Optional AbsoluteUrl As Boolean = False) As String
        Dim retval As String = ""

        Dim sUrlFriendlySegment As String = oNoticia.UrlFriendlySegment
        If sUrlFriendlySegment > "" Then
            Dim sCodSegment As String = oNoticia.Src.ToString
            retval = UrlHelper.Factory(AbsoluteUrl, sCodSegment, sUrlFriendlySegment)
        Else
            Select Case oNoticia.Src
                Case DTOContent.Srcs.Content
                    retval = UrlHelper.Factory(AbsoluteUrl, "content", oNoticia.Guid.ToString())
                Case Else
                    retval = UrlHelper.Factory(AbsoluteUrl, "noticia", oNoticia.Guid.ToString())
            End Select
        End If
        Return retval.ToLower
    End Function

    Shared Function UrlThumbnail(oNoticia As DTONoticia, Optional AbsoluteUrl As Boolean = False) As String
        Dim retval As String = UrlHelper.Image(DTO.Defaults.ImgTypes.News265x150, oNoticia.Guid, AbsoluteUrl)
        Return retval
    End Function

    Shared Function RootUrl(oCod As DTONoticiaBase.Srcs, Optional BlAbsoluteUrl As Boolean = False) As String
        Dim sSegment As String = oCod.ToString
        Dim retval As String = UrlHelper.Factory(BlAbsoluteUrl, sSegment)
        Return retval
    End Function

    Shared Function IsAllowed(oEmp As DTOEmp, oUser As DTOUser, oNoticia As DTONoticia) As Boolean
        Dim exs As New List(Of Exception)
        Dim retval As Boolean

        If oNoticia.professional Then
            If oUser IsNot Nothing AndAlso oUser.Rol IsNot Nothing AndAlso oUser.Rol.IsAuthenticated Then
                Select Case oUser.Rol.id
                    Case DTORol.Ids.SuperUser, DTORol.Ids.Admin, DTORol.Ids.Marketing, DTORol.Ids.Operadora, DTORol.Ids.SalesManager, DTORol.Ids.Taller
                        retval = True
                    Case DTORol.Ids.Denied, DTORol.Ids.Guest, DTORol.Ids.Lead, DTORol.Ids.NotSet, DTORol.Ids.Unregistered
                        retval = False
                    Case Else
                        Dim oProduct As DTOProduct = oNoticia.product
                        If oProduct Is Nothing Then
                            retval = True
                        Else
                            Dim oUserBrands = ProductBrands.AllSync(exs, oUser)
                            Dim oProductBrand = Product.Brand(exs, oProduct)
                            If oProductBrand Is Nothing Then
                                retval = True
                            Else
                                retval = oUserBrands.Exists(Function(x) x.Guid.Equals(oProductBrand.Guid))
                            End If
                        End If
                End Select
            Else
                retval = False
            End If
        Else
            retval = True
        End If

        Return retval
    End Function

End Class


Public Class Noticias
    Inherits _FeblBase


    Shared Async Function HeadersForSitemap(exs As List(Of Exception), oEmp As DTOEmp) As Task(Of List(Of DTONoticia))
        Return Await Api.Fetch(Of List(Of DTONoticia))(exs, "Noticias/HeadersForSitemap", oEmp.Id)
    End Function

    Shared Async Function Headers(exs As List(Of Exception), oSrc As DTONoticia.Srcs, Optional HidePro As Boolean = False, Optional OnlyVisible As Boolean = False) As Task(Of List(Of DTONoticiaBase))
        Dim retval As New List(Of DTONoticiaBase)
        Dim oHeaders = Await Api.Fetch(Of List(Of Object))(exs, "Noticias/Headers", oSrc, OpcionalBool(HidePro), OpcionalBool(OnlyVisible))
        For Each oHeader In oHeaders
            Dim item As DTONoticiaBase = oHeader.toobject(Of DTONoticiaBase)
            Select Case item.Src
                Case DTOContent.Srcs.Eventos
                    item = oHeader.toobject(Of DTOEvento)
                Case DTOContent.Srcs.News, DTOContent.Srcs.Content
                    item = oHeader.toobject(Of DTONoticia)
                Case DTOContent.Srcs.SabiasQue
                    item = oHeader.toobject(Of DTOSabiasQuePost)
            End Select
            retval.Add(item)
        Next
        Return retval
    End Function

    Shared Async Function LastNoticia(exs As List(Of Exception), oUser As DTOUser, oLang As DTOLang, Optional oProduct As DTOProduct = Nothing) As Task(Of DTONoticia)
        Return Await Api.Fetch(Of DTONoticia)(exs, "Noticias/LastNoticia", OpcionalGuid(oUser), oLang.Tag, OpcionalGuid(oProduct))
    End Function

    Shared Async Function LastNoticias(exs As List(Of Exception), oUser As DTOUser, oLang As DTOLang, Optional oProduct As DTOProduct = Nothing) As Task(Of List(Of DTONoticia))
        Return Await Api.Fetch(Of List(Of DTONoticia))(exs, "Noticias/LastNoticias", OpcionalGuid(oUser), oLang.Tag, OpcionalGuid(oProduct))
    End Function

    Shared Async Function Destacats(exs As List(Of Exception), oSrc As DTONoticiaBase.Srcs, oUser As DTOUser) As Task(Of List(Of DTONoticia))
        Return Await Api.Fetch(Of List(Of DTONoticia))(exs, "Noticias/Destacats", oSrc, OpcionalGuid(oUser))
    End Function

End Class