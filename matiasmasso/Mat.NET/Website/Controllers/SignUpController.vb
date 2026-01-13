Public Class SignUpController
    Inherits _MatController

    '===============================================================================================================
    ' sustituit per account/registro
    '===============================================================================================================

    Function Index() As ActionResult
        Dim oModel As New LeadViewModel
        oModel.Fase = LeadViewModel.Fases.Email
        Return View()
    End Function

    <HttpPost>
    Async Function Index(oModel As LeadViewModel) As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim retval As ActionResult = Nothing
        Select Case oModel.Fase
            Case LeadViewModel.Fases.Email
                If Not DTOUser.CheckEmailSintaxis(oModel.EmailAddress) Then
                    ModelState.AddModelError("emailAddress", ContextHelper.Tradueix("la direccion email está mal escrita", "la adreça email no es correcte", "wrong email address"))
                End If
                If ModelState.IsValid Then
                    Dim oUser = Await FEB.User.FromEmail(exs, Website.GlobalVariables.Emp, oModel.EmailAddress)
                    If oUser Is Nothing Then
                        oModel.CountryCod = "ES"
                        oModel.Fase = LeadViewModel.Fases.FillDetails
                        retval = View("fillDetails", oModel)
                    Else
                        retval = View("existingUsuari", oModel)
                    End If
                Else
                    Dim oUser = ContextHelper.GetUser()
                    oModel.EmailAddress = oUser.EmailAddress
                    retval = View(oModel)
                End If

            Case LeadViewModel.Fases.ExistingUser
                retval = View("existingUsuari", oModel)

                If oModel.Password = "" Then
                    ModelState.AddModelError("password", ContextHelper.Tradueix("la contraseña está en blanco", "falta la clau de pas", "blank password"))
                End If

                If ModelState.IsValid Then
                    Dim oValidatePasswordModel = Await FEB.User.ValidatePassword(exs, Website.GlobalVariables.Emp, oModel.EmailAddress, oModel.Password)
                    Dim oUser = oValidatePasswordModel.User
                    Dim oResult = oValidatePasswordModel.Result
                    Select Case oResult
                        Case DTOUser.ValidationResults.success
                            Await MyBase.SignInUser(oUser, True)
                            oModel.Fase = LeadViewModel.Fases.FillDetails
                            retval = View("fillDetails", oModel)
                        Case Else
                            ContextHelper.UnPersist(System.Web.HttpContext.Current)
                            ModelState.AddModelError("Password", ContextHelper.Tradueix("contraseña no válida", "clau de pas no válida", "wrong password"))
                    End Select
                End If
            Case LeadViewModel.Fases.FillDetails, LeadViewModel.Fases.Edit
                oModel.Fase = LeadViewModel.Fases.FillDetails
                retval = View("fillDetails", oModel)

                If oModel.Nom = "" Then
                    ModelState.AddModelError("Nom", ContextHelper.Tradueix("falta rellenar el nombre", "falta omplir el nom", "first name is missing"))
                End If

                If oModel.Cognoms = "" Then
                    ModelState.AddModelError("Cognoms", ContextHelper.Tradueix("falta rellenar los apellidos", "falta omplir els cognoms", "surname is missing"))
                End If

                If oModel.BirthYear = "" Then
                    ModelState.AddModelError("BirthYear", ContextHelper.Tradueix("falta rellenar el año de nacimiento", "falta omplir l'any de neixament", "birth year is missing"))
                ElseIf Not IsNumeric(oModel.BirthYear) Then
                    ModelState.AddModelError("BirthYear", ContextHelper.Tradueix("año de nacimiento incorrecto", "any de neixament incorrecte", "wrong birth year"))
                ElseIf oModel.BirthYear.Length <> 4 Then
                    ModelState.AddModelError("BirthYear", ContextHelper.Tradueix("el año de nacimiento debe tener 4 dígitos", "l'any de neixament ha de tenir 4 digits", "birth year should have 4 digits"))
                ElseIf oModel.BirthYear > DTO.GlobalVariables.Today().Year Or oModel.BirthYear < 1900 Then
                    ModelState.AddModelError("BirthYear", ContextHelper.Tradueix("año de nacimiento incorrecto", "any de neixament incorrecte", "wrong birth year"))
                Else

                End If

                If oModel.sex = DTOUser.Sexs.NotSet Then
                    ModelState.AddModelError("Sex", ContextHelper.Tradueix("indique si es hombre o mujer", "indiqui si es home o dona", "please tell us if you are a man or a women"))
                End If

                If ModelState.IsValid Then
                    Dim oUser = ContextHelper.GetUser()
                    With oUser
                        .emailAddress = oModel.EmailAddress
                        .source = DTOUser.Sources.Website
                        .nom = oModel.Nom
                        .cognoms = oModel.Cognoms
                        .birthYear = oModel.BirthYear
                        .Country = FEB.Country.FromIsoSync(oModel.CountryCod, exs)
                        .FchCreated = DTO.GlobalVariables.Now()
                        .password = TextHelper.RandomString(5)
                        .rol = New DTORol(DTORol.Ids.Guest)
                        .sex = oModel.sex
                        .tel = oModel.tel
                    End With

                    If Await FEB.User.Update(exs, oUser) Then
                        If Await MailActivationRequest(oUser, exs) Then
                            retval = View("fillDetailsThanks", oModel)
                        Else
                            ModelState.AddModelError("nom", ContextHelper.Tradueix("Error técnico. Por favor contacte con oficinas", "Error técnic. Si us plau contacti amb oficines", "Technical error. Please contact our offices"))
                        End If
                    Else
                        ModelState.AddModelError("nom", ContextHelper.Tradueix("Error técnico. Por favor contacte con oficinas", "Error técnic. Si us plau contacti amb oficines", "Technical error. Please contact our offices"))
                    End If
                End If

        End Select
        Return retval
    End Function


    <HttpGet> _
    Public Function existingUsuari() As ActionResult
        Dim retval As ActionResult = Nothing
        Stop
        Return retval
    End Function

    <HttpPost>
    Public Async Function existingUsuari(oModel As LeadViewModel) As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim retval As ActionResult = View("existingUsuari", oModel)
        Select Case oModel.Fase
            Case LeadViewModel.Fases.ExistingUser
                If oModel.Password = "" Then
                    'ModelState.AddModelError("password", ContextHelper.Tradueix("la contraseña está en blanco", "falta la clau de pas", "blank password"))
                End If

                If ModelState.IsValid Then
                    Dim oValidatePasswordModel = Await FEB.User.ValidatePassword(exs, Website.GlobalVariables.Emp, oModel.EmailAddress, oModel.Password)
                    Dim oUser = oValidatePasswordModel.User
                    Dim oResult = oValidatePasswordModel.Result
                    Select Case oResult
                        Case DTOUser.ValidationResults.success
                            Await MyBase.SignInUser(oUser, True)
                            With oModel
                                .EmailAddress = oUser.EmailAddress
                                .Nom = oUser.Nom
                                .Cognoms = oUser.Cognoms
                                .BirthYear = oUser.birthYear
                                If oUser.Country Is Nothing Then
                                    .CountryCod = "ES"
                                Else
                                    .CountryCod = oUser.Country.ISO
                                End If
                                .sex = oModel.sex
                                .tel = oModel.tel
                            End With
                            oModel.Fase = LeadViewModel.Fases.FillDetails
                            retval = View("fillDetails", oModel)
                        Case DTOUser.ValidationResults.userDeleted
                            ContextHelper.UnPersist(System.Web.HttpContext.Current)
                            ModelState.AddModelError("Password", ContextHelper.Tradueix("usuario dado debaja", "usuari donat de baixa", "deleted user"))
                        Case Else
                            ContextHelper.UnPersist(System.Web.HttpContext.Current)
                            ModelState.AddModelError("Password", ContextHelper.Tradueix("contraseña no válida", "clau de pas no válida", "wrong password"))
                    End Select
                End If
            Case LeadViewModel.Fases.FillDetails
                Stop
        End Select

        Return retval
    End Function

    Public Function Regedit() As ActionResult
        Dim exs As New List(Of Exception)
        Dim oUser = ContextHelper.GetUser()
        Dim oModel As New LeadViewModel
        With oModel
            .BirthYear = oUser.BirthYear
            .Cognoms = oUser.Cognoms
            If oUser.Country Is Nothing Then
                .CountryCod = "ES"
            Else
                .CountryCod = oUser.Country.ISO
            End If
            .EmailAddress = oUser.EmailAddress
            .Fase = LeadViewModel.Fases.Edit
            .Nom = oUser.Nom
            .sex = oUser.Sex
            .tel = oUser.Tel
            .Lang = oUser.Lang
            .LangTag = oUser.Lang.Tag
        End With
        Return View("fillDetails", oModel)
    End Function

    <HttpPost>
    Public Async Function Regedit(oModel As LeadViewModel) As Threading.Tasks.Task(Of ActionResult)
        Dim retval As ActionResult = View("fillDetails", oModel)
        Dim exs As New List(Of Exception)
        If ModelState.IsValid Then
            Dim oNewLang = DTOLang.Factory(oModel.LangTag)
            Dim oUser = ContextHelper.GetUser()
            With oUser
                .nom = oModel.Nom
                .cognoms = oModel.Cognoms
                .birthYear = oModel.BirthYear
                .Country = FEB.Country.FromIsoSync(oModel.CountryCod, exs)
                .sex = oModel.sex
                .tel = oModel.tel
                If oModel.LangTag <> .lang.Tag Then
                    .lang = DTOLang.Factory(oModel.LangTag)
                    ContextHelper.SetLangCookie(.lang)
                End If
            End With

            If Await FEB.User.Update(exs, oUser) Then
                retval = View("EditThanks", oModel)
            Else
                ModelState.AddModelError("nom", ContextHelper.Tradueix("Error técnico. Por favor contacte con oficinas", "Error técnic. Si us plau contacti amb oficines", "Technical error. Please contact our offices"))
            End If
        End If
        Return retval
    End Function

    Public Async Function MailPassword(emailAddress As String) As Threading.Tasks.Task(Of String)
        Dim retval As String = ""
        Dim exs As New List(Of Exception)
        If Await FEB.User.EmailPwd(Website.GlobalVariables.Emp, emailAddress, exs) Then
            retval = New MatJSonObject("value", 0, "text", ContextHelper.Tradueix("por favor consulte su correo, le hemos enviado su contraseña", "si us plau consulti la seva bustia, li hem enviat la clau de pas", "please look up your password in your mailbox")).ToString
        Else
            retval = New MatJSonObject("value", 1, "text", ContextHelper.Tradueix("no ha sido posible enviarle su contraseña, por favor contacte con nuestras oficinas",
                                                                                     "no ha estat possible enviar-li la clau de pas, si us plau contacti amb les nostres oficines",
                                                                                     "we have not been able to mail your password, please contact our offices")).ToString
        End If
        Return retval
    End Function

    Public Async Function MailActivationRequest(oUser As DTOUser, exs As List(Of Exception)) As Threading.Tasks.Task(Of Boolean)
        Return Await FEB.User.EmailActivationRequest(exs, Website.GlobalVariables.Emp, oUser)
    End Function


    <HttpPost> _
    Function fillDetails(oModel As LeadViewModel) As ActionResult
        Dim retval As ActionResult = Nothing
        If ModelState.IsValid Then
            retval = View("fillDetailsThanks", oModel)
        End If
        Return retval
    End Function



End Class