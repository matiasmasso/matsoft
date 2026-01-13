Public Class AeatMod349Controller
    Inherits _MatController

    Function Index() As ActionResult
        Dim retval As ActionResult = Nothing

        Dim oUser As DTOUser = MyBase.GetSession.User
        Select Case oUser.Rol.Id
            Case DTORol.Ids.SuperUser, DTORol.Ids.Admin, DTORol.Ids.Accounts, DTORol.Ids.Auditor
                Dim Model As Integer = Today.Year
                retval = View("AeatMod349", Model)
            Case DTORol.Ids.Unregistered
                retval = LoginOrView("AeatMod349")
            Case Else
                retval = MyBase.UnauthorizedView()
        End Select

        Return retval
    End Function

    Function FromYear(year As Integer) As PartialViewResult
        Dim Model As List(Of DTOModel349) = BLL.BLLModel349.All(year)
        Return PartialView("AeatMod349_", Model)
    End Function

End Class
