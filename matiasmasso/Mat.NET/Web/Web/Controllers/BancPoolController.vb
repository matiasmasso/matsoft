Public Class BancPoolController
    Inherits _MatController

    Function Index() As ActionResult
        Dim retval As ActionResult = Nothing

        Dim oUser As DTOUser = MyBase.GetSession.User
        Select Case oUser.Rol.Id
            Case DTORol.Ids.SuperUser, DTORol.Ids.Admin, DTORol.Ids.Banc, DTORol.Ids.Auditor, DTORol.Ids.Accounts
                Dim Model As List(Of DTO.DTOBancPool) = BLLBancPools.All(, Today)
                retval = View("BancPool", Model)
            Case DTORol.Ids.Unregistered
                retval = LoginOrView("BancPool")
            Case Else
                retval = MyBase.UnauthorizedView()
        End Select

        Return retval
    End Function

    Function Detail(guid As Guid) As ActionResult

        Dim retval As ActionResult = Nothing

        Dim oUser As DTOUser = MyBase.GetSession.User
        Select Case oUser.Rol.Id
            Case DTORol.Ids.SuperUser, DTORol.Ids.Admin, DTORol.Ids.Banc, DTORol.Ids.Auditor, DTORol.Ids.Accounts
                Dim Model As New DTOBank(guid)
                retval = View("BancPoolDetail", Model)
            Case DTORol.Ids.Unregistered
                retval = LoginOrView("BancPool")
            Case Else
                retval = MyBase.UnauthorizedView()
        End Select

        Return retval
    End Function

End Class
