Public Class DocsFinanciersController

    Inherits _MatController

    Function Index() As ActionResult
        Dim retval As ActionResult = Nothing

        Select Case MyBase.Authorize({DTORol.Ids.SuperUser, DTORol.Ids.Admin, DTORol.Ids.Banc, DTORol.Ids.Auditor, DTORol.Ids.Accounts})
            Case AuthResults.success
                retval = View("DocsFinanciers")
            Case AuthResults.login
                retval = MyBase.Login()
            Case AuthResults.denied
                retval = MyBase.UnauthorizedView()
        End Select

        Return retval
    End Function
End Class