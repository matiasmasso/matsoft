Public Class User
    Shared Async Function Find(oGuid As Guid, exs As List(Of Exception)) As Task(Of DTOUser)
        Return Await Api.Fetch(Of DTOUser)(exs, "User", oGuid.ToString())
    End Function

    Shared Async Function Exists(exs As List(Of Exception), oEmp As DTOEmp, emailAddress As String) As Task(Of Boolean)
        Return Await Api.Execute(Of String, Boolean)(emailAddress, exs, "User/Exists", oEmp.Id)
    End Function

    Shared Function Load(exs As List(Of Exception), ByRef oUser As DTOUser) As Boolean
        If Not oUser Is Nothing Then
            If Not oUser.IsLoaded And Not oUser.IsNew Then
                Dim pUser = Api.FetchSync(Of DTOUser)(exs, "User", oUser.Guid.ToString())
                If exs.Count = 0 Then
                    DTOBaseGuid.CopyPropertyValues(Of DTOUser)(pUser, oUser, exs)
                End If
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Update(exs As List(Of Exception), oUser As DTOUser) As Task(Of Boolean)
        Return Await Api.Update(Of DTOUser)(oUser, exs, "User")
    End Function

    Shared Async Function UpdateContacts(exs As List(Of Exception), oUser As DTOUser) As Task(Of Boolean)
        Return Await Api.Update(Of DTOUser)(oUser, exs, "User/updateContacts")
    End Function

    Shared Async Function UpdateUser(oSrc As DTOUser.Sources,
                             sEmail As String,
                             sFirstname As String,
                             sSurname As String,
                             sNickName As String,
                             oCountry As Guid,
                             sZipcod As String,
                             sTel As String,
                             iBirthYear As Integer) As Task(Of DTOUser.ValidationResults)

        Dim exs As New List(Of Exception)
        Dim retval As DTOUser.ValidationResults
        Dim oUser As DTOUser = Await User.FromEmail(exs, New DTOEmp(DTOEmp.Ids.MatiasMasso), sEmail) ' TO DEPRECATE
        With oUser
            .Source = oSrc
            .Nom = sFirstname
            .Cognoms = sSurname
            .NickName = sNickName
            .Country = Await Country.Find(oCountry, exs)
            .ZipCod = sZipcod
            .Tel = sTel
            .BirthYear = iBirthYear
        End With

        If Await User.Update(exs, oUser) Then
            retval = DTOUser.ValidationResults.Success
        Else
            retval = DTOUser.ValidationResults.SystemError
        End If
        Return retval
    End Function

    Shared Async Function Delete(exs As List(Of Exception), oUser As DTOUser) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOUser)(oUser, exs, "User")
    End Function

    Shared Async Function BrandManufacturers(exs As List(Of Exception), oUser As DTOUser) As Task(Of List(Of DTOProveidor))
        Return Await Api.Fetch(Of List(Of DTOProveidor))(exs, "User/BrandManufacturers", oUser.Guid.ToString())
    End Function

    Shared Async Function Contacts(exs As List(Of Exception), oUser As DTOUser) As Task(Of List(Of DTOContact))
        Return Await Api.Fetch(Of List(Of DTOContact))(exs, "User/Contacts", oUser.Guid.ToString())
    End Function

    Shared Function CustomersRaonsSocialsSync(exs As List(Of Exception), oUser As DTOUser) As List(Of DTOCustomer)
        Return Api.FetchSync(Of List(Of DTOCustomer))(exs, "User/CustomersRaonsSocials", oUser.Guid.ToString())
    End Function

    Shared Async Function CustomersForBasket(exs As List(Of Exception), oUser As DTOUser) As Task(Of List(Of DTOCustomer))
        Return Await Api.Fetch(Of List(Of DTOCustomer))(exs, "User/CustomersForBasket", oUser.Guid.ToString())
    End Function

    Shared Function CustomersForBasketSync(exs As List(Of Exception), oUser As DTOUser) As List(Of DTOCustomer)
        Return Api.FetchSync(Of List(Of DTOCustomer))(exs, "User/CustomersForBasket", oUser.Guid.ToString())
    End Function


    Shared Async Function FromEmail(exs As List(Of Exception), oEmp As DTOEmp, emailAddress As String) As Task(Of DTOUser)
        Dim retval = Await Api.Execute(Of String, DTOUser)(emailAddress, exs, "User", oEmp.Id)
        If retval IsNot Nothing Then retval.emp = oEmp
        Return retval
    End Function

    Shared Function FromEmailSync(exs As List(Of Exception), oEmp As DTOEmp, emailAddress As String) As DTOUser
        Return Api.ExecuteSync(Of String, DTOUser)(emailAddress, exs, "User", oEmp.Id)
    End Function

    Shared Function FindSync(oGuid As Guid, exs As List(Of Exception)) As DTOUser
        Return Api.FetchSync(Of DTOUser)(exs, "User", oGuid.ToString())
    End Function

    Shared Async Function Validate(oEmp As DTOEmp, email As String, pwd As String, exs As List(Of Exception)) As Task(Of DTOUser)
        'Return Await Api.Fetch(Of DTOUser)(exs, "user/validate", oEmp.Id, email, pwd)
        Dim oCredencials As New DTOUser()
        With oCredencials
            .EmailAddress = email
            .Password = pwd
            .Emp = oEmp.trimmed
        End With
        Dim retval = Await Api.Execute(Of DTOUser, DTOUser)(oCredencials, exs, "user/validate")
        Return retval
    End Function

    Shared Async Function Validate(exs As List(Of Exception), oEmp As DTOEmp, email As String, pwd As String) As Task(Of DTOUser)
        Dim oCredencials As New DTOUser
        With oCredencials
            .Emp = oEmp
            .EmailAddress = email
            .Password = pwd
        End With
        Return Await Api.Execute(Of DTOUser, DTOUser)(oCredencials, exs, "user/validate")
    End Function

    Shared Async Function PasswordEdit(oUser As DTOUser, oldPassword As String, newPassword As String, exs As List(Of Exception)) As Task(Of Boolean)
        Dim retval As Boolean
        If User.Load(exs, oUser) Then
            retval = oUser.Password = oldPassword
            If retval Then
                oUser.Password = newPassword
                If Await User.Update(exs, oUser) Then
                    retval = True
                End If
            End If
        End If

        Return retval
    End Function

    Shared Async Function ValidatePassword(exs As List(Of Exception), oEmp As DTOEmp, sEmail As String, sPassword As String) As Task(Of DTOUser.ValidatePasswordModel)
        Dim retval As New DTOUser.ValidatePasswordModel
        If sEmail = "" Then
            retval.result = DTOUser.ValidationResults.emptyEmail
        ElseIf Not DTOUser.CheckEmailSintaxis(sEmail) Then
            retval.result = DTOUser.ValidationResults.wrongEmail
        ElseIf sPassword = "" Then
            retval.result = DTOUser.ValidationResults.emptyPassword
        Else
            retval.user = Await User.FromEmail(exs, oEmp, sEmail)
            If retval.user Is Nothing Then
                If DTOUser.VerificationCode(sEmail) = sPassword Then
                    retval.result = DTOUser.ValidationResults.newValidatedUser
                Else
                    retval.result = DTOUser.ValidationResults.emailNotRegistered
                End If
            Else
                If retval.user.password = sPassword Then
                    If retval.user.fchDeleted = Nothing Then
                        retval.result = DTOUser.ValidationResults.success
                    Else
                        retval.result = DTOUser.ValidationResults.userDeleted
                    End If
                Else
                    retval.result = DTOUser.ValidationResults.wrongPassword
                End If
            End If
        End If

        Return retval
    End Function

    Shared Async Function EmailPwd(oEmp As DTOEmp, emailAddress As String, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Execute(Of String, Boolean)(emailAddress, exs, "user/emailPwd", oEmp.Id)
    End Function

    Shared Async Function EmailPassword(oUser As DTOUser, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Fetch(Of Boolean)(exs, "user/emailPassword", oUser.Guid.ToString())
    End Function

    Shared Async Function EmailActivationRequest(exs As List(Of Exception), oEmp As DTOEmp, oUser As DTOUser) As Task(Of Boolean)
        Return Await Api.Execute(Of DTOUser, Boolean)(oUser, exs, "user/c", oEmp.Id)
    End Function

    Shared Async Function EmailAddressVerification(exs As List(Of Exception), oEmp As DTOEmp, emailAddress As String) As Task(Of Boolean)
        Return Await Api.Execute(Of String, Boolean)(emailAddress, exs, "user/EmailAddressVerification", oEmp.Id)
    End Function

    Shared Async Function GetProveidor(oUser As DTOUser, exs As List(Of Exception)) As Task(Of DTOProveidor)
        Return Await Api.Fetch(Of DTOProveidor)(exs, "User/GetProveidor", oUser.Guid.ToString())
    End Function

    Shared Function GetProveidorSync(oUser As DTOUser, exs As List(Of Exception)) As DTOProveidor
        Return Api.FetchSync(Of DTOProveidor)(exs, "User/GetProveidor", oUser.Guid.ToString())
    End Function

    Shared Async Function GetCustomers(oUser As DTOUser, exs As List(Of Exception)) As Task(Of List(Of DTOCustomer))
        Return Await Api.Fetch(Of List(Of DTOCustomer))(exs, "User/GetCustomers", oUser.Guid.ToString())
    End Function
    Shared Function GetCustomersSync(oUser As DTOUser, exs As List(Of Exception)) As List(Of DTOCustomer)
        Return Api.FetchSync(Of List(Of DTOCustomer))(exs, "User/GetCustomers", oUser.Guid.ToString())
    End Function

    Shared Async Function GetRep(oUser As DTOUser, exs As List(Of Exception)) As Task(Of DTORep)
        Return Await Api.Fetch(Of DTORep)(exs, "User/GetRep", oUser.Guid.ToString())
    End Function

    Shared Function GetRepSync(oUser As DTOUser, exs As List(Of Exception)) As DTORep
        Return Api.FetchSync(Of DTORep)(exs, "User/GetRep", oUser.Guid.ToString())
    End Function

    Shared Async Function GetStaff(exs As List(Of Exception), oUser As DTOUser) As Task(Of DTOStaff)
        Return Await Api.Fetch(Of DTOStaff)(exs, "User/GetStaff", oUser.Guid.ToString())
    End Function

    Shared Function ActivationUrl(oUser As DTOUser) As String
        Dim retval As String = UrlHelper.Factory(True, "account", "activate", oUser.Guid.ToString())
        Return retval
    End Function

    Shared Function IsAllowedToRead(oLector As DTOUser, oPropietari As DTOUser) As Boolean
        Dim retval As Boolean
        Select Case oLector.Rol.Id
            Case DTORol.Ids.SuperUser, DTORol.Ids.Admin
                Return True
            Case Else
                If DTOUser.IsStaff(oLector) Then
                    If DTOUser.IsStaff(oPropietari) Then
                        retval = (oLector.Equals(oPropietari))
                    Else
                        retval = True
                    End If
                Else
                    retval = (oLector.Equals(oPropietari))
                End If
        End Select
        Return retval
    End Function
    Shared Function IsAllowedToRead(oLector As DTOUser, oPropietari As DTOContact) As Boolean
        Dim exs As New List(Of Exception)
        Dim retval As Boolean
        Select Case oLector.Rol.Id
            Case DTORol.Ids.SuperUser, DTORol.Ids.Admin, DTORol.Ids.Accounts, DTORol.Ids.Auditor
                retval = True
            Case DTORol.Ids.SalesManager
                If oPropietari.Rol Is Nothing Then Contact.Load(oPropietari, exs)
                Select Case oPropietari.Rol.Id
                    Case DTORol.Ids.SuperUser, DTORol.Ids.Admin, DTORol.Ids.Accounts, DTORol.Ids.Auditor
                        retval = False
                    Case DTORol.Ids.Operadora, DTORol.Ids.Marketing, DTORol.Ids.SalesManager, DTORol.Ids.Taller, DTORol.Ids.LogisticManager
                        retval = oPropietari.Equals(oLector.Contact)
                    Case Else
                        retval = True
                End Select
            Case DTORol.Ids.Operadora, DTORol.Ids.Marketing, DTORol.Ids.Comercial, DTORol.Ids.SalesManager, DTORol.Ids.Taller, DTORol.Ids.LogisticManager
                If oPropietari.Rol Is Nothing Then Contact.Load(oPropietari, exs)
                Select Case oPropietari.Rol.Id
                    Case DTORol.Ids.SuperUser, DTORol.Ids.Admin, DTORol.Ids.Accounts, DTORol.Ids.Auditor
                        retval = False
                    Case DTORol.Ids.Operadora, DTORol.Ids.Marketing, DTORol.Ids.Comercial, DTORol.Ids.SalesManager, DTORol.Ids.Taller, DTORol.Ids.LogisticManager
                        retval = oPropietari.Equals(oLector.Contact)
                    Case Else
                        retval = True
                End Select
            Case Else
                retval = False
        End Select
        Return retval
    End Function

    Public Shared Async Function ValidateEmail(exs As List(Of Exception), oEmp As DTOEmp, sEmail As String) As Task(Of DTOUser.ValidationResults)
        Dim retval As DTOUser.ValidationResults = DTOUser.ValidationResults.NotSet
        If sEmail = "" Then
            retval = DTOUser.ValidationResults.EmptyEmail
        ElseIf Not DTOUser.CheckEmailSintaxis(sEmail) Then
            retval = DTOUser.ValidationResults.WrongEmail
        Else
            Dim oUser As DTOUser = Await User.FromEmail(exs, oEmp, sEmail)
            If oUser Is Nothing Then
                retval = DTOUser.ValidationResults.EmailNotRegistered
            Else
                retval = DTOUser.ValidationResults.Success
            End If
        End If

        Return retval
    End Function

    Shared Async Function BadMailReasons(exs As List(Of Exception), oLang As DTOLang) As Task(Of List(Of DTOGuidNom))
        Dim retval As New List(Of DTOGuidNom)
        Dim oParent = DTOCod.Wellknown(DTOCod.Wellknowns.BadMail)
        Dim oCods = Await Cods.All(exs, oParent)
        If oCods IsNot Nothing Then
            retval = oCods.Select(Function(x) New DTOGuidNom(x.Guid, x.Nom.Tradueix(oLang))).ToList()
        End If
        Return retval
    End Function

    Shared Async Function WebLogs(exs As List(Of Exception), oUser As DTOUser) As Task(Of List(Of DTOWebLog))
        Return Await Api.Fetch(Of List(Of DTOWebLog))(exs, "User/WebLogs", oUser.Guid.ToString())
    End Function
End Class

Public Class Users

    Shared Async Function All(exs As List(Of Exception), oEmp As DTOEmp) As Task(Of List(Of DTOUser))
        Return Await Api.Fetch(Of List(Of DTOUser))(exs, "Users/fromEmp", oEmp.Id)
    End Function

    Shared Async Function All(exs As List(Of Exception), oContact As DTOContact, Optional IncludeObsolets As Boolean = False) As Task(Of List(Of DTOUser))
        Return Await Api.Fetch(Of List(Of DTOUser))(exs, "Users/fromContact", oContact.Guid.ToString())
    End Function

    Shared Async Function Search(exs As List(Of Exception), oEmp As DTOEmp, searchKey As String) As Task(Of List(Of DTOUser))
        Return Await Api.Execute(Of String, List(Of DTOUser))(searchKey, exs, "Users/Search", oEmp.Id)
    End Function

    Shared Async Function Professionals(oEmp As DTOEmp, exs As List(Of Exception)) As Task(Of List(Of DTOContact))
        Return Await Api.Fetch(Of List(Of DTOContact))(exs, "Users/professionals", oEmp.Id)
    End Function

    Shared Async Function ProfessionalsExcel(exs As List(Of Exception), oEmp As DTOEmp, oContacts As List(Of DTOAtlas.Contact)) As Task(Of MatHelper.Excel.Sheet)
        Return Await Api.Execute(Of List(Of DTOAtlas.Contact), MatHelper.Excel.Sheet)(oContacts, exs, "Users/professionals/Excel", oEmp.Id)
    End Function

End Class
