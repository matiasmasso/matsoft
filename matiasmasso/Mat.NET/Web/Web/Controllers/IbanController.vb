Public Class IbanController
    Inherits _MatController

    '
    ' GET: /IbanMandato

    Function Index() As ActionResult
        Dim retval As ActionResult = Nothing

        Select Case MyBase.Authorize({DTORol.Ids.CliFull, DTORol.Ids.CliLite})
            Case AuthResults.success
                ViewBag.Title = Mvc.ContextHelper.Tradueix("Mandato de Domiciliación Bancaria",
                                  "Mandat de Domiciliaciò Bancària",
                                  "Direct Debit Mandate")
                retval = View("Download")
            Case AuthResults.login
                retval = MyBase.Login()
            Case AuthResults.denied
                retval = MyBase.UnauthorizedView()
        End Select

        Return retval
    End Function

    Async Function Validate(src As String) As Threading.Tasks.Task(Of JsonResult)
        Dim exs As New List(Of Exception)
        Dim isValidated As Boolean = DTOIban.ValidateDigits(src)
        Dim formated As String = DTOIban.Formated(src)
        Dim text As String = ""


        If src.Length >= 8 Then
            Dim oBranch = Await FEB2.IbanStructure.GetBankBranch(src, exs)
            If oBranch Is Nothing Then
                Dim oBank = Await FEB2.IbanStructure.GetBank(src, exs)
                If oBank IsNot Nothing Then
                    text = DTOBank.NomComercialORaoSocial(oBank)
                End If
            Else
                text = DTOBankBranch.FullNomAndAddress(oBranch)
            End If
        End If


        Dim oData As Object = New With {.isvalidated = isValidated, .text = text, .formated = formated}
        Dim retval As JsonResult = Json(oData, JsonRequestBehavior.AllowGet)
        Return retval
    End Function

    Async Function Save(digits As String, titular As Guid, personNom As String, personDni As String) As Threading.Tasks.Task(Of JsonResult)
        Dim exs As New List(Of Exception)
        Dim oData As Object = Nothing
        Dim oUser = ContextHelper.GetUser()
        Dim url As String = ""
        Dim cleanDigits As String = DTOIban.CleanCcc(digits)
        If personNom = "" And personDni = "" Then
            oData = New With {.status = 2,
                                        .message = MyBase.Tradueix("Por favor indique el nombre y DNI de la persona que firma",
                                          "Si us plau indiqui el nom i el DNI de la persona que signa",
                                          "Please fill the name and Id of the signee person")}
        ElseIf personNom = "" Then
            oData = New With {.status = 2,
                                            .message = MyBase.Tradueix("Por favor indique el nombre de la persona que firma",
                                              "Si us plau indiqui el nom de la persona que signa",
                                              "Please fill the name of the signee person")}
        ElseIf personDni = "" Then
            oData = New With {.status = 2,
                                                .message = MyBase.Tradueix("Por favor indique el DNI de la persona que firma",
                                                  "Si us plau indiqui el DNI de la persona que signa",
                                                  "Please fill the Id of the signee person")}
        ElseIf digits = "" Then
            oData = New With {.status = 2,
                                                .message = MyBase.Tradueix("Por favor rellene el IBAN completo de su cuenta corriente",
                                                  "Si us plau ompli tots els digits del IBAN del seu compte corrent",
                                                  "Please fill the full IBAN digits of your current account")}

        ElseIf DTOIban.ValidateDigits(cleanDigits) Then
            Dim oPendingIbans = Await FEB2.Ibans.PendingUploads(exs, oUser)
            Dim oIban As DTOIban = oPendingIbans.Find(Function(x) x.Digits = cleanDigits And x.Titular.Guid.Equals(titular) And x.FchTo = Nothing)
            If oIban Is Nothing Then
                Dim oTitular As New DTOContact(titular)
                'If CheckIsAlreadyActive(cleanDigits, oTitular) Then
                'oData = New With {.status = 2,
                '.message = MyBase.Tradueix("Esta cuenta ya está activa, no necesita volver a subirla",
                '                         "Aquest compte ja está actiu, no cal tornar-lo a pujar",
                '                        "This account is already active, you don't need to upload it again")}
                'Else
                oIban = Await FEB2.Iban.NewForDownload(cleanDigits, titular, oUser)
                oIban.PersonNom = personNom
                oIban.PersonDni = personDni

                If Await FEB2.Iban.Update(oIban, exs) Then
                    oData = New With {.status = 1, .url = FEB2.Iban.DownloadUrl(oIban, False), .guid = oIban.Guid.ToString, .iban = FEB2.Iban.ToHtml(oIban)}
                Else
                    oData = New With {.status = 0}
                    Dim oWinbug = DTOWinBug.Factory("IbanController: error al registrar Iban " & cleanDigits & " a client " & titular.ToString, oUser)
                    FEB2.WinBug.UpdateSync(exs, oWinbug)
                End If
                'End If
            Else
                oData = New With {.status = 1, .url = FEB2.Iban.DownloadUrl(oIban, False), .guid = oIban.Guid.ToString}
            End If
        Else
            oData = New With {.status = 2,
                                .message = MyBase.Tradueix("Digitos incorrectos. Por favor verifique los datos de su cuenta bancaria",
                                      "Digits incorrectes. Sisplau verifiqui les dades del seu compte bancari",
                                      "Wrong digits. Please check the details of your bank account")}
        End If


        Dim retval As JsonResult = Json(oData, JsonRequestBehavior.AllowGet)
        Return retval
    End Function

    Private Async Function CheckIsAlreadyActive(cleanDigits As String, oTitular As DTOContact) As Threading.Tasks.Task(Of Boolean)
        Dim exs As New List(Of Exception)
        Dim oIbans As List(Of DTOIban) = Await FEB2.Ibans.FromContact(exs, oTitular, OnlyVigent:=True, oCod:=DTOIban.Cods.Client)
        Dim retval As Boolean = oIbans.Exists(Function(x) x.Digits = cleanDigits)
        Return retval
    End Function

    Async Function Upload() As Threading.Tasks.Task(Of JsonResult)
        Dim oData As Object = Nothing
        Dim oFile As Byte() = MyBase.PostedFile
        If oFile IsNot Nothing Then
            Dim exs As New List(Of Exception)
            Dim sGuid As String = Request.Form("guid")
            Dim oDocfile = LegacyHelper.DocfileHelper.Factory(exs, oFile)
            If oDocfile Is Nothing Then
                oData = New With {.result = 2, .status = "ERR"}
                Dim oWinbug = DTOWinBug.Factory("IbanController.Upload oDocFile=Nothing Iban.Guid=" & sGuid)
                FEB2.WinBug.UpdateSync(exs, oWinbug)
            Else
                Dim oGuid As New Guid(sGuid)
                Dim oIban As New DTOIban(oGuid)
                Dim oUser = ContextHelper.GetUser()

                If Await FEB2.Iban.Upload(oIban, oDocfile, oUser, exs) Then

                    oData = New With {.result = 1}

                    Dim oMailMessage = DTOMailMessage.Factory(DTOMailMessage.wellknownAddress(DTOMailMessage.wellknownRecipients.Cuentas))
                    If oDocfile.mime = MimeCods.NotSet Then
                        With oMailMessage
                            .subject = "Error al pujar Iban signat de " & oIban.titular.FullNom
                            .body = "Fitxer amb format desconegut " & FEB2.DocFile.DownloadUrl(oDocfile, True)
                        End With
                    Else
                        With oMailMessage
                            .subject = "Verificar Iban signat de " & oIban.titular.FullNom
                        End With
                    End If
                    Await FEB2.MailMessage.Send(exs, oUser, oMailMessage)
                Else
                    oData = New With {.result = 2, .status = "ERR"}
                    Dim oWinbug = DTOWinBug.Factory("IbanController.Upload failed Iban.Guid=" & sGuid & "exs.message=" & ExceptionsHelper.ToFlatString(exs))
                    FEB2.WinBug.UpdateSync(exs, oWinbug)
                End If

            End If
        Else
            oData = New With {.result = 2, .status = "NOFILES"}
        End If
        Dim retval As JsonResult = Json(oData, JsonRequestBehavior.AllowGet)
        Return retval
    End Function



    Function IsValidated(src As String) As JsonResult
        Dim exs As New List(Of Exception)
        Dim validated As Boolean = DTOIban.ValidateDigits(src)
        Dim oData As Object = New With {.isvalidated = validated}
        Dim retval As JsonResult = Json(oData, JsonRequestBehavior.AllowGet)
        Return retval
    End Function

End Class