Public Class QuizAdvansafixController
    Inherits _MatController

    Function SignIn() As ActionResult
        Dim retval As ActionResult = Nothing
        Dim oUser As DTOUser = MyBase.GetSession.User
        Select Case oUser.Rol.Id
            Case DTORol.Ids.SuperUser, DTORol.Ids.Admin, DTORol.Ids.SalesManager, DTORol.Ids.Comercial, DTORol.Ids.Rep, DTORol.Ids.Operadora, DTORol.Ids.CliFull, DTORol.Ids.CliLite
                retval = View("SignUp", oUser)
            Case DTORol.Ids.Unregistered
                retval = LoginOrView("SignUp")
            Case Else
                retval = MyBase.UnauthorizedView()
        End Select
        Return retval
    End Function

    Function SignUp(guid As Guid) As ActionResult
        Dim retval As ActionResult = Nothing
        Dim oUser As DTOUser = BLL.BLLUser.Find(guid)
        LogBrowse(oUser)
        retval = View(oUser)
        Return retval
    End Function

    Function Pendiente() As ActionResult
        Dim retval As ActionResult = Nothing
        Dim oUser As DTOUser = MyBase.GetSession.User
        Select Case oUser.Rol.Id
            Case DTORol.Ids.SuperUser, DTORol.Ids.Admin, DTORol.Ids.SalesManager, DTORol.Ids.Comercial, DTORol.Ids.Rep, DTORol.Ids.Operadora
                retval = View()
            Case DTORol.Ids.Unregistered
                retval = LoginOrView("Pendiente")
            Case Else
                retval = MyBase.UnauthorizedView()
        End Select
        Return retval
    End Function

    Private Sub LogBrowse(oUser As DTOUser)
        Dim oCustomers As List(Of DTOCustomer) = BLL.BLLQuizAdvansafix.Customers(oUser)
        For Each item As DTOCustomer In oCustomers
            Dim oQuiz As DTOQuizAdvansafix = BLL.BLLQuizAdvansafix.FromCustomer(oCustomers(0))
            BLL.BLLQuizAdvansafix.Load(oQuiz)
            oQuiz.FchBrowse = Now
            oQuiz.LastUser = oUser
            Dim exs As New List(Of Exception)
            BLL.BLLQuizAdvansafix.Update(oQuiz, exs)
        Next
    End Sub


    Function LoadCustomer(guid As Guid) As PartialViewResult
        Dim oCustomer As New DTOCustomer(guid)
        Dim oQuiz As DTOQuizAdvansafix = BLL.BLLQuizAdvansafix.FromCustomer(oCustomer)
        BLL.BLLQuizAdvansafix.Load(oQuiz)
        Dim retval As PartialViewResult = PartialView("SignUp_", oQuiz)
        Return retval
    End Function



    Function Save(user As Guid, customer As Guid, NoSICT As Integer, SICT As Integer) As JsonResult
        Dim oCustomer As New DTOCustomer(customer)
        Dim oQuiz As DTOQuizAdvansafix = BLL.BLLQuizAdvansafix.FromCustomer(oCustomer)
        BLL.BLLQuizAdvansafix.Load(oQuiz)
        With oQuiz
            .QtyNoSICT = NoSICT
            .QtySICT = SICT
            .LastUser = New DTOUser(user)
            .FchConfirmed = Now
        End With

        Dim myData As Object = Nothing
        Dim exs As New List(Of Exception)
        If BLLQuizAdvansafix.Update(oQuiz, exs) Then
            myData = New With {.result = "OK", .template = CInt(BLL.BLLMailing.Templates.QuizConfirmationAdvansafix), .param1 = oQuiz.Guid.ToString}
        Else
            myData = New With {.result = "KO", .message = BLL.Defaults.ExsToMultiline(exs)}
        End If

        Dim retval As JsonResult = Json(myData, JsonRequestBehavior.AllowGet)
        Return retval

    End Function

    Function MailConfirmation(user As Guid, customer As Guid) As JsonResult
        Dim oUser As DTOUser = BLLUser.Find(user)

        Dim oCustomer As New DTOCustomer(customer)
        Dim oQuiz As DTOQuizAdvansafix = BLL.BLLQuizAdvansafix.FromCustomer(oCustomer)
        BLL.BLLQuizAdvansafix.Load(oQuiz)

        Dim myData As Object
        Dim exs As New List(Of Exception)
        If BLLQuizAdvansafix.Send(oQuiz, oUser, exs) Then
            myData = New With {.result = "OK"}
        Else
            myData = New With {.result = "KO"}
        End If
        Dim retval As JsonResult = Json(myData, JsonRequestBehavior.AllowGet)
        Return retval
    End Function
End Class
