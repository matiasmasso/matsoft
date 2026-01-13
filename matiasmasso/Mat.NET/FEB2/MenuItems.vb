Public Class MenuItems
    Inherits _FeblBase

    Shared Async Function All(exs As List(Of Exception), oLang As DTOLang, Optional oUser As DTOUser = Nothing) As Task(Of List(Of DTOMenu))
        Dim sUserGuid = If(oUser Is Nothing, Guid.Empty.ToString(), oUser.Guid.ToString)
        Dim retval = Await Api.Fetch(Of List(Of DTOMenu))(exs, "Menu", sUserGuid, oLang.Tag)
        Return retval
    End Function

    Shared Async Function Fetch(exs As List(Of Exception), Optional oUser As DTOUser = Nothing) As Task(Of DTOMenu.Collection)
        Dim retval = Await Api.Fetch(Of DTOMenu.Collection)(exs, "Menu", OpcionalGuid(oUser))
        Return retval
    End Function

    Shared Function FetchSync(exs As List(Of Exception), Optional oUser As DTOUser = Nothing) As DTOMenu.Collection
        Dim retval = Api.FetchSync(Of DTOMenu.Collection)(exs, "Menu", OpcionalGuid(oUser))
        Return retval
    End Function


    Shared Sub attachDeveloperMenu(oMenus As List(Of DTOMenu))

        If Debugger.IsAttached And oMenus IsNot Nothing Then
            Dim oGroup = DTOMenu.Factory(DTOLangText.Factory("Desarrollador", "Desenvolupador", "Developer"))
            Dim oExistingGroup = oMenus.FirstOrDefault(Function(x) x.Caption.Esp = oGroup.Caption.Esp)
            If oExistingGroup IsNot Nothing Then oMenus.Remove(oExistingGroup)

            oMenus.Add(oGroup)
            If FEB2.Api.UseLocalApi Then
                oGroup.AddChild(DTOLangText.Factory("cambia a Api remota", "canvia a Api remota", "use remote Api"), "/uselocalapi/0")
            Else
                oGroup.AddChild(DTOLangText.Factory("cambia a Api local", "canvia a Api local", "use local Api"), "/uselocalapi/1")
            End If
        End If
    End Sub



End Class
