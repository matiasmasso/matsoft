Public Class Newsletter
    Inherits _FeblBase

    Shared Async Function Find(oGuid As Guid, exs As List(Of Exception)) As Task(Of DTONewsletter)
        Return Await Api.Fetch(Of DTONewsletter)(exs, "Newsletter", oGuid.ToString())
    End Function

    Shared Function Load(ByRef oNewsletter As DTONewsletter, exs As List(Of Exception)) As Boolean
        If Not oNewsletter.IsLoaded And Not oNewsletter.IsNew Then
            Dim pNewsletter = Api.FetchSync(Of DTONewsletter)(exs, "Newsletter", oNewsletter.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTONewsletter)(pNewsletter, oNewsletter, exs)
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Update(oNewsletter As DTONewsletter, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Update(Of DTONewsletter)(oNewsletter, exs, "Newsletter")
        oNewsletter.IsNew = False
    End Function


    Shared Async Function Delete(oNewsletter As DTONewsletter, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Delete(Of DTONewsletter)(oNewsletter, exs, "Newsletter")
    End Function

    Shared Function Url(oNewsletter As DTONewsletter, Optional ByVal AbsoluteUrl As Boolean = False) As String
        Return UrlHelper.Factory(AbsoluteUrl, "mail/newsletter", oNewsletter.Guid.ToString())
    End Function

    Shared Function ItemUrl(item As DTONewsletterSource, Optional ByVal AbsoluteUrl As Boolean = False) As String
        Dim retval As String = ""
        Select Case item.Cod
            Case DTONewsletterSource.Cods.News, DTONewsletterSource.Cods.Events
                Dim oNoticia As DTONoticia = Nothing
                If TypeOf item.Tag Is Newtonsoft.Json.Linq.JObject Then
                    oNoticia = item.Tag.toObject(Of DTONoticia)
                Else
                    oNoticia = item.Tag
                End If
                retval = FEB2.Noticia.UrlFriendly(oNoticia, AbsoluteUrl)
            Case DTONewsletterSource.Cods.Blog
                Dim oBlogPost As DTOBlogPost = Nothing
                If TypeOf item.Tag Is Newtonsoft.Json.Linq.JObject Then
                    oBlogPost = item.Tag.toObject(Of DTOBlogPost)
                Else
                    oBlogPost = item.Tag
                End If
                retval = oBlogPost.Url.AbsoluteUrl(DTOLang.ESP)
            Case DTONewsletterSource.Cods.Promo
                Dim oIncentiu As DTOIncentiu = Nothing
                If TypeOf item.Tag Is Newtonsoft.Json.Linq.JObject Then
                    oIncentiu = item.Tag.toObject(Of DTOIncentiu)
                Else
                    oIncentiu = item.Tag
                End If
                retval = FEB2.Incentiu.Url(oIncentiu, True)
        End Select
        Return retval
    End Function

    Shared Function ImageUrl(Source As DTONewsletterSource, Optional AbsoluteUrl As Boolean = False) As String
        Dim retval As String = ""
        Select Case Source.Cod
            Case DTONewsletterSource.Cods.News, DTONewsletterSource.Cods.Events
                Dim oNoticia As DTONoticia = Nothing
                If TypeOf Source.Tag Is Newtonsoft.Json.Linq.JObject Then
                    oNoticia = Source.Tag.toObject(Of DTONoticia)
                Else
                    oNoticia = Source.Tag
                End If
                retval = UrlHelper.Image(DTO.Defaults.ImgTypes.News265x150, oNoticia.Guid, AbsoluteUrl)
            Case DTONewsletterSource.Cods.Blog
                Dim oBlogPost As DTOBlogPost = Nothing
                If TypeOf Source.Tag Is Newtonsoft.Json.Linq.JObject Then
                    oBlogPost = Source.Tag.toObject(Of DTOBlogPost)
                Else
                    oBlogPost = Source.Tag
                End If
                retval = oBlogPost.ThumbnailUrl()
            Case DTONewsletterSource.Cods.Promo
                Dim oIncentiu As DTOIncentiu = Nothing
                If TypeOf Source.Tag Is Newtonsoft.Json.Linq.JObject Then
                    oIncentiu = Source.Tag.toObject(Of DTOIncentiu)
                Else
                    oIncentiu = Source.Tag
                End If
                retval = UrlHelper.Image(DTO.Defaults.ImgTypes.Incentiu, oIncentiu.Guid, AbsoluteUrl)
        End Select
        Return retval
    End Function
End Class

Public Class Newsletters
    Inherits _FeblBase

    Shared Async Function All(exs As List(Of Exception)) As Task(Of List(Of DTONewsletter))
        Return Await Api.Fetch(Of List(Of DTONewsletter))(exs, "Newsletters")
    End Function

End Class
