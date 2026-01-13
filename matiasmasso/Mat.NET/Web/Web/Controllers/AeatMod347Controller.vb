Public Class AeatMod347Controller
    Inherits _MatController

    '
    ' GET: /AeatMod347

    Async Function Index() As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim retval As ActionResult = Nothing
        Dim oUser = ContextHelper.GetUser()
        If oUser IsNot Nothing AndAlso oUser.Rol.IsAuthenticated Then
            Dim items As List(Of DTOAeatMod347Item) = Nothing
            Dim oRaonsSocials = Await FEB2.Contacts.RaonsSocials(exs, oUser)
            If exs.Count = 0 AndAlso oRaonsSocials.Count > 0 Then
                Dim oExercici As DTOExercici = DTOExercici.Past(GlobalVariables.Emp)
                Dim oMod347 = Await FEB2.AeatMod347.Factory(exs, oExercici)
                If exs.Count = 0 Then
                    retval = View("NoMod347")

                    For Each oRaoSocial As DTOContact In oRaonsSocials
                        items = oMod347.Items.FindAll(Function(x) x.Contact.Guid.Equals(oRaoSocial.Guid))
                        If items.Count > 0 Then
                            retval = View("Mod347", items)
                            Exit For
                        End If
                    Next
                Else
                    retval = View("Error")
                End If
            End If
        Else
            Dim oContext As ControllerContext = Me.ControllerContext
            Dim actionName As String = oContext.RouteData.Values("action")
            Dim u As New System.Web.Mvc.UrlHelper(oContext.RequestContext)
            Dim SelfUrl As String = u.Action(actionName)

            retval = RedirectToAction("Login", "Account", New With {.returnurl = SelfUrl})
        End If
        Return retval
    End Function

    Async Function FromCustomer(guid As Guid) As Threading.Tasks.Task(Of PartialViewResult)
        Dim oExercici As DTOExercici = DTOExercici.Past(GlobalVariables.Emp)
        Dim exs As New List(Of Exception)
        Dim oContact = Await FEB2.Contact.Find(guid, exs)
        Dim oMod347 = Await FEB2.AeatMod347.Factory(exs, oExercici)
        Dim items As List(Of DTOAeatMod347Item) = oMod347.Items.FindAll(Function(x) x.Contact.Guid.Equals(guid))

        Dim retval As PartialViewResult = PartialView("_Mod347", items)
        Return retval

    End Function



End Class