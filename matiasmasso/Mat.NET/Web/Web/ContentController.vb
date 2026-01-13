Imports System.Web.Configuration

Public Class ContentController
    Inherits _MatController
    Async Function Index(id As String) As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim oContent As DTOContent = Nothing

        Select Case id.ToLower
            Case "avisolegal"
                oContent = DTOContent.Wellknown(DTOContent.Wellknowns.avisoLegal)
                With oContent
                    .Title = Await FEB2.LangText.Find(exs, oContent.Guid, DTOLangText.Srcs.ContentTitle)
                    .Text = Await FEB2.LangText.Find(exs, oContent.Guid, DTOLangText.Srcs.ContentText)
                End With
            Case "privacidad"
                oContent = DTOContent.Wellknown(DTOContent.Wellknowns.privacidad)
                With oContent
                    .Title = Await FEB2.LangText.Find(exs, oContent.Guid, DTOLangText.Srcs.ContentTitle)
                    .Text = Await FEB2.LangText.Find(exs, oContent.Guid, DTOLangText.Srcs.ContentText)
                End With
            Case "cookies"
                oContent = DTOContent.Wellknown(DTOContent.Wellknowns.cookies)
                With oContent
                    .Title = Await FEB2.LangText.Find(exs, oContent.Guid, DTOLangText.Srcs.ContentTitle)
                    .Text = Await FEB2.LangText.Find(exs, oContent.Guid, DTOLangText.Srcs.ContentText)
                End With
            Case Else
                oContent = Await FEB2.Noticia.SearchByUrl(exs, id)
        End Select

        If oContent Is Nothing Then
            Return ErrorResult(New Exception(String.Format("Content '{0}' not found", id)))
        Else
            If exs.Count = 0 Then
                ViewBag.Title = oContent.Title.tradueix(Mvc.ContextHelper.Lang)
                Return View("Content", oContent)
            Else
                Return ErrorResult(exs)
            End If
        End If
    End Function
End Class
