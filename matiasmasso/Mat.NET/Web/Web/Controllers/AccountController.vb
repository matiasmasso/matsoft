Public Class AccountController
    Inherits _MatController

    Function CheckPassword(password As String) As JsonResult
        Dim exs As New List(Of Exception)
        Dim verified As Boolean
        Dim oUser = ContextHelper.GetUser
        If FEB2.User.Load(exs, oUser) Then
            verified = (password = oUser.password)
        End If

        Dim myData As Object = New With {.success = verified}
        Dim retval As JsonResult = Json(myData, JsonRequestBehavior.AllowGet)
        Return retval
    End Function

    Function Registro() As ActionResult
        Dim Model As DTOUser.Sources = DTOUser.Sources.website
        ViewBag.Title = Mvc.ContextHelper.Tradueix("Registro de usuario", "Registre d'usuari", "Sign up form", "Formulário de inscrição")
        Return View(Model)
    End Function

    Function SignedUp() As ActionResult
        ViewBag.Title = Mvc.ContextHelper.Tradueix("Formulario de registro", "Formulari de registre", "Sign up form", "Formulário de inscrição")
        Return View("SignUp_Thanks")
    End Function

    Function RegistroBlog() As ActionResult
        Dim Model As DTOUser.Sources = DTOUser.Sources.Blog
        Return View(Model)
    End Function

    Function RegistroFacebook() As ActionResult
        Dim Model As DTOUser.Sources = DTOUser.Sources.Facebook
        Return View("Registro", Model)
    End Function

    Function RegistroFacebook4moms() As ActionResult
        Dim Model As DTOUser.Sources = DTOUser.Sources.Fb4moms
        Return View("Registro", Model)
    End Function

    Async Function VerifyEmail(email As String) As Threading.Tasks.Task(Of JsonResult)
        'Dim SessionGuid As Guid = Session("SessionId")
        'Stop
        Dim exs As New List(Of Exception)
        email = email.Trim.ToLower.Replace(" ", "")
        Dim myData As Object
        If DTOUser.CheckEmailSintaxis(email) Then
            If Await FEB2.User.Exists(exs, GlobalVariables.Emp, email) Then
                myData = New With {.result = CInt(DTOUser.ValidationResults.Success), .color = "blue", .text = "este email ya está registrado como Usero.<br/>Por favor introduzca su contraseña en la siguiente casilla.<br/>Si no la sabe o no la recuerda deje la contraseña en blanco y se la enviaremos por correo"}
            Else
                myData = New With {.result = CInt(DTOUser.ValidationResults.EmailNotRegistered), .color = "red", .text = "email no registrado. Le estamos enviando un código de verificación."}
            End If
        Else
            If email = "" Then
                myData = New With {.result = CInt(DTOUser.ValidationResults.EmptyEmail), .color = "red", .text = "la direccion email no puede estar vacía"}
            Else
                myData = New With {.result = CInt(DTOUser.ValidationResults.WrongEmail), .color = "red", .text = "direccion email incorrecta"}
            End If
        End If
        Dim retval As JsonResult = Json(myData, JsonRequestBehavior.AllowGet)
        Return retval
    End Function

    Async Function EmailVerificationCode(email As String) As Threading.Tasks.Task(Of JsonResult)
        Dim exs As New List(Of Exception)
        Dim emailAddress = email.Trim.ToLower.Replace(" ", "")
        Dim myData As Object
        If Await FEB2.User.EmailAddressVerification(exs, GlobalVariables.Emp, emailAddress) Then
            myData = New With {.result = CInt(DTOUser.ValidationResults.Success), .color = "blue", .text = "entre en la casilla siguiente la contraseña que le hemos enviado por correo"}
        Else
            myData = New With {.result = CInt(DTOUser.ValidationResults.SystemError), .color = "red", .text = "se ha producido un error, por favor inténtelo más tarde o contacte con nuestras oficinas"}
        End If
        Dim retval As JsonResult = Json(myData, JsonRequestBehavior.AllowGet)
        Return retval
    End Function

    Async Function onEmailAuthentication(email As String) As Threading.Tasks.Task(Of JsonResult)
        'actualitza l'User loguejat via SigninOrSignup en comentaris o raffles
        Dim exs As New List(Of Exception)
        Dim msg As String = ""
        Dim oUser = Await FEB2.User.FromEmail(exs, GlobalVariables.Emp, email)
        If oUser Is Nothing Then
            msg = "SYSERR_104"
        Else
            Await MyBase.SignInUser(oUser, persist:=True)
        End If

        Dim myData As Object = New With {.text = msg}
        Dim retval As JsonResult = Json(myData, JsonRequestBehavior.AllowGet)
        Return retval
    End Function

    Async Function EmailPassword(email As String) As Threading.Tasks.Task(Of JsonResult)
        email = email.Trim.ToLower.Replace(" ", "")
        Dim myData As Object
        Dim exs As New List(Of Exception)
        If Await FEB2.User.EmailPwd(GlobalVariables.Emp, email, exs) Then
            myData = New With {.result = CInt(DTOUser.ValidationResults.Success), .color = "blue", .text = "entre en la casilla siguiente la contraseña que le hemos enviado por correo"}
        Else
            myData = New With {.result = CInt(DTOUser.ValidationResults.SystemError), .color = "red", .text = "se ha producido un error, por favor inténtelo más tarde o contacte con nuestras oficinas"}
        End If
        Dim retval As JsonResult = Json(myData, JsonRequestBehavior.AllowGet)
        Return retval
    End Function

    Async Function VerifyPwd(email As String, pwd As String) As Threading.Tasks.Task(Of JsonResult)
        Dim exs As New List(Of Exception)
        email = email.Trim.ToLower.Replace(" ", "")
        Dim oUser As DTOUser = Nothing
        Dim myData As Object = Nothing
        Dim oValidatePasswordModel = Await FEB2.User.ValidatePassword(exs, GlobalVariables.Emp, email, pwd)
        Select Case oValidatePasswordModel.result
            Case DTOUser.ValidationResults.success
                'Session("WebSession") = MVC.WebSession.FromMatSession(oMatSession) TODO: actualitzar sessio amb l'User validat
                oUser = oValidatePasswordModel.User
                Dim sCountry As String = ""
                If oUser.Country IsNot Nothing Then
                    sCountry = oUser.Country.Guid.ToString.ToLower()
                End If
                myData = New With {.result = CInt(DTOUser.ValidationResults.success), .text = "", .nom = oUser.Nom, .cognoms = oUser.Cognoms, .nickname = oUser.NickName, .sex = CInt(oUser.sex), .birthyear = oUser.birthYear, .Country = sCountry, .Zipcod = oUser.ZipCod, .tel = oUser.tel}
            Case DTOUser.ValidationResults.userDeleted
                myData = New With {.result = CInt(DTOUser.ValidationResults.userDeleted), .color = "red", .text = "este email ha sido dado de baja. Si desea reactivarlo puede ponerse en contacto con nuestras oficinas."}
            Case DTOUser.ValidationResults.newValidatedUser
                oUser = Await ContextHelper.SignUpNewLead(exs, System.Web.HttpContext.Current, ContextHelper.Lang(), GlobalVariables.Emp, email, pwd)
                If exs.Count = 0 Then
                    myData = New With {.result = CInt(DTOUser.ValidationResults.newValidatedUser), .text = ""}
                Else
                    myData = New With {.result = CInt(DTOUser.ValidationResults.systemError), .text = "Se ha producido un error en el proceso, por favor contacte con nuestras oficinas"}
                End If
            Case DTOUser.ValidationResults.emptyPassword
                oUser = Await FEB2.User.FromEmail(exs, GlobalVariables.Emp, email)
                If exs.Count = 0 Then
                    If Await FEB2.User.EmailPassword(oUser, exs) Then
                        myData = New With {.result = CInt(DTOUser.ValidationResults.emptyPassword), .text = "Consulte su correo, le hemos enviado la contraseña"}
                    Else
                        myData = New With {.result = CInt(DTOUser.ValidationResults.systemError), .text = "Se ha producido un error en el proceso, por favor contacte con nuestras oficinas"}
                    End If
                Else
                    myData = New With {.result = CInt(DTOUser.ValidationResults.systemError), .text = "Se ha producido un error en el proceso, por favor contacte con nuestras oficinas"}
                End If
            Case DTOUser.ValidationResults.wrongPassword
                myData = New With {.result = CInt(DTOUser.ValidationResults.wrongPassword), .text = "Contraseña errónea"}
            Case DTOUser.ValidationResults.emailNotRegistered
                Dim sVerificationCode As String = DTOUser.VerificationCode(email)
                If sVerificationCode = pwd Then
                    myData = New With {.result = CInt(DTOUser.ValidationResults.newValidatedUser), .text = ""}
                Else
                    myData = New With {.result = CInt(DTOUser.ValidationResults.wrongPassword), .text = "Contraseña errónea"}
                End If
        End Select
        Dim retval As JsonResult = Json(myData, JsonRequestBehavior.AllowGet)
        Return retval
    End Function

    Async Function GetUsuari(email As String) As Threading.Tasks.Task(Of JsonResult)
        Dim exs As New List(Of Exception)
        email = email.Trim.ToLower.Replace(" ", "")
        Dim myData As Object = Nothing
        Dim oUser = Await FEB2.User.FromEmail(exs, GlobalVariables.Emp, email)
        If oUser IsNot Nothing Then
            Dim sCountry As String = ""
            If oUser.Country Is Nothing Then
                sCountry = GlobalVariables.Emp.DefaultCountry.Guid.ToString
            Else
                sCountry = oUser.Country.Guid.ToString
            End If
            myData = New With {.result = CInt(DTOUser.ValidationResults.Success), .text = "", .nom = oUser.Nom, .cognoms = oUser.Cognoms, .nickname = oUser.NickName, .sex = CInt(oUser.Sex), .birthyear = oUser.BirthYear, .Country = sCountry, .Zipcod = oUser.ZipCod, .tel = oUser.Tel}
        Else
            myData = New With {.result = CInt(DTOUser.ValidationResults.EmailNotRegistered)}
        End If
        Dim retval As JsonResult = Json(myData, JsonRequestBehavior.AllowGet)
        Return retval
    End Function

    Async Function UpdateUsuari(Src As Integer,
                          email As String,
                          Firstname As String,
                          Surname As String,
                          NickName As String,
                          Country As Guid,
                          Zipcod As String,
                          Tel As String,
                          BirthYear As String) As Threading.Tasks.Task(Of JsonResult)

        email = email.Trim.ToLower.Replace(" ", "")
        Dim oResult As DTOUser.ValidationResults
        Dim myData As Object
        Try
            Dim oCountry As New DTOCountry(Country)
            If DTOZip.Validate(oCountry, Zipcod) Then
                Dim iBirthYear As Integer
                If IsNumeric(BirthYear) Then iBirthYear = CInt(BirthYear)
                oResult = Await FEB2.User.UpdateUser(Src, email, Firstname, Surname, NickName, Country, Zipcod, Tel, iBirthYear)
                myData = New With {.result = oResult, .text = ""}
            Else
                myData = New With {.result = CInt(DTOUser.ValidationResults.WrongZip), .text = ""}
            End If
        Catch ex As Exception
            myData = New With {.result = CInt(DTOUser.ValidationResults.SystemError), .text = ""}
        End Try
        Dim retval As JsonResult = Json(myData, JsonRequestBehavior.AllowGet)
        Return retval
    End Function

    <HttpGet>
    Shadows Function login() As ActionResult
        Dim s As String = Request.QueryString("returnurl")
        'If s = "" Then s = "/pro"
        Dim oModel As New LoginViewModel
        oModel.ReturnUrl = s
        oModel.Persist = True
        Return View(oModel)
    End Function

    <HttpPost>
    Shadows Async Function login(oModel As LoginViewModel) As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim retval As ActionResult = Nothing
        Dim oUser As DTOUser = Nothing
        If String.IsNullOrEmpty(oModel.EmailAddress.Trim) Then
            ContextHelper.UnPersist(System.Web.HttpContext.Current)
            ModelState.AddModelError("emailAddress", ContextHelper.Tradueix("email vacío", "email buit", "empty email address"))
        Else
            Dim oValidationPasswordModel = Await FEB2.User.ValidatePassword(exs, GlobalVariables.Emp, oModel.EmailAddress.Trim.ToLower, oModel.Password)
            oUser = oValidationPasswordModel.User
            Select Case oValidationPasswordModel.Result
                Case DTOUser.ValidationResults.success
                    Await MyBase.SignInUser(oUser, persist:=oModel.Persist)
                    Dim oLang = ContextHelper.GetLang()

                Case DTOUser.ValidationResults.emailNotRegistered, DTOUser.ValidationResults.newValidatedUser
                    ContextHelper.UnPersist(System.Web.HttpContext.Current)
                    ModelState.AddModelError("emailAddress", ContextHelper.Tradueix("email no registrado", "email no registrat", "unknown email address"))

                Case DTOUser.ValidationResults.emptyEmail
                    ContextHelper.UnPersist(System.Web.HttpContext.Current)
                    ModelState.AddModelError("emailAddress", ContextHelper.Tradueix("email vacío", "email buit", "empty email address"))

                Case DTOUser.ValidationResults.wrongEmail
                    ContextHelper.UnPersist(System.Web.HttpContext.Current)
                    ModelState.AddModelError("emailAddress", ContextHelper.Tradueix("formato de email no válido", "format invalid de email", "wrong email format"))

                Case DTOUser.ValidationResults.wrongPassword
                    ContextHelper.UnPersist(System.Web.HttpContext.Current)
                    ModelState.AddModelError("emailAddress", ContextHelper.Tradueix("contraseña no válida", "clau de pas no válida", "wrong password"))

                Case DTOUser.ValidationResults.emptyPassword
                    ContextHelper.UnPersist(System.Web.HttpContext.Current)
                    oUser = Await FEB2.User.FromEmail(exs, GlobalVariables.Emp, oModel.EmailAddress)
                    If exs.Count = 0 Then
                        If oUser Is Nothing Then
                            ModelState.AddModelError("emailAddress", ContextHelper.Tradueix("email no registrado. Puedes registrarte pinchando en el enlace que encontrarás más abajo.", "email no registrat. Pots registrar-te fent clic a l'enllaç que trobaràs mes avall.", "unknown email address. To sign up click on the appropiate link below."))
                        Else
                            If Await FEB2.User.EmailPassword(oUser, exs) Then
                                ModelState.AddModelError("emailAddress", ContextHelper.Tradueix("por favor consulte su correo, le hemos enviado su contraseña", "si us plau consulti la seva bustia, li hem enviat la clau de pas", "please look up your password in your mailbox"))
                            Else
                                ModelState.AddModelError("emailAddress", ContextHelper.Tradueix("no ha sido posible enviarle su contraseña, por favor contacte con nuestras oficinas",
                                                                                             "no ha estat possible enviar-li la clau de pas, si us plau contacti amb les nostres oficines",
                                                                                             "we have not been able to mail your password, please contact our offices"))
                            End If
                        End If
                    Else
                        ModelState.AddModelError("emailAddress", ContextHelper.Tradueix("no ha sido posible enviarle su contraseña, por favor contacte con nuestras oficinas",
                                                                                             "no ha estat possible enviar-li la clau de pas, si us plau contacti amb les nostres oficines",
                                                                                             "we have not been able to mail your password, please contact our offices"))
                    End If
            End Select
        End If


        If ModelState.IsValid Then
            If oModel.ReturnUrl > "" Then
                retval = Redirect(oModel.ReturnUrl)
            ElseIf oUser IsNot Nothing Then
                If oUser.Rol.IsProfesional Then
                    retval = Redirect("/pro")
                Else
                    retval = Redirect("/")
                End If
            Else
                retval = Redirect("/")
            End If
        Else
            retval = View(oModel)
        End If
        Return retval
    End Function

    Async Function FbLoginConfirmation() As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim model As New LoginViewModel
        Dim retval As ActionResult = Nothing
        Try
            Dim accessToken As String = Request("accessToken")
            Dim persist As Boolean = Request("persist")
            Dim returnUrl As String = Request("returnUrl")

            If String.IsNullOrEmpty(accessToken) Then
                exs.Add(New Exception("missing access token at AccountController.FbLoginConfirmation"))
                retval = Await MyBase.ErrorResult(exs)
            Else
                Dim oUserProfile = UserProfile(accessToken)
                Dim oUser = Await FEB2.User.FromEmail(exs, GlobalVariables.Emp, oUserProfile.email)
                If oUser Is Nothing Then
                    If exs.Count = 0 Then
                        'email no registrat; torna a la pagina de login
                        model.EmailAddress = oUserProfile.email
                        model.Errs.Add(String.Format("No consta ningun usuario en M+O registrado con este correo", oUserProfile.email))
                        retval = View("Test", model)
                    Else
                        retval = Await MyBase.ErrorResult(exs)
                    End If
                Else
                    Await MyBase.SignInUser(oUser, persist)
                    If String.IsNullOrEmpty(returnUrl) Then returnUrl = "/pro"
                    retval = Redirect(returnUrl)
                End If
            End If
        Catch ex As Exception
            exs.Add(ex)
        End Try

        If exs.Count > 0 Then
            retval = Await MyBase.ErrorResult(exs)
        End If

        Return retval
    End Function

    Shared Function UserProfile(accessToken As String) As DTO.DTOFacebook.UserProfile
        Dim fbClient As New Facebook.FacebookClient(accessToken)
        Dim oJObject = fbClient.Get("me", New With {.fields = "email, name, gender, first_name, last_name, locale"})
        Dim json = oJObject.ToString
        Dim retval = Newtonsoft.Json.JsonConvert.DeserializeObject(Of DTO.DTOFacebook.UserProfile)(json)
        Return retval
    End Function

    Function SignUp() As ActionResult
        Dim exs As New List(Of Exception)
        Dim oModel As LeadViewModel = Nothing
        Dim oUser = ContextHelper.GetUser
        If FEB2.User.Load(exs, oUser) Then
            oModel = New LeadViewModel
            With oModel
                .EmailAddress = oUser.EmailAddress.Trim.ToLower.Replace(" ", "")
                .Nom = oUser.Nom
                .Cognoms = oUser.Cognoms
                .sex = oUser.sex
                .BirthYear = oUser.birthYear
                If oUser.Country Is Nothing Then
                    .CountryCod = "ES"
                Else
                    .CountryCod = oUser.Country.ISO
                End If
                .tel = oUser.tel
            End With
            Return View(oModel)
        Else
            Return View("error")
        End If

    End Function

    Async Function Activate(id As String) As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim retval As ActionResult = View("/home/index")
        If GuidHelper.IsGuid(id) Then
            Dim oUser = Await FEB2.User.Find(New Guid(id), exs)
            If oUser IsNot Nothing Then
                If oUser.Rol.id = DTORol.Ids.Guest Then oUser.Rol = New DTORol(DTORol.Ids.Lead)
                If oUser.fchActivated = Nothing Then oUser.fchActivated = Now
                If Await FEB2.User.Update(exs, oUser) Then
                    Await MyBase.SignInUser(oUser, persist:=True)
                    retval = View("Activated", oUser)
                End If
            End If
        End If
        Return retval
    End Function

    <HttpPost>
    Async Function SignUp(oModel As LeadViewModel) As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim retval As ActionResult = Nothing
        Dim sEmailAddress As String = oModel.EmailAddress.Trim.ToLower.Replace(" ", "")
        oModel.EmailAddress = sEmailAddress
        If Not DTOUser.CheckEmailSintaxis(sEmailAddress) Then
            ModelState.AddModelError("emailAddress", ContextHelper.Tradueix("la direccion email está mal escrita", "la adreça email no es correcte", "wrong email address"))
        End If
        If oModel.Nom = "" Then
            ModelState.AddModelError("Nom", ContextHelper.Tradueix("entre su nombre en la casilla correspondiente", "entri el seu nom a la casella corresponent", "enter your name"))
        Else
            If oModel.Nom.Length < 2 Then
                ModelState.AddModelError("Nom", ContextHelper.Tradueix("el nombre no es correcto", "el nom no es correcte", "wrong first name"))
            End If
        End If
        If oModel.BirthYear < 1900 Or oModel.BirthYear > Today.Year Then
            ModelState.AddModelError("BirthYear", ContextHelper.Tradueix("el año de nacimiento es incorrecto", "l'any de neixament no es correcte", "wrong birth year"))
        End If
        If ModelState.IsValid Then

            Dim oUser = Await FEB2.User.FromEmail(exs, GlobalVariables.Emp, oModel.EmailAddress.Trim.ToLower)
            If oUser Is Nothing Then
                oUser = DTOUser.Factory(GlobalVariables.Emp, , oModel.EmailAddress.Trim.ToLower)

                With oUser
                    .nom = oModel.Nom
                    .cognoms = oModel.Cognoms
                    .sex = oModel.sex
                    .birthYear = oModel.BirthYear
                    .Country = FEB2.Country.FromIsoSync(oModel.CountryCod, exs)
                    .tel = oModel.tel
                    If .rol.id = DTORol.Ids.NotSet Then
                        .rol = New DTORol(DTORol.Ids.Lead)
                        .source = DTOUser.Sources.Website
                        .fchCreated = Now
                    End If
                    If .password = "" Then
                        .password = TextHelper.RandomString(5)
                    End If
                End With

                If Await FEB2.User.Update(exs, oUser) Then
                    ContextHelper.SetUserCookie(oUser, True)
                    Await ContextHelper.SetNavViewModel(oUser)
                    retval = View("SignUp_Thanks")
                Else
                    retval = View("Error")
                End If

            Else
                oModel.EmailAddress = oUser.EmailAddress
                retval = View(oModel)
            End If
        End If
        Return retval
    End Function


    Public Shadows Async Function LogOff() As Threading.Tasks.Task(Of ActionResult)
        Await MyBase.LogOff()
        Dim retval As ActionResult = RedirectToAction("index", "home")
        Return retval
    End Function

    Public Async Function Validate(email As String, password As String, persist As Boolean) As Threading.Tasks.Task(Of String)
        Dim exs As New List(Of Exception)
        email = email.Trim.ToLower.Replace(" ", "")
        Dim retval As String = ""
        Dim oValidatePasswordModel = Await FEB2.User.ValidatePassword(exs, GlobalVariables.Emp, email, password)
        Dim oUser = oValidatePasswordModel.User
        Select Case oValidatePasswordModel.Result
            Case DTOUser.ValidationResults.success
                Await MyBase.SignInUser(oUser, persist:=True)
                If oUser.Rol.id = DTORol.Ids.manufacturer Then
                    Dim oBrands = Await FEB2.ProductBrands.All(exs, oUser)
                    If exs.Count = 0 Then
                        If oBrands.Count > 0 Then
                            ContextHelper.SetCookieValue(DTOSession.CookieIds.LastProductBrowsed, oBrands.First)
                        End If
                    Else
                        oValidatePasswordModel.Result = DTOUser.ValidationResults.systemError
                    End If
                End If

                retval = New MatJSonObject("value", oValidatePasswordModel.Result, "text", "").ToString

            Case DTOUser.ValidationResults.emailNotRegistered
                ContextHelper.UnPersist(System.Web.HttpContext.Current)
                retval = New MatJSonObject("value", oValidatePasswordModel.Result, "text", ContextHelper.Tradueix("email no registrado", "email no registrat", "unknown email address")).ToString

            Case DTOUser.ValidationResults.emptyEmail
                ContextHelper.UnPersist(System.Web.HttpContext.Current)
                retval = New MatJSonObject("value", oValidatePasswordModel.Result, "text", ContextHelper.Tradueix("email vacío", "email buit", "empty email address")).ToString

            Case DTOUser.ValidationResults.wrongEmail
                ContextHelper.UnPersist(System.Web.HttpContext.Current)
                retval = New MatJSonObject("value", oValidatePasswordModel.Result, "text", ContextHelper.Tradueix("formato de email no válido", "format invalid de email", "wrong email format")).ToString

            Case DTOUser.ValidationResults.emptyPassword
                ContextHelper.UnPersist(System.Web.HttpContext.Current)
                If Await FEB2.User.EmailPassword(oUser, exs) Then
                    retval = New MatJSonObject("value", oValidatePasswordModel.Result, "text", ContextHelper.Tradueix("por favor consulte su correo, le hemos enviado su contraseña", "si us plau consulti la seva bustia, li hem enviat la clau de pas", "please look up your password in your mailbox")).ToString
                Else
                    retval = New MatJSonObject("value", oValidatePasswordModel.Result, "text", ContextHelper.Tradueix("no ha sido posible enviarle su contraseña, por favor contacte con nuestras oficinas",
                                                                                             "no ha estat possible enviar-li la clau de pas, si us plau contacti amb les nostres oficines",
                                                                                             "we have not been able to mail your password, please contact our offices")).ToString
                End If

            Case DTOUser.ValidationResults.wrongPassword
                ContextHelper.UnPersist(System.Web.HttpContext.Current)
                retval = New MatJSonObject("value", oValidatePasswordModel.Result, "text", ContextHelper.Tradueix("contraseña no válida", "clau de pas no válida", "wrong password")).ToString
        End Select


        Return retval
    End Function


    Public Function Test() As ActionResult
        Return View()
    End Function

    <HttpGet>
    Public Function PasswordEdit() As ActionResult
        Dim oUser = Mvc.ContextHelper.GetUser()
        Return View("PasswordEdit", oUser)
    End Function



    <HttpPost>
    Public Async Function PasswordEdit(oldPassword As String, newPassword As String, newPasswordAgain As String) As Threading.Tasks.Task(Of String)
        Dim success As Integer = 2
        Dim texts As New List(Of String)
        If oldPassword = "" Then texts.Add(ContextHelper.Tradueix("entre la contraseña vigente en la primera casilla", "entri la clau de pas vigent a la primera casella", "please enter your current password on first box"))
        If newPassword = "" Then texts.Add(ContextHelper.Tradueix("entre su nueva contraseña en la segunda casilla", "entri la nova clau de pas a la segona casella", "please enter your new password on second box"))
        If newPassword <> newPasswordAgain Then texts.Add(ContextHelper.Tradueix("los valores de las dos últimas casillas no coinciden", "ha entrat valors diferents en les dues últimes caselles", "last two box values do not match"))
        If texts.Count = 0 Then
            Dim exs As New List(Of Exception)
            If Await FEB2.User.PasswordEdit(ContextHelper.GetUser, oldPassword, newPassword, exs) Then
                success = 1
                texts.Add(ContextHelper.Tradueix("contraseña actualizada correctamente", "clau de pas actualitzada correctament", "password successfully updated"))
            Else
                If exs.Count > 0 Then
                    texts.Add("error SYS089 al grabar la contraseña")
                Else
                    texts.Add(ContextHelper.Tradueix("credenciales invalidas", "credencials invalides", "wrong credentials"))
                End If
            End If
        End If

        Dim sText As String = String.Join("<br/>", texts)
        Dim retval As String = New MatJSonObject("value", success, "text", sText).ToString
        Return retval
    End Function

    <HttpPost>
    Public Async Function MailPassword() As Threading.Tasks.Task(Of String)
        Dim retval As String = ""
        Dim exs As New List(Of Exception)
        If Await FEB2.User.EmailPassword(ContextHelper.GetUser, exs) Then
            retval = New MatJSonObject("value", 0, "text", ContextHelper.Tradueix("por favor consulte su correo, le hemos enviado su contraseña", "si us plau consulti la seva bustia, li hem enviat la clau de pas", "please look up your password in your mailbox")).ToString
        Else
            retval = New MatJSonObject("value", 1, "text", ContextHelper.Tradueix("no ha sido posible enviarle su contraseña, por favor contacte con nuestras oficinas",
                                                                                     "no ha estat possible enviar-li la clau de pas, si us plau contacti amb les nostres oficines",
                                                                                     "we have not been able to mail your password, please contact our offices")).ToString
        End If
        Return retval
    End Function


End Class