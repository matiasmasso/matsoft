'Imports System.Web.Http

Public Class IncidenciaController
    Inherits _MatController

#Region "New Incidencia"

    <HttpGet>
    Function NewIncidencia() As ActionResult
        Return LoginOrView()
    End Function

    Public Async Function Catalog(customer As Guid, procedencia As DTOIncidencia.Procedencias) As Threading.Tasks.Task(Of JsonResult)
        Dim retval As JsonResult = Nothing
        Dim exs As New List(Of Exception)
        Try
            Dim oCustomer As New DTOCustomer(customer)
            Dim value As DTOCatalog = Await FEB2.Incidencia.Catalog(exs, procedencia, oCustomer)
            If exs.Count = 0 Then
                retval = Json(New With {Key .success = True, Key .data = value}, JsonRequestBehavior.AllowGet)
            Else
                retval = Json(New With {Key .success = False, Key .message = ExceptionsHelper.ToFlatString(exs)}, JsonRequestBehavior.AllowGet)
            End If

        Catch ex As Exception
            retval = Json(New With {Key .success = False, Key .message = ex.Message}, JsonRequestBehavior.AllowGet)
        End Try
        Return retval
    End Function

    Public Async Function Save() As Threading.Tasks.Task(Of JsonResult)
        Dim exs As New List(Of Exception)
        Dim myData As Object = Nothing

        Dim oUser = ContextHelper.GetUser()

        If GuidHelper.IsGuid(Request.Form("product")) Then
            Dim oProductGuid As New Guid(Request.Form("product"))
            Dim oProduct = Await FEB2.Product.Find(exs, oProductGuid)
            Select Case oProduct.sourceCod
                Case DTOProduct.SourceCods.Brand
                    myData = New With {.result = 2, .status = "ERRNOPRODUCTCATEGORY", .message = ContextHelper.Tradueix("por favor seleccione la categoría de producto", "si us plau seleccioni la categoría de producte", "please select product category")}
                Case Else
                    Dim oCustomer = Await FEB2.Customer.Find(exs, New Guid(Request.Form("customer").ToString()))
                    Dim oIncidencia As DTOIncidencia = DTOIncidencia.Factory(DTOIncidencia.ContactTypes.Professional, DTOIncidencia.Srcs.Producte)
                    With oIncidencia
                        .ContactPerson = Request.Form("contactPerson")
                        .Customer = oCustomer
                        .EmailAdr = Request.Form("emailAdr")
                        .Tel = Request.Form("tel")
                        .CustomerRef = Request.Form("customerRef")
                        .Product = oProduct
                        .SerialNumber = Request.Form("serialNumber")
                        .ManufactureDate = Request.Form("ManufactureDate")
                        .description = Request.Form("description")
                        .Procedencia = Request.Form("procedencia")
                        .UsrLog.usrLastEdited = oUser
                    End With

                    'Dim oFiles As List(Of Byte()) = MyBase.PostedFiles()
                    'Dim oDocFiles As List(Of DTODocFile) = LegacyHelper.DocfileHelper.Factory(oFiles, exs)
                    Dim oDocFiles = MyBase.PostedDocFiles(exs)
                    'SaveFile(oDocFiles.First, exs) ' to deprecate

                    Dim imgCount As Integer = Request.Form("imgCount")

                    For i As Integer = imgCount To oDocFiles.Count - 1
                        oIncidencia.PurchaseTickets.Add(oDocFiles(i))
                    Next

                    For i As Integer = 0 To imgCount - 1
                        Dim oDocfile = oDocFiles(i)
                        If oDocfile.IsVideo Then
                            oIncidencia.Videos.Add(oDocFiles(i))
                        Else
                            oIncidencia.DocFileImages.Add(oDocFiles(i))
                        End If
                    Next

                    Dim pIncidencia = Await FEB2.Incidencia.Update(exs, oIncidencia)
                    If exs.Count = 0 Then
                        oIncidencia = pIncidencia
                        If Await FEB2.Incidencia.SendMail(exs, oUser, oIncidencia, ContextHelper.Lang()) Then
                            myData = New With {.result = 1, .guid = oIncidencia.Guid.ToString}
                        Else
                            myData = New With {.result = 1, .guid = oIncidencia.Guid.ToString}
                            'TODO: Avisar qu no hem pogut enviar el correu
                        End If
                    Else
                        myData = New With {.result = 2, .status = "ERRUPDATE", .message = ContextHelper.Tradueix("Error al registrar la incidencia, por favor contacte con nuestras oficinas en 932541525 o sat@matiasmasso.es", "Error al registrar la incidencia. Si us plau contacti amb les nostres oficines al tel.932541525 o sat@matiasmasso.es ", "Sorry the system throws an error on registering the support incidence. Please contact our office at phone 932541525 or email sat@matiasmasso.es")}
                    End If

            End Select
        Else
            myData = New With {.result = 2, .status = "ERRNOPRODUCTBRAND", .message = ContextHelper.Tradueix("por favor seleccione el producto", "si us plau seleccioni el producte", "please select product")}
        End If



        Dim retval As JsonResult = Json(myData, JsonRequestBehavior.AllowGet)
        Return retval
    End Function

    Function SaveFile(oDocfile As DTODocFile, exs As List(Of Exception)) As Boolean
        Dim url = String.Format("~/Recursos/test/{0}", oDocfile.Filename)
        Dim path As String = Server.MapPath(url)
        Return MatHelperStd.FileSystemHelper.SaveStream(oDocfile.Stream, exs, path)
    End Function
#End Region

#Region "Incidencias"
    Function Incidencias() As ActionResult
        Dim exs As New List(Of Exception)
        Dim retval As ActionResult = Nothing
        Dim oUser = ContextHelper.GetUser()
        Select Case oUser.Rol.id
            Case DTORol.Ids.superUser, DTORol.Ids.admin, DTORol.Ids.salesManager, DTORol.Ids.logisticManager, DTORol.Ids.taller, DTORol.Ids.manufacturer, DTORol.Ids.rep, DTORol.Ids.comercial, DTORol.Ids.cliFull, DTORol.Ids.cliLite
                ViewBag.Title = Mvc.ContextHelper.Tradueix("Incidencias", "Incidencies", "Support incidences")
                Dim Model = DTOIncidenciaQuery.Factory(oUser, ContextHelper.Lang)
                retval = View(Model)

            Case DTORol.Ids.unregistered
                retval = MyBase.LoginOrView("Incidencias")
            Case Else
                retval = MyBase.UnauthorizedView()
        End Select
        Return retval
    End Function

    Async Function PartialIncidencias(id As Integer) As Threading.Tasks.Task(Of PartialViewResult)
        Dim exs As New List(Of Exception)
        Dim retval As PartialViewResult = Nothing
        Dim oUser = ContextHelper.GetUser()
        If exs.Count = 0 Then
            Dim oQuery = DTOIncidenciaQuery.Factory(oUser, ContextHelper.Lang, year:=id)
            Dim model = Await FEB2.Incidencias.Query(exs, oQuery)
            If exs.Count = 0 Then
                retval = PartialView("Incidencias_", model)
            Else
                retval = PartialView("_Error", exs)
            End If
        Else
            retval = PartialView("_Error", exs)
        End If
        Return retval
    End Function

    Function Excel(id As Integer) As ActionResult
        Dim exs As New List(Of Exception)
        Dim oUser = ContextHelper.GetUser()
        Dim url As String = FEB2.IncidenciaQuery.UrlExcelDoc(oUser, year:=id)
        Return Redirect(url)
    End Function

    Async Function showIncidencia(guid As Guid) As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim oIncidencia = Await FEB2.Incidencia.Find(exs, guid)
        ViewBag.BreadCrumb = DTOBreadCrumb.FromIncidencia(ContextHelper.GetLang)
        Return View("Incidencia", oIncidencia)
    End Function

    Function pageindexchangedOld(returnurl As String, pageindex As Integer) As PartialViewResult
        ViewBag.PageIndex = pageindex
        Dim retval As PartialViewResult = PartialView(returnurl)
        Return retval
    End Function

    Async Function pageindexchanged(pageindex As Integer, pagesize As Integer, guid As String) As Threading.Tasks.Task(Of PartialViewResult)
        Dim exs As New List(Of Exception)
        Dim oUser = ContextHelper.GetUser()
        Dim oItems = Await FEB2.Incidencias.Headers(exs, oUser)
        Dim Model As New List(Of DTOIncidencia)
        Dim indexFrom As Integer = pageindex * pagesize
        For i As Integer = indexFrom To indexFrom + pagesize - 1
            If i >= oItems.Count Then Exit For
            Model.Add(oItems(i))
        Next
        Dim retval As PartialViewResult = PartialView("Incidencias_", Model)
        Return retval
    End Function
#End Region


End Class