Public Class PostCommentController
    Inherits _MatController

    Async Function Index(id As Guid) As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim retval As ActionResult = Nothing

        Dim oComment = Await FEB.PostComment.Find(exs, id)
        If exs.Count = 0 Then
            If oComment Is Nothing Then
                retval = Await ErrorNotFoundResult()
            Else
                Select Case oComment.ParentSource
                    Case DTOPostComment.ParentSources.Blog
                        If oComment.Parent.Equals(DTOContent.Wellknown(DTOContent.Wellknowns.consultasBlog).Guid) Then
                            retval = RedirectToAction("Consultas", "BlogPost", New With {.returnPostGuid = oComment.Guid})
                        Else
                            retval = RedirectToAction("WithComment", "BlogPost", New With {.id = oComment.Guid})
                        End If
                    Case DTOPostComment.ParentSources.News
                        retval = Await ErrorNotFoundResult()
                    Case DTOPostComment.ParentSources.Noticia
                        retval = Redirect(String.Format("/noticias/{0}", oComment.Parent.ToString()))
                        'retval = Await ErrorNotFoundResult()
                    Case Else
                        retval = Await ErrorNotFoundResult()
                End Select
            End If
        Else
            retval = Await ErrorResult(exs)
        End If

        Return retval
    End Function
End Class
