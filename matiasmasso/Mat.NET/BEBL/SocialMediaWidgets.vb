Public Class SocialMediaWidget

    Shared Function Find(oGuid As Guid) As DTOSocialMediaWidget
        Dim retval As DTOSocialMediaWidget = SocialMediaWidgetLoader.Find(oGuid)
        Return retval
    End Function

    Shared Function Load(ByRef oSocialMediaWidget As DTOSocialMediaWidget) As Boolean
        Dim retval As Boolean = SocialMediaWidgetLoader.Load(oSocialMediaWidget)
        Return retval
    End Function

    Shared Function Update(oSocialMediaWidget As DTOSocialMediaWidget, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = SocialMediaWidgetLoader.Update(oSocialMediaWidget, exs)
        Return retval
    End Function

    Shared Function Delete(oSocialMediaWidget As DTOSocialMediaWidget, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = SocialMediaWidgetLoader.Delete(oSocialMediaWidget, exs)
        Return retval
    End Function


    Shared Function Widget(oUser As DTOUser, oPlatform As DTOSocialMediaWidget.Platforms, oProduct As DTOProduct) As DTOSocialMediaWidget
        Dim retval As DTOSocialMediaWidget = SocialMediaWidgetLoader.Widget(oUser, oPlatform, oProduct)
        Return retval
    End Function

End Class

Public Class SocialMediaWidgets

    Shared Function All() As List(Of DTOSocialMediaWidget)
        Dim retval As List(Of DTOSocialMediaWidget) = SocialMediaWidgetsLoader.All()
        Return retval
    End Function


End Class