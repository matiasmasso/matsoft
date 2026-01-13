Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class ContactController

    Inherits _BaseController

    <HttpGet>
    <Route("api/contact/{guid}")>
    Public Function getContact(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oContact = DAL.ContactLoader.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, oContact)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex)
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/contact/fromGln/{Gln}")>
    Public Function getContact(gln As String) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oGln = DTOEan.Factory(gln)
            Dim oContact = BEBL.Contact.FromGLN(oGln)
            retval = Request.CreateResponse(HttpStatusCode.OK, oContact)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex)
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/contact/fromNif/{emp}/{nif}")>
    Public Function getContact(emp As DTOEmp.Ids, nif As String) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = MyBase.GetEmp(emp)
            Dim oContact = BEBL.Contact.FromNif(oEmp, nif)
            retval = Request.CreateResponse(HttpStatusCode.OK, oContact)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex)
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/contact/search/{user}/{searchby}/{searchkey}")>
    Public Function SearchContact(user As Guid, searchby As DTOContact.SearchBy, searchkey As String) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oUser = BEBL.User.Find(user)
            If oUser Is Nothing Then
            Else
                Dim oContacts = BEBL.Contact.Search(searchkey, oUser, oUser.Emp, searchby)
                retval = Request.CreateResponse(HttpStatusCode.OK, oContacts)
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex)
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/contact/Tabs/{contact}")>
    Public Function Tabs(contact As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oContact As New DTOContact(contact)
            Dim values = BEBL.Contact.Tabs(oContact)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex)
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/contact/Tel/{contact}")>
    Public Function Tel(contact As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oContact As New DTOContact(contact)
            Dim values = BEBL.Contact.Tel(oContact)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex)
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/contact/PreviousContacts/{contact}")>
    Public Function PreviousContacts(contact As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oContact As New DTOContact(contact)
            Dim values = BEBL.Contact.PreviousContacts(oContact)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex)
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/Contact")>
    Public Function Upload() As HttpResponseMessage
        Dim oHelper As New ApiHelper.HttpRequestHelper(HttpContext.Current.Request)
        Dim result As HttpResponseMessage = Nothing

        Try
            Dim exs As New List(Of Exception)
            Dim json As String = oHelper.GetValue("serialized")
            Dim value = ApiHelper.Client.DeSerialize(Of DTOContact)(json, exs)
            If value Is Nothing Then
                result = MyBase.HttpErrorResponseMessage(exs, "Error al deserialitzar la Contact")
            Else
                value.Logo = oHelper.GetImage("Logo")
                If DAL.ContactLoader.Update(value, exs) Then
                    result = Request.CreateResponse(HttpStatusCode.OK, value.Id)
                Else
                    result = MyBase.HttpErrorResponseMessage(exs, "Error al pujar el docfile a DAL.ContactLoader")
                End If
            End If

        Catch ex As Exception
            result = MyBase.HttpErrorResponseMessage(ex, "Error a DAL.ContactLoader")
        End Try

        Return result
    End Function

    <HttpPost>
    <Route("api/Contact/delete")>
    Public Function Delete(<FromBody> value As DTOContact) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.Contact.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar el Contacte")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar el Contacte")
        End Try
        Return retval
    End Function


    <HttpGet>
    <Route("api/contact/defaultUser/{contact}")>
    Public Function getDefaultUser(contact As Guid) As DTOUser
        Dim oContact As New DTOContact(contact)
        Dim retval As DTOUser = BEBL.Contact.DefaultUser(oContact)
        Return retval
    End Function


End Class

Public Class ContactsController
    Inherits _BaseController

    <HttpGet>
    <Route("api/Contacts/FromArea/{emp}/{area}")>
    Public Function FromArea(emp As DTOEmp.Ids, area As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = MyBase.GetEmp(emp)
            Dim oArea = New DTOArea(area)
            Dim values = BEBL.Contacts.All(oEmp, oArea)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les Contactes")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Contacts/FromUser/{user}/{area}")>
    Public Function FromUser(user As Guid, area As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oUser = BEBL.User.Find(user)
            Dim oArea = DTOBaseGuid.Opcional(Of DTOArea)(area)
            Dim values = BEBL.Contacts.All(oUser, oArea)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les Contactes")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Contacts/FromCta/{emp}/{year}/{cta}")>
    Public Function FromCta(emp As DTOEmp.Ids, year As Integer, cta As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = MyBase.GetEmp(emp)
            Dim oExercici As New DTOExercici(oEmp, year)
            Dim oCta As New DTOPgcCta(cta)
            Dim values = BEBL.Contacts.All(oExercici, oCta)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les Contactes")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Contacts/FromClass/{user}/{contactClass}")>
    Public Function FromClass(user As Guid, contactClass As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oUser = BEBL.User.Find(user)
            Dim oClass As New DTOContactClass(contactClass)
            Dim values = BEBL.Contacts.All(oUser, oClass)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les Contactes")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Contacts/FromChannel/{user}/{channel}")>
    Public Function FromChannel(user As Guid, channel As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oUser = BEBL.User.Find(user)
            Dim oChannel As New DTODistributionChannel(channel)
            Dim values = BEBL.Contacts.All(oUser, oChannel)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les Contactes")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Contacts/RaonsSocials/{user}")>
    Public Function RaonsSocials(user As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oUser = BEBL.User.Find(user)
            Dim values = BEBL.Contacts.RaonsSocials(oUser)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les Contactes")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/contacts/search/{user}/{searchBy}")>
    Public Function Search(user As Guid, searchBy As DTOContact.SearchBy, <FromBody> searchkey As String) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oUser = BEBL.User.Find(user)
            Dim values = BEBL.Contacts.Search(oUser, searchkey, searchBy)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les Contactes")
        End Try
        Return retval
    End Function


    <HttpGet>
    <Route("api/Contacts/Zonas/{user}/{country}")>
    Public Function Zonas(user As Guid, country As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oUser = BEBL.User.Find(user)
            Dim oCountry As New DTOCountry(country)
            Dim values = BEBL.Contacts.Zonas(oUser, oCountry)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les Contactes")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/contacts/moveToClass/{contactClass}")>
    Public Function moveToClass(contactClass As Guid, <FromBody> oContacts As List(Of DTOContact)) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim oClass As New DTOContactClass(contactClass)
            If BEBL.Contacts.MoveToClass(exs, oClass, oContacts) Then
                retval = Request.CreateResponse(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "Error al moure les Contactes")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al moure les Contactes")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/contacts/AutoCompleteString/{emp}")>
    Public Function AutoCompleteString(emp As DTOEmp.Ids, <FromBody> searchkey As String) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim oEmp = MyBase.GetEmp(emp)
            Dim values = BEBL.Contacts.AutoCompleteString(oEmp, searchkey)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al AutoCompleteString")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/contacts/rezip/{zipTo}")>
    Public Function reZip(zipTo As Guid, <FromBody> oContacts As List(Of DTOContact)) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim oZip As New DTOZip(zipTo)
            Dim value = BEBL.Contacts.reZip(exs, oZip, oContacts)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al reassignar els contactes a un altre codi postal")
        End Try
        Return retval
    End Function
End Class

