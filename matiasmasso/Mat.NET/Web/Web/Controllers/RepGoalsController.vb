Public Class RepGoalsController
    Inherits _MatController


    Function RepGoals1() As ActionResult
        Dim retval As ActionResult = Nothing

        Select Case MyBase.Authorize({DTORol.Ids.SuperUser,
                                               DTORol.Ids.Admin,
                                               DTORol.Ids.Accounts,
                                               DTORol.Ids.SalesManager,
                                               DTORol.Ids.Rep,
                                               DTORol.Ids.Comercial})
            Case AuthResults.success
                Dim Model As DTORep = BLLUser.GetRep(MyBase.GetSession.User)
                retval = View("RepGoals", Model)
            Case AuthResults.login
                retval = LoginOrView()
            Case AuthResults.denied
                retval = MyBase.UnauthorizedView()
        End Select

        Return retval
    End Function

    Function RepGoals2(guid As Guid) As ActionResult
        Dim retval As ActionResult = Nothing

        Select Case MyBase.Authorize({DTORol.Ids.SuperUser,
                                               DTORol.Ids.Admin,
                                               DTORol.Ids.Accounts,
                                               DTORol.Ids.SalesManager,
                                               DTORol.Ids.Rep,
                                               DTORol.Ids.Comercial})
            Case AuthResults.success
                Dim Model As DTORep = BLLRep.Find(guid)
                retval = View("RepGoals", Model)
            Case AuthResults.login
                retval = LoginOrView()
            Case AuthResults.denied
                retval = MyBase.UnauthorizedView()
        End Select

        Return retval
    End Function
End Class
