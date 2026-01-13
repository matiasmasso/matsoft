Public Class RaffleParticipant
    Shared Function Find(oGuid As Guid) As DTORaffleParticipant
        Dim retval As DTORaffleParticipant = RaffleParticipantLoader.Find(oGuid)
        Return retval
    End Function

    Shared Function Find(oRaffle As DTORaffle, oUser As DTOUser) As DTORaffleParticipant
        Dim retval As DTORaffleParticipant = RaffleParticipantLoader.Find(oRaffle, oUser)
        Return retval
    End Function

    Shared Function Load(oParticipant As DTORaffleParticipant) As Boolean
        Dim retval As Boolean = RaffleParticipantLoader.Load(oParticipant)
        Return retval
    End Function

    Shared Function Update(oParticipant As DTORaffleParticipant, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = RaffleParticipantLoader.Update(oParticipant, exs)
        Return retval
    End Function

    Shared Function Delete(oParticipant As DTORaffleParticipant, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = RaffleParticipantLoader.Delete(oParticipant, exs)
        Return retval
    End Function

    Shared Function PlayModelFactory(oRaffle As DTORaffle, oUser As DTOUser, oLang As DTOLang) As DTORafflePlayModel
        Dim retval As New DTORafflePlayModel()
        retval.Participant = DTORaffleParticipant.Factory(oRaffle, oUser)
        retval.StoreLocator = BEBL.StoreLocator.Fetch(retval.Participant, oLang)
        Return retval
    End Function

    Shared Function RaffleParticipationMailMessage(oEmp As DTOEmp, oParticipant As DTORaffleParticipant, ByRef exs As List(Of Exception)) As DTOMailMessage
        RaffleLoader.Load(oParticipant.Raffle)
        Dim oSsc = DTOSubscription.Wellknown(DTOSubscription.Wellknowns.CopiaSorteigConfirmacioParticipacio)
        Dim oLang As DTOLang = oParticipant.User.lang

        Dim retval = DTOMailMessage.Factory(oParticipant.User.EmailAddress)
        With retval
            .Bcc = DTOSubscriptor.Recipients(BEBL.Subscriptors.All(oSsc))
            .Subject = "M+O: " & oLang.Tradueix("Confirmación de Participación en Sorteo", "Confirmació de Participació en Sorteig", "Raffle Enrollment Confirmation") & " " & BEBL.Raffle.BrandNom(oParticipant.Raffle)
            .BodyUrl = BEBL.Mailing.BodyUrl(DTODefault.MailingTemplates.RaffleParticipation, oParticipant.Guid.ToString())
        End With
        Return retval

    End Function



End Class

Public Class RaffleParticipants

    Shared Function All(oRaffle As DTORaffle) As List(Of DTORaffleParticipant)
        Return RaffleParticipantsLoader.FromRaffle(oRaffle)
    End Function

    Shared Function Compact(oRaffle As DTORaffle) As List(Of DTORaffleParticipant.Compact)
        Return RaffleParticipantsLoader.Compact(oRaffle)
    End Function

    Shared Function Valids(oRaffle As DTORaffle) As List(Of DTORaffleParticipant)
        Dim retval As List(Of DTORaffleParticipant) = RaffleParticipantsLoader.Valids(oRaffle)
        Return retval
    End Function

    Shared Function Delete(oParticipants As List(Of DTORaffleParticipant), exs As List(Of Exception)) As Boolean
        Return RaffleParticipantsLoader.Delete(oParticipants, exs)
    End Function

End Class
