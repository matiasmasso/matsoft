Public Class TpvTestController
    Inherits _MatController

    <HttpGet>
    Public Function Index(Cod As Nullable(Of Tpv.Modes), guid As Nullable(Of Guid)) As ActionResult
        If guid Is Nothing Then guid = MyBase.GetSession.User.Guid
        If Cod Is Nothing Then Cod = Tpv.Modes.Free
        Dim oModel As DTOTpvRequest = BLLTpvRequest.FromCod(GetSession.Lang, Cod, guid)
        If oModel.Mode = DTOTpvRequest.Modes.Free Then
            Return LoginOrView(, oModel)
        Else
            Return View(oModel)
        End If
    End Function

    <HttpPost>
    Public Function Index(oModel As DTOTpvRequest) As ActionResult
        Dim retval As ActionResult = Nothing
        If oModel.HisConcepte = "" Then
            ModelState.AddModelError("Concepte", GetSession.Tradueix("el concepto no puede quedar vacío", "el concepte no pot quedar buit", "concept should not be empty"))
        End If

        Select Case oModel.Mode
            Case TpvPayment.Modes.Free
                If oModel.Eur < 1 Then
                    ModelState.AddModelError("Eur", GetSession.Tradueix("importe vacío", "import buit", "empty amount"))
                End If
        End Select

        If ModelState.IsValid Then
            Dim oTpvRedsys As DTOTpvRedsysPasarela = BLLTpvRedsys.SignedRequest(DTOSermepaConfig.Environments.Testing, MyBase.GetSession.User, oModel.Mode, oModel.Ref, BLLApp.GetAmt(oModel.Eur), MyBase.GetSession.Lang, oModel.HisConcepte)
            retval = View("Pasarela", oTpvRedsys)
        Else
            retval = View(oModel)
        End If
        Return retval
    End Function

    <HttpPost>
    Public Sub Log()

        Dim Ds_MerchantParameters As String = Me.HttpContext.Request.Form("Ds_MerchantParameters")
        Dim Ds_Signature As String = Me.HttpContext.Request.Form("Ds_Signature")

        Dim exs As New List(Of Exception)
            If Not BLLTpvRedsys.LogResponse(Ds_MerchantParameters, Ds_Signature, exs) Then
            MailHelper.WarnException("Error al registrar cobrament via Tpv", exs)
        End If

    End Sub

    Public Function Ok() As ActionResult
        Return View()
    End Function

    Public Function Ko() As ActionResult
        Return View()
    End Function

End Class
