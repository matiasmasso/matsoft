Public Class Noticia

    Shared Function Find(oGuid As Guid) As DTONoticia
        Dim retval As DTONoticia = NoticiaLoader.Find(oGuid)
        Return retval
    End Function

    Shared Function Image265x150(oGuid As Guid) As ImageMime
        Return NoticiaLoader.Image265x150(oGuid)
    End Function


    'Noticia/Image265x150/

    Shared Function Load(ByRef oNoticia As DTONoticia) As Boolean
        Dim retval As Boolean = NoticiaLoader.Load(oNoticia)
        Return retval
    End Function

    Shared Function Update(oNoticia As DTONoticia, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = NoticiaLoader.Update(oNoticia, exs)
        Return retval
    End Function

    Shared Function Delete(oNoticia As DTONoticia, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = NoticiaLoader.Delete(oNoticia, exs)
        Return retval
    End Function

    Shared Function SearchByUrl(sUrlFriendlySegment As String) As DTONoticia
        Dim retval As DTONoticia = NoticiaLoader.SearchByUrl(sUrlFriendlySegment)
        Return retval
    End Function

    Shared Function UrlForIMat(oNoticia As DTONoticia, Optional AbsoluteUrl As Boolean = False) As String
        Dim retval As String = UrlHelper.Factory(True, "news", "iMat", oNoticia.Guid.ToString())
        Return retval.ToLower
    End Function

    Shared Function UrlThumbnail(oNoticia As DTONoticia, Optional AbsoluteUrl As Boolean = False) As String
        Dim retval As String = UrlHelper.Image(DTO.Defaults.ImgTypes.News265x150, oNoticia.Guid, AbsoluteUrl)
        Return retval
    End Function

    Shared Function IsAllowed(oEmp As DTOEmp, oUser As DTOUser, oNoticia As DTONoticia) As Boolean
        Dim exs As New List(Of Exception)
        Dim retval As Boolean

        If oNoticia.Professional Then
            If oUser IsNot Nothing Then
                Select Case oUser.Rol.Id
                    Case DTORol.Ids.SuperUser, DTORol.Ids.Admin, DTORol.Ids.Marketing, DTORol.Ids.Operadora, DTORol.Ids.SalesManager, DTORol.Ids.Taller
                        retval = True
                    Case DTORol.Ids.Denied, DTORol.Ids.Guest, DTORol.Ids.Lead, DTORol.Ids.NotSet, DTORol.Ids.Unregistered
                        retval = False
                    Case Else
                        Dim oProduct As DTOProduct = oNoticia.Product
                        If oProduct Is Nothing Then
                            retval = True
                        Else
                            Dim oUserBrands = BEBL.ProductBrands.All(oEmp, oUser)
                            Dim oProductBrand = BEBL.Product.Brand(oProduct)
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
    Shared Function All(oSrc As DTONoticia.Srcs) As List(Of DTONoticia)
        Dim retval As List(Of DTONoticia) = NoticiasLoader.All(oSrc)
        Return retval
    End Function

    Shared Function Headers(oSrc As DTONoticia.Srcs, Optional HidePro As Boolean = False, Optional OnlyVisible As Boolean = False) As List(Of DTONoticiaBase)
        Dim retval As List(Of DTONoticiaBase) = NoticiasLoader.Headers(oSrc, HidePro, OnlyVisible)
        Return retval
    End Function

    Shared Function HeadersForSitemap(oEmp As DTOEmp) As List(Of DTONoticia)
        Dim retval As List(Of DTONoticia) = NoticiasLoader.HeadersForSitemap(oEmp)
        Return retval
    End Function

    Shared Function LastNoticia(oUser As DTOUser, oLang As DTOLang, Optional oProduct As DTOProduct = Nothing) As DTONoticia
        Dim retval As DTONoticia = NoticiasLoader.NoticiaDestacada(oUser, oLang, oProduct)
        If retval Is Nothing Then
            retval = NoticiasLoader.LastNoticia(oUser, oLang, oProduct)
        End If
        Return retval
    End Function

    Shared Function LastPost(oUser As DTOUser, oLang As DTOLang) As DTOPost 'for iMat SwiftUI
        Dim retval As DTOPost = Nothing
        Dim oNoticia As DTONoticia = LastNoticia(oUser, oLang)
        If oNoticia IsNot Nothing Then
            retval = New DTOPost(oNoticia.Guid)
            With retval
                .title = oNoticia.title.Tradueix(oLang)
                .excerpt = oNoticia.excerpt.Tradueix(oLang)
                .url = oNoticia.Url.AbsoluteUrl(oLang)
                .thumbnailUrl = oNoticia.ThumbnailUrl(True)
            End With
        End If
        Return retval
    End Function

    Shared Function LastNoticias(oUser As DTOUser, Optional oLang As DTOLang = Nothing, Optional oProduct As DTOProduct = Nothing, Optional take As Integer = 0) As List(Of DTONoticia)
        Dim retval As List(Of DTONoticia) = NoticiasLoader.LastVisibleNoticiaHeaders(oUser, oLang, oProduct, take)
        Return retval
    End Function

    Shared Function Compact(oLang As DTOLang, HidePro As Boolean) As List(Of DTOContent.Compact)
        Return NoticiasLoader.Compact(oLang, HidePro)
    End Function

    Shared Function FromCategoria(oEmp As DTOEmp, oUser As DTOUser, oCategoria As DTOCategoriaDeNoticia) As List(Of DTONoticia) 'Views\news\xCategoria.vbhtml
        Dim retVal As List(Of DTONoticia) = NoticiasLoader.
            FromCategoria(oCategoria).
            Where(Function(x) BEBL.Noticia.IsAllowed(oEmp, oUser, x)).
            ToList

        Return retVal
    End Function

    Shared Function NextEvents(oUser As DTOUser, OnlyVisible As Boolean, iTop As Integer) As List(Of DTONoticia) 'Views\news\Index.vbhtml
        Dim retVal As List(Of DTONoticia) = NoticiasLoader.
            LastNews(DTOContent.Srcs.Eventos, oUser, OnlyVisible, iTop).
            Where(Function(x) BEBL.Noticia.IsAllowed(oUser.Emp, oUser, x)).
            ToList
        Return retVal
    End Function

    Shared Function Destacats(oSrc As DTONoticiaBase.Srcs, oUser As DTOUser) As List(Of DTONoticia)
        Dim retVal As List(Of DTONoticia) = NoticiasLoader.Destacats(oSrc, oUser)
        Return retVal
    End Function


End Class
