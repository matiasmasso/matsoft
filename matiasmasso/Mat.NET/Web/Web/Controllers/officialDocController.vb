Public Class OfficialDocController
    Inherits _MatController

    '
    ' GET: /Documentacion

    Function Index() As ActionResult
        Return View()
    End Function

    Function Mercantil(id As String) As ActionResult
        Dim retval As ActionResult = Nothing
        Dim oModelId As OfficialDocModel.Ids
        If [Enum].TryParse(Of OfficialDocModel.Ids)(id, True, oModelId) Then
            Dim oModel As OfficialDocModel = OfficialDocModelLoader.FromId(oModelId)

            Dim oBreadCrumb As New DTOBreadCrumb(GetSession.Lang)
            oBreadCrumb.AddItem(GetSession.Tradueix("documentación oficial", "documentació oficial", "official documentation"), "#")
            oBreadCrumb.AddItem(GetSession.Tradueix("mercantil", "mercantil", "merchant"), "#")
            oBreadCrumb.AddItem(GetSession.Tradueix("auditorias", "auditories", "audited accounts"), "#")
            ViewBag.BreadCrumb = oBreadCrumb

            ViewBag.Guid = oModel.Guid
            retval = LoginOrView("Docs")
        End If
        Return retval
    End Function

    Function pageindexchanged(returnurl As String, guid As Guid, pageindex As Integer) As PartialViewResult
        ViewBag.Guid = guid
        ViewBag.PageIndex = pageindex

        Dim retval As PartialViewResult = PartialView(returnurl)
        Return retval
    End Function

    Function LoginOrView(sViewName As String, Optional oModel As Object = Nothing) As ActionResult
        Dim retval As ActionResult = Nothing
        If GetSession.IsAuthenticated Then
            retval = View(sViewName, oModel)
        Else
            Dim oContext As ControllerContext = Me.ControllerContext
            Dim actionName As String = oContext.RouteData.Values("action")
            Dim u As New UrlHelper(oContext.RequestContext)
            Dim SelfUrl As String = u.Action(actionName)

            retval = RedirectToAction("Login", "Account", New With {.returnUrl = SelfUrl})
        End If
        Return retval
    End Function


End Class