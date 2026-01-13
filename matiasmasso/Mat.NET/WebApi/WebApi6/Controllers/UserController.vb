Imports System.Net
Imports System.Web.Http

Namespace Controllers
    Public Class UserController
        Inherits ApiController

        <HttpPost>
        <Route("api/user/load")>
        Public Function Load(oUser As DTOUser) As DTOUser
            BLLUser.Load(oUser)
            Return oUser
        End Function

        <HttpPost>
        <Route("api/user/login")>
        Public Function Login(credencials As DUI.User) As DUI.User
            'iMat
            Dim retval As New DUI.User
            Dim oUser As DTOUser = Nothing
            Dim sEmail As String = credencials.Email
            Dim sPassword As String = credencials.Password
            Dim oLang As DTOLang = DTOLang.ESP
            If credencials.Lang > 0 Then
                oLang = New DTOLang(CType(credencials.Lang, DTOLang.Ids))
            End If

            retval.ValidationResult = BLLUser.ValidatePassword(sEmail, sPassword, oUser)

            If retval.ValidationResult = DTOUser.ValidationResults.NewValidatedUser Then
                Dim exs As New List(Of Exception)
                oUser = BLLUser.CreateNewLead(sEmail, sPassword, oLang, DTOUser.Sources.iMat, exs)
                If exs.Count = 0 Then
                    With retval
                        .Guid = oUser.Guid
                        .Email = sEmail
                        .Rol = oUser.Rol.Id
                        .Lang = oUser.Lang.Id
                    End With
                Else
                    retval.ValidationResult = DTOUser.ValidationResults.SystemError
                End If
            ElseIf retval.ValidationResult = DTOUser.ValidationResults.Success Then
                With retval
                    .Guid = oUser.Guid
                    .Email = sEmail
                    .Nickname = oUser.NickName
                    .Rol = oUser.Rol.Id
                    .Lang = oUser.Lang.Id
                    If oUser.Contact IsNot Nothing Then
                        .ContactGuid = oUser.Contact.Guid.ToString
                    End If
                End With

                BLLiMat.Log(oUser)
            End If

            Return retval
        End Function

        <HttpPost>
        <Route("api/user/validate")>
        Public Function ValidatePassword(credencials As DTOUser) As DTOUser
            'iMat
            Dim retval As New DTOUser
            Dim oUser As DTOUser = Nothing
            Dim sEmail As String = credencials.EmailAddress
            Dim sPassword As String = credencials.Password
            Dim oLang As DTOLang = DTOLang.ESP
            If credencials.Lang IsNot Nothing Then
                oLang = credencials.Lang
            End If

            retval.ValidationResult = BLLUser.ValidatePassword(sEmail, sPassword, oUser)

            If retval.ValidationResult = DTOUser.ValidationResults.NewValidatedUser Then
                Dim exs As New List(Of Exception)
                oUser = BLLUser.CreateNewLead(sEmail, sPassword, oLang, DTOUser.Sources.iMat, exs)
                If exs.Count = 0 Then
                    With retval
                        .Guid = oUser.Guid
                        .EmailAddress = sEmail
                        .Rol = oUser.Rol
                        .Lang = oUser.Lang
                    End With
                Else
                    retval.ValidationResult = DTOUser.ValidationResults.SystemError
                End If
            ElseIf retval.ValidationResult = DTOUser.ValidationResults.Success Then
                With retval
                    .Guid = oUser.Guid
                    .EmailAddress = sEmail
                    .NickName = oUser.NickName
                    .Rol = oUser.Rol
                    .Lang = oUser.Lang
                    If oUser.Contact IsNot Nothing Then
                        .Contact = oUser.Contact
                    End If
                End With

                BLLiMat.Log(oUser)
            End If

            Return retval
        End Function


        <HttpPost>
        <Route("api/email/validate")>
        Public Function ValidateEmail(oEmail As DTOUser) As DTOUser
            'UWP
            Dim retval As New DTOUser
            Dim email As String = oEmail.EmailAddress
            If email = "" Then
                retval.ValidationResult = DTOUser.ValidationResults.EmptyEmail
            ElseIf Not BLLEmail.CheckEmailSintaxis(email) Then
                retval.ValidationResult = DTOUser.ValidationResults.WrongEmail
            Else
                Try
                    Dim oUser As DTOUser = BLLUser.FromEmail(email)
                    If oUser Is Nothing Then
                        retval.ValidationResult = DTOUser.ValidationResults.EmailNotRegistered
                    Else
                        retval.ValidationResult = DTOUser.ValidationResults.Success
                    End If

                Catch ex As Exception
                    retval.ValidationResult = DTOUser.ValidationResults.SystemError
                End Try

            End If

            Return retval
        End Function

        <HttpPost>
        <Route("api/user/validateEmail")>
        Public Function Verify(oEmail As DTOUser) As DUI.User
            'iMat
            Dim retval As New DUI.User
            Dim email As String = oEmail.EmailAddress
            If email = "" Then
                retval.ValidationResult = DTOUser.ValidationResults.EmptyEmail
            ElseIf Not BLLEmail.CheckEmailSintaxis(email) Then
                retval.ValidationResult = DTOUser.ValidationResults.WrongEmail
            Else
                Try
                    Dim oUser As DTOUser = BLLUser.FromEmail(email)
                    If oUser Is Nothing Then
                        retval.ValidationResult = DTOUser.ValidationResults.EmailNotRegistered
                    Else
                        retval.ValidationResult = DTOUser.ValidationResults.Success
                    End If

                Catch ex As Exception
                    retval.ValidationResult = DTOUser.ValidationResults.SystemError
                End Try

            End If

            Return retval
        End Function

        <HttpPost>
        <Route("api/user/VerificationCodeRequest")>
        Public Function VerificationCodeRequest(oEmail As DUI.User) As DUI.TaskResult
            Dim retval As New DUI.TaskResult
            Dim email As String = oEmail.Email

            Dim exs As New List(Of Exception)
            If BLLMail.EmailAddressVerification(email, exs) Then
                retval.Success = True
            Else
                retval.Success = False
            End If

            Return retval
        End Function

        <HttpPost>
        <Route("api/user/update")>
        Public Function Update(dui As DUI.User) As DUI.TaskResult
            Dim retval As New DUI.TaskResult
            Dim oUser As DTOUser = BLLUser.Find(dui.Guid)
            With oUser
                .EmailAddress = dui.Email
                .Lang = New DTOLang(CType(dui.Lang, DTOLang.Ids))
                .NickName = dui.Nickname
                .Nom = dui.Firstnom
                .Cognoms = dui.Cognoms
                .Sex = dui.Sex
                .BirthYear = dui.BirthYear
                If dui.Country IsNot Nothing Then
                    .Country = BLLCountry.Find(dui.Country.Guid)
                End If
                .ZipCod = dui.Zip
                .Tel = dui.Tel
                .ChildCount = dui.ChildrenCount
                If dui.LastChildBirth <> Nothing Then
                    Dim culture As New System.Globalization.CultureInfo("es-ES")
                    Dim DtFch As Date = DateTime.Parse(dui.LastChildBirth, culture)
                    .LastChildBirthday = DtFch
                End If
            End With
            Dim exs As New List(Of Exception)
            If BLLUser.Update(oUser, exs) Then
                retval.Success = True
            Else
                retval.Success = False
                retval.Message = exs.First.Message
            End If

            Return retval
        End Function

        <HttpPost>
        <Route("api/user/lang/update")>
        Public Function UserLangUpdate(dui As DUI.User) As DUI.TaskResult
            Dim retval As New DUI.TaskResult
            Dim oUser As DTOUser = BLLUser.Find(dui.Guid)
            oUser.Lang = New DTOLang(CType(dui.Lang, DTOLang.Ids))
            Dim exs As New List(Of Exception)
            If BLLUser.Update(oUser, exs) Then
                retval.Success = True
            Else
                retval.Success = False
                retval.Message = exs.First.Message
            End If

            Return retval
        End Function

        <HttpPost>
        <Route("api/user/iforgot")>
        Public Function iForgot(credencials As DUI.User) As DUI.User
            Dim retval As New DUI.User
            If credencials.Email = "" Then
                retval.ValidationResult = DTOUser.ValidationResults.EmptyEmail
            ElseIf Not BLLEmail.CheckEmailSintaxis(credencials.Email) Then
                retval.ValidationResult = DTOUser.ValidationResults.WrongEmail
            Else
                Try
                    Dim oUser As DTOUser = BLLUser.FromEmail(credencials.Email)
                    If oUser Is Nothing Then
                        retval.ValidationResult = DTOUser.ValidationResults.EmailNotRegistered
                    Else
                        Dim exs As New List(Of Exception)
                        If BLLUser.EmailPwd(oUser, exs) Then
                            retval.ValidationResult = DTOUser.ValidationResults.Success
                        Else
                            retval.ValidationResult = DTOUser.ValidationResults.SystemError
                        End If
                    End If

                Catch ex As Exception
                    retval.ValidationResult = DTOUser.ValidationResults.SystemError
                End Try

            End If


            Return retval
        End Function


        <HttpPost>
        <Route("api/user/demo")>
        Public Function Demo(credencials As DUI.User) As DUI.User
            Dim retval As DUI.User = Nothing
            Dim oUser As DTOUser = BLLUser.FromEmail(credencials.Email)
            If oUser IsNot Nothing Then
                retval = Dui(oUser)
            End If

            Return retval
        End Function

        <HttpPost>
        <Route("api/users/search")>
        Public Function Search(searchKey As DUI.User) As List(Of DUI.User)
            Dim oUser As New DTOUser()
            With oUser
                .EmailAddress = searchKey.Email
                .Nom = searchKey.Firstnom
                .Cognoms = searchKey.Cognoms
                .NickName = searchKey.Nickname
            End With

            Dim oUsers As List(Of DTOUser) = BLLUsers.search(oUser)
            Dim retval As New List(Of DUI.User)
            For Each item As DTOUser In oUsers
                Dim oDui As DUI.User = Dui(item)
                retval.Add(oDui)
            Next

            Return retval
        End Function

        Function Dui(item As DTOUser) As DUI.User
            Dim retval As New DUI.User
            With retval
                .Guid = item.Guid
                .Email = item.EmailAddress
                .Firstnom = item.Nom
                .Cognoms = item.Cognoms
                .Nickname = item.NickName
                .Sex = item.Sex
                .BirthYear = BLLUser.BirthYear(item)
                If item.Country IsNot Nothing Then
                    .Country = New DUI.Country
                    With .Country
                        .Guid = item.Country.Guid
                        .Nom = item.Country.Nom.Tradueix(item.Lang)
                    End With
                End If
                .Zip = item.ZipCod
                .Tel = item.Tel
                .ChildrenCount = item.ChildCount
                .LastChildBirth = item.LastChildBirthday
                .Rol = item.Rol.Id
                If item.Lang Is Nothing Then item.Lang = DTOLang.ESP
                .Lang = item.Lang.Id
                If item.Contact IsNot Nothing Then
                    .ContactGuid = item.Contact.Guid.ToString
                End If
            End With
            Return retval
        End Function

    End Class
End Namespace