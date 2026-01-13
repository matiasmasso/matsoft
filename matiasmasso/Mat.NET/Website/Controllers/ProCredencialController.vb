Imports MatHelperStd

Public Class ProCredencialController
    Inherits _MatController

    Public Async Function Index(id As Guid) As Threading.Tasks.Task(Of ActionResult)
        Dim retval As ActionResult = Nothing
        Dim exs As New List(Of Exception)
        Dim oUser = MyBase.User()
        If oUser Is Nothing Then
            retval = MyBase.UnauthorizedView()
        Else
            Select Case oUser.Rol.id
                Case DTORol.Ids.superUser, DTORol.Ids.admin, DTORol.Ids.marketing, DTORol.Ids.logisticManager, DTORol.Ids.taller
                    Dim oCredencial = Await FEB.Credencial.Find(id, exs)
                    If exs.Count = 0 Then 'filtrar per usuari autoritzat
                        If oCredencial Is Nothing Then
                            retval = Await ErrorResult("Credencial with Guid '" & id.ToString & "' Not found")
                        Else
                            Dim model = oCredencial.Model()
                            ViewBag.Title = ContextHelper.Lang.Tradueix("Credencial", "Credencial", "Credential")
                            ViewBag.SideMenuItems = MyBase.SideMenuItems()
                            retval = View("Credencial", model)
                        End If
                    Else
                        retval = Await ErrorResult(exs)
                    End If
                Case Else
                    retval = MyBase.UnauthorizedView()
            End Select
        End If
        Return retval
    End Function

    Public Function Factory() As ActionResult
        Dim retval As ActionResult = Nothing
        Dim exs As New List(Of Exception)
        Dim oUser = MyBase.User()
        If oUser Is Nothing Then
            retval = MyBase.UnauthorizedView()
        Else
            Select Case oUser.Rol.id
                Case DTORol.Ids.superUser, DTORol.Ids.admin, DTORol.Ids.marketing, DTORol.Ids.logisticManager, DTORol.Ids.taller
                    Dim model = DTOCredencial.Factory(oUser).Model
                    ViewBag.Title = ContextHelper.Lang.Tradueix("Credencial", "Credencial", "Credential")
                    ViewBag.SideMenuItems = MyBase.SideMenuItems()
                    retval = View("Credencial", model)
                Case Else
                    retval = MyBase.UnauthorizedView()
            End Select
        End If
        Return retval
    End Function

    <HttpPost>
    Public Async Function Update(model As DTOCredencial.ViewModel) As Threading.Tasks.Task(Of JsonResult)
        Dim exs As New List(Of Exception)
        Dim retval As JsonResult = Nothing
        Dim oCredencial As DTOCredencial = Nothing
        If model.IsNew Then
            oCredencial = DTOCredencial.Factory(MyBase.User)
        Else
            oCredencial = Await FEB.Credencial.Find(model.Guid, exs)
        End If

        If exs.Count = 0 Then
            If oCredencial Is Nothing Then
                Dim oObj As New With {.success = False, .message = "Credencial not found"}
                retval = Json(oObj, JsonRequestBehavior.AllowGet)
            Else
                With oCredencial
                    .Referencia = model.Referencia
                    .Url = model.Url
                    .Usuari = model.Usuari
                    .Password = model.Password
                    .Obs = model.Obs
                End With

                If Await FEB.Credencial.Update(oCredencial, exs) Then
                    Dim oObj As New With {.success = True}
                    retval = Json(oObj, JsonRequestBehavior.AllowGet)
                Else
                    Dim oObj As New With {.success = False, .message = ExceptionsHelper.ToFlatString(exs)}
                    retval = Json(oObj, JsonRequestBehavior.AllowGet)
                End If
            End If
        Else
            Dim oObj As New With {.success = False, .message = ExceptionsHelper.ToFlatString(exs)}
            retval = Json(oObj, JsonRequestBehavior.AllowGet)
        End If

        Return retval
    End Function

    <HttpPost>
    Public Async Function Delete(model As DTOCredencial.ViewModel) As Threading.Tasks.Task(Of JsonResult)
        Dim exs As New List(Of Exception)
        Dim retval As JsonResult = Nothing
        Dim oCredencial As New DTOCredencial(model.Guid)
        If Await FEB.Credencial.Delete(oCredencial, exs) Then
            Dim oObj As New With {.success = True}
            retval = Json(oObj, JsonRequestBehavior.AllowGet)
        Else
            Dim oObj As New With {.success = False, .message = ExceptionsHelper.ToFlatString(exs)}
            retval = Json(oObj, JsonRequestBehavior.AllowGet)
        End If
        Return retval
    End Function

End Class
