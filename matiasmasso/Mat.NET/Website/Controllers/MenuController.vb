Public Class MenuController
    Inherits _MatController

    Async Function MainMenu() As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim oUser = ContextHelper.GetUser()
        Dim oWebMenuGroups = Await FEB.WebMenuGroups.ForWebSite(Website.GlobalVariables.Emp, oUser, ContextHelper.GetLang)
        Return View("Menus", oWebMenuGroups)
    End Function

End Class