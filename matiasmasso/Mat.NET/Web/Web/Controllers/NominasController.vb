Public Class NominasController
    Inherits _MatController


    Async Function Index() As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim retval As ActionResult = Nothing
        Dim oUser = ContextHelper.GetUser()
        Dim oNominas = Await FEB2.Nominas.All(exs, oUser)
        If exs.Count = 0 Then
            Dim model = DTONomina.Header.Collection.Factory(oNominas)
            ViewBag.Title = ContextHelper.Lang.Tradueix("Nóminas", "Nómines", "Payrolls", "Folhas de pagamento")
            retval = LoginOrView("Nominas", Model)
        Else
            retval = Await ErrorResult(exs)
        End If
        Return retval
    End Function

    Async Function FromYear(year As Integer) As Threading.Tasks.Task(Of PartialViewResult)
        Dim exs As New List(Of Exception)
        Dim oUser = ContextHelper.GetUser()
        Dim Model As New DTOExtracte
        With Model
            .Cta = Await FEB2.PgcCta.FromCod(DTOPgcPlan.Ctas.Nomina, GlobalVariables.Emp, exs)
            .Contact = Await FEB2.User.GetStaff(exs, oUser)
            .Exercici = DTOExercici.FromYear(GlobalVariables.Emp, year)
        End With
        Dim retval As PartialViewResult = PartialView("Nominas_", Model)
        Return retval
    End Function

End Class