Imports System.ServiceModel.Syndication

Public Class FeedController
    Inherits _MatController

    Public Async Function Index() As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim items = New List(Of SyndicationItem)()
        Dim oDomain = ContextHelper.Domain
        Dim oLang = oDomain.DefaultLang
        Dim oNoticias = Await FEB.Noticias.LastNoticias(exs, DTOUser.Wellknown(DTOUser.Wellknowns.info), oLang)
        Dim oLastYearNoticias = oNoticias.Where(Function(x) x.Fch > DTO.GlobalVariables.Today().AddMonths(-12) And x.professional = False).ToList()
        For Each oNoticia In oNoticias
            Dim item = oNoticia.Rss(oDomain)
            items.Add(item)
        Next
        Dim oBlogPosts = Await FEB.BlogPosts.All(exs, oLang)
        Dim oLastBlogPosts = oBlogPosts.Where(Function(x) x.Fch > DTO.GlobalVariables.Today().AddMonths(-12)).ToList()
        For Each oBlogPost In oLastBlogPosts
            Dim item = oBlogPost.Rss(oDomain)
            items.Add(item)
        Next

        Dim retval = New RssFeedResult(title:=oLang.Tradueix("El blog de Matías Massó", "El blog de Matías Massó", "Matías Massó blog", "O blog de Matias Massó"),
                       items:=items.OrderByDescending(Function(x) x.PublishDate).ToList(),
                       contentType:="application/rss+xml",
                       description:="MATIAS MASSO, S.A. importa marcas líderes en puericultura para su distribución en los mercados Español, Portugués y de Andorra.")
        Return retval
    End Function

    Public Async Function Comments() As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim items = New List(Of SyndicationItem)()
        Dim oDomain = ContextHelper.Domain
        Dim oLang = oDomain.DefaultLang
        Dim fchFrom = DTO.GlobalVariables.Today().AddMonths(-6)
        Dim oComments = Await FEB.PostComments.ForFeed(exs, fchFrom, oDomain.Id)
        For Each oComment In oComments
            Dim item = oComment.Rss(oDomain)
            items.Add(item)
        Next

        Dim retval = New RssFeedResult(title:="Matias Masso comments",
                       items:=items.OrderByDescending(Function(x) x.PublishDate).ToList(),
                       contentType:="application/rss+xml",
                       description:="MATIAS MASSO, S.A. importa marcas líderes en puericultura para su distribución en los mercados Español, Portugués y de Andorra.")
        Return retval
    End Function
End Class
