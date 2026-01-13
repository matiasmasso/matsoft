Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Namespace Controllers
    Public Class UserController
        Inherits _BaseController


        <HttpGet>
        <Route("api/User/{guid}")>
        Public Function Find(guid As Guid) As HttpResponseMessage
            Dim retval As HttpResponseMessage = Nothing
            Try
                Dim value = BEBL.User.Find(guid)
                retval = Request.CreateResponse(HttpStatusCode.OK, value)
            Catch ex As Exception
                retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la User")
            End Try
            Return retval
        End Function


        <HttpPost>
        <Route("api/User/Exists/{emp}")>
        Public Function Find(emp As DTOEmp.Ids, <FromBody> emailAddress As String) As HttpResponseMessage
            Dim retval As HttpResponseMessage = Nothing
            Try
                Dim oEmp = MyBase.GetEmp(emp)
                Dim value = BEBL.User.Exists(oEmp, emailAddress)
                retval = Request.CreateResponse(HttpStatusCode.OK, value)
            Catch ex As Exception
                retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir l'usuari")
            End Try
            Return retval
        End Function

        <HttpPost>
        <Route("api/user/{emp}")> 'MAT.NET
        Public Function FromEmail(emp As Integer, <FromBody> email As String) As HttpResponseMessage
            Dim retval As HttpResponseMessage = Nothing
            Try
                Dim oEmp As New DTOEmp(emp)
                Dim value = BEBL.User.FromEmail(oEmp, email)
                retval = Request.CreateResponse(HttpStatusCode.OK, value)
            Catch ex As Exception
                retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir l'usuarir")
            End Try
            Return retval
        End Function

        <HttpPost>
        <Route("api/user")>
        Public Function Update(<FromBody> oUser As DTOUser) As HttpResponseMessage
            Dim exs As New List(Of Exception)
            Dim retval As HttpResponseMessage = Nothing
            Try
                If BEBL.User.Update(oUser, exs) Then
                    MyBase.UpdateUsersCache(oUser)
                    retval = Request.CreateResponse(HttpStatusCode.OK, True)
                Else
                    retval = MyBase.HttpErrorResponseMessage(exs, "error al desar l'usuari")
                End If
            Catch ex As Exception
                retval = MyBase.HttpErrorResponseMessage(ex, "error al desar l'usuari")
            End Try
            Return retval
        End Function

        <HttpPost>
        <Route("api/user/updateContacts")>
        Public Function UpdateContacts(<FromBody> oUser As DTOUser) As HttpResponseMessage
            Dim exs As New List(Of Exception)
            Dim retval As HttpResponseMessage = Nothing
            Try
                If BEBL.User.UpdateContacts(oUser, exs) Then
                    MyBase.UpdateUsersCache(oUser)
                    retval = Request.CreateResponse(HttpStatusCode.OK, True)
                Else
                    retval = MyBase.HttpErrorResponseMessage(exs, "error al desar els contactes de l'usuari")
                End If
            Catch ex As Exception
                retval = MyBase.HttpErrorResponseMessage(ex, "error al desar els contactes de l'usuari")
            End Try
            Return retval
        End Function

        <HttpGet>
        <Route("api/user/lang/{user}/{lang}")>
        Public Function SwitchLang(user As Guid, lang As String) As HttpResponseMessage
            Dim exs As New List(Of Exception)
            Dim retval As HttpResponseMessage = Nothing
            Try
                Dim oUser = BEBL.User.Find(user)
                oUser.Lang = DTOLang.Factory(lang)
                If BEBL.User.Update(oUser, exs) Then
                    retval = Request.CreateResponse(HttpStatusCode.OK, True)
                Else
                    retval = MyBase.HttpErrorResponseMessage(exs, "error al desar l'idioma de l'usuari")
                End If
            Catch ex As Exception
                retval = MyBase.HttpErrorResponseMessage(ex, "error al desar l'usuari")
            End Try
            Return retval
        End Function

        <HttpPost>
        <Route("api/User/delete")>
        Public Function Delete(<FromBody> value As DTOUser) As HttpResponseMessage
            Dim retval As HttpResponseMessage = Nothing
            Try
                Dim exs As New List(Of Exception)
                If BEBL.User.Delete(exs, value) Then
                    retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
                Else
                    retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar l'usuari")
                End If
            Catch ex As Exception
                retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar l'usuari")
            End Try
            Return retval
        End Function



        <HttpPost>
        <Route("api/user/EmailActivationRequest/{emp}")>
        Public Async Function EmailActivationRequest(emp As Integer, <FromBody> oUser As DTOUser) As Threading.Tasks.Task(Of HttpResponseMessage)
            Dim retval As HttpResponseMessage = Nothing
            Try
                Dim exs As New List(Of Exception)
                Dim oEmp = MyBase.GetEmp(emp)
                Dim oMailMessage = BEBL.User.ActivationRequestMailMessage(oEmp, oUser)
                If Await BEBL.MailMessageHelper.Send(oEmp, oMailMessage, exs) Then
                    retval = Request.CreateResponse(HttpStatusCode.OK, True)
                Else
                    retval = MyBase.HttpErrorResponseMessage(exs, "error al enviar la solicitud de activación")
                End If
            Catch ex As Exception
                retval = MyBase.HttpErrorResponseMessage(ex, "error al enviar la solicitud de activación")
            End Try
            Return retval
        End Function



        <HttpGet>
        <Route("api/user/emailPassword/{user}")> 'MAT.NET
        Public Async Function emailPassword(user As Guid) As Threading.Tasks.Task(Of HttpResponseMessage)
            Dim retval As HttpResponseMessage = Nothing
            Try
                Dim exs As New List(Of Exception)
                Dim oUser = BEBL.User.Find(user)
                Dim oMailMessage = BEBL.User.PasswordMailMessage(oUser)
                If Await BEBL.MailMessageHelper.Send(oUser.Emp, oMailMessage, exs) Then
                    retval = Request.CreateResponse(HttpStatusCode.OK, True)
                Else
                    retval = MyBase.HttpErrorResponseMessage(exs, "error al enviar el password")
                End If
            Catch ex As Exception
                retval = MyBase.HttpErrorResponseMessage(ex, "error al enviar el password")
            End Try
            Return retval
        End Function

        <HttpPost>
        <Route("api/user/emailPwd/{emp}")> 'MAT.NET
        Public Async Function emailPwd(emp As Integer, <FromBody> emailAddress As String) As Threading.Tasks.Task(Of HttpResponseMessage)
            Dim retval As HttpResponseMessage = Nothing
            Try
                Dim exs As New List(Of Exception)
                Dim oEmp = MyBase.GetEmp(emp)
                Dim oMailMessage = BEBL.User.PasswordMailMessage(oEmp, emailAddress)
                If Await BEBL.MailMessageHelper.Send(oEmp, oMailMessage, exs) Then
                    retval = Request.CreateResponse(HttpStatusCode.OK, True)
                Else
                    retval = MyBase.HttpErrorResponseMessage(exs, "error al enviar el password")
                End If
            Catch ex As Exception
                retval = MyBase.HttpErrorResponseMessage(ex, "error al enviar el password")
            End Try
            Return retval
        End Function

        <HttpPost>
        <Route("api/user/EmailAddressVerification/{emp}")>
        Public Async Function EmailAddressVerification(emp As Integer, <FromBody> emailAddress As String) As Threading.Tasks.Task(Of HttpResponseMessage)
            Dim retval As HttpResponseMessage = Nothing
            Try
                Dim exs As New List(Of Exception)
                Dim oEmp = MyBase.GetEmp(emp)
                Dim oMailMessage = BEBL.User.EmailAddressVerificationMailMessage(oEmp, emailAddress)
                If Await BEBL.MailMessageHelper.Send(oEmp, oMailMessage, exs) Then
                    retval = Request.CreateResponse(HttpStatusCode.OK, True)
                Else
                    retval = MyBase.HttpErrorResponseMessage(exs, "error al enviar la verificació de email")
                End If
            Catch ex As Exception
                retval = MyBase.HttpErrorResponseMessage(ex, "error al enviar la verificació de email")
            End Try
            Return retval
        End Function


        <HttpGet>
        <Route("api/user/BrandManufacturers/{user}")>
        Public Function BrandManufacturers(user As Guid) As HttpResponseMessage
            Dim retval As HttpResponseMessage = Nothing
            Try
                Dim exs As New List(Of Exception)
                Dim oUser As New DTOUser(user)
                Dim values = BEBL.User.BrandManufacturers(oUser)
                retval = Request.CreateResponse(HttpStatusCode.OK, values)
            Catch ex As Exception
                retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir els proveidors")
            End Try
            Return retval
        End Function


        <HttpGet>
        <Route("api/user/Contacts/{user}")>
        Public Function Contacts(user As Guid) As HttpResponseMessage
            Dim retval As HttpResponseMessage = Nothing
            Try
                Dim exs As New List(Of Exception)
                Dim oUser As New DTOUser(user)
                Dim values = BEBL.User.Contacts(oUser)
                retval = Request.CreateResponse(HttpStatusCode.OK, values)
            Catch ex As Exception
                retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir els contactes")
            End Try
            Return retval
        End Function


        <HttpGet>
        <Route("api/user/CustomersRaonsSocials/{user}")>
        Public Function CustomersRaonsSocials(user As Guid) As HttpResponseMessage
            Dim retval As HttpResponseMessage = Nothing
            Try
                Dim exs As New List(Of Exception)
                Dim oUser As New DTOUser(user)
                Dim values = BEBL.User.CustomersRaonsSocials(oUser)
                retval = Request.CreateResponse(HttpStatusCode.OK, values)
            Catch ex As Exception
                retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir els clients")
            End Try
            Return retval
        End Function


        <HttpGet>
        <Route("api/user/CustomersForBasket/{user}")>
        Public Function CustomersForBasket(user As Guid) As HttpResponseMessage
            Dim retval As HttpResponseMessage = Nothing
            Try
                Dim exs As New List(Of Exception)
                Dim oUser As New DTOUser(user)
                Dim values = BEBL.User.CustomersForBasket(oUser)
                retval = Request.CreateResponse(HttpStatusCode.OK, values)
            Catch ex As Exception
                retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir els clients")
            End Try
            Return retval
        End Function



        <HttpPost>
        <Route("api/user/load")>
        Public Function Load(oUser As DTOUser) As DTOUser
            BEBL.User.Load(oUser)
            Return oUser
        End Function

        <HttpGet>
        <Route("api/User/getProveidor/{user}")>
        Public Function getProveidor(user As Guid) As HttpResponseMessage
            Dim retval As HttpResponseMessage = Nothing
            Try
                Dim oUser As New DTOUser(user)
                Dim value = BEBL.User.GetProveidor(oUser)
                retval = Request.CreateResponse(HttpStatusCode.OK, value)
            Catch ex As Exception
                retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir l'usuari")
            End Try
            Return retval
        End Function

        <HttpGet>
        <Route("api/User/getCustomers/{user}")>
        Public Function getCustomers(user As Guid) As HttpResponseMessage
            Dim retval As HttpResponseMessage = Nothing
            Try
                Dim oUser As New DTOUser(user)
                Dim value = BEBL.User.GetCustomers(oUser)
                retval = Request.CreateResponse(HttpStatusCode.OK, value)
            Catch ex As Exception
                retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir l'usuari")
            End Try
            Return retval
        End Function

        <HttpGet>
        <Route("api/User/getRep/{user}")>
        Public Function getRep(user As Guid) As HttpResponseMessage
            Dim retval As HttpResponseMessage = Nothing
            Try
                Dim oUser As New DTOUser(user)
                Dim value = BEBL.User.GetRep(oUser)
                retval = Request.CreateResponse(HttpStatusCode.OK, value)
            Catch ex As Exception
                retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la User")
            End Try
            Return retval
        End Function

        <HttpGet>
        <Route("api/User/getStaff/{user}")>
        Public Function getStaff(user As Guid) As HttpResponseMessage
            Dim retval As HttpResponseMessage = Nothing
            Try
                Dim oUser As New DTOUser(user)
                Dim value = BEBL.User.GetStaff(oUser)
                retval = Request.CreateResponse(HttpStatusCode.OK, value)
            Catch ex As Exception
                retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la User")
            End Try
            Return retval
        End Function

        <HttpPost>
        <Route("api/user/validate")>
        Public Function ValidatePassword(credencials As DTOUser) As HttpResponseMessage 'iMat, Mat.Net
            Dim retval As HttpResponseMessage = Nothing
            Try

                Dim oEmp = credencials.Emp
                If oEmp Is Nothing Then oEmp = New DTOEmp(DTOEmp.Ids.MatiasMasso)
                Dim value = BEBL.User.Validate(credencials)
                retval = Request.CreateResponse(HttpStatusCode.OK, value)
            Catch ex As Exception
                retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la User")
            End Try
            Return retval

        End Function

#Region "SpvCli"

        <HttpPost>
        <Route("api/email/EmailPwd")>
        Public Async Sub EmailPwd(oGuid As Guid)
            Dim oUser As DTOUser = BEBL.User.Find(oGuid)
            Dim exs As New List(Of Exception)
            Await BEBL.User.EmailPwd(oUser, exs)
        End Sub

        <HttpGet>
        <Route("api/Org/{emp}")>
        Public Function GetOrg(emp As Integer) As DTOContact
            Dim retval As DTOContact = Nothing
            Dim oEmp = BEBL.Emp.Find(emp)
            If oEmp IsNot Nothing Then
                retval = oEmp.Org
                BEBL.Contact.Load(retval)
            End If
            Return retval
        End Function

#End Region

        <HttpPost>
        <Route("api/email/validate")>
        Public Function ValidateEmail(oEmail As DTOUser) As DTOUser
            'UWP
            Dim oEmp As New DTOEmp(DTOEmp.Ids.MatiasMasso)
            Dim retval = DTOUser.Factory(oEmp)
            Dim email As String = oEmail.EmailAddress
            If email = "" Then
                retval.ValidationResult = DTOUser.ValidationResults.emptyEmail
            ElseIf Not DTOUser.CheckEmailSintaxis(email) Then
                retval.ValidationResult = DTOUser.ValidationResults.wrongEmail
            Else
                Try
                    Dim oUser = BEBL.User.FromEmail(oEmp, email)
                    If oUser Is Nothing Then
                        retval.ValidationResult = DTOUser.ValidationResults.emailNotRegistered
                    Else
                        retval.ValidationResult = DTOUser.ValidationResults.success
                    End If

                Catch ex As Exception
                    retval.ValidationResult = DTOUser.ValidationResults.systemError
                End Try

            End If

            Return retval
        End Function

        <HttpPost>
        <Route("api/user/validateActivationCode")>
        Public Function validateActivationCode(<FromBody> oUser As DTOUser) As HttpResponseMessage
            Dim retval As HttpResponseMessage = Nothing
            Try
                Dim success = (DTOUser.VerificationCode(oUser.EmailAddress) = oUser.Password)
                retval = Request.CreateResponse(HttpStatusCode.OK, success)
            Catch ex As Exception
                retval = MyBase.HttpErrorResponseMessage(ex, "error al verificar la activació de l'usuari")
            End Try
            Return retval
        End Function

        <HttpPost>
        <Route("api/user/mailPwd")>
        Public Async Function mailPwd(credencials As DTOUser) As Threading.Tasks.Task(Of DTOUser)
            Dim oEmp As New DTOEmp(DTOEmp.Ids.MatiasMasso)
            Dim retval = DTOUser.Factory(oEmp)
            If credencials.EmailAddress = "" Then
                retval.ValidationResult = DTOUser.ValidationResults.EmptyEmail
            ElseIf Not DTOUser.CheckEmailSintaxis(credencials.EmailAddress) Then
                retval.ValidationResult = DTOUser.ValidationResults.WrongEmail
            Else
                Try
                    Dim oUser = BEBL.User.FromEmail(oEmp, credencials.EmailAddress)
                    If oUser Is Nothing Then
                        retval.ValidationResult = DTOUser.ValidationResults.EmailNotRegistered
                    Else
                        Dim exs As New List(Of Exception)
                        If Await BEBL.User.EmailPwd(oUser, exs) Then
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

    End Class


    Public Class UsersController
        Inherits _BaseController


        <HttpGet>
        <Route("api/Users/fromEmp/{emp}")>
        Public Function fromEmp(emp As DTOEmp.Ids) As HttpResponseMessage
            Dim retval As HttpResponseMessage = Nothing
            Try
                Dim oEmp = MyBase.GetEmp(emp)
                Dim values = BEBL.Users.All(oEmp)
                retval = Request.CreateResponse(HttpStatusCode.OK, values)
            Catch ex As Exception
                retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els usuaris")
            End Try
            Return retval
        End Function

        <HttpPost>
        <Route("api/Users/Search/{emp}")>
        Public Function Search(emp As DTOEmp.Ids, <FromBody> searchKey As String) As HttpResponseMessage
            Dim retval As HttpResponseMessage = Nothing
            Try
                Dim oEmp = New DTOEmp(emp)
                Dim values = BEBL.Users.Search(oEmp, searchKey)
                retval = Request.CreateResponse(HttpStatusCode.OK, values)
            Catch ex As Exception
                retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els usuaris")
            End Try
            Return retval
        End Function

        <HttpGet>
        <Route("api/Users/fromContact/{contact}")>
        Public Function fromContact(contact As Guid) As HttpResponseMessage
            Dim retval As HttpResponseMessage = Nothing
            Try
                Dim oContact As New DTOContact(contact)
                Dim values = BEBL.Users.All(oContact)
                retval = Request.CreateResponse(HttpStatusCode.OK, values)
            Catch ex As Exception
                retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els professionals")
            End Try
            Return retval
        End Function

        <HttpGet>
        <Route("api/Users/professionals/{emp}")>
        Public Function Pros(emp As Integer) As HttpResponseMessage
            Dim retval As HttpResponseMessage = Nothing
            Try
                Dim oEmp = MyBase.GetEmp(emp)
                Dim values = BEBL.Users.Professionals(oEmp)
                retval = Request.CreateResponse(HttpStatusCode.OK, values)
            Catch ex As Exception
                retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els professionals")
            End Try
            Return retval
        End Function

        <HttpPost>
        <Route("api/Users/professionals/excel/{emp}")>
        Public Function professionalsExcel(emp As DTOEmp.Ids, <FromBody> oContacts As List(Of DTOAtlas.Contact)) As HttpResponseMessage
            Dim retval As HttpResponseMessage = Nothing
            Try
                Dim oEmp = MyBase.GetEmp(emp)
                Dim value = BEBL.Users.ProfessionalsExcel(oEmp, oContacts)
                retval = Request.CreateResponse(HttpStatusCode.OK, value)
            Catch ex As Exception
                retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els professionals")
            End Try
            Return retval
        End Function
    End Class
End Namespace