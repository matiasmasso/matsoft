Public Class HelpController
    Inherits System.Web.Mvc.Controller

    Function Index() As ActionResult
        ViewData("Title") = "Help Page"

        Return View()
    End Function





End Class
