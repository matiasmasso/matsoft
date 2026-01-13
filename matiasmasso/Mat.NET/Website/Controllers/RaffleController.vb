
Imports MatHelperStd

Public Class RaffleController
    Inherits _MatController

    Async Function Zoom(guid As Guid) As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim oLang = ContextHelper.Lang
        Dim oRaffle = Await FEB.Raffle.Find(guid, exs)
        ContextHelper.NavViewModel.LoadCustomMenu(oLang, oRaffle.Brand)
        If exs.Count = 0 Then
            Return View("raffle", oRaffle)
        Else
            Return View("error")
        End If
    End Function

    Async Function Play2(guid As Guid, user As Guid) As Threading.Tasks.Task(Of ActionResult)
        Dim retval As ActionResult = Nothing
        Dim exs As New List(Of Exception)

        Dim oUser = Await FEB.User.Find(user, exs)
        If exs.Count = 0 And oUser IsNot Nothing Then
            Dim oMenuGroups = Await FEB.MenuItems.Fetch(exs, oUser)
            Await MyBase.SignInUser(oUser, persist:=False)
            Await ContextHelper.SetNavViewModel(oUser)
            ViewBag.Title = ContextHelper.Tradueix("Inscripción en sorteo", "Inscripció en sorteig", "Raffle subscription", "Inscrição no sorteio")
            Dim oRaffle As New DTORaffle(guid)
            Dim oLang = ContextHelper.Lang
            Dim model As DTORafflePlayModel = Await FEB.RaffleParticipant.PlayModelFactory(exs, oRaffle, oUser, ContextHelper.Lang)
            model.Src = DTORafflePlayModel.Srcs.app
            ContextHelper.NavViewModel.ResetCustomMenu()
            If exs.Count = 0 Then
                retval = View("play", model)
            Else
                retval = Await ErrorResult(exs)
            End If
        Else
            retval = Await ErrorNotFoundResult()
        End If
        Return retval
    End Function

    Async Function Play(guid As Guid) As Threading.Tasks.Task(Of ActionResult)
        Dim retval As ActionResult = Nothing
        Dim exs As New List(Of Exception)
        ViewBag.Title = ContextHelper.Tradueix("Inscripción en sorteo", "Inscripció en sorteig", "Raffle subscription", "Inscrição no sorteio")

        Dim oRaffle As New DTORaffle(guid)
        Dim oUser = ContextHelper.GetUser()
        Dim oLang = ContextHelper.Lang

        Dim model As DTORafflePlayModel = Await FEB.RaffleParticipant.PlayModelFactory(exs, oRaffle, oUser, ContextHelper.Lang)
        ContextHelper.NavViewModel.ResetCustomMenu()
        If exs.Count = 0 Then
            retval = View("play", model)
        Else
            retval = Await ErrorResult(exs)
        End If
        Return retval
    End Function

    Async Function Share() As Threading.Tasks.Task(Of JsonResult)
        Dim exs As New List(Of Exception)
        Dim retval As JsonResult = Nothing
        Dim sGuid = Request("raffle")
        If GuidHelper.IsGuid(sGuid) Then
            Dim raffleGuid = New Guid(sGuid)
            Dim oTarget = New DTORaffle(raffleGuid)
            Dim oUser = MyBase.User()
            Dim oFeedback = DTOFeedback.Factory(oTarget, DTOFeedback.Cods.Share, oUser)
            Dim oGuid = Await FEB.Feedback.Update(exs, oFeedback)
            If exs.Count = 0 Then
                oFeedback.Guid = oGuid
                Dim oObj As New With {.success = True, .result = oFeedback}
                retval = Json(oObj, JsonRequestBehavior.AllowGet)
            Else
                Dim oObj As New With {.success = False, .message = ExceptionsHelper.ToFlatString(exs)}
                retval = Json(oObj, JsonRequestBehavior.AllowGet)
            End If
        Else
            Dim oObj As New With {.success = False, .message = "raffle empty"}
            retval = Json(oObj, JsonRequestBehavior.AllowGet)
        End If
        Return retval
    End Function

    Async Function Bases(guid As Guid) As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim oRaffle = Await FEB.Raffle.Find(guid, exs)
        If exs.Count = 0 Then
            ViewBag.Title = MatHelperStd.UrlHelper.Title(ContextHelper.Tradueix("Bases del Sorteo", "Bases del Sorteig", "Raffle Terms and Conditions", "Bases do Sorteio"))
            ViewBag.MetaDescription = oRaffle.Title
            Return View("Bases", oRaffle)
        Else
            Return View("error")
        End If
    End Function

    'Play _DEPRECATED ------------------------

    Async Function Locations_DEPRECATED(raffle As Guid, zonaGuid As Guid) As Threading.Tasks.Task(Of JsonResult)
        Dim exs As New List(Of Exception)
        Dim oRaffle = Await FEB.Raffle.Find(raffle, exs)
        Dim oZona = DTOArea.Factory(zonaGuid, DTOArea.Cods.Zona)
        Dim oLocations = Await FEB.Raffle.Locations(exs, Website.GlobalVariables.Emp, oRaffle, oZona)
        Dim retval As JsonResult = Json(oLocations, JsonRequestBehavior.AllowGet)
        Return retval
    End Function

    Async Function Distributors_DEPRECATED(raffle As Guid, locationGuid As Guid) As Threading.Tasks.Task(Of JsonResult)
        Dim exs As New List(Of Exception)
        Dim oRaffle = Await FEB.Raffle.Find(raffle, exs)
        Dim oLocation = DTOArea.Factory(locationGuid, DTOArea.Cods.Location)
        Dim oDistributors = Await FEB.Raffle.Distributors(exs, Website.GlobalVariables.Emp, oRaffle, oLocation)

        Dim oJSONArray As New List(Of Object)
        For Each oDistributor As DTOProductDistributor In oDistributors
            oJSONArray.Add(New With {.guid = oDistributor.Guid.ToString, .nom = oDistributor.Nom, .address = oDistributor.Adr, .telefon = DTOContactTel.CleanValue(oDistributor.Tel)})
        Next
        Dim retval As JsonResult = Json(oJSONArray, JsonRequestBehavior.AllowGet)
        Return retval
    End Function



    <HttpPost>
    Async Function Update(token As Guid, raffle As Guid, answer As Integer, distributor As Guid) As Threading.Tasks.Task(Of PartialViewResult)
        Dim exs As New List(Of Exception)
        Dim retval As PartialViewResult = Nothing
        Dim oRaffle As DTORaffle = Nothing
        Dim oUser As DTOUser = Nothing
        Dim myData As Object
        Try
            oRaffle = Await FEB.Raffle.Find(raffle, exs)
            oUser = Await FEB.User.Find(token, exs)
            myData = New With {.result = 0}

            If oRaffle IsNot Nothing Then
                Dim oDistributor As DTOContact = Nothing
                If distributor <> Nothing Then
                    oDistributor = New DTOContact(distributor)
                End If

                Dim oParticipant = DTORaffleParticipant.Factory(oRaffle, oUser, answer, oDistributor)
                Dim oUserInfo = Await FEB.User.Find(DTOUser.Wellknown(DTOUser.Wellknowns.info).Guid, exs)
                If Await FEB.RaffleParticipant.Enroll(oParticipant, oUserInfo, Website.GlobalVariables.Emp, exs) Then
                    myData = New With {.success = 1, .participant = oParticipant.Guid.ToString}
                Else
                    myData = New With {.success = 2}
                End If
            End If

            'retval = Json(myData, JsonRequestBehavior.AllowGet)
            retval = PartialView("Raffle_Thanks", oRaffle)

        Catch ex As Exception
            Dim sObs As String = ex.Message
            If oUser Is Nothing Then
                sObs = sObs & " usuari is nothing"
            Else
                sObs = sObs & " usuari = " & oUser.EmailAddress & " | usuari.guid = " & oUser.Guid.ToString
            End If
            ContextHelper.LogErrorSync("RaffleController.Update " & vbCrLf & sObs)
        End Try
        Return retval
    End Function

    Async Function MailConfirmation(raffleparticipant As String) As System.Threading.Tasks.Task
        Dim exs As New List(Of Exception)
        Dim oRaffleParticipant = Await FEB.RaffleParticipant.Find(exs, New Guid(raffleparticipant))
        Dim oMailMessage = Await FEB.RaffleParticipant.RaffleParticipationMailMessage(exs, Website.GlobalVariables.Emp, oRaffleParticipant)
        Dim oUserInfo = Await FEB.User.Find(DTOUser.Wellknown(DTOUser.Wellknowns.info).Guid, exs)
        If Not Await FEB.MailMessage.Send(exs, oUserInfo, oMailMessage) Then
            ContextHelper.LogErrorSync("RaffleController.MailConfirmation")
        End If
    End Function

    <HttpPost>
    Public Shadows Async Function onAuthentication() As Threading.Tasks.Task(Of JsonResult)
        Dim ex2 As New List(Of Exception)
        Dim raffle As New Guid(Request("raffle"))
        Dim email As String = Request("email")
        Dim oRaffle = Await FEB.Raffle.Find(raffle, ex2)
        Dim oUser = Await FEB.User.FromEmail(ex2, Website.GlobalVariables.Emp, email)
        Dim myData As Object = Nothing
        Dim exs As New List(Of DTORaffleParticipant.Exception)
        Dim isValidUser As Boolean = Await FEB.RaffleParticipant.Validate(oRaffle, oUser, exs)
        If isValidUser Then
            'myData = New With {.isDuplicated = "false", .token = oUser.Guid.ToString}
            myData = New With {.success = "true", .token = oUser.Guid.ToString}
        Else
            'myData = New With {.isDuplicated = "true", .fch = Format(oParticipant.Fch, "dd/MM/yy HH:mm")}
            myData = New With {.success = "false", .exception = exs.First.Message}
        End If

        Dim retval As JsonResult = Json(myData, JsonRequestBehavior.AllowGet)
        Return retval
    End Function
    Public Shadows Async Function onAuthentication_OldGet(raffle As Guid, email As String) As Threading.Tasks.Task(Of JsonResult)
        Dim ex2 As New List(Of Exception)
        Dim oRaffle = Await FEB.Raffle.Find(raffle, ex2)
        Dim oUser = Await FEB.User.FromEmail(ex2, Website.GlobalVariables.Emp, email)
        Dim myData As Object = Nothing
        Dim exs As New List(Of DTORaffleParticipant.Exception)
        Dim isValidUser As Boolean = Await FEB.RaffleParticipant.Validate(oRaffle, oUser, exs)
        If isValidUser Then
            'myData = New With {.isDuplicated = "false", .token = oUser.Guid.ToString}
            myData = New With {.success = "true", .token = oUser.Guid.ToString}
        Else
            'myData = New With {.isDuplicated = "true", .fch = Format(oParticipant.Fch, "dd/MM/yy HH:mm")}
            myData = New With {.success = "false", .exception = exs.First.Message}
        End If

        Dim retval As JsonResult = Json(myData, JsonRequestBehavior.AllowGet)
        Return retval
    End Function


End Class

Public Class RafflesController
    Inherits _MatController

    Async Function Index() As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim oLang As DTOLang = ContextHelper.Lang()
        Dim Model = Await FEB.Raffles.Model(exs, oLang, 5, 0, MyBase.User)
        ContextHelper.NavViewModel.ResetCustomMenu()
        ViewBag.Lang = MyBase.UrlLang(HttpContext.Request.Url)
        ViewBag.Title = oLang.Tradueix("Sorteos", "Sortejos", "Raffles", "Sorteios")
        ViewBag.Description = oLang.Tradueix("Consulta los sorteos en marcha y los resultados de los últimos sorteos publicados", "Consulta els sortejos en marxa i els resultats dels últims sortejos publicats", "Browse current raffles and last raffle results", "Confira os sorteios em andamento e os resultados dos últimos sorteios publicados")
        ViewBag.Canonical = DTORaffle.Collection.Url()
        Return View("Raffles", Model)
    End Function

    Async Function Play(guid As Guid) As Threading.Tasks.Task(Of ActionResult)
        'per compatibilitat amb links antics, reenviem a sorteos
        Dim retval = Await Index()
        Return retval
    End Function

    <HttpPost>
    Async Function partialRaffles() As Threading.Tasks.Task(Of PartialViewResult)
        Dim exs As New List(Of Exception)
        Dim take As Integer = Request("take")
        Dim takeFrom As Integer = Request("takeFrom")
        Dim oLang As DTOLang = ContextHelper.Lang()
        Dim Model = Await FEB.Raffles.Model(exs, oLang, take, takeFrom, MyBase.User)
        Return PartialView("Raffles_", Model)
    End Function
End Class