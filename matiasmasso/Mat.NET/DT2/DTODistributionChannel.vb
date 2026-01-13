Public Class DTODistributionChannel
    Inherits DTOBaseGuid

    Property langText As DTOLangText
    Property ord As Integer
    Property consumerPriority As Integer
    Property contactClasses As List(Of DTOContactClass)

    Public Enum wellknowns
        notSet
        granDistribucio
        farmacia
        botiga
        cadenas
        diversos
    End Enum


    Public Sub New()
        MyBase.New()
        _LangText = New DTOLangText
        _ContactClasses = New List(Of DTOContactClass)
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
        _LangText = New DTOLangText
        _ContactClasses = New List(Of DTOContactClass)
    End Sub


    Shared Function wellknown(value As DTODistributionChannel.wellknowns) As DTODistributionChannel
        Dim retval As DTODistributionChannel = Nothing
        Select Case value
            Case wellknowns.Botiga
                retval = New DTODistributionChannel(New Guid("EF72040D-8F5D-40C7-B4CE-AB069656858D"))
            Case wellknowns.Cadenas
                retval = New DTODistributionChannel(New Guid("E7261551-E49E-4263-B5F3-D14543D29434"))
            Case wellknowns.Farmacia
                retval = New DTODistributionChannel(New Guid("4C1B6866-B97C-4105-BB52-68E657B8682B"))
            Case wellknowns.GranDistribucio
                retval = New DTODistributionChannel(New Guid("3ED938C2-2466-4E9D-8CED-D73074885016"))
            Case wellknowns.Diversos
                retval = New DTODistributionChannel(New Guid("7E7560D4-D3BE-4CD8-A42C-EF2C54B0EC26"))
        End Select
        Return retval
    End Function

    Shared Function Nom(oDistributionChannel As DTODistributionChannel, oLang As DTOLang) As String
        Dim retval As String = oDistributionChannel.LangText.Tradueix(oLang)
        Return retval
    End Function

    Shared Function Caption(oChannels As List(Of DTODistributionChannel), oLang As DTOLang) As String
        Dim retval As String = ""
        Select Case oChannels.Count
            Case 0
            Case 1
                retval = oChannels.First.LangText.Tradueix(oLang)
            Case 2
                retval = String.Format("{0}, {1}", oChannels(0).LangText.Tradueix(oLang), oChannels(1).LangText.Tradueix(oLang))
            Case Else
                retval = "(diversos)"
        End Select
        Return retval
    End Function

End Class
