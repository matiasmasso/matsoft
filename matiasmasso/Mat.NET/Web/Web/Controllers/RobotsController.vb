Public Class RobotsController
    Inherits _MatController

    Function Index()
        Dim domain = ContextHelper.Domain
        Response.ContentType = "text/plain"
        Return View("robots", domain)
    End Function

End Class
