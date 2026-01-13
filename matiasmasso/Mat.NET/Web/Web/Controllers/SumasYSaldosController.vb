Public Class SumasYSaldosController
    Inherits _MatController

    Async Function Summary(oEmp As DTOEmp) As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim oExercici As DTOExercici = DTOExercici.Current(oEmp)
        Dim Model As DTOContact = Nothing
        Dim retval As ActionResult

        Dim oUser = ContextHelper.GetUser()
        If oUser IsNot Nothing Then
            ViewBag.Title = ContextHelper.Tradueix("Sumas y saldos", "Sumes i saldos", "Sums and balances")
            Select Case oUser.Rol.id
                Case DTORol.Ids.superUser, DTORol.Ids.admin, DTORol.Ids.accounts, DTORol.Ids.auditor
                    retval = View("Summary", Model)
                Case DTORol.Ids.unregistered
                    retval = LoginOrView("Summary", Model)
                Case Else
                    retval = MyBase.UnauthorizedView()
            End Select
        Else
            retval = LoginOrView()
        End If

        Return retval
    End Function

    Async Function FromYear(year As Integer) As Threading.Tasks.Task(Of PartialViewResult)
        Dim oExercici As DTOExercici = DTOExercici.FromYear(Mvc.GlobalVariables.Emp, year)
        Dim DtFch As New Date(oExercici.Year, 12, 31)
        Dim exs As New List(Of Exception)
        Dim Model As List(Of DTOBalanceSaldo) = Await FEB2.Balance.SumasySaldos(exs, oExercici, DtFch)
        ViewBag.fch = DtFch
        Return PartialView("_Summary", Model)
    End Function

    Async Function FromContact(guid As Guid) As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim oExercici As DTOExercici = DTOExercici.Current(GlobalVariables.Emp)
        Dim Model = Await FEB2.Contact.Find(guid, exs)
        Model.Emp = oExercici.Emp
        Return View("SumasYSaldos", Model)
    End Function

    Async Function FromYearContact(guid As Guid, year As Integer) As Threading.Tasks.Task(Of PartialViewResult)
        Dim exs As New List(Of Exception)
        Dim oExercici As DTOExercici = DTOExercici.FromYear(Mvc.GlobalVariables.Emp, year)
        Dim Model As List(Of DTOPgcSaldo)
        If guid = Nothing Then
            Model = Await FEB2.SumasYSaldos.Summary(exs, oExercici)
        Else
            Dim oContact As New DTOContact(guid)
            Model = Await FEB2.SumasYSaldos.All(exs, oExercici, False, DTO.Defaults.ContactRange.OnlyThisContact, oContact)
        End If
        Return PartialView("_SumasYSaldos", Model)
    End Function

    Async Function SubComptes(guid As Guid, year As Integer) As Threading.Tasks.Task(Of ActionResult)
        Dim retval As ActionResult = Nothing
        Dim exs As New List(Of Exception)
        Dim oExercici As DTOExercici = DTOExercici.FromYear(Mvc.GlobalVariables.Emp, year)
        Dim oCta = Await FEB2.PgcCta.Find(guid, exs)
        Dim oSubComptes = Await FEB2.SumasYSaldos.SubComptes(exs, oExercici, oCta)
        If oSubComptes.Count = 1 Then
            If oSubComptes(0).Contact Is Nothing Then
                'Dim oExtracte As DTOPgcExtracte = BLL.BLLPgcExtracte.Load(year, guid)
                'retval = View("Extracte", oExtracte)
            Else
                Dim oSaldo = DTOPgcSaldo.Factory(oExercici, oCta)
                retval = View("SubComptes", oSaldo)
            End If
        Else
            Dim oSaldo = DTOPgcSaldo.Factory(oExercici, oCta)
            retval = View("SubComptes", oSaldo)
        End If
        Return retval
    End Function

End Class