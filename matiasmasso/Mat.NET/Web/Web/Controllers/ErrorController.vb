Public Class ErrorController
    Inherits _MatController

    Async Function Index() As Threading.Tasks.Task(Of ActionResult)
        ViewBag.Title = ContextHelper.Tradueix("Error", "Error", "Error", "Erro")
        Dim model = Await LogWebErr(DTOWebErr.Cods.ManagedErr, "managed Error")
        Return View("Error", model)
    End Function

    Async Function PartialError() As Threading.Tasks.Task(Of ActionResult)
        Dim model = Await LogWebErr(DTOWebErr.Cods.ManagedErr, "managed Error")
        Return View("_Error")
    End Function

    Async Function PageNotFound() As Threading.Tasks.Task(Of ActionResult)
        ViewBag.Title = ContextHelper.Tradueix("Página no encontrada", "No s'ha trobat la pàgina", "Page not found", "Página não encontrada")
        Dim model = Await LogWebErr(DTOWebErr.Cods.PageNotFound, "Page Not Found")
        Return View(model)
    End Function

    Async Function Err403() As Threading.Tasks.Task(Of ActionResult)
        ViewBag.Title = ContextHelper.Tradueix("Error", "Error", "Error", "Erro")
        Dim model = Await LogWebErr(DTOWebErr.Cods.Err403, "Err 403")
        Return View("Error", model)
    End Function

    Async Function Err404() As Threading.Tasks.Task(Of ActionResult)
        ViewBag.Title = ContextHelper.Tradueix("Error", "Error", "Error", "Erro")
        Dim model = Await LogWebErr(DTOWebErr.Cods.Err404, "Err 404")
        Return View("Error", model)
    End Function

    Async Function Err500() As Threading.Tasks.Task(Of ActionResult)
        ViewBag.Title = ContextHelper.Tradueix("Error", "Error", "Error", "Erro")
        Dim model = Await LogWebErr(DTOWebErr.Cods.Err500, "Err 500")
        Return View("Error", model)
    End Function

    Function Msgbody(exs As List(Of Exception)) As String
        Dim oErrorDictionary = ErrorDictionary(exs)
        Dim sb As New Text.StringBuilder
        sb.AppendLine("<table border='1' style='font-family:arial,sans-serif;border:1px solid grey;border-collapse: collapse;'>")
        For Each item In oErrorDictionary
            sb.AppendLine("<tr style='vertical-align:top'>")
            If item.Value = "**" Then
                sb.AppendLine("<td style='font-weight:600;padding:20px 0 10px 0'>")
            Else
                sb.AppendLine("<td>")
            End If
            sb.AppendLine(item.Key)
            sb.AppendLine("</td>")
            sb.AppendLine("<td>")
            If item.Value = "**" Then
                sb.AppendLine("&nbsp;")
            Else
                sb.AppendLine(item.Value)
            End If
            sb.AppendLine("</td>")
            sb.AppendLine("</tr>")
        Next
        sb.AppendLine("</table>")
        Return sb.ToString
    End Function

    Function ErrorDictionary(exs As List(Of Exception)) As Dictionary(Of String, String)
        Dim retval As New Dictionary(Of String, String)
        Dim oUser = ContextHelper.GetUser()

        retval.Add("Exceptions", "**")
        Dim i As Integer
        If exs IsNot Nothing Then
            For Each ex In exs
                i += 1
                retval.Add("ex.Message " & i.ToString, ex.Message)
            Next
        End If

        retval.Add("Source", "**")
        If Request.UrlReferrer Is Nothing Then
            retval.Add("Url referrer", "(not available)")
        Else
            retval.Add("Url referrer", Request.UrlReferrer.ToString())
        End If
        retval.Add("Host", Me.Request.UserHostAddress)
        retval.Add("Browser", String.Format("{0} {1}", Me.Request.Browser.Browser, Me.Request.Browser.Version))

        retval.Add("User", "**")
        If oUser Is Nothing Then
            retval.Add("Nom", "(anonim)")
        Else
            retval.Add("Guid", oUser.Guid.ToString)
            retval.Add("Email", oUser.EmailAddress)
            retval.Add("Nickname", oUser.NickName)
            retval.Add("Nom", oUser.Nom & " " & oUser.Cognoms)
            retval.Add("Rol", String.Format("{0} -> {1}", oUser.Rol.id, oUser.Rol.nom.Tradueix(DTOLang.CAT)))
        End If

        retval.Add("Server Variables", "**")
        For Each name In Request.ServerVariables
            retval.Add(name, Request.ServerVariables(name))
        Next

        retval.Add("Trace", "**")

        If exs IsNot Nothing Then
            For Each ex In exs
                retval.Add("ex.StackTrace " & exs.IndexOf(ex), ex.StackTrace)
            Next
        End If
        Return retval
    End Function
End Class
