Public Class RaffleTrifixController
    Inherits _MatController

    Function Play() As ActionResult
        Dim retval As ActionResult = Nothing
        Dim oGuid As New Guid("53DB8BAC-F557-4676-8E3B-74ADEC8DEA60")
        Dim oRaffle As DTORaffle = BLLRaffle.Find(oGuid)

        If DTORaffle.IsOver(oRaffle) Then
            Dim sUrl As String = FEBL.Raffle.ZoomUrl(oRaffle)
            retval = MyBase.Redirect(sUrl)
        Else
            Dim oParticipant As New DTORaffleParticipant
            With oParticipant
                .Raffle = oRaffle
                .User = GetSession.User
            End With
            retval = View("RaffleTrifix", oParticipant)
        End If

        Return retval
    End Function


    Function Bases(guid As String) As ActionResult
        Dim oGuid As New Guid(guid)
        Dim oRaffle As DTORaffle = BLLRaffle.Find(oGuid)

        ViewBag.Product = BLLRaffle.Brand(oRaffle)

        ViewData("Title") = WebPageHelper.Title(GetSession.Tradueix("Bases del Sorteo", "Bases del Sorteig", "Raffle Terms and Conditiona"))
        ViewBag.MetaDescription = oRaffle.Title
        Return View("Bases", oRaffle)
    End Function

    Function Update(token As String, Raffle As String, code As String) As JsonResult
        Dim retval As JsonResult = Nothing
        Dim oRaffle As DTORaffle = Nothing
        Dim oUser As DTOUser = Nothing
        Dim myData As Object
        Try
            oRaffle = BLLRaffle.Find(New Guid(Raffle))
            oUser = BLLUser.Find(New Guid(token))
            myData = New With {.result = 0}

            If code = "TRIFIX" Then
                If oRaffle IsNot Nothing Then

                    Dim exs As New List(Of Exception)
                    Dim oParticipant As New DTORaffleParticipant
                    If BLLRaffle.EnrollParticipant(oRaffle, oUser, Answer:=0, oDistributor:=Nothing, oParticipant:=oParticipant, exs:=exs) Then
                        myData = New With {.success = 1, .participant = oParticipant.Guid.ToString}
                    Else
                        myData = New With {.success = 2}
                    End If
                End If
            Else
                myData = New With {.success = 3}
            End If

            retval = Json(myData, JsonRequestBehavior.AllowGet)

        Catch ex As Exception
            Dim sObs As String = ex.Message
            If oUser Is Nothing Then
                sObs = sObs & " usuari is nothing"
            Else
                sObs = sObs & " usuari = " & oUser.EmailAddress & " | usuari.guid = " & oUser.Guid.ToString
            End If
            BLLWebBug.LogError(System.Web.HttpContext.Current, GetSession, "RaffleController.Update " & vbCrLf & sObs)
        End Try
        Return retval
    End Function

    Sub MailConfirmation(raffleparticipant As String)
        Dim oParticipant As DTORaffleParticipant = BLLRaffleParticipant.Find(New Guid(raffleparticipant))
        Dim exs As New List(Of Exception)
        If Not BLLRaffleParticipant.MailConfirmation(GlobalVariables.Emp, oParticipant, exs) Then
            BLLWebBug.LogError(System.Web.HttpContext.Current, GetSession)
        End If
    End Sub

    Shadows Function onAuthentication(raffle As String, email As String) As JsonResult
        Dim oRaffle As New DTORaffle(New Guid(raffle))
        Dim oUser As DTOUser = BLLUser.FromEmail(GlobalVariables.Emp, email)
        MyBase.SignInUser(oUser, True)
        Dim oParticipant As DTORaffleParticipant = BLLRaffleParticipant.Find(oRaffle, oUser)
        Dim myData As Object = Nothing
        If oParticipant Is Nothing Then
            myData = New With {.isDuplicated = "false", .token = oUser.Guid.ToString}
        Else
            myData = New With {.isDuplicated = "true", .fch = Format(oParticipant.Fch, "dd/MM/yy HH:mm")}
        End If
        Dim retval As JsonResult = Json(myData, JsonRequestBehavior.AllowGet)
        Return retval
    End Function


End Class
