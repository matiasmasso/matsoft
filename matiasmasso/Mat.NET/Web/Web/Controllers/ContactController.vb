Public Class ContactController
    Inherits _MatController

    '
    ' GET: /Contact

    Async Function Index(guid As Guid) As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim retval As ActionResult = Nothing
        Select Case MyBase.Authorize({DTORol.Ids.SuperUser,
                                               DTORol.Ids.Admin,
                                               DTORol.Ids.LogisticManager,
                                               DTORol.Ids.Marketing,
                                               DTORol.Ids.Operadora,
                                               DTORol.Ids.SalesManager,
                                               DTORol.Ids.Comercial,
                                               DTORol.Ids.Rep})
            Case AuthResults.success
                Dim pass As Boolean = False
                Dim oUser As DTOUser = ContextHelper.GetUser()
                Dim Model As DTOContact = Await FEB2.Contact.Find(guid, exs)
                Select Case oUser.Rol.Id
                    Case DTORol.Ids.SuperUser, DTORol.Ids.Admin
                        pass = True
                    Case Else
                        Select Case Model.Rol.Id
                            Case DTORol.Ids.Rep, DTORol.Ids.Comercial
                                Select Case oUser.Rol.Id
                                    Case DTORol.Ids.SuperUser, DTORol.Ids.Admin, DTORol.Ids.SalesManager
                                        pass = True
                                    Case DTORol.Ids.Rep, DTORol.Ids.Comercial
                                        Dim oRep = Await FEB2.User.GetRep(oUser, exs)
                                        pass = Model.Equals(oRep)
                                End Select
                            Case DTORol.Ids.CliFull, DTORol.Ids.CliLite
                                Select Case oUser.Rol.Id
                                    Case DTORol.Ids.Comercial, DTORol.Ids.Rep, DTORol.Ids.SalesManager
                                        pass = True
                                End Select
                        End Select

                End Select

                If pass Then
                    Dim sTitle As String = ""
                    Select Case Model.Rol.id
                        Case DTORol.Ids.cliFull, DTORol.Ids.cliLite
                            sTitle = Mvc.ContextHelper.Tradueix("Cliente", "Client", "Customer")
                        Case DTORol.Ids.comercial
                            sTitle = Mvc.ContextHelper.Tradueix("Comercial", "Comercial", "Salesman")
                        Case DTORol.Ids.rep
                            sTitle = Mvc.ContextHelper.Tradueix("Representante", "Representant", "Rep")
                        Case Else
                            sTitle = Mvc.ContextHelper.Tradueix("Contacto", "Contacte", "Contact")
                    End Select

                    ViewBag.Title = sTitle
                    retval = View("SingleContact", Model)
                Else
                    retval = MyBase.UnauthorizedView()
                End If
            Case AuthResults.login
                retval = LoginOrView()
            Case AuthResults.denied
                retval = MyBase.UnauthorizedView()
        End Select

        Return retval
    End Function



End Class