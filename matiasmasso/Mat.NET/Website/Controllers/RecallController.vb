Public Class RecallController
    Inherits _MatController

    Async Function Index() As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim retval As ActionResult = Nothing
        Dim oUser = ContextHelper.GetUser()
        If oUser Is Nothing Then
            retval = MyBase.LoginOrView()
        Else
            Select Case oUser.Rol.Id
                Case DTORol.Ids.CliFull, DTORol.Ids.CliLite, DTORol.Ids.Rep, DTORol.Ids.Comercial, DTORol.Ids.SuperUser
                    Dim oRecall As DTORecall = DTORecall.Wellknown(DTORecall.Wellknowns.DualfixR44)
                    Dim Model = Await FEB.RecallCli.Factory(exs, oUser, oRecall)
                    retval = View("Recall", Model)
                Case DTORol.Ids.Unregistered
                    retval = MyBase.LoginOrView()
                Case Else
                    retval = MyBase.UnauthorizedView()
            End Select
        End If

        Return retval
    End Function

    Public Async Function Save() As Threading.Tasks.Task(Of JsonResult)
        Dim exs As New List(Of Exception)
        Dim myData As Object = Nothing

        Dim oUser = ContextHelper.GetUser()
        Dim oRecall As DTORecall = DTORecall.Wellknown(DTORecall.Wellknowns.DualfixR44)
        Dim oRecallCli = Await FEB.RecallCli.Factory(exs, oUser, oRecall)
        With oRecallCli
            .ContactNom = Request.Form("ContactNom")
            .ContactTel = Request.Form("ContactTel")
            .ContactEmail = Request.Form("ContactEmail")
            .Customer = New DTOCustomer(New Guid(Request.Form("Customer")))
            .Address = Request.Form("Address")
            .Zip = Request.Form("Zip")
            .Location = Request.Form("Location")
            .Country = New DTOCountry(New Guid(Request.Form("Pais")))
        End With

        Dim s As String = Request.Form("products")
        For Each row As String In s.Split(";")
            Dim fields() As String = row.Split(",")
            Dim oGuid As New Guid(fields(0))
            Dim oProduct As New DTORecallProduct()
            oProduct.SerialNumber = fields(1)
            oProduct.Sku = New DTOProductSku(oGuid)
            oRecallCli.Products.Add(oProduct)
        Next

        If Await FEB.RecallCli.Update(exs, oRecallCli) Then
            If FEB.RecallCli.Load(exs, oRecallCli) Then
                Dim oPdfStream As Byte() = LegacyHelper.PdfRecallLabel.Factory(oRecallCli)
                Dim oMailMessage As DTOMailMessage = Await FEB.RecallCli.MailMessage(oRecallCli, oPdfStream, exs)
                If Await FEB.MailMessage.Send(exs, oUser, oMailMessage) Then
                    myData = New With {.result = 1, .guid = oRecallCli.Guid.ToString}
                Else
                    myData = New With {.result = 2, .status = "ERRUPDATE", .message = ContextHelper.Tradueix("Error al enviar su solicitud, por favor contacte con nuestras oficinas en 932541522 o info@matiasmasso.es", "Error al enviar la solicitut. Si us plau contacti amb les nostres oficines al tel.932541522 o info@matiasmasso.es ", "Sorry the system throws an error on sending the form. Please contact our office at phone 932541522 or email info@matiasmasso.es")}
                End If
            Else
                myData = New With {.result = 2, .status = "ERRUPDATE", .message = ContextHelper.Tradueix("Error al enviar su solicitud, por favor contacte con nuestras oficinas en 932541522 o info@matiasmasso.es", "Error al enviar la solicitut. Si us plau contacti amb les nostres oficines al tel.932541522 o info@matiasmasso.es ", "Sorry the system throws an error on sending the form. Please contact our office at phone 932541522 or email info@matiasmasso.es")}
            End If
        Else
            myData = New With {.result = 2, .status = "ERRUPDATE", .message = ContextHelper.Tradueix("Error al registrar su solicitud, por favor contacte con nuestras oficinas en 932541522 o info@matiasmasso.es", "Error al registrar la solicitut. Si us plau contacti amb les nostres oficines al tel.932541522 o info@matiasmasso.es ", "Sorry the system throws an error on registering the form. Please contact our office at phone 932541522 or email info@matiasmasso.es")}
        End If


        Dim retval As JsonResult = Json(myData, JsonRequestBehavior.AllowGet)
        Return retval
    End Function

End Class
