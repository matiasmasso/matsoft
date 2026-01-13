Public Class RaffleController
    Inherits _BaseController

    <HttpPost>
    <Route("api/raffles/currentOrNext")>
    Public Function CurrentOrNext(user As DUI.User) As DUI.Raffle
        Dim retval As DUI.Raffle = Nothing

        Dim oUser As New DTOUser(user.Guid)

        Dim oRaffle As DTORaffle = BLLRaffles.CurrentOrNextRaffle(oUser.Lang)
        If oRaffle IsNot Nothing Then
            Dim oRaffleParticipant As DTORaffleParticipant = BLLRaffleParticipant.Find(oRaffle, oUser)

            retval = New DUI.Raffle()
            With retval
                .Guid = oRaffle.Guid
                .Title = oRaffle.Title
                .FchFrom = DateTimeFormat(oRaffle.FchFrom)
                .FchTo = DateTimeFormat(oRaffle.FchTo)
                .ThumbnailUrl = BLLRaffle.ImgBanner600Url(oRaffle, True)
                If oRaffleParticipant IsNot Nothing Then
                    .FchParticipacio = DateTimeFormat(oRaffleParticipant.Fch)
                End If
                .playUrl = BLLRaffle.PlayUrl(oRaffle, True)
            End With

        End If

        Return retval
    End Function

End Class
