Public Class User

    Shared Function FromEmail(oEmp As DTOEmp, ByRef sEmail As String) As DTOUser
        Dim retval = UserLoader.FromEmail(oEmp, sEmail)
        Return retval
    End Function

    Shared Function Find(oGuid As Guid) As DTOUser
        Dim retval = UserLoader.Find(oGuid)
        Return retval
    End Function

    Shared Function Find(owellknown As DTOUser.Wellknowns) As DTOUser
        Dim retval = DTOUser.Wellknown(owellknown)
        UserLoader.Load(retval)
        Return retval
    End Function

    Shared Function Exists(oEmp As DTOEmp, ByRef sEmail As String) As Boolean
        Return UserLoader.Exists(oEmp, sEmail)
    End Function


    Shared Function Load(ByRef oUser As DTOUser) As Boolean
        Dim retval = UserLoader.Load(oUser)
        Return retval
    End Function

    Shared Function Update(oUser As DTOUser, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = UserLoader.Update(oUser, exs)
        Return retval
    End Function

    Shared Function UpdateContacts(oUser As DTOUser, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = UserLoader.UpdateContacts(oUser, exs)
        Return retval
    End Function

    Shared Function Delete(exs As List(Of Exception), oUser As DTOUser) As Boolean
        Return UserLoader.Delete(oUser, exs)
    End Function

    Shared Function CreateNewLead(oEmp As DTOEmp, sEmailAddress As String, sPassword As String, oLang As DTOLang, oSource As DTOUser.Sources, exs As List(Of Exception)) As DTOUser
        Dim retval = DTOUser.Factory(oEmp)
        With retval
            .emailAddress = sEmailAddress
            .password = sPassword
            .lang = oLang
            .source = oSource
            .rol = New DTORol(DTORol.Ids.Lead)
            .FchCreated = DTO.GlobalVariables.Now()
            .fchActivated = .fchCreated
        End With

        BEBL.User.Update(retval, exs)
        Return retval
    End Function


    Shared Function NicknameOrElse(oUser As DTOUser) As String
        Dim retval As String = ""
        If oUser IsNot Nothing Then
            If oUser.NickName > "" Then
                retval = oUser.NickName
            ElseIf oUser.Nom > "" Or oUser.Cognoms > "" Then
                retval = oUser.Nom & " " & oUser.Cognoms
            Else
                retval = oUser.EmailAddress
            End If
        End If
        Return retval
    End Function

    Shared Function GetCustomers(oUser As DTOUser) As List(Of DTOCustomer)
        Dim retval As List(Of DTOCustomer) = UserLoader.GetCustomers(oUser)
        Return retval
    End Function

    Shared Function GetRep(oUser As DTOUser) As DTORep
        Dim retval As DTORep = UserLoader.GetRep(oUser)
        Return retval
    End Function

    Shared Function GetStaff(oUser As DTOUser) As DTOStaff
        Dim retval As DTOStaff = UserLoader.GetStaff(oUser)
        Return retval
    End Function

    Shared Function GetProveidors(oUser As DTOUser) As List(Of DTOProveidor)
        Dim retval As List(Of DTOProveidor) = UserLoader.GetProveidors(oUser)
        Return retval
    End Function

    Shared Function GetProveidor(oUser As DTOUser) As DTOProveidor
        Dim retval As DTOProveidor = Nothing
        Dim oProveidors As List(Of DTOProveidor) = UserLoader.GetProveidors(oUser)
        If oProveidors.Count > 0 Then
            retval = oProveidors.First
        End If
        Return retval
    End Function

    Shared Function BrandManufacturers(oUser As DTOUser) As List(Of DTOProveidor)
        Return UserLoader.BrandManufacturers(oUser)
    End Function

    Shared Function Contacts(oUser As DTOUser) As List(Of DTOContact)
        Return UserLoader.Contacts(oUser)
    End Function

    Shared Function Customers(oUser As DTOUser) As List(Of DTOCustomer)
        Return UserLoader.Customers(oUser)
    End Function

    Shared Function CustomersRaonsSocials(oUser As DTOUser) As List(Of DTOCustomer)
        Dim retval As New List(Of DTOCustomer)
        Dim oCustomers As List(Of DTOCustomer) = BEBL.User.Customers(oUser)
        For Each item As DTOCustomer In oCustomers
            If item.ccx Is Nothing Then
                retval.Add(item)
            End If
        Next
        Return retval
    End Function

    Shared Function CustomersForBasket(oUser As DTOUser) As List(Of DTOCustomer)
        Dim retval As New List(Of DTOCustomer)
        Dim oCustomers As List(Of DTOCustomer) = UserLoader.GetCustomers(oUser)
        For Each item As DTOCustomer In oCustomers
            If (Not item.obsoleto) And (Not item.ordersToCentral) Then
                retval.Add(item)
            End If
        Next
        Return retval
    End Function

    Shared Function PasswordMailMessage(oUser As DTOUser) As DTOMailMessage

        Dim retval = DTOMailMessage.Factory(oUser.EmailAddress)
        With retval
            .subject = oUser.lang.Tradueix("Credenciales de acceso", "Credencials d'acces", "Login credentials")
            .bodyUrl = BEBL.Mailing.BodyUrl(DTODefault.MailingTemplates.Password, oUser.Guid.ToString()) 'posem el trailing slash perque sino el sistema creu que es la extensio de un fitxer despres del punt i l'intenta obrir.
        End With
        Return retval
    End Function

    Shared Function PasswordMailMessage(oEmp As DTOEmp, sEmailAddress As String) As DTOMailMessage
        Dim oUser = BEBL.User.FromEmail(oEmp, sEmailAddress)

        Dim retval = DTOMailMessage.Factory(sEmailAddress)
        With retval
            If oUser Is Nothing Then
                .subject = DTOApp.current.lang.Tradueix("Credenciales de acceso", "Credencials d'acces", "Login credentials")
                .bodyUrl = BEBL.Mailing.BodyUrl(DTODefault.MailingTemplates.EmailAddressVerification, sEmailAddress & "/") 'posem el trailing slash perque sino el sistema creu que es la extensio de un fitxer despres del punt i l'intenta obrir.
            Else
                .subject = oUser.lang.Tradueix("Credenciales de acceso", "Credencials d'acces", "Login credentials")
                .bodyUrl = BEBL.Mailing.BodyUrl(DTODefault.MailingTemplates.Password, oUser.Guid.ToString())
            End If
        End With
        Return retval
    End Function

    Shared Function ActivationRequestMailMessage(oEmp As DTOEmp, oUser As DTOUser) As DTOMailMessage
        Dim retval = DTOMailMessage.Factory(oUser.EmailAddress)
        With retval
            .bcc = {"matias@matiasmasso.es"}.ToList
            .subject = DTOApp.current.lang.Tradueix("Verificación de dirección de correo", "Verificació de adreça de correu", "Email address validation")
            .bodyUrl = BEBL.Mailing.BodyUrl(DTODefault.MailingTemplates.ActivationRequest, oUser.Guid.ToString())
        End With
        Return retval
    End Function

    Shared Function EmailAddressVerificationMailMessage(oEmp As DTOEmp, sEmail As String) As DTOMailMessage
        Dim sRecipient As String = IIf(sEmail.Contains("@test."), "matias@matiasmasso.es", sEmail)

        Dim retval = DTOMailMessage.Factory(sRecipient)
        With retval
            .subject = DTOApp.current.lang.Tradueix("Credenciales de acceso", "Credencials d'acces", "Login credentials")
            .bodyUrl = BEBL.Mailing.BodyUrl(DTODefault.MailingTemplates.EmailAddressVerification, sEmail & "/") 'posem el trailing slash perque sino el sistema creu que es la extensio de un fitxer despres del punt i l'intenta obrir.
        End With
        Return retval
    End Function

    Shared Async Function EmailPwd(oUser As DTOUser, exs As List(Of Exception)) As Task(Of Boolean)
        BEBL.User.Load(oUser)
        Dim oLang As DTOLang = oUser.lang
        Dim sSaludo As String = oLang.Tradueix("Sres.,", "Srs.,", "Dear Sirs,")
        Dim sFirma As String = "MATIAS MASSO, S.A."
        Dim oTxt = BEBL.Txt.Find(DTOTxt.Ids.MailPwd)

        Dim oMailMessage = DTOMailMessage.Factory(oUser.EmailAddress)
        With oMailMessage
            .subject = oLang.Tradueix("Credenciales de acceso online", "Credencials d'acces online", "Online access credentials")
            .body = oTxt.ToHtml(oLang, sSaludo, oUser.EmailAddress, oUser.password, sFirma)
        End With

        Dim retval As Boolean = Await MailMessageHelper.Send(oUser.Emp, oMailMessage, exs)
        Return retval
    End Function

    Shared Function Validate(credencials As DTOUser) As DTOUser
        Dim oEmp = credencials.Emp
        If oEmp Is Nothing Then oEmp = New DTOEmp(DTOEmp.Ids.MatiasMasso)

        Dim retval = DTOUser.Factory(oEmp)
        Dim sEmail As String = credencials.EmailAddress
        Dim sPassword As String = credencials.Password
        Dim oLang As DTOLang = DTOLang.ESP
        If credencials.Lang IsNot Nothing Then
            oLang = credencials.Lang
        End If

        Dim oSource As DTOUser.Sources = DTOUser.Sources.iMat
        If credencials.Source <> DTOUser.Sources.notSet Then
            oSource = credencials.Source
        End If

        Dim oValidationResult = BEBL.User.ValidatePassword(oEmp, sEmail, sPassword, retval)
        retval.ValidationResult = oValidationResult
        If retval.ValidationResult = DTOUser.ValidationResults.newValidatedUser Then
            Dim exs As New List(Of Exception)
            retval = BEBL.User.CreateNewLead(oEmp, sEmail, sPassword, oLang, oSource, exs)
            If exs.Count = 0 Then
                With retval
                    .Guid = retval.Guid
                    .EmailAddress = sEmail
                    .Rol = retval.Rol
                    .Lang = retval.Lang
                End With
            Else
                retval.ValidationResult = DTOUser.ValidationResults.systemError
            End If
        ElseIf retval.ValidationResult = DTOUser.ValidationResults.success Then
            With retval
                .Guid = retval.Guid
                .EmailAddress = sEmail
                .NickName = retval.NickName
                .Rol = retval.Rol
                .Lang = retval.Lang
                If retval.Contact IsNot Nothing Then
                    .Contact = retval.Contact
                End If
            End With

        End If
        Return retval
    End Function

    Shared Function ValidatePassword(oEmp As DTOEmp, sEmail As String, sPassword As String, ByRef oUser As DTOUser) As DTOUser.ValidationResults
        Dim retval As DTOUser.ValidationResults = DTOUser.ValidationResults.notSet
        If sEmail = "" Then
            retval = DTOUser.ValidationResults.emptyEmail
        ElseIf Not DTOUser.CheckEmailSintaxis(sEmail) Then
            retval = DTOUser.ValidationResults.wrongEmail
        ElseIf sPassword = "" Then
            retval = DTOUser.ValidationResults.emptyPassword
        Else
            Dim pUser = UserLoader.FromEmail(oEmp, sEmail)
            If pUser Is Nothing Then
                If DTOUser.VerificationCode(sEmail) = sPassword Then
                    retval = DTOUser.ValidationResults.newValidatedUser
                Else
                    retval = DTOUser.ValidationResults.emailNotRegistered
                End If
            Else
                oUser = pUser
                If oUser.Password = sPassword Then
                    If oUser.FchDeleted = Nothing Then
                        retval = DTOUser.ValidationResults.success
                    Else
                        retval = DTOUser.ValidationResults.userDeleted
                    End If
                Else
                    retval = DTOUser.ValidationResults.wrongPassword
                End If
            End If
        End If

        Return retval
    End Function


    Shared Function BadMailReasons() As List(Of DTOCod)
        Dim oParent = DTOCod.Wellknown(DTOCod.Wellknowns.BadMail)
        Return BEBL.Cods.All(oParent)
    End Function

End Class

Public Class Users

    Shared Function All(oEmp As DTOEmp, Optional IncludeObsolets As Boolean = False) As List(Of DTOUser)
        Return UsersLoader.All(oEmp, IncludeObsolets)
    End Function

    Shared Function All(oContact As DTOContact, Optional IncludeObsolets As Boolean = False) As List(Of DTOUser)
        Return UsersLoader.All(oContact, IncludeObsolets)
    End Function

    Shared Function Search(searchKey As DTOUser) As List(Of DTOUser)
        Return UsersLoader.Search(searchKey)
    End Function

    Shared Function Search(oEmp As DTOEmp, searchKey As String) As List(Of DTOUser)
        Dim retval As New List(Of DTOUser)
        Dim usersCount = UsersLoader.SearchCount(oEmp, searchKey)
        If usersCount < 50 Then
            retval = UsersLoader.Search(oEmp, searchKey)
        End If
        Return retval
    End Function

    Shared Function Professionals(oEmp As DTOEmp) As List(Of DTOContact)
        Dim retval = UsersLoader.Professionals(oEmp)
        Return retval
    End Function

    Shared Function ProfessionalsExcel(oEmp As DTOEmp, oContacts As List(Of DTOAtlas.Contact)) As MatHelper.Excel.Sheet
        Dim oAllPros = UsersLoader.Professionals(oEmp)
        Dim oFilteredPros = oAllPros.Where(Function(x) oContacts.Any(Function(y) y.guid.Equals(x.Guid)))
        Dim retval As New MatHelper.Excel.Sheet
        For Each oContact In oFilteredPros
            For Each oEmail In oContact.Emails
                Dim oRow = retval.AddRow()
                oRow.AddCell(oEmail.Guid.ToString())
                oRow.AddCell(oEmail.EmailAddress)
                oRow.AddCell(oContact.FullNom)
            Next
        Next
        Return retval
    End Function
End Class
