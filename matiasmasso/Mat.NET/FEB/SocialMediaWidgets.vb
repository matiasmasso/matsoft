Public Class SocialMediaWidget
    Inherits _FeblBase

    Shared Async Function Find(exs As List(Of Exception), oGuid As Guid) As Task(Of DTOSocialMediaWidget)
        Return Await Api.Fetch(Of DTOSocialMediaWidget)(exs, "SocialMediaWidget", oGuid.ToString())
    End Function

    Shared Function WidgetSync(exs As List(Of Exception), oUser As DTOUser, oPlatform As DTOSocialMediaWidget.Platforms, oProduct As DTOProduct) As DTOSocialMediaWidget
        Return Api.FetchSync(Of DTOSocialMediaWidget)(exs, "SocialMediaWidget", OpcionalGuid(oUser), oPlatform, OpcionalGuid(oProduct))
    End Function

    Shared Function Load(exs As List(Of Exception), ByRef oSocialMediaWidget As DTOSocialMediaWidget) As Boolean
        If Not oSocialMediaWidget.IsLoaded And Not oSocialMediaWidget.IsNew Then
            Dim pSocialMediaWidget = Api.FetchSync(Of DTOSocialMediaWidget)(exs, "SocialMediaWidget", oSocialMediaWidget.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOSocialMediaWidget)(pSocialMediaWidget, oSocialMediaWidget, exs)
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Update(exs As List(Of Exception), oSocialMediaWidget As DTOSocialMediaWidget) As Task(Of Boolean)
        Return Await Api.Update(Of DTOSocialMediaWidget)(oSocialMediaWidget, exs, "SocialMediaWidget")
        oSocialMediaWidget.IsNew = False
    End Function


    Shared Async Function Delete(exs As List(Of Exception), oSocialMediaWidget As DTOSocialMediaWidget) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOSocialMediaWidget)(oSocialMediaWidget, exs, "SocialMediaWidget")
    End Function
End Class

Public Class SocialMediaWidgets
    Inherits _FeblBase

    Shared Async Function All(exs As List(Of Exception)) As Task(Of List(Of DTOSocialMediaWidget))
        Return Await Api.Fetch(Of List(Of DTOSocialMediaWidget))(exs, "SocialMediaWidgets")
    End Function

End Class
