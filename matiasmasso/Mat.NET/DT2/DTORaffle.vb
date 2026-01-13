
Public Class DTORaffle
    Inherits DTOContestBase

    Property product As DTOProduct
    Property brand As DTOProductBrand
    Property winner As DTORaffleParticipant
    Property suplentesCount As Integer
    Property question As String = Nothing
    Property answers As List(Of String)
    Property rightAnswer As Integer
    Property participants As List(Of DTORaffleParticipant)

    Property participantsCount As Integer
    Property newParticipantsCount As Integer

    <JsonIgnore> Property imageWinner As Image
    Property fchWinnerReaction As Date
    Property fchDistributorReaction As Date
    Property fchDelivery As Date
    Property fchPicture As Date
    Property delivery As DTODelivery

    Public Const THUMBNAILWIDTH As Integer = 325
    Public Const THUMBNAILHEIGHT As Integer = 205


    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
    End Sub

    Shared Function Factory(oLang As DTOLang) As DTORaffle
        Dim retval As New DTORaffle
        With retval
            .Answers = New List(Of String)
            .FchFrom = Today
            .FchTo = Today
            .RightAnswer = 0
            .Visible = True
            .Lang = oLang
            .Country = DTORaffle.CountryFromLang(.Lang)
            .Title = "(nou sorteig)"
            .Question = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. Aenean massa. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Donec quam felis, ultricies nec, pellentesque eu, pretium quis, sem. Nulla consequat massa quis enim. Donec pede justo, fringilla vel, aliquet nec, vulputate eget, arcu. In enim justo, rhoncus ut, imperdiet a, venenatis vitae, justo."
            .Bases = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. Aenean massa. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Donec quam felis, ultricies nec, pellentesque eu, pretium quis, sem. Nulla consequat massa quis enim. Donec pede justo, fringilla vel, aliquet nec, vulputate eget, arcu. In enim justo, rhoncus ut, imperdiet a, venenatis vitae, justo."
        End With
        Return retval
    End Function


    Shared Shadows Function CountryFromLang(oLang As DTOLang) As DTOCountry
        Dim retval As DTOCountry = Nothing
        Select Case oLang.Id
            Case DTOLang.Ids.POR
                retval = DTOCountry.wellknown(DTOCountry.wellknowns.Portugal)
            Case Else
                retval = DTOCountry.wellknown(DTOCountry.wellknowns.Spain)
        End Select
        Return retval
    End Function

    Shared Function TimeToStart(oContestBase As DTOContestBase, oLang As DTOLang) As String
        Dim sb As New System.Text.StringBuilder
        sb.Append(oLang.tradueix("Próximo sorteo ", "Proper sorteig ", "Next raffle ", "Próximo sorteio "))
        Dim oSpan As TimeSpan = oContestBase.FchFrom - Now
        Dim iMinutes As Integer = Fix(oSpan.TotalMinutes)
        Dim iHours As Integer = Fix(iMinutes / 60)
        iMinutes = iMinutes - (iHours * 60)
        Dim iDays As Integer = Fix(iHours / 24)
        iHours = iHours - (iDays * 24)
        If iDays > 0 Then
            If iDays = 1 Then
                sb.Append(oLang.tradueix("mañana a medianoche.", "demà a mitja nit.", "tomorrow midnight.", "amanhã, meia-noite."))
            Else
                sb.Append(oLang.tradueix("en " & iDays & " dias.", "en " & iDays & " dies.", "on " & iDays & " days.", "em " & iDays & " dias."))
            End If
        Else
            If iHours > 0 Then
                sb.Append(oLang.tradueix("en menos de " & iHours + 1 & " horas.", "en menys de " & iHours + 1 & " hores.", "in less than " & iHours + 1 & " hours.", "em menos de " & iHours + 1 & " horas."))
            Else
                sb.Append(oLang.tradueix("en " & iMinutes & " minutos.", "en " & iMinutes & " minuts.", "in " & iMinutes & " minutes.", "em " & iMinutes & " minutos."))
            End If
        End If
        Dim retval As String = sb.ToString
        Return retval
    End Function

    Shared Function RaffleTime(oContestBase As DTOContestBase, oLang As DTOLang) As String
        'ej: el ganador se publicará mañana viernes a media noche
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine(oLang.tradueix("El ganador se publicará ", "El guanyador es publicará ", "Winner to be published "))
        If DateDiff(DateInterval.Hour, Now, oContestBase.FchTo) < 48 Then
            sb.AppendLine(oLang.tradueix("mañana ", "demà ", "tomorrow "))
            sb.AppendLine(oLang.WeekDay(oContestBase.FchTo).ToLower)
            sb.AppendLine(oLang.tradueix(" a media noche.", " a mitja nit.", " midnight."))
        Else
            sb.AppendLine(oLang.tradueix(" a media noche del ", " a mitja nit de ", " on "))
            sb.AppendLine(DTOLang.ESP.WeekDay(oContestBase.FchTo).ToLower)
            sb.AppendLine(oLang.tradueix(".", ".", " midnight."))
        End If
        Dim retval As String = sb.ToString
        Return retval
    End Function

    Shared Function GrfEnrollment(oRaffle As DTORaffle) As DTOGrf
        Dim retval As New DTOGrf
        With retval
            .FchFrom = New Date(oRaffle.FchFrom.Year, oRaffle.FchFrom.Month, oRaffle.FchFrom.Day, 0, 0, 0)
            .FchTo = New Date(oRaffle.FchTo.Year, oRaffle.FchTo.Month, oRaffle.FchTo.Day, 0, 0, 0).AddDays(1).AddMilliseconds(-1)
            .DateInterval = DateInterval.Hour
            .Items = New List(Of DTOGrfItem)
        End With

        If oRaffle.Participants IsNot Nothing Then
            For Each oParticipant In oRaffle.Participants
                Dim oItem As New DTOGrfItem
                oItem.Fch = oParticipant.Fch
                retval.Items.Add(oItem)
            Next
        End If
        Return retval
    End Function

    Shared Function GetRandomWinner(oValidParticipants As List(Of DTORaffleParticipant)) As DTORaffleParticipant
        Dim retval As DTORaffleParticipant = Nothing
        If oValidParticipants.Count > 0 Then
            Dim iWinner As Integer = NumericHelper.RandomNumber(oValidParticipants.Count - 1)
            retval = oValidParticipants(iWinner)
        End If
        Return retval
    End Function

    Shared Function WinnerFullNom(oRaffle As DTORaffle) As String
        Dim sb As New Text.StringBuilder
        If oRaffle.Winner IsNot Nothing Then
            Dim oUser = oRaffle.Winner.User
            If oUser IsNot Nothing Then
                sb.AppendFormat("{0} {1}", oUser.Nom, oUser.Cognoms)
            End If
        End If
        Return sb.ToString
    End Function

    Shared Function WinnerLocationFullNom(oRaffle As DTORaffle) As String
        Dim sb As New Text.StringBuilder
        If oRaffle.Winner IsNot Nothing Then
            Dim oUser = oRaffle.Winner.User
            If oUser IsNot Nothing Then
                If oUser.LocationNom > "" Then
                    sb.Append(oUser.LocationNom)
                    If oUser.ProvinciaNom > "" And oUser.ProvinciaNom <> oUser.LocationNom Then
                        sb.Append(" (" & oUser.LocationNom & ")")
                    End If
                End If
            End If
        End If
        Return sb.ToString
    End Function

    Public Function BannerUrl() As String
        Return MmoUrl.apiUrl("Raffle/ImageBanner600", MyBase.Guid.ToString())
    End Function

    Shared Function rafflesUrl() As String
        Return MmoUrl.factory(False, "sorteos")
    End Function

End Class

Public Class DTORaffleParticipant
    Inherits DTOContestBaseParticipant

    Property Raffle As DTORaffle
    Property Distribuidor As DTOContact
    Property Answer As Integer = -1
    Property Category As Integer

    Property Suplente As Integer
    Property Num As Integer

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
    End Sub

    Shared Function Factory(oRaffle As DTORaffle, oUser As DTOUser, Optional Answer As Integer = 0, Optional oDistributor As DTOContact = Nothing)
        Dim retval As New DTORaffleParticipant()
        With retval
            .Raffle = oRaffle
            .User = oUser
            .Fch = Now
            .Answer = Answer
            .Distribuidor = oDistributor
        End With
        Return retval
    End Function

    Shared Function FullNom(oParticipant As DTORaffleParticipant) As String
        Dim sb As New System.Text.StringBuilder
        Dim oUser As DTOUser = oParticipant.User
        sb.Append(oUser.Nom)
        If oUser.Cognoms > "" Then
            If sb.Length > 0 Then sb.Append(" ")
            sb.Append(oUser.Cognoms)
        End If

        If sb.Length = 0 Then
            If oUser.NickName > "" Then
                sb.Append(oUser.NickName)
            Else
                sb.Append(oUser.EmailAddress)
            End If
        End If

        Dim retval As String = sb.ToString
        Return retval
    End Function

    Shared Function FormattedNumAndNom(oParticipant As DTORaffleParticipant) As String
        Dim sb As New System.Text.StringBuilder
        sb.Append(FormattedNum(oParticipant))
        sb.Append(" ")
        sb.Append(Nom(oParticipant))
        Dim retval As String = sb.ToString
        Return retval
    End Function

    Shared Function FormattedNum(oParticipant As DTORaffleParticipant) As String
        Dim retval As String = ""
        If oParticipant IsNot Nothing Then
            retval = Format(oParticipant.Num, "0000")
        End If
        Return retval
    End Function

    Shared Function Nom(oParticipant As DTORaffleParticipant) As String
        Dim retval As String = ""
        If oParticipant IsNot Nothing Then
            Dim oUser As DTOUser = oParticipant.User
            Dim sb As New System.Text.StringBuilder
            sb.Append(oUser.Nom)
            If sb.Length > 0 Then sb.Append(" ")
            sb.Append(oUser.Cognoms)
            retval = sb.ToString
        End If
        Return retval
    End Function

    Public Function HasRightAnswer() As Boolean
        Dim retval As Boolean = (_Answer = _Raffle.RightAnswer - 1)
        Return retval
    End Function

    Shared Function ExcludedZonas() As List(Of DTOZona)
        Dim retval As New List(Of DTOZona)
        retval.Add(DTOZona.wellknown(DTOZona.wellknowns.CanariasTenerife))
        retval.Add(DTOZona.wellknown(DTOZona.wellknowns.CanariasGranCanaria))
        retval.Add(DTOZona.wellknown(DTOZona.wellknowns.CanariasLaPalma))
        retval.Add(DTOZona.wellknown(DTOZona.wellknowns.CanariasLaGomera))
        retval.Add(DTOZona.wellknown(DTOZona.wellknowns.CanariasHierro))
        retval.Add(DTOZona.wellknown(DTOZona.wellknowns.CanariasLanzarote))
        retval.Add(DTOZona.wellknown(DTOZona.wellknowns.CanariasFuerteventura))
        retval.Add(DTOZona.wellknown(DTOZona.wellknowns.Ceuta))
        retval.Add(DTOZona.wellknown(DTOZona.wellknowns.Melilla))
        retval.Add(DTOZona.wellknown(DTOZona.wellknowns.Azores))
        retval.Add(DTOZona.wellknown(DTOZona.wellknowns.Madeira))
        retval.Add(DTOZona.wellknown(DTOZona.wellknowns.Andorra))
        Return retval
    End Function

    Public Class Exception
        Inherits System.Exception
        Property Reason As Reasons
        Public Enum Reasons
            NotSet
            Duplicated
            WrongCountry
        End Enum

        Public Sub New(reason As Reasons, Optional message As String = "")
            MyBase.New(message)
            _Reason = reason
        End Sub


    End Class
End Class



