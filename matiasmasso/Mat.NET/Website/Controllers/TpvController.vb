Public Class TpvController
    Inherits _MatController

    <HttpGet>
    Public Async Function Index(Optional Base64Json As String = "") As Threading.Tasks.Task(Of ActionResult) '(Cod As Nullable(Of Tpv.Modes), guid As Nullable(Of Guid)) As ActionResult
        Dim exs As New List(Of Exception)
        Dim oLang As DTOLang = ContextHelper.Lang()
        Dim oMode As DTOTpvRequest.Modes = DTOTpvRequest.Modes.Free
        Dim oGuid As Guid
        Dim oModel As DTOTpvRequest

        If Base64Json = "" Then
            oMode = DTOTpvRequest.Modes.Free
            oModel = Await FEB.TPVRequest.FromMode(oMode, oGuid, oLang, exs)

            oMode = DTOTpvRequest.Modes.Free
            Dim oUser = ContextHelper.GetUser()
            If oUser Is Nothing Then
                Return LoginOrView()
            Else
                oGuid = oUser.Guid
                oModel = Await FEB.TPVRequest.FromMode(oMode, oGuid, oLang, exs)
            End If
        Else
            Dim oParams As Dictionary(Of String, String) = CryptoHelper.FromUrlFriendlyBase64Json(Base64Json)
            oMode = oParams("Mode")
            oGuid = New Guid(oParams("Guid"))
            If oMode = DTOTpvRequest.Modes.Free Then
                Dim oTpvLog = Await FEB.TpvLog.Find(oGuid, exs)
                oModel = New DTOTpvRequest(DTOTpvRequest.Modes.Free)
                With oModel
                    .Concept = oTpvLog.Ds_ProductDescription
                    .Eur = DTOTpvLog.Amt(oTpvLog).Eur
                End With
            Else
                oModel = Await FEB.TPVRequest.FromMode(oMode, oGuid, oLang, exs)
            End If
        End If

        If exs.Count = 0 Then
            ViewBag.Title = ContextHelper.Tradueix("Pagos mediante tarjeta de crédito", "Pagaments per tarja de crèdit", "Credit card payments")
            Return View(oModel)
        Else
            Return View("Exs", exs)
        End If
    End Function

    <HttpPost>
    Public Async Function Index(oModel As DTOTpvRequest) As Threading.Tasks.Task(Of ActionResult)
        Dim retval As ActionResult = Nothing
        Dim exs As New List(Of Exception)
        If oModel.Concept = "" Then
            ModelState.AddModelError("Concepte", ContextHelper.Tradueix("el concepto no puede quedar vacío", "el concepte no pot quedar buit", "concept should not be empty"))
        End If

        Select Case oModel.Mode
            Case DTOTpvRequest.Modes.Free
                If oModel.Eur < 1 Then
                    ModelState.AddModelError("Eur", ContextHelper.Tradueix("importe vacío", "import buit", "empty amount"))
                End If
        End Select

        If ModelState.IsValid Then
            Dim oUser = ContextHelper.GetUser()

            Dim oConfig As DTOPaymentGateway = Await FEB.TpvRedSys.Config(exs, Website.GlobalVariables.Emp, DTOPaymentGateway.Environments.Production)
            Dim oTpvRedsys As DTOTpvRedsysPasarela = Await FEB.TpvRedSys.SignedRequest(exs, oConfig, oUser, ContextHelper.GetLang, oModel.Mode, oModel.Ref, DTOAmt.Factory(oModel.Eur), oModel.Concept)
            retval = View("Pasarela", oTpvRedsys)
        Else
            retval = View(oModel)
        End If
        Return retval
    End Function

    <HttpPost>
    Public Shadows Async Function Log() As Threading.Tasks.Task
        Dim exs As New List(Of Exception)
        Dim sb As New Text.StringBuilder
        Dim sOrder As String = ""
        Dim sTitular As String = ""


        Try
            Dim oTpvLog As DTOTpvLog = Await GetLogFromContext()
            If oTpvLog Is Nothing Then
                exs.Add(New Exception("oTpvLog empty"))
            Else
                If Await FEB.TpvRedSys.LogResponse(Website.GlobalVariables.Emp, oTpvLog, exs) Then
                    'avisa a les noies del resultat de la operació
                    Dim oMailMessage = DTOTpvLog.MailMessage(oTpvLog)
                    Dim oUser = Await FEB.User.Find(DTOUser.Wellknown(DTOUser.Wellknowns.info).Guid, exs)
                    Await FEB.MailMessage.Send(exs, oUser, oMailMessage)

                    If oTpvLog Is Nothing Then
                        exs.Add(New Exception("oTpvLog Is Nothing"))
                    Else
                        sOrder = oTpvLog.Ds_Order
                        If oTpvLog.Titular Is Nothing Then
                            exs.Add(New Exception("oTpvLog.Titular Is Nothing"))
                        Else
                            sTitular = oTpvLog.Titular.FullNom
                        End If
                    End If

                Else
                    exs.Add(New Exception("error en FEB.TpvRedSys.LogResponse"))
                End If
            End If

        Catch ex As Exception
            exs.Add(ex)
        Finally

            Dim sSubject = String.Format("Tpv {0} de {1}", sOrder, sTitular)
            sb.AppendLine("<table>")
            If exs.Count > 0 Then
                sb.AppendLine("<tr><td colspan='2'>Errors:</td></tr>")
                For Each ex In exs
                    sb.AppendFormat("<tr><td>{0}</td></tr>", ex.Message)
                Next
                sb.AppendLine("<tr><td colspan='2'>&nbsp;</td></tr>")
            End If
            sb.AppendLine("<tr><td colspan='2'>Form Elements:</td></tr>")
            For Each key As String In Me.HttpContext.Request.Form.AllKeys
                sb.AppendFormat("<tr><td>{0}</td><td>{1}</td></tr>", key, Me.HttpContext.Request.Form(key))
            Next
            sb.AppendLine("</table>")

            exs = New List(Of Exception)
            Dim oMailMessage As DTOMailMessage = MailAdmin(sSubject, sb.ToString())
            FEB.MailMessage.SendSync(DTOUser.Wellknown(DTOUser.Wellknowns.matias), oMailMessage, exs)
        End Try
    End Function

    Shared Function MailAdmin(subject As String, body As String) As DTOMailMessage
        Dim retval = DTOMailMessage.Factory("matias@matiasmasso.es")
        With retval
            .subject = subject
            .body = body
        End With
        Return retval
    End Function

    Public Async Function Ok() As Threading.Tasks.Task(Of ActionResult)
        Dim oTpvLog As DTOTpvLog = Await GetLogFromContext()
        Return View(oTpvLog)
    End Function

    Public Async Function Ko() As Threading.Tasks.Task(Of ActionResult)
        Dim oTpvLog As DTOTpvLog = Await GetLogFromContext()
        Return View(oTpvLog)
    End Function

    Private Async Function GetLogFromContext() As Threading.Tasks.Task(Of DTOTpvLog)
        Dim exs As New List(Of Exception)
        Dim MerchantParameters As String = Me.HttpContext.Request.Form("Ds_MerchantParameters")
        Dim signatureReceived As String = Me.HttpContext.Request.Form("Ds_Signature")

        If MerchantParameters = "" Then
            MerchantParameters = Me.HttpContext.Request.QueryString("Ds_MerchantParameters")
            signatureReceived = Me.HttpContext.Request.QueryString("Ds_Signature")
        End If

        Dim retval As DTOTpvLog = Await FEB.TpvRedSys.GetLogFromEncriptedData(Website.GlobalVariables.Emp, MerchantParameters, signatureReceived)
        Return retval
    End Function


End Class
