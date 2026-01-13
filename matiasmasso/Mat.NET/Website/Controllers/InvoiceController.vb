Public Class InvoiceController
    Inherits _MatController

    Function Index2(guid As Guid) As ActionResult
        Dim retval As ActionResult = Nothing
        Select Case MyBase.Authorize({DTORol.Ids.SuperUser,
                                               DTORol.Ids.Admin,
                                               DTORol.Ids.SalesManager,
                                               DTORol.Ids.Marketing,
                                               DTORol.Ids.Accounts,
                                               DTORol.Ids.Manufacturer,
                                               DTORol.Ids.Comercial,
                                               DTORol.Ids.Rep,
                                               DTORol.Ids.CliFull,
                                               DTORol.Ids.CliLite})
            Case AuthResults.success
                Dim exs As New List(Of Exception)
                Dim Model As DTOInvoice = Nothing ' Await FEB.Invoice.Find(guid, exs)
                If exs.Count = 0 Then
                    retval = View(Model)
                Else
                    retval = View("Error")
                End If
            Case AuthResults.login
                retval = LoginOrView()
            Case AuthResults.denied
                retval = MyBase.UnauthorizedView()
        End Select
        Return retval

    End Function
    Async Function Index(guid As Guid) As Threading.Tasks.Task(Of ActionResult)
        Dim retval As ActionResult = Nothing
        Select Case MyBase.Authorize({DTORol.Ids.SuperUser,
                                               DTORol.Ids.Admin,
                                               DTORol.Ids.SalesManager,
                                               DTORol.Ids.Marketing,
                                               DTORol.Ids.Accounts,
                                               DTORol.Ids.Manufacturer,
                                               DTORol.Ids.Comercial,
                                               DTORol.Ids.Rep,
                                               DTORol.Ids.CliFull,
                                               DTORol.Ids.CliLite})
            Case AuthResults.success
                Dim exs As New List(Of Exception)
                Dim Model As DTOInvoice = Await FEB.Invoice.Find(guid, exs)
                If exs.Count = 0 Then
                    retval = View("Invoice", Model)
                Else
                    retval = View("Error")
                End If
            Case AuthResults.login
                retval = LoginOrView()
            Case AuthResults.denied
                retval = MyBase.UnauthorizedView()
        End Select
        Return retval

    End Function

End Class