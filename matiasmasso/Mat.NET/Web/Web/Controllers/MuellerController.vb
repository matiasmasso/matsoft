Public Class MuellerController
    Inherits _MatController

    Function Index() As ActionResult
        ViewBag.MetaDescription = MyBase.DealerDescription
        Return View()
    End Function

End Class
