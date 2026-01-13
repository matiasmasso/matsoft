Public Class RaffleParticipant
    Inherits _FeblBase

    Shared Async Function PlayModelFactory(exs As List(Of Exception), oRaffle As DTORaffle, oUser As DTOUser, oLang As DTOLang) As Task(Of DTORafflePlayModel)
        Dim retval = Await Api.Fetch(Of DTORafflePlayModel)(exs, "RaffleParticipant/PlayModelFactory", oRaffle.Guid.ToString(), OpcionalGuid(oUser), oLang.Tag)
        Return retval
    End Function

    Shared Async Function Find(exs As List(Of Exception), oGuid As Guid) As Task(Of DTORaffleParticipant)
        Return Await Api.Fetch(Of DTORaffleParticipant)(exs, "RaffleParticipant", oGuid.ToString())
    End Function

    Shared Async Function search(exs As List(Of Exception), oRaffle As DTORaffle, oUser As DTOUser) As Task(Of DTORaffleParticipant)
        Return Await Api.Fetch(Of DTORaffleParticipant)(exs, "RaffleParticipant/search", oRaffle.Guid.ToString, oUser.Guid.ToString())
    End Function

    Shared Function Load(exs As List(Of Exception), ByRef oRaffleParticipant As DTORaffleParticipant) As Boolean
        If Not oRaffleParticipant.IsLoaded And Not oRaffleParticipant.IsNew Then
            Dim pRaffleParticipant = Api.FetchSync(Of DTORaffleParticipant)(exs, "RaffleParticipant", oRaffleParticipant.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTORaffleParticipant)(pRaffleParticipant, oRaffleParticipant, exs)
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Update(exs As List(Of Exception), oRaffleParticipant As DTORaffleParticipant) As Task(Of Boolean)
        Return Await Api.Update(Of DTORaffleParticipant)(oRaffleParticipant, exs, "RaffleParticipant")
        oRaffleParticipant.IsNew = False
    End Function


    Shared Async Function Delete(exs As List(Of Exception), oRaffleParticipant As DTORaffleParticipant) As Task(Of Boolean)
        Return Await Api.Delete(Of DTORaffleParticipant)(oRaffleParticipant, exs, "RaffleParticipant")
    End Function

    Shared Async Function Enroll(oRaffleParticipant As DTORaffleParticipant, oUser As DTOUser, oEmp As DTOEmp, exs As List(Of Exception)) As Task(Of Boolean)
        Dim retval As Boolean = Await RaffleParticipant.Update(exs, oRaffleParticipant)
        If retval Then
            Dim oMailMessage = Await RaffleParticipant.RaffleParticipationMailMessage(exs, oEmp, oRaffleParticipant)
            Await MailMessage.Send(exs, oUser, oMailMessage)
        End If
        Return retval
    End Function

    Shared Async Function RaffleParticipationMailMessage(exs As List(Of Exception), oEmp As DTOEmp, oParticipant As DTORaffleParticipant) As Task(Of DTOMailMessage)
        Dim retval As DTOMailMessage = Nothing
        If Raffle.Load(oParticipant.Raffle, exs) Then
            Dim oLang As DTOLang = oParticipant.User.Lang
            Dim oBrand = Product.Brand(exs, oParticipant.Raffle.Product)
            Dim sBrandNom = DTOProduct.GetNom(oBrand)
            Dim oSsc = DTOSubscription.Wellknown(DTOSubscription.Wellknowns.CopiaSorteigConfirmacioParticipacio)
            retval = DTOMailMessage.Factory(oParticipant.User.EmailAddress)
            With retval
                .Bcc = DTOSubscriptor.Recipients(Await Subscription.Subscriptors(oSsc, exs))
                .subject = "M+O: " & oLang.Tradueix("Confirmación de Participación en Sorteo", "Confirmació de Participació en Sorteig", "Raffle Enrollment Confirmation", "Confirmação de participação no sorteio") & " " & sBrandNom
                .BodyUrl = Mailing.BodyUrl(DTODefault.MailingTemplates.RaffleParticipation, oParticipant.Guid.ToString())
            End With
        End If
        Return retval
    End Function

    Shared Async Function RaffleWinnerCongrats(exs As List(Of Exception), oEmp As DTOEmp, oWinner As DTORaffleParticipant) As Task(Of DTOMailMessage)
        Dim retval As DTOMailMessage = Nothing
        If Raffle.Load(oWinner.Raffle, exs) Then
            Dim oLang As DTOLang = oWinner.User.Lang
            Dim oBrand = Product.Brand(exs, oWinner.Raffle.Product)
            Dim sBrandNom = DTOProduct.GetNom(oBrand)
            Dim oSsc = DTOSubscription.Wellknown(DTOSubscription.Wellknowns.CopiaSorteigConfirmacioParticipacio)
            retval = DTOMailMessage.Factory(oWinner.User.EmailAddress)
            With retval
                .Bcc = DTOSubscriptor.Recipients(Await Subscription.Subscriptors(oSsc, exs))
                .Subject = String.Format(oLang.Tradueix("M+O: ¡Enhorabuena, has ganado el sorteo de {0}!", "M+O: ¡Enhorabona, has guanyat el sorteig de {0}!", "M+O: ¡Congratulations, you won the raffle {0}!", "M+O: Felicidades, ganhou o sorteio de {0}!"), sBrandNom)
                .BodyUrl = Mailing.BodyUrl(DTODefault.MailingTemplates.RaffleWinnerCongrats, oWinner.Guid.ToString())
            End With
        End If
        Return retval
    End Function

    Shared Async Function Validate(oRaffle As DTORaffle, oUser As DTOUser, exs As List(Of DTORaffleParticipant.Exception)) As Task(Of Boolean)
        Dim exs2 As New List(Of Exception)
        Dim retval As Boolean = True

        'verifica que no jugui dos cops
        Dim oParticipant = Await RaffleParticipant.search(exs2, oRaffle, oUser)
        If oParticipant IsNot Nothing Then
            retval = False
            Dim msg As String = String.Format("{0} {1:dd/MM/yy HH:mm}",
                                               oUser.Lang.Tradueix("este usuario ya está registrado en este sorteo desde ",
                                                                   "aquest usuari ja está registrat en aquest sorteig des de ",
                                                                   "this user is already enrolled since ",
                                                                   "este usuário já está registado neste sorteio desde "),
                                               oParticipant.Fch)
            exs.Add(New DTORaffleParticipant.Exception(DTORaffleParticipant.Exception.Reasons.Duplicated, msg))
        End If

        'verifica que no es coli un participant de un altre pais
        Select Case oRaffle.Lang.Id
            Case DTOLang.Ids.ESP
                Select Case oUser.Country.ISO
                    Case "AD", "ES"
                    Case Else
                        retval = False
                        Dim msg As String = oUser.Lang.Tradueix("este usuario está registrado en un país distinto del contemplado para este sorteo",
                                                                "aquest usuari está registrat en un pais diferent del contemplat per aquest sorteig",
                                                                "this user is registered on a country out of the scope of this raffle ",
                                                                "este usuário está registado em un pais distinto del contemplado para este sorteo")
                        exs.Add(New DTORaffleParticipant.Exception(DTORaffleParticipant.Exception.Reasons.WrongCountry, msg))
                End Select
            Case DTOLang.Ids.POR
                Select Case oUser.Country.ISO
                    Case "PT"
                    Case Else
                        retval = False
                        Dim msg As String = oUser.Lang.Tradueix("este usuario está registrado en un país distinto del contemplado para este sorteo",
                                                                "aquest usuari está registrat en un pais diferent del contemplat per aquest sorteig",
                                                                "this user is registered on a country out of the scope of this raffle ",
                                                                "este usuário está registado em un pais distinto del contemplado para este sorteo")
                        exs.Add(New DTORaffleParticipant.Exception(DTORaffleParticipant.Exception.Reasons.WrongCountry, msg))
                End Select
        End Select

        Return retval
    End Function

    Shared Function DistributorNameAndAddressHtml(oParticipant As DTORaffleParticipant) As String
        Dim exs As New List(Of Exception)
        Dim retval As String = ""
        Dim oDistributor As DTOContact = oParticipant.Distribuidor
        If Contact.Load(oDistributor, exs) Then
            Dim sb As New System.Text.StringBuilder
            sb.AppendLine(oDistributor.NomComercialOrDefault())
            sb.AppendLine(DTOAddress.MultiLine(oDistributor.Address))
            retval = sb.ToString.Replace(vbCrLf, "<br/>")
        End If
        Return retval
    End Function

    Shared Async Function SetAsWinner(oWinner As DTORaffleParticipant, exs As List(Of Exception)) As Task(Of Boolean)
        Dim retval As Boolean
        If oWinner IsNot Nothing Then
            Dim oRaffle As DTORaffle = oWinner.Raffle
            If Raffle.Load(oRaffle, exs) Then
                oRaffle.Winner = oWinner
                If Await Raffle.Update(exs, oRaffle) Then
                    retval = True
                End If

            End If
        End If
        Return retval
    End Function
End Class

Public Class RaffleParticipants

    Shared Async Function All(oRaffle As DTORaffle, exs As List(Of Exception)) As Task(Of List(Of DTORaffleParticipant))
        Return Await Api.Fetch(Of List(Of DTORaffleParticipant))(exs, "RaffleParticipants", oRaffle.Guid.ToString())
    End Function

    Shared Async Function Compact(oRaffle As DTORaffle, exs As List(Of Exception)) As Task(Of List(Of DTORaffleParticipant))
        Return Await Api.Fetch(Of List(Of DTORaffleParticipant))(exs, "RaffleParticipants/Compact", oRaffle.Guid.ToString())
    End Function

    Shared Function AllSync(oRaffle As DTORaffle, exs As List(Of Exception)) As List(Of DTORaffleParticipant)
        Return Api.FetchSync(Of List(Of DTORaffleParticipant))(exs, "RaffleParticipants", oRaffle.Guid.ToString())
    End Function

    Shared Async Function Valids(oRaffle As DTORaffle, exs As List(Of Exception)) As Task(Of List(Of DTORaffleParticipant))
        Return Await Api.Fetch(Of List(Of DTORaffleParticipant))(exs, "RaffleParticipants/valids", oRaffle.Guid.ToString())
    End Function

    Shared Async Function Delete(exs As List(Of Exception), oRaffleParticipants As List(Of DTORaffleParticipant)) As Task(Of Boolean)
        Return Await Api.Delete(Of List(Of DTORaffleParticipant))(oRaffleParticipants, exs, "RaffleParticipants")
    End Function


    Shared Async Function ExcelParticipantsList(exs As List(Of Exception), oRaffle As DTORaffle) As Task(Of MatHelper.Excel.Sheet)
        Dim items = Await RaffleParticipants.Compact(oRaffle, exs)
        Dim retval As New MatHelper.Excel.Sheet
        For Each item As DTORaffleParticipant In items
            retval.AddRowWithCells(item.User.EmailAddress)
        Next
        Return retval
    End Function
End Class
