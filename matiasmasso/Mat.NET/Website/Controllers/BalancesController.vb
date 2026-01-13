Imports System.Threading.Tasks

Public Class BalancesController
    Inherits _MatController

    Async Function Index(guid As Nullable(Of Guid)) As Task(Of ActionResult)
        Dim retval As ActionResult = Nothing

        Select Case MyBase.Authorize({DTORol.Ids.SuperUser,
                                               DTORol.Ids.Admin,
                                               DTORol.Ids.Accounts,
                                               DTORol.Ids.Auditor,
                                               DTORol.Ids.Banc,
                                               DTORol.Ids.Manufacturer})
            Case AuthResults.success
                Dim exs As New List(Of Exception)
                Dim DtFch As Date
                Dim sFch As String = Await FEB.Default.EmpValue(Website.GlobalVariables.Emp, DTODefault.Codis.LastBalanceQuadrat, exs)
                If IsNumeric(sFch) Then
                    DtFch = Date.FromOADate(sFch)
                Else
                    DtFch = DTO.GlobalVariables.Today()
                End If
                Dim Model As List(Of DTOPgcClass) = Await FEB.Balance.Tree(Website.GlobalVariables.Emp, DtFch)
                ViewBag.Fch = DtFch
                ViewBag.Title = String.Format("{0} {1:dd/MM/yy}", ContextHelper.Tradueix("Cuentas Anuales a", "Comptes anuals a", "Yearly Accounts on"), DtFch)
                retval = View("Balances", Model)
            Case AuthResults.login
                retval = LoginOrView()
            Case AuthResults.denied
                retval = MyBase.UnauthorizedView()
        End Select

        Return retval
    End Function

End Class
