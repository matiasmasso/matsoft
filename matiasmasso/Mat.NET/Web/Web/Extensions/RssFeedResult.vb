Imports System.ServiceModel.Syndication
Imports System.Xml

Public Class RssFeedResult
    Inherits FileResult

    Private _currentUrl As Uri
    Private ReadOnly _title As String
    Private ReadOnly _description As String
    Private ReadOnly _items As List(Of SyndicationItem)

    Public Sub New(ByVal contentType As String, ByVal title As String, ByVal description As String, ByVal items As List(Of SyndicationItem))
        MyBase.New(contentType)
        _title = title
        _description = description
        _items = items
    End Sub

    Protected Overrides Sub WriteFile(ByVal response As HttpResponseBase)
        Dim feed = New SyndicationFeed(title:=Me._title, description:=_description, feedAlternateLink:=_currentUrl, items:=Me._items)
        Dim formatter = New Rss20FeedFormatter(feed)

        Using writer = XmlWriter.Create(response.Output)
            formatter.WriteTo(writer)
        End Using
    End Sub

    Public Overrides Sub ExecuteResult(ByVal context As ControllerContext)
        _currentUrl = context.RequestContext.HttpContext.Request.Url
        MyBase.ExecuteResult(context)
    End Sub
End Class