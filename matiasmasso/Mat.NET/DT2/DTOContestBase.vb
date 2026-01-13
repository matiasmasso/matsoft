Public Class DTOContestBase
    Inherits DTOBaseGuid
    Property Lang As DTOLang
    Property Country As DTOCountry
    Property Title As String = ""
    Property Bases As String
    Property FchFrom As Date = Date.MinValue
    Property FchTo As Date = Date.MinValue
    Property UrlExterna As String
    Property CostPrize As DTOAmt
    Property CostPubli As DTOAmt

    <JsonIgnore> Property ImageFbFeatured As Image
    <JsonIgnore> Property ImageBanner600 As Image
    <JsonIgnore> Property ImageCallToAction500 As Image
    Property Visible As Boolean

    Property Codi As Codis

    Public Enum Codis
        NotSet
        Raffle
        Contest
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
    End Sub

    Public Function ImageFbFeaturedUrl(Optional AbsoluteUrl As Boolean = False) As String
        Dim retval = MmoUrl.image(Defaults.ImgTypes.sorteofbfeatured200, MyBase.Guid, AbsoluteUrl)
        Return retval
    End Function

    Shared Function IsNotYetStarted(oContestBase As DTOContestBase) As Boolean
        Dim retval As Boolean = True
        If IsOver(oContestBase) Then
            retval = False
        ElseIf IsActive(oContestBase) Then
            retval = False
        End If
        Return retval
    End Function

    Public Function IsOver() As Boolean
        Dim retval As Boolean = _FchTo < Now
        Return retval
    End Function

    Shared Function IsOver(oContestBase As DTOContestBase) As Boolean
        Dim retval As Boolean
        If oContestBase IsNot Nothing Then
            retval = oContestBase.IsOver
        End If
        Return retval
    End Function

    Shared Function IsActive(oContestBase As DTOContestBase) As Boolean
        Dim retval As Boolean
        If IsOver(oContestBase) Then
            retval = False
        Else
            Select Case oContestBase.FchFrom.Year
                Case Is > Now.Year
                    retval = False
                Case Is < Now.Year
                    retval = True
                Case Is = Now.Year
                    Select Case oContestBase.FchFrom.Month
                        Case Is > Now.Month
                            retval = False
                        Case Is < Now.Month
                            retval = True
                        Case Is = Now.Month
                            Select Case oContestBase.FchFrom.Day
                                Case Is > Now.Day
                                    retval = False
                                Case Is < Now.Day
                                    retval = True
                                Case Is = Now.Day
                                    retval = True
                            End Select
                    End Select
            End Select
        End If

        Return retval
    End Function



End Class


Public Class DTOContestBaseParticipant
    Inherits DTOBaseGuid

    Property Parent As DTOContestBase
    Property Fch As Date
    Property User As DTOUser

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
    End Sub

End Class